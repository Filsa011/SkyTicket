using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SkyTicket.Data.Models;

public partial class SkyTicketContext : DbContext
{
    public SkyTicketContext(DbContextOptions<SkyTicketContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Aircraft> Aircraft { get; set; }

    public virtual DbSet<Airport> Airports { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Flight> Flights { get; set; }

    public virtual DbSet<Passenger> Passengers { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aircraft>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("aircraft");

            entity.Property(e => e.Model).HasMaxLength(100);
        });

        modelBuilder.Entity<Airport>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("airports");

            entity.HasIndex(e => e.Code, "Code").IsUnique();

            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.Code).HasMaxLength(10);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("bookings");

            entity.HasIndex(e => e.FlightId, "FlightId");

            entity.HasIndex(e => e.PassengerId, "PassengerId");

            entity.Property(e => e.BookingDate).HasColumnType("datetime");
            entity.Property(e => e.SeatNumber).HasMaxLength(10);
            entity.Property(e => e.Status).HasMaxLength(30);

            entity.HasOne(d => d.Flight).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.FlightId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("bookings_ibfk_1");

            entity.HasOne(d => d.Passenger).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.PassengerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("bookings_ibfk_2");
        });

        modelBuilder.Entity<Flight>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("flights");

            entity.HasIndex(e => e.AircraftId, "AircraftId");

            entity.HasIndex(e => e.ArrivalAirportId, "ArrivalAirportId");

            entity.HasIndex(e => e.DepartureAirportId, "DepartureAirportId");

            entity.Property(e => e.ArrivalTime).HasColumnType("datetime");
            entity.Property(e => e.DepartureTime).HasColumnType("datetime");
            entity.Property(e => e.FlightNumber).HasMaxLength(20);
            entity.Property(e => e.Price).HasPrecision(10);

            entity.HasOne(d => d.Aircraft).WithMany(p => p.Flights)
                .HasForeignKey(d => d.AircraftId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("flights_ibfk_3");

            entity.HasOne(d => d.ArrivalAirport).WithMany(p => p.FlightArrivalAirports)
                .HasForeignKey(d => d.ArrivalAirportId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("flights_ibfk_2");

            entity.HasOne(d => d.DepartureAirport).WithMany(p => p.FlightDepartureAirports)
                .HasForeignKey(d => d.DepartureAirportId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("flights_ibfk_1");
        });

        modelBuilder.Entity<Passenger>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("passengers");

            entity.HasIndex(e => e.PassportNumber, "PassportNumber").IsUnique();

            entity.Property(e => e.BirthDate).HasColumnType("date");
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.PassportNumber).HasMaxLength(30);
            entity.Property(e => e.Patronymic).HasMaxLength(100);
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("payments");

            entity.HasIndex(e => e.BookingId, "BookingId");

            entity.Property(e => e.Amount).HasPrecision(10);
            entity.Property(e => e.PaymentDate).HasColumnType("datetime");
            entity.Property(e => e.PaymentMethod).HasMaxLength(30);
            entity.Property(e => e.Status).HasMaxLength(30);

            entity.HasOne(d => d.Booking).WithMany(p => p.Payments)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("payments_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
