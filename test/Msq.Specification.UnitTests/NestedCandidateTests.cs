using AutoFixture;
using NUnit.Framework;

namespace Msq.Specification.UnitTests
{
    [TestFixture]
    public class NestedCandidateTests
    {
        private ChildStringAttributeIsValidEmailSpecification specificationUnderTest;

        private Fixture fixture;

        [SetUp]
        public void SetUp()
        {
            fixture = new Fixture();
            specificationUnderTest = new ChildStringAttributeIsValidEmailSpecification();
        }

        [Test]
        public void ChildStringAttributeIsValidEmailSpecification_IsSatisfiedByCandidateWithValidChildEmail()
        {
            // Arrange
            var child = fixture.Build<ChildTestCandidate>()
                .With(c => c.ChildStringAttribute, "name@mail.com")
                .Create();

            var targetCandidate = fixture.Build<TestCandidate>()
                .With(c => c.Child, child)
                .Create();

            // Act & Assert
            Assert.IsTrue(specificationUnderTest.IsSatisfiedBy(targetCandidate));
        }

        [Test]
        public void ChildStringAttributeIsValidEmailSpecification_IsNotSatisfiedByCandidateWithInValidChildEmail()
        {
            // Arrange
            var child = fixture.Build<ChildTestCandidate>()
                .With(c => c.ChildStringAttribute, "invalid")
                .Create();

            var targetCandidate = fixture.Build<TestCandidate>()
                .With(c => c.Child, child)
                .Create();

            // Act & Assert
            Assert.IsFalse(specificationUnderTest.IsSatisfiedBy(targetCandidate));
        }
    }
}
