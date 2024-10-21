public class Game
{
    //private Dictionary<Values, Player> quartettes; 
    public Deck stock; 

    private HumanPlayer humanPlayer;
    private ComputerPlayer computerPlayer;

    // public Game(string playerName)
    // {

    //     Deck stock = new Deck();
    //     // HumanPlayer humanPlayer = new HumanPlayer(playerName);
    //     // ComputerPlayer computerPlayer = new ComputerPlayer();

    // }
    

    //public Game()
    public void Run()
    {
        //skapar dictionary för att kunna spara resultatet, vilken spelare 
        //som har vilka kvartetter 
        
        //quartettes = new Dictionary<Values, Player>();
        
        stock = new Deck();
        InitialDeal();
        
        // //skapa ny player 
        Console.WriteLine("Welcome to our pond, let's go fishing ;D What's your name bestie?");
        string playerName = Console.ReadLine();

        HumanPlayer humanPlayer = new HumanPlayer(playerName);
        ComputerPlayer computerPlayer = new ComputerPlayer();

        Console.WriteLine(humanPlayer.Name + computerPlayer.Name);

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
                    //-> om 4tal -> lägg ner kort annars nästas tur



    }

    // Dela ut 4 kort var till varje spelare
    // !!ATT LÖSA!! : syntax? Kan vi använda add på typen Deck? 
    public void InitialDeal()
    {
        
        stock.Shuffle();
        for (int i = 0; i < 4; i++)
        {
            humanPlayer.TakeCard(stock.Deal());
            computerPlayer.TakeCard(stock.Deal());

        }

        humanPlayer.SortHand(); //funkar detta??
        computerPlayer.SortHand();

        // {
        //     stock[i] = humanPlayer.hand.Add();
        //     i++;

        //     stock[i] = computerPlayer.hand.Add();
        // }
    }
}