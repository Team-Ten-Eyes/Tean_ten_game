using Game.Models;
using System.Collections.Generic;

namespace Game.Services
{
    public static class DefaultData
    {
        /// <summary>
        /// Load the Default data
        /// </summary>
        /// <returns></returns>
        public static List<ItemModel> LoadData(ItemModel temp)
        {
            var datalist = new List<ItemModel>()
            {
                new ItemModel {
                    Name = "Gold Sword",
                    Description = "Sword made of Gold, really expensive looking",
                    ImageURI = "http://www.clker.com/cliparts/e/L/A/m/I/c/sword-md.png",
                    Range = 0,
                    Damage = 9,
                    Value = 9,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Defense},

                new ItemModel {
                    Name = "Strong Shield",
                    Description = "Enough to hide behind",
                    ImageURI = "http://www.clipartbest.com/cliparts/4T9/LaR/4T9LaReTE.png",
                    Range = 0,
                    Damage = 0,
                    Value = 9,
                    Location = ItemLocationEnum.OffHand,
                    Attribute = AttributeEnum.Defense},

                new ItemModel {
                    Name = "Bunny Hat",
                    Description = "Pink hat with fluffy ears",
                    ImageURI = "http://www.clipartbest.com/cliparts/yik/e9k/yike9kMyT.png",
                    Range = 0,
                    Damage = 0,
                    Value = 9,
                    Location = ItemLocationEnum.Head,
                    Attribute = AttributeEnum.Speed},
            };

            return datalist;
        }
        
        public static List<ScoreModel> LoadData(ScoreModel temp)
        {
            var datalist = new List<ScoreModel>()
            {
                new ScoreModel {
                    Name = "First Score",
                    Description = "Test Data",
                },

                new ScoreModel {
                    Name = "Second Score",
                    Description = "Test Data",
                }
            };

            return datalist;
        }
        
        public static List<BaseCharacter> LoadData(BaseCharacter temp)
        {
            var datalist = new List<BaseCharacter>()
            {
                new BaseCharacter
                {
                    Name="Courage",
                    ImageURI = "thumbnail_bravery.png",
                    CharHealth=10,
                    MaxHealth=15,
                    Mana=7,
                    MaxMana=10,
                    level=10,
                    Experience=5,
                    Attribute=CharacterTypeEnum.Bravery,
                    Attack=20,
                    defense=10,
                    speed=3
                },
                  new BaseCharacter
                {
                   Name="sneaky",
                    ImageURI = "cunning.png",
                    level=7,
                    CharHealth=15,
                    MaxHealth=15,
                    Mana=5,
                    MaxMana=10,
                    Experience=5,
                    Attribute=CharacterTypeEnum.Cunning,
                    Attack=15,
                    defense=10,
                    speed=20
                },
                new BaseCharacter
                {
                    Name="Atristic",
                    ImageURI = "creativity.png",
                    level=12,
                    CharHealth=20,
                    MaxHealth=20,
                    Mana=10,
                    MaxMana=10,
                    Experience=5,
                    Attribute=CharacterTypeEnum.Creativity,
                    Attack=18,
                    defense=5,
                    speed=5
                }

            };
            return datalist;
        }

        public static List<BaseMonster> LoadData(BaseMonster temp)
        {
            var datalist = new List<BaseMonster>()
            {
                new BaseMonster {
                    Name="Stress",
                    ImageURI = "creativity.png",
                    level=12,
                    MonsterHealth=20,
                    MaxHealth=20,
                    Attribute=MonsterTypeEnum.Stress,
                    Attack=18,
                    defense=5,
                    speed=5
                },

                new BaseMonster {
                    Name="StresssedTest",
                    ImageURI = "creativity.png",
                    level=12,
                    MonsterHealth=20,
                    MaxHealth=20,
                    Attribute=MonsterTypeEnum.Stress,
                    Attack=18,
                    defense=5,
                    speed=5
                }
            };

            return datalist;
        }


    }
}