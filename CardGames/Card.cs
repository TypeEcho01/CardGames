using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using static Library.Methods;

namespace CardGames
{
    public class Card
    {
        public int Rank;
        public string Suit;
        public string Name;

        public Card(int rank, string suit)
        { 
            Rank = rank;
            Suit = suit;
            Name = GetName();
        }

        private string GetName()
        {
            return Rank switch
            {
                1 => $"Ace of {Suit}",
                11 => $"Jack of {Suit}",
                12 => $"Queen of {Suit}",
                13 => $"King of {Suit}",
                _ => $"{Rank} of {Suit}",
            };
        }

        public override string ToString() => Name;
    }
}