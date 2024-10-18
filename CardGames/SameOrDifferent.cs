using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using static Library.Methods;

namespace CardGames
{
    public class SameOrDifferent : CardGame
    {
        public int Score = 0;
        public int Round = 0;
        public Deck? Deck = null;
        public Card? CurrentCard = null;
        public Person? Dealer = null;

        public SameOrDifferent(string playerName = "Player") : base(
            "Same or Different",
            playerName,
            "The Dealer gives you a card.",
            "You must guess if the next card's suit is the same or different as the current card.",
            "Enter \"S\" for same, or \"D\" for different.",
            "If you guess correctly, you gain a point.",
            "The dealer then gives you that card and the game repeats until there are no more cards."
        )
        { }

        public override void PrintHUD()
        {
            base.PrintHUD();
            Print("Score:", Score);
            Print("Round:", Round);
            Print();
            Print("Current Card:", CurrentCard);
        }

        public override void Play()
        {
            base.Play();
            Score = 0;
            Round = 0;
            Deck = new Deck(
                "Dealer's Deck",
                ["Spades", "Clubs"],
                13
            );
            Dealer = new Person("Dealer");
            CurrentCard = Deck.Deal();

            foreach (Card nextCard in Deck.Cards)
            {
                Round++;
                PrintHUD();
                
                char guess = GetGuess();
                char answer = GetAnswer(nextCard);
                bool correct = IsCorrect(guess, answer);

                if (correct)
                    Score++;

                PrintRoundConclusion(nextCard, guess, answer);

                if (correct)
                {
                    Dealer.Speak("Nice job!");
                    Print("Correct! +1 Point");
                }
                else
                {
                    Dealer.Speak("Good guess.");
                    Print("Incorrect.");
                }

                Pause("Press Enter to continue");
                CurrentCard = nextCard;
            }

            PrintGameConclusion();
            Pause("Press Enter to exit");
        }

        private char GetGuess()
        {
            Dealer.Speak("Do you think the next card's suit is the same as or different than this card?");
            Print("Enter \"S\" to choose same");
            Print("Enter \"D\" to choose different");
            while (true)
            {
                string input = Input(">>> ").ToUpper();
                if (input == "S" || input == "D")
                    return input.To<char>();
                Print("Invalid answer. Please try again.");
            }
        }

        private char GetAnswer(Card nextCard)
        {
            if (CurrentCard?.Suit == nextCard.Suit)
                return 'S';
            return 'D';
        }

        private bool IsCorrect(char guess, char answer)
        {
            return guess == answer;
        }

        private string GetCharSignature(char character)
        {
            if (character == 'S')
                return "Same";
            return "Different";
        }

        private void PrintRoundConclusion(Card nextCard, char guess, char answer)
        {
            PrintHUD();
            Print("Next Card:", nextCard);
            Print();
            Print("Your guess:", GetCharSignature(guess));
            Print("Correct answer:", GetCharSignature(answer));
            Print();
        }

        private void PrintGameConclusion()
        {
            ClearScreen();
            PrintInfo();
            Print();
            Print("Game Over!");
            Print();
            string quote;
            if (Score == 0)
                quote = "That's honestly impressive.";
            else if (Score < 10)
                quote = "I'm sure you'll do better next time.";
            else if (Score < 15)
                quote = "You did good!";
            else if (Score < 20)
                quote = "You did great!";
            else
                quote = "Wow! You did amazing!";
            Dealer.Speak(quote);
            Print("Final Score:", Score);
        }
    }
}