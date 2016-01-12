using CatchMeUp.Core.Sharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace CatchMeUp.Core.Networking.Local
{
    public class Broadcaster
    {
        public static int Port { get; set; } = 26194;
        public static int Time { get; set; } = 2000;

        public static bool Send { get; set; } = true;
        public static bool Listen { get; set; } = true;

        public static void BroadcastGameSession<T>(Func<T> sendAction)
            where T : IBytePacket
        {
            var socketSender = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socketSender.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);

            var broadcastAddress = new IPEndPoint(IPAddress.Broadcast, Port);

            var threadBroadcast = new Thread(() =>
            {
                try
                {
                    while (Send)
                    {
                        var packet = sendAction();
                        socketSender.SendTo(packet.Pack(), broadcastAddress);

                        Thread.Sleep(Time);
                    }

                    socketSender.Close();
                }
                catch (Exception ex)
                {
                }
            });

            threadBroadcast.Start();
        }

        public static void ListenToGameSessions<T>(Action<T, IPEndPoint> callback)
            where T : IBytePacket
        {
            var localEndPoint = new IPEndPoint(IPAddress.Any, Port);

            var socketListener = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socketListener.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            socketListener.ExclusiveAddressUse = false;
            socketListener.Bind(localEndPoint);

            var threadListen = new Thread(() =>
            {
                try
                {
                    while (Listen)
                    {
                        var buffer = new byte[1024];
                        var data = new List<byte>();
                        int bytes = 0;

                        var remoteIp = (EndPoint)new IPEndPoint(IPAddress.Any, 0);

                        do
                        {
                            bytes = socketListener.ReceiveFrom(buffer, ref remoteIp);
                            data.AddRange(buffer);
                        }
                        while (socketListener.Available > 0);

                        var remoteFullIp = remoteIp as IPEndPoint;

                        var response = BytePacket<T>.UnPack(data.ToArray());
                        callback(response, remoteFullIp);
                    }


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
