using System;
using System.Linq.Expressions;

namespace Msq.Specification
{
    public class AndSpecification<TCandidate> : CompositeSpecification<TCandidate> where TCandidate : class
    {
        private ISpecification<TCandidate> left;
        private ISpecification<TCandidate> right;

        public AndSpecification(ISpecification<TCandidate> left, ISpecification<TCandidate> right)
        {
            this.left = left;
            this.right = right;
        }

        public override Expression<Func<TCandidate, bool>> ToExpression()
        {
            Expression<Func<TCandidate, bool>> leftExpression = left.ToExpression();
            Expression<Func<TCandidate, bool>> rightExpression = right.ToExpression();

            var andExpression = Expression.AndAlso(leftExpression.Body, rightExpression.Body);

            andExpression = (BinaryExpression)CopyParameters(andExpression);
            var finalExpression = Expression.Lambda<Func<TCandidate, bool>>(andExpression, ParameterExpression);

            return finalExpression;
        }
    }
}
