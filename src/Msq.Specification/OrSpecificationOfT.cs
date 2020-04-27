using System;
using System.Linq.Expressions;

namespace Msq.Specification
{
    public class OrSpecification<TCandidate> : CompositeSpecification<TCandidate> where TCandidate : class
    {
        private ISpecification<TCandidate> left;
        private ISpecification<TCandidate> right;

        public OrSpecification(ISpecification<TCandidate> left, ISpecification<TCandidate> right)
        {
            this.left = left;
            this.right = right;
        }

        public override Expression<Func<TCandidate, bool>> ToExpression()
        {
            var leftExpression = left.ToExpression();
            var rightExpression = right.ToExpression();

            var andExpression = Expression.OrElse(leftExpression.Body, rightExpression.Body);

            andExpression = (BinaryExpression)CopyParameters(andExpression);
            var finalExpression = Expression.Lambda<Func<TCandidate, bool>>(andExpression, ParameterExpression);

            return finalExpression;
        }
    }
}
