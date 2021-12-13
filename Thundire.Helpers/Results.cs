// ReSharper disable ArrangeObjectCreationWhenTypeEvident
namespace Thundire.Helpers
{
    public abstract record Result
    {
#pragma warning disable IDE0090 // Use 'new(...)'
        public static SuccessResult Ok() => new SuccessResult();
        public static FailureResult Fail(params FailureDescription[] failures) => new FailureResult(failures);
        public static SuccessResult<TResult> Ok<TResult>(TResult callBackData) => new SuccessResult<TResult>(callBackData);
        public static ExitResult Exit { get; } = new ExitResult();
#pragma warning restore IDE0090 // Use 'new(...)'
    };

    public record SuccessResult : Result;
    public record SuccessResult<TResult>(TResult CallBackData) : SuccessResult;
    public record FailureResult(params FailureDescription[] Failures) : Result;

    public record ExitResult : Result;

    public record FailureDescription(string Message, string Code);
}