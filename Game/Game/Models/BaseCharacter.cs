 using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Models
{
    class BaseCharacter
    {
        public BaseCharacter()
        { }    
        public CharacterTypeEnum Attribute { get; set; } = CharacterTypeEnum.Bravery;
        public string Id { get; set; } = Guid.NewGuid().ToString();

        // The Name of the Item 
        public string Name { get; set; } = "This is an Item";

        public string ImageURI { get; set; }
        // The Descirption of the Item
        public string Description { get; set; } = "Character Description";

        public bool Alive { get; set; } = true;
          

        public bool update(BaseCharacter charData)
        {
         Name = charData.Name;
         return true;
         }
    }
}
