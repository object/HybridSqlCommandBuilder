using System.Dynamic;

namespace SqlCommandFormatter.Dynamic
{
    public class DynamicCommandExpression : CommandExpression, IDynamicMetaObjectProvider
    {
        public DynamicMetaObject GetMetaObject(System.Linq.Expressions.Expression parameter)
        {
            throw new System.NotImplementedException();
        }
    }
}