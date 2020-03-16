using NUnit.Framework;

using Game.Models;

namespace UnitTests.Models
{
    [TestFixture]
    class PotionsModelTests
    {
        [Test]
        public void potionsModel_expected_default_potion_should_pass()
        {
            //Arrange

            //Act
            var result = new PotionsModel();
            //Assert
            Assert.AreEqual(10, result.Addition);
            Assert.AreEqual(PotionsEnum.Health, result.potionType);
            Assert.AreEqual("Health.png", result.ImageURI);
        }
    }
}
