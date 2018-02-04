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
    public partial class FormAddRoute : Form
    {
        public FormAddRoute()
        {
            InitializeComponent();

            //airport from
            Airport[] items = Program.MyConnection.Airport.GetAll().ToArray();

            string[] tmpSC = new string[8];
            ListViewItem lvi;

            foreach (Airport p in items)
            {
                tmpSC[0] = p.Id.ToString();
                tmpSC[1] = p.Name.ToString();

                lvi = new ListViewItem(tmpSC);

                comboBox1.Items.Add(tmpSC[0] + ' ' + tmpSC[1]);
                comboBox3.Items.Add(tmpSC[0] + ' ' + tmpSC[1]);
            }

           


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Route r = new Route();
            r.FromId = Convert.ToInt32(comboBox1.SelectedItem.ToString().Split(' ')[0]);
            r.DestinationId = Convert.ToInt32(comboBox3.SelectedItem.ToString().Split(' ')[0]);

            Program.MyConnection.Route.Insert(r);
            MessageBox.Show("Item added.");
            Close();
        }
    }
}
