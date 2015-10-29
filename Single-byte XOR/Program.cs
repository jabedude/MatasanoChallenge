using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Single_byte_XOR
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter string to be decoded: ");
            string inputHex = Console.ReadLine();

            byte[] inputByteArray = hexToByteArray(inputHex);
            byte keyVal;
            for(keyVal = 0; keyVal < 255; keyVal++)
            {
                xorBytes(inputByteArray, keyVal);
            }
        }

        static byte[] hexToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        static void xorBytes(byte[] _byteArray, byte keyVal)
        {
            List<byte> xorByte = new List<byte>();
            ASCIIEncoding ascii = new ASCIIEncoding();
            for (int i = 0; i < _byteArray.Length; i++)
            {
                xorByte.Add((byte)(_byteArray[i] ^ keyVal));
            }
            byte[] xorBytesArray = xorByte.ToArray();
            string decoded = ascii.GetString(xorBytesArray, 0, xorBytesArray.Length);
            Console.WriteLine(decoded);
            //int eCount = 0;
            //foreach(char e in decoded)
            //{
            //    if (e == 'e')
            //        eCount++;
            //}
            //Console.WriteLine("{0}, {1}", keyVal, eCount);
        }
    }
}
