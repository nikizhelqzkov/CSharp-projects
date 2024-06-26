﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MyMicroservice.Models
{
    [Table("stocks", Schema = "production")]
    public partial class Stock
    {
        [Key]
        [Column("store_id")]
        public int StoreId { get; set; }
        [Key]
        [Column("product_id")]
        public int ProductId { get; set; }
        [Column("quantity")]
        public int? Quantity { get; set; }

        [ForeignKey("ProductId")]
        [InverseProperty("Stocks")]
        public virtual Product Product { get; set; }
        [ForeignKey("StoreId")]
        [InverseProperty("Stocks")]
        public virtual Store Store { get; set; }
    }
}