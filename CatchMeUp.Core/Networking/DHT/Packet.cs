using CatchMeUp.Core.Sharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace CatchMeUp.Core.Networking.DHT
{
    public enum PacketType
    {
        Put,
        Get,
        Success,
        Failure,
        No_Match,
        Transfer,
        Join
    }

    public enum PacketSource
    {
        AnotherServer,
        Client
    }

    public class Packet : BytePacket<Packet>
    {
        // packet fields - note: all are public
        public PacketType Type { get; set; } // packet type

        public int? TimeToLive { get; set; } // time-to-live

        public string Key { get; set; } // DHT key string
        public string Val { get; set; } // DHT value string

        public string Reason { get; set; } // reason for a failure

        public IPEndPoint ClientAddress { get; set; } // address of original client
        public IPEndPoint RelayAddress { get; set; } // address of first DHT server

        public PacketSource Source { get; set; } // tag used to identify packet

        public Pair<int, int> HashRange { get; set; } // range of hash values

        public Pair<IPEndPoint, int> SenderInfo { get; set; } //address, first hash
        public Pair<IPEndPoint, int> SuccInfo { get; set; } //address, first hash

        public Packet()
        {
        }

        public Packet(PacketSource source)
        {
            Source = source;
        }

        public void CleanPacket()
        {
            ClientAddress = null;
            SenderInfo = null;
            RelayAddress = null;
        }
    }
}
