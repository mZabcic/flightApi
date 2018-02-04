using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FlightControlWinForms
{
    public partial class FormMainWindow : Form
    {
        private IMainController _controller;

        public FormMainWindow(IMainController inController)
        {
            _controller = inController;
            InitializeComponent();
        }

        private void passengersToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void addFlightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controller.ViewFlights();
        }

        private void editFlightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controller.EditFlights();
        }

        private void removeFlightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controller.RemoveFlights();
        }

        private void addFlightToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            _controller.AddFlights();
        }

        private void viewStoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controller.ViewStores();
        }

        private void addStoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controller.AddStores();
        }

        private void removeStoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controller.RemoveStores();
        }

        private void editStoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controller.EditStores();
        }

        private void viewAirportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controller.ViewAirports();
        }

        private void viewPilotsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controller.ViewPilots();
        }

        private void addPilotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controller.AddPilots();
        }

        private void removePilotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controller.RemovePilots();
        }

        private void editPilotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controller.EditPilots();
        }

        private void viewRoutesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controller.ViewRoutes();
        }

        private void viewCountriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controller.ViewCountries();
        }

       

        private void viewPlanesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controller.ViewPlanes();
        }

        private void addPlaneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controller.AddPlanes();
        }

        private void removePlaneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controller.RemovePlanes();
        }

        private void editPlaneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controller.EditPlanes();
        }

        private void addAirportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controller.AddAirports();
        }

        private void removeAirportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controller.RemoveAirports();
        }

        private void editAirportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controller.EditAirports();
        }

        private void addRouteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controller.AddRoutes();
        }

        private void removeRouteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controller.RemoveRoutes();
        }

        private void editRouteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controller.EditRoutes();
        }

        private void viewPassengersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controller.ViewPassengers();
        }

        private void addPassengerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controller.AddPassengers();
        }

        private void removePassegerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controller.RemovePassengers();
        }

        private void editPassengerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controller.EditPassengers();
        }

        private void viewEmployeesToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void viewSeatsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controller.ViewSeats();
        }

        private void addSeatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controller.AddSeats();
        }

        private void removeSeatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controller.RemoveSeats();
        }

        private void editSeatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controller.EditSeats();
        }

        private void viewSeatClassesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controller.ViewSeatClasses();
        }

        private void viewTicketsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controller.ViewTickets();
        }

        private void addTicketToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controller.AddTickets();
        }

        private void removeTicketToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controller.RemoveTickets();
        }

        private void editTicketToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controller.EditTickets();
        }

        private void FormMainWindow_Load(object sender, EventArgs e)
        {

        }
    }
}
