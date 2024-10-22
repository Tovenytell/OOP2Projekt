public class Game
{
    //private Dictionary<Values, Player> quartettes; 
    public Deck stock; 

    private HumanPlayer humanPlayer;
    private ComputerPlayer computerPlayer;

    private Behavior computerBehavior;

    // public Game(string hPlayerName, string cPlayerName)
    // {

    //     Deck stock = new Deck();
    //     HumanPlayer humanPlayer = new HumanPlayer(hPlayerName);
    //     ComputerPlayer computerPlayer = new ComputerPlayer(cPlayerName);

    // }
    
    public void Run()
    {
        //skapar dictionary för att kunna spara resultatet, vilken spelare 
        //som har vilka kvartetter 
        
        stock = new Deck();
        
        // //skapa ny player 
        Console.WriteLine("Welcome to our pond, let's go fishing ;D What's your name bestie?");
        string humanPlayerName = Console.ReadLine();
        Console.WriteLine("Vad ska din motståndare heta?");
        string computerPlayerName = Console.ReadLine();

        humanPlayer = new HumanPlayer(humanPlayerName);
        computerPlayer = new ComputerPlayer(computerPlayerName);

        Console.WriteLine(humanPlayer.Name + computerPlayer.Name);
        InitialDeal();

        //vill du köra random? ja:
        Behavior computerBehavior = new RandomBehavior();
        computerBehavior.CheckAvailableValues(computerPlayer);

        Console.WriteLine("Human player hand: ");
        foreach(Card card in humanPlayer.hand)
        {
            Console.WriteLine(card);
        }

        Console.WriteLine("Comp player hand: ");
        foreach(Card card in computerPlayer.hand)
        {
            Console.WriteLine(card);
        }

        Console.WriteLine("Lake: ");
        foreach(Card card in stock){
            Console.WriteLine(card);
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