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
    public partial class FormEditCountry : Form
    {
        public FormEditCountry(Country[] countries)
        {
            InitializeComponent();
            listView1.FullRowSelect = true;
            listView1.MultiSelect = false;

            FlightControlApi.Models.Country[] items = countries; //Program.MyConnection.Country.GetAll().ToArray();

            string[] tmpCountry = new string[6];
            ListViewItem lvi;

            foreach (FlightControlApi.Models.Country p in items)
            {
                tmpCountry[0] = p.Id.ToString();
                tmpCountry[1] = p.iso;
                tmpCountry[2] = p.name;
                tmpCountry[3] = p.iso3;
                tmpCountry[4] = p.printable_name;
                tmpCountry[5] = p.numcode.ToString();


                lvi = new ListViewItem(tmpCountry);

                listView1.Items.Add(lvi);

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
