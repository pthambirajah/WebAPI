using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebAPITuto.Models;
using WebAPITuto.Models.Factory;

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
                /*FlightFactory flight3 = new BoeingFactory { Departure = "GVA", Destination = "MLN", Date = new DateTime(2020, 05, 31), basePrice = 250 };
                FlightFactory flight4 = new BoeingFactory { Departure = "GVA", Destination = "LAX",  Date = new DateTime(2020, 05, 31), basePrice = 250 };
                FlightFactory flight5 = new BoeingFactory { Departure = "LAX", Destination = "GVA",  Date = new DateTime(2020, 05, 31), basePrice = 250 };
                FlightFactory flight31 = new BoeingFactory { Departure = "GVA", Destination = "LAX", Date = new DateTime(2021, 03, 21), basePrice = 1 };*/
                FlightFactory flight31 = new BoeingFactory ("GVA", "LAX", new DateTime(2020, 06, 21), 23);
                Flight flight4 = flight31.GetFlight();

                flight4.Departure = "LAX";
                flight4.Destination = "GVA";
                flight4.Date = new DateTime(2021, 03, 21);
                flight4.BasePrice = 999;

                //ctx.FlightSet.Add(flight31);
                ctx.FlightSet.Add(flight4);
                //ctx.FlightSet.Add(flight5);

                ctx.SaveChanges();
            }
        }

        public static void NewBooking()
        {
            using (var ctx = new TodoContext())
            {


                Flight f = ctx.FlightSet.Find(1);
                Flight f1 = ctx.FlightSet.Find(2);
                
                Passenger p = ctx.Passengers.Find(1);
                Passenger p2 = ctx.Passengers.Find(2);

                Booking a = new Booking { Flight = f1, Passenger = p2, PersonID = p2.PersonID, FlightNo = f1.FlightNo, SalePrice = 888 };
                ctx.BookingSet.Add(a);




                Booking b = new Booking { PersonID = p.PersonID, FlightNo = f.FlightNo, Flight = f, Passenger = p, SalePrice = 100 };
                //Booking c = new Booking { PersonID = p3.PersonID, FlightNo = f.FlightNo, Flight = f, Passenger = p3, SalePrice = 200 };
                //Booking d = new Booking { PersonID = p2.PersonID, FlightNo = f1.FlightNo, Flight = f1, Passenger = p2, SalePrice = 200 };



                ctx.BookingSet.Add(b);
                //ctx.BookingSet.Add(c);
                //ctx.BookingSet.Add(d);


                //ctx.Entry(p).Collection(x => x.BookingSet).Load();
                // lazy loading





                //ctx.BookingSet.Add(new Booking { Flight = f5, Passenger = p2, PersonID = p2.PersonID, FlightNo = f5.FlightNo, SalePrice = 758 });


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
