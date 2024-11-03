public class ComputerPlayer : Player{
    Behavior behavior;
    public string name = "Torsten";

    public ComputerPlayer(string name) : base(name) {}
    public void SetBehavior(Behavior computerBehavior)
    {
        behavior = computerBehavior;
    }


    

}