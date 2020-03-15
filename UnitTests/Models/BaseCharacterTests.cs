using NUnit.Framework;

using Game.Models;
using Game.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace UnitTests.Models
{
    [TestFixture]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "<Pending>")]
    public class BaseCharacterTests
    {
        [TearDown]
        public void TearDown()
        { // For Tear down delete the Item Dataset.
            // Test that need the Item Dataset should set it
            ItemIndexViewModel.Instance.Dataset.Clear();
        }

        [Test]
        public void BaseCharacter_constructor_Default_Should_pass()
        {
            //arDefense

            //Act
            var result = new BaseCharacter();

            //reset

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void BaseCharacter_New_item_Should_copy()
        {
            //ArDefense
            var dataNew = new BaseCharacter();
            dataNew.Attack = 2;
            dataNew.Id = "oldID";

            //Act
            var result = new BaseCharacter(dataNew);

            //Reset

            //Assert
            Assert.AreNotEqual("oldID", result.Id);
        }

        [Test]
        public void BaseCharacter_Get_Default_Should_Pass()
        {
            //ArDefense
            //Act
            var result = new BaseCharacter();

            //Reset

            //Assert
            Assert.IsNotNull(result.Attack);
            Assert.IsNotNull(result.Defense);
            Assert.IsNotNull(result.Speed);
            Assert.IsNotNull(result.CurrHealth);
            Assert.IsNotNull(result.Mana);
            Assert.IsNotNull(result.Attribute);
        }

        [Test]
        public void BaseCharacter_Set_Default_Should_Pass()
        {
            //ArDefense

            //Act
            var result = new BaseCharacter();
            result.Attack = 6;
            result.Defense = 7;
            result.Speed = 8;

            //Reset

            //Assert
            Assert.AreEqual(6, result.Attack);
            Assert.AreEqual(7, result.Defense);
            Assert.AreEqual(8, result.Speed);

            Assert.IsNotNull(result.Id);
            Assert.AreEqual(result.Id, result.Guid);

            Assert.AreEqual("Michelle.png", result.ImageURI);
            Assert.AreEqual(PlayerTypeEnum.Character, result.PlayerType);


            Assert.AreEqual(true, result.Alive);
            Assert.AreEqual(0, result.Order);
            Assert.AreEqual(0, result.ListOrder);
            Assert.AreEqual(1, result.Level);
            Assert.AreEqual(299, result.ExperienceRemaining);
            Assert.AreEqual(1, result.CurrHealth);
            Assert.AreEqual(1, result.Mana);
            Assert.AreEqual(1, result.MaxMana);
            Assert.AreEqual(1, result.MaxHealth);
            Assert.AreEqual(0, result.Experience);

            Assert.AreEqual(null, result.Head);
            Assert.AreEqual(null, result.Feet);
            Assert.AreEqual(null, result.Necklass);
            Assert.AreEqual(null, result.PrimaryHand);
            Assert.AreEqual(null, result.OffHand);
            Assert.AreEqual(null, result.RightFinger);
            Assert.AreEqual(null, result.LeftFinger);

            Assert.AreEqual(CharacterTypeEnum.Bravery, result.Attribute);
        }
    }
}
