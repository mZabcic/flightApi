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
    public partial class FormEditStore : Form
    {
        public FormEditStore(Country[] countries)
        {
            InitializeComponent();
            listView1.FullRowSelect = true;
            listView1.MultiSelect = false;

            FlightControlApi.Models.Store[] pilots = Program.MyConnection.Store.GetAll().ToArray();

            string[] tmpStore = new string[8];
            ListViewItem lvi;

            foreach (FlightControlApi.Models.Store p in pilots)
            {
                tmpStore[0] = p.Id.ToString();
                tmpStore[1] = p.Name;
                tmpStore[2] = p.Address;
                tmpStore[3] = p.ZipCode.ToString();
                tmpStore[4] = Program.MyConnection.Country.Get(Convert.ToInt16(p.CountryId)).name; //p.CountryId.ToString();

                lvi = new ListViewItem(tmpStore);

                listView1.Items.Add(lvi);
                //comboBox3.Items.Add(p.CountryId.ToString());
            }

            Country[] items = countries;//Program.MyConnection.Country.GetAll().ToArray();

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
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                Program.MyConnection.Store.Delete(Convert.ToInt16(listView1.SelectedItems[0].Text));

                MessageBox.Show("Item removed.");

                listView1.SelectedItems[0].Remove();
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                Store s = Program.MyConnection.Store.Get(Convert.ToInt16(listView1.SelectedItems[0].Text));
                textBox3.Text = s.Name;
                textBox1.Text = s.Address;
                textBox2.Text = s.ZipCode;
                comboBox3.SelectedItem = s.CountryId.ToString() + ' ' + Program.MyConnection.Country.Get(Convert.ToInt16(s.CountryId)).name;

            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Store s = new Store();
            s.Id = Convert.ToInt16(listView1.SelectedItems[0].Text);
            s.Name = textBox3.Text;
            s.Address = textBox1.Text;
            s.ZipCode = textBox2.Text;
            s.CountryId = Convert.ToInt16(comboBox3.SelectedItem.ToString().Split(' ')[0]);

            Program.MyConnection.Store.Update(Convert.ToInt32(s.Id), s);

            MessageBox.Show("Item updated");
            Reload();
        }

        private void Reload()
        {
            FlightControlApi.Models.Store[] pilots = Program.MyConnection.Store.GetAll().ToArray();

            string[] tmpStore = new string[8];
            ListViewItem lvi;
            listView1.Items.Clear();

            foreach (FlightControlApi.Models.Store p in pilots)
            {
                tmpStore[0] = p.Id.ToString();
                tmpStore[1] = p.Name;
                tmpStore[2] = p.Address;
                tmpStore[3] = p.ZipCode.ToString();
                tmpStore[4] = Program.MyConnection.Country.Get(Convert.ToInt16(p.CountryId)).name; //p.CountryId.ToString();


                lvi = new ListViewItem(tmpStore);

                listView1.Items.Add(lvi);
            }
        }

        private void FormEditStore_Load(object sender, EventArgs e)
        {

        }
    }
}
