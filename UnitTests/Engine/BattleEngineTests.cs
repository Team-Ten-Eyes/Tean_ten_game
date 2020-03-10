using Game.Engine;
using Game.Models;
using NUnit.Framework;

namespace UnitTests.Engine
{
    [TestFixture]
    public class BattleEngineTests
    {
        BattleEngine Engine;

        [SetUp]
        public void Setup()
        {
            Engine = new BattleEngine();
        }

        [TearDown]
        public void TearDown()
        {
        }

        [Test]
        public void BattleEngine_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = Engine;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void BattleEngine_StartBattle_AutoModel_True_Should_Pass()
        {
            // Arrange

            // Act
            var result = Engine.StartBattle(true);

            // Reset

            // Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(true, Engine.BattleScore.AutoBattle);
        }

        [Test]
        public void BattleEngine_EndBattle_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = Engine.EndBattle();

            // Reset

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void BattleEngine_PopulateCharacterList_Should_Pass()
        {
            // Arrange
            var character = new BaseCharacter();

            // Act
            var result = Engine.PopulateCharacterList(character);

            // Reset

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void BattleEngine_populatePotionsList_Healing_Off_Should_Pass()
        {
            //Arrange
            Engine.RoundHealing = RoundHealingEnum.Healing_off;

            //Act
            var result =Engine.populatePotionsList();

            //Reset

            //Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void BattleEngine_populatePotionsLIst_Healing_Off_Should_fail()
        {
            //Arrange
            Engine.RoundHealing = RoundHealingEnum.Healing_off;

            //Act
            PotionsModel health = new PotionsModel();
            Engine.potionPool.Add(health);
            var result = Engine.populatePotionsList();


            //Reset

            //Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public void BattleEngine_populatePotionsList_Healing_On_Should_Pass()
        {
            //Arrange
            Engine.RoundHealing = RoundHealingEnum.Healing_on;

            //Act
            var result = Engine.populatePotionsList();

            //Reset

            //Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void BattleEngine_populatePotionsList_Healing_On_should_fail()
        {
            //Arrange
            Engine.RoundHealing = RoundHealingEnum.Healing_on;

            //Act
            PotionsModel potion = new PotionsModel();
            Engine.potionPool.Add(potion);

            var result = Engine.populatePotionsList();

            //Reset

            //Assert
            Assert.AreEqual(false, result);
        }
    }
}