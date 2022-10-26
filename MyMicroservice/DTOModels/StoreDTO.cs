using MyMicroservice.Models;

namespace MyMicroservice.DTOModels
{
    public class StoreDTO
    {

        public int StoreId { get; set; }

        public string StoreName { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public string? Street { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? ZipCode { get; set; }

        public virtual ICollection<OrderDTO> Orders { get; set; }

        public virtual ICollection<Staff> Staff { get; set; }

        public virtual ICollection<Stock> Stocks { get; set; }
    }
}