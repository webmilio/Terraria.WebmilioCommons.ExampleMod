using Microsoft.Xna.Framework.Input;
using System.IO;
using Terraria.ModLoader;
using WebCom.Keybinds;
using WebCom.Networking;

namespace WCExampleMod;

public class WCExampleMod : Mod
{
    public override void HandlePacket(BinaryReader reader, int whoAmI)
    {
        // Tell the NetworkPacketLoader to handle our networking for us.
        ModContent.GetInstance<PacketLoader>()
            .HandlePacket(this, reader, whoAmI);
    }
}