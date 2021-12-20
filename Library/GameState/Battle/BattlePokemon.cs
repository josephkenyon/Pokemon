using Library.Assets;
using Library.Domain;
using System;
using System.Collections.Generic;

namespace Library.GameState.Battle
{
    public class BattlePokemon : Pokemon
    {
        public Vector Position { get; set; }
        public Direction Direction { get; set; }
        public List<StatusEffect> StatusEffects { get; }
        public Dictionary<Stat, int> StatInfluences { get; }
        public StatSet StatChanges { get; set; }
        public bool UsedMove { get; set; }
        public Pokemon OriginalPokemon { get; private set; }

        public BattlePokemon(Direction direction, Pokemon pokemon, int index) : base(pokemon)
        {
            StatusEffects = new List<StatusEffect>();
            Direction = direction;
            Position = BattleStateManager.GetBattlePositions(direction, index);
            StatInfluences = new Dictionary<Stat, int>();
            OriginalPokemon = pokemon;
            foreach (Stat stat in Enum.GetValues(typeof(Stat)))
            {
                StatInfluences.Add(stat, 0);
            }
        }

        public void Damage(float amount)
        {
            if (CurrentHealth - amount < 0)
                CurrentHealth = 0;
            else
                CurrentHealth -= amount;
        }

        public void Heal(float amount)
        {

            if (CurrentHealth + amount > GetStat(Stat.HP))
                CurrentHealth = GetStat(Stat.HP);
            else
                CurrentHealth += amount;
        }

        public void UseMove()
        {
            UsedMove = true;
        }

        public override void FullHeal()
        {
            StatusEffects.Clear();
            CurrentHealth = GetStat(Stat.HP);
        }

        public void ApplyStatDebuff(Move move)
        {
            Stat? stat = move.StatInteraction;
            if (stat != null)
            {
                StatInfluences[(Stat)stat] -= move.SecondaryPower;
                if (StatInfluences[(Stat)stat] < -6)
                {
                    StatInfluences[(Stat)stat] = -6;
                }
            }
        }

        public void AddStatusEffect(StatusEffect statusEffect)
        {
            if (!StatusEffects.Contains(statusEffect))
                StatusEffects.Add(statusEffect);
        }

        public void RemoveStatusEffect(StatusEffect statusEffect)
        {
            if (StatusEffects.Contains(statusEffect))
                StatusEffects.Remove(statusEffect);
        }


        public override int GetStat(Stat stat)
        {
            float scaler = 1f;

            int statInfluence = StatInfluences[stat];

            if (statInfluence > 0)
            {
                scaler = 1f + 0.5f * statInfluence;
            }
            else if (statInfluence < 0)
            {
                switch (statInfluence)
                {
                    case -1:
                        scaler = 0.66f;
                        break;
                    case -2:
                        scaler = 0.5f;
                        break;
                    case -3:
                        scaler = 0.4f;
                        break;
                    case -4:
                        scaler = 0.33f;
                        break;
                    case -5:
                        scaler = 0.28f;
                        break;
                    case -6:
                        scaler = 0.25f;
                        break;
                }
            }

            return (int)(base.GetStat(stat) * scaler);
        }
    }
}
