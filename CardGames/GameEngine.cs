using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using static Library.Methods;

namespace CardGames
{
    public class GameEngine
    {
        public string Name;
        public string Instructions;

        public GameEngine(string name, string instructions)
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