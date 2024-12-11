// public class StandardGame : Game
// {

//     public void Run()
//     {

//         Player humanPlayer = new HumanPlayer(humanPlayerName);
//         Player computerPlayer = new ComputerPlayer("Torsten");

//         string behaviorChoice = DisplayMenu(new string[] { "Random computer behavior", "Smart computer behavior" });
//         computerBehavior = behaviorChoice == "Random computer behavior" 
//             ? new RandomBehavior(pointSystem, humanPlayer, computerPlayer) 
//             : new SmartBehavior(pointSystem, humanPlayer, computerPlayer);
        
//         Console.WriteLine($"\nYou're playing against the fishmaster {computerPlayer.name}! Good luck!\n");

//         InitialDeal();

//         computerPlayer.SetBehavior(computerBehavior); 
        
//         while(stock.Count != 0 || humanPlayer.hand.Any() || computerPlayer.hand.Any()) 
//         {
//             Console.WriteLine($"\nCards left in the pond: {stock.Count}");

//             if(playCounter % 2 == 0)
//             {
//                 PlayerTurn(humanPlayer);
//                 Console.Clear();
               
//             }

//             else 
//             {
//                 PlayerTurn(computerPlayer);
//             }

//             playCounter++;
            
//         }
//     }
// }