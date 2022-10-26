using MyMicroservice.Models;

namespace MyMicroservice.DTOModels
{
    public class OrderDTO
    {

        public int Id { get; set; }

        public int StoreId { get; set; }

        public int StaffId { get; set; }

        public int CustomerId { get; set; }

        public DateTime RequiredDate { get; set; }

        public DateTime? ShippedDate { get; set; }

        public string? StoreName { get; set; }

        public string? CustomerFirstName { get; set; }

        public string? CustomerLastName { get; set; }

        public string? CustomerEmail { get; set; }

        public string? StaffFirstName { get; set; }

        public string? StaffLastName { get; set; }

        public virtual ICollection<OrderItemsDTO>? OrderItems { get; set; }

    }
}
