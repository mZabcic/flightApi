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
               
             
                var flightConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Mappings\Flight.hbm.xml");
                var flightWithTicketsConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Mappings\FlightWithTickets.hbm.xml");
               var storeConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Mappings\Store.hbm.xml");
                var passengerConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Mappings\Passenger.hbm.xml");
               var seatClassConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Mappings\SeatClass.hbm.xml");
                var seatConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Mappings\Seat.hbm.xml");
              var ticketConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Mappings\Ticket.hbm.xml");
          var ticketCreatorConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Mappings\TicketCreator.hbm.xml");
                configuration.AddFile(pilotConfigurationFile);
                configuration.AddFile(planeConfigurationFile);
                configuration.AddFile(countryConfigurationFile);
                configuration.AddFile(airportConfigurationFile);
                configuration.AddFile(routeConfigurationFile);
                configuration.AddFile(flightConfigurationFile);
                configuration.AddFile(storeConfigurationFile);
                configuration.AddFile(passengerConfigurationFile);
                configuration.AddFile(seatClassConfigurationFile);
                configuration.AddFile(seatConfigurationFile);
                configuration.AddFile(ticketConfigurationFile);
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
               
                
                var flightConfigurationFile = Path.Combine(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()).ToString(), @"Mappings\Flight.hbm.xml");
                var flightWithTicketsConfigurationFile = Path.Combine(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()).ToString(), @"Mappings\FlightWithTickets.hbm.xml");
                var storeConfigurationFile = Path.Combine(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()).ToString(), @"Mappings\Store.hbm.xml");
                var passengerConfigurationFile = Path.Combine(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()).ToString(), @"Mappings\Passenger.hbm.xml");
                var seatClassConfigurationFile = Path.Combine(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()).ToString(), @"Mappings\SeatClass.hbm.xml");
                var seatConfigurationFile = Path.Combine(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()).ToString(), @"Mappings\Seat.hbm.xml");
                var ticketConfigurationFile = Path.Combine(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()).ToString(), @"Mappings\Ticket.hbm.xml");
                var ticketCreatorConfigurationFile = Path.Combine(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()).ToString(), @"Mappings\TicketCreator.hbm.xml");
                configuration.AddFile(pilotConfigurationFile);
                configuration.AddFile(planeConfigurationFile);
                configuration.AddFile(countryConfigurationFile);
                configuration.AddFile(airportConfigurationFile);
                configuration.AddFile(routeConfigurationFile);
                configuration.AddFile(flightConfigurationFile);
                configuration.AddFile(storeConfigurationFile);
                configuration.AddFile(passengerConfigurationFile);
                configuration.AddFile(seatClassConfigurationFile);
                configuration.AddFile(seatConfigurationFile);
                configuration.AddFile(ticketConfigurationFile);
                configuration.AddFile(ticketCreatorConfigurationFile);
                configuration.AddFile(flightWithTicketsConfigurationFile); 
                ISessionFactory sessionFactory = configuration.BuildSessionFactory();
                return sessionFactory.OpenSession(); 
            }

        }
    }
}