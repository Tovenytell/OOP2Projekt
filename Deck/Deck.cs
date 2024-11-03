using System.Collections;
using System.Dynamic;
using System.Linq.Expressions;

public class Deck
    {
        private List<Card> cards;
        private Random random = new Random();

        public Deck()
        {
            cards = new List<Card>();
            for (int suit = 0; suit <= 3; suit++)
                for (int value = 1; value <= 13; value++)
                    cards.Add(new Card((Suits)suit, (Values)value));

        }
        public Card Deal(int index)
        {
            if (cards.Any())
            {
                Card CardToDeal = cards[index];
                cards.RemoveAt(index);
                return CardToDeal;
            }
            else
            {
                throw new InvalidOperationException("\nLeken är tom ");
                
            }
        }

        public Card Deal()
        {
            return Deal(0);
        }

        public void Shuffle()
        {
            for (int i = cards.Count - 1; i > 0; i--)
            {
                int j = random.Next(i + 1); 
                Card temp = cards[i];  
                cards[i] = cards[j];
                cards[j] = temp;
            }

        }

        public int Count { get {return cards.Count;}}

        
}