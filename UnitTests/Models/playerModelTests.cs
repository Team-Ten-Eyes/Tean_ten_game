using NUnit.Framework;

using Game.Models;
using Game.Services;
using Game.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using Game.Helpers;

namespace UnitTests.Models
{
    [TestFixture]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "<Pending>")]
    public class BasePlayerModelTests
    {
        [TearDown]
        public void TearDown()
        {
            // For Tear down delete the Item Dataset.
            // Test that need the Item Dataset should set it
            ItemIndexViewModel.Instance.Dataset.Clear();
        }

        [Test]
        public void BasePlayerModel_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = new PlayerModel<BaseCharacter>();

            // Reset

            // Assert
            Assert.AreEqual("Default", result.Name);
        }

        [Test]
        public void BasePlayerModel_Get_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = new PlayerModel<BaseCharacter>();

            // Reset

            // Assert
            Assert.IsNotNull(result.Id);
           
            Assert.AreEqual(PlayerTypeEnum.Unknown, result.PlayerType);
            Assert.AreEqual(true, result.Alive);
            Assert.AreEqual(0, result.Order);
            Assert.AreEqual(result.Id, result.Guid);
            Assert.AreEqual(0, result.ListOrder);
            Assert.AreEqual(0, result.Speed);
            Assert.AreEqual(1, result.Level);

            Assert.AreEqual(0, result.ExperienceRemaining);
            Assert.AreEqual(1, result.CurrHealth);
            Assert.AreEqual(1, result.MaxHealth);
            Assert.AreEqual(0, result.Experience);
            Assert.AreEqual(0, result.Defense);
            Assert.AreEqual(0, result.Attack);
            Assert.AreEqual(null, result.Head);
            Assert.AreEqual(null, result.Feet);
            Assert.AreEqual(null, result.Necklass);
            Assert.AreEqual(null, result.PrimaryHand);
            Assert.AreEqual(null, result.OffHand);
            Assert.AreEqual(null, result.RightFinger);
            Assert.AreEqual(null, result.LeftFinger);

            Assert.AreEqual(DifficultyEnum.Unknown, result.Difficulty);
        }

        [Test]
        public void PlayerModel_Set_Get_Default_Should_Pass()
        {
            // Arrange
            var result = new PlayerModel<BaseCharacter>();

            // Act
            result.Id = "bogus";
            result.ImageURI = "uri";
            result.PlayerType = PlayerTypeEnum.Monster;
            result.Alive = false;
            result.Order = 100;
            result.Guid = "guid";
            result.ListOrder = 200;
            result.Speed = 300;
            result.Level = 400;
           
            result.ExperienceRemaining = 500;
            result.CurrHealth = 600;
            result.MaxHealth = 700;
            result.Experience= 800;
            result.Defense = 900;
            result.Attack = 123;
            result.Head = "head";
            result.Feet = "feet";
            result.Necklass = "necklass";
            result.PrimaryHand = "primaryhand";
            result.OffHand = "offhand";
            result.RightFinger = "rightfinger";
            result.LeftFinger = "leftfinger";

            // Reset

            // Assert
            Assert.AreEqual("bogus", result.Id);
            Assert.AreEqual("uri", result.ImageURI);
            Assert.AreEqual(PlayerTypeEnum.Monster, result.PlayerType);
            Assert.AreEqual(false, result.Alive);
            Assert.AreEqual(100, result.Order);
            Assert.AreEqual("guid", result.Guid);
            Assert.AreEqual(200, result.ListOrder);
            Assert.AreEqual(300, result.Speed);
            Assert.AreEqual(400, result.Level);
            
            Assert.AreEqual(500, result.ExperienceRemaining);
            Assert.AreEqual(600, result.CurrHealth);
            Assert.AreEqual(700, result.MaxHealth);
            Assert.AreEqual(800, result.Experience);
            Assert.AreEqual(900, result.Defense);
            Assert.AreEqual(123, result.Attack);
            Assert.AreEqual("head", result.Head);
            Assert.AreEqual("feet", result.Feet);
            Assert.AreEqual("necklass", result.Necklass);
            Assert.AreEqual("primaryhand", result.PrimaryHand);
            Assert.AreEqual("offhand", result.OffHand);
            Assert.AreEqual("rightfinger", result.RightFinger);
            Assert.AreEqual("leftfinger", result.LeftFinger);
        }

        [Test]
        public void PlayerModel_Update_Default_Should_Pass()
        {
            // Arrange
            var data = new PlayerModel<BaseCharacter>();

            // Act
            var result = data.Update(null);

            // Reset

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void PlayerModel_GetAttack_Default_Should_Pass()
        {
            // Arrange
            var data = new PlayerModel<BaseCharacter>();

            // Act
            var result = data.GetAttack();

            // Reset

            // Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        public void PlayerModel_GetDefense_Default_Should_Pass()
        {
            // Arrange
            var data = new PlayerModel<BaseCharacter>();

            // Act
            var result = data.GetDefense();

            // Reset

            // Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        public void PlayerModel_GetSpeed_Default_Should_Pass()
        {
            // Arrange
            var data = new PlayerModel<BaseCharacter>();

            // Act
            var result = data.GetSpeed();

            // Reset

            // Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        public void PlayerModel_GetHealthCurrent_Default_Should_Pass()
        {
            // Arrange
            var data = new PlayerModel<BaseCharacter>();

            // Act
            var result = data.GetCurrentHealthTotal;

            // Reset

            // Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        public void PlayerModel_GetHealthMax_Default_Should_Pass()
        {
            // Arrange
            var data = new PlayerModel<BaseCharacter>();

            // Act
            var result = data.GetMaxHealthTotal;

            // Reset

            // Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        public void PlayerModel_GetDamageRollValue_Default_Should_Pass()
        {
            // Arrange
            var data = new PlayerModel<BaseCharacter>();
            data.Level = 1;

            // Act
            var result = data.GetDamageRollValue();

            // Reset

            // Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        public void PlayerModel_TakeDamage_Valid_Should_Pass()
        {
            // Arrange
            var data = new PlayerModel<BaseCharacter>();

            // Act
            var result = data.TakeDamage(1);

            // Reset

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void PlayerModel_TakeDamage_InValid_Should_Fail()
        {
            // Arrange
            var data = new PlayerModel<BaseCharacter>();

            // Act
            var result = data.TakeDamage(0);

            // Reset

            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public void PlayerModel_CauseDeath_Default_Should_Pass()
        {
            // Arrange
            var data = new PlayerModel<BaseCharacter>();

            // Act
            var result = data.CauseDeath();

            // Reset

            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public void PlayerModel_FormatOutput_Default_Should_Pass()
        {
            // Arrange
            var data = new PlayerModel<BaseCharacter>();

            // Act
            var result = data.FormatOutput();

            // Reset

            // Assert
            Assert.AreEqual("", result);
        }

        [Test]
        public void PlayerModel_AddExperience_Default_Should_Pass()
        {
            // Arrange
            var data = new PlayerModel<BaseCharacter>();

            // Act
            var result = data.AddExperience(0);

            // Reset

            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public void PlayerModel_AddExperience_InValid_Neg_Should_Fail()
        {
            // Arrange
            var data = new PlayerModel<BaseCharacter>();

            // Act
            var result = data.AddExperience(-1);

            // Reset

            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public void PlayerModel_AddExperience_InValid_Max_Level_Should_Fail()
        {
            // Arrange
            var data = new PlayerModel<BaseCharacter>();
            data.Level = 1000;

            // Act
            var result = data.AddExperience(10);

            // Reset

            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public void PlayerModel_AddExperience_Valid_Level__Up_Should_Pass()
        {
            // Arrange
            var data = new PlayerModel<BaseCharacter>();
            data.Level = 1;
            data.Experience = 10000;

            // Act
            var result = data.AddExperience(1);

            // Reset

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void PlayerModel_CalculateExperienceEarned_Default_Should_Pass()
        {
            // Arrange
            var data = new PlayerModel<BaseCharacter>();

            // Act
            var result = data.CalculateExperienceEarned(0);

            // Reset

            // Assert
            Assert.AreEqual(0, result);
        }

        [Test]
        public void PlayerModel_GetItem_Default_Should_Pass()
        {
            // Arrange
            var data = new PlayerModel<BaseCharacter>();

            // Act
            var result = data.GetItem("test");

            // Reset

            // Assert
            Assert.AreEqual(null, result);
        }

        [Test]
        public void PlayerModel_GetItemByLocation_Head_Default_Should_Pass()
        {
            // Arrange
            var data = new PlayerModel<BaseCharacter>();

            // Act
            var result = data.GetItemByLocation(ItemLocationEnum.Head);

            // Reset

            // Assert
            Assert.AreEqual(null, result);
        }

        [Test]
        public void PlayerModel_GetItemByLocation_Feet_Default_Should_Pass()
        {
            // Arrange
            var data = new PlayerModel<BaseCharacter>();

            // Act
            var result = data.GetItemByLocation(ItemLocationEnum.Feet);

            // Reset

            // Assert
            Assert.AreEqual(null, result);
        }

        [Test]
        public void PlayerModel_GetItemByLocation_Necklass_Default_Should_Pass()
        {
            // Arrange
            var data = new PlayerModel<BaseCharacter>();

            // Act
            var result = data.GetItemByLocation(ItemLocationEnum.Necklass);

            // Reset

            // Assert
            Assert.AreEqual(null, result);
        }

        [Test]
        public void PlayerModel_GetItemByLocation_PrimaryHand_Default_Should_Pass()
        {
            // Arrange
            var data = new PlayerModel<BaseCharacter>();

            // Act
            var result = data.GetItemByLocation(ItemLocationEnum.PrimaryHand);

            // Reset

            // Assert
            Assert.AreEqual(null, result);
        }

        [Test]
        public void PlayerModel_GetItemByLocation_OffHand_Default_Should_Pass()
        {
            // Arrange
            var data = new PlayerModel<BaseCharacter>();

            // Act
            var result = data.GetItemByLocation(ItemLocationEnum.OffHand);

            // Reset

            // Assert
            Assert.AreEqual(null, result);
        }

        [Test]
        public void PlayerModel_GetItemByLocation_RightFinger_Default_Should_Pass()
        {
            // Arrange
            var data = new PlayerModel<BaseCharacter>();

            // Act
            var result = data.GetItemByLocation(ItemLocationEnum.RightFinger);

            // Reset

            // Assert
            Assert.AreEqual(null, result);
        }

        [Test]
        public void PlayerModel_GetItemByLocation_LeftFinger_Default_Should_Pass()
        {
            // Arrange
            var data = new PlayerModel<BaseCharacter>();

            // Act
            var result = data.GetItemByLocation(ItemLocationEnum.LeftFinger);

            // Reset

            // Assert
            Assert.AreEqual(null, result);
        }

        [Test]
        public void PlayerModel_GetItemByLocation_Unknown_Default_Should_Pass()
        {
            // Arrange
            var data = new PlayerModel<BaseCharacter>();

            // Act
            var result = data.GetItemByLocation(ItemLocationEnum.Unknown);

            // Reset

            // Assert
            Assert.AreEqual(null, result);
        }

        [Test]
        public async Task PlayerModel_DropAllItems_Default_Should_Pass()
        {
            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Attack, Value = 1, Id = "head" });
            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Attack, Value = 20, Id = "necklass" });

            var item = ItemIndexViewModel.Instance.Dataset.FirstOrDefault();

            // Arrange
            var data = new PlayerModel<BaseCharacter>
            {
                Head = item.Id,
                Necklass = item.Id,
                PrimaryHand = item.Id,
                OffHand = item.Id,
                RightFinger = item.Id,
                LeftFinger = item.Id,
                Feet = item.Id,
            };

            // Act
            var result = data.DropAllItems();

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void PlayerModel_AddItem_Unknown_Should_Fail()
        {
            // Arrange
            var data = new PlayerModel<BaseCharacter>();

            // Act
            var result = data.AddItem(ItemLocationEnum.Unknown, "bogus");

            // Reset

            // Assert
            Assert.AreEqual(null, result);
        }

        [Test]
        public async Task PlayerModel_AddItem_Default_Should_Pass()
        {
            // Arrange
            var data = new PlayerModel<BaseCharacter>();

            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Attack, Value = 1, Id = "head" });
            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Attack, Value = 20, Id = "necklass" });

            var itemOld = ItemIndexViewModel.Instance.Dataset.FirstOrDefault();

            // Act

            // Add the second item, this will return the first item as the one replaced which is null
            var result = data.AddItem(ItemLocationEnum.Head, itemOld.Id);

            // Reset

            // Assert
            Assert.AreEqual(null, result);
        }

        [Test]
        public async Task PlayerModel_AddItem_Default_Replace_Should_Pass()
        {
            // Arrange
            var data = new PlayerModel<BaseCharacter>();

            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Attack, Value = 1, Id = "head" });
            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Attack, Value = 20, Id = "necklass" });

            var itemOld = ItemIndexViewModel.Instance.Dataset.FirstOrDefault();
            var itemNew = ItemIndexViewModel.Instance.Dataset.LastOrDefault();

            // Add the first item
            data.AddItem(ItemLocationEnum.Head, itemOld.Id);

            // Act

            // Add the second item, this will return the first item as the one replaced
            var result = data.AddItem(ItemLocationEnum.Head, itemNew.Id);

            // Reset

            // Assert
            Assert.AreEqual(itemOld.Id, result.Id);
        }

        [Test]
        public async Task PlayerModel_GetItemBonus_Default_Attack_Should_Pass()
        {
            // Arrange
            // Add each model here to warm up and load it.
            Game.Helpers.DataSetsHelper.WarmUp();

            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Attack, Value = 1, Id = "head" });
            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Attack, Value = 20, Id = "necklass" });
            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Attack, Value = 300, Id = "PrimaryHand" });
            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Attack, Value = 4000, Id = "OffHand" });
            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Attack, Value = 50000, Id = "RightFinger" });
            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Attack, Value = 600000, Id = "LeftFinger" });
            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Attack, Value = 7000000, Id = "feet" });

            var data = new PlayerModel<BaseCharacter>();

            // Add the first item
            data.AddItem(ItemLocationEnum.Head, (await ItemIndexViewModel.Instance.ReadAsync("head")).Id);
            data.AddItem(ItemLocationEnum.Necklass, (await ItemIndexViewModel.Instance.ReadAsync("necklass")).Id);
            data.AddItem(ItemLocationEnum.PrimaryHand, (await ItemIndexViewModel.Instance.ReadAsync("PrimaryHand")).Id);
            data.AddItem(ItemLocationEnum.OffHand, (await ItemIndexViewModel.Instance.ReadAsync("OffHand")).Id);
            data.AddItem(ItemLocationEnum.RightFinger, (await ItemIndexViewModel.Instance.ReadAsync("RightFinger")).Id);
            data.AddItem(ItemLocationEnum.LeftFinger, (await ItemIndexViewModel.Instance.ReadAsync("LeftFinger")).Id);
            data.AddItem(ItemLocationEnum.Feet, (await ItemIndexViewModel.Instance.ReadAsync("feet")).Id);

            // Act

            // Add the second item, this will return the first item as the one replaced
            var result = data.GetItemBonus(AttributeEnum.Attack);

            // Reset

            // Assert
            Assert.AreEqual(7654321, result);
        }

        [Test]
        public async Task PlayerModel_GetAttackTotal_Default_Attack_Should_Pass()
        {
            // Arrange
            // Add each model here to warm up and load it.
            Game.Helpers.DataSetsHelper.WarmUp();

            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Attack, Value = 1, Id = "head" });
            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Attack, Value = 20, Id = "necklass" });
            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Attack, Value = 300, Id = "PrimaryHand" });
            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Attack, Value = 4000, Id = "OffHand" });
            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Attack, Value = 50000, Id = "RightFinger" });
            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Attack, Value = 600000, Id = "LeftFinger" });
            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Attack, Value = 7000000, Id = "feet" });

            var data = new PlayerModel<BaseCharacter>();

            // Add the first item
            data.AddItem(ItemLocationEnum.Head, (await ItemIndexViewModel.Instance.ReadAsync("head")).Id);
            data.AddItem(ItemLocationEnum.Necklass, (await ItemIndexViewModel.Instance.ReadAsync("necklass")).Id);
            data.AddItem(ItemLocationEnum.PrimaryHand, (await ItemIndexViewModel.Instance.ReadAsync("PrimaryHand")).Id);
            data.AddItem(ItemLocationEnum.OffHand, (await ItemIndexViewModel.Instance.ReadAsync("OffHand")).Id);
            data.AddItem(ItemLocationEnum.RightFinger, (await ItemIndexViewModel.Instance.ReadAsync("RightFinger")).Id);
            data.AddItem(ItemLocationEnum.LeftFinger, (await ItemIndexViewModel.Instance.ReadAsync("LeftFinger")).Id);
            data.AddItem(ItemLocationEnum.Feet, (await ItemIndexViewModel.Instance.ReadAsync("feet")).Id);

            // Act

            // Add the second item, this will return the first item as the one replaced
            var result = data.GetAttackTotal;

            // Reset

            // Assert
            Assert.AreEqual(7654322, result);
        }

        [Test]
        public async Task PlayerModel_GetDefenseTotal_Default_Defense_Should_Pass()
        {
            // Arrange
            // Add each model here to warm up and load it.
            Game.Helpers.DataSetsHelper.WarmUp();

            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Defense, Value = 1, Id = "head" });
            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Defense, Value = 20, Id = "necklass" });
            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Defense, Value = 300, Id = "PrimaryHand" });
            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Defense, Value = 4000, Id = "OffHand" });
            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Defense, Value = 50000, Id = "RightFinger" });
            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Defense, Value = 600000, Id = "LeftFinger" });
            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Defense, Value = 7000000, Id = "feet" });

            var data = new PlayerModel<BaseCharacter>();

            // Add the first item
            data.AddItem(ItemLocationEnum.Head, (await ItemIndexViewModel.Instance.ReadAsync("head")).Id);
            data.AddItem(ItemLocationEnum.Necklass, (await ItemIndexViewModel.Instance.ReadAsync("necklass")).Id);
            data.AddItem(ItemLocationEnum.PrimaryHand, (await ItemIndexViewModel.Instance.ReadAsync("PrimaryHand")).Id);
            data.AddItem(ItemLocationEnum.OffHand, (await ItemIndexViewModel.Instance.ReadAsync("OffHand")).Id);
            data.AddItem(ItemLocationEnum.RightFinger, (await ItemIndexViewModel.Instance.ReadAsync("RightFinger")).Id);
            data.AddItem(ItemLocationEnum.LeftFinger, (await ItemIndexViewModel.Instance.ReadAsync("LeftFinger")).Id);
            data.AddItem(ItemLocationEnum.Feet, (await ItemIndexViewModel.Instance.ReadAsync("feet")).Id);

            // Act

            // Add the second item, this will return the first item as the one replaced
            var result = data.GetDefenseTotal;

            // Reset

            // Assert
            Assert.AreEqual(7654322, result);
        }

        [Test]
        public async Task PlayerModel_GetSpeedTotal_Default_Speed_Should_Pass()
        {
            // Arrange
            // Add each model here to warm up and load it.
            Game.Helpers.DataSetsHelper.WarmUp();

            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Speed, Value = 1, Id = "head" });
            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Speed, Value = 20, Id = "necklass" });
            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Speed, Value = 300, Id = "PrimaryHand" });
            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Speed, Value = 4000, Id = "OffHand" });
            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Speed, Value = 50000, Id = "RightFinger" });
            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Speed, Value = 600000, Id = "LeftFinger" });
            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Speed, Value = 7000000, Id = "feet" });

            var data = new PlayerModel<BaseCharacter>();

            // Add the first item
            data.AddItem(ItemLocationEnum.Head, (await ItemIndexViewModel.Instance.ReadAsync("head")).Id);
            data.AddItem(ItemLocationEnum.Necklass, (await ItemIndexViewModel.Instance.ReadAsync("necklass")).Id);
            data.AddItem(ItemLocationEnum.PrimaryHand, (await ItemIndexViewModel.Instance.ReadAsync("PrimaryHand")).Id);
            data.AddItem(ItemLocationEnum.OffHand, (await ItemIndexViewModel.Instance.ReadAsync("OffHand")).Id);
            data.AddItem(ItemLocationEnum.RightFinger, (await ItemIndexViewModel.Instance.ReadAsync("RightFinger")).Id);
            data.AddItem(ItemLocationEnum.LeftFinger, (await ItemIndexViewModel.Instance.ReadAsync("LeftFinger")).Id);
            data.AddItem(ItemLocationEnum.Feet, (await ItemIndexViewModel.Instance.ReadAsync("feet")).Id);

            // Act

            // Add the second item, this will return the first item as the one replaced
            var result = data.GetSpeedTotal;

            // Reset

            // Assert
            Assert.AreEqual(7654322, result);
        }

        [Test]
        public async Task PlayerModel_GetDamageRollValue_Default_Speed_Should_Pass()
        {
            // Arrange
            // Add each model here to warm up and load it.
            Game.Helpers.DataSetsHelper.WarmUp();

            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Attack, Value = 1, Id = "head" });
            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Attack, Value = 20, Id = "necklass" });
            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Attack, Value = 300, Id = "PrimaryHand" });
            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Attack, Value = 4000, Id = "OffHand" });
            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Attack, Value = 50000, Id = "RightFinger" });
            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Attack, Value = 600000, Id = "LeftFinger" });
            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Attack, Value = 7000000, Id = "feet" });

            var data = new PlayerModel<BaseCharacter>();
            data.Level = 1;

            // Add the first item
            data.AddItem(ItemLocationEnum.Head, (await ItemIndexViewModel.Instance.ReadAsync("head")).Id);
            data.AddItem(ItemLocationEnum.Necklass, (await ItemIndexViewModel.Instance.ReadAsync("necklass")).Id);
            data.AddItem(ItemLocationEnum.PrimaryHand, (await ItemIndexViewModel.Instance.ReadAsync("PrimaryHand")).Id);
            data.AddItem(ItemLocationEnum.OffHand, (await ItemIndexViewModel.Instance.ReadAsync("OffHand")).Id);
            data.AddItem(ItemLocationEnum.RightFinger, (await ItemIndexViewModel.Instance.ReadAsync("RightFinger")).Id);
            data.AddItem(ItemLocationEnum.LeftFinger, (await ItemIndexViewModel.Instance.ReadAsync("LeftFinger")).Id);
            data.AddItem(ItemLocationEnum.Feet, (await ItemIndexViewModel.Instance.ReadAsync("feet")).Id);

            Game.Helpers.DiceHelper.EnableForcedRolls();
            Game.Helpers.DiceHelper.SetForcedRollValue(1);

            // Act

            // Add the second item, this will return the first item as the one replaced
            var result = data.GetDamageRollValue();

            // Reset
            Game.Helpers.DiceHelper.DisableForcedRolls();

            // Assert
            Assert.AreEqual(2, result);
        }

        [Test]
        public async Task PlayerModel_GetDamageItemBonus_Default_Speed_Should_Pass()
        {
            // Arrange
            // Add each model here to warm up and load it.
            Game.Helpers.DataSetsHelper.WarmUp();

            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Attack, Value = 300, Id = "PrimaryHand", Damage = 1 });

            var data = new PlayerModel<BaseCharacter>();
            data.Level = 1;

            // Add the first item
            data.AddItem(ItemLocationEnum.PrimaryHand, (await ItemIndexViewModel.Instance.ReadAsync("PrimaryHand")).Id);

            Game.Helpers.DiceHelper.EnableForcedRolls();
            Game.Helpers.DiceHelper.SetForcedRollValue(1);

            // Act

            // Add the second item, this will return the first item as the one replaced
            var result = data.GetDamageItemBonus;

            // Reset
            Game.Helpers.DiceHelper.DisableForcedRolls();


            // Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        public async Task PlayerModel_GetDamageItemBonusString_Default_Speed_Should_Pass()
        {
            // Arrange
            // Add each model here to warm up and load it.
            DataSetsHelper.WarmUp();

            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Attack, Value = 300, Id = "PrimaryHand", Damage = 1 });

            var data = new PlayerModel<BaseCharacter>
            {
                Level = 1
            };

            // Add the first item
            data.AddItem(ItemLocationEnum.PrimaryHand, (await ItemIndexViewModel.Instance.ReadAsync("PrimaryHand")).Id);

            DiceHelper.EnableForcedRolls();
            DiceHelper.SetForcedRollValue(1);

            // Act

            // Add the second item, this will return the first item as the one replaced
            var result = data.GetDamageItemBonusString;

            // Reset
            DiceHelper.DisableForcedRolls();


            // Assert
            Assert.AreEqual("1D 1", result);
        }

        [Test]
        public async Task PlayerModel_GetDamageTotalString_Default_Speed_Should_Pass()
        {
            // Arrange
            // Add each model here to warm up and load it.
            DataSetsHelper.WarmUp();

            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Attack, Value = 300, Id = "PrimaryHand", Damage = 1 });

            var data = new PlayerModel<BaseCharacter>();
            data.Level = 1;

            // Add the first item
            data.AddItem(ItemLocationEnum.PrimaryHand, (await ItemIndexViewModel.Instance.ReadAsync("PrimaryHand")).Id);

            DiceHelper.EnableForcedRolls();
            DiceHelper.SetForcedRollValue(1);

            // Act

            // Add the second item, this will return the first item as the one replaced
            var result = data.GetDamageTotalString;

            // Reset
            DiceHelper.DisableForcedRolls();

            // Assert
            Assert.AreEqual("1 + 1D 1", result);
        }

        [Test]
        public async Task PlayerModel_ItemSlotsFormatOutput_Full_Should_Pass()
        {
            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Attack, Value = 1, Id = "head" });
            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Attack, Value = 20, Id = "necklass" });

            var item = ItemIndexViewModel.Instance.Dataset.FirstOrDefault();

            // Arrange
            var data = new PlayerModel<BaseCharacter>
            {
                Head = item.Id,
                Necklass = item.Id,
                PrimaryHand = item.Id,
                OffHand = item.Id,
                RightFinger = item.Id,
                LeftFinger = item.Id,
                Feet = item.Id,
                UniqueItem = item.Id,
            };

            // Act
            var result = data.ItemSlotsFormatOutput();

            // Reset

            // Assert
            Assert.AreEqual(true, result.Contains("Attack"));
        }

        [Test]
        public void PlayerModel_ItemSlotsFormatOutput_Empty_Should_Pass()
        {
            // Arrange
            var data = new PlayerModel<BaseCharacter>
            {
                Head = null,
                Necklass = null,
                PrimaryHand = null,
                OffHand = null,
                RightFinger = null,
                LeftFinger = null,
                Feet = null,
                UniqueItem = null,
            };

            // Act
            var result = data.ItemSlotsFormatOutput();

            // Reset

            // Assert
            Assert.AreEqual(true, string.IsNullOrEmpty(result));
        }

        [Test]
        public void PlayerModel_LevelUpToValue_Valid_Should_Pass()
        {
            // Arrange
            var TargetLevel = 2;
            var NeededExperience = LevelTableHelper.Instance.LevelDetailsList[TargetLevel].Experience;

            var data = new PlayerModel<BaseCharacter>
            {
                Level = 1,
                Experience = NeededExperience + 1
            };

            // Act
            var result = data.LevelUpToValue(2);

            // Reset

            // Assert
            Assert.AreEqual(2, result);
        }

        [Test]
        public void PlayerModel_LevelUpToValue_InValid_Same_Level_Should_Skip()
        {
            // Arrange
            var data = new PlayerModel<BaseCharacter>
            {
                Level = 1,
                Experience = 10000,
            };

            // Act
            var result = data.LevelUpToValue(1);

            // Reset

            // Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        public void PlayerModel_LevelUpToValue_InValid_Neg_Level_Should_Skip()
        {
            // Arrange
            var data = new PlayerModel<BaseCharacter>
            {
                Level = 1,
                Experience = 10000,
            };

            // Act
            var result = data.LevelUpToValue(-1);

            // Reset

            // Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        public void PlayerModel_LevelUpToValue_InValid_Max_Level_Should_Skip()
        {
            // Arrange
            var data = new PlayerModel<BaseCharacter>
            {
                Level = 1,
                Experience = 1000000,
            };

            // Act
            var result = data.LevelUpToValue(LevelTableHelper.MaxLevel + 1);

            // Reset

            // Assert
            Assert.AreEqual(data.Level, result);
        }

        [Test]
        public void PlayerModel_LevelUpToValue_InValid_Lower_Level_Should_Fail()
        {
            // Arrange
            var data = new PlayerModel<BaseCharacter>
            {
                Level = 5,
                Experience = 10000,
            };

            // Act
            var result = data.LevelUpToValue(1);

            // Reset

            // Assert
            Assert.AreEqual(5, result);
        }

        [Test]
        public void PlayerModel_LevelUp_InValid_Level_1_No_Experience_Should_Fail()
        {
            // Arrange
            var data = new PlayerModel<BaseCharacter>
            {
                Level = 1,
                Experience = -1,
            };

            // Act
            var result = data.LevelUp();

            // Reset

            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public void PlayerModel_LevelUp_Valid_Level_2_Should_Pass()
        {
            // Arrange
            var data = new PlayerModel<BaseCharacter>
            {
                Level = 1,
                Experience = 301,
            };

            // Act
            var result = data.LevelUp();

            // Reset

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void PlayerModel_CalculateExperienceEarned_0_Should_Skip()
        {
            var data = new PlayerModel<BaseMonster>
            {
                ExperienceRemaining = 3000,
                MaxHealth = 20,
                CurrHealth = 20
            };

            // Call calculate experience before applying damage
            var Result = data.CalculateExperienceEarned(0);

            var Expected = 0;

            Assert.AreEqual(Expected, Result);
        }

        [Test]
        public void BasePlayerModel_CalculateExperienceEarned_3_Should_Pass()
        {
            var data = new PlayerModel<BaseMonster>
            {
                ExperienceRemaining = 3000,
                MaxHealth = 20,
                CurrHealth = 20
            };

            // Call calculate experience before applying damage
            var Result = data.CalculateExperienceEarned(3);

            var Expected = 450;

            Assert.AreEqual(Expected, Result, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void PlayerModel_CalculateExperienceEarned_None_Avaiable_Should_Pass()
        {
            var data = new PlayerModel<BaseMonster>
            {
                ExperienceRemaining = 0,
                MaxHealth = 20,
                CurrHealth = 20
            };

            // Call calculate experience before applying damage
            var Result = data.CalculateExperienceEarned(3);

            var Expected = 0;

            Assert.AreEqual(Expected, Result, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void PlayerModel_CalculateExperienceEarned_Remaining_Should_Pass()
        {
            //      CalculateExperienceEarned(int)  11	100.00%	0	0.00%

            var data = new PlayerModel<BaseMonster>
            {
                ExperienceRemaining = 1,
                MaxHealth = 20,
                CurrHealth = 20
            };

            // Call calculate experience before applying damage
            var Result = data.CalculateExperienceEarned(3);

            var Expected = 1;

            Assert.AreEqual(Expected, Result, TestContext.CurrentContext.Test.Name);
        }

    

        

       
    }
}
