using RimWorld;
using Verse;

namespace ScrubGun
{
    // This class is required to allow the game to load your custom XML tags into memory.
    public class ThingDef_PlagueBullet : ThingDef
    {
        public HediffDef HediffToAdd;
        public float AddHediffChance = 0.5f;
    }
}