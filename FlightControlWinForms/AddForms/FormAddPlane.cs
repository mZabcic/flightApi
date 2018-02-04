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
    public partial class FormAddPlane : Form
    {
        public FormAddPlane()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Plane p = new Plane();
            p.Model = textBox3.Text;
            p.SerialNumber = textBox1.Text;
            p.EconomyCapacity = Convert.ToInt32(numericUpDown4.Value);
            p.BusinessCapacity = Convert.ToInt32(numericUpDown3.Value);
            p.FirstClassCapacity = Convert.ToInt32(numericUpDown2.Value);

            Program.MyConnection.Plane.Insert(p);
            MessageBox.Show("Item added.");
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
