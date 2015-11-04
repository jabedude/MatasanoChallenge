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
            //DetermineProbableKeySize();
            BreakCipherText(5);
        }

        static void BreakCipherText(int KEYSIZE)
        {
            string contents = File.ReadAllText(@"C:\Users\jabedude\Documents\Visual Studio 2015\Projects\MatasanoChallenge\Break repeating-key XOR\6.txt");
            byte[] cryptoBytes = Base64Decode(contents);
            
            byte[] firstArray = new byte[cryptoBytes.Length / 5];
            byte[] secondArray = new byte[cryptoBytes.Length / 5];
            byte[] thirdArray = new byte[cryptoBytes.Length / 5];
            byte[] fourthArray = new byte[cryptoBytes.Length / 5];
            byte[] fifthArray = new byte[cryptoBytes.Length / 5];

            for (int i = 0; i < cryptoBytes.Length; i += KEYSIZE)
            {
                //assuming keysize = 5, I'm creating 5 byte lists (one for each value in the Repeating-Key).
                //I don't know how/too lazy to programatically generate the number of lists needed 
                firstArray = cryptoBytes.Skip(i).Take(1).ToArray();
                firstArray = cryptoBytes.Skip(i+1).Take(1).ToArray();
                firstArray = cryptoBytes.Skip(i + 2).Take(1).ToArray();
                firstArray = cryptoBytes.Skip(i + 3).Take(1).ToArray();
                firstArray = cryptoBytes.Skip(i + 4).Take(1).ToArray();
            }
            
            List<byte[]> arrayList = new List<byte[]> { firstArray, secondArray,
                                                      thirdArray, fourthArray, fifthArray };
            
            foreach (byte[] b in arrayList)
            {
                for (byte keyVal = 0; keyVal < 255; keyVal++)
                {
                    xorBytes(b, keyVal);
                }
            }

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
            File.AppendAllText(@"C:\Users\jabedude\Desktop\HERE.txt", decoded + "\n");
            //Above this line decodes strings

            //Console.WriteLine(decoded);

            //Checks if decoded string has common chars
            int commonCount = 0;
            foreach (char e in decoded)
            {
                if (e == 'e' || e == 't' || e == 'a')
                    commonCount++;
            }
            if (commonCount > 10) //hardcoded cause I'm lazy
                Console.WriteLine(keyVal);
        }

    }
}
