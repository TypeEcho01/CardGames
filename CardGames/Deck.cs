using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;

using static CardGames.Functions;

namespace CardGames
{
    public class Deck
    {
        private static int numberOfDecks = 0;

        public string Name;
        public string[] Suits;
        public int SuitSize;
        public int DeckSize;
        public List<Card> Cards;

        public Deck(string? name = null, string[]? suits = null, int suitSize = 13)
        {
            if (suitSize <= 0)
                throw new ArgumentException("Deck() suitSize must be greater than 0");


            if (suits == null) // Default suits
                Suits = ["Spades", "Clubs", "Diamonds", "Hearts"];
            else
            {
                Suits = suits;
                if (Suits.Length == 0)
                    throw new ArgumentException("Deck() suits must contain at least one string");
            }

            SuitSize = suitSize;
            DeckSize = SuitSize * Suits.Length;

            Cards = BuildDeck();
            //Shuffle();
            numberOfDecks++;

            if (name != null)
                Name = name;
            else
                Name = $"Deck {numberOfDecks}";
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

        public override string ToString()
        {
            return Show();
        }

        public string Show()
        {
            StringBuilder stringBuilder = new();
            stringBuilder.AppendLine($"{Name}:");

            foreach (Card card in Cards)
            {
                stringBuilder.AppendLine(card.Name);
            }

            string output = stringBuilder.ToString();
            return output;
        }

        public void Shuffle()
        {
            throw new System.NotImplementedException();
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
