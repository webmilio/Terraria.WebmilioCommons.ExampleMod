using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WebCom.AI;

namespace WCExampleMod.Content.Projectiles;

internal class IntelligenceProjectile : ModProjectile
{
    public override void SetStaticDefaults()
    {
        Projectile.width = 10;
        Projectile.height = 10;
    }

    public override void SetDefaults()
    {
        var builder = Intelligence.Build(this);

        builder.AI()
            .Action(SpewParticles)
            .Finish();

        builder.Finish();
    }

    private void SpewParticles()
    {
        Dust.NewDust(Projectile.position, 2, 2, DustID.BlueFairy, Projectile.velocity.X * -1, Projectile.velocity.Y * -.5f);
        Dust.NewDust(Projectile.position, 2, 2, DustID.BlueFairy, Projectile.velocity.X * -1, Projectile.velocity.Y * 1.5f);
    }
}