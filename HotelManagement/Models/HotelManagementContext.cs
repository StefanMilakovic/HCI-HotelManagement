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
        //public DbSet<Guest> Guests { get; set; }
        //public DbSet<Reservation> Reservations { get; set; }
        //public DbSet<Room> Rooms { get; set; }

        // Ovaj metod je potreban za konfiguraciju veze sa bazom podataka
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

            modelBuilder.Ignore<Guest>(); // Ignoriši Guest entitet
            modelBuilder.Ignore<Reservation>();
            modelBuilder.Ignore<Room>();

            //ovdje dodati


            modelBuilder.Entity<User>(entity =>
            {
                // Definisanje primarnog ključa
                entity.HasKey(e => e.UserId).HasName("PRIMARY");

                // Postavljanje tabele u bazi podataka
                entity.ToTable("User");

                // Definisanje kolona u tabeli
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

                // Postavljanje indeksa za Username (jedinstven)
                entity.HasIndex(e => e.Username)
                    .HasName("Username_UNIQUE")
                    .IsUnique();

                // Opcioni: Ako želiš da postaviš veze (ako postoje) sa drugim entitetima, možeš to dodati ovde.

            });
        }


    }
}
