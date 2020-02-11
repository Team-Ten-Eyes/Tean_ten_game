using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Models
{
    class Level
    {
        public Level()
        {
            LevelThreshold.Add(0);
            LevelThreshold.Add(300);
            LevelThreshold.Add(900);
            LevelThreshold.Add(2700);
            LevelThreshold.Add(6500);
            LevelThreshold.Add(14000);
            LevelThreshold.Add(23000);
            LevelThreshold.Add(34000);
            LevelThreshold.Add(48000);
            LevelThreshold.Add(64000);
            LevelThreshold.Add(85000);
            LevelThreshold.Add(100000);
            LevelThreshold.Add(120000);
            LevelThreshold.Add(140000);
            LevelThreshold.Add(165000);
            LevelThreshold.Add(195000);
            LevelThreshold.Add(225000);
            LevelThreshold.Add(265000);
            LevelThreshold.Add(305000);
            LevelThreshold.Add(355000);
        }
        public List<int> LevelThreshold { get; set; }

    }
}
