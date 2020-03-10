using System;
using System.Collections.Generic;
using System.Text;
using Game.Models;

namespace Game.Models
{
    public class PotionsModel : DefaultModel
    {
        //this is the potions type
        public PotionsEnum potionType {  get; set; } = PotionsEnum.Mana;

        //this it the amount of health or mana the potion would give to a character
        public uint Addition { get; set; } = 1;

        //potion image
        public string ImageURI { get; set; } = "Mana.png" ;



        /// <summary>
        /// Default potion
        /// 
        /// Gets a type, guid, name and description
        /// </summary>
        public PotionsModel()
        {
            Guid = Id;
            potionType = PotionsEnum.Health;
            Addition = 10;
            ImageURI = "Health.png";
            
        }

        /// <summary>
        /// Create a copy
        /// </summary>
        /// <param name="data"></param>
        public PotionsModel(PotionsModel data)
        {
            Update(data);
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="newData"></param>
        /// <returns></returns>
        public bool Update(PotionsModel newData)
        {
            if (newData == null)
            {
                return false;
            }

            potionType = newData.potionType;
            Addition = newData.Addition;
            Guid = newData.Guid;
            ImageURI = newData.ImageURI;


            return true;
        }

        /// <summary>
        /// Helper to show the Specific potion type
        /// </summary>
        /// <returns></returns>
        public PotionsEnum GetCharType()
        {
            return potionType;
        }

  


    }
}
