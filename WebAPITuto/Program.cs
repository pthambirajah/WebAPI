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
            
            using (var ctx = new TodoContext())
            {
                ctx.BookingSet.RemoveRange(ctx.BookingSet);

                ctx.SaveChanges();
            }
            
            NewPassengers();
            NewBooking();
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

        public static void NewBooking()
        {
            using (var ctx = new TodoContext())
            {

                ctx.BookingSet.Add(new Booking { FlightNo = 1, PassengerID = 1 });

                Flight f = ctx.FlightSet.Find(2);

                //ctx.Entry(f).Collection(x => x.BookingSet).Load();
                // lazy loading 

                //f.BookingSet.Add(new Booking { PassengerID = 1 });

                // have a passenger
                Passenger p = ctx.Passengers.Find(2);

                //ctx.Entry(p).Collection(x => x.BookingSet).Load();
                // lazy loading

                //p.BookingSet.Add(new Booking { FlightNo = 1 });

                ctx.BookingSet.Add(new Booking { Flight = f, Passenger = ctx.Passengers.Find(3) });

                ctx.SaveChanges();

            }
        }
        public static void NewPassengers()
        {
            using (var ctx = new TodoContext())
            {
                Passenger p1 = new Passenger() { GivenName = "Igor" };
                ctx.Passengers.Add(p1);

                Passenger p2 = new Passenger() { GivenName = "Toto" };
                ctx.Passengers.Add(p2);

                Passenger p3 = new Passenger() { GivenName = "Anne"};
                ctx.Passengers.Add(p3);

                ctx.SaveChanges();
            }
        }
    }
}
