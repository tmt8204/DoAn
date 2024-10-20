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
    public partial class EditSuplier : Form
    {

        private static Model1 db = new Model1();

        public EditSuplier()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckInput())
                {
                    int suplierID = int.Parse(txtID.Text);
                    var existingCustomer = db.Suppliers.FirstOrDefault(c => c.SupplierID     == suplierID);

                    if (existingCustomer != null)
                    {
                        existingCustomer.SupplierName = txtName.Text;
                        existingCustomer.PhoneNumber = txtPhone.Text;
                        existingCustomer.Email = txtMail.Text;
                        existingCustomer.Address = txtAddress.Text;

                        db.SaveChanges();
                        MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        Supplier newCustomer = new Supplier()
                        {
                            SupplierID = suplierID,
                            SupplierName = txtName.Text,
                            PhoneNumber = txtPhone.Text,
                            Email = txtMail.Text,
                            Address = txtAddress.Text,
                        };

                        db.Suppliers.Add(newCustomer);
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
