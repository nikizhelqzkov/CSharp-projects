﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Repository3.Entities;

[Index("FlightNumber", Name = "idx_flight_number")]
public partial class Flight
{
    [Key]
    [StringLength(10)]
    public string FlightNumber { get; set; }

    [StringLength(3)]
    [Unicode(false)]
    public string FromAirport { get; set; }

    [StringLength(3)]
    [Unicode(false)]
    public string ToAirport { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DepartureDateTime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ArrivalDateTime { get; set; }

    [ForeignKey("FromAirport")]
    [InverseProperty("FlightFromAirportNavigations")]
    public virtual Airport FromAirportNavigation { get; set; }

    [ForeignKey("ToAirport")]
    [InverseProperty("FlightToAirportNavigations")]
    public virtual Airport ToAirportNavigation { get; set; }
}