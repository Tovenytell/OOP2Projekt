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
        Console.WriteLine(lastAskedValue);
        List<Values> availableValues = computerPlayer.hand
            .Select(card => card.Value)
            .Where(value => value != lastAskedValue) // Avoid the last asked value
            .ToList();

        if (availableValues.Count == 0)
        {
            availableValues = computerPlayer.hand.Select(card => card.Value).Distinct().ToList();
        }

        Console.WriteLine("Filtered available values: " + string.Join(", ", availableValues));

        return availableValues;
    }

    public void CompareScore()
    {
        if (pointSystem.CalculatePoints(humanPlayer.listOfQuartettes) > pointSystem.CalculatePoints(humanPlayer.listOfQuartettes ))
        {
            Console.WriteLine("\nHuman leder!!");
        }
        else if (pointSystem.CalculatePoints(humanPlayer.listOfQuartettes) < pointSystem.CalculatePoints(humanPlayer.listOfQuartettes ))
        {
            Console.WriteLine("\nComputer leder!");
        }
        else
        {
            Console.WriteLine("\nDet är lika!");
        }
    }

    // public void UpdateScore(List<int> listOfQuartettes)
    // {
    //     Score += pointSystem.CalculatePoints(listOfQuartettes);
    // }


    

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