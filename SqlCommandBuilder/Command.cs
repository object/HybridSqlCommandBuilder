using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SqlCommandBuilder
{
    public class Command
    {
        private string _table;
        private CommandExpression _tableExpression;
        private string _where;
        private CommandExpression _whereExpression;
        private readonly List<string> _selectColumns = new List<string>();
        private readonly List<KeyValuePair<string, bool>> _orderByColumns = new List<KeyValuePair<string, bool>>();

        public Command()
        {
        }

        public void From(string tableName)
        {
            _table = tableName;
        }

        public void Where(string condition)
        {
            _where = condition;
        }

        public void Where(CommandExpression condition)
        {
            _whereExpression = condition;
        }

        public void Select(IEnumerable<string> columns)
        {
            _selectColumns.AddRange(columns);
        }

        public void Select(params CommandExpression[] columns)
        {
            Select(columns.Select(x => x.Reference));
        }

        public void OrderBy(IEnumerable<string> columns)
        {
            _orderByColumns.AddRange(columns.Select(x => new KeyValuePair<string, bool>(x, false)));
        }

        public void OrderBy(params CommandExpression[] columns)
        {
            OrderBy(columns.Select(x => x.Reference));
        }

        public void OrderByDescending(IEnumerable<string> columns)
        {
            _orderByColumns.AddRange(columns.Select(x => new KeyValuePair<string, bool>(x, true)));
        }

        public void OrderByDescending(params CommandExpression[] columns)
        {
            OrderByDescending(columns.Select(x => x.Reference));
        }

        public override string ToString()
        {
            return Format();
        }

        private string Format()
        {
            EvaluateExpressions();

            var builder = new StringBuilder();

            builder.AppendFormat("SELECT {0} FROM {1}",
                _selectColumns.Any() ? string.Join(",", _selectColumns) : "*",
                _table);

            if (!string.IsNullOrEmpty(_where))
            {
                builder.AppendFormat(" WHERE {0}", _where);
            }

            if (_orderByColumns.Any())
            {
                builder.AppendFormat(" ORDER BY {0}",
                    string.Join(",", _orderByColumns.Select(x => x.Key + (x.Value ? " DESC" : string.Empty))));
            }

            return builder.ToString();
        }

        private void EvaluateExpressions()
        {
            if (!ReferenceEquals(_tableExpression, null))
            {
                From(_tableExpression.AsString());
                _tableExpression = null;
            }

            if (!ReferenceEquals(_whereExpression, null))
            {
                _where = _whereExpression.Format();
                _whereExpression = null;
            }
        }
    }
}
