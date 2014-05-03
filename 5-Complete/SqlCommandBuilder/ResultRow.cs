using System.Collections.Generic;

namespace SqlCommandBuilder
{
    public class ResultRow
    {
        protected Dictionary<string, object> _data;

        public ResultRow()
        {
            _data = new Dictionary<string, object>();
        }

        public ResultRow(IDictionary<string, object> data)
        {
            _data = new Dictionary<string, object>(data);
        }

        public object this[string key]
        {
            get
            {
                return _data[key];
            }
            set
            {
                if (_data.ContainsKey(key))
                    _data[key] = value;
                else
                    _data.Add(key, value);
            }
        }

        public IDictionary<string, object> AsDictionary()
        {
            return _data;
        }

        public static explicit operator ResultRow(Dictionary<string, object> data)
        {
            return new ResultRow() { _data = data };
        }

        public static explicit operator Dictionary<string, object>(ResultRow row)
        {
            return row._data;
        }
    }
}