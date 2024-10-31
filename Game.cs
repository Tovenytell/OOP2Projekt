using System.Linq;

public class Game
{
    public Deck stock; 


    Values valueToAskFor = 0;

    public UI UI;
    private HumanPlayer humanPlayer;
    private ComputerPlayer computerPlayer;

    private Printer<Card> cardPrint = new Printer<Card>();
    private Printer<int> intPrint = new Printer<int>();
    private Printer<string> stringPrint = new Printer<string>();

    public int playCounter = 0;

    private Behavior computerBehavior;
    private IPointSystem pointSystem;
    public void Run()
    {
        stock = new Deck();
        
        //skapa ny player 
        Console.WriteLine("Welcome to our pond, let's go fishing ;D What's your name bestie?");
        string humanPlayerName = Console.ReadLine();
        Console.WriteLine("Vad ska din motståndare heta?");
        string computerPlayerName = Console.ReadLine();

        Console.WriteLine("\nVill du köra simple eller complex point system?");
        if (Console.ReadLine() == "simple")
        {
            pointSystem = new SimplePointSystem();
        }
        else
        {
            pointSystem = new ComplexPointSystem();
        }

        humanPlayer = new HumanPlayer(humanPlayerName);
        computerPlayer = new ComputerPlayer(computerPlayerName); 

        Console.WriteLine("Vill du köra random behavior?");
        if (Console.ReadLine() == "ja")
        {
            computerBehavior = new RandomBehavior(pointSystem, humanPlayer, computerPlayer);
        }
        else
        {
            computerBehavior = new SmartBehavior(pointSystem, humanPlayer, computerPlayer);
        }

        
        Console.WriteLine(humanPlayer.Name + computerPlayer.Name);
        InitialDeal();

        computerPlayer.SetBehavior(computerBehavior); 
        
        while(stock.Count != 0 || humanPlayer.hand.Count != 0 || computerPlayer.hand.Count != 0)
        {
            Console.WriteLine($"\nKort kvar i leken: {stock.Count}");

            if(playCounter % 2 == 0)
            {
                
                HumanPlayerTurn();
            }

            else 
            {
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

        Console.WriteLine("\n\n\n\nHuman player turn:");

        Console.WriteLine("\nHuman player hand:");
        
        PrintHand(humanPlayer.hand);

        Console.Write("Lista av humans fyratal: ");
        foreach (int number in humanPlayer.listOfQuartettes)
        {
            Console.Write($"{number}, ");
        }

        Console.WriteLine("\n\nCompHand: ");
        PrintHand(computerPlayer.hand);
        
        Console.Write("Lista av comps fyratal: ");
        foreach (int number in computerPlayer.listOfQuartettes)
        {
            Console.Write($"{number}, ");
        }

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

        if (!(humanPlayer.handIsEmpty() && stock.Count == 0))
        {
                if (humanPlayer.handIsEmpty())
            {
                humanPlayer.TakeCard(stock.Deal());
            }

            if (computerPlayer.handIsEmpty())
            {
                humanPlayer.TakeCard(stock.Deal());
            }

            if (!pulledOutValuesIsEmpty)
            {
                HumanPlayerTurn();
            }

        }

        computerBehavior.CompareScore();
    }

    public void ComputerPlayerTurn()
    {
        if (computerPlayer.handIsEmpty())
        {
            computerPlayer.TakeCard(stock.Deal());
        }

        Console.WriteLine("\n\n\nComputer player turn:");
        Console.WriteLine("\nHumanHand: ");
        foreach(Card card in humanPlayer.hand)
        {
            Console.WriteLine(card);
        }
        
        Console.Write("\nLista av humans fyratal: ");
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

        valueToAskFor = computerBehavior.AskForCard(availableValues);

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

         
        if (!(computerPlayer.handIsEmpty() && stock.Count == 0))
        {
            if (computerPlayer.handIsEmpty())
            {
                computerPlayer.TakeCard(stock.Deal());
            }
            if (humanPlayer.handIsEmpty())
            {
                humanPlayer.TakeCard(stock.Deal());
            }

            Console.WriteLine();
            

            if (!pulledOutValuesIsEmpty)
            {
                ComputerPlayerTurn();
            }
        }

        computerBehavior.CompareScore();


    }

    // Dela ut 4 kort var till varje spelare
    public void InitialDeal()
    {
        stock.Shuffle();
        for (int i = 0; i < 4; i++)
        {
            humanPlayer.TakeCard(stock.Deal());
            computerPlayer.TakeCard(stock.Deal());

        }
    }

    public void PrintHand(List<Card> hand)
    {
        foreach(Card card in hand)
        {
            cardPrint.PrintCard(card);
        }
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