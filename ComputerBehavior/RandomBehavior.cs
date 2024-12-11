
public class RandomBehavior : Behavior
{

    public readonly List<PreviousMoves> moves;

    public RandomBehavior(List<PreviousMoves> moves)/*, IPointSystem pSystem, Player hPlayer, Player cPlayer) : base (pSystem, hPlayer, cPlayer)*/
    {
        this.moves = moves;
    }
    public override Values AskForCard(List<Values> availableValues)
    {
        Random random = new Random();
        Values chosenValue = availableValues[random.Next(availableValues.Count)];

        lastAskedValue = chosenValue;

        Console.WriteLine($"\n\nTorsten asks: Do you have any {chosenValue}s?");
        moves.Add(new PreviousMoves
        {
            PlayerName = "Torsten",
            Action = $"Asked for {chosenValue}s"
        });
        return chosenValue;
        
    }
}