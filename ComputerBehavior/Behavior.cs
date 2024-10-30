public abstract class Behavior
{
    IPointSystem pointSystem;
    public Values lastAskedValue;

    public Behavior(IPointSystem pSystem)
    {
        pointSystem = pSystem;
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