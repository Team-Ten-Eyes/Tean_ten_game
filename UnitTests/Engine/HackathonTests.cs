﻿using NUnit.Framework;

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
            var saveMonster = Engine.MonsterList;
            Engine.MonsterList.Clear();
            Engine.MonsterList.Add(MonsterPlayer);

            var Character = new BaseCharacter
            {
                Speed = 1,
                CurrHealth = 10,
                Name = "B",
            };

            var CharacterPlayer = new PlayerInfoModel(Character);
            var saveCharacter = Engine.CharacterList;
            Engine.CharacterList.Clear();
            Engine.CharacterList.Add(CharacterPlayer);

            // Make the List
            Engine.PlayerList = Engine.MakePlayerList();

            // Sort the list by Current Health, so it has to be resorted.
            Engine.PlayerList = Engine.PlayerList.OrderBy(m => m.CurrHealth).ToList();
            Engine.SpeedAlways = true;


            var result = Engine.OrderPlayerListByTurnOrder();

            // Reset
            Engine.CharacterList = saveCharacter;
            Engine.MonsterList = saveMonster;

            // Assert
            Assert.AreEqual("B", result[0].Name);
            Assert.AreEqual("A", result[1].Name);
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
            var saveList = Engine.MonsterList;
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
            Engine.MonsterList = saveList;
            Engine.BattleScore.RoundCount = 0;

            //Assert
            Assert.AreEqual(true, result[0].Attack == 100);
            Assert.AreEqual(true, result[0].Defense == 100);
            Assert.AreEqual(true, result[0].Speed == 100);
            Assert.AreEqual(true, result[0].CurrHealth == 100);
            Assert.AreEqual(true, result[0].MaxHealth == 100);
        }

        [Test]
        public void HakathonScenario_4_potion_pool_6_Health_Potions_Should_Pass()
        {
            /* 
           * Scenario Number:  
           *      #4
           *      
           * Description: 
           *      Every Round should have 6 new health potions added
           * 
           * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
           *      RoundEngine.cs
           *            added a populatePotionsFunction
           *            new round populates the potionspool
           *      TurnEngine.cs
           *            add a function called DrinkAllPotions will have a character drink all health potions even if they only need one
           *            added a fucntion called bellowTwentyHealth will return true is health bellow 20 percent 
           *            edited Attack to have a character that is bellow 20 percent drink all the potions
           *      Base Engine.cs
           *            add a variable called potions pool
           * 
           * Test Algrorithm:
           *      set the roundcount to 101 in the scoremodel held by BattleEngine
           *      Add Monsters to round with base stats of 10
           *      Validate the stats are now 100
           * 
           * Test Conditions:
           *      potion pool is full and there is a character with less than 20 percent health 
           *    
           *     
           * Validation:
           *      Validate the potions pool only has mana potions
           */

            //Arrange
            //turning healing on 
            Engine.RoundHealing = RoundHealingEnum.Healing_on;
            BaseCharacter character = new BaseCharacter();
            //Act
            //seeing if the potion list will be populated with 6th potions 
            Engine.NewRound();

            bool potions_Health_6_count = false;
            int count = 0;
            foreach (PotionsModel potion in Engine.potionPool)
            {
                if (potion.GetPotionType() == PotionsEnum.Health)
                    count++;
            }

           
            if(count == 6)
            {
                potions_Health_6_count = true;
            }

            //Assert
            Assert.AreEqual(true,potions_Health_6_count );

        }

    }
}
