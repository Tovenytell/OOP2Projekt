using System.Text.RegularExpressions;

public abstract class Player
{
    public string Name {get; set;}

    public Hand hand {get; private set;}
     

    public Player(string name)
    {
        this.Name = name;
        hand = new Hand();
    }
    public void TakeCard (Card card) 
    {   
        hand.Add(card); 
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