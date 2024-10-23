public class SmartBehavior : Behavior
{
    public Values AskSmart(List<Values> availableValues)
    {

        //SKA ÄNDRAS!!!
         Random random = new Random();
        Values chosenValue = availableValues[random.Next(availableValues.Count)];

        // Store the chosen value as the last asked value
        lastAskedValue = chosenValue;

        return chosenValue;
    }
}