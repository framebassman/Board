using RestSharp.Deserializers;

namespace Board.Server.Models
{
    public class VirutalMachine
    {
        public string Name { get; set; }
        public string Status => IsBusy ? "Busy" : "Free";
        public string UsedBy { get; set; }
        public bool IsBusy { get; set; }

        [DeserializeAs(Name = "IpAddressString")]
        public string IpAddress { get; set; }
    }
}