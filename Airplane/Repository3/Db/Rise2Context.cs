﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Repository3.Entities;

namespace Repository3.Db;

public partial class Rise2Context : DbContext
{
    public Rise2Context()
    {
    }

    public Rise2Context(DbContextOptions<Rise2Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Airline> Airlines { get; set; }

    public virtual DbSet<Airport> Airports { get; set; }

    public virtual DbSet<Flight> Flights { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        //=> optionsBuilder.UseSqlServer("Data Source=NZZHIVKOV-CM\\SQLEXPRESS;Initial Catalog=Rise2;Integrated Security=True,Encrypt=False");
        => optionsBuilder.UseSqlServer("data source=NZZHIVKOV-CM\\SQLEXPRESS;initial catalog=Rise2;integrated security=True;MultipleActiveResultSets=True;Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Airline>(entity =>
        {
            entity.HasKey(e => e.AirlineId).HasName("PK__Airlines__DC45827329681210");
        });

        modelBuilder.Entity<Airport>(entity =>
        {
            entity.HasKey(e => e.AirportId).HasName("PK__Airports__E3DBE08A93557503");

            entity.Property(e => e.Code).IsFixedLength();
        });

        modelBuilder.Entity<Flight>(entity =>
        {
            entity.HasKey(e => e.FlightNumber).HasName("PK__Flights__2EAE6F51832A63F1");

            entity.Property(e => e.FromAirport).IsFixedLength();
            entity.Property(e => e.ToAirport).IsFixedLength();

            entity.HasOne(d => d.FromAirportNavigation).WithMany(p => p.FlightFromAirportNavigations)
                .HasPrincipalKey(p => p.Code)
                .HasForeignKey(d => d.FromAirport)
                .HasConstraintName("FK_FromAirport");

            entity.HasOne(d => d.ToAirportNavigation).WithMany(p => p.FlightToAirportNavigations)
                .HasPrincipalKey(p => p.Code)
                .HasForeignKey(d => d.ToAirport)
                .HasConstraintName("FK_ToAirport");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}