﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable enable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Repository.Entities;

[Index("Code", Name = "UC_Airports_Code", IsUnique = true)]
[Index("Code", Name = "idx_code")]
public partial class Airport
{
    [Key]
    [Column("AirportID")]
    public int AirportId { get; set; }

    [StringLength(255)]
    public string? Name { get; set; }

    [StringLength(255)]
    public string? Country { get; set; }

    [StringLength(255)]
    public string? City { get; set; }

    [StringLength(3)]
    [Unicode(false)]
    public string Code { get; set; } = null!;

    public int? RunwaysCount { get; set; }

    public int? Founded { get; set; }

    [InverseProperty("FromAirportNavigation")]
    public virtual ICollection<Flight> FlightFromAirportNavigations { get; set; } = new List<Flight>();

    [InverseProperty("ToAirportNavigation")]
    public virtual ICollection<Flight> FlightToAirportNavigations { get; set; } = new List<Flight>();
}