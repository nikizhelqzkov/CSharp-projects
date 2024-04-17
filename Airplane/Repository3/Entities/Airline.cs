﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Repository3.Entities;

public partial class Airline
{
    [Key]
    [Column("AirlineID")]
    public int AirlineId { get; set; }

    [StringLength(255)]
    public string Name { get; set; }

    public int? Founded { get; set; }

    public int? FleetSize { get; set; }

    public string Description { get; set; }
}