using System.Linq;

public class Game
{
    //private Dictionary<Values, Player> quartettes; 
    public Deck stock; 

    private HumanPlayer humanPlayer;
    private ComputerPlayer computerPlayer;

    public int playCounter = 0;

    private Behavior computerBehavior;

    // private IPointSystem simplePointSystem;
    // private IPointSystem complexPointSystem;
    private IPointSystem pointSystem;

    // public Game(string hPlayerName, string cPlayerName)
    // {

    //     Deck stock = new Deck();
    //     HumanPlayer humanPlayer = new HumanPlayer(hPlayerName);
    //     ComputerPlayer computerPlayer = new ComputerPlayer(cPlayerName);

    // }
    
    public void Run()
    {
        stock = new Deck();
        
        //skapa ny player 
        Console.WriteLine("Welcome to our pond, let's go fishing ;D What's your name bestie?");
        string humanPlayerName = Console.ReadLine();
        Console.WriteLine("Vad ska din motståndare heta?");
        string computerPlayerName = Console.ReadLine();

        humanPlayer = new HumanPlayer(humanPlayerName);
        computerPlayer = new ComputerPlayer(computerPlayerName);

        Console.WriteLine(humanPlayer.Name + computerPlayer.Name);
        InitialDeal();

        //vill du köra random? if yes:
        Console.WriteLine("Vill du köra random behavior?");
        if (Console.ReadLine() == "ja")
        {
            computerBehavior = new RandomBehavior();
        }
        else
        {
            computerBehavior = new SmartBehavior();
        }
        
        computerPlayer.SetBehavior(computerBehavior); 
        
        Console.WriteLine("\nVill du köra simple eller complex point system?");
        if (Console.ReadLine() == "simple")
        {
            pointSystem = new SimplePointSystem();
        }
        else
        {
            pointSystem = new ComplexPointSystem();
        }

        
        while(stock.Count != 0 || humanPlayer.hand.Count != 0 || computerPlayer.hand.Count != 0)
        {
            Console.WriteLine($"\nKort kvar i leken: {stock.Count}");

            if(playCounter % 2 == 0)
            {
                
                HumanPlayerTurn();
            }

            else 
            {
                //Console.WriteLine($"\nKort kvar i leken: {stock.Count}");
                ComputerPlayerTurn();
            }
            
            playCounter++;
            
        }

        AnnounceWinner(pointSystem);

    }

    public void HumanPlayerTurn()
    {
        if (humanPlayer.handIsEmpty())
        {
            humanPlayer.TakeCard(stock.Deal());
        }

        Console.WriteLine();
        Console.WriteLine("\n\n\n\nHuman player turn:");
        Console.WriteLine();

        Console.WriteLine();
        Console.WriteLine("Human player hand:");
        
        foreach(Card card in humanPlayer.hand){
            Console.WriteLine(card);
        }

        Console.Write("Lista av humans fyratal: ");
        foreach (int number in humanPlayer.listOfQuartettes)
        {
            Console.Write($"{number}, ");
        }


        Console.WriteLine("\n\nCompHand: ");
        foreach(Card card in computerPlayer.hand)
        {
            Console.WriteLine(card);
        }
        Console.Write("Lista av comps fyratal: ");
        foreach (int number in computerPlayer.listOfQuartettes)
        {
            Console.Write($"{number}, ");
        }


        Console.WriteLine();
        Values valueToAskFor = 0;
        bool askedInHand = false;
       
        while (askedInHand == false)
        {
            Console.WriteLine("\nVad vill du fråga efter? Kortet måste finnas på din hand");
            valueToAskFor = (Values)int.Parse(Console.ReadLine());
        
        for (int i = 0; i < humanPlayer.hand.Count(); i++)
        {
            if (humanPlayer.hand[i].Value == valueToAskFor)
            {
                askedInHand = true;
            }
        }
        }
        
        List <Card> pulledOutValues = computerPlayer.PullOutValues(valueToAskFor);
        bool pulledOutValuesIsEmpty = !pulledOutValues.Any();
        if (pulledOutValuesIsEmpty)
        {
            humanPlayer.TakeCard(stock.Deal());
        }
        else
        {
            humanPlayer.ReceiveAskedCards(pulledOutValues);
        }

        humanPlayer.SortHand();
        humanPlayer.HasQuartette();
        // humanPlayer.HasQuartette(humanPlayer.hand);

        //kolla efter 4tal och om man har det ska det läggas ner/lagras för point system sen

        if (humanPlayer.handIsEmpty())
        {
            humanPlayer.TakeCard(stock.Deal());
        }

        if (!pulledOutValuesIsEmpty)
        {
            HumanPlayerTurn();
        }
        
    }

