namespace CardGames
{
    using static CardGames.Functions;
    internal class Program
    {
        static void Main()
        {
            Print(null, 3, 8.2435325, 77.532f, true, "Hello, world!", 'J', new Printer());
            Print();
            Print(null, null, null);
        }
    }
}