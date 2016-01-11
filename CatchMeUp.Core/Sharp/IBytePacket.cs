namespace CatchMeUp.Core.Sharp
{
    public interface IBytePacket
    {
        byte[] Pack();
        byte[] Pack(out int length);
    }
}
