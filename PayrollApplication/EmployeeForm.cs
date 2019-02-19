using System;
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Net.Mail;

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
        #region regexExpressions
        Regex objEmployeeID = new Regex("^[0-9]{3,4}$");
        Regex objFirstName = new Regex("^[A-Z][a-zA-Z]*$");
        Regex objLastName = new Regex("^[A-Z][a-zA-Z]*$");

        // *** UK Insurance Number ***
        // Must be 9 Characters
        //First 2 characters are alpha characters
        // Next 6 numbers are numeric
        // Final character can only be A, B, C, D or space
        // First character must not be D, F, I, Q, U or V
        // Example SB123456C
        // Regex objNI = new Regex(@"^[A-CEGHJ-PR-TW-Z]{1}[A-CEGHJ-NPR-TW-Z]{1}[0-9]{6}[A-D\s]$");

        //***  US SSN Number Validation ***
        // Must be 9 digits
        // Can be accepted as 123456789
        // or 123-45-6789
        //000-00-0000
        //Regex objSSN = new Regex(@"^\d{3}-?\d{2}-?\d{4}$");

        // Accepts eitehr NI or SSN number as input
        Regex objNNI = new Regex(@"^\d{3}-?\d{2}-?\d{4}$|^[A-CEGHJ-PR-TW-Z]{1}[A-CEGHJ-NPR-TW-Z]{1}[0-9]{6}[A-D\s]$");


        // email validation
        // example somevalue@somewhere.someaddress
        //somevalue : up to 30 characters and digits 
        // somewhere : up to 30 characters and digits 
        //someaddress : minumum of 2 characters  up to 30
       Regex objEmail = new Regex(@"^[a-zA-Z0-9]{1,30}@[a-zA-Z0-9]{1,30}.[a-zA-Z]{2,30}$");
        

        #endregion



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
            contolDataValid = validStringInput(txtEmployeeID) && validPatternMatch(objEmployeeID, txtEmployeeID);
            // Test the First Name
            contolDataValid = !contolDataValid ? false : validStringInput(txtFirstName)  && validPatternMatch(objFirstName, txtFirstName);
            // Test the Last Name
            contolDataValid = !contolDataValid ? false : validStringInput(txtLastName) && validPatternMatch(objLastName, txtLastName);

            // Test Gender
            contolDataValid = !contolDataValid ? false : validRadioInput(rdbMaile, rdbFemaile);
            // Test National Insurance
            contolDataValid = !contolDataValid ? false : validStringInput(txtNationalInsuranceNumber) && validPatternMatch(objNNI, txtNationalInsuranceNumber);
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
            contolDataValid = !contolDataValid ? false : validStringInput(txtEmailAddress) && validPatternMatch(objEmail, txtEmailAddress) && validateEmailAddress(txtEmailAddress);

            // Test Notes
            contolDataValid = !contolDataValid ? false : validNoteInput(txtNotes);

            return contolDataValid;
        }

        public bool validPatternMatch(Regex pattern, TextBox inputValue)
        {
            if (!pattern.IsMatch(inputValue.Text))
            {
                MessageBox.Show("Please enter " + inputValue.Name.ToString(), "Data Entry Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        public bool validateEmailAddress(TextBox inputValue)
        {
            try
            {
                MailAddress objMail = new MailAddress(inputValue.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + inputValue.Name.ToString() + " " + ex.Message, "Data Entry Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                inputValue.Focus();
                inputValue.BackColor = Color.OrangeRed;
                return false;
            }

            return true;
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
            txtEmployeeID.Clear();
            txtFirstName.Clear();
            txtLastName.Text = "";
            txtNationalInsuranceNumber.Text = "";
            txtAddress.Text = "";
            txtCity.Text = null;
            txtPostCode.Text = "";
            txtEmailAddress.Text = "";
            txtNotes.Text = "";
            txtPhoneNumber.Text = "";

            rdbMaile.Checked = false;
            rdbFemaile.Checked = false;
            rdbMarried.Checked = false;
            rdbSingle.Checked = false;

            cbUnionMembership.Checked = false;
            cboCountry.SelectedIndex = 0;
            cboState.SelectedIndex = 0;
           
            dtpDateOfBirth.Value = new DateTime(1998, 12, 31);

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
