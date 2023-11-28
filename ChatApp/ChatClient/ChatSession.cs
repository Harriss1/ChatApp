namespace ChatApp.ChatClient {
    internal class ChatSession {
        public string Username { get; private set; }

        private ChatSession() { }
        public ChatSession(string username) {
            Username = username;
        }
    }
}