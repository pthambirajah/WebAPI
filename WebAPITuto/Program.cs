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
           // NewFlights();
            NewPassengers();
            //Delete existing bookings
            using (var ctx = new TodoContext())
            {
                ctx.BookingSet.RemoveRange(ctx.BookingSet);

                ctx.SaveChanges();
            }
            
            
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
               /* Flight flight1 = new Flight { Departure = "GVA", Destination = "LAX", Seats = 350, Date = new DateTime(2021, 04, 30), basePrice = 450, AvailableSeats = 60 };
                
                ctx.FlightSet.Add(flight1);

                Flight flight2 = new Flight { Departure = "LAX", Destination = "GVA", Seats = 350, Date = new DateTime(2020, 05, 30), basePrice = 500, AvailableSeats = 350};
             
                ctx.FlightSet.Add(flight2);*/

                Flight flight3 = new Flight { Departure = "GVA", Destination = "MLN", Seats = 350, Date = new DateTime(2020, 05, 31), basePrice = 250, AvailableSeats = 260 };
               
                ctx.FlightSet.Add(flight3);

                ctx.SaveChanges();
            }
        }

        public static void NewBooking()
        {
            using (var ctx = new TodoContext())
            {
                Flight f1 = ctx.FlightSet.Find(1);
                Flight f5 = ctx.FlightSet.Find(4);
                if (f1.AvailableSeats > 0)
                {
                //    ctx.BookingSet.Add(new Booking { FlightNo = f1.FlightNo, PersonID = 1, SalePrice = 555 });
                //    f1.AvailableSeats = 0;
                }
                Flight f = ctx.FlightSet.Find(3);
                // have a passenger
                Passenger p = ctx.Passengers.Find(2);
                Passenger p2 = ctx.Passengers.Find(3);
                Passenger p3 = ctx.Passengers.Find(4);
                //ctx.Entry(f).Collection(x => x.BookingSet).Load();
                // lazy loading 

                Booking b = new Booking { PersonID = p.PersonID, FlightNo = f.FlightNo, Flight = f, Passenger = p, SalePrice = 100 };
                Booking c = new Booking { PersonID = p3.PersonID, FlightNo = f.FlightNo, Flight = f, Passenger = p3, SalePrice = 200 };
                

                ctx.BookingSet.Add(b);
                ctx.BookingSet.Add(c);

                //ctx.Entry(p).Collection(x => x.BookingSet).Load();
                // lazy loading

                p.BookingSet = new List<Booking>();
                p.BookingSet.Add(new Booking { FlightNo = 1});

                if (f.AvailableSeats > 0)
                {
                    ctx.BookingSet.Add(new Booking { Flight = f1, Passenger = p2, PersonID = p2.PersonID, FlightNo = f1.FlightNo, SalePrice = 888 });
                    f.AvailableSeats--;
                    ctx.BookingSet.Add(new Booking { Flight = f5, Passenger = p2, PersonID = p2.PersonID, FlightNo = f5.FlightNo, SalePrice = 758 });
                    f5.AvailableSeats--;
                }
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
