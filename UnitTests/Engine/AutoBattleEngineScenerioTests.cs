﻿using Game.Engine;
using Game.Helpers;
using Game.Models;
using Game.ViewModels;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Scenario
{
    [TestFixture]
    public class AutoBattleEngineScenarioTests
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
        public async Task AutoBattleEngine_Hack13_First_Round_Boss_Fight_Game_Over_At_Round_One()
        {
            AutoBattleEngine MyEngine = new AutoBattleEngine();
            MyEngine.testBossHack = true;
            await MyEngine.RunAutoBattle();

            var result = MyEngine.BattleScore.RoundCount;

            Assert.AreEqual(result, 1);
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
            await MyEngine.RunAutoBattle();

            bool result = false;
            if (MyEngine.BattleScore.RoundCount > 3)
            {
                result = true;
            }

            //Reset
            MyEngine = null;
            //Assert
            Assert.AreEqual(true, result);

        }





        [Test]
        public async Task AutoBattleEngine_RunAutoBattle_Monsters_1_Should_Pass()
        {
            //Arrange

            // Add Characters

            Engine.MaxNumberPartyCharacters = 1;


            var CharacterPlayerMike = new PlayerInfoModel(
                            new BaseCharacter
                            {
                                Speed = -1,
                                Level = 10,
                                CurrHealth = 11,
                                Experience = 1,
                                ExperienceRemaining = 1,
                                Name = "Mike",
                                ListOrder = 1,
                            });

            Engine.CharacterList.Add(CharacterPlayerMike);

            
            
            // Add Monsters

            // Need to set the Monster count to 1, so the battle goes to Next Round Faster
            Engine.MaxNumberPartyMonsters = 1;

            //Act
            var result = await Engine.RunAutoBattle();

            //Reset

            //Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public async Task AutoBattleEngine_RunAutoBattle_Character_Level_Up_Should_Pass()
        {

            /* 
             * Test to force leveling up of a character during the battle
             * 
             * 1 Character, Experience set at next level mark
             * 
             * 6 Monsters
             * 
             * Character Should Level UP 1 level
             * 
             */

            //Arrange

            // Add Characters

            Engine.MaxNumberPartyCharacters = 1;

            CharacterViewModel.Instance.Dataset.Clear();

            // To See Level UP happening, a character needs to be close to the next level
            var Character = new BaseCharacter
            {
                Experience = 300,    // Enough for next level
                Name = "Mike Level Example",
                Speed = 100,    // Go first
            };

            // Remember Start Level
            var StartLevel = Character.Level;

            var CharacterPlayer = new PlayerInfoModel(Character);

            Engine.CharacterList.Add(CharacterPlayer);


            // Add Monsters

            //Act
            var result = await Engine.RunAutoBattle();

            //Reset

            //Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(true, Engine.BattleScore.CharacterAtDeathList.Contains("Mike Level Example"));
            // Assert.AreEqual(StartLevel+1, Engine.BattleScore.BaseCharacterDeathList.Where(m=>m.Guid.Equals(Character.Guid)).First().Level);
        }
        [Test]
        public async Task AutoBattleEngine_Hack48_Character_To_Hit_Roll_Equal_To_Hack_48_Condition_Attacker_Dies_Game_Ends_At_Round_One()
        {


            Engine.testHack48 = true;
            //FIRST CHARACTER 
            await Engine.RunAutoBattle();

            var result = Engine.BattleScore.RoundCount;

            Assert.AreEqual(result, 1);
        }
        

        [Test]
        public async Task AutoBattleEngine_RunAutoBattle_GameOver_Round_1_Should_Pass()
        { 
            Engine.MaxNumberPartyCharacters = 1;

            var CharacterPlayer = new PlayerInfoModel(
                            new BaseCharacter
                            {
                                Speed = -1, // Will go last...
                                Level = 10,
                                CurrHealth = 1,
                                Experience = 1,
                                ExperienceRemaining = 1,
                                ListOrder = 1,
                            });

            Engine.CharacterList.Add(CharacterPlayer);


            // Add Monsters

            Engine.MaxNumberPartyMonsters = 6;

            //Act
            var result = await Engine.RunAutoBattle();

            //Reset

            //Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public async Task AutoBattleEngine_RunAutoBattle_InValid_Round_Loop_Should_Fail()
        {
            /* 
             * Test infinate rounds.  
             * 
             * Characters overpower monsters, game never ends
             * 
             * 6 Character
             *      Speed high
             *      Hit Hard
             *      High health
             * 
             * 1 Monsters
             *      Slow
             *      Weak Hit
             *      Weak health
             * 
             * Should never end
             * 
             * Inifinite Loop Check should stop the game
             * 
             */

            //Arrange

            // Add Characters

            Engine.MaxNumberPartyCharacters = 6;

            var CharacterPlayer = new PlayerInfoModel(
                            new BaseCharacter
                            {
                                Speed = 100,
                                Level = 20,
                                MaxHealth = 200,
                                CurrHealth = 200,
                                Experience = 1,
                            });

            var CharacterPlayerMin = new PlayerInfoModel(
                new BaseCharacter
                {
                    Speed = 99,
                    Level = 1,
                    MaxHealth = 200,
                    CurrHealth = 200,
                    Experience = 1,
                });

            Engine.CharacterList.Add(CharacterPlayer);
            Engine.CharacterList.Add(CharacterPlayer);
            Engine.CharacterList.Add(CharacterPlayer);
            Engine.CharacterList.Add(CharacterPlayer);
            Engine.CharacterList.Add(CharacterPlayer);
            Engine.CharacterList.Add(CharacterPlayerMin);

            // Add Monsters

            Engine.MaxNumberPartyMonsters = 1;

            // Controll Rolls,  Hit is always a 3
            DiceHelper.EnableForcedRolls();
            DiceHelper.SetForcedRollValue(3);

            //Act
            var result = await Engine.RunAutoBattle();

            //Reset
            DiceHelper.DisableForcedRolls();

            //Assert
            Assert.AreEqual(false, result);
            Assert.AreEqual(true, (Engine.BattleScore.RoundCount > Engine.MaxRoundCount) || (Engine.BattleScore.TurnCount > Engine.MaxTurnCount));
        }

        [Test]
        public async Task AutoBattleEngine_RunAutoBattle_InValid_Trun_Loop_Should_Fail()
        {
            /* 
             * Test infinate turn.  
             * 
             * Monsters overpower Characters game never ends
             * 
             * 1 Character
             *      Speed low
             *      Hit weak
             *      Health low
             * 
             * 6 Monsters
             *      Speed High
             *      Hit strong
             *      Health High
             * 
             * Rolls for always Miss
             * 
             * Should never end
             * 
             * Inifinite Loop Check should stop the game
             * 
             */

            //Arrange

            // Add Characters

            Engine.MaxNumberPartyCharacters = 1;

            var CharacterPlayerMike = new PlayerInfoModel(
                            new BaseCharacter
                            {
                                Speed = 1,
                                Level = 1,
                                MaxHealth = 1,
                                CurrHealth = 1,
                            });

            Engine.CharacterList.Add(CharacterPlayerMike);


            // Add Monsters

            Engine.MaxNumberPartyMonsters = 5;

            // Controll Rolls,  Always Miss
            DiceHelper.EnableForcedRolls();
            DiceHelper.SetForcedRollValue(1);

            //Act
            var result = await Engine.RunAutoBattle();

            //Reset
            DiceHelper.DisableForcedRolls();

            //Assert
            Assert.AreEqual(false, result);
            Assert.AreEqual(true, Engine.BattleScore.TurnCount > Engine.MaxTurnCount);
            Assert.AreEqual(true, Engine.BattleScore.RoundCount > Engine.MaxRoundCount);
        }

        //[Test]
        //public async Task AutoBattleEngine_RunAutoBattle_GameOver_Round_2_Should_Pass()
        //{
        //    /* 
        //     * 
        //     * 2 Character, Speed slowest, only 1 HP each
        //     * 
        //     * 2 Monsters
        //     * 
        //     * Should end in the first round
        //     * 
        //     */

        //    //Arrange

        //    // Add Characters

        //    Engine.MaxNumberPartyCharacters = 2;

        //    var CharacterPlayerMike = new PlayerInfoModel(
        //                    new BaseCharacter
        //                    {
        //                        Speed = -1, // Will go last...
        //                        Level = 10,
        //                        CurrHealth = 1,
        //                        //ExperienceTotal = 1,
        //                        ExperienceRemaining = 1,
        //                        Name = "Mike",
        //                    });

        //    Engine.CharacterList.Add(CharacterPlayerMike);
        //    Engine.CharacterList.Add(CharacterPlayerMike);


        //    // Add Monsters

        //    Engine.MaxNumberPartyMonsters = 2;

        //    var MonsterPlayer = new PlayerInfoModel(
        //        new BaseMonster
        //        {
        //            Speed = 100, // Will go first...
        //            Level = 10,
        //            CurrHealth = 1,
        //            //ExperienceTotal = 1,
        //            ExperienceRemaining = 1,
        //        });

        //    Engine.MonsterList.Add(MonsterPlayer);
        //    Engine.MonsterList.Add(MonsterPlayer);

        //    //Act
        //    var result = await Engine.RunAutoBattle();

        //    //Reset

        //    //Assert
        //    Assert.AreEqual(true, result);
        //}
    }
}