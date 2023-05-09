using Terraria.ModLoader;
using WCExampleMod.Networking;
using WebCom.Networking;
using WebCom;
using WebCom.Annotations;
using Terraria.ModLoader.IO;
using WebCom.Saving;
using WCExampleMod.Content.Systems;
using Terraria.GameInput;
using Terraria;
using Terraria.DataStructures;
using WCExampleMod.Content.Projectiles;
using Microsoft.Xna.Framework;

namespace WCExampleMod.Content.Players;

public class EMPlayer : ModPlayer
{
    public override void PreUpdate()
    {
        Player.statLifeMax2 = ExtraHealth;
    }

    public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
    {
        if (newPlayer)
        {
            var keys = ModContent.GetInstance<EMSystem>();
            Mod.PreparePacket(new PublicKeyPacket()
            {
                PublicRSAKey = keys.PublicKey
            })
                .Send(Packet.ServerWhoAmI);
        }
    }

    public override void ProcessTriggers(TriggersSet triggersSet)
    {
        var emSystem = ModContent.GetInstance<EMSystem>();

        if (emSystem.ThrowProjectileUpwards.JustPressed)
        {
            Projectile.NewProjectile(new EntitySource_Parent(Player), Player.position, Player.velocity - new Vector2(0, 5), ModContent.ProjectileType<IntelligenceProjectile>(), 10, 1);
        }
    }

    public override void SaveData(TagCompound tag)
    {
        Saver.This.Save(this, tag);
    }

    public override void LoadData(TagCompound tag)
    {
        Saver.This.Load(this, tag);
    }

    [Save] public int ExtraHealth { get; set; }
}
