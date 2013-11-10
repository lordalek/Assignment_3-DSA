using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using DSA.Models;

namespace DSA
{
    public class DSA
    {
        public Signature Signature { get; set; }
        public static void GetPublicKeyCompontents(out BigInteger p, out BigInteger q, out BigInteger k, out BigInteger g)
        {
            q = PseudoRandomPrimeNumber.GetRandomPrimeNumber(160);
            GetP(q, PseudoRandomPrimeNumber.GetRandomEvenNumber(q), out k, out p);
            g = (GetG(p, q));
        }

        //#TODO
        //        Choose q first, where q is a random integer in the range 2^159 to 2^160; if q is not prime, try again. Choose k as a random even integer in the range 2^1023/q to 2^1024/q. Multiply q by k and add 1 to compute p, then test if p is prime; if not, try a different k

        //private static BigInteger GetPrimeDivisor(BigInteger p)
        //{
        //    var q = 
        //    while ((p - 1) % q != 0)
        //    {
        //        q = PseudoRandomPrimeNumber.GetRandomPrimeNumber(160);
        //    }
        //    return q;
        //}

        public static void GetP(BigInteger q, BigInteger k, out BigInteger newK, out BigInteger p)
        {
            //var k = PseudoRandomPrimeNumber.GetRandomEvenNumber(q);
            p = q * k;
            newK = k;
            while (!PseudoRandomPrimeNumber.IsPrime(p + 1))
            {
                k = PseudoRandomPrimeNumber.GetRandomEvenNumber(q);
                newK = k;
                p = q * k;
            }
        }

        private static BigInteger GetG(BigInteger p, BigInteger q)
        {
            //var h = new BigInteger(2);
            //BigInteger g = BigInteger.ModPow(h, (p - 1) / q, p);
            //while (g <= 1)
            //{
            //    h++;
            //    g = BigInteger.ModPow(h, (p - 1) / q, p);
            //}
            //return BigInteger.ModPow(h, (p - 1) / q, p);
            var h = new BigInteger(2);
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
            //var w = BigInteger.ModPow(signature.GetS(), -1, q);
            //var w = (1 / signature.GetS()) % q;
            var w = BigInteger.ModPow(signature.GetS(), p - 2, q);
            //var u1 = -1;
            var hashValue = BigInteger.Parse(Hasher.GetHash(message), NumberStyles.AllowHexSpecifier);
            var u1 = (hashValue * w);
            var u3t = u1 % q;
            //int u1 = int.Parse(tm_u1.ToString());//, out u1);
            var u2 = -1;
            var tempu2 = (signature.GetR() * w) % q;
            int.TryParse(tempu2.ToString(), out u2);
            //var v_temp1 = BigInteger.Pow(g, u1);
            var v_temp1 = power2(g, u3t);
            var v_temp2 = BigInteger.Pow(publicKey, u2);
            var v = ((v_temp1 * v_temp2) % p) % q;
            return v == signature.GetR();
        }

        public bool VerifySignature2(Signature signature, BigInteger q, BigInteger p, BigInteger g, BigInteger publicKey,
            string message)
        {
            var hashValue = BigInteger.Parse(Hasher.GetHash(message), NumberStyles.AllowHexSpecifier);
            var w = BigInteger.ModPow(signature.GetS(), q - 2, q);
            var u1 = (hashValue * w) % q;
            var u2 = (signature.GetR() * w) % q;

            var v = ((BigInteger.ModPow(g, u1, p) *
                      BigInteger.ModPow(publicKey, u2, p) % p) % q);
            return signature.GetR() == v;
        }

        private BigInteger power(BigInteger a, BigInteger b)
        {
            for (int i = 0; i < b; i++)
            {
                a = a * b;
            }
            return a;
        }

        private BigInteger power2(BigInteger value, BigInteger exponent)
        {
            var tempExponent = 100000000;
            for (int i = 0; i < exponent; i++)
            {
                if (i + tempExponent < exponent)
                {
                    value = BigInteger.Pow(value, tempExponent);
                    i += tempExponent;
                }
                else
                {
                    value = value * value;
                }
            }
            return value;
        }
    }
}
