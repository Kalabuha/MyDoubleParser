using System.Globalization;

namespace Resources
{
    public class Separators
    {
        public char Group { get; }
        public char Decimal { get; }

        public Separators()
        {
            (Group, Decimal) = GetCultureSeparators();
        }

        private (char groupSeparator, char decimalSeparator) GetCultureSeparators()
        {
            NumberFormatInfo formatter = CultureInfo.CurrentCulture.NumberFormat;
            char decimalSeparator = Convert.ToChar(formatter.CurrencyDecimalSeparator);
            char groupSeparator = Convert.ToChar(formatter.NumberGroupSeparator);

            return (groupSeparator, decimalSeparator);
        }
    }
}
