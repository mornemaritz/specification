using System;
using System.Linq.Expressions;

namespace Msq.Specification
{
    public class NotSpecification<TCandidate> : CompositeSpecification<TCandidate> where TCandidate : class
    {
        private ISpecification<TCandidate> other;

        public NotSpecification(ISpecification<TCandidate> other)
        {
            this.other = other;
        }

        public override Expression<Func<TCandidate, bool>> ToExpression()
        {
            var otherExpression = other.ToExpression();

            var notExpression = Expression.Not(otherExpression.Body);

            notExpression = (UnaryExpression)CopyParameters(notExpression);
            var finalExpression = Expression.Lambda<Func<TCandidate, bool>>(notExpression, ParameterExpression);

            return finalExpression;
        }
    }
}
