namespace Resources
{
    public class Symbol
    {
        private const string ErrorMessage = "Символ не может одновременно иметь более одного значения.";

        private char? unarySymbol;
        public char? UnarySymbol
        {
            get
            {
                if (decimalSeparator != null && groupSeparator != null && number != null)
                {
                    throw new ApplicationException(ErrorMessage);
                }
                return unarySymbol;
            }

            set
            {
                unarySymbol = value;
                number = null;
                decimalSeparator = null;
                groupSeparator = null;
            }
        }


        private byte? number;
        public byte? Number
        {
            get
            {
                if (decimalSeparator != null && groupSeparator != null && unarySymbol != null)
                {
                    throw new ApplicationException(ErrorMessage);
                }
                return number;
            }

            set
            {
                number = value;
                unarySymbol = null;
                decimalSeparator = null;
                groupSeparator = null;
            }
        }

        private char? decimalSeparator;
        public char? DecimalSeparator
        {
            get
            {
                if (number != null && groupSeparator != null && unarySymbol != null)
                {
                    throw new ApplicationException(ErrorMessage);
                }
                return decimalSeparator;
            }

            set
            {
                decimalSeparator = value;
                unarySymbol = null;
                groupSeparator = null;
                number = null;
            }
        }

        private char? groupSeparator;
        public char? GroupSeparator
        {
            get
            {
                if (number != null && decimalSeparator != null && unarySymbol != null)
                {
                    throw new ApplicationException(ErrorMessage);
                }
                return groupSeparator;
            }

            set
            {
                groupSeparator = value;
                unarySymbol = null;
                decimalSeparator = null;
                number = null;
            }
        }
    }
}

