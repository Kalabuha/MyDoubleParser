namespace Resources
{
    public enum ResultStatus
    {
        LineIsNullOrEmpty = 1,
        LineIsWhiteSpace,
        LineContainsInvalidChars,
        LineContainsMoreThanOneDecimalSeparator,
        TheDecimalSeparatorMustBePrecededByNumber,
        UnaryOperatorIsInInvalidPosition,
        TheUnaryOperatorMustBePrecededByNumber,
        GroupSeparatorCannotBeAfterUnaryOperator,
        LineIsTooLong,
        LineWasParsed
    }
}
