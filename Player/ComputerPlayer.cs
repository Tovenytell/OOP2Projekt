public class ComputerPlayer : Player{
    Behavior behavior;

    // Behavior randomBehavior = new RandomBehavior();
    // Behavior SmartBehavior = new SmartBehavior();

    public Values lastAskedValue;
    public string name;
    //public string Name { get { return name; } }
    // public Deck hand;
    // private int points = 0;

    public ComputerPlayer(string name) : base(name)
    {
        
    }
    public void SetBehavior(Behavior computerBehavior)
    {
        behavior = computerBehavior;
    }

    public void Ask() //nödvändig?
    {
        //behavior.
    }


    

}