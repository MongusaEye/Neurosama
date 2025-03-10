using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Neurosama.Content.Items
{
	public class iwannadie : ModItem
	{
        // test item lol

		public override void SetDefaults()
		{
			Item.damage = 50;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 80;
			Item.height = 80;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
			Item.value = 10000;
			Item.rare = ItemRarityID.Green;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
		}

		public override void AddRecipes()
		{
            // My beloved
			CreateRecipe()
				.AddIngredient(ItemID.DirtBlock, 1)
				.AddIngredient(ItemID.Wood, 1)
				.AddTile(TileID.WorkBenches)
				.Register();


		}
	}
}