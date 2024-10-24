using System.Text.RegularExpressions;

public abstract class Player{

    public string Name {get; set;}
    public List<Card> hand; 

    public List<int> listOfQuartettes;
     

    public Player(string name)
    {
        this.Name = name;
        hand = new List<Card>();
        listOfQuartettes = new List<int>();
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
       public bool HasQuartette()
       {
            int numbOfCards = 1;
            for (int i = 1; i <= hand.Count; i++)
            {
                if (hand[i].Value == hand[i-1].Value)
                {
                    numbOfCards++;
                    if (numbOfCards == 4)
                    {
                        int intOfFoundQuartette = (int)hand[i].Value; 
                        listOfQuartettes.Add(intOfFoundQuartette);
                        for (int j = i; j == j-3; j--)
                        {
                            hand.RemoveAt(i);
                        }
                    }
                }
            }

            // foreach (Card card in hand) //Hand/cards??
            // {
            //       if (card.Value == value)
            //       {
            //          numbOfCards++;
            //       }
            // }

            
                    
            if (numbOfCards == 4)
            {

                return true;
            }
                
            else
            {
                return false;
            }
       }

    public void SortHand() 
    {
        hand.Sort(new CompareCardByValue());
    }

        

       public bool ContainsValue(Values value)
       {
            foreach (Card card in hand)
            {
                if (card.Value == value)
                {
                    return true;
                }
            }
            
            return false;
       }

       
       
    
    public void SortByValue()
    {
        hand.Sort(new CompareCardByValue());
    }

    internal object GroupBy(Func<object, object> value)
    {
        throw new NotImplementedException();
    }

    // public IEnumerator<Card> GetEnumerator()
    // {
    //     throw new NotImplementedException();
    // }
    
}