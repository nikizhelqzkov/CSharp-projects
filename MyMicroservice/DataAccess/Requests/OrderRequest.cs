using MyMicroservice.DTOModels;

namespace MyMicroservice.DataAccess.Requests
{
    public class OrderRequest
    {
        public OrderDTO Order { get; set; }

        public virtual ICollection<OrderItemsDTO>? OrderItems { get; set; }
    }
}
