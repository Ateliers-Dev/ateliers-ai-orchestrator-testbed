using WordCounter;

var failures = new List<string>();

Check("ordinary spaces", 3, TextMetrics.CountWords("one two three"));
Check("repeated spaces", 2, TextMetrics.CountWords("  one   two  "));
Check("tabs and newlines", 3, TextMetrics.CountWords("one\ttwo\nthree"));
Check("empty input", 0, TextMetrics.CountWords(""));
Check("whitespace only", 0, TextMetrics.CountWords(" \t\r\n "));

foreach (var failure in failures)
{
    Console.Error.WriteLine($"FAIL: {failure}");
}

if (failures.Count > 0)
{
    Console.Error.WriteLine($"{failures.Count} check(s) failed.");
    return 1;
}

Console.WriteLine("All word-count checks passed.");
return 0;

void Check(string name, int expected, int actual)
{
    if (expected != actual)
    {
        failures.Add($"{name}: expected {expected}, got {actual}");
    }
}
