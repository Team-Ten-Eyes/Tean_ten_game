using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using NUnit.Framework;

using Game.Helpers;
using Game.Models;

namespace UnitTests.Helpers
{
    class MonsterTypeEnumHelperTests
    {

        [Test]
        public void MonsterTypeEnumHelper_MonsterTypeListAll_should_have_unkown_should_pass()
        {
            //arrange
            string unknown = "Unknown";
            bool hasUnknown = false;

            //act
            var result = MonsterTypeEnumHelper.GetListMonsterTypeAll;


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
        public void MonsterTypeEnumHelper_MonsterTypeList_should_not_have_unkown_should_pass()
        {
            //arrange
            string unknown = "Unknown";
            bool hasUnknown = false;

            //act
            var result = MonsterTypeEnumHelper.GetListMonsterType;

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
        public void MonsterTypeEnumHelper_ConvertStringToEnum_expected_value_should_pass()
        {
            // Arrange

            var myList = Enum.GetNames(typeof(MonsterTypeEnum)).ToList();

            MonsterTypeEnum myActual;
            MonsterTypeEnum myExpected;

            // Act

            foreach (var item in myList)
            {
                myActual = MonsterTypeEnumHelper.ConvertStringToEnum(item);
                myExpected = (MonsterTypeEnum)Enum.Parse(typeof(MonsterTypeEnum), item);

                // Assert
                Assert.AreEqual(myExpected, myActual, "string: " + item + TestContext.CurrentContext.Test.Name);
            }
        }
    }
}
