public interface IPointSystem
// KRAV 3
// Koncept: Bridge Pattern
// Vi använder konceptet bridge pattern genom att behavior har ett pointsystem

// Behavior har ett pointsystem som används för att kontinueligt används för att jämföra 
// spelarnas poängställning under spelets gång 

// Detta gör vi för att kunna göra olika kombinationer av behavior och pointsystem i spelet. 
// Det är även viktigt för kodens underhållbarhet ifall vi vill lägga till fler i framtiden 
{
    public int CalculatePoints(List<int> listOfQuartettes);
}