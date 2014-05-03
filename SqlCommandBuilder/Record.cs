using System.Collections.Generic;

namespace SqlCommandBuilder
{
    public class Record
    {
        protected Dictionary<string, object> _data;

        public Record()
        {
            _data = new Dictionary<string, object>();
        }

        public Record(IDictionary<string, object> entry)
        {
            _data = new Dictionary<string, object>(entry);
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

        public static explicit operator Record(Dictionary<string, object> entry)
        {
            return new Record() { _data = entry };
        }

        public static explicit operator Dictionary<string, object>(Record entry)
        {
            return entry._data;
        }
    }
}