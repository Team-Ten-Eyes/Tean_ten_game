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


        [Test]
        public void SrengthAndWeaknessHelper_get_expected_strengths_Cunning_should_pass()
        {
            //arrange
            //creating a Creative character class
            BaseCharacter character = new BaseCharacter();
            character.Attribute = CharacterTypeEnum.Cunning;

            var monsterType = Enum.GetNames(typeof(MonsterTypeEnum)).ToList();
            //getting expected strengths
            var expected = monsterType.Where(a =>
               a.ToString() != MonsterTypeEnum.Paranoia.ToString() &&
               a.ToString() != MonsterTypeEnum.Insanity.ToString() &&
               a.ToString() != MonsterTypeEnum.Unknown.ToString() &&
               a.ToString() != MonsterTypeEnum.Fear.ToString()).ToList();

            //Act
            var result = StrengthWeaknessHelper.getCharacterStrengths(character.Attribute.ToString());

            //assert
            //should be the same
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void SrengthAndWeaknessHelper_GetCharacterWeaknesses_get_Bravery_expected_weaknesses()
        {
            //arrange
            //creating a brave character class
            BaseCharacter character = new BaseCharacter();
            character.Attribute = CharacterTypeEnum.Bravery;

            //expected list
            var monsterType = Enum.GetNames(typeof(MonsterTypeEnum)).ToList();
            var expected = monsterType.Where(a =>
                      a.ToString() == MonsterTypeEnum.Paranoia.ToString() &&
                      a.ToString() == MonsterTypeEnum.Anger.ToString() &&
                      a.ToString() == MonsterTypeEnum.Depression.ToString()).ToList();

            //Act
            var result = StrengthWeaknessHelper.GetCharacterWeaknesses(character.Attribute.ToString());

            //assert
            //should be the same
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void SrengthAndWeaknessHelper_GetCharacterWeaknesses_get_Creativity_expected_weaknesses()
        {
            //arrange
            //creating a brave character class
            BaseCharacter character = new BaseCharacter();
            character.Attribute = CharacterTypeEnum.Creativity;

            //expected list
            var monsterType = Enum.GetNames(typeof(MonsterTypeEnum)).ToList();
            var expected = monsterType.Where(a =>
               a.ToString() == MonsterTypeEnum.Anxiety.ToString() &&
               a.ToString() == MonsterTypeEnum.Depression.ToString() &&
               a.ToString() == MonsterTypeEnum.BurnOut.ToString()).ToList();

            //Act
            var result = StrengthWeaknessHelper.GetCharacterWeaknesses(character.Attribute.ToString());

            //assert
            //should be the same
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void SrengthAndWeaknessHelper_GetCharacterWeaknesses_get_Cunning_expected_weaknesses()
        {
            //arrange
            //creating a brave character class
            BaseCharacter character = new BaseCharacter();
            character.Attribute = CharacterTypeEnum.Cunning;

            //expected list
            var monsterType = Enum.GetNames(typeof(MonsterTypeEnum)).ToList();
            var expected  = monsterType.Where(a =>
              a.ToString() == MonsterTypeEnum.Paranoia.ToString() &&
              a.ToString() == MonsterTypeEnum.Fear.ToString()).ToList();

            //Act
            var result = StrengthWeaknessHelper.GetCharacterWeaknesses(character.Attribute.ToString());

            //assert
            //should be the same
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void SrengthAndWeaknessHelper_getMonstersStrengths_all_expected_outcomes_should_pass()
        {
            //arrange
            //chreating all monster types
            BaseMonster monsterD = new BaseMonster();
            monsterD.Attribute = MonsterTypeEnum.Depression;
            BaseMonster stress = new BaseMonster();
            stress.Attribute = MonsterTypeEnum.Stress;
            BaseMonster anxiety = new BaseMonster();
            anxiety.Attribute = MonsterTypeEnum.Anxiety;
            BaseMonster Anger = new BaseMonster();
            Anger.Attribute = MonsterTypeEnum.Anger;
            BaseMonster fear = new BaseMonster();
            fear.Attribute = MonsterTypeEnum.Fear;
            BaseMonster burnOut = new BaseMonster();
            burnOut.Attribute = MonsterTypeEnum.BurnOut;
            BaseMonster paraniona = new BaseMonster();
            paraniona.Attribute = MonsterTypeEnum.Paranoia;

            //all the expected types
            var characterType = Enum.GetNames(typeof(CharacterTypeEnum)).ToList();

            var Dexpected = characterType.Where(a =>
            a.ToString() != CharacterTypeEnum.Unknown.ToString() &&
            a.ToString() != CharacterTypeEnum.Cunning.ToString()).ToList();

            var PStrengths = characterType.Where(a =>
               a.ToString() != CharacterTypeEnum.Creativity.ToString() &&
               a.ToString() != CharacterTypeEnum.Unknown.ToString()).ToList();

            var AngerStrengths = characterType.Where(a =>
                a.ToString() == CharacterTypeEnum.Bravery.ToString()).ToList();

            var fearStrenths = characterType.Where(a =>
               a.ToString() == CharacterTypeEnum.Cunning.ToString()).ToList();

            var AnxietyStrengths = characterType.Where(a =>
              a.ToString() == CharacterTypeEnum.Creativity.ToString()).ToList();

            var burnoutStrengths = characterType.Where(a =>
               a.ToString() == CharacterTypeEnum.Creativity.ToString()).ToList();

            List<string> nothing = new List<string>();
            nothing.Add("none");

            //act
            var Dresult = StrengthWeaknessHelper.getMonsterStrengths(monsterD.Attribute.ToString());
            var Presult = StrengthWeaknessHelper.getMonsterStrengths(paraniona.Attribute.ToString());
            var AngerResult = StrengthWeaknessHelper.getMonsterStrengths(Anger.Attribute.ToString());
            var Fresults = StrengthWeaknessHelper.getMonsterStrengths(fear.Attribute.ToString());
            var AnxietyResults = StrengthWeaknessHelper.getMonsterStrengths(anxiety.Attribute.ToString());
            var Bresults = StrengthWeaknessHelper.getMonsterStrengths(burnOut.Attribute.ToString());
            var Sresults = StrengthWeaknessHelper.getMonsterStrengths(stress.Attribute.ToString());

            //Assert
            Assert.AreEqual(Dexpected, Dresult);
            Assert.AreEqual(PStrengths, Presult);
            Assert.AreEqual(AngerStrengths, AngerResult);
            Assert.AreEqual(fearStrenths, Fresults);
            Assert.AreEqual(AnxietyStrengths, AnxietyResults);
            Assert.AreEqual(burnoutStrengths, Bresults);
            Assert.AreEqual(nothing, Sresults);


        }
        [Test]
        public void SrengthAndWeaknessHelper_characterStrongAgainst_expected_stregth_should_pass()
        {
            //arrange
            BaseCharacter character = new BaseCharacter();
            character.Attribute = CharacterTypeEnum.Bravery;

            BaseMonster monster = new BaseMonster();
            monster.Attribute = MonsterTypeEnum.Anxiety;

            //act
            var result = StrengthWeaknessHelper.characterStrongAgainst(character.Attribute.ToString(), monster.Attribute.ToString());

            //Assert
            Assert.AreEqual(true, result);

        }



    }



}
