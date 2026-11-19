namespace WordCounter;

public static class TextMetrics
{
    public static int CountWords(string text)
    {
        ArgumentNullException.ThrowIfNull(text);

        return text.Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries).Length;
    }
}
