public class Printer<T>
{
    // Prints the input on a new line (default Console.WriteLine behavior)
    public void PrintVertically(T input)
    {
        Console.WriteLine(input);
    }

    // Prints the input inline without a newline at the end
    public void PrintHorizontally(T input)
    {
        Console.Write(input);
    }

    // Specialized pretty-print method for "Card" type if applicable
    public void PrintCard(T input)
    {
        if (input is Card card)
        {
            Console.WriteLine("╔══════════╗");
            Console.WriteLine($"║ {card.Value,-8} ║");
            Console.WriteLine($"║ of       ║");
            Console.WriteLine($"║ {card.Suit,-8} ║");
            Console.WriteLine("╚══════════╝");
        }
        else
        {
            Console.WriteLine(input);  // Fallback for non-card types
        }
    }
}