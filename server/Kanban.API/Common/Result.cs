using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace Kanban.API.Common
{
    public record struct Result<T>
    {
        private enum ResultState
        {
            Null,
            Failure,
            Success
        }

        private readonly ResultState _state;

        public T Value { get; init; } = default!;

        public Exception? Exception { get; init; }

        public bool IsSuccess => _state == ResultState.Success;

        public bool IsFailure => _state == ResultState.Failure;

        public bool IsNull => _state == ResultState.Null;

        private Result(ResultState state, T value, Exception? exception)
        {
            _state = state;
            Value = value;
            Exception = exception;
        }

        public Result(T value) : this(ResultState.Success, value, null) { }

        public Result(Exception exception) : this(ResultState.Failure, default!, exception) { }

        [Pure]
        public TR Match<TR>(Func<T, TR> onSuccess,
                            Func<Exception, TR> onFailure,
                            Func<TR>? onNull = null) =>
            IsSuccess ? onSuccess(Value)
          : IsFailure ? onFailure(Exception!)
          : onNull is not null
                ? onNull()
                : throw new InvalidOperationException(
                      "Result is null, but no onNull function was provided.");
                  
        public static implicit operator Result<T>(T?        value) =>
            value is not null ? new(value) : new Result<T>();

        public static implicit operator Result<T>(Exception exception) =>
            new(exception);
    }
}