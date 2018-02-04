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
    public partial class FormEditSeat : Form
    {
        public FormEditSeat()
        {
            InitializeComponent();
            listView1.FullRowSelect = true;
            listView1.MultiSelect = false;

            FlightControlApi.Models.Seat[] pilots = Program.MyConnection.Seat.GetAll().ToArray();

            string[] tmpSeat = new string[8];
            ListViewItem lvi;

            foreach (FlightControlApi.Models.Seat p in pilots)
            {
                tmpSeat[0] = p.Id.ToString();
                tmpSeat[1] = p.Num.ToString();
                tmpSeat[2] = Program.MyConnection.Plane.Get(Convert.ToInt16(p.PlaneId)).Model; //p.PlaneId.ToString();
                tmpSeat[3] = Program.MyConnection.SeatClass.Get(Convert.ToInt16(p.SeatClassId)).Name; //p.SeatClassId.ToString();

                lvi = new ListViewItem(tmpSeat);

                listView1.Items.Add(lvi);
                //comboBox1.Items.Add(p.PlaneId.ToString());
                //comboBox3.Items.Add(p.SeatClassId.ToString());

            }

            //seatclass
            FlightControlApi.Models.SeatClass[] items = Program.MyConnection.SeatClass.GetAll().ToArray();

            string[] tmpSC = new string[8];
            //ListViewItem lvi;

            foreach (FlightControlApi.Models.SeatClass p in items)
            {
                tmpSC[0] = p.Id.ToString();
                tmpSC[1] = p.Name.ToString();
                tmpSC[2] = p.PriceMultipler.ToString();

                lvi = new ListViewItem(tmpSC);

                comboBox3.Items.Add(tmpSC[0] + ' ' + tmpSC[1]);
            }

            //plane 
            Plane[] planes = Program.MyConnection.Plane.GetAll().ToArray();

            string[] tmpPlane = new string[8];

            foreach (Plane p in planes)
            {
                tmpPlane[0] = p.Id.ToString();
                tmpPlane[1] = p.Model;
                tmpPlane[2] = p.SerialNumber;
                tmpPlane[3] = p.EconomyCapacity.ToString();
                tmpPlane[4] = p.BusinessCapacity.ToString();
                tmpPlane[5] = p.FirstClassCapacity.ToString();
                tmpPlane[6] = p.Active.ToString();

                lvi = new ListViewItem(tmpPlane);

                comboBox1.Items.Add(tmpPlane[0] + ' ' + tmpPlane[1]);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                Program.MyConnection.Seat.Delete(Convert.ToInt16(listView1.SelectedItems[0].Text));

                MessageBox.Show("Item removed.");

                listView1.SelectedItems[0].Remove();
            }
        }

        private void FormEditSeat_Load(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                Seat s = Program.MyConnection.Seat.Get(Convert.ToInt16(listView1.SelectedItems[0].Text));
                numericUpDown4.Value = s.Num;
                comboBox3.SelectedItem = s.SeatClassId.ToString() + ' ' + Program.MyConnection.SeatClass.Get(Convert.ToInt16(s.SeatClassId)).Name;
                comboBox1.SelectedItem = s.PlaneId.ToString() + ' ' + Program.MyConnection.Plane.Get(Convert.ToInt16(s.PlaneId)).Model;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Seat s = new Seat();
            s.Id = Convert.ToInt16(listView1.SelectedItems[0].Text);
            s.Num = Convert.ToInt16(numericUpDown4.Value);
            s.PlaneId = Convert.ToInt32(comboBox1.SelectedItem.ToString().Split(' ')[0]);
            s.SeatClassId = Convert.ToInt32(comboBox3.SelectedItem.ToString().Split(' ')[0]);

            Program.MyConnection.Seat.Update(Convert.ToInt32(s.Id), s);

            MessageBox.Show("Item updated");
            Reload();
        }

        private void Reload()
        {
            FlightControlApi.Models.Seat[] pilots = Program.MyConnection.Seat.GetAll().ToArray();

            string[] tmpSeat = new string[8];
            ListViewItem lvi;
            listView1.Items.Clear();

            foreach (FlightControlApi.Models.Seat p in pilots)
            {
                tmpSeat[0] = p.Id.ToString();
                tmpSeat[1] = p.Num.ToString();
                tmpSeat[2] = Program.MyConnection.Plane.Get(Convert.ToInt16(p.PlaneId)).Model; //p.PlaneId.ToString();
                tmpSeat[3] = Program.MyConnection.SeatClass.Get(Convert.ToInt16(p.SeatClassId)).Name; //p.SeatClassId.ToString();


                lvi = new ListViewItem(tmpSeat);

                listView1.Items.Add(lvi);
            }
        }
    }
}
