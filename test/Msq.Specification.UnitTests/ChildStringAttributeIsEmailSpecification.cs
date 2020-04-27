using System;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Msq.Specification.UnitTests
{
    public class ChildStringAttributeIsValidEmailSpecification : CompositeSpecification<TestCandidate>
    {
        public override Expression<Func<TestCandidate, bool>> ToExpression()
        {
            return candidate => Regex.IsMatch(candidate.Child.ChildStringAttribute,
                    @"^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,}$",
                    RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
        }
    }
}