    public void ComputerPlayerTurn()
    {
        if (computerPlayer.handIsEmpty())
        {
            computerPlayer.TakeCard(stock.Deal());
        }
        Console.WriteLine();
        Console.WriteLine("\n\n\n\nComputer player turn:");

        Console.WriteLine();
        Console.WriteLine("HumanHand: ");
        foreach(Card card in humanPlayer.hand)
        {
            Console.WriteLine(card);
        }
        
        Console.Write("Lista av humans fyratal: ");
        foreach (int number in humanPlayer.listOfQuartettes)
        {
            Console.Write($"{number}, ");
        }  


        Console.WriteLine("\n\nCompHand: ");
        foreach(Card card in computerPlayer.hand)
        {
            Console.WriteLine(card);
        }
        Console.Write("Lista av comps fyratal: ");
        foreach (int number in computerPlayer.listOfQuartettes)
        {
            Console.Write($"{number}, ");
        }

        List<Values> availableValues = computerBehavior.CheckAvailableValues(computerPlayer);
        Values valueToAskFor;

        // Check if the behavior is RandomBehavior and cast it
        if (computerBehavior is RandomBehavior randomBehavior)
        {
            // Use AskRandomValue from RandomBehavior
            valueToAskFor = randomBehavior.AskRandomValue(availableValues);
        }
        else if (computerBehavior is SmartBehavior smartBehavior)
        {
            valueToAskFor = smartBehavior.AskSmart(availableValues);
        }
        else
        {
            valueToAskFor = availableValues[0];
            Console.WriteLine("Kom inte hit");
        }

        List <Card> pulledOutValues = humanPlayer.PullOutValues(valueToAskFor);
        bool pulledOutValuesIsEmpty = !pulledOutValues.Any();
        if (pulledOutValuesIsEmpty)
        {
            computerPlayer.TakeCard(stock.Deal());
        }
        else
        {
            computerPlayer.ReceiveAskedCards(pulledOutValues);
        }


        computerPlayer.SortHand();
        computerPlayer.HasQuartette();

         

        if (computerPlayer.handIsEmpty())
        {
            computerPlayer.TakeCard(stock.Deal());
        }

        Console.WriteLine();
        

        if (!pulledOutValuesIsEmpty)
        {
            ComputerPlayerTurn();
        }


    }

    // Dela ut 4 kort var till varje spelare
    public void InitialDeal()
    {
        
        //Deck cards = stock.Shuffle();
        stock.Shuffle();
        for (int i = 0; i < 4; i++)
        {
            humanPlayer.TakeCard(stock.Deal());
            computerPlayer.TakeCard(stock.Deal());

        }

        // humanPlayer.SortHand(); //funkar detta??
        // computerPlayer.SortHand();
    }

    public void AnnounceWinner(IPointSystem pointSystem)
    {
        int humanPlayerPoints = pointSystem.CalculatePoints(humanPlayer.listOfQuartettes);
        int computerPlayerPoints = pointSystem.CalculatePoints(computerPlayer.listOfQuartettes);

        if (humanPlayerPoints > computerPlayerPoints)
        {
            Console.WriteLine($"Grattis {humanPlayer.name} du har vunnit!");
        }
        else
        {
            Console.WriteLine("Datorn har vunnit!");
        }

    }
}