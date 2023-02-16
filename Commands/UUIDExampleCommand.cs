using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WCExampleMod.Networking;
using WebCom.Networking;

namespace WCExampleMod.Commands;

internal class UUIDExampleCommand : ModCommand
{

    public override void Action(CommandCaller caller, string input, string[] args)
    {
        var guid = Guid.NewGuid();

        if (Main.netMode == NetmodeID.Server)
            Console.WriteLine($"Sending GUID {guid} to clients!");
        else
            Main.NewText($"Sending GUID {guid} to server!");

        Mod.PreparePacket(new UUIDExamplePacket()
        {
            Guid = guid
        }).Send();
    }

    public override string Command { get; } = nameof(UUIDExampleCommand);
    public override CommandType Type { get; } = CommandType.Chat;
}
