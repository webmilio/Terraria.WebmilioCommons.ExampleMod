using Microsoft.Xna.Framework;
using System.Text;
using Terraria;
using Terraria.ModLoader;
using WCExampleMod.Networking;
using WCExampleMod.Systems;
using WebCom;
using WebCom.Networking;
using WebCom.Tinq;

namespace WCExampleMod.Commands;

internal class WhisperCommand : ModCommand
{
    public override void Action(CommandCaller caller, string input, string[] args)
    {
        var dst = Main.player.FirstActive(p => p.name.Equals(args[0], System.StringComparison.OrdinalIgnoreCase));

        if (dst == null)
        {
            Main.NewText($"Could not find player with name {args[0]}!", Color.Red);
            return;
        }

        var msg = string.Join(" ", args, 1, args.Length - 1);

        Main.NewText($"To {dst.name}: {msg}", Color.Gray);

        var keys = ModContent.GetInstance<EMSystem>();
        var msgBytes = Encoding.Unicode.GetBytes(msg);
        var msgEncrypted = keys.Encrypt(msgBytes, Packet.ServerWhoAmI);

        Mod.PreparePacket(new WhisperPacket()
        {
            Player = caller.Player,

            DestinationId = dst.whoAmI,
            Message = msgEncrypted
        })
            .Send(Packet.ServerWhoAmI);
    }

    public override string Command { get; } = "msg";
    public override CommandType Type { get; } = CommandType.Chat;
}
