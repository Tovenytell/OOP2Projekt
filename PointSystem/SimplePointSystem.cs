public class SimplePointSystem : IPointSystem
{
    
    public int CalculatePoints(List<int> listOfQuartettes)
    {
        int counter = 0;
        
        foreach (int quartette in listOfQuartettes)
        {
            counter++;
        }

        return counter;
    }
}