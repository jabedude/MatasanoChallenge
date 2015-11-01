using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace repeating_key_XOR
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter string to be encoded: ");
            string inputString = Console.ReadLine();
            //string inputString = "Burning 'em, if you ain't quick and nimble\nI go crazy when I hear a cymbal";
            byte[] inputBytes = asciiToByteArray(inputString);
            xorBytes(inputBytes, 73);
        }

        static byte[] hexToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        static byte[] asciiToByteArray(string ascii)
        {
            return Encoding.ASCII.GetBytes(ascii);
        }

        static void xorBytes(byte[] _byteArray, byte keyVal)
        {
            List<byte> xorByte = new List<byte>();
            byte[] cryptBytes = new byte[_byteArray.Length];
            
            for(int i = 0; i < _byteArray.Length; i += 3)
                cryptBytes[i] = (byte)(_byteArray[i] ^ 73);
            
            for(int i =  1; i < _byteArray.Length; i += 3)
                cryptBytes[i] = (byte)(_byteArray[i] ^ 67);

            for (int i = 2; i < _byteArray.Length; i += 3)
                cryptBytes[i] = (byte)(_byteArray[i] ^ 69);

            byte[] xorBytesArray = cryptBytes.ToArray();
            string decoded = HexFromBytes(xorBytesArray);
            Console.WriteLine(decoded);
        }

        static string HexFromBytes(byte[] bytes)
        {
            return BitConverter.ToString(bytes).Replace("-", "");
        }
    }
}
