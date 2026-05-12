using MyFirstWindowsForm.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyFirstWindowsForm
{


    public partial class Form1 : Form
    {
        short ID = 0;
        
        public Form1()
        {
            InitializeComponent();
            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFirstName.Text) || string.IsNullOrEmpty(txtLastName.Text))
                return;
            ID++;
            ListViewItem item = new ListViewItem(ID.ToString());
            if (rbMale.Checked)
            {
                item.ImageIndex = 0;
            }
            else
            {
                item.ImageIndex = 1;
            }

            item.SubItems.Add(txtFirstName.Text.Trim());
            item.SubItems.Add(txtLastName.Text.Trim());
            item.SubItems.Add(txtAge.Text.Trim());
            item.SubItems.Add(txtMajor.Text.Trim());
            item.SubItems.Add(dateTimePicker1.Text.Trim());
            listView1.Items.Add(item);

            txtFirstName.Clear();
            txtLastName.Clear();
            txtAge.Clear();
            txtMajor.Clear();
            txtFirstName.Focus();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem selctedItem = listView1.SelectedItems[0];

                
                lblID.Text = selctedItem.SubItems[0].Text;
                lblID_2.Text = selctedItem.SubItems[0].Text;
                lblFirstName.Text = selctedItem.SubItems[1].Text;
                lblLastName.Text = selctedItem.SubItems[2].Text;
                lblAge.Text = selctedItem.SubItems[3].Text;
                lblMajor.Text = selctedItem.SubItems[4].Text;
                lblDate.Text = selctedItem.SubItems[5].Text;

                if (selctedItem.ImageIndex == 0)
                {
                    pictureBox1.Image = Resources.Man;

                }
                else
                {
                    pictureBox1.Image = Resources.Women; 
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("HH:mm:ss");
            labelDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
        }

        private void rbDetails_CheckedChanged(object sender, EventArgs e)
        {
            listView1.View = View.Details;
        }

        private void rbList_CheckedChanged(object sender, EventArgs e)
        {
            listView1.View = View.List;
        }

        private void rbSmallIcon_CheckedChanged(object sender, EventArgs e)
        {
            listView1.View = View.SmallIcon;
        }

        private void rbLarge_CheckedChanged(object sender, EventArgs e)
        {
            listView1.View = View.LargeIcon;
        }

        private void rbTile_CheckedChanged(object sender, EventArgs e)
        {
            listView1.View = View.Tile;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count > 0)
            { 
                ListViewItem selectedItem = listView1.SelectedItems[0];

                selectedItem.Remove();
            }

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count > 0)
            { 
                ListViewItem item = listView1.SelectedItems[0];
                Form2 editfrm = new Form2(item);
                editfrm.ShowDialog();
            }

        }
    }
}