using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Server.ConnectionManager {
    internal class ConnectionRegister {
        private static ConnectionRegister instance;
        public List<Connection> connections = new List<Connection>();
        private ConnectionRegister() {
        }
        public void Add(Connection connection) {
            connections.Add(connection);
        }

        internal static ConnectionRegister GetInstance() {
            object _lock = new object();
            lock (_lock) {
                if (instance == null) {
                    instance = new ConnectionRegister();
                }
            }
            return instance;
        }

        public void Remove(Connection connection) {
            if (connections.Contains(connection)) {
                connections.Remove(connection);
            }
        }

        internal Connection SearchByUsername(string username) {
            foreach (Connection connection in connections) {
                if (connection.Client.Name.Equals(username)) {
                    return connection;
                }
            }
            return null;
        }

        internal Connection FindConnectionByThread(int managedThreadId) {
            foreach (Connection connection in connections) {
                if (connection.Thread.ManagedThreadId == managedThreadId) {
                    return connection;
                }
            }
            throw new InvalidOperationException("ConnectionRegister:FindConnectionByThread() Thread existiert nicht.");
        }
    }
}
