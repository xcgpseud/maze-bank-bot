using System;
using System.Collections.Generic;
using System.Linq;

namespace MazeBankBot.App.Helpers
{
    public class NumberRepresentationHelper
    {
        private const int Million = 1_000_000;
        private const int Thousand = 1_000;
        private const int Hundred = 100;

        private static readonly string[] UnitsMap =
        {
            "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten",
            "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen",
        };

        private static readonly string[] TensMap =
        {
            "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety",
        };

        public static string NumberToWords(int number)
        {
            if (number == 0)
            {
                return "zero";
            }

            if (number < 0)
            {
                return $"minus {NumberToWords(Math.Abs(number))}";
            }

            var words = "";

            if (number / Million > 0)
            {
                words += $"{NumberToWords(number / Million)} million ";
                number %= Million;
            }

            if (number / Thousand > 0)
            {
                words += $"{NumberToWords(number / Thousand)} thousand ";
                number %= Thousand;
            }

            if (number / Hundred > 0)
            {
                words += $"{NumberToWords(number / Hundred)} hundred ";
                number %= Hundred;
            }

            if (number > 0)
            {
                if (words != string.Empty)
                {
                    words += "and ";
                }

                if (number < 20)
                {
                    words += UnitsMap[number];
                }
                else
                {
                    words += TensMap[number / 10];
                    if (number % 10 > 0)
                    {
                        words += "-" + UnitsMap[number % 10];
                    }
                }
            }

            return words;
        }

        public static List<string> NumberToSingleWordArray(int number)
        {
            var digits = number.ToString().Select(x => int.Parse(new string(x, 1))).ToList();

            return digits.Select(NumberToWords).ToList();
        }
    }
}