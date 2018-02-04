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
    public partial class FormEditPilot : Form
    {
        public FormEditPilot()
        {

            InitializeComponent();
            listView1.FullRowSelect = true;
            listView1.MultiSelect = false;

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

                lvi = new ListViewItem(tmpPilot);

                listView1.Items.Add(lvi);

            }

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                Pilot p = Program.MyConnection.Pilot.Get(Convert.ToInt16(listView1.SelectedItems[0].Text));
                textBox3.Text = p.FirstName;
                textBox1.Text = p.LastName;
                dateTimePicker2.Value = Convert.ToDateTime(p.BirthDay);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                Pilot p = new Pilot();
                p.Id = Convert.ToInt16(listView1.SelectedItems[0].Text);
                p.FirstName = textBox3.Text;
                p.LastName = textBox1.Text;
                p.BirthDay = dateTimePicker2.Value;

                Program.MyConnection.Pilot.Update(Convert.ToInt32(p.Id), p);

                MessageBox.Show("Item edited");
                Reload();
            }
        }

        private void Reload()
        {
            FlightControlApi.Models.Pilot[] pilots = Program.MyConnection.Pilot.GetAll().ToArray();

            string[] tmpPilot = new string[5];
            ListViewItem lvi;
            listView1.Items.Clear();

            foreach (FlightControlApi.Models.Pilot p in pilots)
            {
                tmpPilot[0] = p.Id.ToString();
                tmpPilot[1] = p.FirstName;
                tmpPilot[2] = p.LastName;
                tmpPilot[3] = p.BirthDay.ToString();
                tmpPilot[4] = p.Active.ToString();

                lvi = new ListViewItem(tmpPilot);

                listView1.Items.Add(lvi);

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.MyConnection.Pilot.Delete(Convert.ToInt16(listView1.SelectedItems[0].Text));

            MessageBox.Show("Item deactivated.");

            Reload();
        }
    }
}