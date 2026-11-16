namespace WordCounter;

public static class TextMetrics
{
    // Intentional training defect: only ordinary spaces separate words.
    public static int CountWords(string text)
    {
        ArgumentNullException.ThrowIfNull(text);

        return text.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;
    }
}
