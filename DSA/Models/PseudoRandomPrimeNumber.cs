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
            while (!IsPrime(prime) &&  byteLength != bitLength / 8)
            {
                //prime = prime += (16807 * (seed + 13)) % 2147483647;
                var flow = (16807 * prev) % 2147483647;
                prime += flow;
                prev = prime;
                byteLength = prime.ToByteArray().Length;
            }
            return prime;
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
            while (!PseudoRandomPrimeNumber.IsPrime(privateKey))
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
