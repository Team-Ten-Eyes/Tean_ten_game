using NUnit.Framework;

using System.Linq;
using System.Collections.Generic;

using Game.Engine;
using Game.Models;
using Game.Helpers;
using Game.ViewModels;

namespace UnitTests.Engine
{
    [TestFixture]
    public class TurnEngineTests
    {
        BattleEngine Engine;

        [SetUp]
        public void Setup()
        {
            Engine = new BattleEngine();
            Engine.StartBattle(true);   // Start engine in auto battle mode
        }

        [TearDown]
        public void TearDown()
        {
        }

        [Test]
        public void TurnEngine_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = Engine;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void TurnEngine_Attack_Valid_Empty_Monster_List_Should_Fail()
        {
            // Arrange
            var PlayerInfo = new PlayerInfoModel(new BaseCharacter());

            // Act
            var result = Engine.Attack(PlayerInfo);

            // Reset
            Engine.StartBattle(false);   // Clear the Engine

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void TurnEngine_Attack_InValid_Empty_Monster_List_Should_Fail()
        {
            // Arrange
            var PlayerInfo = new PlayerInfoModel(new BaseCharacter());

            // Clar the Monster List to cause the error
            Engine.MonsterList.Clear();

            // Act
            var result = Engine.Attack(PlayerInfo);

            // Reset
            Engine.StartBattle(false);   // Clear the Engine

            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public void TurnEngine_Attack_InValid_Empty_Character_List_Should_Fail()
        {
            // Arrange
            var PlayerInfo = new PlayerInfoModel(new BaseMonster());

            // Cause an error by making the list empty
            Engine.CharacterList.Clear();

            // Act
            var result = Engine.Attack(PlayerInfo);

            // Reset
            Engine.StartBattle(false);   // Clear the Engine

            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public void TurnEngine_Attack_Valid_Correct_List_Should_Pass()
        {
            // Arrange
            var PlayerInfo = new PlayerInfoModel();
            Engine.MonsterList.Add(new PlayerInfoModel(new BaseMonster()));

            // Act
            var result = Engine.Attack(PlayerInfo);

            // Reset
            Engine.StartBattle(false);   // Clear the Engine

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void TurnEngine_SelectCharacterToAttack_InValid_Empty_List_Should_Fail()
        {
            // Arrange
            var PlayerInfo = new PlayerInfoModel();
            Engine.CharacterList = new List<PlayerInfoModel>();

            // Act
            var result = Engine.SelectCharacterToAttack();

            // Reset
            Engine.StartBattle(false);   // Clear the Engine

            // Assert
            Assert.AreEqual(null, result);
        }

        [Test]
        public void TurnEngine_SelectCharacterToAttack_InValid_Null_List_Should_Fail()
        {
            // Arrange
            var PlayerInfo = new PlayerInfoModel();

            // Remember the List
            var saveList = Engine.CharacterList;

            Engine.CharacterList = null;

            // Act
            var result = Engine.SelectCharacterToAttack();

            // Reset
            Engine.CharacterList = saveList;
            Engine.StartBattle(false);   // Clear the Engine

            // Assert
            Assert.AreEqual(null, result);
        }

        [Test]
        public void TurnEngine_SelectCharacterToAttack_Valid_Default_Should_Pass()
        {
            // Arrange
            var PlayerInfo = new PlayerInfoModel();
            Engine.CharacterList.Add(new PlayerInfoModel(new BaseCharacter()));

            // Act
            var result = Engine.SelectCharacterToAttack();

            // Reset
            Engine.StartBattle(false);   // Clear the Engine

            // Assert
            Assert.AreNotEqual(null, result);
        }

        [Test]
        public void TurnEngine_SelectMonsterToAttack_InValid_Empty_List_Should_Fail()
        {
            // Arrange
            var PlayerInfo = new PlayerInfoModel();
            Engine.MonsterList = new List<PlayerInfoModel>();

            // Act
            var result = Engine.SelectMonsterToAttack();

            // Reset
            Engine.StartBattle(false);   // Clear the Engine

            // Assert
            Assert.AreEqual(null, result);
        }

        [Test]
        public void TurnEngine_SelectMonsterToAttack_InValid_Null_List_Should_Fail()
        {
            // Arrange

            // Remember the List
            var saveList = Engine.MonsterList;

            Engine.MonsterList = null;

            // Act
            var result = Engine.SelectMonsterToAttack();

            // Reset

            // Restore the List
            Engine.MonsterList = saveList;
            Engine.StartBattle(false);   // Clear the Engine

            // Assert
            Assert.AreEqual(null, result);
        }

        [Test]
        public void TurnEngine_SelectMonsterToAttack_Valid_Default_Should_Pass()
        {
            // Arrange
            var PlayerInfo = new PlayerInfoModel();
            Engine.MonsterList.Add(new PlayerInfoModel(new BaseMonster()));

            // Act
            var result = Engine.SelectMonsterToAttack();

            // Reset
            Engine.StartBattle(false);   // Clear the Engine

            // Assert
            Assert.AreNotEqual(null, result);
        }

        [Test]
        public void TurnEngine_RolltoHitTarget_Hit_Should_Pass()
        {
            // Arrange
            var AttackScore = 10;
            var DefenseScore = 0;

            DiceHelper.EnableForcedRolls();
            DiceHelper.SetForcedRollValue(3); // Always roll a 3.

            // Act
            var result = Engine.RollToHitTarget(AttackScore, DefenseScore);

            // Reset
            DiceHelper.DisableForcedRolls();

            // Assert
            Assert.AreEqual(HitStatusEnum.Hit, result);
        }

        [Test]
        public void TurnEngine_RolltoHitTarget_Miss_Should_Pass()
        {
            // Arrange
            var AttackScore = 1;
            var DefenseScore = 100;

            DiceHelper.EnableForcedRolls();
            DiceHelper.SetForcedRollValue(2);

            // Act
            var result = Engine.RollToHitTarget(AttackScore, DefenseScore);

            // Reset
            DiceHelper.DisableForcedRolls();

            // Assert
            Assert.AreEqual(HitStatusEnum.Miss, result);
        }

        [Test]
        public void TurnEngine_RolltoHitTarget_Forced_1_Should_Miss()
        {
            // Arrange
            var AttackScore = 1;
            var DefenseScore = 100;

            DiceHelper.EnableForcedRolls();
            DiceHelper.SetForcedRollValue(1);

            // Act
            var result = Engine.RollToHitTarget(AttackScore, DefenseScore);

            // Reset
            DiceHelper.DisableForcedRolls();

            // Assert
            Assert.AreEqual(HitStatusEnum.Miss, result);
        }

        [Test]
        public void TurnEngine_RolltoHitTarget_Forced_20_Should_Hit()
        {
            // Arrange
            var AttackScore = 1;
            var DefenseScore = 100;

            DiceHelper.EnableForcedRolls();
            DiceHelper.SetForcedRollValue(20);

            // Act
            var result = Engine.RollToHitTarget(AttackScore, DefenseScore);

            // Reset
            DiceHelper.DisableForcedRolls();

            // Assert
            Assert.AreEqual(HitStatusEnum.Hit, result);
        }

        [Test]
        public void TurnEngine_TakeTurn_Default_Should_Pass()
        {
            // Arrange
            var PlayerInfo = new PlayerInfoModel(new BaseCharacter());

            // Act
            var result = Engine.TakeTurn(PlayerInfo);

            // Reset

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void TurnEngine_DropItems_No_Items_Should_Return_0()
        {
            // Arrange
            var player = new BaseCharacter();

            var PlayerInfo = new PlayerInfoModel(player);

            DiceHelper.EnableForcedRolls();
            DiceHelper.SetForcedRollValue(0);

            // Act
            var result = Engine.DropItems(PlayerInfo);

            // Reset
            DiceHelper.DisableForcedRolls();

            // Assert
            Assert.AreEqual(0, result);
        }

        [Test]
        public void TurnEngine_DropItems_Character_Items_2_Should_Return_2()
        {
            // Arrange
            var player = new BaseCharacter
            {
                Head = ItemIndexViewModel.Instance.Dataset.FirstOrDefault().Id,
                Feet = ItemIndexViewModel.Instance.Dataset.FirstOrDefault().Id,
            };

            var PlayerInfo = new PlayerInfoModel(player);

            DiceHelper.EnableForcedRolls();
            DiceHelper.SetForcedRollValue(0);

            // Act
            var result = Engine.DropItems(PlayerInfo);

            // Reset
            DiceHelper.DisableForcedRolls();

            // Assert
            Assert.AreEqual(2, result);
        }

        [Test]
        public void TurnEngine_DropItems_Monster_Items_0_Random_Drop_1_Should_Return_1()
        {
            // Arrange
            var player = new BaseCharacter();

            var PlayerInfo = new PlayerInfoModel(player);

            DiceHelper.EnableForcedRolls();

            // Drop is 0-Number, so 2 will yield 1
            DiceHelper.SetForcedRollValue(2);

            // Act
            var result = Engine.DropItems(PlayerInfo);

            // Reset
            DiceHelper.DisableForcedRolls();

            // Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        public void TurnEngine_TargedDied_Character_Should_Pass()
        {
            // Arrange
            var player = new BaseCharacter();

            var PlayerInfo = new PlayerInfoModel(player);
            Engine.CharacterList.Add(PlayerInfo);

            // Act
            var result = Engine.TargetDied(PlayerInfo);

            // Reset

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void TurnEngine_TargedDied_Monseter_Should_Pass()
        {
            // Arrange
            var player = new BaseMonster();

            var PlayerInfo = new PlayerInfoModel(player);
            Engine.CharacterList.Add(PlayerInfo);

            // Act
            var result = Engine.TargetDied(PlayerInfo);

            // Reset

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void TurnEngine_TurnAsAttack_Character_Attacks_Null_Should_Fail()
        {
            // Arrange
            var Character = new BaseCharacter();
            var CharacterPlayer = new PlayerInfoModel(Character);
            Engine.CharacterList.Add(CharacterPlayer);

            var Monster = new BaseMonster();
            var MonsterPlayer = new PlayerInfoModel(Monster);
            Engine.MonsterList.Add(MonsterPlayer);

            // Act
            var result = Engine.TurnAsAttack(CharacterPlayer, null);

            // Reset

            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public void TurnEngine_TurnAsAttack_Null_Attacks_Character_Should_Fail()
        {
            // Arrange
            var Character = new BaseCharacter();
            var CharacterPlayer = new PlayerInfoModel(Character);
            Engine.CharacterList.Add(CharacterPlayer);

            var Monster = new BaseMonster();
            var MonsterPlayer = new PlayerInfoModel(Monster);
            Engine.MonsterList.Add(MonsterPlayer);

            // Act
            var result = Engine.TurnAsAttack(null, CharacterPlayer);

            // Reset

            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public void TurnEngine_TurnAsAttack_Character_Attacks_Monster_Miss_Should_Pass()
        {
            // Arrange
            var Character = new BaseCharacter();
            var CharacterPlayer = new PlayerInfoModel(Character);
            Engine.CharacterList.Add(CharacterPlayer);

            var Monster = new BaseMonster();
            var MonsterPlayer = new PlayerInfoModel(Monster);
            Engine.MonsterList.Add(MonsterPlayer);

            // Forece a Miss
            DiceHelper.EnableForcedRolls();
            DiceHelper.SetForcedRollValue(1);

            // Act
            var result = Engine.TurnAsAttack(CharacterPlayer, MonsterPlayer);

            // Reset
            DiceHelper.DisableForcedRolls();

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void TurnEngine_TurnAsAttack_Character_Attacks_Monster_Hit_Should_Pass()
        {
            // Arrange
            var Character = new BaseCharacter();
            var CharacterPlayer = new PlayerInfoModel(Character);
            Engine.CharacterList.Add(CharacterPlayer);

            var Monster = new BaseMonster();
            var MonsterPlayer = new PlayerInfoModel(Monster);
            Engine.MonsterList.Add(MonsterPlayer);

            // Forece a Miss
            DiceHelper.EnableForcedRolls();
            DiceHelper.SetForcedRollValue(20);

            // Act
            var result = Engine.TurnAsAttack(CharacterPlayer, MonsterPlayer);

            // Reset
            DiceHelper.DisableForcedRolls();

            // Assert
            Assert.AreEqual(true, result);
        }


        [Test]
        public void TurnEngine_TurnAsAttack_Character_Attacks_Monster_Hit_Death_Should_Pass()
        {
            // Arrange
            var Character = new BaseCharacter();
            Character.CurrHealth = 1;
            var CharacterPlayer = new PlayerInfoModel(Character);
            Engine.CharacterList.Add(CharacterPlayer);

            var Monster = new BaseMonster();
            var MonsterPlayer = new PlayerInfoModel(Monster);
            Engine.MonsterList.Add(MonsterPlayer);

            // Forece a Miss
            DiceHelper.EnableForcedRolls();
            DiceHelper.SetForcedRollValue(20);

            // Act
            var result = Engine.TurnAsAttack(CharacterPlayer, MonsterPlayer);

            // Reset
            DiceHelper.DisableForcedRolls();

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void TurnEngine_TurnAsAttack_Monster_Attacks_Character_Miss_Should_Pass()
        {
            // Arrange
            var Monster = new BaseMonster();
            var MonsterPlayer = new PlayerInfoModel(Monster);
            Engine.MonsterList.Add(MonsterPlayer);

            var Character = new BaseCharacter();
            var CharacterPlayer = new PlayerInfoModel(Character);
            Engine.CharacterList.Add(CharacterPlayer);

            // Forece a Miss
            DiceHelper.EnableForcedRolls();
            DiceHelper.SetForcedRollValue(1);

            // Act
            var result = Engine.TurnAsAttack(MonsterPlayer, CharacterPlayer);

            // Reset
            DiceHelper.DisableForcedRolls();

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void TurnEngine_TurnAsAttack_Monster_Attacks_Character_Hit_Should_Pass()
        {
            // Arrange
            var Monster = new BaseMonster();
            var MonsterPlayer = new PlayerInfoModel(Monster);
            Engine.MonsterList.Add(MonsterPlayer);

            var Character = new BaseCharacter();
            var CharacterPlayer = new PlayerInfoModel(Character);
            Engine.CharacterList.Add(CharacterPlayer);

            // Forece a Miss
            DiceHelper.EnableForcedRolls();
            DiceHelper.SetForcedRollValue(20);

            // Act
            var result = Engine.TurnAsAttack(MonsterPlayer, CharacterPlayer);

            // Reset
            DiceHelper.DisableForcedRolls();

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void TurnEngine_RemoveIfDead_Dead_True_Should_Return_False()
        {
            // Arrange
            var Monster = new BaseMonster
            {
                CurrHealth = 1,
                Alive = true,
                Guid = "me"
            };

            var PlayerInfo = new PlayerInfoModel(Monster);

            Engine.MonsterList.Clear();
            Engine.MonsterList.Add(PlayerInfo);
            Engine.MakePlayerList();

            // Act
            var result = Engine.RemoveIfDead(PlayerInfo);

            // Reset

            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public void TurnEngine_RemoveIfDead_Dead_false_Should_Return_true()
        {
            // Arrange
            var Monster = new BaseMonster
            {
                CurrHealth = 1,
                Alive = true,
                Guid = "me"
            };

            var PlayerInfo = new PlayerInfoModel(Monster);

            Engine.MonsterList.Clear();
            Engine.MonsterList.Add(PlayerInfo);
            Engine.MakePlayerList();

            // Act
            var result = Engine.RemoveIfDead(PlayerInfo);

            // Reset

            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public void TurnEngine_TurnAsAttack_Character_Attacks_Monster_Levels_Up_Should_Pass()
        {
            // Arrange
            var Monster = new BaseMonster();
            var MonsterPlayer = new PlayerInfoModel(Monster);
            Engine.MonsterList.Add(MonsterPlayer);

            var Character = new BaseCharacter();
            //Character.//ExperienceTotal = 300;    // Enough for next level

            var CharacterPlayer = new PlayerInfoModel(Character);
            Engine.CharacterList.Add(CharacterPlayer);

            // Forece a Miss
            DiceHelper.EnableForcedRolls();
            DiceHelper.SetForcedRollValue(20);

            // Act
            var result = Engine.TurnAsAttack(CharacterPlayer, MonsterPlayer);

            // Reset
            DiceHelper.DisableForcedRolls();

            // Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(2, CharacterPlayer.Level);
        }

    }
}