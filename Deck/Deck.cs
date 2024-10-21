using System.Dynamic;
using System.Linq.Expressions;

public class Deck
    {
        private List<Card> cards; //IEnumerable ist för list?
        private Random random = new Random();

        public Deck()
        {
            //? IEnumerable<Card> cards = new List<Card>();
            //cards = new List<Card>();
            for (int suit = 0; suit <= 3; suit++)
                for (int value = 1; value <= 13; value++)
                    cards.Add(new Card((Suits)suit, (Values)value));

        }

        public Deck(IEnumerable<Card> initialCards)
        {
            cards = new List<Card>(initialCards);
        }

        public Card Deal(int index)
        {
            Card CardToDeal = cards[index];
            cards.RemoveAt(index);
            return CardToDeal;
        }

        public Card Deal()
        {
            return Deal(0);
        }

        //Metod som kallar på andra Deal-metoden med index 0 för att ge det 
        //första kortet i den blandade högen ????
        // public Card PickUpCard ()
        // {
        //     return PickUpCard(0);
        // }

        public void Add(Card cardToAdd)
        {
            cards.Add(cardToAdd);
        }

         public void PrintDeck()
        {
        foreach (Card card in cards)
        {
            Console.WriteLine($"{card.Value} of {card.Suit}"); // This calls the overridden ToString() of Card
        }
        }

        public List<Card> Shuffle()
        {
            List<Card> shuffledDeck = new List<Card>();  // Create a new empty list to hold shuffled cards
            while (cards.Count > 0)                  // Continue looping until the original 'cards' list is empty
            {
            int cardToMove = random.Next(cards.Count);  // Pick a random index within the current number of cards
            shuffledDeck.Add(cards[cardToMove]);            // Add the card at that index to the new shuffled list
            cards.RemoveAt(cardToMove);                 // Remove the card from the original list
            }
            //cards = NewCards;??
            return shuffledDeck;

        }

        public int Count { get {return cards.Count;}}

       public bool ContainsValue(Values value)
       {
            foreach (Card card in cards)
            {
                if (card.Value == value)
                {
                    return true;
                }
            }
            
            return false;
       }

       public Deck PullOutValues(Values value)
       {
            Deck deckToReturn = new Deck(new Card[] {});//ska vi ha array här?
            for (int i = cards.Count - 1; i >= 0; i--) //vad bör skrivas? Hand/cards?
            {
                if (cards[i].Value == value)
                {
                    deckToReturn.Add(Deal(i));
                } 
            }
            return deckToReturn;
       } 

       public bool HasQuartette(Values value)
       {
            int numbOfCards = 0;
            foreach (Card card in cards) //Hand/cards??
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

        public void SortByValue()
        {
            cards.Sort(new CompareCardByValue());
        }

       

    }