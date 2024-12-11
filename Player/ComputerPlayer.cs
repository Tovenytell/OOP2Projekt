public class ComputerPlayer : Player{
    //Behavior behavior;
    //public string Name = "Torsten";

    // public ComputerPlayer(string name) : base(name) {}
    // public void SetBehavior(Behavior computerBehavior)
    // {
    //     behavior = computerBehavior;
    // }

    // Method to simulate thinking time
    public void Think()
    {
        Random random = new Random();
        int thinkingTime = random.Next(1000, 3000); // Random delay between 1 and 3 seconds
        Console.WriteLine($"{Name} is thinking...");
        System.Threading.Thread.Sleep(thinkingTime); // Pause the program for the chosen duration
    }

    

}