using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardGames
{
    public class Game
    {
        public string Name;
        public string Instructions;

        public Game(string name, string instructions) 
        {
            Name = name;
            Instructions = instructions;
        }

        public void Play()
        {
            throw new System.NotImplementedException();
        }
    }
}