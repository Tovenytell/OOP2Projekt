class Card
    {
        public Suits Suit { get; set; }
        public Values Value { get; set; }

        public Card(Suits suit, Values value)
        {
            this.Suit = suit;
            this.Value = value;
        }

        private static string[] names = new string[] { "", "Ace", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Jack", "Queen", "King" };
        private static string[] suits = new string[] { "spades", "club", "diamond", "heart" };

        public string Name
        {
            get
            {
                return names[(int)Value] + " " + suits[(int)Suit];
            }
        }

        
    }