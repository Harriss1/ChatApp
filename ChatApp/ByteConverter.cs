using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp {
    internal class ByteConverter {
        public static string ToString(byte[] bytes, int length) {
            return Encoding.ASCII.GetString(bytes, 0, length);
        }
        public static byte[] ToByteArray(string message) {
            return Encoding.ASCII.GetBytes(message);
        }
    }
}
