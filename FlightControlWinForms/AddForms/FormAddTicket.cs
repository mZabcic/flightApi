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
    public partial class FormAddTicket : Form
    {
        public FormAddTicket()
        {
            InitializeComponent();

            //passengers
            Passenger[] pilots = Program.MyConnection.Passenger.GetAll().ToArray();

            string[] tmpPassenger = new string[5];
            ListViewItem lvi;

            foreach (Passenger p in pilots)
            {
                tmpPassenger[0] = p.Id.ToString();
                tmpPassenger[1] = p.Name;

                lvi = new ListViewItem(tmpPassenger);

                comboBox2.Items.Add(tmpPassenger[0] + ' ' + tmpPassenger[1]);
            }

            //Seat
            Seat[] seats = Program.MyConnection.Seat.GetAll().ToArray();

            string[] tmpSeat = new string[8];

            foreach (Seat p in seats)
            {
                tmpSeat[0] = p.Id.ToString();
                tmpSeat[1] = p.Num.ToString();


                comboBox1.Items.Add(tmpSeat[0] + ' ' + tmpSeat[1]);
            }

            //Stores

            Store[] stores = Program.MyConnection.Store.GetAll().ToArray();

            string[] tmpStore = new string[8];

            foreach (Store p in stores)
            {
                tmpStore[0] = p.Id.ToString();
                tmpStore[1] = p.Name;


                comboBox3.Items.Add(tmpStore[0] + ' ' + tmpStore[1]);
            }

            //flights
            Flight[] items = Program.MyConnection.Flight.GetAll().ToArray();

            string[] tmpFlight = new string[8];

            foreach (Flight p in items)
            {
                tmpFlight[0] = p.Id.ToString();
                tmpFlight[1] = p.RouteId.ToString();
               
                
                comboBox4.Items.Add(tmpFlight[0] + ' ' + tmpFlight[1]);

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Ticket t = new Ticket();
            t.Price = numericUpDown1.Value;
            t.PassengerId = Convert.ToInt32(comboBox2.SelectedItem.ToString().Split(' ')[0]);
            t.SeatId = Convert.ToInt32(comboBox1.SelectedItem.ToString().Split(' ')[0]);
            t.StoreId = Convert.ToInt32(comboBox3.SelectedItem.ToString().Split(' ')[0]);
            t.FlightId = Convert.ToInt32(comboBox4.SelectedItem.ToString().Split(' ')[0]);
            t.Revoked = false;

            Program.MyConnection.Ticket.Insert(t);

            MessageBox.Show("Item added.");
            Close();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
