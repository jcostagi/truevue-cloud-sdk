/*******************************************************************************
 * Copyright (c) 2020 Sensormatic Electronics, LLC.  All rights reserved.
 * Reproduction is forbidden without written approval of Sensormatic Electronics, LLC.
 *******************************************************************************/

namespace TrueVUE.Cloud.SDK.Barcode.Models
{
    public abstract class BaseGS1Barcode
    {
        protected int CalculateMod10CheckDigit(string code)
        {
            int oddSum = 0;
            int evenSum = 0;
            int mod;
            // sum up all odd digits
            for (int i = code.Length - 1; i >= 0; i -= 2)
            {
                oddSum += int.Parse(code.Substring(i, 1));
            }

            oddSum *= 3;
            // sum up all even digits
            for (int i = code.Length - 2; i >= 0; i -= 2)
            {
                evenSum += int.Parse(code.Substring(i, 1));
            }

            mod = (oddSum + evenSum) % 10;
            if (mod == 0)
            {
                return 0;
            }

            return 10 - mod;
        }
    }
}
