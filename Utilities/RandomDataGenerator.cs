namespace MarsAutomation.Utilities
{
    public static class RandomDataGenerator
    {
        private static readonly Random random = new Random();

        public static string RandomString(int length = 6)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static int RandomNumber(int min = 1, int max = 999)
        {
            return random.Next(min, max);
        }
    }
}
