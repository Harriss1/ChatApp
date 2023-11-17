using ChatApp.Server.ConnectionManager;

namespace ChatApp.Server.MessageMediator {
    internal class ByteMessage {
        public byte[] Data { get; }
        public int Length { get; }
        public Connection connection { get; set; }
    public ByteMessage(byte[] data, int length, Connection connection) {
            this.connection = connection;
            this.Data = data;
            this.Length = length;
        }
    }
}