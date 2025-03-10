using Neurosama.Content.Items.Furniture;
using Terraria.ModLoader;

namespace Neurosama.Common
{
    public class NeurosamaPlayer : ModPlayer
    {
        public bool neuroFumoEquipped;
        public bool evilFumoEquipped;

        public override void Initialize() => ResetEquips();
        public override void ResetEffects() => ResetEquips();
        public override void UpdateDead() => ResetEquips();

        public override void UpdateEquips()
        {
            if (Player.armor[10].type == ModContent.ItemType<NeuroFumo>())
            {
                neuroFumoEquipped = true;
            }
            else if (Player.armor[10].type == ModContent.ItemType<EvilFumo>())
            {
                evilFumoEquipped = true;
            }
        }

        private void ResetEquips()
        {
            neuroFumoEquipped = false;
            evilFumoEquipped = false;
        }
    }
}
