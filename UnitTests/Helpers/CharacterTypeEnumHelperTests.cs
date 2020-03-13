using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.Linq;

using Game.Models;
using Game.Helpers;


namespace UnitTests.Helpers
{
    [TestFixture]
    class CharacterTypeEnumHelperTests
    {
        [Test]
        public void CharacterTypeEnumHelper_characterTypeListAll_should_have_unkown_should_pass()
        {
            //arrange
            string unknown = "Unknown";
            bool hasUnknown = false;

            //act
            var result = CharacterTypeEnumHelper.GetListCharacterTypeALL;

            foreach (string type in result)
            {
                if (type == unknown)
                {
                    hasUnknown = true;
                    break;
                }
            }
            //Assert
            Assert.AreEqual(true, hasUnknown);
        }

        [Test]
        public void CharacterTypeEnumHelper_charterTypeList_should_not_have_unkown_should_pass()
        {
            //arrange
            string unknown = "Unknown";
            bool hasUnknown = false;

            //act
            var result = CharacterTypeEnumHelper.GetListCharacterType;

            foreach (string type in result)
            {
                if (type == unknown)
                {
                    hasUnknown = true;
                    break;
                }
            }
            //Assert
            Assert.AreEqual(false, hasUnknown);
        }

        [Test]
        public void CharacterTypeEnumHelper_ConvertStringToEnum_expected_value_should_pass()
        {
            // Arrange

            var myList = Enum.GetNames(typeof(CharacterTypeEnum)).ToList();

            CharacterTypeEnum myActual;
            CharacterTypeEnum myExpected;

            // Act

            foreach (var item in myList)
            {
                myActual = CharacterTypeEnumHelper.ConvertStringToEnum(item);
                myExpected = (CharacterTypeEnum)Enum.Parse(typeof(CharacterTypeEnum), item);

                // Assert
                Assert.AreEqual(myExpected, myActual, "string: " + item + TestContext.CurrentContext.Test.Name);
            }
        }
    }
}
