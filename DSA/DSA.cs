using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using DSA.Models;

namespace DSA
{
    public class DSA
    {
        public static List<BigInteger> GetPublicKeyCompontents()
        {
            var components = new List<BigInteger>();
            components.Add(PseudoRandomPrimeNumber.GetRandomPrimeNumber(160));
            components.Add(PseudoRandomPrimeNumber.GetRandomPrimeNumber(1024));
            components.Add(GetG(components[0], components[1]));
            return components;
        }

        private static BigInteger GetG(BigInteger p, BigInteger q)
        {
            var g = new BigInteger();
            var h = new BigInteger(2);
            while (BigInteger.ModPow(h, (p - 1) / q, p) > 1)
            {
                h++;
            }
            return g;
        }

        public static BigInteger GetMessageSecretKey(BigInteger p)
        {
            return PseudoRandomPrimeNumber.GetRandomPrimeNumber(p);
        }

        public static BigInteger GetPublicKey(BigInteger g, BigInteger privateKey, BigInteger p)
        {
            return BigInteger.ModPow(g, privateKey, p);
        }

        //public static string Sign(BigInteger g, BigInteger k, BigInteger p, BigInteger q, string message,
        //    BigInteger privateKey)
        //{
        //    var r = (BigInteger.ModPow(g, k, p)%q);

        //}
    }
}
