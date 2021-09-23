using System;
using System.Linq;

namespace RobotWars.Common.Helpers
{
    public static class InputValidate
    {
        public static string Validate(string input)
        {
            const string result = "Valid";

            if (ValidateEmptyString(input))
            {
                return "Input Cannot Be Empty or Null";
            }

            if (ValidateOnlyAlphaInString(input))
            {
                return "Input Cannot Contain Numerics";
            }

            if (CheckForAllowedValues(input))
            {
                return "Input Is Valid";
            }

            return result;
        }

        public static string RemoveWhiteSpace(string input)
        {
            return string.Concat(input.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
        }

        public static string[] CleanseInput(string input)
        {
            var len = input.Length;
            string[] b = new string[len];

            for (int i = 0; i < len; i++)
            {
                b[i] = input[i].ToString();
            }

            return b;
        }

        public static bool ValidateNumeric(string input)
        {
            return int.TryParse(input, out _);
        }

        private static bool ValidateEmptyString(string input)
        {
            return string.IsNullOrEmpty(input);
        }

        private static bool ValidateOnlyAlphaInString(string input)
        {
            return int.TryParse(input, out _);
        }

        private static bool CheckForAllowedValues(string input)
        {
            return input switch
            {
                "M" => true,
                "L" => true,
                "R" => true,
                _ => false
            };
        }
    }
}
