using System.Collections;

// KRAV 4
// Koncept: Enumerable och enumerators 
// Vi använder konceptet genom att ha en hand (som varje player har) som 
// går att iterera över. 

// Vi använder detta istället för att ha hand som en lista, detta medför en 
// säkrare iteration över korten då vi inte exponerar hela kollektionen utan 
// endast ett åt gången. 
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

        var groups = cards.GroupBy(card => card.Value);

        foreach (var group in groups)
        {
            if (group.Count() == 4)
            {
                listOfQuartettes.Add((int)(object)group.Key);

                cards.RemoveAll(card => card.Value == group.Key);
            }
        }
    }

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