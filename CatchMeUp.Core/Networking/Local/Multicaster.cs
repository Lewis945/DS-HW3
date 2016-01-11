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
    public class Multicaster
    {
        public static int Port { get; set; } = 9999;
        public static int Time { get; set; } = 5000;

        public static bool Send { get; set; } = true;
        public static bool Listen { get; set; } = true;

        public static bool LocalComputer { get; set; } = true;

        public static void MulticastGameSession<T>(string ip, Func<T> sendAction)
            where T : IBytePacket
        {
            var udpclient = new UdpClient();

            var localEndPoint = new IPEndPoint(IPAddress.Any, Port);

            var multicastaddress = IPAddress.Parse(ip);
            udpclient.JoinMulticastGroup(multicastaddress);
            var remoteEp = new IPEndPoint(multicastaddress, Port);

            var threadMulticast = new Thread(() =>
            {
                while (Send)
                {
                    var packet = sendAction();

                    int length;
                    udpclient.Send(packet.Pack(out length), length, remoteEp);
                }
            });

            threadMulticast.Start();
        }

        public static void ListenToGameSession<T>(string ip, Action<T, IPEndPoint> callback, Func<T> sendAction)
            where T : IBytePacket
        {
            var localEp = new IPEndPoint(IPAddress.Any, Port);
            var localAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault(ipa => ipa.AddressFamily == AddressFamily.InterNetwork);

            var udpclient = new UdpClient();
            if (LocalComputer)
            {
                udpclient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            }
            udpclient.Client.Bind(localEp);

            var multicastaddress = IPAddress.Parse(ip);
            udpclient.JoinMulticastGroup(multicastaddress);

            var threadListen = new Thread(() =>
            {
                while (Listen)
                {
                    var sender = new IPEndPoint(IPAddress.Any, Port);
                    var data = udpclient.Receive(ref sender);
                    var response = BytePacket<T>.UnPack(data);
                    callback(response, sender);
                }
            });

            threadListen.Start();

            var threadSend = new Thread(() =>
            {
                var remoteEp = new IPEndPoint(multicastaddress, Port);

                while (Send)
                {
                    var responseData = sendAction();
                    int length;
                    udpclient.Send(responseData.Pack(out length), length, remoteEp);
                }
            });

            threadSend.Start();
        }
    }
}
