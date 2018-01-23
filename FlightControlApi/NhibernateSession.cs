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
            configuration.AddFile(pilotConfigurationFile);
            configuration.AddFile(planeConfigurationFile);
            configuration.AddFile(countryConfigurationFile);
            configuration.AddFile(airportConfigurationFile);
            ISessionFactory sessionFactory = configuration.BuildSessionFactory();
            return sessionFactory.OpenSession();
        }
    }
}