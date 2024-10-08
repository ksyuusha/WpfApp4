using System;

namespace WpfApp
{
    public class SomeClass
    {
        public bool CheckKeywords(string input, string keyword)
        {
            // Используем IndexOf для игнорирования регистра
            return input.IndexOf(keyword, StringComparison.InvariantCultureIgnoreCase) >= 0;
        }
    }
}
