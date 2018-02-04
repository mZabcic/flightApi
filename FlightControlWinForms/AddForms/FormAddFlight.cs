using FlightControlApi.Models;
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
    public partial class FormAddFlight : Form
    {
        public FormAddFlight()
        {
            InitializeComponent();

            //pilots
            FlightControlApi.Models.Pilot[] pilots = Program.MyConnection.Pilot.GetAll().ToArray();

            string[] tmpPilot = new string[5];
            ListViewItem lvi;

            foreach (FlightControlApi.Models.Pilot p in pilots)
            {
                tmpPilot[0] = p.Id.ToString();
                tmpPilot[1] = p.FirstName;
                tmpPilot[2] = p.LastName;
                tmpPilot[3] = p.BirthDay.ToString();
                tmpPilot[4] = p.Active.ToString();


                comboBox2.Items.Add(tmpPilot[0] + ' ' + tmpPilot[1] + ' ' + tmpPilot[2]);

            }

            //routes
            FlightControlApi.Models.Route[] routes = Program.MyConnection.Route.GetAll().ToArray();

            string[] tmpRoute = new string[8];

            foreach (FlightControlApi.Models.Route p in routes)
            {
                tmpRoute[0] = p.Id.ToString();
                tmpRoute[1] = p.FromId.ToString();
                tmpRoute[2] = p.DestinationId.ToString();



                cbxSensor.Items.Add(tmpRoute[0] + ' ' + tmpRoute[1] + ' '+ tmpRoute[2]);
            }

            //planes
            FlightControlApi.Models.Plane[] planes = Program.MyConnection.Plane.GetAll().ToArray();

            string[] tmpPlane = new string[8];

            foreach (FlightControlApi.Models.Plane p in planes)
            {
                tmpPlane[0] = p.Id.ToString();
                tmpPlane[1] = p.Model;
                tmpPlane[2] = p.SerialNumber;
                tmpPlane[3] = p.EconomyCapacity.ToString();
                tmpPlane[4] = p.BusinessCapacity.ToString();
                tmpPlane[5] = p.FirstClassCapacity.ToString();
                tmpPlane[6] = p.Active.ToString();


                comboBox1.Items.Add(tmpPlane[0] +' '+ tmpPlane[1]);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //todo posalji flight
            Flight f = new Flight();
            f.DepTime = dateTimePicker1.Value;
            f.ArrTime = dateTimePicker2.Value;
            f.PilotId = Convert.ToInt32(comboBox2.SelectedItem.ToString().Split(' ')[0]);
            f.PlaneId = Convert.ToInt32(comboBox1.SelectedItem.ToString().Split(' ')[0]);
            f.RouteId = Convert.ToInt32(cbxSensor.SelectedItem.ToString().Split(' ')[0]);
            f.Price =  Convert.ToInt32(numericUpDown1.Value);

            Program.MyConnection.Flight.Insert(f);
            MessageBox.Show("Item created");
            Close();
        }

        private void cbxSensor_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
