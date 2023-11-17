using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Server.ConnectionManager {
    internal class ConnectionRegister {
        public List<Connection> connections = new List<Connection>();
        public void Add(Connection connection) {
            connections.Add(connection);
        }
        public void Remove(Connection connection) {
            if (connections.Contains(connection)) {
                connections.Remove(connection);
            }
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
