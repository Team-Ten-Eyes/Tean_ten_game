﻿using Game.Engine;
using Game.Models;
using Game.ViewModels;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace UnitTests.Engine
{
    [TestFixture]
    public class RoundEngineTests
    {
        BattleEngine Engine;

        [SetUp]
        public void Setup()
        {
            Engine = new BattleEngine();

            //Start the Engine in AutoBattle Mode
            Engine.StartBattle(true);
        }

        [TearDown]
        public void TearDown()
        {
        }

        //unit test for clear list function seeing if it clears the potions list
        [Test]
        public void RoundEngine_ClearList_PotionsPool_Should_Pass()
        {

            //Arrange
            //populating Potions Potion pool
            Engine.populatePotionsList();

            //adding a character to the potions pool
            BaseMonster monster = new BaseMonster();
            Engine.NewRound();

            //Act
            Engine.EndRound();
            //Reset

            //Assert
            Assert.AreEqual(Engine.potionPool.Count, 0);

        }

        //unit test to make sure all mike's are dead
        [Test]
        public void RoundEngine_Test_Mike_List_Should_Pass()
        {
            //Arrange
            Engine.CharacterList.Add(new PlayerInfoModel
            {
                Name = "Mike"
            });

            //Act
            Engine.NewRound();
            var result = Engine.PlayerList;

            //Reset
            Engine.EndRound();

            //Asset
            Assert.AreEqual(false, result.Contains(new PlayerInfoModel { Name = "Mike" }));

        }

        //unit test for clear list funciton seeing if it clears the monster list
        [Test]
        public void RoundEngine_ClearList_MonsterList_Should_Pass()
        {


            //adding monsters and monsters are added to new Round
            Engine.NewRound();

            //Act
            Engine.EndRound();
            //Reset

            //Assert
            Assert.AreEqual(Engine.MonsterList.Count, 0);

        }


        [Test]
        public void RoundEngine_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = Engine;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void RoundEngine_OrderPlayerListByTurnOrder_Speed_Higher_Should_Pass()
        {
            // Arrange
            var Monster = new BaseMonster
            {
                Speed = 20,
                Level = 20,
                CurrHealth = 100,
                ////ExperienceTotal = 1000,
                Name = "Z",
                ListOrder = 1,
                Guid = "me"
            };

            var MonsterPlayer = new PlayerInfoModel(Monster);
            Engine.MonsterList.Clear();
            Engine.MonsterList.Add(MonsterPlayer);

            var Character = new BaseCharacter
            {
                Speed = 1,
                Level = 1,
                CurrHealth = 2,
                ////ExperienceTotal = 1,
                Name = "C",
                ListOrder = 10
            };

            var CharacterPlayer = new PlayerInfoModel(Character);
            Engine.CharacterList.Clear();
            Engine.CharacterList.Add(CharacterPlayer);

            // Make the List
            Engine.PlayerList = Engine.MakePlayerList();

            // Sort the list by Current Health, so it has to be resorted.
            Engine.PlayerList = Engine.PlayerList.OrderBy(m => m.CurrHealth).ToList();

            // Act
            var result = Engine.OrderPlayerListByTurnOrder();

            // Reset

            // Assert
            Assert.AreEqual("me", result[0].Guid);
        }

        [Test]
        public void RoundEngine_OrderPlayerListByTurnOrder_Level_Higher_Should_Pass()
        {
            // Arrange
            var Monster = new BaseMonster
            {
                Speed = 20,
                Level = 20,
                CurrHealth = 100,
                ////ExperienceTotal = 1000,
                Name = "Z",
                ListOrder = 1,
                Guid = "me"
            };

            var MonsterPlayer = new PlayerInfoModel(Monster);
            Engine.MonsterList.Clear();
            Engine.MonsterList.Add(MonsterPlayer);

            var Character = new BaseCharacter
            {
                Speed = 20,
                Level = 1,
                CurrHealth = 2,
                ////ExperienceTotal = 1,
                Name = "C",
                ListOrder = 10
            };

            var CharacterPlayer = new PlayerInfoModel(Character);
            Engine.CharacterList.Clear();
            Engine.CharacterList.Add(CharacterPlayer);

            // Make the List
            Engine.PlayerList = Engine.MakePlayerList();

            // Sort the list by Current Health, so it has to be resorted.
            Engine.PlayerList = Engine.PlayerList.OrderBy(m => m.CurrHealth).ToList();

            // Act
            var result = Engine.OrderPlayerListByTurnOrder();

            // Reset

            // Assert
            Assert.AreEqual("me", result[0].Guid);
        }

        [Test]
        public void RoundEngine_OrderPlayerListByTurnOrder_Experience_Higher_Should_Pass()
        {
            // Arrange

            var Monster = new BaseMonster
            {
                Speed = 20,
                Level = 1,
                CurrHealth = 100,
                ////ExperienceTotal = 1,
                Name = "Z",
                ListOrder = 1,
                Guid = "me"
            };

            var MonsterPlayer = new PlayerInfoModel(Monster);

            Engine.MonsterList.Clear();
            Engine.MonsterList.Add(MonsterPlayer);

            var Character = new BaseCharacter
            {
                Speed = 20,
                Level = 1,
                CurrHealth = 2,
                ////ExperienceTotal = 1,
                Name = "C",
                ListOrder = 10,
            };

            var CharacterPlayer = new PlayerInfoModel(Character);
            Engine.CharacterList.Clear();
            Engine.CharacterList.Add(CharacterPlayer);

            // Make the List
            Engine.PlayerList = Engine.MakePlayerList();

            // Sort the list by Current Health, so it has to be resorted.
            Engine.PlayerList = Engine.PlayerList.OrderBy(m => m.CurrHealth).ToList();

            // Act
            var result = Engine.OrderPlayerListByTurnOrder();

            // Reset

            // Assert
            Assert.AreEqual("me", result[0].Guid);
        }

        [Test]
        public void RoundEngine_OrderPlayerListByTurnOrder_ListOrder_Should_Pass()
        {
            // Arrange
            var Monster = new BaseMonster
            {
                Speed = 20,
                Level = 1,
                CurrHealth = 1,
                ////ExperienceTotal = 1,
                Name = "A",
                ListOrder = 1,
                Guid = "me"
            };

            var MonsterPlayer = new PlayerInfoModel(Monster);
            Engine.MonsterList.Clear();
            Engine.MonsterList.Add(MonsterPlayer);

            var Character = new BaseCharacter
            {
                Speed = 20,
                Level = 1,
                CurrHealth = 2,
                ////ExperienceTotal = 1,
                Name = "A",
                ListOrder = 10
            };

            var CharacterPlayer = new PlayerInfoModel(Character);
            Engine.CharacterList.Clear();
            Engine.CharacterList.Add(CharacterPlayer);

            // Make the List
            Engine.PlayerList = Engine.MakePlayerList();

            // Sort the list by Current Health, so it has to be resorted.
            Engine.PlayerList = Engine.PlayerList.OrderBy(m => m.CurrHealth).ToList();

            // Act
            var result = Engine.OrderPlayerListByTurnOrder();

            // Reset

            // Assert
            Assert.AreEqual("me", result[0].Guid);
        }

        [Test]
        public void RoundEngine_OrderPlayerListByTurnOrder_Name_A_Z_Should_Pass()
        {
            Engine.MonsterList.Clear();

            // Both need to be character to fall through to the Name Test
            // Arrange
            var Character = new BaseCharacter
            {
                Speed = 20,
                Level = 1,
                CurrHealth = 1,
                ////ExperienceTotal = 1,
                Name = "Z",
                ListOrder = 1,
                Guid = "me"
            };

            var CharacterPlayer = new PlayerInfoModel(Character);
            Engine.CharacterList.Clear();
            Engine.CharacterList.Add(new PlayerInfoModel(Character));

            Character = new BaseCharacter
            {
                Speed = 20,
                Level = 1,
                CurrHealth = 2,
                ////ExperienceTotal = 1,
                Name = "ZZ",
                ListOrder = 10
            };

            CharacterPlayer = new PlayerInfoModel(Character);
            Engine.CharacterList.Add(new PlayerInfoModel(Character));

            // Make the List
            Engine.PlayerList = Engine.MakePlayerList();

            // Sort the list by Current Health, so it has to be resorted.
            Engine.PlayerList = Engine.PlayerList.OrderBy(m => m.CurrHealth).ToList();

            // Act
            var result = Engine.OrderPlayerListByTurnOrder();

            // Reset

            // Assert
            Assert.AreEqual("me", result[0].Guid);
        }

        [Test]
        public async Task RoundEngine_GetItemFromPoolIfBetter_Location_Empty_Should_Fail()
        {
            Engine.MonsterList.Clear();

            // Both need to be character to fall through to the Name Test
            // Arrange
            var Character = new BaseCharacter
            {
                Speed = 20,
                Level = 1,
                CurrHealth = 1,
                ////ExperienceTotal = 1,
                Name = "Z",
                ListOrder = 1,
                Guid = "me"
            };

            // Add each model here to warm up and load it.
            Game.Helpers.DataSetsHelper.WarmUp();

            var item1 = new ItemModel { Attribute = AttributeEnum.Attack, Value = 1, Location = ItemLocationEnum.Feet };
            var item2 = new ItemModel { Attribute = AttributeEnum.Attack, Value = 20, Location = ItemLocationEnum.Feet };

            await ItemIndexViewModel.Instance.CreateAsync(item1);
            await ItemIndexViewModel.Instance.CreateAsync(item2);

            Engine.ItemPool.Add(item1);
            Engine.ItemPool.Add(item2);

            // Put the Item on the Character
            Character.Head = null;

            var CharacterPlayer = new PlayerInfoModel(Character);
            Engine.CharacterList.Clear();
            Engine.CharacterList.Add(new PlayerInfoModel(Character));

            // Make the List
            Engine.PlayerList = Engine.MakePlayerList();

            // Act

            var result = Engine.GetItemFromPoolIfBetter(CharacterPlayer, ItemLocationEnum.Feet);

            // Reset

            // Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(item2.Id, CharacterPlayer.Feet);    // The 2nd item is better, so did they swap?
        }

        [Test]
        public async Task RoundEngine_GetItemFromPoolIfBetter_Head_Better_Should_Pass()
        {
            Engine.MonsterList.Clear();

            // Both need to be character to fall through to the Name Test
            // Arrange
            var Character = new BaseCharacter
            {
                Speed = 20,
                Level = 1,
                CurrHealth = 1,
                ////ExperienceTotal = 1,
                Name = "Z",
                ListOrder = 1,
                Guid = "me"
            };

            // Add each model here to warm up and load it.
            Game.Helpers.DataSetsHelper.WarmUp();

            var item1 = new ItemModel { Attribute = AttributeEnum.Attack, Value = 1, Location = ItemLocationEnum.Head };
            var item2 = new ItemModel { Attribute = AttributeEnum.Attack, Value = 20, Location = ItemLocationEnum.Head };

            await ItemIndexViewModel.Instance.CreateAsync(item1);
            await ItemIndexViewModel.Instance.CreateAsync(item2);

            Engine.ItemPool.Add(item1);
            Engine.ItemPool.Add(item2);

            // Put the Item on the Character
            Character.AddItem(ItemLocationEnum.Head, item1.Id);

            var CharacterPlayer = new PlayerInfoModel(Character);
            Engine.CharacterList.Clear();
            Engine.CharacterList.Add(new PlayerInfoModel(Character));

            // Make the List
            Engine.PlayerList = Engine.MakePlayerList();

            // Act
            var result = Engine.GetItemFromPoolIfBetter(CharacterPlayer, ItemLocationEnum.Head);

            // Reset

            // Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(item2.Id, CharacterPlayer.Head);    // The 2nd item is better, so did they swap?
        }

        [Test]
        public async Task RoundEngine_GetItemFromPoolIfBetter_Head_Worse_Should_Skip()
        {
            Engine.MonsterList.Clear();

            // Both need to be character to fall through to the Name Test
            // Arrange
            var Character = new BaseCharacter
            {
                Speed = 20,
                Level = 1,
                CurrHealth = 1,
                ////ExperienceTotal = 1,
                Name = "Z",
                ListOrder = 1,
                Guid = "me"
            };

            // Add each model here to warm up and load it.
            Game.Helpers.DataSetsHelper.WarmUp();

            var item1 = new ItemModel { Attribute = AttributeEnum.Attack, Value = 1, Location = ItemLocationEnum.Head };
            var item2 = new ItemModel { Attribute = AttributeEnum.Attack, Value = 20, Location = ItemLocationEnum.Head };

            await ItemIndexViewModel.Instance.CreateAsync(item1);
            await ItemIndexViewModel.Instance.CreateAsync(item2);

            Engine.ItemPool.Add(item1);
            Engine.ItemPool.Add(item2);

            // Put the Item on the Character
            Character.AddItem(ItemLocationEnum.Head, item2.Id);

            var CharacterPlayer = new PlayerInfoModel(Character);
            Engine.CharacterList.Clear();
            Engine.CharacterList.Add(new PlayerInfoModel(Character));

            // Make the List
            Engine.PlayerList = Engine.MakePlayerList();

            // Act
            var result = Engine.GetItemFromPoolIfBetter(CharacterPlayer, ItemLocationEnum.Head);

            // Reset

            // Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(item2.Id, CharacterPlayer.Head);    // Kept the one
        }

        [Test]
        public async Task RoundEngine_GetItemFromPoolIfBetter_Pool_Empty_Should_Fail()
        {
            Engine.MonsterList.Clear();

            // Both need to be character to fall through to the Name Test
            // Arrange
            var Character = new BaseCharacter
            {
                Speed = 20,
                Level = 1,
                CurrHealth = 1,
                ////ExperienceTotal = 1,
                Name = "Z",
                ListOrder = 1,
                Guid = "me"
            };

            // Add each model here to warm up and load it.
            Game.Helpers.DataSetsHelper.WarmUp();

            var item1 = new ItemModel { Attribute = AttributeEnum.Attack, Value = 1, Location = ItemLocationEnum.Head };
            var item2 = new ItemModel { Attribute = AttributeEnum.Attack, Value = 20, Location = ItemLocationEnum.Head };

            await ItemIndexViewModel.Instance.CreateAsync(item1);
            await ItemIndexViewModel.Instance.CreateAsync(item2);

            //Engine.ItemPool.Add(item1);
            //Engine.ItemPool.Add(item2);

            // Put the Item on the Character
            Character.AddItem(ItemLocationEnum.Head, item2.Id);

            var CharacterPlayer = new PlayerInfoModel(Character);
            Engine.CharacterList.Clear();
            Engine.CharacterList.Add(new PlayerInfoModel(Character));

            // Make the List
            Engine.PlayerList = Engine.MakePlayerList();

            // Act
            var result = Engine.GetItemFromPoolIfBetter(CharacterPlayer, ItemLocationEnum.Head);

            // Reset

            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public async Task RoundEngine_PickupItemsFromPool_Default_Should_Pass()
        {
            Engine.MonsterList.Clear();

            // Both need to be character to fall through to the Name Test
            // Arrange
            var Character = new BaseCharacter
            {
                Speed = 20,
                Level = 1,
                CurrHealth = 1,
                ////ExperienceTotal = 1,
                Name = "Z",
                ListOrder = 1,
                Guid = "me"
            };

            // Add each model here to warm up and load it.
            Game.Helpers.DataSetsHelper.WarmUp();

            var CharacterPlayer = new PlayerInfoModel(Character);
            Engine.CharacterList.Clear();
            Engine.CharacterList.Add(new PlayerInfoModel(Character));

            // Make the List
            Engine.PlayerList = Engine.MakePlayerList();

            // Act
            var result = Engine.PickupItemsFromPool(CharacterPlayer);

            // Reset

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void RoundEngine_EndRound_Default_Should_Pass()
        {
            Engine.MonsterList.Clear();

            // Both need to be character to fall through to the Name Test
            // Arrange
            var Character = new BaseCharacter
            {
                Speed = 20,
                Level = 1,
                CurrHealth = 1,
                ////ExperienceTotal = 1,
                Name = "Z",
                ListOrder = 1,
                Guid = "me"
            };

            // Add each model here to warm up and load it.
            Game.Helpers.DataSetsHelper.WarmUp();

            var CharacterPlayer = new PlayerInfoModel(Character);
            Engine.CharacterList.Clear();
            Engine.CharacterList.Add(new PlayerInfoModel(Character));

            // Make the List
            Engine.PlayerList = Engine.MakePlayerList();

            // Act
            var result = Engine.EndRound();

            // Reset

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void RoundEngine_RoundNextTurn_No_Characters_Should_Return_GameOver()
        {
            Engine.MonsterList.Clear();

            // Arrange
            var Character = new BaseCharacter
            {
                Speed = 20,
                Level = 1,
                CurrHealth = 1,
                ////ExperienceTotal = 1,
                Name = "Characer",
                ListOrder = 1,
            };

            var Monster = new BaseMonster
            {
                Speed = 20,
                Level = 1,
                CurrHealth = 1,
                //ExperienceTotal = 1,
                Name = "Monster",
                ListOrder = 1,
            };

            // Add each model here to warm up and load it.
            Game.Helpers.DataSetsHelper.WarmUp();

            Engine.CharacterList.Clear();

            Engine.MonsterList.Add(new PlayerInfoModel(Character));

            // Make the List
            Engine.PlayerList = Engine.MakePlayerList();

            // Act
            var result = Engine.RoundNextTurn();

            // Reset

            // Assert
            Assert.AreEqual(RoundEnum.GameOver, result);
        }

        [Test]
        public void RoundEngine_RoundNextTurn_No_Monsters_Should_Return_NewRound()
        {
            Engine.MonsterList.Clear();

            // Arrange
            var Character = new BaseCharacter
            {
                Speed = 20,
                Level = 1,
                CurrHealth = 1,
                ////ExperienceTotal = 1,
                Name = "Characer",
                ListOrder = 1,
            };

            var Monster = new BaseMonster
            {
                Speed = 20,
                Level = 1,
                CurrHealth = 1,
                //ExperienceTotal = 1,
                Name = "Monster",
                ListOrder = 1,
            };

            // Add each model here to warm up and load it.
            Game.Helpers.DataSetsHelper.WarmUp();

            var CharacterPlayer = new PlayerInfoModel(Character);
            Engine.CharacterList.Clear();

            Engine.CharacterList.Add(new PlayerInfoModel(Character));

            //Engine.MonsterList.Add(new PlayerInfoModel(Character));

            // Make the List
            Engine.PlayerList = Engine.MakePlayerList();

            // Act
            var result = Engine.RoundNextTurn();

            // Reset

            // Assert
            Assert.AreEqual(RoundEnum.NewRound, result);
        }

        [Test]
        public void RoundEngine_RoundNextTurn_Characters_Monsters_Should_Return_NewRound()
        {
            Engine.MonsterList.Clear();

            // Arrange
            var Character = new BaseCharacter
            {
                Speed = 20,
                Level = 1,
                CurrHealth = 1,
                //ExperienceTotal = 1,
                Name = "Characer",
                ListOrder = 1,
            };

            var Monster = new BaseMonster
            {
                Speed = 20,
                Level = 1,
                CurrHealth = 1,
                //ExperienceTotal = 1,
                Name = "Monster",
                ListOrder = 1,
            };

            // Add each model here to warm up and load it.
            Game.Helpers.DataSetsHelper.WarmUp();

            var CharacterPlayer = new PlayerInfoModel(Character);
            Engine.CharacterList.Clear();

            Engine.CharacterList.Add(new PlayerInfoModel(Character));

            Engine.MonsterList.Add(new PlayerInfoModel(Character));

            // Make the List
            Engine.PlayerList = Engine.MakePlayerList();

            // Act
            var result = Engine.RoundNextTurn();

            // Reset

            // Assert
            Assert.AreEqual(RoundEnum.NextTurn, result);
        }

        [Test]
        public void RoundEngine_GetNextPlayerInList_Mike_Should_Return_Doug()
        {
            Engine.MonsterList.Clear();

            // Arrange
            var CharacterPlayerMike = new PlayerInfoModel(
                                        new BaseCharacter
                                        {
                                            Speed = 200,
                                            Level = 1,
                                            CurrHealth = 1,
                                            //ExperienceTotal = 1,
                                            Name = "Mike",
                                            ListOrder = 1,
                                        });

            var CharacterPlayerDoug = new PlayerInfoModel(
                                        new BaseCharacter
                                        {
                                            Speed = 20,
                                            Level = 1,
                                            CurrHealth = 1,
                                            //ExperienceTotal = 1,
                                            Name = "Doug",
                                            ListOrder = 2,
                                        });

            var CharacterPlayerSue = new PlayerInfoModel(
                                        new BaseCharacter
                                        {
                                            Speed = 2,
                                            Level = 1,
                                            CurrHealth = 1,
                                            //ExperienceTotal = 1,
                                            Name = "Sue",
                                            ListOrder = 3,
                                        });

            var MonsterPlayer = new PlayerInfoModel(
                                    new BaseMonster
                                    {
                                        Speed = 1,
                                        Level = 1,
                                        CurrHealth = 1,
                                        //ExperienceTotal = 1,
                                        Name = "Monster",
                                        ListOrder = 4,
                                    });

            // Add each model here to warm up and load it.
            Game.Helpers.DataSetsHelper.WarmUp();

            Engine.CharacterList.Clear();

            Engine.CharacterList.Add(CharacterPlayerMike);
            Engine.CharacterList.Add(CharacterPlayerDoug);
            Engine.CharacterList.Add(CharacterPlayerSue);

            Engine.MonsterList.Clear();
            Engine.MonsterList.Add(MonsterPlayer);

            // Make the List
            Engine.PlayerList = Engine.MakePlayerList();

            // Set Mike as the Player
            Engine.CurrentAttacker = CharacterPlayerMike;

            // Act
            var result = Engine.GetNextPlayerInList();

            // Reset


            // Assert
            Assert.AreEqual("Doug", result.Name);
        }

        [Test]
        public void RoundEngine_GetNextPlayerInList_Sue_Should_Return_Monster()
        {
            Engine.MonsterList.Clear();

            // Arrange
            var CharacterPlayerMike = new PlayerInfoModel(
                                        new BaseCharacter
                                        {
                                            Speed = 200,
                                            Level = 1,
                                            CurrHealth = 1,
                                            //ExperienceTotal = 1,
                                            Name = "Mike",
                                            ListOrder = 1,
                                        });

            var CharacterPlayerDoug = new PlayerInfoModel(
                                        new BaseCharacter
                                        {
                                            Speed = 20,
                                            Level = 1,
                                            CurrHealth = 1,
                                            //ExperienceTotal = 1,
                                            Name = "Doug",
                                            ListOrder = 2,
                                        });

            var CharacterPlayerSue = new PlayerInfoModel(
                                        new BaseCharacter
                                        {
                                            Speed = 2,
                                            Level = 1,
                                            CurrHealth = 1,
                                            //ExperienceTotal = 1,
                                            Name = "Sue",
                                            ListOrder = 3,
                                        });

            var MonsterPlayer = new PlayerInfoModel(
                                    new BaseMonster
                                    {
                                        Speed = 1,
                                        Level = 1,
                                        CurrHealth = 1,
                                        //ExperienceTotal = 1,
                                        Name = "Monster",
                                        ListOrder = 4,
                                    });

            // Add each model here to warm up and load it.
            Game.Helpers.DataSetsHelper.WarmUp();

            Engine.CharacterList.Clear();

            Engine.CharacterList.Add(CharacterPlayerMike);
            Engine.CharacterList.Add(CharacterPlayerDoug);
            Engine.CharacterList.Add(CharacterPlayerSue);

            Engine.MonsterList.Clear();
            Engine.MonsterList.Add(MonsterPlayer);

            // Make the List
            Engine.PlayerList = Engine.MakePlayerList();

            // Set Sue as the Player
            Engine.CurrentAttacker = CharacterPlayerSue;

            // Act
            var result = Engine.GetNextPlayerInList();

            // Reset


            // Assert
            Assert.AreEqual("Monster", result.Name);
        }

        [Test]
        public void RoundEngine_GetNextPlayerInList_Monster_Should_Return_Mike()
        {
            Engine.MonsterList.Clear();

            // Arrange
            var CharacterPlayerMike = new PlayerInfoModel(
                                        new BaseCharacter
                                        {
                                            Speed = 200,
                                            Level = 1,
                                            CurrHealth = 1,
                                            ////ExperienceTotal = 1,
                                            Name = "Mike",
                                            ListOrder = 1,
                                        });

            var CharacterPlayerDoug = new PlayerInfoModel(
                                        new BaseCharacter
                                        {
                                            Speed = 20,
                                            Level = 1,
                                            CurrHealth = 1,
                                            //ExperienceTotal = 1,
                                            Name = "Doug",
                                            ListOrder = 2,
                                        });

            var CharacterPlayerSue = new PlayerInfoModel(
                                        new BaseCharacter
                                        {
                                            Speed = 2,
                                            Level = 1,
                                            CurrHealth = 1,
                                            //ExperienceTotal = 1,
                                            Name = "Sue",
                                            ListOrder = 3,
                                        });

            var MonsterPlayer = new PlayerInfoModel(
                                    new BaseMonster
                                    {
                                        Speed = 1,
                                        Level = 1,
                                        CurrHealth = 1,
                                        //ExperienceTotal = 1,
                                        Name = "Monster",
                                        ListOrder = 4,
                                    });

            // Add each model here to warm up and load it.
            Game.Helpers.DataSetsHelper.WarmUp();

            Engine.CharacterList.Clear();

            Engine.CharacterList.Add(CharacterPlayerMike);
            Engine.CharacterList.Add(CharacterPlayerDoug);
            Engine.CharacterList.Add(CharacterPlayerSue);

            Engine.MonsterList.Clear();
            Engine.MonsterList.Add(MonsterPlayer);

            // Make the List
            Engine.PlayerList = Engine.MakePlayerList();

            // Act
            var result = Engine.GetNextPlayerInList();

            // Reset


            // Assert
            Assert.AreEqual("Mike", result.Name);
        }



        //[Test]
        public void RoundEngine_Monsters_Buffed_Ten_Should_Return_True (){
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
        }


        [Test]
        public void RoundEngine_Speed_Reversal_Test_Should_Return_True() {
            // Arrange
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

            // Reset

            // Assert
            Assert.AreEqual("B", result[0].Name);
        }


        [Test]
        public void RoundEngine_GetNextPlayerInList_EmptyList_Should_Return_Null()
        {
            // Arrange

            // Add each model here to warm up and load it.
            Game.Helpers.DataSetsHelper.WarmUp();

            var CharacterPlayerSue = new PlayerInfoModel(
                                        new BaseCharacter
                                        {
                                            Speed = 2,
                                            Level = 1,
                                            CurrHealth = 1,
                                            Experience = 1,
                                            Name = "Sue",
                                            ListOrder = 3,
                                        });

            var MonsterPlayer = new PlayerInfoModel(
                                    new BaseMonster
                                    {
                                        Speed = 1,
                                        Level = 1,
                                        CurrHealth = 1,
                                        Experience = 1,
                                        Name = "Monster",
                                        ListOrder = 4,
                                    });

            Engine.CharacterList.Clear();

            Engine.CharacterList.Add(CharacterPlayerSue);

            Engine.MonsterList.Clear();
            Engine.MonsterList.Add(MonsterPlayer);

            // Make the List
            Engine.PlayerList = Engine.MakePlayerList();

            // Clear the List to cause the error
            Engine.PlayerList.Clear();

            // Arrange

            // Act
            var result = Engine.GetNextPlayerInList();

            // Reset


            // Assert
            Assert.AreEqual(null, result);
        }


        [Test]
        public void RoundEngine_populatePotionsList_Healing_Off_Should_Pass()
        {
            //Arrange
            Engine.RoundHealing = RoundHealingEnum.Healing_off;

            //Act
            var result = Engine.populatePotionsList();

            //Reset

            //Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void RoundEngine_populatePotionsLIst_Healing_Off_Should_fail()
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
        public void RoundEngine_populatePotionsList_Healing_On_Should_Pass()
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
        public void RoundEngine_populatePotionsList_Healing_On_should_fail()
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