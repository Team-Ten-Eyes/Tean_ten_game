using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.ViewModels
{
    [TestFixture]
    public class ItemIndexViewModelTests
    {
        [Test]
        public void ItemIndexViewModel_Read_Invalid_ID_Bogus_Should_Fail()
        {
            // TODO: Add your test code here
            var answer = 42;
            Assert.That(answer, Is.EqualTo(42), "Some useful error message");
        }
    }
}
