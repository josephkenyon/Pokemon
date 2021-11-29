using System;

namespace Library.Domain
{
    public class StatSet
    {
        public int HP { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int SpecialAttack { get; set; }
        public int SpecialDefense { get; set; }
        public int Speed { get; set; }
        public int Accuracy { get; set; }
        public int Evasiveness { get; set; }

        public StatSet()
        {
            foreach (Stat stat in Enum.GetValues(typeof(Stat)))
            {
                SetStat(stat, 0);
            }
        }

        public int GetStat(Stat stat)
        {
            switch (stat)
            {
                case Stat.HP:
                    return HP;
                case Stat.Attack:
                    return Attack;
                case Stat.Defense:
                    return Defense;
                case Stat.SpecialAttack:
                    return SpecialAttack;
                case Stat.SpecialDefense:
                    return SpecialDefense;
                case Stat.Speed:
                    return Speed;
                case Stat.Accuracy:
                    return Accuracy;
                case Stat.Evasiveness:
                    return Evasiveness;
                default:
                    return 0;
            }
        }

        void SetStat(Stat stat, int value)
        {
            switch (stat)
            {
                case Stat.HP:
                    HP = value;
                    break;
                case Stat.Attack:
                    Attack = value;
                    break;
                case Stat.Defense:
                    Defense = value;
                    break;
                case Stat.SpecialAttack:
                    SpecialAttack = value;
                    break;
                case Stat.SpecialDefense:
                    SpecialDefense = value;
                    break;
                case Stat.Speed:
                    Speed = value;
                    break;
                case Stat.Accuracy:
                    Accuracy = value;
                    break;
                case Stat.Evasiveness:
                    Evasiveness = value;
                    break;
            }
        }
    }
}
