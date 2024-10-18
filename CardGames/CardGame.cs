using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using static Library.Methods;

namespace CardGames
{
    public class CardGame
    {
        public string Name;
        public string Instructions;

        protected readonly string _playerName;
        protected readonly string _separator =  // 100 chars
            "-------------------------" +
            "-------------------------" +
            "-------------------------" +
            "-------------------------";

        public CardGame(string gameName, string playerName, params string[] instructions)
        {
            Name = gameName;
            _playerName = playerName;
            Instructions = string.Join(Environment.NewLine, instructions);
        }

        public CardGame() : this("CardGame", string.Empty) { }

        public void PrintInfo()
        {
            Print($"{Name}:");
            Print(Instructions);
            Print(_separator);
        }

        public virtual void PrintHUD()
        {
            ClearScreen();
            PrintInfo();
        }

        public virtual void Play()
        {
            Console.Title = Name;
            PrintInfo();
            Pause("Press Enter to play");
        }
    }
}