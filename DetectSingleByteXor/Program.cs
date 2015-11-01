using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace DetectSingleByteXor
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> possibleStrings = new List<string>();
            string file = @"C:\Users\jabedude\Documents\Visual Studio 2015\Projects\MatasanoChallenge\DetectSingleByteXor\4.txt";
            List<byte[]> byteList = new List<byte[]>();
            byte keyVal;

            using (StreamReader r = new StreamReader(file))
            {
                string line;
                while ((line = r.ReadLine()) != null)
                {
                    possibleStrings.Add(line);
                }

                foreach (string s in possibleStrings)
                {
                    byteList.Add(hexToByteArray(s));
                }
                foreach (byte[] b in byteList)
                {
                    for (keyVal = 0; keyVal < 255; keyVal++)
                    {
                        xorBytes(b, keyVal);
                    }
                }          
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
            //Above this line decodes strings

            //Console.WriteLine(decoded);

            //Checks if decoded string has common chars
            int commonCount = 0;
            foreach (char e in decoded)
            {
                if (e == 'e' || e == 't' || e == 'a')
                    commonCount++;
            }
            if (commonCount > 5)
                Console.WriteLine(decoded);
        }
    }
}
