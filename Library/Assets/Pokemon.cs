using System;
using System.Collections.Generic;
using System.Linq;
using Library.Domain;

namespace Library.Assets
{

    public class Pokemon
    {
        public Species Species { get; }
        public int Level { get; protected set; }
        public float CurrentHealth { get; protected set; }
        public int Experience { get; protected set; }
        public List<Move> Moves { get; set;  }
        public StatSet Effort { get; }

        public bool IsFainted => CurrentHealth <= 0;

        public int GetStat(Stat stat)
            => Encyclopedia.GetSpeciesProperties(Species).StatSet.GetStat(stat)
                + ((int)(Encyclopedia.GetSpeciesProperties(Species).StatSet.GetStat(stat) / 25f)
                + (int)(Effort.GetStat(stat) / 50f)) * Level;

        public int ExperienceToLevelUp
            => 15 + (int)(Math.Pow(Level, 3) * (100f - Level) / 50f);

        public Pokemon(Species species, int level)
        {
            Species = species;
            Level = level;

            Moves = new List<Move>();
            Effort = new StatSet();

            Experience = 0;

            CurrentHealth = GetStat(Stat.HP);

            SpeciesProperties speciesProperties = Encyclopedia.GetSpeciesProperties(Species);

            List<MoveLearn> learnableMoves = new List<MoveLearn>();

            if (speciesProperties.Moves == null) return;

            IEnumerable<MoveLearn> movesToLearn = speciesProperties.Moves.Where(move => move.Level <= level);

            foreach (MoveLearn moveToLearn in movesToLearn)
            {
                if (learnableMoves.Count < 4)
                {
                    learnableMoves.Add(moveToLearn);
                }
                else
                {
                    if (learnableMoves.Exists(move => move.Rank > moveToLearn.Rank))
                    {
                        learnableMoves = learnableMoves.OrderByDescending(move => move.Rank).ToList();
                        learnableMoves.RemoveAt(0);
                        learnableMoves.Add(moveToLearn);
                    }
                }
            }

            foreach (MoveLearn moveToLearn in learnableMoves) {
                Moves.Add(MoveManager.Moves[moveToLearn.MoveName].GetCopy());
            }

        }

        public Pokemon(Pokemon pokemon)
        {
            Species = pokemon.Species;
            Level = pokemon.Level;
            CurrentHealth = pokemon.CurrentHealth;
            Experience = pokemon.Experience;
            Moves = pokemon.Moves;
            Effort = pokemon.Effort;
        }

        public void LevelUp()
        {
            int health = GetStat(Stat.HP);

            Level += 1;
            CurrentHealth += GetStat(Stat.HP) - health;
        }

        public virtual void FullHeal()
        {
            CurrentHealth = GetStat(Stat.HP);
        }
    }
}
