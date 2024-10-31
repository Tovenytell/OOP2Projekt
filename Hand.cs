using System.Collections;

public class Hand : IEnumerable<Card>
{
    public List<int> listOfQuartettes;
    private List<Card> cards;
    public void RemoveCard (Card card) => cards.Remove(card);


    public Hand()
    {
        cards = new List<Card>();
        listOfQuartettes = new List<int>();
    }
     public List<Card> PullOutValues(Values value)
    {
        List<Card> deckToReturn = new List<Card>();
        List<Card> cardsToRemove = new List<Card>();

        foreach (Card card in cards)
        {
            if (card.Value == value)
            {
                deckToReturn.Add(card);
                cardsToRemove.Add(card);
                //RemoveCard(card);
            }
        }
        foreach (Card card in cardsToRemove)
        {
            RemoveCard(card);
        }
        
        return deckToReturn;

    }

    public void SortHand()
    {
        cards = cards.OrderBy(card => (int)card.Value).ToList();
    }

    public void HasQuartette()
    {
        List<Values> quartetteValues = new List<Values>();
        // Group cards by value
        var groups = cards.GroupBy(card => card.Value);

        foreach (var group in groups)
        {
            // Check if the group contains exactly four cards
            if (group.Count() == 4)
            {
                listOfQuartettes.Add((int)(object)group.Key);

                //quartetteValues.Add(group.Key);

                // Remove all cards of this value from the hand
                cards.RemoveAll(card => card.Value == group.Key);
            }
        }

    //     foreach (var quartetteValue in quartetteValues)
    // {
    //     cards.RemoveAll(card => card.Value == quartetteValue);
    // }
    }

    // public void HasQuartette()
    // {
    //         int numbOfCards = 1;
    //         for (int i = 1; i < hand.Count; i++)
    //         {
    //             if (hand[i].Value == hand[i - 1].Value)
    //             {
    //                 numbOfCards++;
    //                 if (numbOfCards == 4)
    //                 {
    //                     int intOfFoundQuartette = (int)hand[i].Value; 
    //                     listOfQuartettes.Add(intOfFoundQuartette);
    //                     for (int j = i; j > i - 4; j--)
    //                     {
    //                         hand.RemoveAt(j);
    //                     }
    //                 }
    //             }
    //             else
    //             {
    //                 numbOfCards = 1;
    //             }
    //         }
    // }

    public void Add(Card cardToAdd)
    {
        cards.Add(cardToAdd);
        SortHand();
    }
    

    
    public IEnumerator<Card> GetEnumerator()
    {
        return cards.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}