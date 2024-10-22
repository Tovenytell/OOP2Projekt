using System.Text.RegularExpressions;

public abstract class Player{

    public string Name {get; set;}
    public List<Card> hand; 
     //Metod som dealar ut ett kort till personen som måste "fiska"
        //Metoden går att använda i andra sammanhang då den kan ta in ett 
        //godtyckligt index 

    public Player(string name)
    {
        this.Name = name;
        hand = new List<Card>();
    }
    public void TakeCard (Card card) //int index
    {   
        hand.Add(card); 
            // Card cardToGet = shuffledDeck[index];
            // shuffledDeck.RemoveAt(index);
            // return cardToGet;
    }

    public void ReceiveAskedCards(List<Card> askedCards)
    {
        foreach (Card card in askedCards)
        {
            hand.Add(card);
        } 
    }

    //public void SortHand() {hand.SortByValue();}

    
}