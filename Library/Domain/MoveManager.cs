using Library.Assets;
using Library.Content;
using Microsoft.Xna.Framework.Content;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Library.GameState.Battle;
using System;

namespace Library.Domain
{
    public static class MoveManager
    {
        public static Dictionary<MoveName, Move> Moves { get; set; }

        public static List<Type> PhysicalTypes = new List<Type>
        {
            Type.Normal,
            Type.Fighting,
            Type.Flying,
            Type.Ground,
            Type.Rock,
            Type.Bug,
            Type.Ghost,
            Type.Dragon,
            Type.Poison,
            Type.Steel
        };

        public static List<Type> SpecialTypes = new List<Type>
        {
            Type.Water,
            Type.Grass,
            Type.Fire,
            Type.Ice,
            Type.Electric,
            Type.Psychic,
            Type.Dragon,
            Type.Dark,
        };

        public static MoveResult GetMoveResult(Move move, Pokemon user, Pokemon target, GameState.Battle.Battle battle)
        {
            float damage;
            float userAttack = PhysicalTypes.Contains(move.Type) ? user.GetStat(Stat.Attack) : user.GetStat(Stat.SpecialAttack);
            float targetDefense = PhysicalTypes.Contains(move.Type) ? target.GetStat(Stat.Defense) : target.GetStat(Stat.SpecialDefense);
            float modifer = 1;

            if (battle.BattleStatusEffects.Contains(BattleStatusEffect.HarshSunlight) && move.Type == Type.Fire)
            {
                modifer *= 1.5f;
            }
            else
            if (battle.BattleStatusEffects.Contains(BattleStatusEffect.Rain) && move.Type == Type.Water)
            {
                modifer *= 1.5f;
            }

            damage = (((user.Level * 2f / 5f) + 2) * move.Power * userAttack / targetDefense / 50f * 2) + 2f;

            double randomValue = 0.85 + (1 - 0.85) * new Random().NextDouble();

            float typeBonus = 1f;

            foreach (Type type in Encyclopedia.GetSpeciesProperties(target.Species).Types)
            {
                if (TypeManager.SuperEffective[move.Type].Contains(type))
                {
                    typeBonus *= 2f;
                }
                else if (TypeManager.NotVeryEffective[move.Type].Contains(type))
                {
                    typeBonus /= 2f;
                }
                else if (TypeManager.NoEffect[move.Type].Contains(type))
                {
                    return new MoveResult { Damage = 0, MoveResultType = MoveResultType.No_Effect };
                }
            }

            MoveResult moveResult = new MoveResult();

            if (typeBonus < 0.9f)
            {
                moveResult.MoveResultType = MoveResultType.Not_Very_Effective;
            }
            else if (typeBonus < 1.1f)
            {
                moveResult.MoveResultType = MoveResultType.Normal;
            }
            else if (typeBonus > 1.1f)
            {
                moveResult.MoveResultType = MoveResultType.Super_Effective;
            }

            float sameTypeBonus = Encyclopedia.GetSpeciesProperties(user.Species).Types.Contains(move.Type) ? 1.5f : 1f;

            moveResult.Damage = (float)(damage * modifer * randomValue * sameTypeBonus * typeBonus);

            return moveResult;
        }

        public static void Load(ContentManager contentManager)
        {
            Moves = new Dictionary<MoveName, Move>();

            string filePath = contentManager.RootDirectory + FileHelper.EncyclopediaDirectory + FileHelper.MoveFileName + FileHelper.JsonExtension;

            if (File.Exists(filePath))
            {
                using (StreamReader r = new StreamReader(filePath))
                {
                    foreach (Move move in JsonConvert.DeserializeObject<List<Move>>(r.ReadToEnd(), FileHelper.JsonSerializerSettings))
                    {
                        Moves.Add(move.MoveName, move);
                    }
                }
            }
            else
            {
                Trace.TraceError("Moves file not found at " + filePath + ".");
            }
        }
    }

    public class MoveResult
    {
        public MoveResultType MoveResultType { get; set; }
        public float Damage { get; set; }
    }
}
