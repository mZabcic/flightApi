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
    public partial class FormEditPlane : Form
    {
        public FormEditPlane()
        {
            InitializeComponent();
            listView1.FullRowSelect = true;
            listView1.MultiSelect = false;

            FlightControlApi.Models.Plane[] planes = Program.MyConnection.Plane.GetAll().ToArray();

            string[] tmpPlane = new string[8];
            ListViewItem lvi;

            foreach (FlightControlApi.Models.Plane p in planes)
            {
                tmpPlane[0] = p.Id.ToString();
                tmpPlane[1] = p.Model;
                tmpPlane[2] = p.SerialNumber;
                tmpPlane[3] = p.EconomyCapacity.ToString();
                tmpPlane[4] = p.BusinessCapacity.ToString();
                tmpPlane[5] = p.FirstClassCapacity.ToString();
                tmpPlane[6] = p.Active.ToString();

                lvi = new ListViewItem(tmpPlane);

                listView1.Items.Add(lvi);
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
                Plane p = new Plane();
                p.Id = Convert.ToInt16(listView1.SelectedItems[0].Text);
                p.Model = textBox3.Text;
                p.SerialNumber = textBox1.Text;
                p.EconomyCapacity = Convert.ToInt16(numericUpDown4.Value);
                p.BusinessCapacity = Convert.ToInt16(numericUpDown3.Value);
                p.FirstClassCapacity = Convert.ToInt16(numericUpDown2.Value);
                p.Active = 1;

                Program.MyConnection.Plane.Update(Convert.ToInt16(p.Id), p);

                MessageBox.Show("Item edited");
                Reload();
            }

        }

        private void Reload()
        {
            FlightControlApi.Models.Plane[] planes = Program.MyConnection.Plane.GetAll().ToArray();

            string[] tmpPlane = new string[8];
            ListViewItem lvi;
            listView1.Items.Clear();

            foreach (FlightControlApi.Models.Plane p in planes)
            {
                tmpPlane[0] = p.Id.ToString();
                tmpPlane[1] = p.Model;
                tmpPlane[2] = p.SerialNumber;
                tmpPlane[3] = p.EconomyCapacity.ToString();
                tmpPlane[4] = p.BusinessCapacity.ToString();
                tmpPlane[5] = p.FirstClassCapacity.ToString();
                tmpPlane[6] = p.Active.ToString();

                lvi = new ListViewItem(tmpPlane);

                listView1.Items.Add(lvi);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                Plane p = Program.MyConnection.Plane.Get(Convert.ToInt16(listView1.SelectedItems[0].Text));
                textBox3.Text = p.Model;
                textBox1.Text = p.SerialNumber;
                numericUpDown4.Value = p.EconomyCapacity;
                numericUpDown3.Value = p.BusinessCapacity;
                numericUpDown2.Value = p.FirstClassCapacity;

            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.MyConnection.Plane.Delete(Convert.ToInt16(listView1.SelectedItems[0].Text));

            MessageBox.Show("Item deactivated.");

            Reload();
        }
    }
}
