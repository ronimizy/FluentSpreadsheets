using System.Text;

namespace FluentSpreadsheets;

public readonly record struct Index(int Row, int Column)
{
    public string ColumnString => GetColumnIndex(Column);

    public static Index FromSpan(ReadOnlySpan<char> value)
    {
        int columnCharactersCount = CountColumnCharactersCount(value);
        int rowCharactersCount = value.Length - columnCharactersCount;

        ReadOnlySpan<char> column = value[..columnCharactersCount];
        ReadOnlySpan<char> row = value.Slice(columnCharactersCount, rowCharactersCount);

        return new Index(int.Parse(row), ParseColumn(column));
    }

    public override string ToString()
        => $"{GetColumnIndex(Column)}{Row}";

    private static string GetColumnIndex(int value)
    {
        // Column index is represented as a sequence of 26 number system
        // represented by English alphabet

        if (value is 0)
            return string.Empty;

        var builder = new StringBuilder();

        while (value > 0)
        {
            int mod = (value - 1) % 26;
            builder.Insert(0, (char)('A' + mod));

            value = (value - mod) / 26;
        }

        return builder.ToString();
    }

    private static int ParseColumn(ReadOnlySpan<char> str)
    {
        // Column index is represented as a sequence of 26 number system
        // represented by English alphabet

        var result = 0;
        int factor = str.Length - 1;

        foreach (char c in str)
        {
            var symbol = char.ToUpper(c);
            result += (int)Math.Pow(26, factor) * (symbol - 'A' + 1);

            factor--;
        }

        return result;
    }

    private static int CountColumnCharactersCount(ReadOnlySpan<char> value)
    {
        var count = 0;

        foreach (char c in value)
        {
            if (char.ToUpper(c) is >= 'A' and <= 'Z')
            {
                count++;
            }
            else
            {
                break;
            }
        }

        return count;
    }
}
