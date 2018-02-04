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
    public partial class FormEditTicket : Form
    {
        public FormEditTicket()
        {
            InitializeComponent();
            listView1.FullRowSelect = true;
            listView1.MultiSelect = false;

            FlightControlApi.Models.Ticket[] planes = Program.MyConnection.Ticket.GetAll().ToArray();

            string[] tmpPlane = new string[8];
            ListViewItem lvi;

            foreach (FlightControlApi.Models.Ticket p in planes)
            {
                tmpPlane[0] = p.Id.ToString();
                tmpPlane[1] = p.Price.ToString();
                tmpPlane[2] = Program.MyConnection.Passenger.Get(Convert.ToInt16(p.PassengerId)).Name; //p.PassengerId.ToString();
                tmpPlane[3] = Program.MyConnection.Seat.Get(Convert.ToInt16(p.SeatId)).Num.ToString(); //p.SeatId.ToString();
                tmpPlane[4] = Program.MyConnection.Store.Get(Convert.ToInt16(p.StoreId)).Name; //p.StoreId.ToString();
                tmpPlane[5] = p.FlightId.ToString();
                tmpPlane[6] = p.Revoked.ToString();

                lvi = new ListViewItem(tmpPlane);

                listView1.Items.Add(lvi);
                //comboBox2.Items.Add(p.PassengerId.ToString());
                //comboBox1.Items.Add(p.SeatId.ToString());
                //comboBox3.Items.Add(p.StoreId.ToString());
                //comboBox4.Items.Add(tmpPlane[5]);
            }

            //passengers
            Passenger[] pilots = Program.MyConnection.Passenger.GetAll().ToArray();

            string[] tmpPassenger = new string[5];
            //ListViewItem lvi;

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

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                Ticket t = Program.MyConnection.Ticket.Get(Convert.ToInt16(listView1.SelectedItems[0].Text));
                numericUpDown1.Value = t.Price;
                comboBox1.SelectedItem = t.SeatId.ToString() + ' ' + Program.MyConnection.Seat.Get(Convert.ToInt16(t.SeatId)).Num;
                comboBox2.SelectedItem = t.PassengerId.ToString() + ' ' + Program.MyConnection.Passenger.Get(Convert.ToInt16(t.PassengerId)).Name;
                comboBox3.SelectedItem = t.StoreId.ToString() + ' ' + Program.MyConnection.Store.Get(Convert.ToInt16(t.StoreId)).Name;
                comboBox4.SelectedItem = t.FlightId.ToString() + ' ' + Program.MyConnection.Flight.Get(Convert.ToInt16(t.FlightId)).RouteId;

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Ticket t = new Ticket();
            t.Id = Convert.ToInt16(listView1.SelectedItems[0].Text);
            t.Price = numericUpDown1.Value;
            t.PassengerId = Convert.ToInt32(comboBox2.SelectedItem.ToString().Split(' ')[0]);
            t.SeatId = Convert.ToInt32(comboBox1.SelectedItem.ToString().Split(' ')[0]);
            t.StoreId = Convert.ToInt32(comboBox3.SelectedItem.ToString().Split(' ')[0]);
            t.FlightId = Convert.ToInt32(comboBox4.SelectedItem.ToString().Split(' ')[0]);

            Program.MyConnection.Ticket.Update(Convert.ToInt32(t.Id), t);

            MessageBox.Show("Item updated");
            Reload();
        }

        private void Reload()
        {
            FlightControlApi.Models.Ticket[] planes = Program.MyConnection.Ticket.GetAll().ToArray();

            string[] tmpPlane = new string[8];
            ListViewItem lvi;
            listView1.Items.Clear();

            foreach (FlightControlApi.Models.Ticket p in planes)
            {
                tmpPlane[0] = p.Id.ToString();
                tmpPlane[1] = p.Price.ToString();
                tmpPlane[2] = Program.MyConnection.Passenger.Get(Convert.ToInt16(p.PassengerId)).Name; //p.PassengerId.ToString();
                tmpPlane[3] = Program.MyConnection.Seat.Get(Convert.ToInt16(p.SeatId)).Num.ToString(); //p.SeatId.ToString();
                tmpPlane[4] = Program.MyConnection.Store.Get(Convert.ToInt16(p.StoreId)).Name; //p.StoreId.ToString();
                tmpPlane[5] = p.FlightId.ToString();
                tmpPlane[6] = p.Revoked.ToString();

                lvi = new ListViewItem(tmpPlane);

                listView1.Items.Add(lvi);
               
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.MyConnection.Ticket.Delete(Convert.ToInt16(listView1.SelectedItems[0].Text));

            MessageBox.Show("Item deactivated.");

            Reload();
        }
    }
}
