namespace MyMicroservice.DTOModels
{
    public class OrderItemsDTO
    {
        public int ItemId { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public decimal ListPrice { get; set; }
        public decimal Discount { get; set; }


    }
}
