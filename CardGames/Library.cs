using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CardGames
{
    public class Library
    {
        public static void Print(params object[] args)
        {
            string sep = " ";
            string end = "\n";

            string output = "";
            foreach (object arg in args)
            {
                output += arg.ToString() + sep;
            }
            output += end;

            Console.Write(output);
        }

        public static string Input(string? prompt = null)
        {
            if (prompt != null) Console.Write(prompt);

            string? output = Console.ReadLine();

            if (output != null) return output;
            return "";
        }

        public static void PauseUntilInput(string? prompt = null)
        {
            if (prompt != null) Console.Write(prompt);
            Console.ReadKey();
        }

        public static string StringToTitle(string substring)
        {
            int length = substring.Length;
            if (length == 0) return substring;
            if (length == 1) return substring.ToUpper();

            string firstChar = (substring[0].ToString()).ToUpper(); // Uppercases the first character
            string remainingString = (substring[1..]).ToLower();  // Lowercases the rest of the characters

            return firstChar + remainingString;
        }

        public static void ClearScreen()
        {
            Console.Clear();
        }
    }
}