using System;
using System.Collections.Generic;
using System.Globalization;
using CurrencyToWords.Services.Components;

namespace CurrencyToWords.Services
{
    // maximum 19 chars for non decimal
    public class NumberServiceA : INumberService
    {
        private Dictionary<long, string> _numberWordDict;

        public string ConvertPrice(double input)
        {
            return ConvertPrice(input.ToString());
        }

        public string ConvertPrice(string input)
        {
            var inputArr = input.Split('.');

            // perform dollar transformation
            var dollarAmount = inputArr[0];

            var outputString = GetWordsForAmount(dollarAmount, "dollar");

            // exit if no decimal
            if (inputArr.Length <= 1)
                return outputString;

            // perform cent transformation
            var centAmount = Convert.ToDouble("0." + inputArr[1]).ToString("#.00").Split('.')[1];

            if (Convert.ToInt64(centAmount) == 0)
                return outputString;

            if (string.IsNullOrWhiteSpace(dollarAmount) || Convert.ToInt64(dollarAmount) == 0)
                outputString = "";

            outputString += (String.IsNullOrWhiteSpace(outputString) ? "" : " and ") + GetWordsForAmount(centAmount, "cent");

            // return result
            return outputString;   
        }

        private string GetWordsForAmount(string amountString, string suffix)
        {
            string output;

            var amount = Convert.ToInt64((string.IsNullOrWhiteSpace(amountString) ? "0" : amountString));

            if (amount <= 1)
            {
                output = NumberWordDict[amount];
            }
            else
            {
                output = GetWordsForPriceGreaterThanOne(amount);
                suffix += "s";
            }

            return string.Format("{0} {1}", output, suffix).Trim();
        }

        private string GetWordsForPriceGreaterThanOne(long amount)
        {
            var output = "";

            var amountMagnitude = (int)Math.Ceiling((double)(amount.ToString(CultureInfo.InvariantCulture).Length) / 3);
            var amountString = amount.ToString(CultureInfo.InvariantCulture);

            for (var i = amountMagnitude - 1; i >= 0; i--)
            {
                var chunk = amountString.Substring(Math.Max(0, amountString.Length - (3 + (i * 3))),
                                                    Math.Min(amountString.Length - (i * 3), 3));

                if (Convert.ToInt32(chunk) == 0)
                    continue;

                if (!string.IsNullOrWhiteSpace(output))
                    output += " and ";

                // Get words for a chunk of 3 numbers
                output += GetWordForThreeNumbers(chunk, i);
            }

            return output;
        }

        private string GetWordForThreeNumbers(string chunk, int magnitude)
        {
            var output = "";

            var chunkie = new ChunkOfThree(chunk);

            if ((chunkie.Length == 3) && (chunkie.HundredToInt != 0))
                output += NumberWordDict[chunkie.HundredToInt]
                         + " hundred"
                         + (chunkie.TenSingleValue > 0 ? " and " : "");

            if (chunkie.Length > 1)
            {
                if (((chunkie.TenToInt == 0) && (chunkie.SingleToInt != 0)) || (chunkie.TenToInt == 1))
                {
                    output += NumberWordDict[chunkie.TenSingleValue];
                }
                else if ((chunkie.TenToInt != 0) || (chunkie.SingleToInt != 0))
                {
                    output += NumberWordDict[chunkie.TenValue];

                    if (chunkie.SingleValue != 0)
                        output += "-" + NumberWordDict[chunkie.SingleValue];
                }
            }
            else if (chunkie.Length > 0)
            {
                output += NumberWordDict[chunkie.SingleValue];
            }

            // Set magnitude
            output += GetMagnitudeText(magnitude);

            
            return output;
        }

        private string GetMagnitudeText(int i)
        {
            switch (i)
            {
                case 1:
                    return " thousand";
                case 2:
                    return " million";
                case 3:
                    return " billion";
                case 4:
                    return " trillion";
                case 5:
                    return " quadrillion";
                case 6:
                    return " quintillion";
                default:
                    return "";
            }
        }

        #region Properties
        public Dictionary<long, string> NumberWordDict
        {
            get
            {
                return _numberWordDict
                       ?? (_numberWordDict = new Dictionary<long, string>()
                       {
                           // zero to nine
                           {0, "zero"},
                           {1, "one"},
                           {2 , "two"},
                           {3 , "three"},
                           {4 , "four"},
                           {5 , "five"},
                           {6 , "six"},
                           {7 , "seven"},
                           {8 , "eight"},
                           {9 , "nine"},
                           // ten to nineteen
                           {10 , "ten"},
                           {11 , "eleven"},
                           {12 , "twelve"},
                           {13 , "thirteen"},
                           {14 , "fourteen"},
                           {15 , "fifteen"},
                           {16 , "sixteen"},
                           {17 , "seventeen"},
                           {18 , "eighteen"},
                           {19 , "nineteen"},
                           // twenty to ninety
                           {20 , "twenty"},
                           {30 , "thirty"},
                           {40 , "forty"},
                           {50 , "fifty"},
                           {60 , "sixty"},
                           {70 , "seventy"},
                           {80 , "eighty"},
                           {90 , "ninety"},
                       });
            }
        }
        #endregion
    }
}
