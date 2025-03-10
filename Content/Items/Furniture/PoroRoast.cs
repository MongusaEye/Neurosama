using Neurosama.Content.Tiles.Furniture;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Neurosama.Content.Items.Furniture
{
	public class PoroRoast : ModItem
	{
        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.PoroRoast>());

            Item.width = 32;
            Item.height = 32;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.buyPrice(0, 0, 80);
        }
    }
}
