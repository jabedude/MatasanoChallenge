using System;

namespace CryptoMethods
{
    public class CryptoMethods
    {
        public void Class1()
        {

        }

        public static void HammingDistance(string _first, string _second)
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
            Console.WriteLine(bitCount);
        }

        public static int CountBits(int value)
        {
            int count = 0;
            for (int i = 0; i < 8; i++)
            {
                count += (value >> i) & 1;
            }
            return count;
        }
    }
}
