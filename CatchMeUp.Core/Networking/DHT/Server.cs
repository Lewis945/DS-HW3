using CatchMeUp.Core.Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CatchMeUp.Core.Networking.DHT
{
    public class Server
    {
        #region Fields

        private string _ip;
        private int _port;

        private string _predecessorIp;
        private int _predecessorPort;

        private IPEndPoint _address;
        private IPEndPoint _predecessorAddress;

        private UdpClient _client;

        private Pair<IPEndPoint, int> _succInfo; // successor
        private Pair<int, int> _hashRange; // my DHT hash range

        private int _numRoutes;      // number of routes in routing table
        private bool _cacheOn = true;    // enables caching when true

        private Dictionary<string, string> _map; // key/value pairs
        private Dictionary<string, string> _cache; // cached pairs
        private List<Pair<IPEndPoint, int>> _routesTable;//Routing Table

        #endregion

        #region .ctor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <param name="predecessorIp"></param>
        /// <param name="predecessorPort"></param>
        public Server(string ip, int port, string predecessorIp, int predecessorPort)
        {
            _ip = ip;
            _port = port;

            _predecessorIp = predecessorIp;
            _predecessorPort = predecessorPort;

            if (!string.IsNullOrWhiteSpace(ip) && port > 0)
            {
                _client = new UdpClient(new IPEndPoint(IPAddress.Parse(ip), _port));
            }
            else if (port > 0)
            {
                _client = new UdpClient(_port);
            }

            _address = _client.Client.LocalEndPoint as IPEndPoint;

            if (!string.IsNullOrWhiteSpace(predecessorIp) && predecessorPort > 0)
            {
                _predecessorAddress = new IPEndPoint(IPAddress.Parse(predecessorIp), predecessorPort);
                //_client.Connect(_predecessorAddress);
            }

            _numRoutes = 2; // Routes in table.

            _cacheOn = false;    // Default false for cache and debug.

            _map = new Dictionary<string, string>(); // Map of key,value pairs.
            _cache = new Dictionary<string, string>(); // Map of cache.

            //The routing table to store server shortcuts.
            _routesTable = new List<Pair<IPEndPoint, int>>();

            //Range of values allowed to be mapped in this server.
            _hashRange = new Pair<int, int>(0, int.MaxValue);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        public void StartServer()
        {
            if (_predecessorAddress == null)
            {
                _succInfo = new Pair<IPEndPoint, int>(_address, 0);

                System.Diagnostics.Debug.WriteLine("Server started at " + _address.ToString());
            }
            else
            {
                //Joining the predecessor server.
                Join(_predecessorAddress);

                System.Diagnostics.Debug.WriteLine("Server joined to " + _predecessorAddress.ToString());
            }

            //Begin listening for clients.
            ListenForClients();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Join an existing DHT.
        /// </summary>
        /// <param name="predAdr">is the address of the already existing DHT server.</param>
        private void Join(IPEndPoint predAdr)
        {
            //Creating the outgoing packet with type join and tag sendTag.
            var packet = new Packet()
            {
                Type = PacketType.Join,
                Source = PacketSource.AnotherServer,
                SenderInfo = new Pair<IPEndPoint, int>(_address, 0)
            };

            try
            {
                int length;
                _client.Send(packet.Pack(out length), length, predAdr);
            }
            catch (Exception ex)
            {

            }

            var sender = new IPEndPoint(IPAddress.Any, 0);
            var data = _client.Receive(ref sender);
            packet = Packet.UnPack(data);

            //Upon succesfull joining of another server.
            if (packet.Type == PacketType.Success)
            {
                //Setting the server's hashRange to the one specified by the original server's packet.
                _hashRange = new Pair<int, int>(packet.HashRange.First, packet.HashRange.Second);

                //Setting the succesor of this server to the one specified by the previous server.
                _succInfo = new Pair<IPEndPoint, int>(packet.SuccInfo.First, packet.SuccInfo.Second);
                AddRoute(_succInfo);
            }
        }

        /// <summary>
        /// Server now begins to listen for client's requests.
        /// </summary>
        private void ListenForClients()
        {
            new Thread(() =>
            {
                while (true)
                {
                    var sender = new IPEndPoint(IPAddress.Any, 0);
                    var data = _client.Receive(ref sender);
                    var packet = Packet.UnPack(data);

                    if (packet.SenderInfo != null)
                    {
                        AddRoute(packet.SenderInfo);
                    }

                    //Handle the packet according to its type.
                    HandlePacket(packet, sender);

                    //udpServer.Send(response, response.Length, remoteEP); // if data is received reply letting the client know that we got his data          
                }
            }).Start();
        }

        /// <summary>
        /// Add an entry to the route table.
        /// Adds a route or not depending on all the different sates that
        /// the route table can be in.
        /// </summary>
        /// <param name="newRoute">
        /// is a pair (addr,hash) where addr is the socket address for
        /// some server and hash is the first hash in that server's range
        /// If the number of entries in the table exceeds the max number
        /// allowed, the first entry that does not refer to the successor
        /// of this server, is removed.If debug is true and the set of
        /// stored routes does change, print the string "rteTbl=" +
        /// rteTbl. (IMPORTANT)</param>
        private void AddRoute(Pair<IPEndPoint, int> newRoute)
        {
            var myPair = new Pair<IPEndPoint, int>(_address, _hashRange.Second);

            //Does not add null routes or this server itself to the table.
            if (newRoute == null || newRoute.Equals(myPair))
            {
                return;
            }

            //Iterate over the routing table so that we do not add repeating routes.
            foreach (var element in _routesTable)
            {
                if (element.Equals(newRoute))
                {
                    return;
                }
            }

            //Consider the cases when the size of the routing table is at its limit.
            if (_routesTable.Count >= _numRoutes)
            {
                if (_routesTable.Count == 1 && _routesTable[0].Equals(_succInfo))
                {
                    return;
                }

                int rm_index = _routesTable[0].Equals(_succInfo) ? 1 : 0;
                _routesTable.RemoveAt(rm_index);
            }

            //Add the new route.
            var routeToAdd = new Pair<IPEndPoint, int>(newRoute.First, newRoute.Second);

            _routesTable.Add(routeToAdd);

            //Print the debug routing table.
            //if (debug)
            //    System.out.println("rteTbl=" + rteTbl);
        }

        /// <summary>
        /// Handle packets received from clients or other servers
        /// </summary>
        /// <param name="packet">packet to be analyzed.</param>
        /// <param name="sender">the address of the packet's sender.</param>
        private void HandlePacket(Packet packet, IPEndPoint sender)
        {
            //Checking the type of the packet, and calling the responsible
            //function for it.
            if (packet.Type == PacketType.Transfer)
            {
                HandleXfer(packet);
                return;
            }
            else if (packet.Type == PacketType.Join)
            {
                HandleJoin(packet, sender);
                return;
            }
            else if (packet.Type == PacketType.Success || packet.Type == PacketType.No_Match)
            {
                //Add to cache if successful or no match and cache is set.
                AddToCache(packet);
                //reply the packet.
                SendBack(packet, packet.ClientAddress);
                return;
            }
            //Checking if the packet is outside the server's assigned range.
            else if (!IsInRange(packet.Key))
            {
                //If the key is in cache, return its value.
                if (packet.Type == PacketType.Get && GetFromCache(packet))
                {
                    SendBack(packet, sender);
                }
                //Otherwise forward the packet
                else
                {
                    Forward(packet, sender);
                }
                return;
            }
            else if (packet.Type == PacketType.Get)
            {
                HandleGet(packet);
            }
            else if (packet.Type == PacketType.Put)
            {
                HandlePut(packet);
            }

            //After modifying the packet accordingly, return it.
            SendBack(packet, sender);
        }

        /// <summary>
        /// Checks if a packet is in range of this server or not.
        /// </summary>
        /// <param name="p">packet to be checked for range</param>
        /// <returns>returns true if the packet is in range or false if otherwise.</returns>
        private bool IsInRange(string key)
        {
            //Hashing the key and comparing it to the server's hashRange.
            var hash = HashIt(key);
            int first = _hashRange.First;
            int second = _hashRange.Second;
            if (first <= hash && hash <= second)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if a packet's key is in the cache, if the cache is set.
        /// </summary>
        /// <param name="p">returns true of the packet's key is in the cache and the
        /// packet to be checked.</param>
        /// <returns></returns>
        private bool GetFromCache(Packet p)
        {
            if (_cacheOn && _cache.ContainsKey(p.Key))
            {
                p.Type = PacketType.Success;
                p.Val = _cache[p.Key];
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns the packet to the address specified, usually the client.
        /// </summary>
        /// <param name="packet">packet to be returned.</param>
        /// <param name="sender">address for the packet to be returned.</param>
        private void SendBack(Packet packet, IPEndPoint sender)
        {
            //Initializing the reply address.
            IPEndPoint replyAdr = null;

            //Setting the return address to the client, whether that is 
            //known in the packet or is the sender.
            if (EqualInetSocketAddress(packet.RelayAddress, _address) || packet.RelayAddress == null)
            {
                replyAdr = packet.ClientAddress == null ? sender : packet.ClientAddress;
                packet.CleanPacket();
            }

            //Returns to the relay address.
            else if (packet.RelayAddress != null)
            {
                packet.SenderInfo = new Pair<IPEndPoint, int>(_address, _hashRange.First);
                replyAdr = packet.RelayAddress;
            }

            //return the packet.
            int length;
            _client.Send(packet.Pack(out length), length, replyAdr);
        }

        /// <summary>
        /// Forward a packet using the local routing table.
        /// This method selects a server from its route table that is
        /// "closest" to the target of this packet (based on hash). If
        /// firstHash is the first hash in a server's range, then we
        /// seek to minimize the difference hash-firstHash, where the
        /// difference is interpreted modulo the range of hash values.
        /// IMPORTANT POINT - handle "wrap-around" correctly. Once a
        /// server is selected, p is sent to that server.
        /// </summary>
        /// <param name="packet">is a packet to be forwarded</param>
        /// <param name="sender">the packet's sender address.</param>
        private void Forward(Packet packet, IPEndPoint sender)
        {
            //Initializing the closestServer
            IPEndPoint closestServer = null;

            //Setting the server's sender information.
            packet.SenderInfo = new Pair<IPEndPoint, int>(_address, _hashRange.First);

            //Set proper relay and client address.
            if (packet.ClientAddress == null)
            {
                packet.RelayAddress = _address;
                packet.ClientAddress = sender;
            }

            //Find the closest server
            closestServer = GetClosestServer(HashIt(packet.Key));
            // forward the packet.
            int length;
            _client.Send(packet.Pack(out length), length, closestServer);
        }

        /// <summary>
        /// Adds a packet's key and value if cache is on and the packet is a success packet.
        /// </summary>
        /// <param name="packet">the packet to be checked.</param>
        private void AddToCache(Packet packet)
        {
            if (packet.Type == PacketType.Success && _cacheOn)
            {
                _cache[packet.Key] = packet.Val;
            }
        }

        /// <summary>
        /// Handle a get packet.
        /// </summary>
        /// <param name="p">is the packet with type set to get.</param>
        private void HandleGet(Packet p)
        {
            //If the hashmap contains the key, set type to success and
            //fill in the corresponding value.
            if (_map.ContainsKey(p.Key))
            {
                p.Type = PacketType.Success;
                p.Val = _map[p.Key];
            }
            //otherwise, return no match.
            else
            {
                p.Type = PacketType.No_Match;
            }
        }

        /// <summary>
        /// Handle a put packet.
        /// </summary>
        /// <param name="p">is the packet with type set to put.</param>
        private void HandlePut(Packet p)
        {
            //If the put has no val clear the key.
            if (p.Val == null)
            {
                _map.Remove(p.Key);
            }
            //otherwise set it.
            else
            {
                _map[p.Key] = p.Val;
            }
            p.Type = PacketType.Success; //indicate completion of command
        }

        /// <summary>
        /// Handle a join packet from a prospective DHT node. This function
        /// initializes the out packet as well as halves its hashRange and
        /// sends it to the requesting server, as well as any data that the
        /// new server might now be responsible for.
        /// </summary>
        /// <param name="packet">is the received join packet</param>
        /// <param name="succAdr">
        /// is the socket address of the host that sent
        /// the join packet (the new successor)
        /// </param>
        private void HandleJoin(Packet packet, IPEndPoint succAdr)
        {
            //Clearing the incoming packet, initializingit to success
            //and no hashRange.
            //packet.clear();
            packet.Type = PacketType.Success;
            packet.HashRange = new Pair<int, int>(0, 0);

            //evaluating the appropriate hashRange difference and setting
            //it to the outgoing packet.
            int rangeDifference = _hashRange.Second - _hashRange.First;

            packet.HashRange = new Pair<int, int>(_hashRange.First + (int)(rangeDifference / 2), _hashRange.Second);

            //Setting the new hashRange maximum.
            _hashRange.Second = packet.HashRange.First;

            //setting the successor the output packet.
            packet.SuccInfo = new Pair<IPEndPoint, int>(_succInfo.First, _succInfo.Second);

            //Updating the server's own successor.
            _succInfo = new Pair<IPEndPoint, int>(succAdr, packet.HashRange.First);
            AddRoute(_succInfo);
            //send the packet.
            int length;
            _client.Send(packet.Pack(out length), length, succAdr);
            //transfer any data that the new server is now responsible for.
            TransferData(succAdr);
        }

        /// <summary>
        /// Transfers all the data the newly created server is responsible for.
        /// </summary>
        /// <param name="succAddress">address of the newly created server.</param>
        private void TransferData(IPEndPoint succAddress)
        {
            //If the key,value pair is no longer in its range,
            //send it to the newly created server and remove it
            //from this server's hashmap.
            foreach (var item in _map.Where(m => !IsInRange(m.Key)))
            {
                //Create the outgong packet on which to send the pairs.
                var packet = new Packet();
                packet.Key = item.Key;
                packet.Val = item.Value;
                packet.Type = PacketType.Transfer;

                int length;
                _client.Send(packet.Pack(out length), length, succAddress);
            }
        }

        /// <summary>
        /// Handle a transfer packet.
        /// Simply accept the packet's key value pair into the new server's
        /// hashmap.
        /// </summary>
        /// <param name="packet">is a transfer packet</param>
        public void HandleXfer(Packet packet)
        {
            _map[packet.Key] = packet.Val;
        }

        /// <summary>
        /// Returns the address of the closest server of the given hash.
        /// </summary>
        /// <param name="hash">address of the closest server to the hash.</param>
        /// <returns></returns>
        private IPEndPoint GetClosestServer(int hash)
        {
            //Let the successor be the base case.
            int minimum = Math.Abs(hash - _succInfo.Second);
            int difference;

            //Calculate the difference between each hash and each
            //element in the routing table.
            IPEndPoint closestAddress = _succInfo.First;
            foreach (var element in _routesTable)
            {
                difference = Math.Abs(hash - element.Second);

                //Choose a new min if it is closer to the hash and it does
                //not go beyond it.
                if (difference < minimum)
                {
                    minimum = difference;
                    closestAddress = element.First;
                }
            }
            return closestAddress;
        }

        /// <summary>
        /// Compares the two input socket address and returns true if they
        /// are the same and false otherwise.
        /// </summary>
        /// <param name="a">The  first socket address to compare</param>
        /// <param name="b">The second socket address to compare.</param>
        /// <returns></returns>
        private bool EqualInetSocketAddress(IPEndPoint a, IPEndPoint b)
        {
            if (a == null || b == null) return false;
            return a.Address.Equals(b.Address) && a.Port == b.Port;
        }

        /// <summary>
        /// Hash a string, returning a 32 bit integer.
        /// </summary>
        /// <param name="s">is a string, typically the key from some
        /// get/put operation.
        /// </param>
        /// <returns>and integer hash value in the interval [0,2^31).</returns>
        public int _HashIt(string s)
        {
            while (s.Length < 16)
            {
                s += s;
            }

            byte[] sbytes = System.Text.Encoding.Unicode.GetBytes(s);

            int i = 0;
            int h = 0x37ace45d;
            while (i + 1 < sbytes.Length)
            {
                int x = (sbytes[i] << 8) | sbytes[i + 1];
                h *= x;
                int top = Convert.ToInt32(h & 0xffff0000);
                int bot = h & 0xffff;
                h = top | (bot ^ ((top >> 16) & 0xffff));
                i += 2;
            }

            if (h < 0)
            {
                h = -(h + 1);
            }
            return h;
        }

        /// <summary>
        /// Similar to String.GetHashCode but returns the same as the x86 version of String.GetHashCode for x64 and x86 frameworks.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        //public static unsafe int GetHashCode32(string s)
        public static unsafe int HashIt(string s)
        {
            fixed (char* str = s.ToCharArray())
            {
                char* chPtr = str;
                int num = 0x15051505;
                int num2 = num;
                int* numPtr = (int*)chPtr;
                for (int i = s.Length; i > 0; i -= 4)
                {
                    num = (((num << 5) + num) + (num >> 0x1b)) ^ numPtr[0];
                    if (i <= 2)
                    {
                        break;
                    }
                    num2 = (((num2 << 5) + num2) + (num2 >> 0x1b)) ^ numPtr[1];
                    numPtr += 2;
                }
                return (num + (num2 * 0x5d588b65));
            }
        }

        #endregion
    }
}
