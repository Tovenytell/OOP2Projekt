using System.Text.RegularExpressions;
using System.Text.Json.Serialization;

public abstract class Player
{
    public string Name {get; set;}

    public Behavior behavior;

    [JsonIgnore]
    public Hand hand {get; private set;}
     

    public Player()
    {
        //this.Name = name;
        hand = new Hand();

    }
    public void TakeCard (Card card) 
    {   
        hand.Add(card); 
    }
   
    public void SetBehavior(Behavior playerBehavior)
    {
        behavior = playerBehavior;
    }

    public void ReceiveAskedCards(List<Card> askedCards)
    {
        foreach (Card card in askedCards)
        {
            hand.Add(card);
        } 
    }
    public bool handIsEmpty()
    {
        if (!hand.Any())
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}