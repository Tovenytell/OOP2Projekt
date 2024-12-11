public abstract class Behavior //KRAV #2
//Koncept: Strategy pattern
//Vi använder det genom att det finns två olika typer av Behavior (Smart och Random) som ärver av den abstrakta klassen Behavior. 
//Vi har alltså abstraherat en ComputerPlayers olika beteenden till olika typer av Behaviors
//Vi använder detta för att kunna skapa variande spel och ett program som enkelt går att underhålla och utveckla
//om man vill ha fler beteenden i framtiden
{
    //public IPointSystem pointSystem;
    public Values lastAskedValue;

    // Player humanPlayer;
    // Player computerPlayer;

    public int Score {get; private set;} = 0;

    // public Behavior(IPointSystem pSystem, Player hPlayer, Player cPlayer)
    // {
    //     pointSystem = pSystem;
    //     humanPlayer = hPlayer;
    //     computerPlayer = cPlayer;
    // }
    public List<Values> CheckAvailableValues(Player player)
    {
        // KRAV 5
        // Koncept: LINQ 
        // Vi använder LINQ genom att gå igenom spelarens hand och hitta korten som 
        // uppfyller ett visst krav, exempelvis att det inte får vara lika som det 
        // senast frågade kortet. 
        // Vi använder LINQ för att på ett enkelt sätt kunna filtrera korten och 
        // enkelt hitta det vi vill ha/identifiera det vi inte vill ha. 
        List<Values> availableValues = player.hand
            .Select(card => card.Value)
            .Where(value => value != lastAskedValue) 
            .ToList();

        if (availableValues.Count == 0)
        {
            availableValues = player.hand.Select(card => card.Value).Distinct().ToList();
        }

        return availableValues;
    }

    public int CompareScore(IPointSystem pointSystem, Player humanPlayer, Player computerPlayer)
    {
        int humanPlayerPoints = pointSystem.CalculatePoints(humanPlayer.hand.listOfQuartettes);
        int computerPlayerPoints = pointSystem.CalculatePoints(computerPlayer.hand.listOfQuartettes);

        Console.WriteLine("\nYour points: " + humanPlayerPoints);
        Console.WriteLine("Torsten's points: " + computerPlayerPoints);


        
        if (humanPlayerPoints > computerPlayerPoints)
        {
            Console.WriteLine("\nYou're in the lead!");
            return 1;
        }
        else if (humanPlayerPoints < computerPlayerPoints)
        {
            Console.WriteLine("\nTorsten is in the lead!");
            return 2;
        }
        else
        {
            Console.WriteLine("\nIt's a tie!");
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