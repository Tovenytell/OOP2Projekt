using System.Linq;

public class Game
{
    
    public Deck stock; 
    private static HumanPlayer humanPlayer;
    private static ComputerPlayer computerPlayer;

    // Declare the moves list
    private readonly List<PreviousMoves> moves = new List<PreviousMoves>();
    
    private readonly FileHandler<List<PreviousMoves>> moveHandler = new FileHandler<List<PreviousMoves>>();

    // private readonly FileHandler<List<PreviousScores>> scoreHandler = new FileHandler<List<PreviousScores>>();
    // private const string ScoreLogFilePath = "scores.json";
    // private readonly List<PreviousScores> highScores = new List<PreviousScores>();

    private readonly FileHandler<List<PlayerWins>> winsHandler = new FileHandler<List<PlayerWins>>();
    private const string WinsLogFilePath = "wins.json";
    private readonly List<PlayerWins> playerWins = new List<PlayerWins>();

    // private readonly List<PlayerWins> playerWins = new List<PlayerWins>
    // {
    //     new PlayerWins { Player = humanPlayer, Wins = 0 },
    //     new PlayerWins { Player = computerPlayer, Wins = 0 }
    // };

    public Player player;
    private Printer<Card> cardPrint = new Printer<Card>();
    private Printer<int> intPrint = new Printer<int>();
    private Printer<string> stringPrint = new Printer<string>();

    IPointSystem pointSystem;
    public int playCounter = 0;
    private Behavior computerBehavior;
    public void Run()
    {
        moves.Clear();
        moveHandler.Save(moves, "moves.json");

        Console.Clear();
        stock = new Deck();
        
        //Hälsa och skapa players och be HumanPlayer välja PointSystem och datorns Behavior med val och pilar
        string humanPlayerName = string.Empty;
        while (string.IsNullOrWhiteSpace(humanPlayerName))
        {
                
                string welcometext = "Welcome to our pond where we play go fish! Your goal in this game is to get more points than your competitor.";
                
                string gamerules = "In the game you will be able to choose between simple- and complex point system, and random or smart computer behavior.";
                string gamerules2 = "Simple pointsystem gives one point per collected quartette, and complex point system gives for example 5 points if you have a quartette of fives";
                Console.ForegroundColor = ConsoleColor.Green;
                DrawBoxAroundText(welcometext);
                Console.ResetColor();
                Console.WriteLine("\nGame rules:");
                DrawBoxAroundText(gamerules);
                DrawBoxAroundText(gamerules2);
                Console.WriteLine("\nLet's go fishing! What's your name?");
                humanPlayerName = Console.ReadLine();
                
                if (string.IsNullOrWhiteSpace(humanPlayerName))
                {
                    Console.WriteLine("Name cannot be empty. Please try again.");
                }
        }
        string pointSystemChoice = DisplayMenu(new string[] { "Simple point system", "Complex point system" });
        pointSystem = pointSystemChoice == "Simple point system" ? new SimplePointSystem() : new ComplexPointSystem();

        humanPlayer = new HumanPlayer();
        computerPlayer = new ComputerPlayer();

        humanPlayer.Name = "Human player";
        computerPlayer.Name = "Torsten";

        string behaviorChoice = DisplayMenu(new string[] { "Random computer behavior", "Smart computer behavior" });
        computerBehavior = behaviorChoice == "Random computer behavior" 
            ? new RandomBehavior(moves/*, pointSystem, humanPlayer, computerPlayer*/) 
            : new SmartBehavior(moves/*, pointSystem, humanPlayer, computerPlayer*/);

        Console.WriteLine("Do you want help deciding what card to ask for? (y/n)");
        string helpDesicion = Console.ReadLine();
        if (helpDesicion?.ToLower() == "y")
        {
            humanPlayer.SetBehavior(new HelpBehavior());
        }
        
        Console.WriteLine($"\nYou're playing against the fishmaster {computerPlayer.Name}! Good luck!\n");

        InitialDeal();

        computerPlayer.SetBehavior(computerBehavior); 
        
        
        
        while(stock.Count != 0 || humanPlayer.hand.Any() || computerPlayer.hand.Any()) 
        {
            Console.WriteLine($"\nCards left in the pond: {stock.Count}");

            if(playCounter % 2 == 0)
            {
                PlayerTurn(humanPlayer);
                Console.Clear();

            }

            else 
            {
                PlayerTurn(computerPlayer);
            }

            playCounter++;
            moveHandler.Save(moves, "moves.json");
            
        }

        //AnnounceWinner(computerBehavior);
        Player winner = AnnounceWinner(computerBehavior);
        UpdateWins(winner);
        SaveWins();
        PromptPreviousWins();
        Console.WriteLine("Would you like to play again? y/n");
        string input = Console.ReadLine();
        if (input?.ToLower() == "y")
        {
            
            Run();
        }
        // else
        // {
        //     // Final cleanup if game is exiting
        //     moves.Clear(); // Clear the in-memory list
        //     moveHandler.Save(moves, "moves.json"); // Save the empty list to reset the file
        // }
        
    }

