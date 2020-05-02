using NUnit.Framework;

namespace Msq.Specification.UnitTests
{
    [TestFixture]
    public class NotSpecificationTests
    {
        private ISpecification<TestCandidate> originalSpecification;
        private ISpecification<TestCandidate> specificationUnderTest;

        private TestCandidate candidate;

        [Test]
        public void NegatesSpecifiedSpecification()
        {
            // Arrange
            candidate = new TestCandidate { StringAttribute = "NonEmpty", IntegerAtttribute = 5 };
            originalSpecification = new NonEmptyStringAttributeSpecification();

            specificationUnderTest = new NotSpecification<TestCandidate>(originalSpecification);

            // Act & Assert
            Assert.That(specificationUnderTest.IsSatisfiedBy(candidate), Is.False.And.Not.EqualTo(originalSpecification.IsSatisfiedBy(candidate)));
        }
    }
}