using Resources;

namespace ParserSystem
{
    public class ParserValidation
    {
        private readonly int _lineLengthLimit;
        public Separators Separators { get; }

        public ParserValidation(int lineLengthLimit, Separators separators)
        {
            if (lineLengthLimit < 0)
            {
                throw new ApplicationException("Задано недопустимое значение предельной длины строки. Оно не может быть отрицательным.");
            }
            if (lineLengthLimit == 0)
            {
                throw new ApplicationException("Задано недопустимое значение предельной длины строки. Оно не может быть нулевым.");
            }

            Separators = separators;
            _lineLengthLimit = lineLengthLimit;
        }

        public InputValidationResult InitialChecks(string? input)
        {
            ResultStatus? initialStatus = GetIninitStatusIsNullEmptyOrWhiteSpace(input);
            if (initialStatus.HasValue)
            {
                return new InputValidationResult { Result = new Result { Status = initialStatus.Value } };
            }

            // line здесь проверен и не может быть null
            string validatedLine = input!.Trim();
            initialStatus = LineLengthCheck(input, _lineLengthLimit);
            if (initialStatus.HasValue)
            {
                return new InputValidationResult { Result = new Result { Status = initialStatus.Value } };

            }

            return new InputValidationResult { Line = validatedLine };
        }

        private ResultStatus? GetIninitStatusIsNullEmptyOrWhiteSpace(string? input)
        {
            if (string.IsNullOrEmpty(input))
            {
                // null и empty будем считать одним и тем же состоянием входной строки.
                return ResultStatus.LineIsNullOrEmpty;
            }
            else if (string.IsNullOrWhiteSpace(input))
            {
                return ResultStatus.LineIsWhiteSpace;
            }

            return null;
        }

        private ResultStatus? LineLengthCheck(string line, int maxLength)
        {
            if (line.Length > maxLength)
            {
                return ResultStatus.LineIsTooLong;
            }

            return null;
        }
    }
}
