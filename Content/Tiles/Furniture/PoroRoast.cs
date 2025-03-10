using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Neurosama.Content.Tiles.Furniture
{
	public class PoroRoast : ModTile
	{
		public override void SetStaticDefaults() {
			Main.tileFrameImportant[Type] = true;
			Main.tileLavaDeath[Type] = true;
			TileID.Sets.FramesOnKillWall[Type] = true;

			// Create normal
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.Direction = TileObjectDirection.PlaceLeft;

			// I have no idea why you need these lines
            TileObjectData.newTile.StyleWrapLimit = 2;
            TileObjectData.newTile.StyleMultiplier = 2;
            TileObjectData.newTile.StyleHorizontal = true;

            // Create flipped ver
            TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceRight;

			// Add tiles
			TileObjectData.addAlternate(1); // Facing right will use the second texture style
            TileObjectData.addTile(Type);

            LocalizedText name = CreateMapEntryName();
            AddMapEntry(new Color(189, 95, 27), name);
			DustType = DustID.Torch;

            // Animation stuff
            AnimationFrameHeight = 36;
        }

        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            if (++frameCounter >= 8)
            {
                frameCounter = 0;
                if (++frame >= 15)
                {
                    frame = 0;
                }
            }
        }
    }
}
