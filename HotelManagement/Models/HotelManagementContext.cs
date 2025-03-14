﻿using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace HotelManagement.Models
{
    public class HotelManagementContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<ReservationHasService> ReservationHasServices { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId).HasName("PRIMARY");

                entity.ToTable("User");

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasColumnType("INT")
                    .IsRequired();

                entity.Property(e => e.FirstName)
                    .HasColumnName("FirstName")
                    .HasColumnType("VARCHAR(45)")
                    .IsRequired(false);

                entity.Property(e => e.LastName)
                    .HasColumnName("LastName")
                    .HasColumnType("VARCHAR(45)")
                    .IsRequired(false);

                entity.Property(e => e.Username)
                    .HasColumnName("Username")
                    .HasColumnType("VARCHAR(45)")
                    .IsRequired();

                entity.Property(e => e.PasswordHash)
                    .HasColumnName("PasswordHash")
                    .HasColumnType("VARCHAR(200)")
                    .IsRequired(false);

                entity.Property(e => e.Role)
                    .HasColumnName("Role")
                    .HasColumnType("ENUM('admin', 'receptionist')")
                    .IsRequired(false);
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasKey(r => r.RoomID).HasName("PRIMARY");

                entity.ToTable("Room");

                entity.Property(r => r.RoomID)
                    .HasColumnName("RoomID")
                    .HasColumnType("INT")
                    .IsRequired();

                entity.Property(r => r.RoomNumber)
                    .HasColumnName("Room_Number")
                    .HasColumnType("INT")
                    .IsRequired();

                entity.Property(r => r.Floor)
                    .HasColumnName("Floor")
                    .HasColumnType("INT")
                    .IsRequired();

                entity.Property(r => r.RoomTypeId)
                    .HasColumnName("RoomTypeId")
                    .HasColumnType("INT")
                    .IsRequired();
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.HasKey(s => s.ServiceId).HasName("PRIMARY");

                entity.ToTable("Service");

                entity.Property(s => s.ServiceId)
                    .HasColumnName("ServiceID")
                    .HasColumnType("INT")
                    .IsRequired();

                entity.Property(s => s.Name)
                    .HasColumnName("Name")
                    .HasColumnType("VARCHAR(45)")
                    .IsRequired();

                entity.Property(s => s.Price)
                    .HasColumnName("Price")
                    .HasColumnType("DECIMAL(10,2)")
                    .IsRequired();
            });


            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.HasKey(r => r.ReservationID).HasName("PRIMARY");

                entity.ToTable("Reservation");

                entity.Property(r => r.ReservationID)
                    .HasColumnName("ReservationID")
                    .HasColumnType("INT")
                    .IsRequired();

                entity.Property(r => r.CheckInDate)
                    .HasColumnName("Check_In_Date")
                    .HasColumnType("DATETIME")
                    .IsRequired();

                entity.Property(r => r.CheckOutDate)
                    .HasColumnName("Check_Out_Date")
                    .HasColumnType("DATETIME")
                    .IsRequired();
                
                entity.Property(r => r.GuestID)
                    .HasColumnName("GuestID")
                    .HasColumnType("INT")
                    .IsRequired();

                entity.Property(r => r.RoomID)
                    .HasColumnName("RoomID")
                    .HasColumnType("INT")
                    .IsRequired();

                entity.Property(r => r.UserID)
                    .HasColumnName("UserID")
                    .HasColumnType("INT")
                    .IsRequired();
               
            });
            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.HasKey(i => i.InvoiceID).HasName("PRIMARY");

                entity.ToTable("Invoice");

                entity.Property(i => i.InvoiceID)
                    .HasColumnName("InvoiceID")
                    .HasColumnType("INT")
                    .IsRequired();

                entity.Property(i => i.IssuedDate)
                    .HasColumnName("Issued_Date")
                    .HasColumnType("DATETIME")
                    .IsRequired();

                entity.Property(i => i.TotalAmount)
                    .HasColumnName("Total_Amount")
                    .HasColumnType("DECIMAL(10,2)")
                    .IsRequired();

                entity.Property(i => i.GuestID)
                    .HasColumnName("GuestID")
                    .HasColumnType("INT")
                    .IsRequired();

                entity.Property(i => i.ReservationID)
                    .HasColumnName("ReservationID")
                    .HasColumnType("INT")
                    .IsRequired();

            });


            modelBuilder.Entity<Guest>(entity =>
            {
                entity.HasKey(e => e.GuestId).HasName("PRIMARY");

                entity.ToTable("Guest");

                entity.Property(e => e.GuestId)
                    .HasColumnName("GuestID")
                    .HasColumnType("INT")
                    .IsRequired();

                entity.Property(e => e.FirstName)
                    .HasColumnName("First_Name")
                    .HasColumnType("VARCHAR(45)")
                    .IsRequired(false);

                entity.Property(e => e.LastName)
                    .HasColumnName("Last_Name")
                    .HasColumnType("VARCHAR(45)")
                    .IsRequired(false);

                entity.Property(e => e.PassportNumber)
                    .HasColumnName("Passport_Number")
                    .HasColumnType("VARCHAR(45)")
                    .IsRequired(false);

                entity.Property(e => e.Email)
                    .HasColumnName("Email")
                    .HasColumnType("VARCHAR(100)")
                    .IsRequired(false);

                entity.Property(e => e.PhoneNumber)
                    .HasColumnName("Phone_Number")
                    .HasColumnType("VARCHAR(20)")
                    .IsRequired(false);
            });

            modelBuilder.Entity<ReservationHasService>(entity =>
            {
                entity.HasKey(rh => rh.ReservationHasServiceId).HasName("PRIMARY");

                entity.ToTable("Reservation_Has_Service");

                entity.Property(rh => rh.ReservationHasServiceId)
                    .HasColumnName("ReservationHasServiceId")
                    .HasColumnType("INT")
                    .IsRequired();

                entity.Property(rh => rh.Quantity)
                    .HasColumnName("Quantity")
                    .HasColumnType("INT")
                    .IsRequired();

                entity.Property(rh => rh.ServiceId)
                    .HasColumnName("ServiceId")
                    .HasColumnType("INT")
                    .IsRequired();

                entity.Property(rh => rh.ReservationId)
                    .HasColumnName("ReservationId")
                    .HasColumnType("INT")
                    .IsRequired();

                entity.Property(rh => rh.TotalPrice)
                    .HasColumnName("Total_Price")
                    .HasColumnType("DECIMAL(10,2)")
                    .IsRequired();

            });


            modelBuilder.Entity<RoomType>(entity =>
            {
                entity.HasKey(rt => rt.RoomTypeId).HasName("PRIMARY");

                entity.ToTable("Room_Type");

                entity.Property(rt => rt.RoomTypeId)
                    .HasColumnName("RoomTypeId")
                    .HasColumnType("INT")
                    .IsRequired();

                entity.Property(rt => rt.Name)
                    .HasColumnName("Name")
                    .HasColumnType("VARCHAR(45)")
                    .IsRequired();

                entity.Property(rt => rt.PricePerNight)
                    .HasColumnName("PricePerNight")
                    .HasColumnType("DECIMAL(10, 2)")
                    .IsRequired();

                entity.Property(rt => rt.MaxNumOfGuests)
                    .HasColumnName("MaxNumOfGuests")
                    .HasColumnType("INT")
                    .IsRequired();
            });

        }


    }
}
