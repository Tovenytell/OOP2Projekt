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

        public List<Card> Shuffle()
        {
            List<Card> shuffledDeck = new List<Card>();  // Create a new empty list to hold shuffled cards
            while (cards.Count > 0)                  // Continue looping until the original 'cards' list is empty
            {
            int cardToMove = random.Next(cards.Count);  // Pick a random index within the current number of cards
            shuffledDeck.Add(cards[cardToMove]);            // Add the card at that index to the new shuffled list
            cards.RemoveAt(cardToMove);                 // Remove the card from the original list
            }

            // foreach (Card card in shuffledDeck)
            // {
            // Console.WriteLine($"{card.Value} of {card.Suit}");
            // }
            return shuffledDeck;
        }

        //Metod som dealar ut ett kort till personen som måste "fiska"
        //Metoden går att använda i andra sammanhang då den kan ta in ett 
        //godtyckligt index 
        // public Card PickUpCard (int index)
        // {
        //     Card cardToGet = shuffledDeck[index];
        //     shuffledDeck.RemoveAt(index);
        //     return cardToGet;
        // }

        //Metod som kallar på andra Deal-metoden med index 0 för att ge det 
        //första kortet i den blandade högen 
        // public Card PickUpCard ()
        // {
        //     return PickUpCard(0);
        // }

    }