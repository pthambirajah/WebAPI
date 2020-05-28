using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPITuto.Models
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options):base(options)
        {

        }
        public TodoContext()
        {

        }

        //public DbSet<ToDoItem> ToDoItems { get; set; }
        public DbSet<Flight> FlightSet { get; set; }
        public DbSet<Passenger> Passengers { get; set; }

        public static string ConnectionString { get; set; } = @"Server=(localDB)\MSSQLLocalDB;Database=WWWings_2020Step2;Trusted_Connection=True;MultipleActiveResultSets=True;App=WebAPITuto";

       protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
           
                builder.UseSqlServer(ConnectionString);
            
            // convinient, flexible BUT DANGEROUS FOR PERFORMANCE
            //builder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            // composed
            builder.Entity<Booking>().HasKey(x => new { x.FlightNo, x.PassengerID });

            // mapping many to many relationship
            builder.Entity<Booking>()
                .HasOne(x => x.Flight)
                .WithMany(x => x.BookingSet)
                .HasForeignKey(x => x.FlightNo)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Booking>()
                .HasOne(x => x.Passenger)
                .WithMany(x => x.BookingSet)
                .HasForeignKey(x => x.PassengerID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
