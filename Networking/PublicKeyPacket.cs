using System;
using System.IO;
using Terraria;
using Terraria.ModLoader;
using WCExampleMod.Content.Systems;
using WebCom;

namespace WCExampleMod.Networking;

internal class PublicKeyPacket : Packet
{
    protected override void PostReceive(BinaryReader reader, int fromWho)
    {
        var keys = ModContent.GetInstance<EMSystem>();
        keys.ReceiveKey(PublicRSAKey, fromWho);

        if (IsServer)
        {
            ServerResponse = true;
            
            PublicRSAKey = keys.PublicKey;

            Send(fromWho);
        }
    }

    public byte[] PublicRSAKey { get; set; }
    public byte[] AESKey { get; set; }

    public bool ServerResponse { get; set; }
}
