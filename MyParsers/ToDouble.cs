using ParserSystem;
using Resources;

namespace MyParsers
{
    public class ToDouble
    {
        const int LineLengthLimit = 16;

        private readonly ParserData _data;
        private readonly ParserValidation _validation;

        public ToDouble()
        {
            var separators = new Separators();

            _data = new ParserData(separators);
            _validation = new ParserValidation(LineLengthLimit, separators);
        }

        public Result GetResult(string? input)
        {
            InputValidationResult inputValidationResult = _validation.InitialChecks(input);
            // Если на этом этапе нет результата, значит можно попробовать распарсить
            if (inputValidationResult.Result != null)
            {
                return inputValidationResult.Result;
            }

            // Пробуем распарсить. Строка здесь передаётся без боковых пробелов.
            Result result = LineProcessing(inputValidationResult.Line);
            return result;
        }

        private Result LineProcessing(string line)
        {
            for (int i = line.Length - 1; i >= 0; i--)
            {
                ResultStatus? receivedError = null;

                var symbol = TryGetDefineSymbol(line[i]);
                if (symbol == null)
                {
                    return new Result { Status = ResultStatus.LineContainsInvalidChars };
                }

                else if (symbol.UnarySymbol != null)
                {
                    receivedError = UnarySymbolProcessing(i, symbol.UnarySymbol.Value);
                }
                else if (symbol.Number != null)
                {
                    NumberProcessing(symbol.Number.Value);
                }
                else if (symbol.DecimalSeparator != null)
                {
                    receivedError = DecimalSeparatorProcessing();
                }
                else if (symbol.GroupSeparator != null)
                {
                    receivedError = GroupSeparatorProcessing();
                }

                if (receivedError != null)
                {
                    return new Result { Status = receivedError.Value };
                }
            }

            return new Result { Status = ResultStatus.LineWasParsed, Value = _data.LineAsDouble };
        }

        private ResultStatus? UnarySymbolProcessing(int index, char unarySymbol)
        {
            if (index != 0)
            {
                return ResultStatus.UnaryOperatorIsInInvalidPosition;
            }

            if (!_data.IsPreviousCharNumber)
            {
                return ResultStatus.TheUnaryOperatorMustBePrecededByNumber;
            }

            if (unarySymbol == '-')
            {
                _data.LineAsDouble *= -1;
            }

            return null;
        }

        private void NumberProcessing(byte number)
        {
            _data.LineAsDouble += number * Math.Pow(10, _data.ExponentiationOfTen++);
            _data.IsPreviousCharNumber = true;
        }

        private ResultStatus? DecimalSeparatorProcessing()
        {
            if (_data.IsWasDecimalSeparator)
            {
                return ResultStatus.LineContainsMoreThanOneDecimalSeparator;
            }

            if (!_data.IsPreviousCharNumber)
            {
                return ResultStatus.TheDecimalSeparatorMustBePrecededByNumber;
            }

            _data.IsWasDecimalSeparator = true;
            _data.LineAsDouble /= Math.Pow(10, _data.ExponentiationOfTen);
            _data.ExponentiationOfTen = 0;
            _data.IsPreviousCharNumber = false;
            return null;
        }
        private ResultStatus? GroupSeparatorProcessing()
        {
            _data.IsPreviousCharNumber = false;
            return null;
        }

        private Symbol? TryGetDefineSymbol(char symbol)
        {
            switch (symbol)
            {
                case '0': return new Symbol { Number = 0 };
                case '1': return new Symbol { Number = 1 };
                case '2': return new Symbol { Number = 2 };
                case '3': return new Symbol { Number = 3 };
                case '4': return new Symbol { Number = 4 };
                case '5': return new Symbol { Number = 5 };
                case '6': return new Symbol { Number = 6 };
                case '7': return new Symbol { Number = 7 };
                case '8': return new Symbol { Number = 8 };
                case '9': return new Symbol { Number = 9 };

                default:
                    if (symbol == _data.Separators.Decimal)
                        return new Symbol { DecimalSeparator = symbol };

                    else if (symbol == _data.Separators.Group)
                        return new Symbol { GroupSeparator = symbol };

                    else if (symbol == '+' || symbol == '-')
                        return new Symbol { UnarySymbol = symbol };

                    else
                        return null;
            }
        }
    }
}
