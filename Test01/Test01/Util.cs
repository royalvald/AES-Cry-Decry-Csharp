using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Test01
{
    class Util
    {
        public const int PRIME_CERTAINTY = 5;

        public static string toHexString(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder(bytes.Length * 3);
            foreach (byte b in bytes)
            {
                sb.Append(Convert.ToString(b, 16).PadLeft(2, '0').PadRight(3, ' '));
            }

            return sb.ToString().ToUpper();
        }

        //public static BigInteger generateLargePrime(int bitLength)
        //{

        //}

        public BigInteger StringToBigInteger(string s)
        {
            byte[] stringBytes = Encoding.UTF8.GetBytes(s);
            BigInteger integer = new BigInteger(stringBytes);
            return integer;
        }

        public string IntegerToBiginteger(BigInteger integer)
        {
            byte[] integerBytes = integer.ToByteArray();
            string info = Encoding.UTF8.GetString(integerBytes);
            return info;
        }
    }
}
