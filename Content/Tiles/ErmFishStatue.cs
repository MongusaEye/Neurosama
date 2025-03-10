using Microsoft.Xna.Framework;
using Neurosama.Content.NPCs;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Neurosama.Content.Tiles
{
	// See StatueWorldGen to see how ExampleStatue is added as an option for naturally spawning statues during worldgen.
	public class ErmFishStatue : ModTile
	{
		public override void SetStaticDefaults() {
			Main.tileFrameImportant[Type] = true;
			Main.tileObsidianKill[Type] = true;
			TileID.Sets.DisableSmartCursor[Type] = true;
			TileID.Sets.IsAMechanism[Type] = true; // Ensures that this tile and connected pressure plate won't be removed during the "Remove Broken Traps" worldgen step

			TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
			TileObjectData.addTile(Type);

			DustType = DustID.Stone;

			AddMapEntry(new Color(144, 148, 144), Language.GetText("MapObject.Statue"));
		}

		// Example mod forgot to do this lol
		public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
		{
			offsetY = 2;
			// TODO: offset in preview as well
		}

        public override void HitWire(int i, int j) {
            Tile tile = Main.tile[i, j];

            // Find the coordinates of top left tile square through math
            int topLeftX = i - tile.TileFrameX / 18;
            int topLeftY = j - tile.TileFrameY / 18;

			const int TileWidth = 2;
			const int TileHeight = 3;

			// SkipWire all tile coordinates covered by this tile to make sure it doesnt activate multiple times
			for (int y = topLeftY; y < topLeftY + TileHeight; y++) {
				for (int x = topLeftX; x < topLeftX + TileWidth; x++) {
					Wiring.SkipWire(x, y);
				}
			}

			float spawnX = (topLeftX + TileWidth * 0.5f) * 16;
			float spawnY = (topLeftY + TileHeight * 1.125f) * 16; // Not 100% sure if this height is correct

			var entitySource = new EntitySource_TileUpdate(topLeftX, topLeftY, context: "ErmFishStatue");

			int spawnedNpcId = ModContent.NPCType<ErmFish>();

            int npcIndex = -1;
            if (Wiring.CheckMech(topLeftX, topLeftY, 30) && NPC.MechSpawn(spawnX, spawnY, spawnedNpcId)) {
				npcIndex = NPC.NewNPC(entitySource, (int)spawnX, (int)spawnY - 12, spawnedNpcId);
			}

			if (npcIndex >= 0) {
				var npc = Main.npc[npcIndex];

				npc.value = 0f;
				npc.npcSlots = 0f; // Statue enemies don't take up npc slots
                npc.SpawnedFromStatue = true; // Makes drops and catchability consistent with statue enemies
            }
		}
	}
}