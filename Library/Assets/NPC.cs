using System;
using System.Collections.Generic;
using Library.Content;
using Library.Domain;
using Library.GameState;
using Microsoft.Xna.Framework;

namespace Library.Assets
{
    public class NPC : Character
    {
        public List<Pokemon> PokemonStorage { get; set; }
        public Point SpriteLocation { get; set; }
        public override int NumberOfFrames => Name != null ? base.NumberOfFrames : Constants.NPCDefaultFrameCount;

        public NPC() {
            CharacterState = new NPCState(this);
        }

        public Rectangle GetTargetRectangle() => new Rectangle(
            location: ((CharacterState.Position - new Vector(0, 1)) * Constants.ScaledTileSize).ToPoint(),
            size: (SpriteSize * Constants.ScaledTileSize).ToPoint()
        );

        public override Rectangle GetSourceRectangle()
        {
            if (Name != null && Name != CharacterName.Pokemon_Center_Person)
            {
                return base.GetSourceRectangle();
            }

            int x, y;
            x = SpriteLocation.X;
            y = SpriteLocation.Y;

            if (CharacterState.Direction == Direction.Down)
            {
                if (CharacterState.CurrentFrame == 1)
                {
                    x += 3;
                }
            }
            else if (CharacterState.Direction == Direction.Up)
            {
                x += 1;
                if (CharacterState.CurrentFrame == 1)
                {
                    x += 3;
                }
            }
            else
            {
                x += 2;
                if (CharacterState.IsMoving)
                {
                    x += 3 + CharacterState.CurrentFrame;
                }
            }

            return new Rectangle(x * Constants.TileSize, y * Constants.TileSize, 1 * Constants.TileSize, 2 * Constants.TileSize);
        }

        public override TextureName GetTextureName() => Name != null && Name != CharacterName.Pokemon_Center_Person ? (TextureName) Enum.Parse(typeof(TextureName), Name.ToString()) : TextureName.NPCTileset;
    }
}