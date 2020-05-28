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
    }
}
