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
    public partial class FormEditSeatClass : Form
    {
        public FormEditSeatClass()
        {
            InitializeComponent();
            listView1.FullRowSelect = true;
            listView1.MultiSelect = false;

            FlightControlApi.Models.SeatClass[] items = Program.MyConnection.SeatClass.GetAll().ToArray();

            string[] tmpSC = new string[8];
            ListViewItem lvi;

            foreach (FlightControlApi.Models.SeatClass p in items)
            {
                tmpSC[0] = p.Id.ToString();
                tmpSC[1] = p.Name.ToString();
                tmpSC[2] = p.PriceMultipler.ToString();



                lvi = new ListViewItem(tmpSC);

                listView1.Items.Add(lvi);
            }


          
        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
