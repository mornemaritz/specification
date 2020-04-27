using System;
using System.Linq.Expressions;

namespace Msq.Specification
{
    public abstract class CompositeSpecification<TCandidate> : ISpecification<TCandidate> where TCandidate : class
    {
        protected ParameterExpression ParameterExpression = Expression.Parameter(typeof(TCandidate));
        public abstract Expression<Func<TCandidate, bool>> ToExpression();
        public bool IsSatisfiedBy(TCandidate candidate)
        {
            var predicate = ToExpression().Compile();

            return predicate(candidate);
        }

        protected Expression CopyParameters(Expression binaryExpression)
        {
            var parameterCopier = new ParameterExpressionVisitor(ParameterExpression);

            return parameterCopier.Visit(binaryExpression);
        }

        public ISpecification<TCandidate> And(ISpecification<TCandidate> other) => new AndSpecification<TCandidate>(this, other);
        public ISpecification<TCandidate> Or(ISpecification<TCandidate> other) => new OrSpecification<TCandidate>(this, other);
        public ISpecification<TCandidate> Not() => new NotSpecification<TCandidate>(this);
    }
}
