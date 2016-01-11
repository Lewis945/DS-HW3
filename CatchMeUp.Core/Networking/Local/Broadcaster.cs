using CatchMeUp.Core.Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CatchMeUp.Core.Networking.Local
{
    public class Broadcaster<T> 
        where T : IBytePacket
    {
        public static int Port { get; set; } = 26194;
        public static int Time { get; set; } = 2000;

        public static bool Send { get; set; } = true;
        public static bool Listen { get; set; } = true;

        public static bool LocalComputer { get; set; } = true;

        public static T SendPacket { get; set; }

        public static void BroadcastGameSession(T packet)
        {
            SendPacket = packet;

            var socketSender = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socketSender.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);

            var broadcastAddress = new IPEndPoint(IPAddress.Broadcast, Port);

            var threadBroadcast = new Thread(() =>
            {
                while (Send)
                {
                    try
                    {
                        socketSender.SendTo(SendPacket.Pack(), broadcastAddress);
                    }
                    catch (Exception ex)
                    {
                    }
                    Thread.Sleep(Time);
                }
            });

            threadBroadcast.Start();
        }

        public static void ListenToGameSessions(Action<T, IPEndPoint> callback)
        {
            var localEndPoint = new IPEndPoint(IPAddress.Any, Port);

            var socketListener = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            if (LocalComputer)
            {
                socketListener.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                socketListener.ExclusiveAddressUse = false;
            }
            socketListener.Bind(localEndPoint);

            var threadListen = new Thread(() =>
            {
                while (Listen)
                {
                    var buffer = new byte[1024];
                    var data = new List<byte>();
                    int bytes = 0;

                    var remoteIp = (EndPoint)new IPEndPoint(IPAddress.Any, 0);

                    try
                    {
                        do
                        {
                            bytes = socketListener.ReceiveFrom(buffer, ref remoteIp);
                            data.AddRange(buffer);
                        }
                        while (socketListener.Available > 0);
                    }
                    catch (Exception ex)
                    {
                        break;
                    }

                    var remoteFullIp = remoteIp as IPEndPoint;

                    var response = BytePacket<T>.UnPack(data.ToArray());
                    callback(response, remoteFullIp);
                }

                try
                {
                    socketListener.Close();
                }
                catch (Exception ex)
                {
                }
            });
            threadListen.Start();
        }
    }
}
