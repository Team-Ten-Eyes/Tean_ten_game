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
        /*
        public static List<BaseCharacter> LoadData(BaseCharacter temp)
        {
            var datalist = new List<BaseCharacter>()
            {
                new BaseCharacter
                {
                    Name="Courage",
                    ImageURI = "knight.png",
                    Alive=true,
                    Level=10,
                    Experience=5,
                    Speed=3,
                    Attack=10,
                    Defense=15,
                    MaxHealth=30,
                    CurrentHealth=30
                },
                  new BaseCharacter
                {
                    Name="sneaky",
                    ImageURI = "knight.png",
                    Alive=true,
                    Level=10,
                    Experience=5,
                    Speed=3,
                    Attack=10,
                    Defense=15,
                    MaxHealth=30,
                    CurrentHealth=30
                }

            };
            return datalist;
        }
        */

    }
}