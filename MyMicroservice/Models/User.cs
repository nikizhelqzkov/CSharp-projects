using System.ComponentModel.DataAnnotations;

namespace MyMicroservice.Models
{
    public class User
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Name { get; set; }

        public int Age { get; set; }

    }
}
