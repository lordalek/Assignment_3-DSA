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
        public Signature Signature { get; set; }
        public static List<BigInteger> GetPublicKeyCompontents()
        {
            var components = new List<BigInteger>
            {
                PseudoRandomPrimeNumber.GetRandomPrimeNumber(160),
                PseudoRandomPrimeNumber.GetRandomPrimeNumber(1024)
            };
            components.Add(GetG(components[0], components[1]));
            return components;
        }

        private static BigInteger GetG(BigInteger p, BigInteger q)
        {
            var h = new BigInteger(2);
            var g = BigInteger.ModPow(h, (p - 1) / q, p);
            while (g <= 1)
            {
                h++;
                g = BigInteger.ModPow(h, (p - 1) / q, p);
            }
            return BigInteger.ModPow(h, (p - 1) / q, p);
        }

        public static BigInteger GetPrivateKey(BigInteger q)
        {
            return PseudoRandomPrimeNumber.GetRandomPrimeNumber(q);
        }

        public static BigInteger GetMessageSecretKey(BigInteger q)
        {
            return PseudoRandomPrimeNumber.GetRandomPrimeNumber(q);
        }

        public static BigInteger GetPublicKey(BigInteger g, BigInteger privateKey, BigInteger p)
        {
            return BigInteger.ModPow(g, privateKey, p);
        }

        public void SignMessage(BigInteger g, BigInteger k, BigInteger p, BigInteger q, string message,
            BigInteger privateKey)
        {
            Signature = Signature.Sign(g, k, p, q, message, privateKey);
        }

        public bool VerifySignature(Signature signature, BigInteger q, BigInteger p, BigInteger g, BigInteger publicKey, string message)
        {
            var w = BigInteger.ModPow(signature.GetS(), -1, q);
            var u1 = -1;
            int.TryParse((BigInteger.Parse(Hasher.GetHash(message)) * w % q).ToString(), out u1);
            var u2 = -1;
            var tempu2 = signature.GetR() * w % q;
            int.TryParse(tempu2.ToString(), out u2);
            var v = ((BigInteger.Pow(g, u1) * BigInteger.Pow(publicKey, u2)) % p) % q;
            return v == signature.GetR();
        }
    }
}
