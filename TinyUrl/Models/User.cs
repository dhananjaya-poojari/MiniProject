using System.ComponentModel.DataAnnotations;

namespace TinyUrl.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string APIKey { get; set; }
    }
}
