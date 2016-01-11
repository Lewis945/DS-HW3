using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CatchMeUp.Core.Networking.DHT
{
    public class Client
    {
        //Initializes all the parameters for the DhtClient.
        private PacketType _type;

        private string _key;
        private string _value;

        private IPEndPoint _address;
        private IPEndPoint _serverAddress;

        private UdpClient _client;

        /// <summary>
        /// Initializes all the paremeters of a DHT client object.
        /// </summary>
        /// <param name="address"></param>
        /// <param name="serverAddress"></param>
        /// <param name="operation"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public Client(string ip, int port, string serverAddressIp, int serverAddressPort)
        {
            try
            {
                _serverAddress = new IPEndPoint(IPAddress.Parse(serverAddressIp), serverAddressPort);

                if (!string.IsNullOrWhiteSpace(ip) && port > 0)
                {
                    _client = new UdpClient(new IPEndPoint(IPAddress.Parse(ip), port));
                }
                else if (port > 0)
                {
                    _client = new UdpClient(port);
                }

                _address = _client.Client.LocalEndPoint as IPEndPoint;

            }
            catch (Exception e)
            {
            }
        }

        /// <summary>
        /// Sends the packet to the corresponding server.
        /// </summary>
        /// <param name="packet"> packet to be send to the client.</param>
        public void sendToServer(PacketType type, string key, string value = null)
        {
            var packet = new Packet(PacketSource.Client);

            //Initializing the outgoing packet's values.
            packet.Type = type;
            packet.Key = key;
            packet.Val = value;

            //Reads the servers information from its configuration file.
            try
            {
                int length;
                _client.Send(packet.Pack(out length), length, _serverAddress);
            }
            catch (Exception e)
            {
                //System.out.println("Incorrect server IP or port.");
            }
        }

        /// <summary>
        /// Receives a packet and outputs an error if it fails.
        /// </summary>
        /// <param name="packet">packet to be received</param>
        public Packet receiveFromServer()
        {
            //Receive a packet and store its address.
            var receiver = new IPEndPoint(IPAddress.Any, 0);
            var data = _client.Receive(ref receiver);
            var packet = Packet.UnPack(data);

            //Detect any errors.
            if (receiver == null)
            {
                //System.out.println("Received packet failure");
                //System.exit(gen.PACKET_FAILURE);
            }

            return packet;
        }
    }
}
