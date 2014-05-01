using System;
using System.Collections.Generic;
using System.Linq;

namespace SqlCommandBuilder
{
    public partial class CommandExpression
    {
        internal string Format()
        {
            if (_operator == ExpressionOperator.None)
            {
                return this.Reference != null ?
                    FormatReference() : this.Function != null ?
                    FormatFunction() :
                    FormatValue();
            }
            else if (_operator == ExpressionOperator.NOT || _operator == ExpressionOperator.NEG)
            {
                var left = FormatExpression(_left);
                var op = FormatOperator();
                if (NeedsGrouping(_left))
                    return string.Format("{0}({1})", op, left);
                else
                    return string.Format("{0} {1}", op, left);
            }
            else
            {
                var left = FormatExpression(_left);
                var right = FormatExpression(_right);
                var op = FormatOperator();
                if (NeedsGrouping(_left))
                    return string.Format("({0}){1}{2}", left, op, right);
                else if (NeedsGrouping(_right))
                    return string.Format("{0}{1}({2})", left, op, right);
                else
                    return string.Format("{0}{1}{2}", left, op, right);
            }
        }

        private static string FormatExpression(CommandExpression expr)
        {
            if (ReferenceEquals(expr, null))
            {
                return "null";
            }
            else
            {
                return expr.Format();
            }
        }

        private string FormatReference()
        {
            return this.Reference;
        }

        private string FormatFunction()
        {
            FunctionMapping mapping;
            if (FunctionMapping.SupportedFunctions.TryGetValue(new ExpressionFunction.FunctionCall(this.Function.FunctionName, this.Function.Arguments.Count()), out mapping))
            {
                var mappedFunction = mapping.FunctionMapper(this.Function.FunctionName, _functionCaller.Format(), this.Function.Arguments).Function;
                return string.Format("{0}({1})", mappedFunction.FunctionName,
                    string.Join(",", (IEnumerable<object>)mappedFunction.Arguments.Select(x => FormatExpression(x))));
            }
            else
            {
                throw new NotSupportedException(string.Format("The function {0} is not supported or called with wrong number of arguments", this.Function.FunctionName));
            }
        }

        private string FormatValue()
        {
            return (new ValueFormatter()).FormatExpressionValue(Value);
        }

        private string FormatOperator()
        {
            switch (_operator)
            {
                case ExpressionOperator.AND:
                    return " AND ";
                case ExpressionOperator.OR:
                    return " OR ";
                case ExpressionOperator.NOT:
                    return "NOT ";
                case ExpressionOperator.EQ:
                    return "=";
                case ExpressionOperator.NE:
                    return "<>";
                case ExpressionOperator.GT:
                    return ">";
                case ExpressionOperator.GE:
                    return ">=";
                case ExpressionOperator.LT:
                    return "<";
                case ExpressionOperator.LE:
                    return "<=";
                case ExpressionOperator.ADD:
                    return "+";
                case ExpressionOperator.SUB:
                    return "-";
                case ExpressionOperator.MUL:
                    return "*";
                case ExpressionOperator.DIV:
                    return "/";
                case ExpressionOperator.MOD:
                    return "%";
                case ExpressionOperator.NEG:
                    return "-";
                default:
                    return null;
            }
        }

        private string FormatAsFunction(string objectName)
        {
            FunctionMapping mapping;
            if (FunctionMapping.SupportedFunctions.TryGetValue(new ExpressionFunction.FunctionCall(objectName, 0), out mapping))
            {
                string targetName = _functionCaller.Format();
                var mappedFunction = mapping.FunctionMapper(objectName, targetName, null).Function;
                return string.Format("{0}({1})", mappedFunction.FunctionName, targetName);
            }
            else
            {
                return null;
            }
        }

        private int GetPrecedence(ExpressionOperator op)
        {
            switch (op)
            {
                case ExpressionOperator.NOT:
                case ExpressionOperator.NEG:
                    return 1;
                case ExpressionOperator.MOD:
                case ExpressionOperator.MUL:
                case ExpressionOperator.DIV:
                    return 2;
                case ExpressionOperator.ADD:
                case ExpressionOperator.SUB:
                    return 3;
                case ExpressionOperator.GT:
                case ExpressionOperator.GE:
                case ExpressionOperator.LT:
                case ExpressionOperator.LE:
                    return 4;
                case ExpressionOperator.EQ:
                case ExpressionOperator.NE:
                    return 5;
                case ExpressionOperator.AND:
                    return 6;
                case ExpressionOperator.OR:
                    return 7;
                default:
                    return 0;
            }
        }

        private bool NeedsGrouping(CommandExpression expr)
        {
            if (_operator == ExpressionOperator.None)
                return false;
            if (ReferenceEquals(expr, null))
                return false;
            if (expr._operator == ExpressionOperator.None)
                return false;

            int outerPrecedence = GetPrecedence(_operator);
            int innerPrecedence = GetPrecedence(expr._operator);
            return outerPrecedence < innerPrecedence;
        }
    }
}