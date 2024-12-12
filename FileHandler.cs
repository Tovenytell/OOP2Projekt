using System;
using System.IO;
using System.Text.Json;

// KRAV #1:
//1: Koncept: Generics
//2: Klassen FileHandler<T> är en generisk typ som kan hantera valfri datatyp T. 
// Vi använder den för att spara och ladda olika typer av data i JSON-format, t.ex. listor av PreviousMoves och PlayerWins.
// Exempel på användning:
// FileHandler<List<PreviousMoves>> moveHandler = new FileHandler<List<PreviousMoves>>();
// FileHandler<List<PlayerWins>> winsHandler = new FileHandler<List<PlayerWins>>();
//3:Vi använder generics här eftersom det gör vår kod flexibel och återanvändbar eftersom vi kan använda samma klass för olika typer av data (moves, wins etc.) utan att duplicera logik.
// Det är meningsfullt att kunna se vad datorn tidigare har frågat om för kort eftersom det ger HumanPlayer en indikation på vad för kort den har på sin hand, och då fördelaktigt kan be om dessa.
// Dessutom gör loggandet av PreviousMoves det möjligt att i HelpBehavior kunna jämföra vad datorn tidigare har frågat om med humanplayers hand och baserat på det ger förslag på vad som bör efterfrågas.
// Det är även bra att kunna se ställningen i hur många gånger varje spealre har vunnit eftersom man vill kunna mötas i till exempel en match i bäst av 3.
//Den generiska implementationen gör att vi enkelt kan utöka funktionaliteten utan att ändra grundstrukturen i FileHandler.
public class FileHandler<T>
{
    public void Save(T data, string filePath)
    {
        string json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(filePath, json);
    }

    public T Load(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"The file at {filePath} was not found.");
        }

        string json = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<T>(json);
    }

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


