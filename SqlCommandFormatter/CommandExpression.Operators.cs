using System;

namespace SqlCommandFormatter
{
    public partial class CommandExpression
    {
        public static implicit operator CommandExpression(bool value) { return CommandExpression.FromValue(value); }
        public static implicit operator CommandExpression(byte value) { return CommandExpression.FromValue(value); }
        public static implicit operator CommandExpression(sbyte value) { return CommandExpression.FromValue(value); }
        public static implicit operator CommandExpression(short value) { return CommandExpression.FromValue(value); }
        public static implicit operator CommandExpression(ushort value) { return CommandExpression.FromValue(value); }
        public static implicit operator CommandExpression(int value) { return CommandExpression.FromValue(value); }
        public static implicit operator CommandExpression(uint value) { return CommandExpression.FromValue(value); }
        public static implicit operator CommandExpression(long value) { return CommandExpression.FromValue(value); }
        public static implicit operator CommandExpression(ulong value) { return CommandExpression.FromValue(value); }
        public static implicit operator CommandExpression(float value) { return CommandExpression.FromValue(value); }
        public static implicit operator CommandExpression(double value) { return CommandExpression.FromValue(value); }
        public static implicit operator CommandExpression(decimal value) { return CommandExpression.FromValue(value); }
        public static implicit operator CommandExpression(DateTime value) { return CommandExpression.FromValue(value); }
        public static implicit operator CommandExpression(DateTimeOffset value) { return CommandExpression.FromValue(value); }
        public static implicit operator CommandExpression(TimeSpan value) { return CommandExpression.FromValue(value); }
        public static implicit operator CommandExpression(Guid value) { return CommandExpression.FromValue(value); }
        public static implicit operator CommandExpression(string value) { return CommandExpression.FromValue(value); }

        public static CommandExpression operator !(CommandExpression expr)
        {
            return new CommandExpression(expr, null, ExpressionOperator.NOT);
        }

        public static CommandExpression operator -(CommandExpression expr)
        {
            return new CommandExpression(expr, null, ExpressionOperator.NEG);
        }

        public static CommandExpression operator ==(CommandExpression expr1, CommandExpression expr2)
        {
            return new CommandExpression(expr1, expr2, ExpressionOperator.EQ);
        }

        public static CommandExpression operator !=(CommandExpression expr1, CommandExpression expr2)
        {
            return new CommandExpression(expr1, expr2, ExpressionOperator.NE);
        }

        public static CommandExpression operator &(CommandExpression expr1, CommandExpression expr2)
        {
            return new CommandExpression(expr1, expr2, ExpressionOperator.AND);
        }

        public static CommandExpression operator |(CommandExpression expr1, CommandExpression expr2)
        {
            return new CommandExpression(expr1, expr2, ExpressionOperator.OR);
        }

        public static CommandExpression operator >(CommandExpression expr1, CommandExpression expr2)
        {
            return new CommandExpression(expr1, expr2, ExpressionOperator.GT);
        }

        public static CommandExpression operator <(CommandExpression expr1, CommandExpression expr2)
        {
            return new CommandExpression(expr1, expr2, ExpressionOperator.LT);
        }

        public static CommandExpression operator >=(CommandExpression expr1, CommandExpression expr2)
        {
            return new CommandExpression(expr1, expr2, ExpressionOperator.GE);
        }

        public static CommandExpression operator <=(CommandExpression expr1, CommandExpression expr2)
        {
            return new CommandExpression(expr1, expr2, ExpressionOperator.LE);
        }

        public static CommandExpression operator +(CommandExpression expr1, CommandExpression expr2)
        {
            return new CommandExpression(expr1, expr2, ExpressionOperator.ADD);
        }

        public static CommandExpression operator -(CommandExpression expr1, CommandExpression expr2)
        {
            return new CommandExpression(expr1, expr2, ExpressionOperator.SUB);
        }

        public static CommandExpression operator *(CommandExpression expr1, CommandExpression expr2)
        {
            return new CommandExpression(expr1, expr2, ExpressionOperator.MUL);
        }

        public static CommandExpression operator /(CommandExpression expr1, CommandExpression expr2)
        {
            return new CommandExpression(expr1, expr2, ExpressionOperator.DIV);
        }

        public static CommandExpression operator %(CommandExpression expr1, CommandExpression expr2)
        {
            return new CommandExpression(expr1, expr2, ExpressionOperator.MOD);
        }

        public static bool operator true(CommandExpression expr)
        {
            return false;
        }

        public static bool operator false(CommandExpression expr)
        {
            return false;
        }
    }
}