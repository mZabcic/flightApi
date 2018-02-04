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
    public partial class FormAddSeat : Form
    {

        string SelectedSC, selectedPl;

        public FormAddSeat()
        {
            InitializeComponent();

            //seatclass
            FlightControlApi.Models.SeatClass[] items = Program.MyConnection.SeatClass.GetAll().ToArray();

            string[] tmpSC = new string[8];
            ListViewItem lvi;

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

                comboBox1.Items.Add(tmpPlane[0] +' '+ tmpPlane[1]);
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SelectedSC = comboBox3.SelectedItem.ToString().Split(' ')[0];
            selectedPl = comboBox1.SelectedItem.ToString().Split(' ')[0];
            Seat s = new Seat();
            s.Num = Convert.ToInt32(numericUpDown4.Value);
            s.PlaneId = Convert.ToInt32(selectedPl);
            s.SeatClassId = Convert.ToInt32(SelectedSC);

            Program.MyConnection.Seat.Insert(s);
            MessageBox.Show("Item added.");
            Close();
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
