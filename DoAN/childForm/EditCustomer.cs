using DoAN.form;
using DoAN.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAN.childForm
{
    public partial class EditCustomer : Form
    {
        private static Model1 db = new Model1();

        public EditCustomer()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckInput())
                {
                    int customerId = int.Parse(txtID.Text);
                    var existingCustomer = db.Customers.FirstOrDefault(c => c.CustomerID == customerId);

                    if (existingCustomer != null)
                    {
                        existingCustomer.CustomerName = txtName.Text;
                        existingCustomer.PhoneNumber = txtPhone.Text;
                        existingCustomer.Email = txtMail.Text;
                        existingCustomer.Address = txtAddress.Text;

                        db.SaveChanges();
                        MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        Customer newCustomer = new Customer()
                        {
                            CustomerID = customerId,
                            CustomerName = txtName.Text,
                            PhoneNumber = txtPhone.Text,
                            Email = txtMail.Text,
                            Address = txtAddress.Text,
                        };

                        db.Customers.Add(newCustomer);
                        db.SaveChanges();
                        MessageBox.Show("Thêm khách hàng mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    this.Close(); 
                }
                else
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool CheckInput()
        {
            return !string.IsNullOrEmpty(txtID.Text) &&
                   !string.IsNullOrEmpty(txtName.Text) &&
                   !string.IsNullOrEmpty(txtPhone.Text) &&
                   !string.IsNullOrEmpty(txtMail.Text) &&
                   !string.IsNullOrEmpty(txtAddress.Text);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }


}
