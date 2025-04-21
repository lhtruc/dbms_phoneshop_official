using PhoneStore.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhoneStore.Forms
{
    public partial class ucCustomerForEmployee : UserControl
    {
        public ucCustomerForEmployee()
        {
            InitializeComponent();
        }
        private void LoadData()
        {
            try
            {
                dgv_Customer.DataSource = Customer.GetAllCustomerInfo();
                txt_PhoneNumber.ResetText();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void ucCustomerForEmployee_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btn_Reload_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            dgv_Customer.DataSource = Customer.GetCustomerByPhoneNumberForEmployee(txt_PhoneNumber.Text.Trim());
        }
    }
}
