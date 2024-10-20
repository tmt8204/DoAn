using DoAN.childForm;
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

namespace DoAN.form
{
    public partial class CustomerForm : Form
    {

        public CustomerForm()
        {
            InitializeComponent();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            Model1 db = new Model1();
            string searchText = txtSearch.Text.Trim();
            var searchResult = db.Customers
                .Where(emp => emp.CustomerName.Contains(searchText) ||
                              emp.Email.Contains(searchText) ||
                              emp.CustomerID.ToString().Contains(searchText))
                .ToList();

            dgvCustomer.Rows.Clear();

            foreach (var item in searchResult)
            {
                int newRow = dgvCustomer.Rows.Add();
                dgvCustomer.Rows[newRow].Cells[0].Value = item.CustomerID.ToString();
                dgvCustomer.Rows[newRow].Cells[1].Value = item.CustomerName;
                dgvCustomer.Rows[newRow].Cells[2].Value = item.PhoneNumber;
                dgvCustomer.Rows[newRow].Cells[3].Value = item.Email;
                dgvCustomer.Rows[newRow].Cells[4].Value = item.Address;
            }
        }

        private void CustomerForm_Load(object sender, EventArgs e)
        {
            Model1 db = new Model1();
            var listCustomers = db.Customers.ToList();
            dgvCustomer.Rows.Clear();

            foreach (var customer in listCustomers)
            {
                int newRow = dgvCustomer.Rows.Add();
                dgvCustomer.Rows[newRow].Cells[0].Value = customer.CustomerID.ToString();
                dgvCustomer.Rows[newRow].Cells[1].Value = customer.CustomerName;
                dgvCustomer.Rows[newRow].Cells[2].Value = customer.PhoneNumber;
                dgvCustomer.Rows[newRow].Cells[3].Value = customer.Email;
                dgvCustomer.Rows[newRow].Cells[4].Value = customer.Address;
            }
        }

        public void LoadData()
        {
            Model1 db = new Model1();
            var listCustomers = db.Customers.ToList();
            dgvCustomer.Rows.Clear();

            foreach (var customer in listCustomers)
            {
                int newRow = dgvCustomer.Rows.Add();
                dgvCustomer.Rows[newRow].Cells[0].Value = customer.CustomerID.ToString();
                dgvCustomer.Rows[newRow].Cells[1].Value = customer.CustomerName;
                dgvCustomer.Rows[newRow].Cells[2].Value = customer.PhoneNumber;
                dgvCustomer.Rows[newRow].Cells[3].Value = customer.Email;
                dgvCustomer.Rows[newRow].Cells[4].Value = customer.Address;
            }
        }

        private void dgvCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Model1 db = new Model1();
            if (dgvCustomer.CurrentCell.OwningColumn.Name == "dgvDelete")
            {
                int id = Convert.ToInt32(dgvCustomer.CurrentRow.Cells[0].Value);

                var confirmResult = MessageBox.Show("Bạn có chắc muốn xoá?",
                                                    "Xác nhận xoá",
                                                    MessageBoxButtons.YesNo,
                                                    MessageBoxIcon.Warning);
                if (confirmResult == DialogResult.Yes)
                {
                    try
                    {
                        var customer = db.Customers.FirstOrDefault(p => p.CustomerID == id);
                        if (customer != null)
                        {
                            db.Customers.Remove(customer);
                            db.SaveChanges();

                            MessageBox.Show("Xoá thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadData();
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi xoá: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            if (dgvCustomer.CurrentCell.OwningColumn.Name == "dgvEdit")
            {
                int customerId = Convert.ToInt32(dgvCustomer.CurrentRow.Cells[0].Value);
                string customerName = dgvCustomer.CurrentRow.Cells[1].Value.ToString();
                string phoneNumber = dgvCustomer.CurrentRow.Cells[2].Value.ToString();
                string email = dgvCustomer.CurrentRow.Cells[3].Value.ToString();
                string address = dgvCustomer.CurrentRow.Cells[4].Value.ToString();

                EditEmploy editForm = new EditEmploy();
                editForm.txtID.Text = customerId.ToString();
                editForm.txtName.Text = customerName;
                editForm.txtPhone.Text = phoneNumber;
                editForm.txtMail.Text = email;
                editForm.txtAddress.Text = address;

                editForm.FormClosed += new FormClosedEventHandler(EditForm_FormClosed);

                editForm.ShowDialog();
            }
        }

        private void EditForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            LoadData(); 
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            EditCustomer f = new EditCustomer();
            f.FormClosed += new FormClosedEventHandler(EditForm_FormClosed);
            f.ShowDialog();
        }
    }

}
