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

            public override DynamicMetaObject BindGetMember(GetMemberBinder binder)
            {
                var methodInfo = typeof(DynamicResultCollection).GetMethod("GetEntryValue", BindingFlags.Instance | BindingFlags.NonPublic);
                var arguments = new Expression[]
                {
                    Expression.Constant(binder.Name)
                };

                return new DynamicMetaObject(
                    Expression.Call(Expression.Convert(Expression, LimitType), methodInfo, arguments),
                    BindingRestrictions.GetTypeRestriction(Expression, LimitType));
            }

            public override DynamicMetaObject BindConvert(ConvertBinder binder)
            {
                var value = this.HasValue
                    ? (this.Value as DynamicResultCollection).AsEnumerable().ToObject(binder.Type, true)
                    : null;

                return new DynamicMetaObject(
                    Expression.Constant(value),
                    BindingRestrictions.GetTypeRestriction(Expression, LimitType));
            }
        }
    }
}