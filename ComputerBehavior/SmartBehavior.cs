public class SmartBehavior : Behavior
{

    public SmartBehavior(IPointSystem pSystem) : base (pSystem)
    {
        
    }
//     public Values AskSmart(List<Values> availableValues)
// // {
// //     // Dictionary to store the frequency of each rank in the computer's hand
// //     Dictionary<Values, int> rankFrequencies = new Dictionary<Values, int>();

// //     // Count occurrences of each rank
// //     foreach (Values rank in availableValues)
// //     {
// //         if (rankFrequencies.ContainsKey(rank))
// //         {
// //             rankFrequencies[rank]++;
// //         }
// //         else
// //         {
// //             rankFrequencies[rank] = 1;
// //         }
// //     }

// //     // Find the most frequent rank(s)
// //     List<Values> mostFrequentRanks = new List<Values>();
// //     int maxCount = 0;

// //     foreach (var rank in rankFrequencies)
// //     {
// //         if (rank.Value > maxCount)
// //         {
// //             mostFrequentRanks.Clear(); // Clear previous ranks
// //             mostFrequentRanks.Add(rank.Key); // Add new most frequent rank
// //             maxCount = rank.Value; // Update max count
// //         }
// //         else if (rank.Value == maxCount)
// //         {
// //             mostFrequentRanks.Add(rank.Key); // Add to list of ranks with max count
// //         }
// //     }

// //     // Randomly select one of the most frequent ranks
// //     Random random = new Random();
// //     Values selectedRank = mostFrequentRanks[random.Next(mostFrequentRanks.Count)];

// //     // "Ask" for the selected card rank
// //     Console.WriteLine($"\nComputer asks: Do you have any {selectedRank}s?");
// //     return selectedRank;
// //     // Here, you can implement logic to check if the human player has this card rank
// //     // and handle the response accordingly.
// // }

    public override Values AskForCard(List<Values> availableValues)
    {
        
        // Dictionary to store the frequency of each rank in the computer's hand
        Dictionary<Values, int> rankFrequencies = new Dictionary<Values, int>();

        // Count occurrences of each rank
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

        // Find the most frequent rank(s)
        List<Values> mostFrequentRanks = new List<Values>();
        int maxCount = 0;

        foreach (var rank in rankFrequencies)
        {
            if (rank.Value > maxCount)
            {
                mostFrequentRanks.Clear(); // Clear previous ranks
                mostFrequentRanks.Add(rank.Key); // Add new most frequent rank
                maxCount = rank.Value; // Update max count
            }
            else if (rank.Value == maxCount)
            {
                mostFrequentRanks.Add(rank.Key); // Add to list of ranks with max count
            }
        }

        // Randomly select one of the most frequent ranks
        Random random = new Random();
        Values selectedRank = mostFrequentRanks[random.Next(mostFrequentRanks.Count)];

        lastAskedValue = selectedRank;

        // "Ask" for the selected card rank
        Console.WriteLine($"\nComputer asks: Do you have any {selectedRank}s?");
        return selectedRank;
        // Here, you can implement logic to check if the human player has this card rank
        // and handle the response accordingly.
    }
        

}