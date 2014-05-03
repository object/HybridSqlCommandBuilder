using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SqlCommandBuilder
{
    public class CommandBuilder
    {
        public ICommandBuilder<Record> From(string tableName)
        {
            return new CommandBuilder<Record>(tableName);
        }

        public ICommandBuilder<T> From<T>()
        {
            return new CommandBuilder<T>(typeof(T).Name);
        }

        public ICommandBuilder<Record> From(CommandExpression expression)
        {
            return new CommandBuilder<Record>(expression);
        }
    }

    class CommandBuilder<T> : ICommandBuilder<T>
    {
        private readonly Command _command = new Command();

        public CommandBuilder(string tableName)
        {
            _command.From(tableName);
        }

        public CommandBuilder(CommandExpression expression)
        {
            _command.From(expression.Reference);
        }

        public Command Build()
        {
            return _command;
        }

        public ICommandBuilder<T> Where(string condition)
        {
            _command.Where(condition);
            return this;
        }

        public ICommandBuilder<T> Where(Expression<Func<T, bool>> expression)
        {
            _command.Where(CommandExpression.FromLinqExpression(expression.Body));
            return this;
        }

        public ICommandBuilder<T> Where(CommandExpression expression)
        {
            _command.Where(expression);
            return this;
        }

        public ICommandBuilder<T> Select(IEnumerable<string> columns)
        {
            _command.Select(columns);
            return this;
        }

        public ICommandBuilder<T> Select(params string[] columns)
        {
            _command.Select(columns);
            return this;
        }

        public ICommandBuilder<T> Select(Expression<Func<T, object>> expression)
        {
            _command.Select(ExtractColumnNames(expression));
            return this;
        }

        public ICommandBuilder<T> Select(params CommandExpression[] columns)
        {
            _command.Select(columns);
            return this;
        }

        public ICommandBuilder<T> OrderBy(params string[] columns)
        {
            _command.OrderBy(columns);
            return this;
        }

        public ICommandBuilder<T> OrderBy(Expression<Func<T, object>> expression)
        {
            _command.OrderBy(ExtractColumnNames(expression));
            return this;
        }

        public ICommandBuilder<T> OrderBy(params CommandExpression[] columns)
        {
            _command.OrderBy(columns);
            return this;
        }

        public ICommandBuilder<T> OrderByDescending(params string[] columns)
        {
            _command.OrderByDescending(columns);
            return this;
        }

        public ICommandBuilder<T> OrderByDescending(Expression<Func<T, object>> expression)
        {
            _command.OrderByDescending(ExtractColumnNames(expression));
            return this;
        }

        public ICommandBuilder<T> OrderByDescending(params CommandExpression[] columns)
        {
            _command.OrderByDescending(columns);
            return this;
        }

        private static IEnumerable<string> ExtractColumnNames(Expression<Func<T, object>> expression)
        {
            var lambdaExpression = Utils.CastExpressionWithTypeCheck<LambdaExpression>(expression);
            switch (lambdaExpression.Body.NodeType)
            {
                case ExpressionType.MemberAccess:
                case ExpressionType.Convert:
                    return new[] { ExtractColumnName(lambdaExpression.Body) };

                case ExpressionType.New:
                    var newExpression = lambdaExpression.Body as NewExpression;
                    return newExpression.Arguments.Select(ExtractColumnName);

                default:
                    throw Utils.NotSupportedExpression(lambdaExpression.Body);
            }
        }

        private static string ExtractColumnName(Expression expression)
        {
            switch (expression.NodeType)
            {
                case ExpressionType.MemberAccess:
                    return (expression as MemberExpression).Member.Name;

                case ExpressionType.Convert:
                    return ExtractColumnName((expression as UnaryExpression).Operand);

                case ExpressionType.Lambda:
                    return ExtractColumnName((expression as LambdaExpression).Body);

                default:
                    throw Utils.NotSupportedExpression(expression);
            }
        }
    }
}