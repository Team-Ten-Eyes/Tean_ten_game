using NUnit.Framework;

using Game.Helpers;
using Game.Models;
using Game.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace UnitTests.Helpers
{
    [TestFixture]
    public class RandomPlayerHelperTests
    {
        [Test]
        public void RandomPlayerHelper_GetAbilityValue_2_Should_Return_2()
        {
            // Arrange
            DiceHelper.EnableForcedRolls();
            DiceHelper.SetForcedRollValue(2);

            // Act
            var result = RandomPlayerHelper.GetAbilityValue();

            // Reset
            DiceHelper.DisableForcedRolls();

            // Assert
            Assert.AreEqual(2 - 1, result);
        }

        [Test]
        public void RandomPlayerHelper_GetLevel_2_Should_Return_2()
        {
            // Arrange
            DiceHelper.EnableForcedRolls();
            DiceHelper.SetForcedRollValue(2);

            // Act
            var result = RandomPlayerHelper.GetLevel();

            // Reset
            DiceHelper.DisableForcedRolls();

            // Assert
            Assert.AreEqual(2, result);
        }

        [Test]
        public void RandomPlayerHelper_GetHealth_2_Should_Return_2()
        {
            // Arrange
            DiceHelper.EnableForcedRolls();
            DiceHelper.SetForcedRollValue(2);

            // Act
            var result = RandomPlayerHelper.GetHealth(1);

            // Reset
            DiceHelper.DisableForcedRolls();

            // Assert
            Assert.AreEqual(2, result);
        }

        [Test]
        public void RandomPlayerHelper_GetMonsterName_2_Should_Return_2()
        {
            // Arrange
            DiceHelper.EnableForcedRolls();
            DiceHelper.SetForcedRollValue(2);

            // Act
            var result = RandomPlayerHelper.GetMonsterName();

            // Reset
            DiceHelper.DisableForcedRolls();

            // Assert
            Assert.AreEqual("Deg", result);
        }

        [Test]
        public void RandomPlayerHelper_GetMonsterDescription_2_Should_Return_2()
        {
            // Arrange
            DiceHelper.EnableForcedRolls();
            DiceHelper.SetForcedRollValue(2);

            // Act
            var result = RandomPlayerHelper.GetMonsterDescription();

            // Reset
            DiceHelper.DisableForcedRolls();

            // Assert
            Assert.AreEqual("the Elf hater", result);
        }

        [Test]
        public void RandomPlayerHelper_GetCharacterDescription_2_Should_Return_2()
        {
            // Arrange
            DiceHelper.EnableForcedRolls();
            DiceHelper.SetForcedRollValue(2);

            // Act
            var result = RandomPlayerHelper.GetCharacterDescription();

            // Reset
            DiceHelper.DisableForcedRolls();

            // Assert
            Assert.AreEqual("the awesome", result);
        }

        [Test]
        public void RandomPlayerHelper_GetCharacterName_2_Should_Return_2()
        {
            // Arrange
            DiceHelper.EnableForcedRolls();
            DiceHelper.SetForcedRollValue(2);

            // Act
            var result = RandomPlayerHelper.GetCharacterName();

            // Reset
            DiceHelper.DisableForcedRolls();

            // Assert
            Assert.AreEqual("Doug", result);
        }

        [Test]
        public void RandomPlayerHelper_GetItem_2_Should_Return_2()
        {
            // Arrange
            DiceHelper.EnableForcedRolls();
            DiceHelper.SetForcedRollValue(2);

            // Act
            var result = RandomPlayerHelper.GetItem(Game.Models.ItemLocationEnum.Feet);

            // Reset
            DiceHelper.DisableForcedRolls();

            // Assert
            Assert.AreNotEqual(null, result);
        }

        [Test]
        public void RandomPlayerHelper_GetMonsterDifficultyValue_Should_Pass()
        {
            // Arrange
            DiceHelper.EnableForcedRolls();
            DiceHelper.SetForcedRollValue(2);

            // Act
            var result = RandomPlayerHelper.GetMonsterDifficultyValue();

            // Reset
            DiceHelper.DisableForcedRolls();

            // Assert
            Assert.AreEqual(DifficultyEnum.Average, result);
        }

        [Test]
        public void RandomPlayerHelper_GetMonsterImage_2_Should_Return_2()
        {
            // Arrange
            DiceHelper.EnableForcedRolls();
            DiceHelper.SetForcedRollValue(2);

            // Act
            var result = RandomPlayerHelper.GetMonsterImage();

            // Reset
            DiceHelper.DisableForcedRolls();

            // Assert
            Assert.AreEqual("troll2.png", result);
        }

        [Test]
        public void RandomPlayerHelper_GetCharacterImage_2_Should_Return_2()
        {
            // Arrange
            DiceHelper.EnableForcedRolls();
            DiceHelper.SetForcedRollValue(2);

            // Act
            var result = RandomPlayerHelper.GetCharacterImage();

            // Reset
            DiceHelper.DisableForcedRolls();

            // Assert
            Assert.AreEqual("elf2.png", result);
        }

        [Test]
        public void RandomPlayerHelper_GetMonsterUniqueItem_2_Should_Return_2()
        {
            // Arrange
            DiceHelper.EnableForcedRolls();
            DiceHelper.SetForcedRollValue(2);

            var expected = ItemIndexViewModel.Instance.Dataset.ElementAt(1).Id;

            // Act
            var result = RandomPlayerHelper.GetMonsterUniqueItem();

            // Reset
            DiceHelper.DisableForcedRolls();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void RandomPlayerHelper_GetRandomCharacter_InValid_Empty_CharacterList_Should_Return_New()
        {
            // Arrange
            CharacterViewModel.Instance.Dataset.Clear();

            // Act
            var result = RandomPlayerHelper.GetRandomCharacter(1);

            // Reset

            // Assert
            Assert.AreEqual(true, result.Name.Contains("Elf"));
        }

        [Test]
        public async Task RandomPlayerHelper_GetRandomCharacter_Valid_CharacterList_1_Should_Return_1()
        {
            // Arrange
            CharacterViewModel.Instance.Dataset.Clear();
            await CharacterViewModel.Instance.CreateAsync(new BaseCharacter { Guid = "1" });

            // Act
            var result = RandomPlayerHelper.GetRandomCharacter(1);

            // Reset

            // Assert
            Assert.AreEqual(true, result.Guid.Contains("1"));
        }

        [Test]
        public async Task RandomPlayerHelper_GetRandomCharacter_Valid_CharacterList_3_Should_Return_2()
        {
            // Arrange
            CharacterViewModel.Instance.Dataset.Clear();
            await CharacterViewModel.Instance.CreateAsync(new BaseCharacter { Guid = "1" });
            await CharacterViewModel.Instance.CreateAsync(new BaseCharacter { Guid = "2" });
            await CharacterViewModel.Instance.CreateAsync(new BaseCharacter { Guid = "3" });

            DiceHelper.EnableForcedRolls();
            DiceHelper.SetForcedRollValue(2);

            // Act
            var result = RandomPlayerHelper.GetRandomCharacter(1);

            // Reset
            DiceHelper.DisableForcedRolls();

            // Assert
            Assert.AreEqual(true, result.Guid.Contains("2"));
        }

        [Test]
        public async Task RandomPlayerHelper_GetRandomCharacter_Valid_Health_Should_Be_Correct()
        {
            // Arrange
            CharacterViewModel.Instance.Dataset.Clear();
            await CharacterViewModel.Instance.CreateAsync(new BaseCharacter { Guid = "1" });
            await CharacterViewModel.Instance.CreateAsync(new BaseCharacter { Guid = "2" });
            await CharacterViewModel.Instance.CreateAsync(new BaseCharacter { Guid = "3" });

            DiceHelper.EnableForcedRolls();
            DiceHelper.SetForcedRollValue(2);

            // Act
            var result = RandomPlayerHelper.GetRandomCharacter(1);

            // Reset
            DiceHelper.DisableForcedRolls();

            // Assert
            Assert.AreEqual(result.MaxHealth, result.CurrHealth);
        }

        [Test]
        public void RandomPlayerHelper_GetRandomMonster_InValid_Empty_List_Should_Return_New_Monster()
        {
            // Arrange
            MonsterViewModel.Instance.Dataset.Clear();

            // Act
            var result = RandomPlayerHelper.GetRandomMonster(1);

            // Reset

            // Assert
            Assert.AreEqual(PlayerTypeEnum.Monster, result.PlayerType);
        }
    }
}