using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Models
{
    class PlayerModel : BaseModel<PlayerModel>
    {
        public uint Level { get; set; } = 1;
    }
}
