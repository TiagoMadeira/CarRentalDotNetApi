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

    public  static class RentaltErrors
    {
        public static readonly Errors RentalDoesNotExist = new(new Dictionary<string, string[]> { { "Rental", ["Rental does exist"] } }, "Rental does exist");
        public static readonly Errors RentalVehicleDoesNotExist = new(new Dictionary<string, string[]> { { "Rental", ["Vehicle does not exist"] } }, "Vehicle");
        public static readonly Errors RentalVehicleIsNotAvailable = new(new Dictionary<string, string[]> { { "Rental", ["Vehicle is not available for input dates"] } }, "Vehicle is not available");

    }
}
