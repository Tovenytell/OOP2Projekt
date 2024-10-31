
public class RandomBehavior : Behavior
{

    public RandomBehavior(IPointSystem pSystem, Player hPlayer, Player cPlayer) : base (pSystem, hPlayer, cPlayer)
    {
        
    }
    // public Values AskRandomValue(List<Values> availableValues)
    // {
    //     Random random = new Random();
    //     Values chosenValue = availableValues[random.Next(availableValues.Count)];

    //     // Store the chosen value as the last asked value
    //     lastAskedValue = chosenValue;

    //     Console.WriteLine($"\nComputer asks: Do you have any {chosenValue}s?");

    //     return chosenValue;

    // }

    public override Values AskForCard(List<Values> availableValues)
    {
        Random random = new Random();
        Values chosenValue = availableValues[random.Next(availableValues.Count)];

        // Store the chosen value as the last asked value
        lastAskedValue = chosenValue;

        Console.WriteLine($"\nComputer asks: Do you have any {chosenValue}s?");

        return chosenValue;
        
    }
}