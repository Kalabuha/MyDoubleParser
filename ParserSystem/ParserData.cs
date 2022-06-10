using Resources;

namespace ParserSystem
{
    public class ParserData
    {
        public Separators Separators { get; }
        public bool IsInteger { get; set; }
        public int ExponentiationOfTen { get; set; }

        public bool IsWasDecimalSeparator { get; set; }
        public bool IsPreviousCharNumber { get; set; }

        public double LineAsDouble { get; set; }


        public ParserData(Separators separators)
        {
            Separators = separators;
            IsInteger = true;
            ExponentiationOfTen = 0;
            IsWasDecimalSeparator = false;
            IsPreviousCharNumber = true;
            LineAsDouble = 0;
        }
    }
}
