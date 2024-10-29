public class SimplePointSystem : IPointSystem
{
    int counter = 0;
     public int CalculatePoints(List<int> listOfQuartettes)
     {
        foreach (int quartette in listOfQuartettes)
        {
            counter++;
        }

        return counter;
     }
}