using MyMicroservice.DTOModels;
using MyMicroservice.Models;

namespace MyMicroservice.Helper.CustomMappings
{
    public static class OrderAddition
    {
        public static OrderDTO AdditionMap(Order src, OrderDTO dest)
        {
            if (src.Customer != null)
            {
                dest.CustomerFirstName = src.Customer.FirstName ?? null;
                dest.CustomerLastName = src.Customer.LastName ?? null;
                dest.CustomerEmail = src.Customer.Email ?? null;
            }
            if (src.Store != null)
            {
                dest.StoreName = src.Store.StoreName ?? null;
            }
            if (src.Staff != null)
            {
                dest.StaffFirstName = src.Staff.FirstName ?? null;
                dest.StaffLastName = src.Staff.LastName ?? null;
            }

            return dest;
        }
        public static Order AdditionMap(OrderDTO src, Order dest)
        {
            dest.Store.StoreName = src.StoreName ?? null;
            dest.Customer.FirstName = src.CustomerFirstName ?? null;
            dest.Customer.LastName = src.CustomerLastName ?? null;
            dest.Customer.Email = src.CustomerEmail ?? null;
            dest.Staff.FirstName = src.StaffFirstName ?? null;
            dest.Staff.LastName = src.StaffLastName ?? null;

            return dest;
        }
    }
}
