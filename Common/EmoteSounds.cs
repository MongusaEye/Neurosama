using Terraria;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using Neurosama.Content.EmoteBubbles;
using Neurosama.Content.NPCs.Town;
using Neurosama.Common.Configs;

public class EmoteSounds : GlobalEmoteBubble
{
    public override void OnSpawn(EmoteBubble bubble)
    {
        if (Main.netMode == NetmodeID.Server)
            return;

        if (ModContent.GetInstance<NeurosamaConfig>().EmoteSoundsToggle == false)
            return;
        // add modded emote sounds here
        // Options: "Neuro", "Evil", "Twins", "TwinsAndPlayer"
        PlayEmoteSound(ModContent.EmoteBubbleType<NeweroEmote>(), bubble, "fx_groantube", "Twins");
        PlayEmoteSound(ModContent.EmoteBubbleType<NewlivEmote>(), bubble, "fx_metalpipe", "Evil"); //temporary until pipes emote is drawn
        PlayEmoteSound(EmoteID.EmoteSadness, bubble, "fx_wompwompwomp", "Twins");
        PlayEmoteSound(EmoteID.EmoteSilly, bubble, "fx_clownhorn", "Twins");
        PlayEmoteSound(EmoteID.DebuffCurse, bubble, "fx_incorrectbuzzer", "Twins");
        PlayEmoteSound(EmoteID.EmoteNote, bubble, "fx_correctbuzzer", "Twins");
        PlayEmoteSound(EmoteID.ItemDynamite, bubble, "fx_vineboom", "Twins");
        PlayEmoteSound(EmoteID.DebuffSilence, bubble, "fx_awkwardcrickets", "Twins");
        PlayEmoteSound(EmoteID.EmoteKiss, bubble, "fx_rizz", "Twins"); // temporary until rizz emote is drawn (will not be used currently because not in default emote pool)
        PlayEmoteSound(EmoteID.EmoteFear, bubble, "fx_scaryviolin", "Twins");
        //PlayEmoteSound(EmoteID.ItemSword, bubble, "fx_poke", "Twins"); 
        PlayEmoteSound(EmoteID.CritterSlime, bubble, "fx_sus", "Twins"); //(it looks like the visor of the crewmate trust)
        PlayEmoteSound(EmoteID.ItemTombstone, bubble, "fx_deathbell", "Twins"); // extremely rare, only used by default when like 5% health which they will probably die or heal before using)
        PlayEmoteSound(EmoteID.EmoteLaugh, bubble, "fx_badumtss", "Twins");
            // voicelines
        PlayEmoteSound(ModContent.EmoteBubbleType<ErmEmote>(), bubble, "evil_erm", "Evil");
        PlayEmoteSound(ModContent.EmoteBubbleType<ErmEmote>(), bubble, "neuro_erm", "Neuro");

        PlayEmoteSound(ModContent.EmoteBubbleType<HeartEmote>(), bubble, "evil_heart", "Evil");
        PlayEmoteSound(ModContent.EmoteBubbleType<HeartEmote>(), bubble, "neuro_heart!!", "Neuro");

        PlayEmoteSound(ModContent.EmoteBubbleType<FrickEmote>(), bubble, "evil_frick", "Evil");
        PlayEmoteSound(ModContent.EmoteBubbleType<FrickEmote>(), bubble, "neuro_frick", "Neuro");

        PlayEmoteSound(ModContent.EmoteBubbleType<SmileEmote>(), bubble, "evil_smile", "Evil");
        PlayEmoteSound(ModContent.EmoteBubbleType<SmileEmote>(), bubble, "neuro_smile", "Neuro");

        PlayEmoteSound(ModContent.EmoteBubbleType<FocusEmote>(), bubble, "evil_focus!!", "Evil");
        PlayEmoteSound(ModContent.EmoteBubbleType<FocusEmote>(), bubble, "neuro_hmm!", "Neuro");

    }

    private void PlayEmoteSound(
        int emoteID,
        EmoteBubble bubble,
        string soundName,
        string speaker
    )
    {

        if (bubble.emote != emoteID)
            return;

        if (bubble.anchor.entity is not Player && bubble.anchor.entity is not NPC)
            return;

        if (speaker != "TwinsAndPlayer" && bubble.anchor.entity is Player)
            return;
        
        if (bubble.anchor.entity is NPC npc){

        bool isNeuro = npc.type == ModContent.NPCType<Neuro>();
        bool isEvil  = npc.type == ModContent.NPCType<Evil>();

        if (speaker == "Neuro" && !isNeuro)
            return;

        if (speaker == "Evil" && !isEvil)
            return;

        if (speaker == "Twins" && !isEvil && !isNeuro)
            return;

        if (speaker == "TwinsAndPlayer" && !isEvil && !isNeuro)
            return;

        }

        SoundEngine.PlaySound(
            new SoundStyle($"Neurosama/Assets/Sounds/{soundName}"),
            bubble.anchor.entity.Center
        );

    }
}
