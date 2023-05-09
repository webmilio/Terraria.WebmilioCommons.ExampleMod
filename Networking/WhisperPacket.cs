using Microsoft.Xna.Framework;
using System;
using System.IO;
using System.Text;
using Terraria;
using Terraria.ModLoader;
using WCExampleMod.Content.Systems;
using WebCom.Networking;

namespace WCExampleMod.Networking;

internal class WhisperPacket : PlayerPacket
{
    protected override void PostReceive(BinaryReader reader, int fromWho)
    {
        var keys = ModContent.GetInstance<EMSystem>();

        var msgBytes = keys.Decrypt(Message);
        var message = Encoding.Unicode.GetString(msgBytes);

        if (IsServer)
        {
            Console.WriteLine($"Player {Player.name} whispered to player {Destination.name}: {message}");

            Message = keys.Encrypt(msgBytes, DestinationId);
            Send(DestinationId);
        }
        else
        {
            Main.NewText($"@{Player.name}: {message}", Color.Gray);
        }
    }

    public int DestinationId { get; set; }
    public Player Destination => Main.player[DestinationId];

    public byte[] Message { get; set; }
}
