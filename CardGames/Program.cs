namespace CardGames
{
    using static CardGames.Functions;
    internal class Program
    {
        static void Main()
        {
            Deck CardDeck = new();
            Card MyCard = CardDeck.Deal();
            ConfigurePrint(sep: "\n");
            Print(MyCard, CardDeck);
        }
    }
}