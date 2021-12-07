using Library.Content;
using Library.Domain;
using Library.GameState.Battle.BattleDrawingObjects;
using Library.GameState.Battle.GamePadHelpers;
using Library.GameState.Menu;
using Library.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Library.GameState.Battle
{
    public static class BattleGraphicsHelper
    {
        public readonly static float Scale = 0.7f;

        private static Vector BattleWallpaperTextureSize => new Vector(384, 216);
        public static Rectangle BattleWallpaperSourceRectangle => new Rectangle(Vector.Zero.ToPoint(), BattleWallpaperTextureSize.ToPoint());
        public static Rectangle BattleWallpaperTargetRectangle => new Rectangle(Vector.Zero.ToPoint(), new Vector(Constants.ResolutionWidth / Constants.Scaler, Constants.ResolutionHeight / Constants.Scaler).ToPoint());

        public static Vector2 HealthBarLocation => new Vector2(16f, 2f);
        public static Vector2 ExperienceBarLocation => new Vector2(16f, 6.25f);

        public static Vector BattleMenuLocation => new Vector(1.25f, 11f) * Constants.ScaledTileSize;
        private static Vector MenuTexturePosition => new Vector(0, 10);
        private static Vector MenuTextureSize => new Vector(12, 2);
        public static Rectangle MenuSourceRectangle => new Rectangle((MenuTexturePosition * Constants.TileSize).ToPoint(), (MenuTextureSize * Constants.TileSize).ToPoint());

        private static Vector BattleMessageTexturePosition => new Vector(0, 12);
        public static Vector BattleMessageTextureSize => new Vector(12, 3);
        public static Rectangle BattleMessageSourceRectangle => new Rectangle((BattleMessageTexturePosition * Constants.TileSize).ToPoint(), (BattleMessageTextureSize * Constants.TileSize).ToPoint());


        private static Vector MenuPointerStartingPosition => new Vector(-7, 19);

        private static readonly int[] PointerIncrements = new int[] { 36, 46, 27 };

        private static int GetTotalIncrement(int index)
        {
            float returnValue = 0;

            for (int i = 0; i < index; i++)
            {
                returnValue += PointerIncrements[i] * Constants.Scaler;
            }

            return (int)returnValue;
        }

        public static Rectangle PointerTargetRectangle => new Rectangle(new Point(BattleMenuLocation.X + MenuPointerStartingPosition.X + GetTotalIncrement(AshSelectHelper.SelectedIndex), BattleMenuLocation.Y + MenuPointerStartingPosition.Y), (MenuGraphicsHelper.PointerTextureSize * Constants.ScaledTileSize).ToPoint());


        public static PokemonDrawingObject GetPokemonDrawingObject(BattlePokemon pokemon, int index)
        {
            return new PokemonDrawingObject
            {
                Index = index,
                SpeciesName = pokemon.Species,
                Position = pokemon.Position.ToVector2(),
                Direction = pokemon.Direction,
                Pokemon = pokemon,
            };
        }

        public static PokemonShadowDrawingObject GetShadowDrawingObject(PokemonDrawingObject battlePokemonDrawingObject)
        {
            return new PokemonShadowDrawingObject
            {
                SpeciesName = battlePokemonDrawingObject.SpeciesName,
                Position = battlePokemonDrawingObject.Position,
                Direction = battlePokemonDrawingObject.Direction
            };
        }

        public static HealthBarShell GetHealthBarShell(PokemonDrawingObject battlePokemonDrawingObject)
        {
            return new HealthBarShell
            {
                PokemonDrawingObject = battlePokemonDrawingObject,
                SpeciesName = battlePokemonDrawingObject.SpeciesName,
                Direction = battlePokemonDrawingObject.Direction
            };
        }

        public static HealthBarDrawingObject GetHealthBar(HealthBarShell healthBarShell)
        {
            return new HealthBarDrawingObject
            {
                HealthBarShell = healthBarShell,
                Position = healthBarShell.GetPosition(),
            };
        }

        public static ExpBarDrawingObject GetExpBar(HealthBarShell healthBarShell)
        {
            return new ExpBarDrawingObject
            {
                HealthBarShell = healthBarShell,
                Position = healthBarShell.GetPosition()
            };
        }
    }

    public abstract class BattleDrawingObject : IDrawingObject
    {
        public Species SpeciesName { get; set; }
        public Vector2 Position { get; set; }
        public Direction Direction { get; set; }

        public abstract Vector2 GetPosition();
        public Rectangle GetSourceRectangle() => new Rectangle(0, 0, GetTexture().Width, GetTexture().Height);
        public virtual Texture2D GetTexture() => TextureManager.PokemonTextures[SpeciesName];
        public virtual Color GetColor() => Color.White;
        public virtual bool WhiteFlash() => false;
        public virtual Texture2D GetWhiteTexture() => TextureManager.WhiteBasicTextures[SpeciesName];
        public virtual Vector2 GetScale() => Vector2.One;
        public virtual SpriteEffects GetSpriteEffects() => SpriteEffects.None;
    }

    public class Wallpaper : IDrawingObject
    {
        public Rectangle GetSourceRectangle() => BattleGraphicsHelper.BattleWallpaperSourceRectangle;

        public Texture2D GetTexture() => TextureManager.BasicTextures[TextureName.BattleWallpaper];

        public Vector2 GetPosition() => BattleGraphicsHelper.BattleWallpaperTargetRectangle.Location.ToVector2();
    }

    public class BattleAsh : IDrawingObject
    {
        public Rectangle GetSourceRectangle() => new Rectangle(0, 0, GetTexture().Width, GetTexture().Height);
        public Texture2D GetTexture() => TextureManager.BasicTextures[TextureName.BattleAsh];
        public Texture2D GetWhiteTexture() => TextureManager.WhiteNamedTextures[TextureName.BattleAsh];
        public Vector2 GetPosition() => new Vector2(2.5f, 2.5f) * Constants.ScaledTileSize;
        public Color GetColor() => Color.White * (BattleStateManager.Battle.State == BattleState.AshSelect ? BattleStateManager.Battle.Timer : 1f);
        public bool WhiteFlash() => BattleStateManager.Battle.State == BattleState.AshSelect;
        public Vector2 GetScale() => new Vector2(0.75f, 0.75f);
        public SpriteEffects GetSpriteEffects() => SpriteEffects.FlipHorizontally;
    }

    public class AshMenu : IDrawingObject
    {
        public Rectangle GetSourceRectangle() => BattleGraphicsHelper.MenuSourceRectangle;
        public Texture2D GetTexture() => TextureManager.BasicTextures[TextureName.Effects];
        public Vector2 GetPosition() => BattleGraphicsHelper.BattleMenuLocation.ToVector2();
        public Color GetColor() => Color.White;
        public bool WhiteFlash() => false;
        public Vector2 GetScale() => new Vector2(0.75f, 0.75f);
        public SpriteEffects GetSpriteEffects() => SpriteEffects.None;
    }

    public class BattleMessageMenu : IDrawingObject
    {
        public Rectangle GetSourceRectangle() => BattleGraphicsHelper.BattleMessageSourceRectangle;
        public Texture2D GetTexture() => TextureManager.BasicTextures[TextureName.Effects];
        public Vector2 GetPosition() => BattleGraphicsHelper.BattleMenuLocation.ToVector2();
        public Color GetColor() => Color.White;
        public bool WhiteFlash() => false;
        public Vector2 GetScale() => new Vector2(0.75f, 0.75f);
        public SpriteEffects GetSpriteEffects() => SpriteEffects.None;
    }

    public class BattleMessageStringObject : IDrawingString
    {
        public string String { get; set; }
        public Vector Position { get; set; }

        public string GetString() => String;
        public Vector2 GetPosition() => Position.ToVector2();
    }
}

