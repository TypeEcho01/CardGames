using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using static System.Formats.Asn1.AsnWriter;

using static Library.Methods;

namespace CardGames
{
    public class HighestTotal : CardGame
    {
        public int Round = 0;
        public int MaxRoundCount = 5;
        public Person? Player = null;
        public Person? Dealer = null;
        public Deck? Deck = null;

        private readonly Random _random = new();

        public HighestTotal(string playerName = "Player") : base(
            "Highest Total",
            playerName,
            "You and the Dealer have a hand of 4 cards each.",
            "You can see your hand, but not the Dealer's.",
            "The person with the highest value of all their cards wins.",
            "Each round, you can choose to replace one of your cards or not.",
            "The dealer may also choose to replace a card or not.",
            "The game ends after all rounds are over.",
            "An Ace is worth 1, a Jack is worth 11, a Queen is worth 12, and a King is worth 13."
        )
        { }

        public override void PrintHUD()
        {
            base.PrintHUD();
            Print("Round:", Round, '/', MaxRoundCount);
            PrintHand(Player);
            Print();
        }

        public static void PrintHand(Person person)
        {
            string prefix;

            if (person.Name == "Player")
                prefix = "Your";
            else
                prefix = $"{person.Name}'s";

            Print(prefix, "hand:");
            for (int i = 1; i <= person.Hand.Count; i++)
            {
                Card card = person.Hand[i - 1];
                Print($"    {i}) {card}");
            }
            Print("    Value:", GetHandValue(person));
        }

        public override void Play()
        {
            base.Play();
            Round = 0;
            Deck = new Deck(
                "Dealer's Deck",
                ["Spades", "Clubs", "Diamonds", "Hearts"],
                13
            );

            int numberOfCardsPerPerson = 4;

            Player = CreatePerson(_playerName, numberOfCardsPerPerson);
            Dealer = CreatePerson("Dealer", numberOfCardsPerPerson);

            for (Round = 1; Round <= MaxRoundCount; Round++)
            {
                PrintHUD();

                // Player Choice
                int playerChoice = GetPlayerChoice();
                string oldCardName = string.Empty;
                string newCardName = string.Empty;
                int oldPlayerHandValue = GetHandValue(Player);

                if (playerChoice != -1)
                {
                    oldCardName = Player.Hand[playerChoice].Name;
                    ReplaceCard(Player, playerChoice);
                    newCardName = Player.Hand[playerChoice].Name;
                }

                PrintHUD();

                // Player does not replace a card
                if (playerChoice == -1)  
                {
                    Dealer.Speak("Okay.");
                    Print("You have chosen to not replace a card.");
                    Print();
                    Print("Your hand value of", oldPlayerHandValue, "is unchanged.");
                }
                // Player replaces a card
                else
                {
                    Dealer.Speak("Here is your new card.");
                    Print($"You have chosen to replace your {oldCardName}. You drew a {newCardName}.");
                    Print();
                    int newPlayerHandValue = GetHandValue(Player);
                    string descriptor;

                    if (newPlayerHandValue > oldPlayerHandValue)
                        descriptor = "is higher than";
                    else if (newPlayerHandValue < oldPlayerHandValue)
                        descriptor = "is lower than";
                    else
                        descriptor = "is the same as";

                    Print("Your new hand value of", newPlayerHandValue, descriptor, $"your previous hand value of {oldPlayerHandValue}.");
                }
                
                Print();

                int dealerChoice = GetDealerChoice();

                // Dealer does not replace a card
                if (dealerChoice == -1)
                {
                    Dealer.Speak("I won't replace any of my cards.");
                    Print("The dealer has chosen to not replace a card.");
                }
                // Dealer replaces a carda
                else
                {
                    Dealer.Speak("I'll replace this card.");
                    Print("The dealer has chosen to replace a card.");
                    ReplaceCard(Dealer, dealerChoice);
                }

                Print();
                Pause("Press Enter to continue");
            }
            Print();
            Dealer.Speak("Let's see who the winner is!");
            Pause("Game Over! Press Enter to continue");
            PrintGameConclusion();
            Print();
            Pause("Press Enter to exit");
        }

