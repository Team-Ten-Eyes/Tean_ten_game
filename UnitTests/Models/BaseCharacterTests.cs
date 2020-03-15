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
    }
}
