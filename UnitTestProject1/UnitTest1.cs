using System;
using System.Collections;
using DSA.Models;
using NUnit.Framework;

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
    }
}