    public void PlayerTurn(Player player)
    {
        

        Player opponent = player is HumanPlayer ? (Player)computerPlayer : humanPlayer;

        if (player.handIsEmpty())
        {
            player.TakeCard(stock.Deal());
        }

        if (opponent.handIsEmpty())
        {
            opponent.TakeCard(stock.Deal());
        }

        
        PrintHandsAndQuartettes(player);
        Values valueToAskFor = 0;

        if (player is HumanPlayer)
        {
            if (player.behavior is HelpBehavior helpBehavior)
            {
                List<Values> suggestedValues = helpBehavior.GetSuggestedValues(player, "moves.json");
                
                valueToAskFor = player.behavior.AskForCard(suggestedValues);//detta ska sparas i en Values variabel?
                //denna variabel ska sen användas för att plocka det kortet från datorns hand
            }

            else 
            {
                bool askedInHand = false;
        
                while (!askedInHand)
                {
                    bool validInput = false;
                    
                    while (!validInput)
                    {
                            
                        Console.WriteLine("Do you want to see the previous moves? (y/n)");
                        string input = Console.ReadLine();
                        if (input?.ToLower() == "y")
                        {
                            try
                            {
                                var previousMoves = moveHandler.Load("moves.json");
                                moveHandler.DisplayLog(previousMoves);
                            }
                            catch (FileNotFoundException)
                            {
                                Console.WriteLine("No previous moves found.");
                            }
                        }

                        
                        

                        
                        Console.WriteLine("\nWhat card would you like to ask for? The value must already be in your hand");
                        Console.WriteLine("Enter a number between 1 and 13");
                        
                        string input2 = Console.ReadLine();
                        if (int.TryParse(input2, out int parsedValue))
                        {
                            
                            if (Enum.IsDefined(typeof(Values), parsedValue))
                            {
                                valueToAskFor = (Values)parsedValue;
                                validInput = true; 
                            }
                            else
                            {
                                Console.WriteLine("Incorrect input, the value has to be between 1 and 13.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Incorrect input, please try again");
                        }
                    }
                        foreach (Card card in humanPlayer.hand)
                        {
                                if (card.Value == valueToAskFor)
                                {
                                    askedInHand = true;
                                    break;
                                }
                        }
                            
                        if (!askedInHand)
                        {
                            Console.WriteLine("You don't have that card on your hand, please try again");
                            valueToAskFor = 0; 
                        }
                }
            }
        }
        else 
        {
            List<Values> availableValues = computerBehavior.CheckAvailableValues((ComputerPlayer)player);
            computerPlayer.Think();
            valueToAskFor = player.behavior.AskForCard(availableValues);
        }

        List<Card> pulledOutValues = opponent.hand.PullOutValues(valueToAskFor);
        bool pulledOutValuesIsEmpty = !pulledOutValues.Any();

        if (pulledOutValuesIsEmpty)
        {
            player.TakeCard(stock.Deal());
        }
        else
        {
            player.ReceiveAskedCards(pulledOutValues);
        }

        
        player.hand.HasQuartette();

        
        if (!(player.handIsEmpty() && stock.Count == 0))
        {
            if (player.handIsEmpty())
            {
                player.TakeCard(stock.Deal());
            }
            if (opponent.handIsEmpty())
            {
                opponent.TakeCard(stock.Deal());
            }

            if (!pulledOutValuesIsEmpty)
            {
                computerBehavior.CompareScore(pointSystem, humanPlayer, computerPlayer);
                PlayerTurn(player);
            }
            else
            {
                computerBehavior.CompareScore(pointSystem, humanPlayer, computerPlayer);
                
            }
        }
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

    public void PrintHand(Hand hand)
    {
        foreach(Card card in hand)
        {
            cardPrint.PrintCard(card);
        }
    }

    public void PrintQuartettes(List<int> quartettes)
    {
        foreach(int quartette in quartettes)
        {
            intPrint.PrintHorizontally(quartette);
        }
    }

    public Player AnnounceWinner(Behavior computerBehavior)
    {
        int winner = computerBehavior.CompareScore(pointSystem, humanPlayer, computerPlayer);
        Console.ForegroundColor = ConsoleColor.Yellow;


        if (winner == 1)
        {
            stringPrint.PrintHorizontally($"\nCongrats! {humanPlayer.Name} won!\n\n");
            return humanPlayer;

        }
        else if (winner == 2)
        {
            stringPrint.PrintHorizontally("\nTorsten won!\n\n");
            return computerPlayer;
        }
        else
        {
            return null;
        }
        // else if (winner == 3)
        // {
        //     stringPrint.PrintHorizontally("\nIt's a tie!\n\n");
        //     return "Draw";
        // }
        // else
        // {
        //     return ".";
        // }

        //Console.ResetColor();
    }

     private void UpdateWins(Player winner)
    {
    if (winner == null)
    {
        Console.WriteLine("The game ended in a draw. No wins recorded.");
        return;
    }

    // Find the winner in the playerWins list by reference
    var playerWin = playerWins.Find(p => p.Player?.Name == winner.Name);
    if (playerWin != null)
    {
        playerWin.Wins++;
    }
    else
    {
        playerWins.Add(new PlayerWins { Player = winner, Wins = 1 });
    }


        //Console.WriteLine($"{winner.Name} has won this game!");
    }

    private void SaveWins()
    {
        winsHandler.Save(playerWins, WinsLogFilePath);
        //Console.WriteLine("Win records saved.");
    }

    private void PromptPreviousWins()   
{       
    // foreach (var win in playerWins)
    // {
    //     Console.WriteLine(win == null ? "Null entry in playerWins" : $"\n{win.Player?.Name ?? "null"}'s wins: {win.Wins}");
    // }

    // Console.WriteLine("Do you want to see the win records? (yes/no)");
    // string input = Console.ReadLine();
    // if (input?.ToLower() == "yes")
    // {
    //     if (!playerWins.Any())
    //     {
    //         Console.WriteLine("No win records found.");
    //         return;
    //     }

        Console.WriteLine("Win Records:");
        foreach (var record in playerWins)
        {
            if (record == null || record.Player == null)
            {
                Console.WriteLine("Invalid record found.");
                continue;
            }
            Console.WriteLine(record); // Calls PlayerWins.ToString()
        }
    // }
}


    static string DisplayMenu(string[] options)
    {
        int selectedIndex = 0;
        ConsoleKey key;

        do
        {
            Console.Clear();
            for (int i = 0; i < options.Length; i++)
            {
                if (i == selectedIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("-> " + options[i]);
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine("   " + options[i]);
                }
            }

            key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.UpArrow)
            {
                selectedIndex = (selectedIndex == 0) ? options.Length - 1 : selectedIndex - 1;
            }
            else if (key == ConsoleKey.DownArrow)
            {
                selectedIndex = (selectedIndex == options.Length - 1) ? 0 : selectedIndex + 1;
            }

        } while (key != ConsoleKey.Enter);

        return options[selectedIndex];
    }

    public void PrintHandsAndQuartettes(Player player)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\n\n\n{(player is HumanPlayer ? "Your" : "Torsten´s")} turn:");
        Console.ResetColor();
        
        Console.WriteLine("\nYour hand:");
        PrintHand(humanPlayer.hand);

        Console.WriteLine("\n\n--------------------------");
        Console.Write($"Your quartettes: ");
        Console.ForegroundColor = ConsoleColor.Green;
        PrintQuartettes(humanPlayer.hand.listOfQuartettes);
        Console.ResetColor();
        
        Console.Write($"\n{computerPlayer.Name}'s quartettes: ");
        Console.ForegroundColor = ConsoleColor.Green;
        PrintQuartettes(computerPlayer.hand.listOfQuartettes);
        Console.ResetColor();
        Console.WriteLine("\n--------------------------");


    }


    //från chat
    static void DrawBoxAroundText(string text)
    {
        int padding = 2; // Extra space on either side of the text
        int width = text.Length + padding * 2; // Total width of the box

        // Top border
        Console.WriteLine("+" + new string('-', width) + "+");

        // Empty line above the text
        Console.WriteLine("|" + new string(' ', width) + "|");

        // Text line with padding
        Console.WriteLine("|" + new string(' ', padding) + text + new string(' ', padding) + "|");

        // Empty line below the text
        Console.WriteLine("|" + new string(' ', width) + "|");

        // Bottom border
        Console.WriteLine("+" + new string('-', width) + "+");
    }
}
    