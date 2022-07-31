namespace FluentSpreadsheets.Tools;

internal static class LcmCounter
{
    public static int Count(IEnumerable<int> values)
    {
        return values.Aggregate(1, (ans, value) =>
        {
            var gcd = Gcd(value, ans);
            return gcd is 0 ? 0 : value * ans / gcd;
        });
    }

    private static int Gcd(int a, int b)
    {
        while (true)
        {
            if (b is 0)
                return a;

            (a, b) = (b, a % b);
        }
    }
}