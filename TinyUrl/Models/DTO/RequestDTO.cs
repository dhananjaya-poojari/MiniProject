namespace TinyUrl.Models.DTO
{
    public class RequestDTO
    {
        public string URL { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
}
