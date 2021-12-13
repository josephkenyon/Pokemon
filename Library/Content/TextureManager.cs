using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using Library.Domain;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using Microsoft.Xna.Framework;
using Library.Graphics;

namespace Library.Content
{
    public static class TextureManager
    {
        public static Dictionary<TextureName, Texture2D> BasicTextures { get; private set; }
        public static Dictionary<TextureName, Texture2D> WhiteNamedTextures { get; private set; }
        public static Dictionary<Species, Texture2D> PokemonTextures { get; private set; }
        public static Dictionary<Species, Texture2D> WhiteBasicTextures { get; private set; }

        public static void Load(ContentManager contentManager)
        {
            PokemonTextures = new Dictionary<Species, Texture2D>();
            WhiteBasicTextures = new Dictionary<Species, Texture2D>();
            WhiteNamedTextures = new Dictionary<TextureName, Texture2D>();
            BasicTextures = new Dictionary<TextureName, Texture2D>
            {
                { TextureName.Ash, contentManager.Load<Texture2D>("sprites\\characters\\ashWalking") },
                { TextureName.Oak, contentManager.Load<Texture2D>("sprites\\characters\\oakWalking") },
                { TextureName.Green, contentManager.Load<Texture2D>("sprites\\characters\\greenWalking") },
                { TextureName.Background, contentManager.Load<Texture2D>("sprites\\tilesets\\backgroundTileset") },
                { TextureName.Foreground, contentManager.Load<Texture2D>("sprites\\tilesets\\foregroundTileset") },
                { TextureName.Grass, contentManager.Load<Texture2D>("sprites\\tilesets\\grassTileset") },
                { TextureName.Animation, contentManager.Load<Texture2D>("sprites\\tilesets\\animationTileset") },
                { TextureName.Effects, contentManager.Load<Texture2D>("sprites\\tilesets\\effectsTileset") },
                { TextureName.BattleWallpaper, contentManager.Load<Texture2D>("sprites\\wallpapers\\battleArea") },
                { TextureName.HealthExpBar, contentManager.Load<Texture2D>("sprites\\effects\\healthExpBar") },
                { TextureName.EmptyWhiteTexture, contentManager.Load<Texture2D>("sprites\\effects\\blankWhiteTexture") },
                { TextureName.BattleAsh, contentManager.Load<Texture2D>("sprites\\characters\\battleAsh") },
                { TextureName.NPCTileset, contentManager.Load<Texture2D>("sprites\\characters\\npcTileset") }
            };

            foreach (Species species in Enum.GetValues(typeof(Species)))
            {
                PokemonTextures.Add(species, contentManager.Load<Texture2D>("sprites\\pokemon\\" + species.ToString().ToLower()));
            }

            foreach (Species species in Enum.GetValues(typeof(Species)))
            {
                WhiteBasicTextures.Add(species, GetWhiteTexture(PokemonTextures[species]));
            }

            foreach (TextureName textureName in BasicTextures.Keys)
            {
                WhiteNamedTextures.Add(textureName, GetWhiteTexture(BasicTextures[textureName]));
            }
        }

        private static Texture2D GetWhiteTexture(Texture2D texture) {
            var b = new Bgr565[texture.Width * texture.Height * 2];
            var c = new Bgr565[texture.Width * texture.Height * 2];
            texture.GetData(b);

            for (int i = 0; i < b.Length; i += 2)
            {
                Vector4 vector4Alpha = b[i + 1].ToVector4();
                if (vector4Alpha.X > 0 || vector4Alpha.Y > 0 || vector4Alpha.Z > 0)
                {
                    c[i] = new Bgr565(1, 1, 1);
                    c[i + 1] = new Bgr565(1, 1, 1);
                }
                else
                {
                    c[i] = new Bgr565(0, 0, 0);
                    c[i + 1] = new Bgr565(0, 0, 0);
                }
            };

            Texture2D newTexture = new Texture2D(GraphicsManager.GraphicsDevice, texture.Width, texture.Height);
            newTexture.SetData(c);
            return newTexture;
        }
    }
}
