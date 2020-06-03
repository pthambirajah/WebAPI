using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
            //NewPassengers();
            //Delete existing bookings
            using (var ctx = new TodoContext())
            {
                ctx.BookingSet.RemoveRange(ctx.BookingSet);

                ctx.SaveChanges();
            }

            NewBooking();

            //CheckState();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        //Method used to create new database.
        private static void CreateDatabase()
        {
            using (var ctx = new TodoContext())
            {

                var e = ctx.Database.EnsureCreated();

                if (e)
                    Console.WriteLine("Database has been created !");
            }
        }

        //Method used to add new flights.
        private static void NewFlights()
        {
            using (var ctx = new TodoContext())
            {
                FlightFactory flightFactory = new BoeingFactory(new PreSaleState());

                Boeing boeing = (Boeing)flightFactory.CreateFlight();

                //Check what is the state of the journey
                CheckState(boeing);

                //open the sales of this particular boeing
                boeing.OpenSales("MTX", "BKK", new DateTime(2020, 07, 1), 199);

                //check the state after the opening of the sales
                CheckState(boeing);

                ctx.FlightSet.Add(boeing);

                ctx.SaveChanges();
            }
        }
        //Method used to add new bookings.
        public static void NewBooking()
        {
            using (var ctx = new TodoContext())
            {
                Flight f = ctx.FlightSet.Find(5);
                Flight f1 = ctx.FlightSet.Find(2);

                Passenger p = ctx.Passengers.Find(1);
                Passenger p2 = ctx.Passengers.Find(2);

                Booking a = new Booking { Flight = f1, Passenger = p2, PersonID = p2.PersonID, FlightNo = f1.FlightNo, SalePrice = 888 };
                ctx.BookingSet.Add(a);

                Booking b = new Booking { PersonID = p.PersonID, FlightNo = f.FlightNo, Flight = f, Passenger = p, SalePrice = 100 };

                ctx.BookingSet.Add(b);

                ctx.SaveChanges();

            }
        }

        //Method used to add new passengers.
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

        //method used to check the state of the sales
        public static void CheckState(Flight flight)
        {
            Boeing toCheck = (Boeing)flight;
            string canbuyticket = toCheck.CanBuyTickets();
            Console.WriteLine(canbuyticket);
        }
    }
}

