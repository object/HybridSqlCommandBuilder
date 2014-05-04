using System.Collections.Generic;

namespace SqlCommandBuilder
{
    public class ResultCollection
    {
        protected List<ResultRow> _data;

        public ResultCollection()
        {
            _data = new List<ResultRow>();
        }

        public ResultCollection(IEnumerable<ResultRow> data)
        {
            _data = new List<ResultRow>(data);
        }

        public IEnumerable<ResultRow> AsEnumerable()
        {
            return _data;
        }

        public static explicit operator ResultCollection(List<ResultRow> data)
        {
            return new ResultCollection() { _data = new List<ResultRow>(data) };
        }

        public static explicit operator List<ResultRow>(ResultCollection collection)
        {
            return collection._data;
        }
    }
}