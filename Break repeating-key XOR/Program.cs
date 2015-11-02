using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Break_repeating_key_XOR
{
    class Program
    {
        static void Main(string[] args)
        {
            DetermineProbableKeySize();

        }

        static void DetermineProbableKeySize()
        {
            int KEYSIZE;
            Dictionary<int, int> KEYSIZE_DICT = new Dictionary<int, int>();
            /*
            HammingDistance("this is a test", "wokka wokka!!!");
            */
            //Second Test
            string contents = File.ReadAllText(@"C:\Users\jabedude\Documents\Visual Studio 2015\Projects\MatasanoChallenge\Break repeating-key XOR\6.txt");
            byte[] cryptoBytes = Base64Decode(contents);

            for (KEYSIZE = 2; KEYSIZE <= 40; KEYSIZE++)
            {
                try
                {
                    byte[] subCryptOne = cryptoBytes.Take(KEYSIZE).ToArray();
                    byte[] subCryptTwo = cryptoBytes.Skip(KEYSIZE).Take(KEYSIZE).ToArray();
                    KEYSIZE_DICT.Add(KEYSIZE,
                                    (HammingDistance(subCryptOne, subCryptTwo)));
                }
                catch (Exception e) { }
            }

            foreach (int key in KEYSIZE_DICT.Keys)
                Console.WriteLine("{0}, {1}", key, KEYSIZE_DICT[key]);
        }

        static int HammingDistance(string _first, string _second)
        {
            int bitCount = 0;
            int[] LookupTable =
                    Enumerable.Range(0, 256)
                              .Select(CountBits)
                              .ToArray();

            byte[] first = Encoding.ASCII.GetBytes(_first);
            byte[] second = Encoding.ASCII.GetBytes(_second);
            byte[] differentBytes = new byte[first.Length];
            for (int i = 0; i < differentBytes.Length; i++)
                differentBytes[i] = (byte)(first[i] ^ second[i]); //Swiggity Swooty. Took me long enough.

            foreach (byte b in differentBytes)
                bitCount += LookupTable[b];
            return bitCount;
        }

        static int HammingDistance(byte[] array1, byte[] array2)
        {
            string _first = Encoding.ASCII.GetString(array1);
            string _second = Encoding.ASCII.GetString(array2);

            int bitCount = 0;
            int[] LookupTable =
                    Enumerable.Range(0, 256)
                              .Select(CountBits)
                              .ToArray();

            byte[] first = Encoding.ASCII.GetBytes(_first);
            byte[] second = Encoding.ASCII.GetBytes(_second);
            byte[] differentBytes = new byte[first.Length];
            for (int i = 0; i < differentBytes.Length; i++)
                differentBytes[i] = (byte)(first[i] ^ second[i]); //Swiggity Swooty. Took me long enough.

            foreach (byte b in differentBytes)
                bitCount += LookupTable[b];
            //Console.WriteLine(bitCount);
            return bitCount;
        }

        static int CountBits(int value)
        {
            int count = 0;
            for (int i = 0; i < 8; i++)
            {
                count += (value >> i) & 1;
            }
            return count;
        }

        static byte[] Base64Decode(string cryptoText)
        {
            return Convert.FromBase64String(cryptoText);
            //string decodedString = Encoding.ASCII.GetString(data); <-for changing byte[] to string
        }

    }
}
