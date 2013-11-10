using System;
using System.Collections;
using System.Numerics;
using DSA.Models;
using NUnit.Framework;
using NUnit.Util;

namespace UnitTestProject1
{
    [TestFixture]
    public class UnitTest1
    {
        [Test]
        public void Insert_20_lenth_expect_512_padded()
        {
            var padded = Hasher.AppendPadding(new BitArray(20));
            Assert.IsTrue(padded.Length == 512);
        }

        [Test]
        public void getHash()
        {
            var hash = Hasher.GetHash("Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.");
            Assert.IsTrue(!string.IsNullOrEmpty(hash));
        }

        [Test]
        public void CheckThatTwoSimilarTextsHashRnotEqual()
        {
            var hash = Hasher.GetHash("abc123");
            var hash2 = Hasher.GetHash("abc124");
            Assert.AreNotEqual(hash, hash2);
        }

        [Test]
        public void Shift_1011_Expect_0111()
        {
            var shifted = Hasher.ShiftOneStep("1011");
            Assert.AreEqual("0111", shifted);
        }

        [Test]
        public void GetPrimeNumber()
        {
            var randomNum = PseudoRandomPrimeNumber.GetRandomPrimeNumber(512);
            Assert.IsTrue(PseudoRandomPrimeNumber.IsPrime(randomNum));
        }

        [Test]
        public void CheckIfSignatureIsGenerated()
        {
            var g = new BigInteger(4);
            var p = new BigInteger(58400248469);
            var q = new BigInteger(29151201623);
            var privateKey = new BigInteger(14692259843);
            var messagePrivateKey = new BigInteger(15788060033);
            var publicKey = DSA.DSA.GetPublicKey(g, privateKey, p);
            var message = "Hello";
            var signature = new Signature().Sign(g, messagePrivateKey, p, q, message, privateKey);
            var v = new DSA.DSA().VerifySignature2(signature, q, p, g, publicKey, message);
            Assert.IsTrue(v);
        }

        [Test]
        public void VerifyNewmessage()
        {
            BigInteger q, g, p, k;
            DSA.DSA.GetPublicKeyCompontents(out p, out q, out k, out g);
            var privateKey = DSA.DSA.GetPrivateKey(q);
            var publicKey = DSA.DSA.GetPublicKey(g, privateKey, p);
            
            var message = "abc";
            var signature = new Signature().Sign(g, k, p, q, message, privateKey);
            Assert.IsTrue(new DSA.DSA().VerifySignature2(signature, q, p, g, publicKey, message));
        }

        [Test]
        public void getEvenNumber()
        {
            var even = PseudoRandomPrimeNumber.GetRandomEvenNumber(PseudoRandomPrimeNumber.GetRandomPrimeNumber(160));
            Assert.IsTrue(even.IsEven);
        }

        //[Test]
        //public void getP()
        //{
        //    var q = PseudoRandomPrimeNumber.GetRandomPrimeNumber(160);
        //    var p = DSA.DSA.GetP(q);
        //    Assert.IsTrue(PseudoRandomPrimeNumber.IsPrime(p));
        //}
    }
}
