using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using static Library.Methods;

namespace CardGames
{
    public class HigherOrLower : CardGame
    {
        public int Score = 0;
        public int Round = 0;
        public Deck? Deck = null;
        public Card? CurrentCard = null;
        public Person? Dealer = null;

        public HigherOrLower(string playerName = "Player") : base(
            "Higher or Lower",
            playerName,
            "The Dealer gives you a card.",
            "You must guess if the next card's value is higher or lower than the current card.",
            "Enter \"H\" for higher, \"L\" for lower, or \"S\" for same.",
            "If you guess correctly, you gain a point. If you guessed same, you gain 5 times the points",
            "The dealer then gives you that card and the game repeats until there are no more cards.",
            "An Ace is worth 1, a Jack is worth 11, a Queen is worth 12, and a King is worth 13."
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
                int scoreIncreaseAmount = 1;
                Round++;
                PrintHUD();

                char guess = GetGuess();
                char answer = GetAnswer(nextCard);
                bool correct = IsCorrect(guess, answer);

                if (correct)
                {
                    if (guess == 'S')
                        scoreIncreaseAmount *= 5;
                    Score += scoreIncreaseAmount;
                }

                PrintRoundConclusion(nextCard, guess, answer);

                if (correct)
                {
                    if (scoreIncreaseAmount == 1)
                    {
                        Dealer.Speak("Nice job!");
                        Print("Correct! +1 Point");
                    }
                    else
                    {
                        Dealer.Speak("Wow! That was great!");
                        Print($"Correct! +{scoreIncreaseAmount} Points");
                    }
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
            Dealer.Speak("Do you think the next card's value is higher, lower, or the same as this card?");
            Print("Enter \"H\" to choose higher");
            Print("Enter \"L\" to choose lower");
            Print("Enter \"S\" to choose same");
            while (true)
            {
                string input = Input(">>> ").ToUpper();
                if (input == "H" || input == "L" || input == "S")
                    return input.To<char>();
                Print("Invalid answer. Please try again.");
            }
        }

        private char GetAnswer(Card nextCard)
        {
            if (nextCard.Rank > CurrentCard?.Rank)
                return 'H';
            if (nextCard.Rank < CurrentCard?.Rank)
                return 'L';
            return 'S';
        }

        private bool IsCorrect(char guess, char answer)
        {
            return guess == answer;
        }

        private string GetCharSignature(char character)
        {
            if (character == 'H')
                return "Higher";
            if (character == 'L')
                return "Lower";
            return "Same";
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