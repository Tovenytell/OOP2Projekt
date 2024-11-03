public class Printer<T> // KRAV #1:
//Koncept: Generics
//Vi använder den generiska typen Printer<T> genom att skapa olika typer av printers i Game för att 
//kunna printa ut olika typer, till exempel kunna printa ut typen Hand och int
//Vi använder det för att kunna printa ut olika typer. Skulle vi i famtiden vilja kunna printa ut andra typer av kort
//t.ex. en UNO-kortlek kan detta även vara användbart.
{

    //från chat
    private readonly Dictionary<Suits, string> suitIcons = new Dictionary<Suits, string>
    {
        { Suits.Hearts, "♥" },
        { Suits.Spades, "♠" },
        { Suits.Diamonds, "♦" },
        { Suits.Clubs, "♣" }
    };

    //från chat
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
 
    public void PrintVertically(T input)
    {
        Console.WriteLine($"\n{input}\n");
        Console.WriteLine();
    }


    public void PrintHorizontally(T input)
    {
        Console.Write(input + ", ");
    }


    public void PrintCard(T input)
    {
        if (input is Card card)
        {
            string value = valueIcons.ContainsKey(card.Value) ? valueIcons[card.Value] : card.Value.ToString();
            string suitIcon = suitIcons.ContainsKey(card.Suit) ? suitIcons[card.Suit] : card.Suit.ToString();
            
            if (card.Suit == Suits.Hearts || card.Suit == Suits.Diamonds)
            {
                Console.ForegroundColor = ConsoleColor.Red; 
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Black; 
            }

   
        Console.Write($"|{value}{suitIcon}| ");


        Console.ResetColor();
        }

        else
        {
            Console.WriteLine(input);
        }
    }
}