using Terraria;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using Neurosama.Content.EmoteBubbles;

public class EmoteSounds : GlobalEmoteBubble
{
    public override void OnSpawn(EmoteBubble bubble)
    {
        if (Main.netMode == NetmodeID.Server)
            return;

        // add emote sounds here
        //PlayEmoteSound<ErmEmote>(bubble, "neuro_erf");
        //PlayEmoteSound<FrickEmote>(bubble, "fx_vineboom");
    }

     private void PlayEmoteSound<T>(EmoteBubble bubble, string soundName) where T : ModEmoteBubble
    {
        int emoteType = ModContent.EmoteBubbleType<T>();

        if (bubble.emote != emoteType)
            return;

        Entity entity = bubble.anchor.entity;
        if (entity == null)
            return;

        SoundEngine.PlaySound(new SoundStyle($"Neurosama/Assets/Sounds/{soundName}"), entity.Center);
    }
}
