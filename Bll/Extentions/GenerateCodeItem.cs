namespace Bll.Extentions
{
    public class GenerateCodeItem
    {
        private static Random _random = new();
        public static string CodeInput(string message)
        {
            string prefix = "";
            if (string.IsNullOrEmpty(message)) { throw new ArgumentException("Message cannot be null or empty.", nameof(message)); }
            if (message.Length < 2) { prefix = message.Substring(0, 1).ToUpper(); }
            else
            {
                string firstTwo = message.Substring(0, 2).ToUpper();
                string lastTwo = message.Substring(message.Length - 2, 2).ToUpper();
                prefix = firstTwo + lastTwo;
            }
            int randomNumber = _random.Next(1, 1000000);
            string randomString = randomNumber.ToString("D6");
            return $"{prefix}{randomString}";
        }
    }
}
