using System.Collections.Generic;
using System.Dynamic;
using System.Linq.Expressions;
using System.Reflection;

namespace SqlCommandBuilder.Dynamic
{
    public class DynamicRecord : Record, IDynamicMetaObjectProvider
    {
        internal DynamicRecord()
        {
        }

        internal DynamicRecord(IDictionary<string, object> entry)
            : base(entry)
        {
        }

        private object GetEntryValue(string propertyName)
        {
            var value = base[propertyName];
            if (value is IDictionary<string, object>)
                value = new DynamicRecord(value as IDictionary<string, object>);
            return value;
        }

        public DynamicMetaObject GetMetaObject(Expression parameter)
        {
            return new DynamicEntryMetaObject(parameter, this);
        }

        private class DynamicEntryMetaObject : DynamicMetaObject
        {
            internal DynamicEntryMetaObject(
                Expression parameter,
                DynamicRecord value)
                : base(parameter, BindingRestrictions.Empty, value)
            {
            }

            public override DynamicMetaObject BindGetMember(GetMemberBinder binder)
            {
                var methodInfo = typeof(DynamicRecord).GetMethod("GetEntryValue", BindingFlags.Instance | BindingFlags.NonPublic);
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
                    ? (this.Value as Record).AsDictionary().ToObject(binder.Type)
                    : null;

                return new DynamicMetaObject(
                    Expression.Constant(value),
                    BindingRestrictions.GetTypeRestriction(Expression, LimitType));
            }

            public override DynamicMetaObject BindInvoke(InvokeBinder binder, DynamicMetaObject[] args)
            {
                return base.BindInvoke(binder, args);
            }

            public override DynamicMetaObject BindInvokeMember(InvokeMemberBinder binder, DynamicMetaObject[] args)
            {
                return base.BindInvokeMember(binder, args);
            }

            public override DynamicMetaObject BindCreateInstance(CreateInstanceBinder binder, DynamicMetaObject[] args)
            {
                return base.BindCreateInstance(binder, args);
            }
        }
    }
}