using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Models
{
    public enum RoundEnum
    {
        // Invalid State
        Unknown = 0,

        // Next Turn, monsters and Characters still alive
        NextTurn = 1,

        // New Round, Monsters dead
        NewRound = 2,

        // Game Over, characters dead
        GameOver = 3,
    }
}
