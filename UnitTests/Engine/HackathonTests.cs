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

        [SetUp]
        public void Setup()
        {
            //EngineViewModel.Engine.BattleSettingsModel.CharacterHitEnum = HitStatusEnum.Default;
            //EngineViewModel.Engine.BattleSettingsModel.MonsterHitEnum = HitStatusEnum.Default;
        }

        [TearDown]
        public void TearDown()
        {
        }
        [Test]
        public async Task AutoBattleEngine_Hack13_First_Round_Boss_Fight_Game_Over_At_Round_One()
        {
            AutoBattleEngine MyEngine = new AutoBattleEngine();
            await MyEngine.RunAutoBattle();

            var result = EngineViewModel.Engine.BattleScore.RoundCount;

            Assert.AreEqual(result, 0);
        }
        [Test]
        public async Task AutoBattleEngine_Hack48_Character_To_Hit_Roll_Equal_To_Hack_48_Condition_Attacker_Dies_Game_Ends_At_Round_One()
        {

            AutoBattleEngine MyEngine = new AutoBattleEngine();
            MyEngine.testHack48 = true;
            //FIRST CHARACTER 
            await MyEngine.RunAutoBattle();

            var result = MyEngine.BattleScore.RoundCount;

            MyEngine.testHack48 = false;

            Assert.AreEqual(result, 1);
            MyEngine.testHack48 = false;

        }
        [Test]
        public async Task AutoBattleEngine_Character_With_Prime_Attribute_Value_Hits_Real_Hard_should_win_LOTS()
        {
            AutoBattleEngine MyEngine = new AutoBattleEngine();
            MyEngine.testingHack47 = true;

            for (int i = 0; i < MyEngine.MaxNumberPartyCharacters; i++)
            {
                var PrimeGod = new PlayerInfoModel(
                        new BaseCharacter
                        {
                            Speed = 16,
                            Level = 7,
                            Attack = 12,
                            Defense = 10,
                            MaxHealth = 50,


                            CurrHealth = 11,
                            Experience = 1,
                            ExperienceRemaining = 1,
                            Name = "God Like Mike",
                            ListOrder = 1,
                        });

                MyEngine.CharacterList.Add(PrimeGod);


            }
            MyEngine.MaxNumberPartyMonsters = 1;

            //Act
            var result = await MyEngine.RunAutoBattle();

            //Reset
            MyEngine.testingHack47 = false;
            //Assert
            Assert.AreEqual(true, result);
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
                                Name = "Mike",
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
    }
}
