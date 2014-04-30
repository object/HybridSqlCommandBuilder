using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace SqlCommandFormatter
{
    public partial class CommandExpression
    {
        private static CommandExpression ParseLinqExpression(Expression expression)
        {
            switch (expression.NodeType)
            {
                case ExpressionType.MemberAccess:
                    return ParseMemberExpression(expression);

                case ExpressionType.Call:
                    return ParseCallExpression(expression);

                case ExpressionType.Lambda:
                    return ParseLambdaExpression(expression);

                case ExpressionType.Constant:
                    return ParseConstantExpression(expression);

                case ExpressionType.Not:
                case ExpressionType.Convert:
                case ExpressionType.Negate:
                    return ParseUnaryExpression(expression);

                case ExpressionType.Equal:
                case ExpressionType.NotEqual:
                case ExpressionType.LessThan:
                case ExpressionType.LessThanOrEqual:
                case ExpressionType.GreaterThan:
                case ExpressionType.GreaterThanOrEqual:
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                case ExpressionType.Add:
                case ExpressionType.AddChecked:
                case ExpressionType.Subtract:
                case ExpressionType.SubtractChecked:
                case ExpressionType.Multiply:
                case ExpressionType.MultiplyChecked:
                case ExpressionType.Divide:
                case ExpressionType.Modulo:
                    return ParseBinaryExpression(expression);

                case ExpressionType.New:
                    return ParseNewExpression(expression);
            }

            throw Utils.NotSupportedExpression(expression);
        }

        private static CommandExpression ParseMemberExpression(Expression expression, string memberNames = null)
        {
            var memberExpression = expression as MemberExpression;
            if (memberExpression.Expression == null)
            {
                return new CommandExpression(EvaluateStaticMember(memberExpression));
            }
            else
            {
                var memberName = memberExpression.Member.Name;
                memberNames = memberNames == null ? memberName : string.Join(".", memberName, memberNames);
                switch (memberExpression.Expression.NodeType)
                {
                    case ExpressionType.Parameter:
                        return new CommandExpression(memberName);
                    case ExpressionType.Constant:
                        return ParseConstantExpression(memberExpression.Expression, memberNames);
                    case ExpressionType.MemberAccess:
                        FunctionMapping mapping;
                        if (FunctionMapping.SupportedFunctions.TryGetValue(
                            new ExpressionFunction.FunctionCall(memberName, 0), out mapping))
                        {
                            var contextExpression = memberExpression.Expression as MemberExpression;
                            return FromFunction(memberName, contextExpression.Member.Name, new List<object>());
                        }
                        else
                        {
                            return ParseMemberExpression(memberExpression.Expression as MemberExpression, memberNames);
                        }

                    default:
                        throw Utils.NotSupportedExpression(expression);
                }
            }
        }

        private static CommandExpression ParseCallExpression(Expression expression)
        {
            var callExpression = expression as MethodCallExpression;
            if (callExpression.Object == null)
            {
                var target = callExpression.Arguments.FirstOrDefault();
                if (target != null)
                {
                    return FromFunction(callExpression.Method.Name, ParseLinqExpression(target).Reference,
                        callExpression.Arguments.Skip(1).Select(x => ParseConstantExpression(x)));
                }
                else
                {
                    throw Utils.NotSupportedExpression(expression);
                }
            }
            else
            {
                var memberExpression = Utils.CastExpressionWithTypeCheck<MemberExpression>(callExpression.Object);
                var arguments = new List<object>();
                arguments.AddRange(callExpression.Arguments.Select(ParseCallArgumentExpression));

                return FromFunction(callExpression.Method.Name, memberExpression.Member.Name, arguments);
            }
        }

        private static CommandExpression ParseCallArgumentExpression(Expression expression)
        {
            switch (expression.NodeType)
            {
                case ExpressionType.Constant:
                    return new CommandExpression(ParseConstantExpression(expression).Value);
                case ExpressionType.MemberAccess:
                    return new CommandExpression(ParseMemberExpression(expression).Value);

                default:
                    throw Utils.NotSupportedExpression(expression);
            }
        }

        private static CommandExpression ParseLambdaExpression(Expression expression)
        {
            var lambdaExpression = expression as LambdaExpression;

            return ParseLinqExpression(lambdaExpression.Body);
        }

        private static CommandExpression ParseConstantExpression(Expression expression, string memberNames = null)
        {
            var constExpression = expression as ConstantExpression;

            if (constExpression.Value is Expression)
            {
                return ParseConstantExpression(constExpression.Value as Expression, memberNames);
            }
            else
            {
                if (constExpression.Type.IsValue() || constExpression.Type == typeof(string))
                {
                    return new CommandExpression(constExpression.Value);
                }
                else
                {
                    return new CommandExpression(EvaluateConstValue(
                        constExpression.Type, constExpression.Value,
                        memberNames == null ? new List<string>() : memberNames.Split('.').ToList()));
                }
            }
        }

        private static CommandExpression ParseUnaryExpression(Expression expression)
        {
            var unaryExpression = (expression as UnaryExpression).Operand;
            var CommandExpression = ParseLinqExpression(unaryExpression);
            switch (expression.NodeType)
            {
                case ExpressionType.Not:
                    return !CommandExpression;
                case ExpressionType.Convert:
                    return CommandExpression;
                case ExpressionType.Negate:
                    return -CommandExpression;
            }

            throw Utils.NotSupportedExpression(expression);
        }

        private static CommandExpression ParseBinaryExpression(Expression expression)
        {
            var binaryExpression = expression as BinaryExpression;
            var leftExpression = ParseLinqExpression(binaryExpression.Left);
            var rightExpression = ParseLinqExpression(binaryExpression.Right);

            switch (expression.NodeType)
            {
                case ExpressionType.Equal:
                    return leftExpression == rightExpression;
                case ExpressionType.NotEqual:
                    return leftExpression != rightExpression;
                case ExpressionType.LessThan:
                    return leftExpression < rightExpression;
                case ExpressionType.LessThanOrEqual:
                    return leftExpression <= rightExpression;
                case ExpressionType.GreaterThan:
                    return leftExpression > rightExpression;
                case ExpressionType.GreaterThanOrEqual:
                    return leftExpression >= rightExpression;
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                    return leftExpression && rightExpression;
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                    return leftExpression || rightExpression;
                case ExpressionType.Add:
                case ExpressionType.AddChecked:
                    return leftExpression + rightExpression;
                case ExpressionType.Subtract:
                case ExpressionType.SubtractChecked:
                    return leftExpression - rightExpression;
                case ExpressionType.Multiply:
                case ExpressionType.MultiplyChecked:
                    return leftExpression * rightExpression;
                case ExpressionType.Divide:
                    return leftExpression / rightExpression;
                case ExpressionType.Modulo:
                    return leftExpression % rightExpression;
            }

            throw Utils.NotSupportedExpression(expression);
        }

        private static CommandExpression ParseNewExpression(Expression expression)
        {
            var newExpression = expression as NewExpression;
            return FromValue(newExpression.Constructor.Invoke(newExpression.Arguments.Select(x => ParseLinqExpression(x).Value).ToArray()));
        }

        private static object EvaluateStaticMember(MemberExpression expression)
        {
            object value = null;
            if (expression.Member is FieldInfo)
            {
                var fi = (FieldInfo)expression.Member;
                value = fi.GetValue(null);
            }
            else if (expression.Member is PropertyInfo)
            {
                var pi = (PropertyInfo)expression.Member;
                if (pi.GetIndexParameters().Length != 0)
                    throw new ArgumentException("cannot eliminate closure references to indexed properties");
                value = pi.GetValue(null, null);
            }
            return value;
        }

        private static object EvaluateConstValue(Type type, object value, IList<string> memberNames)
        {
            string memberName = null;
            if (memberNames.Any())
            {
                memberName = memberNames.First();
                memberNames = memberNames.Skip(1).ToList();
            }

            Type itemType;
            object itemValue;
            if (type.GetDeclaredProperties().Any(x => x.Name == memberName))
            {
                var property = type.GetDeclaredProperties().Single(x => x.Name == memberName);
                itemType = property.PropertyType;
                itemValue = property.GetValue(value, null);
            }
            else if (type.GetDeclaredFields().Any(x => x.Name == memberName))
            {
                var field = type.GetDeclaredFields().Single(x => x.Name == memberName);
                itemType = field.FieldType;
                itemValue = field.GetValue(value);
            }
            else
            {
                return value;
            }

            return EvaluateConstValue(itemType, itemValue, memberNames);
        }
    }
}