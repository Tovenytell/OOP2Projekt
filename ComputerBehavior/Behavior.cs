public abstract class Behavior
{
    IPointSystem pointSystem;
    public Values lastAskedValue;

    public List<Values> CheckAvailableValues(Player computerPlayer)
    {
        //från chat
        // Filter out the last asked value from the current hand
        List<Values> availableValues = computerPlayer.hand
            .Select(card => card.Value)
            .Distinct() // Get distinct values from the hand
            .Where(value => value != lastAskedValue) // Avoid the last asked value
            .ToList();

        if (availableValues.Count == 0)
        {
            availableValues = computerPlayer.hand.Select(card => card.Value).Distinct().ToList();
        }

        return availableValues;
    }

    
    public bool IsSame(int prevnumber, int currentnumber){
        if (prevnumber == currentnumber) 
        {
            return true;
        }

        return false;
    }
    // public List<Cards> AskForCard(Deck hand){}
    
}