        private void PrintGameConclusion()
        {
            int playerScore = GetHandValue(Player);
            int dealerScore = GetHandValue(Dealer);

            ClearScreen();
            PrintInfo();
            Print();

            PrintHand(Player);
            Print();

            PrintHand(Dealer);
            Print();

            if (playerScore > dealerScore)
            {
                string quote;
                int difference = playerScore - dealerScore;
                if (difference == 1)
                    quote = "Wow! That was a super close game. Congratulations!";
                else if (difference < 4)
                    quote = "Wow! That was a pretty close game. Next time I'll get you!";
                else if (difference < 8)
                    quote = "That was a close game. Good job!";
                else if (difference < 16)
                    quote = "You beat me. Great job!";
                else
                    quote = "Well, I wasn't even close to your score. Amazing job!";
                Dealer.Speak(quote);
                Print("You win!");
                return;
            }
            if (playerScore < dealerScore)
            {
                string quote;
                int difference = dealerScore - playerScore;
                if (difference == 1)
                    quote = "Wow! That was a super close game. Unlucky!";
                else if (difference < 4)
                    quote = "Wow! That was a pretty close game. Great try!";
                else if (difference < 8)
                    quote = "That was a close game. Good try!";
                else if (difference < 16)
                    quote = "Good try. I bet you'll do better next time!";
                else
                    quote = "You were quite far off, but I know you'll do better next time!";
                Dealer.Speak(quote);
                Print("Dealer wins!");
                return;
            }
            Dealer.Speak("Wow! What are the chances?");
            Print("Draw!");
        }

        private static int GetCardValue(Card card) => card.Rank;  // For this game, the rank is equal to the value, so ace == 1, jack == 11, etc.

        private static int GetHandValue(Person person)
        {
            int sum = 0;
            foreach (Card card in person.Hand)
                sum += GetCardValue(card);

            return sum;
        }

        private Person CreatePerson(string name, int numberOfCards)
        {
            Person person = new Person(name);
            for (int i = 0; i < numberOfCards; i++)
                person.Hand.Add(Deck.Deal());

            return person;
        }

        private void ReplaceCard(Person person, int index)
        {
            person.Hand[index] = Deck.Deal();
        }

        private int GetPlayerChoice()
        {
            Dealer.Speak("Would you like to replace one of your cards with a new card?");
            Print("Enter the corresponding number to replace that card.");
            Print("Enter \"0\" to not replace a card.");
            while (true)
            {
                string input = Input(">>> ");
                if (!int.TryParse(input, out int value) || value < 0 || value > Player.Hand.Count)
                {
                    Print("Invalid answer. Please try again.");
                    continue;
                }
                return value - 1;  // value - 1 for indexing purposes
            }
        }

        private int GetDealerChoice()
        {
            int doNotReturnCard = -1;
            int panicChance = _random.Next(1, 101);
            int panicThreshold = _random.Next(0, 26); // Anywhere between a 0% and 25% chance to panic

            // Replaces a random card or completely skips if dealer panics
            if (panicChance <= panicThreshold)
            {
                int replaceCardChance = _random.Next(3);
                if (replaceCardChance == 0)  // 1 in 3 chance to not replace a card
                    return doNotReturnCard;

                // 2 in 3 chance to replace a random card
                return _random.Next(Dealer.Hand.Count);
            }

            // Continue if the dealer does not panic
            int highestPossibleValue = Deck.SuitSize;
            int minValueRank = highestPossibleValue;
            int minValueIndex = -1;

            for (int i = 0; i < Dealer.Hand.Count; i++)
            {
                Card card = Dealer.Hand[i];
                if (GetCardValue(card) < minValueRank)
                    continue;

                minValueRank = card.Rank;
                minValueIndex = i;
            }

            int valueIsLowThreshold = (highestPossibleValue / 2) + _random.Next(-1, 2);
            int goodChoiceChance = _random.Next(1, 101);
            int goodChoiceThreshold = _random.Next(50, 101);  // Anywhere between a 50% and 100% chance to make a good choice

            // Should replace a card because lowest value is under or at the threshold
            if (minValueRank <= valueIsLowThreshold)
            {
                if (goodChoiceChance <= goodChoiceThreshold)
                    return minValueIndex;

                return doNotReturnCard;
            }
            // Should NOT replace a card because the lowest value is high
            if (goodChoiceChance <= goodChoiceThreshold)
                return doNotReturnCard;

            return minValueIndex;
        }
    }
}