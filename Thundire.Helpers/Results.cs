// ReSharper disable ArrangeObjectCreationWhenTypeEvident
using System.Collections.Generic;
using System;

namespace Thundire.Helpers
{
    public abstract record Result
    {
#pragma warning disable IDE0090 // Use 'new(...)'
        public static SuccessResult Ok() => new SuccessResult();
        public static FailureResult Fail(params FailureDescription[] failures) => new FailureResult(failures);
        public static ExitResult Exit { get; } = new ExitResult();
        public bool IsSuccess => this is SuccessResult;
#pragma warning restore IDE0090 // Use 'new(...)'

        public static void Handle(Result result, Action<ResultHandlerConfiguration> handlerConfiguration)
        {
            ResultHandlerConfiguration handler = new();
            handlerConfiguration.Invoke(handler);

            switch (result)
            {
                case SuccessResult successResult:
                    handler.OnSuccess?.Invoke(successResult);
                    break;
                case FailureResult failureResult:
                    handler.OnFailure?.Invoke(failureResult.Failures);
                    break;
                case ExitResult exitResult:
                    handler.OnExit?.Invoke(exitResult);
                    break;
            }
        }
    }

    public record SuccessResult : Result;
    public record FailureResult(params FailureDescription[] Failures) : Result;
    public record ExitResult : Result;
    public record FailureDescription(string Message, string Code);

    public abstract record Result<TResult>
    {
#pragma warning disable IDE0090 // Use 'new(...)'
        public static SuccessResult<TResult> Ok(TResult callBackData) => new SuccessResult<TResult>(callBackData);
        public static FailureResult<TResult> Fail(params FailureDescription[] failures) => new FailureResult<TResult>(failures);
        public static ExitResult<TResult> Exit { get; } = new ExitResult<TResult>();
        public bool IsSuccess => this is SuccessResult<TResult>;
#pragma warning restore IDE0090 // Use 'new(...)'

        public static void Handle(Result<TResult> result, Action<ResultHandlerConfiguration<TResult>> handlerConfiguration)
        {
            ResultHandlerConfiguration<TResult> handler = new();
            handlerConfiguration.Invoke(handler);

            switch (result)
            {
                case SuccessResult<TResult> successResult:
                    handler.OnSuccess?.Invoke(successResult);
                    break;
                case FailureResult<TResult> failureResult:
                    handler.OnFailure?.Invoke(failureResult.Failures);
                    break;
                case ExitResult<TResult> exitResult:
                    handler.OnExit?.Invoke(exitResult);
                    break;
            }
        }

        public TResult ReturnResult(TResult defaultValue) => this is SuccessResult<TResult> successResult ? successResult.CallBackData : defaultValue;
        public TResult GetResult()
        {
            if (this is not SuccessResult<TResult> successResult) throw new InvalidOperationException("Result is not success");
            return successResult.CallBackData;
        }
    }

    public record SuccessResult<TResult>(TResult CallBackData) : Result<TResult>;
    public record FailureResult<TResult>(params FailureDescription[] Failures) : Result<TResult>;
    public record ExitResult<TResult> : Result<TResult>;

    public record ResultHandlerConfiguration<T>
    {
        public Action<SuccessResult<T>>? OnSuccess { get; set; }
        public Action<IEnumerable<FailureDescription>>? OnFailure { get; set; }
        public Action<ExitResult<T>>? OnExit { get; set; }
    }

    public record ResultHandlerConfiguration
    {
        public Action<SuccessResult>? OnSuccess { get; set; }
        public Action<IEnumerable<FailureDescription>>? OnFailure { get; set; }
        public Action<ExitResult>? OnExit { get; set; }
    }
}