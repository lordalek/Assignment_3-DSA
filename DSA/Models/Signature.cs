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
            var hashAsHex = Hasher.GetHash(message);
            //parse hex
            var temp1 = (BigInteger.Parse(hashAsHex, NumberStyles.HexNumber));
            //add privatekey * _r
            temp1 += privateKey * _r;
            _s = (temp1 / g) % q;
        }

        public Signature Sign(BigInteger g, BigInteger messageSecretKey, BigInteger p, BigInteger q, string message,
            BigInteger privateKey)
        {
            SetR(g, messageSecretKey, p, q);
            SetS(g, messageSecretKey, p, q, message, privateKey);
            return this;
        }
    }
}
