public class HelpBehavior : Behavior
{
    public IPointSystem pointSystem;
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
    // public HelpBehavior(IPointSystem pSystem, Player hPlayer, Player cPlayer) : base(pSystem, hPlayer, cPlayer)
    // {
    // }

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
                    // Exempel: Tolka "Asked for 5s" och extrahera värdet 5
                    // if (move.Action.StartsWith("Asked for "))
                    // {
                    //     //string valueStr = move.Action.Substring(10).TrimEnd('s');
                    //     string valueStr = move.Action.Substring("Asked for ".Length).TrimEnd('s', ' ', '\n', '\r');
                    //     // return int.TryParse(valueStr, out int value) && Enum.IsDefined(typeof(Values), value)
                    //     //     ? (Values)value
                    //     //     : (Values?)null;
                    //     if (int.TryParse(valueStr, out int value) && Enum.IsDefined(typeof(Values), value))
                    //     {
                    //         return (Values?)value; // Return successfully parsed value as nullable
                    //     }
                    // else
                    // {
                    //     Console.WriteLine($"Failed to parse or invalid value: {valueStr}");
                    // }
                    // }

                if (move.Action.StartsWith("Asked for ", StringComparison.OrdinalIgnoreCase))
                {
                    // Extract the card value as a string
                    string valueStr = move.Action.Substring("Asked for ".Length).TrimEnd('s', ' ', '\n', '\r');

                    // Map the string to a Values enum
                    if (ValueMapping.TryGetValue(valueStr, out Values value))
                    {
                        return (Values?)value; // Return the mapped value
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
        // Step 1: Use CheckAvailableValues to get available values from the player's hand
        List<Values> availableValues = CheckAvailableValues(player);

        // Step 2: Load previously asked values from the JSON file
        List<Values> previouslyAskedValues = GetPreviouslyAskedValues(movesFilePath);

        // Step 3: Find the intersection of available values and previously asked values
        List<Values> suggestedValues = availableValues
            .Intersect(previouslyAskedValues) // Only keep values that are in both lists
            .ToList();

        if (!suggestedValues.Any())
        {
            Console.WriteLine("No information about help exists at the moment.");
            return new List<Values>(); // Return an empty list, or alternatively return availableValues if needed
        }

            // Return the suggested values
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
        //LÄGG TILL FELHANTERING!!!!
        Values input = (Values)Enum.Parse(typeof(Values), Console.ReadLine(), true);

        return input;
    }

}