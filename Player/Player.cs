public abstract class Player{

    private Deck hand;
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
}