using System;

namespace Temiang.Avicenna.Common
{
    public class Convertion
    {
        /// <summary>
        /// Return words for this value between 1 and 999.
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        private string Words_1_999(int num)
        {
            var result = string.Empty;

            var hundreds = num / 100;
            var remainder = num - hundreds * 100;

            if (hundreds > 0)
            {
                if (hundreds == 1)
                    result = "seratus ";
                else
                    result = Words_1_19(hundreds) + " ratus ";
            }

            if (remainder > 0)
                result = result + Words_1_99(remainder);

            string returnValue = result.Trim();
            return returnValue;
        }

        /// <summary>
        /// Return a word for this value between 1 and 99.
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        private string Words_1_99(int num)
        {
            var result = string.Empty;

            var tens = num / 10;

            if (tens <= 1)
                // 1 <= num <= 19
                result = result + " " + Words_1_19(num);
            else
            {
                // 20 <= num
                // Get the tens digit word.
                switch (tens)
                {
                    case 2:
                        result = "dua puluh";
                        break;
                    case 3:
                        result = "tiga puluh";
                        break;
                    case 4:
                        result = "empat puluh";
                        break;
                    case 5:

                        result = "lima puluh";
                        break;
                    case 6:

                        result = "enam puluh";
                        break;
                    case 7:

                        result = "tujuh puluh";
                        break;
                    case 8:
                        result = "delapan puluh";
                        break;
                    case 9:
                        result = "sembilan puluh";
                        break;
                }

                // Add the ones digit number.
                result = result + " " + Words_1_19(num - tens * 10);
            }

            var returnValue = result.Trim();
            return returnValue;
        }

        /// <summary>
        /// Return a word for this value between 1 to 19
        /// </summary>
        /// <param name="number">
        /// number (start from 1 to 19)
        /// </param>
        /// <returns></returns>
        private string Words_1_19(int number)
        {
            switch (number)
            {
                case 1:
                    return "satu";
                case 2:
                    return "dua";
                case 3:
                    return "tiga";
                case 4:
                    return "empat";
                case 5:
                    return "lima";
                case 6:
                    return "enam";
                case 7:
                    return "tujuh";
                case 8:
                    return "delapan";
                case 9:
                    return "sembilan";
                case 10:
                    return "sepuluh";
                case 11:
                    return "sebelas";
                case 12:
                    return "dua belas";
                case 13:
                    return "tiga belas";
                case 14:
                    return "empat belas";
                case 15:
                    return "lima belas";
                case 16:
                    return "enam belas";
                case 17:
                    return "tujuh belas";
                case 18:
                    return "delapan belas";
                case 19:
                    return "sembilan belas";
            }
            return string.Empty;
        }

        /// <summary>
        /// Return a string of words to represent the integer part of this value.
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        private string Words_1_all(decimal num)
        {
            if (num == 0)
                return "Nol";

            var power_value = new decimal[6];
            var power_name = new string[6];
            var result = string.Empty;
            int i;

            // Initialize the power names and values.
            power_name[1] = "triliun";

            power_value[1] = 1000000000000.0M;
            power_name[2] = "milyar";

            power_value[2] = 1000000000;
            power_name[3] = "juta";

            power_value[3] = 1000000;
            power_name[4] = "ribu";

            power_value[4] = 1000;
            power_name[5] = "";

            power_value[5] = 1;

            for (i = 1; i <= 5; i++)
            {
                // See if we have digits in this range.
                if (num >= power_value[i])
                {
                    // Get the digits.
                    var digits = (int)(num / power_value[i]);

                    // Add the digits to the result.
                    if (result.Length > 0)
                        result = result + " ";

                    result = result + Words_1_999(digits) + " " + power_name[i];

                    // Get the number without these digits.
                    num = num - digits * power_value[i];
                }
            }

            var koma = Convert.ToDecimal(num.ToString().Substring(num.ToString().Length - 2, 2));
            if (koma > 0)
            {
                result += " koma";
                for (i = 1; i <= 5; i++)
                {
                    // See if we have digits in this range.
                    if (koma >= power_value[i])
                    {
                        // Get the digits.
                        var digits = (int)(koma / power_value[i]);

                        // Add the digits to the result.
                        if (result.Length > 0)
                            result = result + " ";

                        result = result + Words_1_99(digits) + " " + power_name[i];

                        // Get the number without these digits.
                        koma = koma - digits * power_value[i];
                    }
                }
            }

            var returnValue = result.Trim();
            return returnValue;
        }

        /// <summary>
        /// Return a string of words to represent this currency value in dollars and cents.
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public string NumericToWords(decimal num)
        {
            // Dollars.
            var dollars = Math.Abs(num);
            var dollars_result = Words_1_all(dollars);
            dollars_result = dollars_result + " rupiah";

            // Combine the results.
            var returnValue = dollars_result;
            return returnValue.Substring(0, 1).ToUpper() + returnValue.Substring(1);
        }

        public string NumericToWordsWithoutCurrency(int num)
        {
            if (num == 0) return string.Empty;
            // Dollars.
            var dollars = Math.Abs(num);
            var dollars_result = Words_1_999(dollars);

            // Combine the results.
            var returnValue = dollars_result;
            return returnValue.Substring(0, 1).ToUpper() + returnValue.Substring(1);
        }
    }
}