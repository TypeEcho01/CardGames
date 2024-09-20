using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace CardGames
{
    public static class Functions
    {
        private static readonly Printer printer = new(end: Environment.NewLine);

        public static void Print(params object?[] values)
        {
            printer.Print(values);
        }

        public static void ConfigurePrint(string? sep = null, string? end = null)
        {
            printer.Configure(sep, end);
        }

        public static string Input(string? prompt = null)
        {
            if (prompt != null)
            {
                ConfigurePrint(end: "");
                Print(prompt);
            }

            string? output = Console.ReadLine();

            if (output is not null)
                return output;

            return "";
        }

        public static void ClearScreen()
        {
            Console.Clear();
        }

        public static int StringToInt(string value, int failsafe = 0, bool throwException = false)
        {
            if (int.TryParse(value, out int result))
                return result;

            if (!throwException)
                return failsafe;

            throw new Exception($"Functions.StringToInt(\"{value}\", throwException: true) could not convert \"{value}\" to int");
        }

        public static float StringToFloat(string value, float failsafe = 0.0f, bool throwException = false)
        {
            if (float.TryParse(value, out float result))
                return result;

            if (!throwException)
                return failsafe;

            throw new Exception($"Functions.StringToFloat(\"{value}\", throwException: true) could not convert \"{value}\" to float");
        }

        public static double StringToDouble(string value, double failsafe = 0.0, bool throwException = false)
        {
            if (double.TryParse(value, out double result))
                return result;

            if (!throwException)
                return failsafe;

            throw new Exception($"Functions.StringToDouble(\"{value}\", throwException: true) could not convert \"{value}\" to double");
        }
    }
}