using System;
using System.Linq.Expressions;

public interface ISpecification<TCandidate> where TCandidate : class
    {
        bool IsSatisfiedBy(TCandidate candidate);
        Expression<Func<TCandidate, bool>> ToExpression();
        ISpecification<TCandidate> And(ISpecification<TCandidate> specification);
        ISpecification<TCandidate> Or(ISpecification<TCandidate> specification);
        ISpecification<TCandidate> Not();
    }