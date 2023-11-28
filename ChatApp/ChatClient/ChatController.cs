using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.ChatClient {
    internal class ChatController {
        TcpClient client = new TcpClient();
        private string lastReceivedMessage = "";
        public void LoginToServer(string username, string ipAddress) {

            client.Connect(ipAddress, Config.ServerPort);
        }

        internal void SendMessage(string text) {
            lastReceivedMessage = client.Send(text);
        }

        internal string GetLastReceivedMessage() {
            return lastReceivedMessage;
        }
    }
}
