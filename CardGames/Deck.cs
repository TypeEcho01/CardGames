using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;

using static Library.Methods;

namespace CardGames
{
    public class Deck
    {
        public string Name;
        public string[] Suits;
        public int SuitSize;
        public int DeckSize;
        public List<Card> Cards;

        public Deck(string name, string[] suits, int suitSize = 13)
        {
            Name = name;
            Suits = suits;
            SuitSize = suitSize;

            DeckSize = SuitSize * Suits.Length;
            Cards = BuildDeck();
            Shuffle();
        }

        private List<Card> BuildDeck()
        {
            List<Card> cards = [];
            for (int rank = 1; rank <= SuitSize; rank++)
            {
                foreach(string suit in Suits)
                {
                    cards.Add(new Card(rank, suit));
                }
            }
            return cards;
        }

        public override string ToString() => Display();

        public string Display()
        {
            StringBuilder stringBuilder = new();
            stringBuilder.AppendLine($"{Name}:");

            foreach (Card card in Cards)
                stringBuilder.AppendLine(card.Name);

            string output = stringBuilder.ToString();
            return output;
        }

        public void Shuffle()
        {
            RandomShuffle(Cards);
        }

        public Card Deal()
        {
            int topIndex = Cards.Count - 1;

            Card card = Cards[topIndex];
            Cards.RemoveAt(topIndex);

            return card;
        }
    }
}
