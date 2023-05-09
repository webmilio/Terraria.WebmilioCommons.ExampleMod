using Microsoft.Xna.Framework.Input;
using System.Security.Cryptography;
using Terraria;
using Terraria.ModLoader;
using WebCom;
using WebCom.Extensions;
using WebCom.Keybinds;

namespace WCExampleMod.Content.Systems;

internal class EMSystem : ModSystem
{
    private RSACryptoServiceProvider _selfKey;

    private readonly RSACryptoServiceProvider[] _remoteRSAKeys = new RSACryptoServiceProvider[Main.player.Length + 1];

    public override void Load()
    {
        _selfKey = RSACSP;

        Keybinder.Register(this); // Register all [Keybind]s in the class.
    }

    public override void Unload()
    {
        _selfKey?.Dispose();
        _remoteRSAKeys.Do(k => k?.Dispose());
    }

    internal void ReceiveKey(byte[] publicKey, int fromWho)
    {
        _remoteRSAKeys[fromWho]?.Dispose();

        _remoteRSAKeys[fromWho] = RSACSP;
        _remoteRSAKeys[fromWho].ImportRSAPublicKey(publicKey, out _);

        string name;
        if (fromWho == Packet.ServerWhoAmI)
        {
            name = "server";
        }
        else
        {
            name = Main.player[fromWho].name;
        }

        // TODO Figure out how to use it on server as well.
        Mod.Logger.Info($"Received public key from {name}.");
    }

    internal byte[] Encrypt(byte[] content, int toWho)
    {
        return _remoteRSAKeys[toWho].Encrypt(content, false);
    }

    internal byte[] Decrypt(byte[] content)
    {
        return _selfKey.Decrypt(content, false);
    }

    public byte[] PublicKey => _selfKey.ExportRSAPublicKey();
    private static RSACryptoServiceProvider RSACSP => new RSACryptoServiceProvider(8096);

    [Keybind(nameof(ThrowProjectileUpwards), Keys.P)]
    public ModKeybind ThrowProjectileUpwards { get; private set; }
}
