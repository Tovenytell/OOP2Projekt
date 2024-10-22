public class RandomBehavior : Behavior
{
    public Values AskRandomValue(List<Values> availableValues)
    {
        Random random = new Random();
        Values chosenValue = availableValues[random.Next(availableValues.Count)];

        // Store the chosen value as the last asked value
        lastAskedValue = chosenValue;

        return chosenValue;
        
    }
}