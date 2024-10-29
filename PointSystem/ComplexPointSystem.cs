public class ComplexPointSystem : IPointSystem
{
    int sumPoints = 0;
    public int CalculatePoints(List<int> listOfQuartettes)
    {
        foreach (int quartette in listOfQuartettes)
        {
            sumPoints += quartette;
        }

        return sumPoints;
    }
}