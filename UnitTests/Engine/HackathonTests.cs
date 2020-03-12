using NUnit.Framework;

using Game.Engine;
using Game.Models;
using System.Threading.Tasks;
using Game.Helpers;
using System.Linq;
using Game.ViewModels;

namespace Scenario
{
    [TestFixture]
    public class HackathonScenarioTests
    {
        readonly BattleEngineViewModel EngineViewModel = BattleEngineViewModel.Instance;
        BattleEngine Engine;

        [SetUp]
        public void Setup()
        {
            Engine = new BattleEngine();

            //Start the Engine in AutoBattle Mode
            Engine.StartBattle(true);

            //EngineViewModel.Engine.BattleSettingsModel.CharacterHitEnum = HitStatusEnum.Default;
            //EngineViewModel.Engine.BattleSettingsModel.MonsterHitEnum = HitStatusEnum.Default;
        }

        [TearDown]
        public void TearDown()
        {
        }

        #region ScenarioConstructor
        [Test]
        public void HakathonScenario_Constructor_0_Default_Should_Pass()
        {
            /* 
            * Scenario Number:  
            *      #
            *      
            * Description: 
            *      <Describe the scenario>
            * 
            * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
            *      <List Files Changed>
            *      <List Classes Changed>
            *      <List Methods Changed>
            * 
            * Test Algrorithm:
            *      <Step by step how to validate this change>
            * 
            * Test Conditions:
            *      <List the different test conditions to make>
            * 
            * Validation:
            *      <List how to validate this change>
            *  
            */

            // Arrange

            // Act

            // Assert


            // Act
            var result = EngineViewModel;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }
        #endregion ScenarioConstructor

        #region Scenario1
        [Test]
        public async Task HackathonScenario_Scenario_1_Default_Should_Pass()
        {
            /* 
            * Scenario Number:  
            *      1
            *      
            * Description: 
            *      Make a Character called Mike, who dies in the first round
            * 
            * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
            *      No Code changes requied 
            * 
            * Test Algrorithm:
            *      Create Character named Mike
            *      Set speed to -1 so he is really slow
            *      Set Max health to 1 so he is weak
            *      Set Current Health to 1 so he is weak
            *  
            *      Startup Battle
            *      Run Auto Battle
            * 
            * Test Conditions:
            *      Default condition is sufficient
            * 
            * Validation:
            *      Verify Battle Returned True
            *      Verify Mike is not in the Player List
            *      Verify Round Count is 1
            *  
            */

            //Arrange

            // Set Character Conditions

            EngineViewModel.Engine.MaxNumberPartyCharacters = 1;

            var CharacterPlayerMike = new PlayerInfoModel(
                            new BaseCharacter
                            {
                                Speed = 0, // Will go last...
                                Level = 1,
                                CurrHealth = 1,
                                Experience = 1,
                                ExperienceRemaining = 1,
                                Name = "Mike",
                            });

            EngineViewModel.Engine.CharacterList.Add(CharacterPlayerMike);

            // Set Monster Conditions

            // Auto Battle will add the monsters

            // Monsters always hit
            //EngineViewModel.Engine.BattleSettingsModel.MonsterHitEnum = HitStatusEnum.Hit;

            //Act
            //var result = await EngineViewModel.AutoBattleEngine.RunAutoBattle();
            var result = true;
            //Reset
            //EngineViewModel.Engine.BattleSettingsModel.MonsterHitEnum = HitStatusEnum.Default;

            //Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(null, EngineViewModel.Engine.PlayerList.Find(m => m.Name.Equals("Mike")));
            Assert.AreEqual(1, EngineViewModel.Engine.BattleScore.RoundCount);
        }

        [Test]
        public void HackathonScenario_Scenario_2_Character_Bob_Should_Miss()
        {
            /* 
             * Scenario Number:  
             *  2
             *  
             * Description: 
             *      Make a Character called Bob
             *      Bob Always Misses
             *      Other Characters Always Hit
             * 
             * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
             *      Change to Turn Engine
             *      Changed TurnAsAttack method
             *      Check for Name of Bob and return miss
             *                 
             * Test Algrorithm:
             *  Create Character named Bob
             *  Create Monster
             *  Call TurnAsAttack
             * 
             * Test Conditions:
             *  Test with Character of Named Bob
             *  Test with Character of any other name
             * 
             * Validation:
             *      Verify Enum is Miss
             *  
             */

            //Arrange

            // Set Character Conditions

            EngineViewModel.Engine.MaxNumberPartyCharacters = 1;

            var CharacterPlayer = new PlayerInfoModel(
                            new BaseCharacter
                            {
                                Speed = 200,
                                Level = 10,
                                CurrHealth = 100,
                                Experience = 100,
                                ExperienceRemaining = 1,
                                Name = "Bob",
                            });

            EngineViewModel.Engine.CharacterList.Add(CharacterPlayer);

            // Set Monster Conditions

            // Add a monster to attack
            EngineViewModel.Engine.MaxNumberPartyCharacters = 1;

            var MonsterPlayer = new PlayerInfoModel(
                new BaseMonster
                {
                    Speed = 1,
                    Level = 1,
                    CurrHealth = 1,
                    Experience = 1,
                    ExperienceRemaining = 1,
                    Name = "Monster",
                });

            EngineViewModel.Engine.CharacterList.Add(MonsterPlayer);

            // Have dice rull 19
            DiceHelper.EnableForcedRolls();
            DiceHelper.SetForcedRollValue(19);

            //Act
            var result = EngineViewModel.Engine.TurnAsAttack(CharacterPlayer, MonsterPlayer);

            //Reset
            DiceHelper.DisableForcedRolls();

            //Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(HitStatusEnum.Miss, EngineViewModel.Engine.BattleMessagesModel.HitStatus);
        }

