using AutoFixture;
using NUnit.Framework;
using System.Linq;

namespace Msq.Specification.UnitTests
{
    [TestFixture]
    public class NoneSpecificationTests
    {
        private NoneSpecification<TestCandidate> specificationUnderTest;

        private Fixture fixture;

        [SetUp]
        public void SetUp()
        {
            fixture = new Fixture();
            specificationUnderTest = new NoneSpecification<TestCandidate>();
        }

        [Test]
        public void NoneSpecification_MatchesNone()
        {
            // Arrange 
            var soureCollection = fixture.CreateMany<TestCandidate>(10).AsQueryable();

            // Act
            var actualCollection = soureCollection.Where(specificationUnderTest.ToExpression());

            // Assert
            Assert.AreEqual(0, actualCollection.Count());
        }

        [Test]
        public void NoneSpecification_IsNotSatisfiedByCandidate()
        {
            // Arrange
            var targetCandidate = fixture.Create<TestCandidate>();

            // Act & Assert
            Assert.IsFalse(specificationUnderTest.IsSatisfiedBy(targetCandidate));
        }
    }
}
