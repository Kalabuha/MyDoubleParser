using MyParsers;
using Resources;
using Models;

namespace Services
{
    public class ParserService
    {
        private readonly ToDouble _toDoubleParser;

        public ParserService()
        {
            _toDoubleParser = new ToDouble();
        }

        public ResultModel ParseToDouble(string? input)
        {
            var result = _toDoubleParser.GetResult(input);

            if (result == null)
            {
                return new ResultModel { Message = "Результат не получен. Что несколько неожиданно." };
            }

            var resultModel = new ResultModel();
            switch (result.Status)
            {
                case ResultStatus.LineIsNullOrEmpty:
                    resultModel.Message = "Входная строка пуста.";
                    break;

                case ResultStatus.LineIsWhiteSpace:
                    resultModel.Message = "Входная строка пуста.";
                    break;

                case ResultStatus.LineContainsInvalidChars:
                    resultModel.Message = $"Входная строка содержит недопустимые символы.";
                    break;

                case ResultStatus.LineContainsMoreThanOneDecimalSeparator:
                    resultModel.Message = $"Содержание более одного десятичного разделителя во входной строке недопустимо.";
                    break;

                case ResultStatus.TheDecimalSeparatorMustBePrecededByNumber:
                    resultModel.Message = "Недопускается размещать групповой разделитель после десятичного разделителя.";
                    break;

                case ResultStatus.UnaryOperatorIsInInvalidPosition:
                    resultModel.Message = "Унарный оператор должен быть в самом начале входной строки.";
                    break;

                case ResultStatus.TheUnaryOperatorMustBePrecededByNumber:
                    resultModel.Message = "Недопускается размещать какой-либо разделитель после унарного оператора.";
                    break;

                case ResultStatus.GroupSeparatorCannotBeAfterUnaryOperator:
                    resultModel.Message = "Недопускается какие-либо символы размещать перед унарным оператором.";
                    break;

                case ResultStatus.LineIsTooLong:
                    resultModel.Message = "Строка слишком длинная.";
                    break;

                case ResultStatus.LineWasParsed:
                    resultModel.Value = result.Value;
                    resultModel.Message = "Успешное преобразование строки в число.";
                    break;

                default:
                    throw new ApplicationException("Резултат работы парсера не известен.");
            }

            return resultModel;
        }
    }
}