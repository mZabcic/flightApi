using NHibernate;
using NHibernate.Cfg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlightControlApi
{
    public class NHibernateSession
    {
        public static ISession OpenSession()
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
        }
    }
}