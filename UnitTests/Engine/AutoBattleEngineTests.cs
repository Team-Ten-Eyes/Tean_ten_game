using NUnit.Framework;

using Game.Engine;
using Game.Models;
using System.Threading.Tasks;
using Game.Helpers;
using System.Linq;
using Game.ViewModels;

namespace UnitTests.Engine
{
    [TestFixture]
    public class AutoBattleEngineTests
    {
        AutoBattleEngine Engine;

        [SetUp]
        public void Setup()
        {
            Engine = new AutoBattleEngine();
        }

        [TearDown]
        public void TearDown()
        {
        }

        [Test]
        public void AutoBattleEngine_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = Engine;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void AutoBattleEngine_GetScoreObject_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = Engine.GetScoreObject();

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task AutoBattleEngine_RunAutoBattle_Default_Should_Pass()
        {
            //Arrange

            DiceHelper.EnableForcedRolls();
            DiceHelper.SetForcedRollValue(3);

            var data = new BaseCharacter { Level = 1, MaxHealth = 10 };

            Engine.CharacterList.Add(new PlayerInfoModel(data));
            Engine.CharacterList.Add(new PlayerInfoModel(data));
            Engine.CharacterList.Add(new PlayerInfoModel(data));
            Engine.CharacterList.Add(new PlayerInfoModel(data));
            Engine.CharacterList.Add(new PlayerInfoModel(data));
            Engine.CharacterList.Add(new PlayerInfoModel(data));

            //Act
            var result = await Engine.RunAutoBattle();

            //Reset
            DiceHelper.DisableForcedRolls();

            //Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public async Task AutoBattleEngine_RunAutoBattle_Monsters_1_Should_Pass()
        {
            //Arrange

            // Need to set the Monster count to 1, so the battle goes to Next Round Faster
            Engine.MaxNumberPartyMonsters = 1;
            Engine.MaxNumberPartyCharacters = 1;

            var CharacterPlayerMike = new PlayerInfoModel(
                            new BaseCharacter
                            {
                                Speed = -1,
                                Level = 10,
                                CurrHealth = 11,
                                Experience = 1,
                                //ExperienceRemaining = 1,
                                Name = "Mike",
                                ListOrder = 1,
                            });

            Engine.CharacterList.Add(CharacterPlayerMike);

            //Act
            var result = await Engine.RunAutoBattle();

            //Reset

            //Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public async Task AutoBattleEngine_CreateCharacterParty_Characters_Should_Assign_6()
        {
            //Arrange
            Engine.MaxNumberPartyCharacters = 6;

            CharacterViewModel.Instance.Dataset.Clear();

            await CharacterViewModel.Instance.CreateAsync(new BaseCharacter { Name = "1" });
            await CharacterViewModel.Instance.CreateAsync(new BaseCharacter { Name = "2" });
            await CharacterViewModel.Instance.CreateAsync(new BaseCharacter { Name = "3" });
            await CharacterViewModel.Instance.CreateAsync(new BaseCharacter { Name = "4" });
            await CharacterViewModel.Instance.CreateAsync(new BaseCharacter { Name = "5" });
            await CharacterViewModel.Instance.CreateAsync(new BaseCharacter { Name = "6" });
            await CharacterViewModel.Instance.CreateAsync(new BaseCharacter { Name = "7" });

            //Act
            var result = Engine.CreateCharacterParty();

            //Reset

            //Assert
            Assert.AreEqual(6, Engine.CharacterList.Count());
            Assert.AreEqual("6", Engine.CharacterList.ElementAt(5).Name);
        }

        [Test]
        public void AutoBattleEngine_CreateCharacterParty_Characters_CharacterIndex_None_Should_Create_6()
        {
            //Arrange
            Engine.MaxNumberPartyCharacters = 6;

            CharacterViewModel.Instance.Dataset.Clear();

            //Act
            var result = Engine.CreateCharacterParty();

            //Reset

            //Assert
            Assert.AreEqual(6, Engine.CharacterList.Count());
        }
    }
}