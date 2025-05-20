using api.Models;

namespace api.Helpers
{
    public class VehicleQueryObject
    {
        public string? Model { get; set; }
        public string? Brand { get; set; }
        public string? Category { get; set; }
        public string? Transmission { get; set; }
        public string? VehicleType { get; set; }
    }
}
