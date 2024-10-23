using System.Text.RegularExpressions;

public abstract class Player{

    public string Name {get; set;}
    public List<Card> hand; 
     

    public Player(string name)
    {
        this.Name = name;
        hand = new List<Card>();
    }

    //Metod som dealar ut ett kort till personen som måste "fiska"
        //Metoden går att använda i andra sammanhang då den kan ta in ett 
        //godtyckligt index 
    public void TakeCard (Card card) //int index
    {   
        hand.Add(card); 
            // Card cardToGet = shuffledDeck[index];
            // shuffledDeck.RemoveAt(index);
            // return cardToGet;
    }

    public List<Card> PullOutValues(/*List<Card> hand, */Values value)
       {
            List<Card> deckToReturn = new List<Card>(new Card[] {});//ska vi ha array här?
            for (int i = hand.Count - 1; i >= 0; i--) //vad bör skrivas? Hand/cards?
            {
                if (hand[i].Value == value)
                {
                    //deckToReturn.Add(Deal(i));
                    deckToReturn.Add(hand[i]);
                    hand.RemoveAt(i);
                } 
            }
            return deckToReturn;
       } 

       public void ReceiveAskedCards(List<Card> askedCards)
    {
        foreach (Card card in askedCards)
        {
            hand.Add(card);
        } 
    }
       //    Kollar om en hand har 4:tal
       public bool HasQuartette(Values value)
       {
            int numbOfCards = 0;
            foreach (Card card in hand) //Hand/cards??
            {
                  if (card.Value == value)
                  {
                     numbOfCards++;
                  }
            }
                    
            if (numbOfCards == 4)
            {
                return true;
            }
                
            else
            {
                return false;
            }
       }

    //public void SortHand() {hand.SortByValue();}

    
}