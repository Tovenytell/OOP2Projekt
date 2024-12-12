public class HumanPlayer : Player
{
    public Values ValidateCard(Values value)
    {
        foreach (Card card in this.hand)
        {
                if (card.Value == value)
                {
                    //askedInHand = true;
                    return value;
                    
                    
                }
        }
      
        return Values.None;          
    }

    public void ProvideFeedback(int humanScore, int computerScore)
    {
        Console.WriteLine(new string('-', 30));
        if (humanScore > computerScore)
        {
            Console.WriteLine("Great job! You're leading the game!");
        }
        else if (humanScore < computerScore)
        {
            Console.WriteLine("Don't worry! You're behind, but you can catch up. Keep going!");
        }
        else
        {
            Console.WriteLine("It's a tie so far! Stay focused!");
        }
        Console.WriteLine(new string('-', 30));
    }
}

            

