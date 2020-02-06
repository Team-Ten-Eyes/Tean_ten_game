using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Models;

namespace UnitTests.Models
{
    [TestFixture]
    public class ItemModelTests
    {
        [Test]
        public void ItemModel_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = new ItemModel();
            
            // Reset

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
