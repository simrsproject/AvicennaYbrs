using System;

namespace Temiang.Avicenna.Common
{
    public class ConvertionToEnglish
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
                result = Words_1_19(hundreds) + " hundred ";
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
                        result = "twenty";
                        break;
                    case 3:
                        result = "thirty";
                        break;
                    case 4:
                        result = "forty";
                        break;
                    case 5:

                        result = "fifty";
                        break;
                    case 6:

                        result = "sixty";
                        break;
                    case 7:

                        result = "seventy";
                        break;
                    case 8:
                        result = "eighty";
                        break;
                    case 9:
                        result = "ninety";
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
                    return "one";
                case 2:
                    return "two";
                case 3:
                    return "three";
                case 4:
                    return "four";
                case 5:
                    return "five";
                case 6:
                    return "six";
                case 7:
                    return "seven";
                case 8:
                    return "eight";
                case 9:
                    return "nine";
                case 10:
                    return "ten";
                case 11:
                    return "eleven";
                case 12:
                    return "twelve";
                case 13:
                    return "thirteen";
                case 14:
                    return "fourteen";
                case 15:
                    return "fifteen";
                case 16:
                    return "sixteen";
                case 17:
                    return "seventeen";
                case 18:
                    return "eighteen";
                case 19:
                    return "nineteen";
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
            var power_value = new decimal[6];
            var power_name = new string[6];
            var result = string.Empty;
            int i;

            // Initialize the power names and values.
            power_name[1] = "trillion";

            power_value[1] = 1000000000000.0M;
            power_name[2] = "billion";

            power_value[2] = 1000000000;
            power_name[3] = "million";

            power_value[3] = 1000000;
            power_name[4] = "thousand";

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
            dollars_result = dollars_result + " rupiahs";

            // Combine the results.
            var returnValue = dollars_result;
            return returnValue.Substring(0, 1).ToUpper() + returnValue.Substring(1);
        }
    }
}