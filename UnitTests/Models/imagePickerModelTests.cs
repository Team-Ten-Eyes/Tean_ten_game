using NUnit.Framework;

using Game.Models;
using Game.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace UnitTests.Models
{
    [TestFixture]
    class imagePickerModelTests
    {
        [Test]
        public void ImagePickerModel_Default_Is_Null_Should_Pass()
        {
            //Arrange

            //Act
            ImagePickerModel test = new ImagePickerModel();

            //Assert
            Assert.AreEqual(null, test.Url);
            
        }
    }
}
