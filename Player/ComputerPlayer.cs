public class ComputerPlayer : Player{
    
    public void Think()
    {
        Random random = new Random();
        int thinkingTime = random.Next(1000, 3000); 
        Console.WriteLine($"{Name} is thinking...");
        System.Threading.Thread.Sleep(thinkingTime);
    }

}