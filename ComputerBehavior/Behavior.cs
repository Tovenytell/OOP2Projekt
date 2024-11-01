public abstract class Behavior
{
    public IPointSystem pointSystem;
    public Values lastAskedValue;

    Player humanPlayer;
    Player computerPlayer;

    public int Score {get; private set;} = 0;

    public Behavior(IPointSystem pSystem, Player hPlayer, Player cPlayer)
    {
        pointSystem = pSystem;
        humanPlayer = hPlayer;
        computerPlayer = cPlayer;
    }
    public List<Values> CheckAvailableValues(Player computerPlayer)
    {
        //från chat
        // Filter out the last asked value from the current hand
        //Console.WriteLine(lastAskedValue);
        List<Values> availableValues = computerPlayer.hand
            .Select(card => card.Value)
            .Where(value => value != lastAskedValue) // Avoid the last asked value
            .ToList();

        if (availableValues.Count == 0)
        {
            availableValues = computerPlayer.hand.Select(card => card.Value).Distinct().ToList();
        }

        Console.WriteLine("\nFiltered available values: " + string.Join(", ", availableValues));

        return availableValues;
    }

    public int CompareScore()
    {
        int humanPlayerPoints = pointSystem.CalculatePoints(humanPlayer.hand.listOfQuartettes);
        int computerPlayerPoints = pointSystem.CalculatePoints(computerPlayer.hand.listOfQuartettes);

        Console.WriteLine("Human player points: " + humanPlayerPoints);
        Console.WriteLine("Computer player points: " + computerPlayerPoints);


        
        if (humanPlayerPoints > computerPlayerPoints)
        {
            Console.WriteLine("\nHuman leder!!");
            return 1;
        }
        else if (humanPlayerPoints < computerPlayerPoints)
        {
            Console.WriteLine("\nComputer leder!");
            return 2;
        }
        else
        {
            Console.WriteLine("\nDet är lika!");
            return 3;
        }
    }
    public bool IsSame(int prevnumber, int currentnumber)
    {
        if (prevnumber == currentnumber) 
        {
            return true;
        }

        return false;
    }
    
    public abstract Values AskForCard(List<Values> availableValues);
    
}