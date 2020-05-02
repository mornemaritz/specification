using System;
using System.Linq.Expressions;

namespace Msq.Specification.UnitTests
{
    public class PositiveIntegerAttributeSpecification : CompositeSpecification<TestCandidate>
    {
        public override Expression<Func<TestCandidate, bool>> ToExpression()
        {
            return testCandidate => testCandidate.IntegerAtttribute > 0;
        }
    }

    public class IntegerAttributeFilterSpecification : CompositeSpecification<TestCandidate>
    {
        private int filterValue;

        public IntegerAttributeFilterSpecification(int filterValue)
        {
            this.filterValue = filterValue;
        }

        public override Expression<Func<TestCandidate, bool>> ToExpression()
        {
            return testCandidate => testCandidate.IntegerAtttribute == filterValue;
        }
    }
}