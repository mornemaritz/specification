using System;
using System.Linq.Expressions;

namespace Msq.Specification
{
    public class NoneSpecification<T> : CompositeSpecification<T> where T : class
    {
        public override Expression<Func<T, bool>> ToExpression()
        {
            return e => false;
        }
    }
}
