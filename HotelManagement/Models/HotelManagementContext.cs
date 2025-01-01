using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace HotelManagement.Models
{
    public class HotelManagementContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Invoice> Invoices { get; set; }


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

            modelBuilder.Ignore<Guest>();


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

                entity.Property(r => r.RoomType)
                    .HasColumnName("Room_Type")
                    .HasColumnType("ENUM('SingleRoom', 'DoubleRoom', 'Suite', 'DeluxeRoom', 'FamilyRoom')")
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

                entity.Property(r => r.NumberOfGuests)
                    .HasColumnName("Number_Of_Guests")
                    .HasColumnType("INT")
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


                /*
                // Definisanje odnosa sa Room entitetom
                entity.HasOne<Room>()
                    .WithMany()
                    .HasForeignKey(r => r.RoomID)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Reservation_Room");

                // Definisanje odnosa sa User entitetom
                entity.HasOne<User>()
                    .WithMany()
                    .HasForeignKey(r => r.UserID)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Reservation_User");

                // Opcionalno, definisanje odnosa sa Guest entitetom (ako postoji)
                entity.HasOne<Guest>()
                    .WithMany()
                    .HasForeignKey(r => r.GuestID)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Reservation_Guest");

                */
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

                /*
                // Definisanje odnosa sa tabelom Reservation
                entity.HasOne<Reservation>()
                    .WithMany()
                    .HasForeignKey(i => i.ReservationID)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Invoice_Reservation");

                // Definisanje odnosa sa tabelom Guest (ako postoji)
                entity.HasOne<Guest>()
                    .WithMany()
                    .HasForeignKey(i => i.GuestID)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Invoice_Guest");
                */
            });


        }


    }
}
