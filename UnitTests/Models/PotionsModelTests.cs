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

        [Test]
        public void potionModel_testing_update_function_should_pass()
        {
            //Arrange
            PotionsModel origonal = new PotionsModel();
            PotionsModel updatePotion= new PotionsModel();
            updatePotion.ImageURI = "Mana.png";
            updatePotion.potionType = PotionsEnum.Mana;
            updatePotion.Addition = 5;

            //Act
            origonal.Update(updatePotion);

            //Assert
            Assert.AreEqual(updatePotion.ImageURI, origonal.ImageURI);
            Assert.AreEqual(updatePotion.potionType, origonal.potionType);
            Assert.AreEqual(updatePotion.Addition, origonal.Addition);
        }

        [Test]
        public void potionModel_getType_expected_type_for_default_should_pass()
        {
            //Arrange
            var update = new PotionsModel();

            //Act
            var result = update.GetPotionType();

            //Assert
            Assert.AreEqual(PotionsEnum.Health, result);
        }
    }
}
