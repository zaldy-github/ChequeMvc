using System;

namespace ChequeMvc.Services.Shared.Utilities
{
    public class ChequeUtilities : IChequeUtilities
    {
        #region Methods to translate dollar amount to words
        public string TranslateDollarAmountToWords(decimal? amount)
        {
            string dollarAmountInWords = IsValidDollarAmount(amount);

            if (string.IsNullOrEmpty(dollarAmountInWords))
            {
                long integerPart = (long)Math.Truncate((decimal)amount);
                decimal decimalPart = (decimal)amount - integerPart;

                dollarAmountInWords = TranslateDigitsToWords(integerPart.ToString()) + " Dollars";

                if (decimalPart != 0)
                {
                    string decimals = decimalPart.ToString().Replace("0.", "");
                    dollarAmountInWords += " and " + TranslateDigitsToWords(decimals) + " Cents";
                }
            }

            return dollarAmountInWords;
        }

        private string IsValidDollarAmount(decimal? amount)
        {
            if (amount < 0)
            {
                return "Invalid amount.";
            }

            if (amount == 0)
            {
                return "Zero Dollars";
            }

            return "";
        }

        private string TranslateDigitsToWords(string digits)
        {
            // Comma separators
            string[] digitSeparators = { "", " Thousand ", " Million ", " Billion " };

            // Initialize indexer for seperators and return value
            int separatorIdx = 0;
            string digitsInWords = "";

            // Parse digits in 3s
            while (digits.Length > 0)
            {
                // Get last 3 digits
                string _3digitsInWord = digits.Length < 3 ? digits : digits.Substring(digits.Length - 3);

                // Remove last 3 digits from value and parse
                digits = digits.Length < 3 ? "" : digits.Remove(digits.Length - 3);

                // Make sure number is not zero
                int number = int.Parse(_3digitsInWord);
                if (number != 0)
                {
                    // Translate 3 digits to words
                    _3digitsInWord = Translate3DigitsToWords(number);

                    // Apply the separator
                    _3digitsInWord += digitSeparators[separatorIdx];

                    // Append result to return value.
                    digitsInWords = _3digitsInWord + digitsInWords;
                }

                // Get next 3 digits
                separatorIdx++;
            }

            return digitsInWords;
        }

        private static string Translate3DigitsToWords(int digits)
        {
            string[] ones = {
                "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven",
                "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen"
            };
            string[] tens = { "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };
            string _3digitsInWord = "";

            if (digits > 99 && digits < 1000)
            {
                int i = digits / 100;
                _3digitsInWord = _3digitsInWord + ones[i - 1] + " Hundred ";
                digits = digits % 100;
            }

            if (digits > 19 && digits < 100)
            {
                int i = digits / 10;
                _3digitsInWord = _3digitsInWord + tens[i - 1] + " ";
                digits = digits % 10;
            }

            if (digits > 0 && digits < 20)
            {
                _3digitsInWord = _3digitsInWord + ones[digits - 1];
            }

            return _3digitsInWord;
        }
        #endregion
    }
}
