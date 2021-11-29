using Library.Assets;
using Library.Domain;
using System.Collections.Generic;

namespace Library.GameState.Battle
{
    public class BattlePokemon : Pokemon
    {
        public Vector Position { get; set; }
        public Direction Direction { get; set; }
        public List<StatusEffect> StatusEffects { get; }
        public StatSet StatChanges { get; set; }
        public bool UsedMove { get; set; }

        public BattlePokemon(Direction direction, Pokemon pokemon, int index) : base (pokemon)
        {
            StatusEffects = new List<StatusEffect>();
            Direction = direction;
            Position = BattleStateManager.GetBattlePositions(direction, index);
        }

        public void Damage(float amount)
        {
            if (CurrentHealth - amount < 0)
                CurrentHealth = 0;
            else
                CurrentHealth -= amount;
        }

        public void Heal(int amount)
        {

            if (CurrentHealth + amount > Encyclopedia.GetSpeciesProperties(Species).StatSet.GetStat(Stat.HP))
                CurrentHealth = Encyclopedia.GetSpeciesProperties(Species).StatSet.GetStat(Stat.HP);
            else
                CurrentHealth += amount;
        }

        public void UseMove() {
            UsedMove = true;
        }

        public override void FullHeal()
        {
            StatusEffects.Clear();
            CurrentHealth = GetStat(Stat.HP);
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
    }
}
