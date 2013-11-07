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
            var shizzle = new Hasher();
            var padded = shizzle.AppendPadding(new BitArray(20));
            Assert.IsTrue(padded.Length == 512);
        }

        [Test]
        public void getHash()
        {
            var shizzle = new Hasher();
            var hash = shizzle.GetHash("abc123");
            Assert.IsTrue(!string.IsNullOrEmpty(hash));
        }

        [Test]
        public void CheckThatTwoSimilarTextsHashRnotEqual()
        {
            var shizzle = new Hasher();
            var hash = shizzle.GetHash("abc123");
            var hash2 = shizzle.GetHash("abc124");
            Assert.AreNotEqual(hash, hash2);
        }

        [Test]
        public void Shift_1011_Expect_0111()
        {
            var shizzle = new Hasher();
            var shifted = shizzle.ShiftOneStep("1011");
            Assert.AreEqual("0111", shifted);
        }
    }
}
