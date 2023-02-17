using Terraria.ModLoader;
using WCExampleMod.Networking;
using WebCom.Networking;
using WebCom;
using WCExampleMod.Systems;
using System;

namespace WCExampleMod.Players;

public class EMPlayer : ModPlayer
{
    public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
    {
        if (newPlayer)
        {
            var keys = ModContent.GetInstance<EMSystem>();
            Mod.PreparePacket(new PublicKeyPacket()
            {
                PublicRSAKey = keys.PublicKey,
                AESKey = Array.Empty<byte>()
            })
                .Send(Packet.ServerWhoAmI);
        }
    }
}
