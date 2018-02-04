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
    public partial class FormEditRoute : Form
    {
        public FormEditRoute()
        {
            InitializeComponent();
            listView1.FullRowSelect = true;
            listView1.MultiSelect = false;

            FlightControlApi.Models.Route[] pilots = Program.MyConnection.Route.GetAll().ToArray();

            string[] tmpRoute = new string[8];
            ListViewItem lvi;

            foreach (FlightControlApi.Models.Route p in pilots)
            {
                tmpRoute[0] = p.Id.ToString();
                tmpRoute[1] = Program.MyConnection.Airport.Get(Convert.ToInt16(p.FromId)).Name; //p.FromId.ToString();
                tmpRoute[2] = Program.MyConnection.Airport.Get(Convert.ToInt16(p.DestinationId)).Name; //p.DestinationId.ToString();


                lvi = new ListViewItem(tmpRoute);

                listView1.Items.Add(lvi);
                //comboBox1.Items.Add(p.FromId.ToString());
                //comboBox3.Items.Add(p.DestinationId.ToString());
            }

            //airport from
            Airport[] items = Program.MyConnection.Airport.GetAll().ToArray();

            string[] tmpSC = new string[8];
            //ListViewItem lvi;

            foreach (Airport p in items)
            {
                tmpSC[0] = p.Id.ToString();
                tmpSC[1] = p.Name.ToString();

                lvi = new ListViewItem(tmpSC);

                comboBox1.Items.Add(tmpSC[0] + ' ' + tmpSC[1]);
                comboBox3.Items.Add(tmpSC[0] + ' ' + tmpSC[1]);
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
                Program.MyConnection.Route.Delete(Convert.ToInt16(listView1.SelectedItems[0].Text));

                MessageBox.Show("Item removed.");

                listView1.SelectedItems[0].Remove();
            }
        }

        private void FormEditRoute_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Route r = new Route();
            r.Id = Convert.ToInt16(listView1.SelectedItems[0].Text);
            r.FromId = Convert.ToInt16(comboBox1.SelectedItem.ToString().Split(' ')[0]);
            r.DestinationId = Convert.ToInt16(comboBox3.SelectedItem.ToString().Split(' ')[0]);

            Program.MyConnection.Route.Update(Convert.ToInt32(r.Id), r);

            MessageBox.Show("Item updated");
            Reload();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                Route p = Program.MyConnection.Route.Get(Convert.ToInt16(listView1.SelectedItems[0].Text));
                comboBox1.SelectedItem = p.FromId.ToString() + ' ' + Program.MyConnection.Airport.Get(Convert.ToInt16(p.FromId)).Name;
                comboBox3.SelectedItem = p.DestinationId.ToString() + ' ' + Program.MyConnection.Airport.Get(Convert.ToInt16(p.DestinationId)).Name;
            }
        }

        private void Reload()
        {
            FlightControlApi.Models.Route[] pilots = Program.MyConnection.Route.GetAll().ToArray();

            string[] tmpRoute = new string[8];
            ListViewItem lvi;
            listView1.Items.Clear();

            foreach (FlightControlApi.Models.Route p in pilots)
            {
                tmpRoute[0] = p.Id.ToString();
                tmpRoute[1] = Program.MyConnection.Airport.Get(Convert.ToInt16(p.FromId)).Name; //p.FromId.ToString();
                tmpRoute[2] = Program.MyConnection.Airport.Get(Convert.ToInt16(p.DestinationId)).Name; //p.DestinationId.ToString();


                lvi = new ListViewItem(tmpRoute);

                listView1.Items.Add(lvi);
            }
        }
    }
}