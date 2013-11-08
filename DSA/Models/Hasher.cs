using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Runtime.Remoting.Messaging;
using System.Text;
using DSA.Interfaces;

namespace DSA.Models
{
    public class Hasher
    {
        #region IHash Members

        public static string GetHash(string rawText)
        {
            if (string.IsNullOrEmpty(rawText))
                throw new NullReferenceException("rawText is null or empty");
            var binaries = ConvertStringToBinaryString(rawText);
            binaries = AppendPadding(binaries);
            var sb = new StringBuilder();
            for (var j = 0; j < binaries.Length; j += 512)
            {
                var tempBinaries = binaries.Substring(j, 512);
                tempBinaries = PerformRoundFunction(tempBinaries);

                for (var i = 0; i < tempBinaries.Length; i += 8)
                {
                    string eightBits = tempBinaries.Substring(i, 8);
                    sb.AppendFormat("{0:X2}", Convert.ToByte(eightBits, 2));
                }
            }
            return sb.ToString();
        }

        public static string PerformRoundFunction(string binaries)
        {
            // 7^5 four times
            const string hashFlipper = "0010000011010011110000011010011100100000110100111100000110100111";
            var part1 = binaries.Substring(0, 64);
            var part2 = binaries.Substring(64, 64);
            var part3 = binaries.Substring(64 * 2, 64);
            var part4 = binaries.Substring(64 * 3, 64);
            var part5 = binaries.Substring(64 * 4, 64);
            var part6 = binaries.Substring(64 * 5, 64);
            var part7 = binaries.Substring(64 * 6, 64);
            var part8 = binaries.Substring(64 * 7, 64);
            part6 = XORTwoBinaryStrings(part6, part8);
            for (int i = 0; i <= 80; i++)
            {
                part8 = part7;
                part7 = XORTwoBinaryStrings(part6, part1);
                part6 = part5;
                part5 = part4;
                part4 = ShiftOneStep(part3);
                part3 = part2;
                part2 = part1;
                part1 = XORTwoBinaryStrings(part8, hashFlipper);
            }

            return part1 + part2 + part3 + part4 + part5 + part6 + part7 + part8;
        }

        public static string ShiftOneStep(string part)
        {
            var templeft = part[0];
            var sb = new StringBuilder(part.Length);
            for (var i = 1; i < part.Length; i++)
            {
                sb.Append(part[i]);
            }
            sb.Append(templeft);
            return sb.ToString();
        }

        #endregion

        #region IInternalHash Members

        public static BitArray AppendPadding(BitArray unPaddedBitArray)
        {
            var paddedArray = new BitArray(unPaddedBitArray.Length + 1);
            paddedArray[0] = true;
            for (var i = 0; i < unPaddedBitArray.Length; i++)
            {
                paddedArray[i + 1] = unPaddedBitArray.Get(i);
            }
            while (paddedArray.Length % 512 != 0)
            {
                paddedArray = AddZeroPadder(paddedArray);
            }

            return paddedArray;
        }

        private static BitArray AddZeroPadder(BitArray array)
        {
            var newArray = new BitArray(array.Length + 1);
            newArray[0] = true;
            newArray[1] = false;
            for (var i = 2; i < array.Length; i++)
            {
                newArray[i + 1] = array.Get(i);
            }
            return newArray;
        }
        #endregion

        public static string XORTwoBinaryStrings(string leftSide, string rightSide)
        {
            if (leftSide == null) throw new ArgumentNullException("leftSide");
            if (rightSide == null) throw new ArgumentNullException("rightSide");
            if (rightSide.Length != leftSide.Length)
                throw new Exception("Length of left and right side does not match");
            var sb = new StringBuilder();
            for (var i = 0; i < leftSide.Length; i++)
            {
                sb.Append(leftSide[i].Equals(rightSide[i]) ? "0" : "1");
            }
            return sb.ToString();
        }

        public static string ByteArrayToString(byte[] ba)
        {
            // concat the bytes into one long string
            return ba.Aggregate(new StringBuilder(32),
                                    (sb, b) => sb.Append(b.ToString("X2"))
                                    ).ToString();
        }

        public static string ConvertStringToBinaryString(string inputString)
        {
            if (string.IsNullOrEmpty(inputString))
                throw new NullReferenceException("inputString is null or empty");

            var sb = new StringBuilder();
            foreach (var c in inputString)
            {
                sb.Append(Convert.ToString(c, 2));
            }
            return sb.ToString();
        }



        #region IInternalHash Members

        public static string AppendPadding(string unPaddedBinaries)
        {
            unPaddedBinaries = unPaddedBinaries.Insert(0, "1");
            while (unPaddedBinaries.Length % 512 != 0)
            {
                unPaddedBinaries = unPaddedBinaries.Insert(1, "0");
            }
            return unPaddedBinaries;
        }

        #endregion
    }
}
