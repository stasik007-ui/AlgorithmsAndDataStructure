using System.Text.RegularExpressions;

namespace LabWork2_2
{
    public class RegexIdentifier
    {
        private string pattern = @"^0\d*!\d*1$";
        private Regex regex;

        public RegexIdentifier()
        {
            regex = new Regex(pattern);
        }

        public bool IsMatch(string word)
        {
            return regex.IsMatch(word);
        }

        public string GetPattern()
        {
            return pattern;
        }
    }
}