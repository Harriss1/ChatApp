namespace ChatApp.ChatClient {
    internal class ChatSession {
        public string Username { get; private set; }
        public bool IsLoggedIn { get; set; }

        private ChatSession() { }
        public ChatSession(string username) {
            Username = username;
        }
    }
}