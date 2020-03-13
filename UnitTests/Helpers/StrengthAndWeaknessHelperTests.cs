using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

using Game.Helpers;
using Game.Models;
using System.Linq;

namespace UnitTests.Helpers
{
    [TestFixture]
    class StrengthAndWeaknessHelperTests
    {
      
        [Test]
        public void SrengthAndWeaknessHelper_get_expected_strengths_bravery_should_pass()
        {
            
            //arrange
            //creating a brave character class
            BaseCharacter character = new BaseCharacter();
            character.Attribute = CharacterTypeEnum.Bravery;

            //getting expected strengths
            var monsterType = Enum.GetNames(typeof(MonsterTypeEnum)).ToList();
            var Expected = monsterType.Where(a =>
                       a.ToString() != MonsterTypeEnum.Paranoia.ToString() &&
                       a.ToString() != MonsterTypeEnum.Unknown.ToString() &&
                       a.ToString() != MonsterTypeEnum.Anger.ToString() &&
                       a.ToString() != MonsterTypeEnum.Insanity.ToString() &&
                       a.ToString() != MonsterTypeEnum.Depression.ToString()).ToList();

            //Act
            var result = StrengthWeaknessHelper.getCharacterStrengths(character.Attribute.ToString());

            //assert
            //should be the same
            Assert.AreEqual(Expected, result);
        }

        [Test]
        public void SrengthAndWeaknessHelper_get_expected_strengths_Creativity_should_pass()
        {
            //arrange
            //creating a Creative character class
            BaseCharacter character = new BaseCharacter();
            character.Attribute = CharacterTypeEnum.Creativity;

            var monsterType = Enum.GetNames(typeof(MonsterTypeEnum)).ToList();
            //getting expected strengths
            var Expected= monsterType.Where(a =>
             a.ToString() != MonsterTypeEnum.Anxiety.ToString() &&
             a.ToString() != MonsterTypeEnum.Unknown.ToString() &&
             a.ToString() != MonsterTypeEnum.Depression.ToString() &&
             a.ToString() != MonsterTypeEnum.Insanity.ToString() &&
             a.ToString() != MonsterTypeEnum.BurnOut.ToString()
                  ).ToList();

            //Act
            var result = StrengthWeaknessHelper.getCharacterStrengths(character.Attribute.ToString());

            //assert
            //should be the same
            Assert.AreEqual(Expected, result);

        }



    }
}
