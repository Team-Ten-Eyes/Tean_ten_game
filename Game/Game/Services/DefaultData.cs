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
                    Name = "Sword Of Truth",
                    Description = "Sword made of Gold, really expensive looking",
                    ImageURI = "http://www.clker.com/cliparts/e/L/A/m/I/c/sword-md.png",
                    Range = 0,
                    Damage = 5,
                    Value = 6,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Attack},

                new ItemModel {
                    Name = "Tinfoil Hat",
                    Description = "It'll stop the government rays",
                    ImageURI = "http://www.clipartbest.com/cliparts/4T9/LaR/4T9LaReTE.png",
                    Range = 0,
                    Damage = 2,
                    Value = 9,
                    Location = ItemLocationEnum.Head,
                    Attribute = AttributeEnum.Defense},

                new ItemModel {
                    Name = "Starbucks Coffee",
                    Description = "Good bean juice makes my brain fast",
                    ImageURI = "http://www.clipartbest.com/cliparts/yik/e9k/yike9kMyT.png",
                    Range = 0,
                    Damage = 0,
                    Value = 9,
                    Location = ItemLocationEnum.OffHand,
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
                    Level=10,
                    Experience=5,
                    Attribute=CharacterTypeEnum.Bravery,
                    Attack=20,
                    Defense=10,
                    Speed=3
                },
                  new BaseCharacter
                {
                   Name="sneaky",
                    ImageURI = "cunning.png",
                    Level=7,
                    CharHealth=15,
                    MaxHealth=15,
                    Mana=5,
                    MaxMana=10,
                    Experience=5,
                    Attribute=CharacterTypeEnum.Cunning,
                    Attack=15,
                    Defense=10,
                    Speed=20
                },
                new BaseCharacter
                {
                    Name="Atristic",
                    ImageURI = "creativity.png",
                    Level=12,
                    CharHealth=20,
                    MaxHealth=20,
                    Mana=10,
                    MaxMana=10,
                    Experience=5,
                    Attribute=CharacterTypeEnum.Creativity,
                    Attack=18,
                    Defense=5,
                    Speed=5
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
                    ImageURI = "Fire_monster.png",
                    Level=12,
                    MonsterHealth=20,
                    MaxHealth=20,
                    Attribute=MonsterTypeEnum.Stress,
                    Attack=18,
                    Defense=5,
                    Speed=5
                },

                new BaseMonster {
                    Name="StresssedTest",
                    ImageURI = "Shadow_Monster.png",
                    Level=12,
                    MonsterHealth=20,
                    MaxHealth=20,
                    Attribute=MonsterTypeEnum.Stress,
                    Attack=18,
                    Defense=5,
                    Speed=5
                }
            };

            return datalist;
        }


    }
}