using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PayrollApplication
{
    public partial class EmployeeForm : Form
    {
        public EmployeeForm()
        {
            InitializeComponent();
        }

        private void lblNationalInsuranceFile_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void btnAddEmployees_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Employee Added");
        }

        private void btnUpdateEmployee_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Employee Updated");
        }

        private void btnDeleteEmployee_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Employee Deleted");
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Form Reset");
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Form Preview");
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
