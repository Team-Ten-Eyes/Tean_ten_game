using NUnit.Framework;

using Game.Models;

namespace UnitTests.Models
{
    [TestFixture]
    class CharacterTypeEnumTests
    {
        [Test]
        public void CharacterTypeEnumExtensionsTests_Unknown_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = CharacterTypeEnum.Unknown.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Player", result);
        }

        [Test]
        public void CharacterJobEnumExtensionsTests_Bravery_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = CharacterTypeEnum.Bravery.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Bravery", result);
        }

        [Test]
        public void CharacterJobEnumExtensionsTests_Creativity_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = CharacterTypeEnum.Creativity.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Creativity", result);
        }

        [Test]
        public void CharacterJobEnumExtensionsTests_Cunning_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = CharacterTypeEnum.Cunning.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Cunning", result);
        }
    }
}
