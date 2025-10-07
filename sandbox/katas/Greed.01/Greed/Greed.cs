namespace GreedSpace;

public class Greed
{
    private static readonly Random Rnd = new();

    private static List<int> GetDiceValues()
    {
        var diceValues = new List<int>();
        for (int i = 0; i < 5; i++)
        {
            diceValues.Add(Rnd.Next(1, 7));
        }
        return diceValues;
    }

    private static int CountScore()
    {
        var diceValues = GetDiceValues();
        Console.WriteLine($"Dice: [{string.Join(", ", diceValues)}]");
        var counts = new Dictionary<int, int>();
        foreach (int v in diceValues)
        {
            if (counts.TryGetValue(v, out int c))
            { counts[v] = c + 1; }
            else
            { counts[v] = 1; }
        }
        int score = 0;
        foreach (var kv in counts)
        {
            Console.WriteLine($"{kv.Key}: {kv.Value}");
            int actualValue = kv.Value;
            if (kv.Value >= 3)
            {
                if (kv.Key == 1)
                {
                    score += 1000;
                }
                else
                {
                    score += 100 * kv.Key;
                }
                actualValue = kv.Value - 3;
            }
            if (actualValue > 0)
            {
                if (kv.Key == 1)
                {
                    score += actualValue * 100;
                }
                else if (kv.Key == 5)
                {
                    score += actualValue * 50;
                }
            }
        }
        return score;
    }

    public void Roll()
    {
        int score = CountScore();
        Console.WriteLine(score);
    }
}
