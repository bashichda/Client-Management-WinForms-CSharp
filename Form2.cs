using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyFirstWindowsForm
{
    public partial class Form2 : Form
    {
        private ListViewItem selectedItem;

        public Form2(ListViewItem Item)
        {
            InitializeComponent();
            selectedItem = Item;

            txtFirstName.Text = selectedItem.SubItems[1].Text;
            txtLastName.Text = selectedItem.SubItems[2].Text;
            txtAge.Text = selectedItem.SubItems[3].Text;
            txtMajor.Text = selectedItem.SubItems[4].Text;
            dateTimePicker1.Text = selectedItem.SubItems[5].Text;
            if (selectedItem.ImageIndex == 0)
            {
                rbMale.Checked = true;
            }
            else
            {
                rbFemale.Checked = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            
            selectedItem.SubItems[1].Text = txtFirstName.Text;
            selectedItem.SubItems[2].Text = txtLastName.Text;
            selectedItem.SubItems[3].Text = txtAge.Text;
            selectedItem.SubItems[4].Text = txtMajor.Text;
            selectedItem.SubItems[5].Text = dateTimePicker1.Text;

            if (rbMale.Checked)
            {
                selectedItem.ImageIndex = 0;
            }
            else
            {
                selectedItem.ImageIndex = 1;
            }

            MessageBox.Show("Client Changed Info successfully.", "Successfuly");
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You sure you wanna Cancel This Operation? ","Confirm",MessageBoxButtons.YesNo,MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Close();
            }
        }
    }
}
