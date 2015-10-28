using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XOR
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter text to XOR: ");
            string hex = Console.ReadLine();
            string hex2 = Console.ReadLine();
            byte[] by = hexToByteArray(hex);
            byte[] by2 = hexToByteArray(hex2);
            xorBytes(by, by2);
            //foreach (byte b in by)
            //{
            //    Console.Write(b + " ");
            //}
        }

        static byte[] hexToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        static void xorBytes(byte[] _byteArray, byte[] _byteArray2) 
        {
            List<byte> xorByte = new List<byte>();

            for(int i = 0; i < _byteArray.Length; i++)
            {
                xorByte.Add((byte)(_byteArray[i] ^ _byteArray2[i]));
            }
            byte[] xorBytesArray = xorByte.ToArray();
            Console.Write(BitConverter.ToString(xorBytesArray)
                                      .Replace("-", ""));
        }
    }
}
