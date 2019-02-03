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

        bool isNumberOrBackspace;

        const int backspace_key = 8;

        public EmployeeForm()
        {
            InitializeComponent();
        }

        private bool validNumericInput(char input)
        {
          bool numberOrBackspace  = false;

            // Constrain input to numeric digits, exclude characters and backspace
            if (char.IsNumber(input) ||   char.GetNumericValue(input)== backspace_key)
            {
                numberOrBackspace = true;
            }

            return numberOrBackspace;
           
        }

        private bool validStringInput(TextBox inputValue)
        {
            if( String.IsNullOrEmpty(inputValue.Text) || inputValue.Text.Length < 0  )
            {
                MessageBox.Show("Please enter " + inputValue.Name.ToString() , "Data Entry Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                inputValue.Focus();
                inputValue.BackColor = Color.OrangeRed;
                return false;
            }
            else
            {
                inputValue.BackColor = Color.White;
            }
            return true;
        }

        private bool validNoteInput(TextBox inputValue)
        {
            bool validLength = validStringInput(inputValue);

            if (!validLength)
            {
                return false;
            }
            else
            {
                if (inputValue.Text.Length > 30)
                {
                    MessageBox.Show(inputValue.Name.ToString() + " exceeds maximum length, please shorten note", "Data Entry Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    inputValue.BackColor = Color.OrangeRed;

                    return false;
                }
                else
                {
                    inputValue.BackColor = Color.White;
                }

            }
            return true;
        }

        private bool validComboBoxInput(ComboBox input)
        {
            if( input.SelectedIndex <= 0)
            {
                MessageBox.Show("Please enter " + input.Name.ToString(), "Data Entry Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                input.Focus();
                input.BackColor = Color.OrangeRed;
                return false;
            }
            else
            {
                input.BackColor = Color.White;
            }

            return true;
        }



        private bool validRadioInput(RadioButton input1, RadioButton input2)
        {
            if(input1.Checked == false && input2.Checked == false)
            {
                MessageBox.Show("Please check either " +  input1.Name + " or " + input2.Name,
                                                        "Data Entry Error",
                                                        MessageBoxButtons.OK,
                                                        MessageBoxIcon.Error );
                input1.BackColor = Color.OrangeRed;
                input2.BackColor = Color.OrangeRed;
                return false;
            }
            else
            {
                input1.BackColor = Color.White;
                input2.BackColor = Color.White;
            }
            return true;
        }
        private bool isControlsDataValid()
        {
            bool contolDataValid = true;

            //Test the employeeID
            contolDataValid = validStringInput(txtEmployeeID);

            // Test the Last Name
            contolDataValid = !contolDataValid?false: validStringInput(txtLastName);
            // Test the First Name
            contolDataValid = !contolDataValid ? false : validStringInput(txtFirstName);
            // Test the Last Name
            contolDataValid = !contolDataValid ? false : validStringInput(txtLastName);
            // Test Gender
            contolDataValid = !contolDataValid ? false : validRadioInput(rdbMaile, rdbFemaile);
            // Test National Insurance
            contolDataValid = !contolDataValid ? false : validStringInput(txtNationalInsuranceNumber);
            // Test Marital Status
            contolDataValid = !contolDataValid ? false : validRadioInput(rdbMarried, rdbSingle);

            // Test Address
            contolDataValid = !contolDataValid ? false : validStringInput(txtAddress);
            // Test City
            contolDataValid = !contolDataValid ? false : validStringInput(txtCity);

            // Test State
            contolDataValid = !contolDataValid ? false : validComboBoxInput(cboState);

            // Test Post/Zip Codes
            contolDataValid = !contolDataValid ? false : validStringInput(txtPostCode);

            // Test Country
            contolDataValid = !contolDataValid ? false : validComboBoxInput(cboCountry);

            // Test phone
            contolDataValid = !contolDataValid ? false : validStringInput(txtPhoneNumber);

            // Test email
            contolDataValid = !contolDataValid ? false : validStringInput(txtEmailAddress);

            // Test Notes
            contolDataValid = !contolDataValid ? false : validNoteInput(txtNotes);

            return contolDataValid;
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
            if (isControlsDataValid())
            {
                MessageBox.Show("Employee Added");
            }
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
        #region  implicit_validation

        private void txtEmployeeID_KeyPress(object sender, KeyPressEventArgs e)
        {
            isNumberOrBackspace = validNumericInput(e.KeyChar);

            if (!isNumberOrBackspace)
            {
                e.Handled = true;
            }
        }

        private void txtPhoneNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            isNumberOrBackspace = validNumericInput(e.KeyChar);

            if (!isNumberOrBackspace)
            {
                e.Handled = true;
            }

        }
        #endregion // keypress event validation

        private void txtEmployeeID_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
