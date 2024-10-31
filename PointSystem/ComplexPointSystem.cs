public class ComplexPointSystem : IPointSystem
{
    
    public int CalculatePoints(List<int> listOfQuartettes)
    { 
        int sumPoints = 0;
        
        foreach (int quartette in listOfQuartettes)
        {
            sumPoints += quartette;
        }

        return sumPoints;
    }
}