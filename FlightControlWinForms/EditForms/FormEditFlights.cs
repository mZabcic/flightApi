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
    public partial class FormEditFlights : Form
    {
        public FormEditFlights()
        {
            InitializeComponent();
            listView1.FullRowSelect = true;
            listView1.MultiSelect = false;

            FlightControlApi.Models.Flight[] items = Program.MyConnection.Flight.GetAll().ToArray();

            string[] tmpFlight = new string[8];
            ListViewItem lvi;

            foreach (FlightControlApi.Models.Flight p in items)
            {

                tmpFlight[0] = p.Id.ToString();
                tmpFlight[1] = p.RouteId.ToString();
                tmpFlight[2] = Program.MyConnection.Plane.Get(Convert.ToInt32(p.PlaneId)).Model; //p.PlaneId.ToString();
                tmpFlight[3] = Program.MyConnection.Pilot.Get(Convert.ToInt32(p.PilotId)).LastName; //p.PilotId.ToString();
                tmpFlight[4] = p.DepTime.ToString();
                tmpFlight[5] = p.ArrTime.ToString();
                tmpFlight[6] = p.Canceled.ToString();
                tmpFlight[7] = p.Price.ToString();


                lvi = new ListViewItem(tmpFlight);

                listView1.Items.Add(lvi);
                //cbxSensor.Items.Add(tmpFlight[1]);
                //comboBox1.Items.Add(p.PlaneId.ToString());
                //comboBox2.Items.Add(p.PilotId.ToString());

            }
            //pilots
            FlightControlApi.Models.Pilot[] pilots = Program.MyConnection.Pilot.GetAll().ToArray();

            string[] tmpPilot = new string[5];
            //ListViewItem lvi;

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



                cbxSensor.Items.Add(tmpRoute[0] + ' ' + tmpRoute[1] + ' ' + tmpRoute[2]);
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


                comboBox1.Items.Add(tmpPlane[0] + ' ' + tmpPlane[1]);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                Flight f = Program.MyConnection.Flight.Get(Convert.ToInt16(listView1.SelectedItems[0].Text));
                dateTimePicker1.Value = f.DepTime;
                dateTimePicker2.Value = f.ArrTime;
                cbxSensor.SelectedItem = f.RouteId.ToString() + ' ' + Program.MyConnection.Route.Get(Convert.ToInt32(f.RouteId)).FromId + ' ' + Program.MyConnection.Route.Get(Convert.ToInt32(f.RouteId)).DestinationId;
                comboBox1.SelectedItem = f.PlaneId.ToString() + ' ' + Program.MyConnection.Plane.Get(Convert.ToInt32(f.PlaneId)).Model;
                comboBox2.SelectedItem = f.PilotId.ToString() + ' ' + Program.MyConnection.Pilot.Get(Convert.ToInt32(f.PilotId)).FirstName + ' ' + Program.MyConnection.Pilot.Get(Convert.ToInt32(f.PilotId)).LastName;
                numericUpDown1.Value = f.Price;

            }
        }

        private void FormEditFlights_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Flight f = new Flight();
            f.Id = Convert.ToInt16(listView1.SelectedItems[0].Text);
            f.DepTime = dateTimePicker1.Value;
            f.ArrTime = dateTimePicker2.Value;
            f.RouteId = Convert.ToInt16(cbxSensor.SelectedItem.ToString().Split(' ')[0]);
            f.PlaneId = Convert.ToInt16(comboBox1.SelectedItem.ToString().Split(' ')[0]);
            f.PilotId = Convert.ToInt16(comboBox2.SelectedItem.ToString().Split(' ')[0]);
            f.Price = numericUpDown1.Value;

            Program.MyConnection.Flight.Update(Convert.ToInt32(f.Id), f);

            MessageBox.Show("Item updated");

            Reload();
        }

        private void Reload()
        {
            FlightControlApi.Models.Flight[] items = Program.MyConnection.Flight.GetAll().ToArray();

            string[] tmpFlight = new string[8];
            ListViewItem lvi;

            listView1.Items.Clear();

            foreach (FlightControlApi.Models.Flight p in items)
            {
                tmpFlight[0] = p.Id.ToString();
                tmpFlight[1] = p.RouteId.ToString();
                tmpFlight[2] = Program.MyConnection.Plane.Get(Convert.ToInt32(p.PlaneId)).Model; //p.PlaneId.ToString();
                tmpFlight[3] = Program.MyConnection.Pilot.Get(Convert.ToInt32(p.PilotId)).LastName; //p.PilotId.ToString();
                tmpFlight[4] = p.DepTime.ToString();
                tmpFlight[5] = p.ArrTime.ToString();
                tmpFlight[6] = p.Canceled.ToString();
                tmpFlight[7] = p.Price.ToString();


                lvi = new ListViewItem(tmpFlight);

                listView1.Items.Add(lvi);

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.MyConnection.Flight.Delete(Convert.ToInt16(listView1.SelectedItems[0].Text));

            MessageBox.Show("Item deactivated.");

            Reload();
        }
    }
}
