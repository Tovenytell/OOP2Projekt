
public class RandomBehavior : Behavior
{

    public RandomBehavior(IPointSystem pSystem, Player hPlayer, Player cPlayer) : base (pSystem, hPlayer, cPlayer)
    {
        
    }
    public override Values AskForCard(List<Values> availableValues)
    {
        Random random = new Random();
        Values chosenValue = availableValues[random.Next(availableValues.Count)];

        lastAskedValue = chosenValue;

        Console.WriteLine($"\n\nTorsten asks: Do you have any {chosenValue}s?");

        return chosenValue;
        
    }
}