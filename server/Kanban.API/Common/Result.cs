using System.Diagnostics.Contracts;


namespace Kanban.API.Common
{
    /// <summary>
    /// Represents the result of an operation that can succeed with a value, fail with an exception, or be null.
    /// Provides a functional approach to error handling without throwing exceptions.
    /// </summary>
    /// <typeparam name="T">The type of the value when the operation succeeds.</typeparam>
    public record struct Result<T>
    {
        /// <summary>
        /// Defines the possible states of a Result.
        /// </summary>
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

        /// <summary>
        /// Initializes a new instance of the Result struct with the specified state, value, and exception.
        /// </summary>
        /// <param name="state">The state of the result.</param>
        /// <param name="value">The value when successful.</param>
        /// <param name="exception">The exception when failed.</param>
        private Result(ResultState state, T value, Exception? exception)
        {
            _state = state;
            Value = value;
            Exception = exception;
        }

        public Result(T value) : this(ResultState.Success, value, null) { }

        public Result(Exception exception) : this(ResultState.Failure, default!, exception) { }

        /// <summary>
        /// Executes one of the provided functions based on the result state and returns the result.
        /// </summary>
        /// <typeparam name="TR">The return type of the match operation.</typeparam>
        /// <param name="onSuccess">Function to execute when the result is successful.</param>
        /// <param name="onFailure">Function to execute when the result is a failure.</param>
        /// <param name="onNull">Optional function to execute when the result is null.</param>
        /// <returns>The result of the executed function.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the result is null and no onNull function is provided.</exception>
        /// <example>
        /// <code>
        /// // Usage in API controllers with TypedResults
        /// app.MapGet("/api/users/{id}", async (int id, IUserRepository repository) =>
        /// {
        ///     var result = await GetUserById(id, repository);
        ///     
        ///     return result.Match(
        ///         onSuccess: user => Results.Ok(user),
        ///         onFailure: ex => Results.Problem(ex.Message),
        ///         onNull: () => Results.NotFound()
        ///     );
        /// });
        /// 
        /// // Example service method returning Result
        /// async Task&lt;Result&lt;User&gt;&gt; GetUserById(int id, IUserRepository repository)
        /// {
        ///     try
        ///     {
        ///         var user = await repository.FindByIdAsync(id);
        ///         return user; // Implicit conversion from T? to Result&lt;T&gt;
        ///     }
        ///     catch (Exception ex)
        ///     {
        ///         return ex; // Implicit conversion from Exception to Result&lt;T&gt;
        ///     }
        /// }
        /// </code>
        /// </example>
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
                  
        /// <summary>
        /// Implicitly converts a value to a Result. If the value is null, creates a null Result.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>A successful Result if the value is not null, otherwise a null Result.</returns>
        public static implicit operator Result<T>(T? value) =>
            value is not null ? new(value) : new Result<T>();

        /// <summary>
        /// Implicitly converts an exception to a failed Result.
        /// </summary>
        /// <param name="exception">The exception to convert.</param>
        /// <returns>A failed Result containing the exception.</returns>
        public static implicit operator Result<T>(Exception exception) =>
            new(exception);
    }
}