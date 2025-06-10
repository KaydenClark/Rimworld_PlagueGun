using RimWorld;
using Verse;

namespace ScrubGun
{
    // This is the code that runs when the bullet is in the game world.
    public class Projectile_PlagueBullet : Bullet
    {
        // This is a property to get your custom Def, which is much cleaner than casting it every time.
        public ThingDef_PlagueBullet Def
        {
            get
            {
                return this.def as ThingDef_PlagueBullet;
            }
        }

        protected override void Impact(Thing hitThing, bool blockedByShield = false)
        {
            base.Impact(hitThing, blockedByShield);

            if (Def != null && hitThing != null && hitThing is Pawn hitPawn)
            {
                float rand = Rand.Value;
                if (rand <= Def.AddHediffChance)
                {
                    Messages.Message("SLK_PlagueBullet_SuccessMessage".Translate(this.launcher.Label, hitPawn.Label), MessageTypeDefOf.NegativeHealthEvent);

                    Hediff plagueOnPawn = hitPawn.health?.hediffSet?.GetFirstHediffOfDef(Def.HediffToAdd);
                    float randomSeverity = Rand.Range(0.15f, 0.30f);

                    if (plagueOnPawn != null)
                    {
                        plagueOnPawn.Severity += randomSeverity;
                    }
                    else
                    {
                        Hediff hediff = HediffMaker.MakeHediff(Def.HediffToAdd, hitPawn);
                        hediff.Severity = randomSeverity;
                        hitPawn.health.AddHediff(hediff);
                    }
                }
                else
                {
                    MoteMaker.ThrowText(hitThing.PositionHeld.ToVector3(), hitThing.MapHeld, "SLK_PlagueBullet_FailureMote".Translate(Def.AddHediffChance.ToStringPercent()), 1.9f);
                }
            }
        }
    }
}