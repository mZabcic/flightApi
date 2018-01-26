using NHibernate;
using NHibernate.Cfg;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace FlightControlApi
{
    public class NHibernateSession
    {
        public static ISession OpenSession()
        {
            // Try ulovi exception kad se vrte testovi te onda ne gleda Current server, nego lokalne datoteke
            try
            {
                var configuration = new Configuration();
                var configurationPath = HttpContext.Current.Server.MapPath(@"~\Models\hibernate.cfg.xml");
                configuration.Configure(configurationPath);
                var pilotConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Mappings\Pilot.hbm.xml");
                var planeConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Mappings\Plane.hbm.xml");
                var countryConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Mappings\Country.hbm.xml");
                var airportConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Mappings\Airport.hbm.xml");
                var routeConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Mappings\Route.hbm.xml");
                var airportVMConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Mappings\AirportVM.hbm.xml");
                var routeVMConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Mappings\RouteVM.hbm.xml");
                var flightConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Mappings\Flight.hbm.xml");
                var flightWithTicketsConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Mappings\FlightWithTickets.hbm.xml");
                var flightVMConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Mappings\FlightVM.hbm.xml");
                var storeConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Mappings\Store.hbm.xml");
                var storeVMConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Mappings\StoreVM.hbm.xml");
                var passengerConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Mappings\Passenger.hbm.xml");
                var passengerVMConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Mappings\PassengerVM.hbm.xml");
                var seatClassConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Mappings\SeatClass.hbm.xml");
                var seatConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Mappings\Seat.hbm.xml");
                var seatVMConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Mappings\SeatVM.hbm.xml");
                var ticketConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Mappings\Ticket.hbm.xml");
                var ticketVMConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Mappings\TicketVM.hbm.xml");
                var ticketCreatorConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Mappings\TicketCreator.hbm.xml");
                configuration.AddFile(pilotConfigurationFile);
                configuration.AddFile(planeConfigurationFile);
                configuration.AddFile(countryConfigurationFile);
                configuration.AddFile(airportConfigurationFile);
                configuration.AddFile(routeConfigurationFile);
                configuration.AddFile(airportVMConfigurationFile);
                configuration.AddFile(routeVMConfigurationFile);
                configuration.AddFile(flightConfigurationFile);
                configuration.AddFile(flightVMConfigurationFile);
                configuration.AddFile(storeConfigurationFile);
                configuration.AddFile(storeVMConfigurationFile);
                configuration.AddFile(passengerConfigurationFile);
                configuration.AddFile(passengerVMConfigurationFile);
                configuration.AddFile(seatClassConfigurationFile);
                configuration.AddFile(seatConfigurationFile);
                configuration.AddFile(seatVMConfigurationFile);
                configuration.AddFile(ticketConfigurationFile);
                configuration.AddFile(ticketVMConfigurationFile);
                configuration.AddFile(ticketCreatorConfigurationFile);
                configuration.AddFile(flightWithTicketsConfigurationFile);
                ISessionFactory sessionFactory = configuration.BuildSessionFactory();
                return sessionFactory.OpenSession();
            } catch (NullReferenceException e)
            {
                var configuration = new Configuration();
                var configurationPath = Path.Combine(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()).ToString(), @"Models\hibernateForTesting.cfg.xml");
                configuration.Configure(configurationPath);
                var pilotConfigurationFile = Path.Combine(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()).ToString(), @"Mappings\Pilot.hbm.xml");
                var planeConfigurationFile = Path.Combine(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()).ToString(), @"Mappings\Plane.hbm.xml");
                var countryConfigurationFile = Path.Combine(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()).ToString(), @"Mappings\Country.hbm.xml");
                var airportConfigurationFile = Path.Combine(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()).ToString(), @"Mappings\Airport.hbm.xml");
                var routeConfigurationFile = Path.Combine(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()).ToString(), @"Mappings\Route.hbm.xml");
                var airportVMConfigurationFile = Path.Combine(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()).ToString(), @"Mappings\AirportVM.hbm.xml");
                var routeVMConfigurationFile = Path.Combine(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()).ToString(), @"Mappings\RouteVM.hbm.xml");
                var flightConfigurationFile = Path.Combine(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()).ToString(), @"Mappings\Flight.hbm.xml");
                var flightWithTicketsConfigurationFile = Path.Combine(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()).ToString(), @"Mappings\FlightWithTickets.hbm.xml");
                var flightVMConfigurationFile = Path.Combine(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()).ToString(), @"Mappings\FlightVM.hbm.xml");
                var storeConfigurationFile = Path.Combine(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()).ToString(), @"Mappings\Store.hbm.xml");
                var storeVMConfigurationFile = Path.Combine(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()).ToString(), @"Mappings\StoreVM.hbm.xml");
                var passengerConfigurationFile = Path.Combine(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()).ToString(), @"Mappings\Passenger.hbm.xml");
                var passengerVMConfigurationFile = Path.Combine(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()).ToString(), @"Mappings\PassengerVM.hbm.xml");
                var seatClassConfigurationFile = Path.Combine(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()).ToString(), @"Mappings\SeatClass.hbm.xml");
                var seatConfigurationFile = Path.Combine(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()).ToString(), @"Mappings\Seat.hbm.xml");
                var seatVMConfigurationFile = Path.Combine(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()).ToString(), @"Mappings\SeatVM.hbm.xml");
                var ticketConfigurationFile = Path.Combine(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()).ToString(), @"Mappings\Ticket.hbm.xml");
                var ticketVMConfigurationFile = Path.Combine(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()).ToString(), @"Mappings\TicketVM.hbm.xml");
                var ticketCreatorConfigurationFile = Path.Combine(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()).ToString(), @"Mappings\TicketCreator.hbm.xml");
                configuration.AddFile(pilotConfigurationFile);
                configuration.AddFile(planeConfigurationFile);
                configuration.AddFile(countryConfigurationFile);
                configuration.AddFile(airportConfigurationFile);
                configuration.AddFile(routeConfigurationFile);
                configuration.AddFile(airportVMConfigurationFile);
                configuration.AddFile(routeVMConfigurationFile);
                configuration.AddFile(flightConfigurationFile);
                configuration.AddFile(flightVMConfigurationFile);
                configuration.AddFile(storeConfigurationFile);
                configuration.AddFile(storeVMConfigurationFile);
                configuration.AddFile(passengerConfigurationFile);
                configuration.AddFile(passengerVMConfigurationFile);
                configuration.AddFile(seatClassConfigurationFile);
                configuration.AddFile(seatConfigurationFile);
                configuration.AddFile(seatVMConfigurationFile);
                configuration.AddFile(ticketConfigurationFile);
                configuration.AddFile(ticketVMConfigurationFile);
                configuration.AddFile(ticketCreatorConfigurationFile);
                configuration.AddFile(flightWithTicketsConfigurationFile);
                ISessionFactory sessionFactory = configuration.BuildSessionFactory();
                return sessionFactory.OpenSession();
            }

        }
    }
}