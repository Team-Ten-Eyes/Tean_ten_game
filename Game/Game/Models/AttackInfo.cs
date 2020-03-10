using System;
using System.Collections.Generic;
using System.Text;
using Game.ViewModels;
namespace Game.Models
{
    public class AttackInfo
    {

        public GenericViewModel<PlayerInfoModel> target { get; set; }
        public GenericViewModel<SpecialAttack> attack { get; set; } = null;
        public AttackInfo(GenericViewModel<PlayerInfoModel> target, GenericViewModel<SpecialAttack> attack = null)
        {
            this.target = target;
            this.attack = attack;
        }

    }
}
