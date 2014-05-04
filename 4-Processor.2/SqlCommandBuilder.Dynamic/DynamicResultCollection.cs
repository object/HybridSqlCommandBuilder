using System.Collections.Generic;
using System.Dynamic;
using System.Linq.Expressions;
using System.Reflection;

namespace SqlCommandBuilder.Dynamic
{
    public class DynamicResultCollection : ResultCollection, IDynamicMetaObjectProvider
    {
        internal DynamicResultCollection()
        {
        }

        internal DynamicResultCollection(IEnumerable<ResultRow> data)
            : base(data)
        {
        }

        public DynamicMetaObject GetMetaObject(Expression parameter)
        {
            return new DynamicResultCollectionMetaObject(parameter, this);
        }

        private class DynamicResultCollectionMetaObject : DynamicMetaObject
        {
            internal DynamicResultCollectionMetaObject(
                Expression parameter,
                DynamicResultCollection value)
                : base(parameter, BindingRestrictions.Empty, value)
            {
            }
        }
    }
}