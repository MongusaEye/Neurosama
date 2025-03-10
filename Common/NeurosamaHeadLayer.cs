using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Neurosama.Content.Items.Furniture;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Neurosama.Common
{
    public class NeurosamaHeadLayer : PlayerDrawLayer
    {
        private static readonly Texture2D neuroFumoTexture = ModContent.Request<Texture2D>(typeof(NeuroFumo).FullName.Replace('.', '/')).Value;
        private static readonly Texture2D evilFumoTexture  = ModContent.Request<Texture2D>(typeof(EvilFumo ).FullName.Replace('.', '/')).Value;

        public override bool IsHeadLayer => true;

        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
            NeurosamaPlayer modPlayer = drawInfo.drawPlayer.GetModPlayer<NeurosamaPlayer>();

            if (modPlayer.neuroFumoEquipped ||
                modPlayer.evilFumoEquipped)
            {
                return true;
            }
            return false;
        }

        public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.Head);

        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            if (drawInfo.shadow != 0f)
            {
                return;
            }

            Player drawPlayer = drawInfo.drawPlayer;
            NeurosamaPlayer modPlayer = drawPlayer.GetModPlayer<NeurosamaPlayer>();

            // Calculate offset for head bob
            int secondaryOffset;

            var bodFrame = drawPlayer.bodyFrame;
            if (bodFrame.Y == bodFrame.Height * 7 || bodFrame.Y == bodFrame.Height * 8 || bodFrame.Y == bodFrame.Height * 9
                || bodFrame.Y == bodFrame.Height * 14 || bodFrame.Y == bodFrame.Height * 15 || bodFrame.Y == bodFrame.Height * 16)
                secondaryOffset = 2;
            else
                secondaryOffset = 0;

            // Calculate head position
            Vector2 headPosition = drawInfo.helmetOffset +
                new Vector2(
                    (int)(drawInfo.Position.X - drawInfo.drawPlayer.bodyFrame.Width / 2 + drawInfo.drawPlayer.width / 2),
                    (int)(drawInfo.Position.Y + drawInfo.drawPlayer.height - drawInfo.drawPlayer.bodyFrame.Height) - secondaryOffset)
                + drawInfo.drawPlayer.headPosition
                + drawInfo.headVect;

            headPosition -= Main.screenPosition;

            // Get dye shader
            int dyeShader = drawPlayer.dye?[0].dye ?? 0;

            // Draw the equipped item
            // TODO: fix evil always sitting on top and inability to stack 2 neuros or 2 evils
            if (modPlayer.neuroFumoEquipped)
            {
                DrawFumo(drawInfo, drawPlayer, dyeShader, headPosition, neuroFumoTexture);
            }

            if (modPlayer.evilFumoEquipped)
            {
                Vector2 doubleFumoOffset = modPlayer.neuroFumoEquipped ? new Vector2(drawPlayer.direction == 1 ? -2 : 2, 6 - neuroFumoTexture.Height) : Vector2.Zero;
                DrawFumo(drawInfo, drawPlayer, dyeShader, headPosition + doubleFumoOffset, evilFumoTexture);
            }
        }

        private static void DrawFumo(PlayerDrawSet drawInfo, Player drawPlayer, int dyeShader, Vector2 headPosition, Texture2D texture)
        {
            Vector2 position = new Vector2(headPosition.X, headPosition.Y - 2);
            Vector2 origin = new Vector2(texture.Width / 2f, texture.Height);

            SpriteEffects spriteEffect = drawPlayer.direction == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            DrawData drawData = new DrawData(texture, position, null, drawInfo.colorArmorHead, 0f, origin, 1f, spriteEffect, 0);
            drawData.shader = dyeShader;

            drawInfo.DrawDataCache.Add(drawData);
        }
    }
}