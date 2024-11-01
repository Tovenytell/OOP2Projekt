public class Printer<T>
{
    private readonly Dictionary<Suits, string> suitIcons = new Dictionary<Suits, string>
    {
        { Suits.Hearts, "♥" },
        { Suits.Spades, "♠" },
        { Suits.Diamonds, "♦" },
        { Suits.Clubs, "♣" }
    };

    private readonly Dictionary<Values, string> valueIcons = new Dictionary<Values, string>
    {
        { Values.Ace, "1" },       // Ace as "1"
        { Values.Two, "2" },
        { Values.Three, "3" },
        { Values.Four, "4" },
        { Values.Five, "5" },
        { Values.Six, "6" },
        { Values.Seven, "7" },
        { Values.Eight, "8" },
        { Values.Nine, "9" },
        { Values.Ten, "10" },
        { Values.Jack, "11" },     // Jack as "11"
        { Values.Queen, "12" },    // Queen as "12"
        { Values.King, "13" }      // King as "13"
    };
    // Prints the input on a new line (default Console.WriteLine behavior)
    public void PrintVertically(T input)
    {
        Console.WriteLine($"\n{input}\n");
        Console.WriteLine();

    }

    // Prints the input inline without a newline at the end
    public void PrintHorizontally(T input)
    {
        Console.Write(input + ", ");
    }

    // Specialized pretty-print method for "Card" type if applicable
    public void PrintCard(T input)
    {
        if (input is Card card)
        {
            // Console.WriteLine("╔══════════╗");
            // Console.WriteLine($"║ {card.Value,-8} ║");
            // Console.WriteLine($"║ of       ║");
            // Console.WriteLine($"║ {card.Suit,-8} ║");
            // Console.WriteLine("╚══════════╝");

            string value = valueIcons.ContainsKey(card.Value) ? valueIcons[card.Value] : card.Value.ToString();
            string suitIcon = suitIcons.ContainsKey(card.Suit) ? suitIcons[card.Suit] : card.Suit.ToString();

            // Print the value with the suit icon
            Console.Write($"|{value}{suitIcon}| ");
            
        }

        else
        {
            Console.WriteLine(input);  // Fallback for non-card types
        }
    }
}