using System.Collections.Generic;
using System.Dynamic;
using System.Linq.Expressions;
using System.Reflection;

namespace SqlCommandBuilder.Dynamic
{
    public class DynamicResultRow : ResultRow, IDynamicMetaObjectProvider
    {
        internal DynamicResultRow()
        {
        }

        internal DynamicResultRow(IDictionary<string, object> data)
            : base(data)
        {
        }

        private object GetPropertyValue(string propertyName)
        {
            var value = base[propertyName];
            if (value is IDictionary<string, object>)
                value = new DynamicResultRow(value as IDictionary<string, object>);
            return value;
        }

        public DynamicMetaObject GetMetaObject(Expression parameter)
        {
            return new DynamicRowMetaObject(parameter, this);
        }

        private class DynamicRowMetaObject : DynamicMetaObject
        {
            internal DynamicRowMetaObject(
                Expression parameter,
                DynamicResultRow value)
                : base(parameter, BindingRestrictions.Empty, value)
            {
            }

            public override DynamicMetaObject BindGetMember(GetMemberBinder binder)
            {
                var methodInfo = typeof(DynamicResultRow).GetMethod("GetPropertyValue", BindingFlags.Instance | BindingFlags.NonPublic);
                var arguments = new Expression[]
                {
                    Expression.Constant(binder.Name)
                };

                return new DynamicMetaObject(
                    Expression.Call(Expression.Convert(Expression, LimitType), methodInfo, arguments), 
                    BindingRestrictions.GetTypeRestriction(Expression, LimitType));
            }
        }
    }
}