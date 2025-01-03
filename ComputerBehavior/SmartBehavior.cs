public class SmartBehavior : Behavior
{
    public readonly List<PreviousMoves> moves;

    public SmartBehavior(List<PreviousMoves> moves)
    {
        this.moves = moves;
    }
    public override Values AskForCard(List<Values> availableValues)
    {
        
        Dictionary<Values, int> rankFrequencies = new Dictionary<Values, int>();

        foreach (Values rank in availableValues)
        {
            if (rankFrequencies.ContainsKey(rank))
            {
                rankFrequencies[rank]++;
            }
            else
            {
                rankFrequencies[rank] = 1;
            }
        }

        List<Values> mostFrequentRanks = new List<Values>();
        int maxCount = 0;

        foreach (var rank in rankFrequencies)
        {
            if (rank.Value > maxCount)
            {
                mostFrequentRanks.Clear();
                mostFrequentRanks.Add(rank.Key);
                maxCount = rank.Value;
            }
            else if (rank.Value == maxCount)
            {
                mostFrequentRanks.Add(rank.Key);
            }
        }

        Random random = new Random();
        Values selectedRank = mostFrequentRanks[random.Next(mostFrequentRanks.Count)];

        lastAskedValue = selectedRank;

        Console.WriteLine($"\n\nTorsten asks: Do you have any {selectedRank}s?");
        
        moves.Add(new PreviousMoves
        {
            PlayerName = "Torsten",
            Action = $"Asked for {selectedRank}s"
        });

        return selectedRank;
    }
        

}