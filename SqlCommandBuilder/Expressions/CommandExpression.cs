using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SqlCommandBuilder
{
    public partial class CommandExpression
    {
        private readonly CommandExpression _functionCaller;
        private readonly CommandExpression _left;
        private readonly CommandExpression _right;
        private readonly ExpressionOperator _operator;

        public string Reference { get; private set; }
        public object Value { get; private set; }
        public ExpressionFunction Function { get; private set; }

        internal CommandExpression()
        {
        }

        protected CommandExpression(object value)
        {
            this.Value = value;
        }

        protected CommandExpression(string reference)
        {
            this.Reference = reference;
        }

        protected CommandExpression(string reference, object value)
        {
            this.Reference = reference;
            this.Value = value;
        }

        protected CommandExpression(ExpressionFunction function)
        {
            this.Function = function;
        }

        protected CommandExpression(CommandExpression left, CommandExpression right, ExpressionOperator expressionOperator)
        {
            _left = left;
            _right = right;
            _operator = expressionOperator;
        }

        protected CommandExpression(CommandExpression caller, string reference)
        {
            _functionCaller = caller;
            this.Reference = reference;
        }

        protected CommandExpression(CommandExpression caller, ExpressionFunction function)
        {
            _functionCaller = caller;
            this.Function = function;
        }

        internal static CommandExpression FromReference(string reference)
        {
            return new CommandExpression(reference);
        }

        internal static CommandExpression FromValue(object value)
        {
            return new CommandExpression(value);
        }

        internal static CommandExpression FromAssignment(string reference, object value)
        {
            return new CommandExpression(reference, value);
        }

        internal static CommandExpression FromFunction(ExpressionFunction function)
        {
            return new CommandExpression(function);
        }

        internal static CommandExpression FromFunction(string functionName, string targetName, IEnumerable<object> arguments)
        {
            return new CommandExpression(
                new CommandExpression(targetName),
                new ExpressionFunction(functionName, arguments));
        }

        internal static CommandExpression FromLinqExpression(Expression expression)
        {
            return ParseLinqExpression(expression);
        }

        public override string ToString()
        {
            return Format();
        }

        public string AsString()
        {
            return ToString();
        }
    }
}