using System;
using System.Collections.Generic;
using System.Globalization;

namespace CurrencyToWords.Services
{
    // maximum 19 chars for non decimal
    public class NumberServiceB : INumberService
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
            var first = inputArr[0];

            var outputString = GetWordsForPrice(first, "dollar");

            // exit if no decimal
            if (inputArr.Length <= 1) 
                return outputString;

            // perform cent transformation
            var second = Convert.ToDouble("0." + inputArr[1]).ToString("#.00").Split('.')[1];

            if (Convert.ToInt64(second) == 0)
                return outputString;

            if (string.IsNullOrWhiteSpace(first) || Convert.ToInt64(first) == 0)
                outputString = "";

            outputString += (String.IsNullOrWhiteSpace(outputString) ? "" : " and ") + GetWordsForPrice(second, "cent");

            // return result
            return outputString;
        }

        private string GetWordsForPrice(string amountString, string suffix)
        {
            string output;

            var amount = Convert.ToInt64((string.IsNullOrWhiteSpace(amountString) ? "0" : amountString));

            if ((amount == 0) && (amount == 1))
            {
                output = NumberWordDict[amount];
            }
            else
            {
                output = GetWordsForPriceGtOne(amount);
                suffix += "s";
            }

            return (output + " " + suffix).Trim();
        }

        private string GetWordsForPriceGtOne(long amount)
        {
            var output = "";

            var amountMagnitude = (int)Math.Ceiling((double)(amount.ToString(CultureInfo.InvariantCulture).Length) / 3);
            var amountString = amount.ToString(CultureInfo.InvariantCulture);

            for (var i = 0; i < amountMagnitude; i++)
            {
                var chunk = amountString.Substring(Math.Max(0, amountString.Length - (3 + (i * 3))),
                                                    Math.Min(amountString.Length - (i * 3), 3));

                if (Convert.ToInt32(chunk) == 0)
                    continue;

                if (!string.IsNullOrWhiteSpace(output))
                    output = string.Concat(" and ", output);

                // Set magnitude
                output = string.Concat(GetMagnitudeText(i), output);

                // Get words for a chunk of 3 numbers
                output = GetWordForThreeNumbers(chunk, output);
            }

            return output;
        }

        private string GetWordForThreeNumbers(string chunk, string output)
        {
            if (chunk.Length > 1)
            {
                if (((chunk[chunk.Length - 2] == '0') && (chunk[chunk.Length - 1] != '0')) || (chunk[chunk.Length - 2] == '1'))
                {
                    output =
                        string.Concat(
                            NumberWordDict[
                                Convert.ToInt32(chunk[chunk.Length - 2].ToString() + chunk[chunk.Length - 1].ToString())],
                            output);
                }
                else if ((chunk[chunk.Length - 2] != '0') || (chunk[chunk.Length - 1] != '0'))
                {
                    if (chunk[chunk.Length - 1] != '0')
                        output = string.Concat("-" + NumberWordDict[Convert.ToInt32(chunk[chunk.Length - 1].ToString())], output);

                    output = string.Concat(NumberWordDict[Convert.ToInt32(chunk[chunk.Length - 2].ToString() + "0")], output);
                }
            }
            else if (chunk.Length > 0)
            {
                output = string.Concat(NumberWordDict[Convert.ToInt32(chunk[chunk.Length - 1].ToString())], output);
            }

            if (chunk.Length == 3 && chunk[0] != '0')
                output = string.Concat(NumberWordDict[Convert.ToInt32(chunk[0].ToString())]
                                       + " hundred"
                                       + (((Convert.ToInt32(chunk[2].ToString() + chunk[1].ToString()) > 0)) ? " and " : ""),
                    output);
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
                    //                    case 7:
                    //                        output = string.Concat(" sextillion ", output);
                    //                        break;
                    //                    case 8:
                    //                        output = string.Concat(" septillion ", output);
                    //                        break;
                    //                    case 9:
                    //                        output = string.Concat(" octillion ", output);
                    //                        break;
                    //                    case 10:
                    //                        output = string.Concat(" nonillion ", output);
                    //                        break;
                    //                    case 11:
                    //                        output = string.Concat(" decillion ", output);
                    //                        break;
                    //                    case 12:
                    //                        output = string.Concat(" undecillion ", output);
                    //                        break;
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
