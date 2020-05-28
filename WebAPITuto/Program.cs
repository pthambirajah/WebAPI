using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebAPITuto.Models;

namespace WebAPITuto
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateDatabase();
            NewFlights();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static void CreateDatabase()
        {
            using (var ctx = new TodoContext())
            {

                var e = ctx.Database.EnsureCreated();

                if (e)
                    Console.WriteLine("Database has been created !");
            }
        }

        private static void NewFlights()
        {
            using (var ctx = new TodoContext())
            {
                // add a Flight in FlightSet .... with Linq language 
                Flight flight1 = new Flight { Departure = "GVA", Destination = "LAX", Seats = 350, Date = new DateTime() };
                
                ctx.FlightSet.Add(flight1);

                Flight flight2 = new Flight { Departure = "LAX", Destination = "GVA", Seats = 350, Date = new DateTime() };
             
                ctx.FlightSet.Add(flight2);

                Flight flight3 = new Flight { Departure = "GVA", Destination = "MLN", Seats = 100, Date = new DateTime() };
               
                ctx.FlightSet.Add(flight3);

                ctx.SaveChanges();
            }
        }
    }
}
