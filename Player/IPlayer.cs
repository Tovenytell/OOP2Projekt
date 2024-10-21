public abstract class Player{
     //Metod som dealar ut ett kort till personen som måste "fiska"
        //Metoden går att använda i andra sammanhang då den kan ta in ett 
        //godtyckligt index 
        public Card TakeCard (Card card) //int index
        {   
            cards.Add(card);
            // Card cardToGet = shuffledDeck[index];
            // shuffledDeck.RemoveAt(index);
            // return cardToGet;
        }
}