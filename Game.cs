public class Game
{
    //private Dictionary<Values, Player> quartettes; 
    public Deck stock; 

    private HumanPlayer humanPlayer;
    private ComputerPlayer computerPlayer;

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


        // //sortera spelarens hand 
        // player.SortHand();
    }

    // Dela ut 4 kort var till varje spelare
    // !!ATT LÖSA!! : syntax? Kan vi använda add på typen Deck? 
    public void InitialDeal()
    {
        stock.Shuffle();
        for (int i = 0; i < 5; i++)
        {
            humanPlayer.TakeCard(stock.Deal());
            computerPlayer.TakeCard(stock.Deal());

        }

        // {
        //     stock[i] = humanPlayer.hand.Add();
        //     i++;

        //     stock[i] = computerPlayer.hand.Add();
        // }
    }
}