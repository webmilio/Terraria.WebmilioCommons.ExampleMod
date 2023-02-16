using System;
using System.IO;
using Terraria;
using WebCom;
using WebCom.Annotations;

namespace WCExampleMod.Networking;

internal class UUIDExamplePacket : Packet
{
    protected override void PostReceive(BinaryReader reader, int fromWho)
    {
        Console.WriteLine($"Received GUID {Guid} from {fromWho}.");

        if (Main.dedServ)
            this.Send(ignoreClient: fromWho);
    }

    // We skip this property because there are no UUID serializers defined.
    [Skip] public Guid Guid { get; set; }

    // These are what I call "transformers", and allow the packets to read and write in serializers they know.
    // By "transforming" the Guid to a string with `Get`, we use the string serializer. Same for set.
    public string StrGuid
    {
        get => Guid.ToString();
        set => Guid = Guid.Parse(value);
    }
}
