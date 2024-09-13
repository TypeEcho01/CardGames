using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardGames
{
    public class Deck
    {
        public string Name;
        public System.Collections.Generic.List<Card> Cards;

        public Deck(string name, List<Card> cards)
        {
            Name = name;
            Cards = cards;
        }

        public void Shuffle()
        {
            throw new System.NotImplementedException();
        }

        public Card Deal()
        {
            throw new System.NotImplementedException();
        }
    }
}