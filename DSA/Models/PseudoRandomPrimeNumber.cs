using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace DSA.Models
{
    public static class PseudoRandomPrimeNumber
    {
        public static BigInteger GetRandomPrimeNumber(int bitLength)
        {
            var seed = DateTime.Now.Millisecond + 1;
            var prime = new BigInteger((16807 * seed) % 2147483647);
            var prev = prime;
            var byteLength = prime.ToByteArray().Length;
            while (!IsPrime(prime) && byteLength != bitLength / 8)
            {
                if (byteLength > bitLength / 8)
                    prime = (16807 * (seed + 13)) % 2147483647;
                var flow = (16807 * prev) % 2147483647;
                prime += flow;
                prev = prime;
                byteLength = prime.ToByteArray().Length;
            }
            return prime;
        }

        public static BigInteger GetRandomPrimeNumber2(int bitLength)
        {
            var seed = DateTime.Now.Millisecond + 1;
            var prime = new BigInteger((16807 * seed) % 2147483647);
            var prev = prime;
            var byteLength = prime.ToByteArray().Length;
            var bitCount = IteratedBitcount(prime);
            while (!IsPrime(prime) && bitCount != bitLength)
            {
                if (bitCount > bitLength)
                    prime = (16807 * (seed + 13)) % 2147483647;
                var flow = (16807 * prev) % 2147483647;
                prime += flow;
                prev = prime;
                bitCount = IteratedBitcount(prime);
            }
            return prime;
        }

        public static int IteratedBitcount(BigInteger n)
        {
            var test = n;
            var count = 0;

            while (test != 0)
            {
                if ((test & 1) == 1)// || (test & 0) == 0)
                {
                    count++;
                }
                test >>= 1;
            }
            return count;
        }

        public static BigInteger GetRandomEvenNumber(BigInteger p)
        {
            var seed = DateTime.Now.Millisecond + 1;
            var k = new BigInteger((16807 * seed) % 2147483647);
            var byteLength = k.ToByteArray().Length;
            var prev = k;
            var bitCount = IteratedBitcount(k);
            var bitLength = IteratedBitcount(p);
            while (!k.IsEven && bitCount < bitLength)
            {
                if (bitCount > bitLength / 8)
                    k = (16807 * (seed + 13)) % 2147483647;
                var flow = (16807 * prev) % 2147483647;
                k += flow;
                prev = k;
                bitCount = IteratedBitcount(k);
            }
            return k;
        }

        public static BigInteger GetRandomEvenNumber2(BigInteger p)
        {
            var seed = DateTime.Now.Millisecond + 1;
            var k = new BigInteger((16807 * seed) % 2147483647);
            var byteLength = k.ToByteArray().Length;
            var prev = k;
            var bitLength = (BigInteger.Pow(2, 1024) / p).ToByteArray().Length;
            while (!k.IsEven && byteLength != bitLength / 8)
            {
                if (byteLength > bitLength / 8)
                    k = (16807 * (seed + 13)) % 2147483647;
                var flow = (16807 * prev) % 2147483647;
                k += flow;
                prev = k;
                byteLength = k.ToByteArray().Length;
            }
            return k;
        }

        public static bool IsPrime(BigInteger maybePrimeNumber)
        {
            if (maybePrimeNumber % 2 == 0)
                return false;
            for (BigInteger i = 3; (i * i) < maybePrimeNumber; i += 2)
            {
                if (maybePrimeNumber % i == 0)
                    return false;
            }
            return true;
        }

        public static BigInteger GetRandomPrimeNumber(BigInteger topLimit)
        {
            var seed = DateTime.Now.Millisecond + 1;
            var privateKey = new BigInteger((16807 * seed) % 2147483647);
            var prev = privateKey;
            while (!IsPrime(privateKey))
            {
                if (privateKey > topLimit)
                {
                    seed = DateTime.Now.Millisecond + 1;
                    privateKey = new BigInteger((16807 * seed) % 2147483647);
                }
                var flow = (16807 * prev) % 2147483647;
                privateKey += flow;
                prev = privateKey;
            }
            return privateKey;
        }
    }
}
