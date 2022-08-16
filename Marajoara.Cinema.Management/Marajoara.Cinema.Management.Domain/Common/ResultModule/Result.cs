using System;

namespace Marajoara.Cinema.Management.Domain.Common.ResultModule
{
    using static Helpers;
    public struct Result<TFailure, TSuccess>
    {
        public TFailure Failure { get; set; }
        public TSuccess Success { get; set; }

        public bool IsFailure { get; set; }
        public bool IsSuccess => !IsFailure;

        public Option<TFailure> OptionalFailure => IsFailure ? Some(Failure) : None;

        public Option<TSuccess> OptionalSuccess => IsSuccess ? Some(Success) : None;

        internal Result(TFailure failure)
        {
            IsFailure = true;
            Failure = failure;
            Success = default;
        }

        internal Result(TSuccess success)
        {
            IsFailure = false;
            Failure = default;
            Success = success;
        }

        public TResult Match<TResult>(
                Func<TFailure, TResult> failure,
                Func<TSuccess, TResult> success
            )
            => IsFailure ? failure(Failure) : success(Success);

        public Unit Match(
                Action<TFailure> failure,
                Action<TSuccess> success
            )
            => Match(ToFunc(failure), ToFunc(success));

        public static implicit operator Result<TFailure, TSuccess>(TFailure failure) => new Result<TFailure, TSuccess>(failure);

        public static implicit operator Result<TFailure, TSuccess>(TSuccess success) => new Result<TFailure, TSuccess>(success);

        public static Result<TFailure, TSuccess> Of(TSuccess obj) => obj;

        public static Result<TFailure, TSuccess> Of(TFailure obj) => obj;
    }

    public struct Unit
    {
        public static Unit Successful { get { return new Unit(); } }
    }

    public static partial class Helpers
    {
        private static readonly Unit unit = new Unit();
        public static Unit Unit() => unit;

        public static Func<T, Unit> ToFunc<T>(Action<T> action) => o =>
        {
            action(o);
            return Unit();
        };

        public static Func<Unit> ToFunc(Action action) => () =>
        {
            action();
            return Unit();
        };
    }
}
