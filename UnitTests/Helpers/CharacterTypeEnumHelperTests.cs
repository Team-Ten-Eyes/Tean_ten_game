using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

using Game.Models;
using Game.Helpers;


namespace UnitTests.Helpers
{
    [TestFixture]
    class CharacterTypeEnumHelperTests
    {
        [Test]
        public void characterTypeListAll_should_have_unkown_should_pass()
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
        public void charterTypeList_should_not_have_unkown_should_pass()
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
    }
}
