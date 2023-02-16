using Microsoft.Xna.Framework;
using System;
using System.IO;
using Terraria;
using Terraria.ID;
using WebCom.Annotations;
using WebCom.Networking;

namespace WCExampleMod.Networking;

internal class WhisperPacket : PlayerPacket
{
    protected override void PostReceive(BinaryReader reader, int fromWho)
    {
        if (IsServer)
        {
            Console.WriteLine($"Player {Player.name} whispered to player {Destination.name}: {Message}");
            Send(DestinationId);
        }
        else
        {
            Main.NewText($"@{Player.name}: {Message}", Color.Gray);
        }
    }

    public int DestinationId { get; set; }
    public Player Destination => Main.player[DestinationId];

    public string Message { get; set; }
}
