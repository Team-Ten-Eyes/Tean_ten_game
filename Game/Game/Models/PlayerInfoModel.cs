using System.Collections.Generic;

using Game.Helpers;

namespace Game.Models
{
    /// <summary>
    /// Player for the game.
    /// 
    /// Either Monster or Character
    /// 
    /// Constructor Player to Player used a T in Round
    /// </summary>
    public class PlayerInfoModel : PlayerModel<PlayerInfoModel>
    {
        public bool SelectedForBattle { get; set; }
        /// <summary>
        /// Default Constructor
        /// </summary>
        public PlayerInfoModel() { }

        /// <summary>
        /// Copy from one PlayerInfoModel into Another
        /// </summary>
        /// <param name="data"></param>
        public PlayerInfoModel(PlayerInfoModel data)
        {
            Speed = data.Speed;
            Attack = data.Attack;
            Defense = data.Defense;

            SelectedForBattle = data.SelectedForBattle;
            PlayerType = data.PlayerType;
            Guid = data.Guid;
            Alive = data.Alive;
            Experience = data.Experience;
            ExperienceRemaining = data.ExperienceRemaining;
            Level = data.Level;
            Name = data.Name;
            Description = data.Description;
            Speed = data.GetSpeed();
            ImageURI = data.ImageURI;
            MaxHealth = data.GetMaxHealthTotal;
            CurrHealth = data.GetMaxHealthTotal;

            // Set the strings for the items
            Head = data.Head;
            Feet = data.Feet;
            Necklass = data.Necklass;
            RightFinger = data.RightFinger;
            LeftFinger = data.LeftFinger;
            Feet = data.Feet;
            UniqueItem = data.UniqueItem;

            Difficulty = data.Difficulty;
            
        }

        /// <summary>
        /// Create PlayerInfoModel from character
        /// </summary>
        /// <param name="data"></param>
        public PlayerInfoModel(BaseCharacter data)
        {
            SelectedForBattle = false;
            PlayerType = data.PlayerType;
            Guid = data.Guid;
            Alive = data.Alive;
            Experience = data.Experience;
            ExperienceRemaining = data.ExperienceRemaining;
            Level = data.Level;
            Name = data.Name;
            Description = data.Description;
            Speed = data.GetSpeed();
            Attack = data.GetAttack();
            Defense = data.GetDefense();
            
            ImageURI = data.ImageURI;
            MaxHealth = data.GetMaxHealthTotal;
            CurrHealth = data.GetMaxHealthTotal;

            // Set the strings for the items
            Head = data.Head;
            Feet = data.Feet;
            Necklass = data.Necklass;
            RightFinger = data.RightFinger;
            LeftFinger = data.LeftFinger;
            Feet = data.Feet;

            UniqueItem = data.UniqueItem;

            Difficulty = data.Difficulty;

            // Give the copy a differet quid, so it can be used in the battles as a copy
            Guid = System.Guid.NewGuid().ToString();

            // Set current experience to be 1 above minimum.
            Experience = LevelTableHelper.Instance.LevelDetailsList[Level - 1].Experience + 1;
        }

        /// <summary>
        /// Crate PlayerInfoModel from Monster
        /// </summary>
        /// <param name="data"></param>
        public PlayerInfoModel(BaseMonster data)
        {
            SelectedForBattle = false;
            PlayerType = data.PlayerType;
            Guid = data.Guid;
            Alive = data.Alive;
            Experience = data.Experience;
            ExperienceRemaining = data.ExperienceRemaining;
            Level = data.Level;
            Name = data.Name;
            Description = data.Description;
            Speed = data.GetSpeed();
            ImageURI = data.ImageURI;
            MaxHealth = data.GetMaxHealthTotal;
            CurrHealth = data.GetCurrentHealthTotal;

            // Set the strings for the items
            Head = data.Head;
            Feet = data.Feet;
            Necklass = data.Necklass;
            RightFinger = data.RightFinger;
            LeftFinger = data.LeftFinger;
            Feet = data.Feet;

            UniqueItem = data.UniqueItem;

            Difficulty = data.Difficulty;

            // Give the copy a differet quid, so it can be used in the battles as a copy
            Guid = System.Guid.NewGuid().ToString();

            // Set amount to give to be 1 below max for that level.
            ExperienceRemaining = LevelTableHelper.Instance.LevelDetailsList[Level + 1].Experience - 1;
        }

        

        

        public bool AttributesPrime()
        {
            int checkIfPrime = 0;
            checkIfPrime += Speed;
            checkIfPrime += Attack;
            checkIfPrime += Defense;
            checkIfPrime += MaxHealth;
            checkIfPrime += Level;

            List<int> primes = GeneratePrimesNaive(checkIfPrime);
            foreach(int x in primes)
            {
                if ((double)x / (double)checkIfPrime == 1)
                    return true;
            }
            return false;
        }
        public static List<int> GeneratePrimesNaive(int n)
        {
            List<int> primes = new List<int>();
            primes.Add(2);
            int nextPrime = 3;
            while (primes[primes.Count - 1] < n)
            {
                int sqrt = (int)System.Math.Sqrt(nextPrime);
                bool isPrime = true;
                for (int i = 0; (int)primes[i] <= sqrt; i++)
                {
                    if (nextPrime % primes[i] == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }
                if (isPrime)
                {
                    primes.Add(nextPrime);
                }
                nextPrime += 2;
            }
            return primes;
        }
        public override string FormatOutput()
        {
            var myReturn = string.Empty;
            myReturn += Name;
            myReturn += " , " + Description;
            myReturn += " , Level : " + Level.ToString();

            if (PlayerType == PlayerTypeEnum.Character)
            {
                myReturn += " , Total Experience : " + Experience;
                myReturn += " , Damage : " + GetDamageTotalString;
                myReturn += " , Attack :" + GetAttackTotal;
                myReturn += " , Defense :" + GetDefenseTotal;
                myReturn += " , Speed :" + GetSpeedTotal;
            }

            myReturn += " , Items : " + ItemSlotsFormatOutput();

            return myReturn;
        }
    }
}