using System;
using System.IO;
using System.Text.Json;

public class FileHandler<T>
{
    // Save data to a JSON file
    public void Save(T data, string filePath)
    {
        string json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(filePath, json);
    }

    // Load data from a JSON file
    public T Load(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"The file at {filePath} was not found.");
        }

        string json = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<T>(json);
    }

    // Display a log of moves (if T is a list of Move objects)
    public void DisplayLog(IEnumerable<PreviousMoves> moves)
    {
        Console.WriteLine("Moves Log:");
        foreach (var move in moves)
        {
            Console.WriteLine(move);
        }
    }
}

public class PreviousMoves
{
    public string PlayerName { get; set; }
    public string Action { get; set; }

    public override string ToString()
    {
        return $"{PlayerName} - Action: {Action}";
    }
}

public class PlayerWins
{
    public Player Player { get; set; }
    public int Wins { get; set; }

    public override string ToString()
    {
        return $"{Player.Name}: {Wins} wins";
    }
}



// public class PreviousScores
// {
//     public string PlayerName { get; set; }
//     public int Score { get; set; }

//     public override string ToString()
//     {
//         return $"{PlayerName}: {Score}";
//     }
// }

