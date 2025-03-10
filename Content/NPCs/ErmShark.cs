using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader.Utilities;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System;
using Terraria.Chat;
using Terraria.Localization;
using Terraria.GameContent.ItemDropRules;
using System.Collections.Generic;
using System.Linq;
using Terraria.GameContent.Bestiary;

namespace Neurosama.Content.NPCs
{
    public class ErmShark : ModNPC
    {
        // would probably be better to modify the npc after spawning for depth and family
        public ref float family => ref NPC.ai[2];

        public int GetDepth() { return (int)NPC.ai[3]; }

        // Splits 3 times normally, 4 times on expert, and FTW adds 2 extra splits
        public int GetMaxDepth() { return (Main.getGoodWorld ? 2 : 0) + (Main.expertMode ? 4 : 3); }

        public bool MaxDepthReached() { return GetDepth() >= GetMaxDepth(); }

        public bool IsLastDescendant()
        {
            return false; // todo
        }

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 6;
        }

        public override void SetDefaults()
        {
            NPC.width = 62;
            NPC.height = 34;
            NPC.damage = 20;
            NPC.defense = 4;
            NPC.lifeMax = 240;
            NPC.value = 15f;
            NPC.aiStyle = 16;
            NPC.noGravity = true;
            NPC.HitSound = SoundID.NPCHit1; // TODO
            NPC.DeathSound = SoundID.NPCDeath1; // TODO
            AIType = NPCID.Shark; // Moves the same as a shark. Could potentailly add ai that differs individually so splits feel better

            // TODO: only final ermshark counts towards banner
            Banner = Type;
            BannerItem = ModContent.ItemType<Items.Furniture.Banners.ErmSharkBanner>();
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				// Beastiary element from localisation key		
				new FlavorTextBestiaryInfoElement(Language.GetTextValue("Mods.Neurosama.Bestiary.ErmShark"))
            });
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return SpawnCondition.OceanMonster.Chance * 0.02f;
        }

        public override void OnSpawn(IEntitySource source)
        {
            // Set health
            if (!Main.getGoodWorld)
            {
                // Halves max health for each split
                NPC.lifeMax >>= GetDepth();

            }
            else
            {
                // divides max health by 1.5 for each split on FTW
                NPC.lifeMax = (int)(NPC.lifeMax / Math.Pow(1.5, GetDepth()));
            }

            // Should never happen normally, but make sure the health is at least 1
            NPC.lifeMax = Math.Max(NPC.lifeMax, 1);

            NPC.life = NPC.lifeMax;

            // Give first ermshark a family id
            if (GetDepth() == 0)
            {
                family = Main.rand.NextFloat();
                //CombatText.NewText(new Rectangle((int)NPC.Center.X, (int)NPC.Center.Y, 0, 0), new Color(191, 191, 255), family.ToString());
            }

            // Give immunity on spawn so it doesnt instantly die to piercing damage
            Array.Fill(NPC.immune, 10);
        }

        public override void OnKill()
        {
            if (!MaxDepthReached())
            {
                var entitySource = NPC.GetSource_FromAI();
                int newDepth = GetDepth() + 1;

                // Some X variance is added so they arent in the same spot
                // This line randomises whether the higher one is on the left or right
                int spawnOffset = Main.rand.NextBool() ? -12 : 12;

                // Erm Erm
                NPC.NewNPCDirect(entitySource, (int)NPC.Center.X + spawnOffset, (int)NPC.Center.Y, Type, NPC.whoAmI, ai2: family, ai3: newDepth);
                NPC.NewNPCDirect(entitySource, (int)NPC.Center.X - spawnOffset, (int)NPC.Center.Y + 16, Type, NPC.whoAmI, ai2: family, ai3: newDepth);
            }
            else if (Main.npc.Where(x => x.active && x.type == NPC.type && x.ai[2] == family).Count() == 1) // No other ermsharks in family
            {
                // Add code here
                CombatText.NewText(new Rectangle((int)NPC.Center.X, (int)NPC.Center.Y, 0, 0), new Color(255, 191, 191), family.ToString());
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            // Drops not final
            // TODO: ignore all drops unless it's the last in the family
            npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<Items.Furniture.NeuroCatErm>(), 64, 128));
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter++;
            if (NPC.frameCounter >= 30)
            {
                NPC.frameCounter = 0;
            }
            NPC.frame.Y = (int)NPC.frameCounter / 6 * frameHeight;

            NPC.spriteDirection = NPC.direction;
        }
    }
}