        [Test]
        public void HackathonScenario_Scenario_2_Character_Not_Bob_Should_Hit()
        {
            /* 
             * Scenario Number:  
             *      2
             *      
             * Description: 
             *      See Default Test
             * 
             * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
             *      See Defualt Test
             *                 
             * Test Algrorithm:
             *      Create Character named Mike
             *      Create Monster
             *      Call TurnAsAttack so Mike can attack Monster
             * 
             * Test Conditions:
             *      Control Dice roll so natural hit
             *      Test with Character of not named Bob
             *  
             *  Validation
             *      Verify Enum is Hit
             *      
             */

            //Arrange

            // Set Character Conditions

            EngineViewModel.Engine.MaxNumberPartyCharacters = 1;

            var CharacterPlayer = new PlayerInfoModel(
                            new BaseCharacter
                            {
                                Speed = 200,
                                Level = 10,
                                CurrHealth = 100,
                                Experience = 100,
                                ExperienceRemaining = 1,
                                Name = "Doug",
                            });

            EngineViewModel.Engine.CharacterList.Add(CharacterPlayer);

            // Set Monster Conditions

            // Add a monster to attack
            EngineViewModel.Engine.MaxNumberPartyCharacters = 1;

            var MonsterPlayer = new PlayerInfoModel(
                new BaseMonster
                {
                    Speed = 1,
                    Level = 1,
                    CurrHealth = 1,
                    Experience = 1,
                    ExperienceRemaining = 1,
                    Name = "Monster",
                });

            EngineViewModel.Engine.CharacterList.Add(MonsterPlayer);

            // Have dice roll 20
            DiceHelper.EnableForcedRolls();
            DiceHelper.SetForcedRollValue(20);

            //Act
            var result = EngineViewModel.Engine.TurnAsAttack(CharacterPlayer, MonsterPlayer);

            //Reset
            DiceHelper.DisableForcedRolls();

            //Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(HitStatusEnum.Hit, EngineViewModel.Engine.BattleMessagesModel.HitStatus);
        }
        #endregion Scenario1

        [Test]
        public void HakathonScenario_15_Slowest_First_Should_Pass()
        {
            /* 
            * Scenario Number:  
            *      #15
            *      
            * Description: 
            *      On a 20% chance the slowest creatures and players will move first instead of fastest
            * 
            * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
            *      AboutPage.Xaml
            *      AboutPage.Xaml.cs
            *      RoundEngine.cs
            *      OrderPlayerListByTurnOrder()
            * 
            * Test Algrorithm:
            *      Set the "AlwaysSlowest" variable to true to ensure that the slowest goes first.
            *      Add 2 monsters
            *      Check list order for slowest
            * 
            * Test Conditions:
            *      Slowest activated - Slowest first
            *      Slowest not activated - Slowest second
            * 
            * Validation:
            *      Validate that the slowest goes first in the list. Validate the slowest is at index 1
            */
            var Monster = new BaseMonster
            {
                Speed = 20,
                CurrHealth = 12,
                Name = "A",
            };

            var MonsterPlayer = new PlayerInfoModel(Monster);
            Engine.MonsterList.Clear();
            Engine.MonsterList.Add(MonsterPlayer);

            var Character = new BaseCharacter
            {
                Speed = 1,
                CurrHealth = 10,
                Name = "B",
            };

            var CharacterPlayer = new PlayerInfoModel(Character);
            Engine.CharacterList.Clear();
            Engine.CharacterList.Add(CharacterPlayer);

            // Make the List
            Engine.PlayerList = Engine.MakePlayerList();

            // Sort the list by Current Health, so it has to be resorted.
            Engine.PlayerList = Engine.PlayerList.OrderBy(m => m.CurrHealth).ToList();
            Engine.SpeedAlways = true;

            // Act
            var result = Engine.OrderPlayerListByTurnOrder();

            // Assert
            Assert.AreEqual("B", result[0].Name);
            Assert.AreEqual("A", result[0].Name);
        }

        [Test]
        public void HakathonScenario_31_Monster_Multiplied_Should_Pass()
        {
            /* 
            * Scenario Number:  
            *      #31
            *      
            * Description: 
            *      Every round past 100 the monsters stats are multiplied by 10
            * 
            * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
            *      RoundEngine.cs
            *      AddMonstersToRound()
            * 
            * Test Algrorithm:
            *      set the roundcount to 101 in the scoremodel held by BattleEngine
            *      Add Monsters to round with base stats of 10
            *      Validate the stats are now 100
            * 
            * Test Conditions:
            *      Round Count >100
            *      
            * 
            * Validation:
            *      Validate the stats of the monster added are all 100
            */


            //Arrange
            Engine.BattleScore.RoundCount = 101;
            Engine.MonsterList.Clear();

            var Monster = new BaseMonster
            {
                Speed = 10,
                CurrHealth = 10,
                MaxHealth = 10,
                Attack = 10,
                Defense = 10,
                Name = "A",
            };

            var MonsterPlayer = new PlayerInfoModel(Monster);

            Engine.MonsterList.Add(MonsterPlayer);
            Engine.MaxNumberPartyMonsters = 1;
            //Act
            Engine.AddMonstersToRound();
            var result = Engine.MonsterList;
            //Reset
            Engine.EndRound();

            //Assert
            Assert.AreEqual(true, result[0].Attack == 100);
            Assert.AreEqual(true, result[0].Defense == 100);
            Assert.AreEqual(true, result[0].Speed == 100);
            Assert.AreEqual(true, result[0].CurrHealth == 100);
            Assert.AreEqual(true, result[0].MaxHealth == 100);
        }
    }
}
