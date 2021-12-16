using System;
using System.Collections.Generic;
using System.Linq;
using Library.Domain;
using Library.GameState.Battle;
using Newtonsoft.Json;

namespace Library.Assets
{

    public class Pokemon
    {
        [JsonProperty]
        public Species Species { get; private set; }

        [JsonProperty]
        public int Level { get; protected set; }

        [JsonProperty]
        public float CurrentHealth { get; protected set; }

        [JsonProperty]
        public int Experience { get; protected set; }

        public List<Move> Moves { get; set; }

        [JsonProperty]
        public StatSet Effort { get; private set; }

        public bool IsFainted => CurrentHealth <= 0;

        public int GetStat(Stat stat)
            => Encyclopedia.GetSpeciesProperties(Species).StatSet.GetStat(stat)
                + ((int)(Encyclopedia.GetSpeciesProperties(Species).StatSet.GetStat(stat) / 35f * Level)
                + (int)(Effort.GetStat(stat) / 50f)) * Level;

        public int ExperienceToLevelUp
            => (int)(Math.Pow(Level, 3) - (15 * Math.Pow(Level, 2)) + (100f * Level) - 140);

        public int GetExperienceReward()
        {
            int a = 1;  // Is this wild or trainer, 1 or 1.5
            return (a * Encyclopedia.GetSpeciesProperties(Species).BaseExperienceReward * Level) / 7;
        }

        public void UpdateAfterBattle(BattlePokemon battlePokemon)
        {
            Level = battlePokemon.Level;
            CurrentHealth = battlePokemon.CurrentHealth;
            Experience = battlePokemon.Experience;
            Moves = battlePokemon.Moves;
            Effort = battlePokemon.Effort;
        }

        public Pokemon() { }
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

            foreach (MoveLearn moveToLearn in learnableMoves)
            {
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

        public void GainExperience(int experience)
        {
            Experience += experience;
            if (Experience > ExperienceToLevelUp)
            {
                LevelUp();
                Experience -= ExperienceToLevelUp;
            }
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