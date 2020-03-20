using System;
using System.Collections.Generic;
using System.Text;
using Game.ViewModels;
namespace Game.Models
{
    public class AttackInfo
    {

        public GenericViewModel<PlayerInfoModel> target { get; set; }
  
        public AttackInfo(GenericViewModel<PlayerInfoModel> target)
        {
            this.target = target;
          
        }

    }
}
