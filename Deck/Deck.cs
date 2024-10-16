  class Deck
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

         public void PrintDeck()
        {
        foreach (Card card in cards)
        {
            Console.WriteLine($"{card.Value} of {card.Suit}"); // This calls the overridden ToString() of Card
        }
        }

    }