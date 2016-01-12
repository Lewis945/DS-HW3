using CatchMeUp.Core.Sharp;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace CatchMeUp.Core.Networking.Local
{
    public class Multicaster
    {
        public static int Port { get; set; } = 26294;
        public static int Time { get; set; } = 100;

        public static bool Send { get; set; } = true;
        public static bool Listen { get; set; } = true;

        public static void MulticastGameSession<T>(string ip, Func<T> sendAction)
            where T : IBytePacket
        {
            var client = new UdpClient();
            client.ExclusiveAddressUse = false;

            var localEp = new IPEndPoint(IPAddress.Any, Port);

            client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            client.ExclusiveAddressUse = false;

            client.Client.Bind(localEp);

            var multicastaddress = IPAddress.Parse(ip);
            client.JoinMulticastGroup(multicastaddress);

            var threadSend = new Thread(() =>
            {
                var remoteEp = new IPEndPoint(multicastaddress, Port);

                try
                {
                    while (Send)
                    {
                        var responseData = sendAction();
                        int length;
                        client.Send(responseData.Pack(out length), length, remoteEp);

                        Thread.Sleep(Time);
                    }
                }
                catch (Exception ex)
                {
                }
            });

            threadSend.Start();
        }

        public static void ListenToGameSession<T>(string ip, Action<T, IPEndPoint> callback)
            where T : IBytePacket
        {
            var client = new UdpClient();
            client.ExclusiveAddressUse = false;

            var localEp = new IPEndPoint(IPAddress.Any, Port);

            client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            client.ExclusiveAddressUse = false;

            client.Client.Bind(localEp);

            var multicastaddress = IPAddress.Parse(ip);
            client.JoinMulticastGroup(multicastaddress);

            var threadListen = new Thread(() =>
            {
                try
                {
                    while (Listen)
                    {
                        var data = client.Receive(ref localEp);
                        var response = BytePacket<T>.UnPack(data);
                        callback(response, localEp);

                        //if ((response as GameMulticastPacket).Move != 0)
                        //    Debug.WriteLine("Received " + response.ToString());
                    }

                    client.Close();
                }
                catch (Exception ex)
                {
                }
            });
            threadListen.Start();
        }
    }
}
