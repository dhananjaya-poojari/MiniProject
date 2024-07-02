namespace TinyUrl.Models.DTO
{
    public class ResponseDTO
    {
        public string RedirectedUrl { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string OriginalUrl { get; set; }
    }
}
