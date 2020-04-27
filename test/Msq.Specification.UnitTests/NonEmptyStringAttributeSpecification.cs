using System;
using System.Linq.Expressions;

namespace Msq.Specification.UnitTests
{
    public class NonEmptyStringAttributeSpecification : CompositeSpecification<TestCandidate>
    {
        public override Expression<Func<TestCandidate, bool>> ToExpression()
        {
            return candidate => !string.IsNullOrWhiteSpace(candidate.StringAttribute);
        }
    }
}
