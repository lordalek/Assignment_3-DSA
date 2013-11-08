using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;

namespace DSA.Models
{
    public class Signature
    {
        private BigInteger _r;
        private BigInteger _s;

        public BigInteger GetR()
        {
            return _r;
        }

        public BigInteger GetS()
        {
            return _s;
        }

        private void SetR(BigInteger g, BigInteger k, BigInteger p, BigInteger q)
        {
            _r = (BigInteger.ModPow(g, k, p) % q);
        }

        private void SetS(BigInteger g, BigInteger k, BigInteger p, BigInteger q, string message,
            BigInteger privateKey)
        {
            _s = (BigInteger.Pow(g, -1) * (BigInteger.Parse(Hasher.GetHash(message), NumberStyles.AllowHexSpecifier))) % q;
        }

        public Signature Sign(BigInteger g, BigInteger k, BigInteger p, BigInteger q, string message,
            BigInteger privateKey)
        {
            SetR(g, k, p, q);
            SetS(g, k, p, q, message, privateKey);
            return this;
        }
    }
}
