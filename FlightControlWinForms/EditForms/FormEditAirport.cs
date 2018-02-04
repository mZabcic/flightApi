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
    public partial class FormEditAirport : Form
    {
        public FormEditAirport(Country[] countries)
        {
            InitializeComponent();

            listView1.FullRowSelect = true;
            listView1.MultiSelect = false;

            FlightControlApi.Models.Airport[] pilots = Program.MyConnection.Airport.GetAll().ToArray();

            string[] tmpAirport = new string[5];
            ListViewItem lvi;

            foreach (FlightControlApi.Models.Airport p in pilots)
            {
                tmpAirport[0] = p.Id.ToString();
                tmpAirport[1] = p.Name;
                tmpAirport[2] = p.Address;
                tmpAirport[3] = p.ZipCode;
                tmpAirport[4] = Program.MyConnection.Country.Get(Convert.ToInt16(p.CountryId)).name; //p.CountryId.ToString();

                lvi = new ListViewItem(tmpAirport);

                listView1.Items.Add(lvi);
                //comboBox3.Items.Add(p.CountryId.ToString());
            }

            Country[] items = countries; //Program.MyConnection.Country.GetAll().ToArray();

            string[] tmpCountry = new string[6];

            foreach (Country p in items)
            {
                tmpCountry[0] = p.Id.ToString();
                tmpCountry[1] = p.iso;
                tmpCountry[2] = p.name;
                tmpCountry[3] = p.iso3;
                tmpCountry[4] = p.printable_name;
                tmpCountry[5] = p.numcode.ToString();


                comboBox3.Items.Add(tmpCountry[0] + ' ' + tmpCountry[2]);

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void button1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                Program.MyConnection.Airport.Delete(Convert.ToInt16(listView1.SelectedItems[0].Text));

                MessageBox.Show("Item removed.");

                listView1.SelectedItems[0].Remove();
            }

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                Airport p = Program.MyConnection.Airport.Get(Convert.ToInt16(listView1.SelectedItems[0].Text));
                textBox3.Text = p.Name;
                textBox1.Text = p.Address;
                textBox4.Text = p.ZipCode;
                comboBox3.SelectedItem = p.CountryId.ToString() + ' ' + Program.MyConnection.Country.Get(Convert.ToInt16(p.CountryId)).name;

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Airport a = new Airport();
            a.Id = Convert.ToInt16(listView1.SelectedItems[0].Text);
            a.Name = textBox3.Text;
            a.Address = textBox1.Text;
            a.ZipCode = textBox4.Text;
            a.CountryId = Convert.ToInt32(comboBox3.SelectedItem.ToString().Split(' ')[0]);

            Program.MyConnection.Airport.Update(Convert.ToInt32(a.Id), a);

            MessageBox.Show("Item updated");
            Reload();
        }

        private void Reload()
        {
            FlightControlApi.Models.Airport[] pilots = Program.MyConnection.Airport.GetAll().ToArray();

            string[] tmpAirport = new string[5];
            ListViewItem lvi;
            listView1.Items.Clear();

            foreach (FlightControlApi.Models.Airport p in pilots)
            {
                tmpAirport[0] = p.Id.ToString();
                tmpAirport[1] = p.Name;
                tmpAirport[2] = p.Address;
                tmpAirport[3] = p.ZipCode;
                tmpAirport[4] = Program.MyConnection.Country.Get(Convert.ToInt16(p.CountryId)).name; //p.CountryId.ToString();


                lvi = new ListViewItem(tmpAirport);

                listView1.Items.Add(lvi);
            }
        }

        private void FormEditAirport_Load(object sender, EventArgs e)
        {

        }
    }
}
