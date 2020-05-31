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
                Flight flight3 = new Flight { Departure = "GVA", Destination = "MLN", Seats = 350, Date = new DateTime(2020, 05, 31), basePrice = 250 };
                Flight flight4 = new Flight { Departure = "GVA", Destination = "LAX", Seats = 350, Date = new DateTime(2020, 05, 31), basePrice = 250 };
                Flight flight5 = new Flight { Departure = "LAX", Destination = "GVA", Seats = 2, Date = new DateTime(2020, 05, 31), basePrice = 250 };

                ctx.FlightSet.Add(flight3);
                ctx.FlightSet.Add(flight4);
                ctx.FlightSet.Add(flight5);

                ctx.SaveChanges();
            }
        }

        public static void NewBooking()
        {
            using (var ctx = new TodoContext())
            {
                Flight f = ctx.FlightSet.Find(1);
                Flight f1 = ctx.FlightSet.Find(2);
                Flight f5 = ctx.FlightSet.Find(3);

                Passenger p = ctx.Passengers.Find(1);
                Passenger p2 = ctx.Passengers.Find(2);
                Passenger p3 = ctx.Passengers.Find(3);

                Booking a = new Booking { Flight = f1, Passenger = p2, PersonID = p2.PersonID, FlightNo = f1.FlightNo, SalePrice = 888 };
                ctx.BookingSet.Add(a);
                f1.BookingSet.Add(a);
                p2.BookingSet.Add(a);



                Booking b = new Booking { PersonID = p.PersonID, FlightNo = f.FlightNo, Flight = f, Passenger = p, SalePrice = 100 };
                Booking c = new Booking { PersonID = p3.PersonID, FlightNo = f.FlightNo, Flight = f, Passenger = p3, SalePrice = 200 };
                //Booking d = new Booking { PersonID = p2.PersonID, FlightNo = f1.FlightNo, Flight = f1, Passenger = p2, SalePrice = 200 };



                ctx.BookingSet.Add(b);
                ctx.BookingSet.Add(c);
                //ctx.BookingSet.Add(d);


                //ctx.Entry(p).Collection(x => x.BookingSet).Load();
                // lazy loading

                p.BookingSet = new List<Booking>();
                p.BookingSet.Add(new Booking { FlightNo = 1 });



                ctx.BookingSet.Add(new Booking { Flight = f5, Passenger = p2, PersonID = p2.PersonID, FlightNo = f5.FlightNo, SalePrice = 758 });


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

                Passenger p3 = new Passenger() { GivenName = "Anne" };

                ctx.Passengers.Add(p3);

                ctx.SaveChanges();
            }
        }
    }
}
