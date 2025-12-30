using Terraria;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using Neurosama.Content.EmoteBubbles;
using Neurosama.Content.NPCs.Town;
using Neurosama.Common.Configs;


public enum EmoteOrigin
{
    Twins,
    Evil,
    Neuro,
    TwinsAndPlayer
}

public class EmoteSounds : GlobalEmoteBubble
{
    public override void OnSpawn(EmoteBubble bubble)
    {
        if (Main.netMode == NetmodeID.Server)
            return;

        if (ModContent.GetInstance<NeurosamaConfig>().EmoteSoundsToggle == false)
            return;
        // add emote sounds here
        PlayEmoteSound(ModContent.EmoteBubbleType<NeweroEmote>(), bubble, "SFX/groantube", EmoteOrigin.Twins);
        PlayEmoteSound(ModContent.EmoteBubbleType<NewlivEmote>(), bubble, "SFX/metalpipe", EmoteOrigin.Evil); //temporary until pipes emote is drawn
        PlayEmoteSound(EmoteID.EmoteSadness, bubble, "SFX/wompwompwomp", EmoteOrigin.Twins);
        PlayEmoteSound(EmoteID.EmoteSilly, bubble, "SFX/clownhorn", EmoteOrigin.Twins);
        PlayEmoteSound(EmoteID.DebuffCurse, bubble, "SFX/incorrectbuzzer", EmoteOrigin.Twins);
        PlayEmoteSound(EmoteID.EmoteNote, bubble, "SFX/correctbuzzer", EmoteOrigin.Twins);
        PlayEmoteSound(EmoteID.ItemDynamite, bubble, "SFX/vineboom", EmoteOrigin.Twins);
        PlayEmoteSound(EmoteID.DebuffSilence, bubble, "SFX/awkwardcrickets", EmoteOrigin.Twins);
        PlayEmoteSound(EmoteID.EmoteKiss, bubble, "SFX/rizz", EmoteOrigin.Twins); // temporary until rizz emote is drawn (will not be used currently because not in default emote pool)
        PlayEmoteSound(EmoteID.EmoteFear, bubble, "SFX/scaryviolin", EmoteOrigin.Twins);
        //PlayEmoteSound(EmoteID.ItemSword, bubble, "SFX/poke", EmoteOrigin.Twins); 
        PlayEmoteSound(EmoteID.CritterSlime, bubble, "SFX/sus", EmoteOrigin.Twins); //(it looks like the visor of the crewmate trust)
        PlayEmoteSound(EmoteID.ItemTombstone, bubble, "SFX/deathbell", EmoteOrigin.Twins); // extremely rare, only used by default when like 5% health which they will probably die or heal before using)
        PlayEmoteSound(EmoteID.EmoteLaugh, bubble, "SFX/badumtss", EmoteOrigin.Twins);
            // voicelines
        PlayEmoteSound(ModContent.EmoteBubbleType<ErmEmote>(), bubble, "evil_erm", EmoteOrigin.Evil);
        PlayEmoteSound(ModContent.EmoteBubbleType<ErmEmote>(), bubble, "neuro_erm", EmoteOrigin.Neuro);

        PlayEmoteSound(ModContent.EmoteBubbleType<HeartEmote>(), bubble, "evil_heart", EmoteOrigin.Evil);
        PlayEmoteSound(ModContent.EmoteBubbleType<HeartEmote>(), bubble, "neuro_heart!!", EmoteOrigin.Neuro);
        PlayEmoteSound(ModContent.EmoteBubbleType<FrickEmote>(), bubble, "evil_frick", EmoteOrigin.Evil);
        PlayEmoteSound(ModContent.EmoteBubbleType<FrickEmote>(), bubble, "neuro_frick", EmoteOrigin.Neuro);

        PlayEmoteSound(ModContent.EmoteBubbleType<SmileEmote>(), bubble, "evil_smile", EmoteOrigin.Evil);
        PlayEmoteSound(ModContent.EmoteBubbleType<SmileEmote>(), bubble, "neuro_smile", EmoteOrigin.Neuro);

        PlayEmoteSound(ModContent.EmoteBubbleType<FocusEmote>(), bubble, "evil_focus!!", EmoteOrigin.Evil);
        PlayEmoteSound(ModContent.EmoteBubbleType<FocusEmote>(), bubble, "neuro_hmm!", EmoteOrigin.Neuro);

    }

    private void PlayEmoteSound(
        int emoteID,
        EmoteBubble bubble,
        string soundName,
        EmoteOrigin speaker
    )
    {

        if (bubble.emote != emoteID)
            return;

        if (bubble.anchor.entity is not Player && bubble.anchor.entity is not NPC)
            return;

        if (speaker != EmoteOrigin.TwinsAndPlayer && bubble.anchor.entity is Player)
            return;
        
        if (bubble.anchor.entity is NPC npc){

        bool isNeuro = npc.type == ModContent.NPCType<Neuro>();
        bool isEvil  = npc.type == ModContent.NPCType<Evil>();

        if (speaker == EmoteOrigin.Neuro && !isNeuro)
            return;

        if (speaker == EmoteOrigin.Evil && !isEvil)
            return;

        if (speaker == EmoteOrigin.Twins && !isEvil && !isNeuro)
            return;

        if (speaker == EmoteOrigin.TwinsAndPlayer && !isEvil && !isNeuro)
            return;

        }

        SoundEngine.PlaySound(
            new SoundStyle($"Neurosama/Assets/Sounds/{soundName}"),
            bubble.anchor.entity.Center
        );

    }
}
