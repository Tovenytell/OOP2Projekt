public class HelpBehavior : Behavior
{
    
    private readonly FileHandler<List<PreviousMoves>> moveHandler = new FileHandler<List<PreviousMoves>>();

    private static readonly Dictionary<string, Values> ValueMapping = new()
    {
        { "Ace", Values.Ace },
        { "Two", Values.Two },
        { "Three", Values.Three },
        { "Four", Values.Four },
        { "Five", Values.Five },
        { "Six", Values.Six },
        { "Seven", Values.Seven },
        { "Eight", Values.Eight },
        { "Nine", Values.Nine },
        { "Ten", Values.Ten },
        { "Jack", Values.Jack },
        { "Queen", Values.Queen },
        { "King", Values.King }
    };
    
    public List<Values> GetPreviouslyAskedValues(string filePath)
    {
        try
        {
            var previousMoves = moveHandler.Load("moves.json");

            // Extrahera alla kort som datorn tidigare har frågat efter
            return previousMoves
                .Where(move => move.PlayerName == "Torsten") // Bara datorns drag
                .Select(move =>
                {

                if (move.Action.StartsWith("Asked for ", StringComparison.OrdinalIgnoreCase))
                {
                    
                    string valueStr = move.Action.Substring("Asked for ".Length).TrimEnd('s', ' ', '\n', '\r');

                    
                    if (ValueMapping.TryGetValue(valueStr, out Values value))
                    {
                        return (Values?)value; 
                    }
                    else
                    {
                        Console.WriteLine($"Failed to map value: {valueStr}");
                    }
                }
                else
                {
                    Console.WriteLine($"Unexpected format: {move.Action}");
                }

                    return null;
                })
                .Where(value => value.HasValue)
                .Select(value => value.Value)
                .Distinct()
                .ToList();
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("No previous moves found.");
            return new List<Values>();
        }
    }


    public List<Values> GetSuggestedValues(Player player, string movesFilePath)
    {   
        
        List<Values> availableValues = CheckAvailableValues(player);

        
        List<Values> previouslyAskedValues = GetPreviouslyAskedValues(movesFilePath);

        
        List<Values> suggestedValues = availableValues
            .Intersect(previouslyAskedValues) 
            .ToList();

        if (!suggestedValues.Any())
        {
            Console.WriteLine("No information about help exists at the moment.");
            return new List<Values>(); 
        }

            
            return suggestedValues;
         }

    public override Values AskForCard(List<Values> suggestedValues)
    {
        Console.WriteLine("These numbers are suggested for you to pick: ");
        foreach (Values value in suggestedValues)
        {
            Console.Write($"{value} | ");
        }

        Console.WriteLine("\n\nWhat card would you like to ask for?");
  
        while (true) 
        {
            string input = Console.ReadLine();

            // Validate input
            if (Enum.TryParse(typeof(Values), input, true, out var result) && Enum.IsDefined(typeof(Values), result))
            {
                return (Values)result;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid card value.");
            }
        }
    }
}