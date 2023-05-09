using Terraria.ModLoader;
using WCExampleMod.Content.Players;

namespace WCExampleMod.Commands;

internal class SetExtraHealthCommand : ModCommand
{
    public override void Action(CommandCaller caller, string input, string[] args)
    {
        var extraHealth = int.Parse(args[1]);
        caller.Player.GetModPlayer<EMPlayer>().ExtraHealth = extraHealth;
    }

    public override string Command { get; } = nameof(SetExtraHealthCommand);

    public override CommandType Type { get; } = CommandType.Chat;
}
