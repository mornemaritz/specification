using AutoFixture;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Msq.Specification.UnitTests
{
    [TestFixture]
    public class SpecificationTests
    {
        private PositiveIntegerAttributeSpecification positiveIntegerAttribute;
        private NonEmptyStringAttributeSpecification nonEmptyStringAttribute;
        private IntegerAttributeFilterSpecification integerAttributeFilterSpecification;

        private IQueryable<TestCandidate> queryableCollection;
        private IList<TestCandidate> combinedCollection;

        [SetUp]
        public void SetUp()
        {
            positiveIntegerAttribute = new PositiveIntegerAttributeSpecification();
            nonEmptyStringAttribute = new NonEmptyStringAttributeSpecification();

            combinedCollection = new List<TestCandidate>();

            combinedCollection.AddMany(() => new TestCandidate { IntegerAtttribute = -2, StringAttribute = string.Empty }, 3);
            combinedCollection.AddMany(() => new TestCandidate { IntegerAtttribute = 2, StringAttribute = string.Empty }, 3);
            combinedCollection.AddMany(() => new TestCandidate { IntegerAtttribute = -1, StringAttribute = "NonEmpty" }, 3);
            combinedCollection.AddMany(() => new TestCandidate { IntegerAtttribute = 1, StringAttribute = "NonEmptyu" }, 3);

            queryableCollection = combinedCollection.AsQueryable();
        }

        [Test]
        public void PositiveIntegerAttributeSpecification_WhenAppliedToCollection_FiltersCollection()
        {
            Assert.AreEqual(6, combinedCollection.Count(positiveIntegerAttribute.IsSatisfiedBy));
        }

        [Test]
        public void PositiveIntegerAttributeSpecification_WhenExpressionAppliedToQuerableCollection_FiltersCollection()
        {
            Assert.AreEqual(6, queryableCollection.Where(positiveIntegerAttribute.ToExpression()).Count());
        }

        [Test]
        public void PositiveIntegerAttributeSpecification_IsSatisfiedByMatchingCandidate()
        {
            var matchingCandidate = new TestCandidate { IntegerAtttribute = 3 };

            Assert.That(positiveIntegerAttribute.IsSatisfiedBy(matchingCandidate), Is.True);
        }

        [Test]
        public void PositiveIntegerAttributeSpecification_IsNotSatisfiedByNonMatchingCandidate()
        {
            var matchingCandidate = new TestCandidate { IntegerAtttribute = -3 };

            Assert.That(positiveIntegerAttribute.IsSatisfiedBy(matchingCandidate), Is.False);
        }

        [Test]
        public void PositiveIntegerAttributeSpecification_AndNonEmptyStringAttributeSpecification_IsSatisfiedByMatchingCandidate()
        {
            var andedSpecification = positiveIntegerAttribute
                .And(nonEmptyStringAttribute);

            var candidate = new TestCandidate { IntegerAtttribute = 1, StringAttribute = "test" };

            Assert.That(andedSpecification.IsSatisfiedBy(candidate), Is.True);
        }

        [Test]
        public void PositiveIntegerAttributeSpecification_AndNonEmptyStringAttributeSpecification_WhenAppliedToCollection_FiltersCollection()
        {
            var andedSpecification = positiveIntegerAttribute
                .And(nonEmptyStringAttribute);

            Assert.AreEqual(3, combinedCollection.Count(andedSpecification.IsSatisfiedBy));
        } 

        [Test]
        public void PositiveIntegerAttributeSpecification_AndNonEmptyStringAttributeSpecification_WhenExpressionAppliedToQueryableCollection_FiltersCollection()
        {
            var andedSpecification = positiveIntegerAttribute
                .And(nonEmptyStringAttribute);

            Assert.AreEqual(3, queryableCollection.Where(andedSpecification.ToExpression()).Count());
        }        

        [Test]
        public void PositiveIntegerAttributeSpecification_AndNonEmptyStringAttributeSpecification_IsNotSatisfiedByNonMatchingCandidate()
        {
            var andedSpecification = positiveIntegerAttribute
                .And(nonEmptyStringAttribute);

            var candidate = new TestCandidate { IntegerAtttribute = 1, StringAttribute = string.Empty };

            Assert.IsFalse(andedSpecification.IsSatisfiedBy(candidate));            
        }

        [Test]
        public void PositiveIntegerAttributeSpecification_OrNonEmptyStringAttributeSpecification_IsSatisfiedByMatchingCandidate()
        {
            var oredSpecification = positiveIntegerAttribute
                .Or(nonEmptyStringAttribute);

            var candidate = new TestCandidate { IntegerAtttribute = 1, StringAttribute = string.Empty };

            Assert.IsTrue(oredSpecification.IsSatisfiedBy(candidate));
        }

        [Test]
        public void PositiveIntegerAttributeSpecification_OrNonEmptyStringAttributeSpecification_WhenExpressionAppliedToQueryableCollection_FiltersCollection()
        {
            var oredSpecification = positiveIntegerAttribute
                .Or(nonEmptyStringAttribute);

            Assert.AreEqual(9, queryableCollection.Where(oredSpecification.ToExpression()).Count());
        }

        [Test]
        public void IntegerAttributeFilterSpecification_FiltersOutNonMatchingIntegers()
        {
            integerAttributeFilterSpecification = new IntegerAttributeFilterSpecification(2);

            Assert.AreEqual(3, queryableCollection.Where(integerAttributeFilterSpecification.ToExpression()).Count());
        }
    }
}
