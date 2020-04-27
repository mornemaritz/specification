using System.Linq;
using AutoFixture;
using NUnit.Framework;

namespace Msq.Specification.UnitTests
{
    [TestFixture]
    public class AllSpecificationTests
    {
        private AllSpecification<TestCandidate> specificationUnderTest;

        private Fixture fixture;

        [SetUp]
        public void SetUp()
        {
            fixture = new Fixture();
            specificationUnderTest = new AllSpecification<TestCandidate>();
        }

        [Test]
        public void AllSpecification_MatchesAll()
        {
            // Arrange 
            var soureCollection = fixture.CreateMany<TestCandidate>(10).AsQueryable();

            // Act
            var actualCollection = soureCollection.Where(specificationUnderTest.ToExpression());

            // Assert
            Assert.AreEqual(10, actualCollection.Count());
        }

        [Test]
        public void AllSpecification_IsSatisfiedByCandidate()
        {
            // Arrange
            var targetCandidate = fixture.Create<TestCandidate>();

            // Act & Assert
            Assert.IsTrue(specificationUnderTest.IsSatisfiedBy(targetCandidate));
        }
    }
}