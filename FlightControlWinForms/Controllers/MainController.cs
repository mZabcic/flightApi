using FlightControlApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;



namespace FlightControlWinForms
{
    public class MainController : IMainController
    {

        Country[] countries = Program.MyConnection.Country.GetAll().ToArray();
      

        public void AddAirports()
        {
            FormAddAirport faa = new FormAddAirport(countries);
            faa.ShowDialog();
        }

       

        public void AddFlights()
        {
            FormAddFlight faf = new FormAddFlight();
            faf.ShowDialog();
        }

        public void AddPassengers()
        {
            FormAddPassenger fap = new FormAddPassenger(countries);
            fap.ShowDialog();
        }

        public void AddPilots()
        {
            FormAddPilot fap = new FormAddPilot();
            fap.ShowDialog();
        }

        public void AddPlanes()
        {
            FormAddPlane fap = new FormAddPlane();
            fap.ShowDialog();
        }

        public void AddRoutes()
        {
            FormAddRoute far = new FormAddRoute();
            far.ShowDialog();
        }

        public void AddSeats()
        {
            FormAddSeat fas = new FormAddSeat();
            fas.ShowDialog();
        }

        public void AddStores()
        {
            FormAddStore fas = new FormAddStore(countries);
            fas.ShowDialog();
        }

       

        public void AddTickets()
        {
            FormAddTicket fat = new FormAddTicket();
            fat.ShowDialog();
        }

        public void EditAirports()
        {
            FormEditAirport fea = new FormEditAirport(countries);
            fea.ShowDialog();
        }

        public void EditCountries()
        {
            FormEditCountry fec = new FormEditCountry(countries);
            fec.ShowDialog();
        }

        public void EditFlights()
        {
            FormEditFlights fef = new FormEditFlights();
            fef.ShowDialog();
        }

        public void EditPassengers()
        {
            FormEditPassenger fep = new FormEditPassenger(countries);
            fep.ShowDialog();
        }

        public void EditPilots()
        {
            FormEditPilot fep = new FormEditPilot();
            fep.ShowDialog();
        }

        public void EditPlanes()
        {
            FormEditPlane fep = new FormEditPlane();
            fep.ShowDialog();
        }

        public void EditRoutes()
        {
            FormEditRoute fer = new FormEditRoute();
            fer.ShowDialog();
        }

        public void EditSeats()
        {
            FormEditSeat fes = new FormEditSeat();
            fes.ShowDialog();
        }

        public void EditStores()
        {
            FormEditStore fes = new FormEditStore(countries);
            fes.ShowDialog();
        }

       

        public void EditTickets()
        {
            FormEditTicket fet = new FormEditTicket();
            fet.ShowDialog();
           
        }

        public void RemoveAirports()
        {
            FormEditAirport fea = new FormEditAirport(countries);
            fea.ShowDialog();
        }

        public void RemoveCountries()
        {
            FormEditCountry fec = new FormEditCountry(countries);
            fec.ShowDialog();
        }

        public void RemoveFlights()
        {
            FormEditFlights fef = new FormEditFlights();
            fef.ShowDialog();
        }

        public void RemovePassengers()
        {
            FormEditPassenger fep = new FormEditPassenger(countries);
            fep.ShowDialog();
        }

        public void RemovePilots()
        {
            FormEditPilot fep = new FormEditPilot();
            fep.ShowDialog();
        }

        public void RemovePlanes()
        {
            FormEditPlane fep = new FormEditPlane();
            fep.ShowDialog();
        }

        public void RemoveRoutes()
        {
            FormEditRoute fer = new FormEditRoute();
            fer.ShowDialog();
        }

        public void RemoveSeats()
        {
            FormEditSeat fes = new FormEditSeat();
            fes.ShowDialog();
        }

        public void RemoveStores()
        {
            FormEditStore fes = new FormEditStore(countries);
            fes.ShowDialog();
        }

      

        public void RemoveTickets()
        {
            FormEditTicket fet = new FormEditTicket();
            fet.ShowDialog();
        }

        public void ViewAirports()
        {
            FormEditAirport fea = new FormEditAirport(countries);
            fea.ShowDialog();
        }

        public void ViewCountries()
        {
            FormEditCountry fec = new FormEditCountry(countries);
            fec.ShowDialog();
        }

        public void ViewFlights()
        {
            FormEditFlights fef = new FormEditFlights();
            fef.ShowDialog();
        }

        public void ViewPassengers()
        {
            FormEditPassenger fep = new FormEditPassenger(countries);
            fep.ShowDialog();
        }

        public void ViewPilots()
        {
            FormEditPilot fep = new FormEditPilot();
            fep.ShowDialog();
        }

        public void ViewPlanes()
        {
            FormEditPlane fep = new FormEditPlane();
            fep.ShowDialog();
        }

        public void ViewRoutes()
        {
            FormEditRoute fer = new FormEditRoute();
            fer.ShowDialog();

        }

        public void ViewSeatClasses()
        {
            FormEditSeatClass fvsc = new FormEditSeatClass();
            fvsc.ShowDialog();
        }

        public void ViewSeats()
        {
            FormEditSeat fes = new FormEditSeat();
            fes.ShowDialog();
        }

        public void ViewStores()
        {
            FormEditStore fes = new FormEditStore(countries);
            fes.ShowDialog();
        }

      

        public void ViewTickets()
        {
            FormEditTicket fet = new FormEditTicket();
            fet.ShowDialog();

        }
    }
}
