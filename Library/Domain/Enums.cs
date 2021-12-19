using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Domain
{
    public enum StatusEffect
    {
        Paralyzed,
        Poisoned,
        Burned,
        Frozen,
        Confused,
    }

    public enum CharacterName
    {
        Ash,
        Oak,
        Green
    }

    public enum LocationDoodad
    {
        Grass,
        Red_Flower,
        Water,
    }

    public enum SpecialActionKey
    {
        Give_Rival_Pokemon,
        Battle_Rival,
        Put_Missing_Pokemon_Back,
        Heal_All_Pokemon,
    }

    public enum CutsceneTriggerType
    {
        Movement,
        Selection,
        Flag
    }

    public enum CutsceneTransactionType
    {
        Message,
        Movement,
        Teleport,
        Flag,
        Special_Action,
    }

    public enum LocationName
    {
        PalletTown,
        AshHomeUpStairs,
        AshHomeDownStairs,
        OakPokemonResearchLab,
        Route1,
        //ViridianCity,
    }

    public enum Stat
    {
        HP,
        Attack,
        Defense,
        SpecialAttack,
        SpecialDefense,
        Speed,
        Accuracy,
        Evasiveness,
        All
    }

    public enum PokedexStatus { 
        Unknown,
        Seen,
        Caught,
    }

    public enum BagStateState
    {
        Items,
        Key_Items,
        Poke_Balls,
    }

    public enum UIState
    {
        Base,
        Menu,
        Battle,
        Pokemon,
        Pokedex,
        Bag,
    }

    public enum BaseState
    {
        Base,
        Message,
        Transition,
        Cutscene,
    }
    public enum MenuItem
    {
        Pokemon,
        Bag,
        Ash,
        Save,
        Load,
        Option,
        Return,
        ExitGame
    }

    public enum ItemType
    {
        Potion,
        Super_Potion,
        Hyper_Potion,
        Max_Potion,
    }

    public enum KeyItemType { 
    }

    public enum PokeBallType
    {
        Poke_Ball,
        Great_Ball,
        Lure_Ball,
        Ultra_Ball,
        Master_Ball
    }

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    public enum Orientation
    {
        Vertical,
        Horizontal,
    }

    public enum TextureName
    {
        Background,
        Foreground,
        Grass,
        Animation,
        Ash,
        BattleAsh,
        Policeman,
        Effects,
        BattleWallpaper,
        HealthExpBar,
        EmptyWhiteTexture,
        NPCTileset,
        Oak,
        Green,
        BagWallpaper,
        PokemonWallpaper,
    }

    public enum Badge {
        Boulder,
        Cascade,
        Thunder,
        Grass,
        Poison,
        Psychic,
        Fire,
        Ground,
    }

    public enum BattleState
    {
        AshSelect,
        PokemonSelect,
        MoveSelect,
        ItemSelect,
        EnemySelect,
        EnemyAttack,
        Wait
    }

    public enum BattleStatusEffect
    {
        Rain,
        HarshSunlight,
    }

    public enum BattleMenuItem
    {
        Fight,
        Bag,
        Run
    }

    public enum Type
    {
        Normal,
        Fire,
        Water,
        Grass,
        Electric,
        Ice,
        Fighting,
        Poison,
        Ground,
        Flying,
        Psychic,
        Bug,
        Rock,
        Ghost,
        Dragon,
        Dark,
        Steel,
        Fairy
    }
    public enum MoveResultType
    {
        Normal,
        Super_Effective,
        Not_Very_Effective,
        No_Effect,
        Missed
    }

    public enum Flag
    {
        Select_Pokemon,
        Selected_Pokemon,
        Pokemon_Choice,
    }

    public enum MoveType
    {
        ResetStat,
        StatBuff,
        StatDebuff,
        Burn,
        Confuse,
        Seed,
        Poison,
        Sleep,
        Recoil,
        Heal,
        Insomnia,
        Charge,
        Flinch,
        Protect,
        StatusProtect,
        Rain,
        Paralyze,
        MultiAttack,
        CritBuff,
        Double,
        Quick,
        HPNormalizer,
        Mirror,
        PercentDamage,
        IgnoreAccuracy,
        Unique
    }

    public enum HM
    {
       Cut,
       Fly,
       Surf,
       Strength,
       Flash,
       Rock_Smash,
       Waterfall,
       Dive
    }

    public enum MoveName
    {
        Tackle,
        Growl,
        Growth,
        Scratch,
        Leech_Seed,
        Ember,
        Bubble,
        Vine_Whip,
        Razor_Leaf,
        Poison_Powder,
        Sleep_Powder,
        Seed_Bomb,
        Take_Down,
        Sweet_Scent,
        Synthesis,
        Worry_Seed,
        Double_Edge,
        Solar_Beam,
        Petal_Blizzard,
        Petal_Dance,
        Metal_Claw,
        Smokescreen,
        Scary_Face,
        Flamethrower,
        Slash,
        Dragon_Rage,
        Fire_Spin,
        Wing_Attack,
        Rage,
        Tail_Whip,
        Withdraw,
        Water_Gun,
        Bite,
        Rapid_Spin,
        Protect,
        Rain_Dance,
        Skull_Bash,
        Hydro_Pump,
        String_Shot,
        Bug_Bite,
        Harden,
        Confusion,
        Stun_Spore,
        Supersonic,
        Whirlwind,
        Gust,
        Psybeam,
        Safeguard,
        Silver_Wind,
        Poison_Sting,
        Fury_Attack,
        Focus_Energy,
        Twineedle,
        Pursuit,
        Pin_Missile,
        Agility,
        Endeavor,
        Sand_Attack,
        Quick_Attack,
        Feather_Dance,
        Mirror_Move,
        Hyper_Fang,
        Super_Fang,
        Peck,
        Leer,
        Aerial_Ace,
        Drill_Peck,
        Wrap,
        Glare,
        Screech,
        Acid,
        Spit_Up,
        Stockpile,
        Swallow,
        Haze,
        Thunder_Shock,
        Thunder_Wave,
        Double_Team,
        Slam,
        Thunderbolt,
        Thunder,
        Light_Screen
    }

    public enum Species
    {
        Bulbasaur,
        Ivysaur,
        Venusaur,
        Charmander,
        Charmeleon,
        Charizard,
        Squirtle,
        Warturtle,
        Blastoise,
        Caterpie,
        Metapod,
        Butterfree,
        Weedle,
        Kakuna,
        Beedrill,
        Pidgey,
        Pidgeotto,
        Pidgeot,
        Rattata,
        Raticate,
        Spearow,
        Fearow,
        Ekans,
        Arbok,
        Pikachu
    }
}
