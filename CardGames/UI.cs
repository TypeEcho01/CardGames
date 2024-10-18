using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using static Library.Methods;

namespace CardGames
{
    public class UI
    {
        public CardGame? Game = null;

        public string Title;
        public string Author;
        public int Year;
        
        public string PlayerName;

        private string _location = "Unknown";
        protected readonly string _separator =  // 100 chars
            "-------------------------" +
            "-------------------------" +
            "-------------------------" +
            "-------------------------";

        public UI() 
        {
            Title = "Card Games";
            Author = "Echo";
            Year = 2024;

            PlayerName = "Player";
        }

        public void Load() => LoadMainMenu();

        private void SetLocation(string location)
        {
            _location = location;
            Console.Title = $"{Title} | {location}";
        }

        private void LoadMainMenu()
        {
            SetLocation("Main Menu");

            while (true)
            {
                Game = null;
                ClearScreen();
                PrintInfo();
                Print("1. Same or Different");
                Print("2. Higher or Lower");
                Print("3. Highest Match");
                Print();
                Print("9. Settings");
                Print("0. Exit Game");
                Print();

                int choice = GetChoice(1, 2, 3, 9, 0);
                switch (choice)
                {
                    case 1:
                        Game = new SameOrDifferent(PlayerName);
                        break;
                    case 2:
                        Game = new HigherOrLower(PlayerName);
                        break;
                    case 3:
                        Game = new HighestMatch(PlayerName);
                        break;
                    case 9:
                        LoadSettings();
                        break;
                    case 0:
                        return;
                }

                if (Game is not null)
                {
                    ClearScreen();
                    Game.Play();
                }
            }
        }

        private void LoadSettings()
        {
            SetLocation("Settings");

            while (true)
            {
                ClearScreen();
                PrintInfo();
                Print("1. Set name");
                Print();
                Print("9. View Credits");
                Print("0. Exit Settings");
                Print();

                int choice = GetChoice(1, 9, 0);
                switch (choice)
                {
                    case 1:
                        SetPlayerName();
                        break;
                    case 9:
                        ViewCredits();
                        break;
                    case 0:
                        return;
                }
                Print();
                Pause("Press Enter to continue");
            }
        }

        private void ViewCredits()
        {
            Print(Title);
            Print("Created by", Author);
            Print("Created in", Year);
        }

        private void SetPlayerName()
        {
            Print("Enter your new name.");
            while (true)
            {
                string input = Input(">>> ");
                if (input.ToLower() != "dealer")
                {
                    PlayerName = input;
                    break;
                }
                Print($"Sorry, but your name cannot be set to \"{input}\"");
            }
            Print($"Your name is now {PlayerName}!");
        }

        private void PrintInfo()
        {
            Print(Title);
            Print(_location);
            Print(_separator);
        }

        private int GetChoice(params int[] validChoices)
        {
            if (validChoices.Length == 0)
                throw new ArgumentException("Number of valid choices cannot be 0");

            while (true)
            {
                string input = Input(">>> ");
                if (!int.TryParse(input, out int choice))
                {
                    Print("Invalid answer. Please try again.");
                    continue;
                }

                foreach (int validChoice in validChoices)
                {
                    if (choice == validChoice)
                        return validChoice;
                }

                Print("Invalid answer. Please try again.");
            }
        }

        private int GetChoice(string prompt, int[] validChoices)
        {
            Print(prompt);
            return GetChoice(validChoices);
        }
    }
}