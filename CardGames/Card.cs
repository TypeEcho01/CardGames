using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardGames
{
    using static Library;
    public class Card
    {
        public string Rank;
        public string Suit;
        public string Name;
        public int Value;

        public Card(string rank, string suit) 
        { 
            Rank = StringToTitle(rank);
            Suit = StringToTitle(suit);
            Name = $"{Rank} of {Suit}";
            Value = DetermineValue();
        }

        private int DetermineValue()
        {
            switch (Rank)
            {
                case "Ace":
                    return 1;
                case "Jester":
                    return 11;
                case "Queen":
                    return 12;
                case "King":
                    return 13;
            }
            if ((int.TryParse(Rank, out int value)) && (value >= 1) && (value <= 13)) return value;

            throw new Exception("The Rank of a Card must be \"Ace\", \"Jester\", \"Queen\", \"King\", or a string of 1-13");
        }
    }
}