using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlightControlWinForms
{
    public interface IMainController
    {
        void ViewAirports();
        void AddAirports();
        void RemoveAirports();
        void EditAirports();

        void ViewCountries();
        void ViewSeatClasses();

        void ViewFlights();
        void AddFlights();
        void RemoveFlights();
        void EditFlights();

        void ViewPassengers();
        void AddPassengers();
        void RemovePassengers();
        void EditPassengers();  

        void ViewPilots();
        void AddPilots();
        void RemovePilots();
        void EditPilots();

        void ViewPlanes();
        void AddPlanes();
        void RemovePlanes();
        void EditPlanes();

        void ViewRoutes();
        void AddRoutes();
        void RemoveRoutes();
        void EditRoutes();

        void ViewSeats();
        void AddSeats();
        void RemoveSeats();
        void EditSeats();

        void ViewStores();
        void AddStores();
        void RemoveStores();
        void EditStores();

       

        void ViewTickets();
        void AddTickets();
        void RemoveTickets();
        void EditTickets();

    }
}
