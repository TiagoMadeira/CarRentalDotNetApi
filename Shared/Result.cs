namespace api.Shared
{
    public class Result<T>
    {
        private Result(T value)
        {
            Value = value;
            Errors = null;
        }
        private Result(Errors errors)
        {
            Errors = errors;
            Value = default;
        }

        public static Result<T> Success(T value) => new Result<T>(value);
        public static Result<T> Failure(Errors errors) => new Result<T>(errors);
        public T? Value { get; }
        public Errors? Errors { get; }
        public bool IsSuccess => Errors == null; 
    }
}
