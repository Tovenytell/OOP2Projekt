using System.Text.RegularExpressions;
using System.Text.Json.Serialization;

// KRAV 3
// Koncept: Bridge Pattern
// 2: Player är en abstraktion med två konkretioner: HumanPlayer och ComputerPlayer. Dessa representerar olika typer av spelare.
// Behavior är en separat abstraktion som beskriver ett beteende. Den har flera konkretioner: RandomBehavior, SmartBehavior, och HelpBehavior.
// Kopplingen mellan Player och Behavior: Varje Player har ett Behavior, vilket gör att spelaren kan använda olika beteenden utan att ändra koden i Player.
// 3: Vi använder oss av detta bridgepattern eftersom det gör att vi kan lägga till fler beteenden utan att påverka Player-klassen (tex: AggressiveBehavior)
// men även lägga till fler spelartyper utan att påverka Behavior-klassen (tex: AIPlayer).
// Flexibilitet:
// Genom att separera Player från Behavior kan vi återanvända och kombinera olika subklasser för att skapa nya beteenden och spelartyper.
// Exempel: Vi kan skapa en HumanPlayer med ett HelpBehavior eller en ComputerPlayer med ett SmartBehavior.
// Utbyggbarhet:
// Om vi vill lägga till fler spelartyper eller beteenden kan vi göra det utan att behöva ändra koden i andra klasser.
// Exempel: Lägg till ett DefensiveBehavior som fokuserar på att förhindra motståndaren från att samla kvartetter.
// Tydlighet:
// Designen gör att varje klass har ett specifikt ansvar (Single Responsibility Principle)
public abstract class Player
{
    public string Name {get; set;}

    public Behavior behavior;

    [JsonIgnore]
    public Hand hand {get; private set;}
     

    public Player()
    {
        hand = new Hand();

    }
    public void TakeCard (Card card) 
    {   
        hand.Add(card); 
    }
   
    public void SetBehavior(Behavior playerBehavior)
    {
        behavior = playerBehavior;
    }

    public void ReceiveAskedCards(List<Card> askedCards)
    {
        foreach (Card card in askedCards)
        {
            hand.Add(card);
        } 
    }
    public bool handIsEmpty()
    {
        if (!hand.Any())
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}