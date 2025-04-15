namespace api.Shared
{
    public class Errors
    {
        public Errors(IDictionary<string,string[]> error, string code)
        {
            Error = error;
            Code = code;
        }

        public IDictionary<string, string[]> Error { get; }
        public string Code { get; }
    }

    public  static class RentalDoesNotExsistError 
    {
        public static readonly Errors RentalDoesNotExist = new(new Dictionary<string, string[]> { { "Rental", ["Rental does exist"] } }, "Rental does exist");
    }
}
