public class Game
{
    //private Dictionary<Values, Player> quartettes; 
    public Deck stock; 

    private HumanPlayer humanPlayer;
    private ComputerPlayer computerPlayer;

    public int playCounter = 0;

    private Behavior computerBehavior;

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
            //else
            computerBehavior = new SmartBehavior();
        }
        
        computerPlayer.SetBehavior(computerBehavior); //lagt till
        
        
        while(stock.Count != 0 && humanPlayer.hand.Count != 0 && computerPlayer.hand.Count != 0)
        {
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

        //Human player ska välja vilken typ av behavior computer player ska ha
        //Human player ska välja vilken typ av point system

        //Starta loop gällande spelarnas turer
            //Kolla innan varje tur startar om det finns kort i båda spelarnas händer
            //om inte, ska spelaren utan kort kunna ta upp
            //om korten är slut i bådas händer och i sjön är spelet slut

            //ena spelarens tur börjar
                //frågar efter ett kort
                //får kort -> korten läggs in på rätt plats -> kollar om man har 4tal
                    //-> om 4tal -> lägg ner kort annars fråga igen
                //inte får kort -> tar kort från sjön -> kollar om man har 4tal
                    //-> om 4tal -> lägg ner kort annars nästas



    }

    public void HumanPlayerTurn()
    {
        Console.WriteLine();
        Console.WriteLine("Human player turn:");
        Console.WriteLine();

        Console.WriteLine("CompHand: ");
        foreach(Card card in computerPlayer.hand)
        {
            Console.WriteLine(card);
        }

        Console.WriteLine();

        Console.WriteLine("Vad vill du fråga efter?");
        Values valueToAskFor = (Values)int.Parse(Console.ReadLine());
        humanPlayer.ReceiveAskedCards(computerPlayer.PullOutValues(valueToAskFor));
        humanPlayer.SortHand();
        // humanPlayer.HasQuartette(humanPlayer.hand);

        //kolla efter 4tal och om man har det ska det läggas ner/lagras för point system sen

        Console.WriteLine();
        Console.WriteLine("Human hand: ");
        foreach(Card card in humanPlayer.hand){
            Console.WriteLine(card);
        }
    }

    public void ComputerPlayerTurn()
    {
        Console.WriteLine();
        Console.WriteLine("Computer player turn:");
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


        computerPlayer.ReceiveAskedCards(humanPlayer.PullOutValues(valueToAskFor));
        computerPlayer.SortHand();

        Console.WriteLine();
        Console.WriteLine("CompHand: ");
        foreach(Card card in computerPlayer.hand)
        {
            Console.WriteLine(card);
        }

        Console.WriteLine("Human player hand:");
        Console.WriteLine();
        foreach(Card card in humanPlayer.hand){
            Console.WriteLine(card);
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
}