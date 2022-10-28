using MyMicroservice.Models;

namespace MyMicroservice.DTOModels
{
    public class CustomerDTO
    {
        public int CustomerId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string? Phone { get; set; }

        public string Email { get; set; }

        public string? Street { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? ZipCode { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<Order?>? Orders { get; set; }
    }
}
