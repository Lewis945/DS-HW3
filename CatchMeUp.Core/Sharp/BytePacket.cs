using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CatchMeUp.Core.Sharp
{
    [Serializable]
    public abstract class BytePacket<T> : IBytePacket
        where T : IBytePacket
    {
        // Convert an object to a byte array
        public byte[] Pack(out int length)
        {
            var bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, this);
                var array = ms.ToArray();
                length = array.Length;
                return array;
            }
        }

        public byte[] Pack()
        {
            int length;
            return Pack(out length);
        }

        public static T UnPack(byte[] arrBytes)
        {
            using (var ms = new MemoryStream())
            {
                var bf = new BinaryFormatter();
                ms.Write(arrBytes, 0, arrBytes.Length);
                ms.Seek(0, SeekOrigin.Begin);
                var packet = (T)bf.Deserialize(ms);
                return packet;
            }
        }
    }
}
