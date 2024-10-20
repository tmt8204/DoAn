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
    public partial class EmployForm : Form
    {
        public EmployForm()
        {
            InitializeComponent();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            Model1 db = new Model1();
            string searchText = txtSearch.Text.Trim();
            var searchResult = db.Employees
                .Where(emp => emp.EmployeeName.Contains(searchText) ||
                              emp.Email.Contains(searchText) ||
                              emp.EmployeeID.ToString().Contains(searchText))
                .ToList();

            dgvEmploy.Rows.Clear();

            foreach (var item in searchResult)
            {
                int newRow = dgvEmploy.Rows.Add();
                dgvEmploy.Rows[newRow].Cells[0].Value = item.EmployeeID.ToString();
                dgvEmploy.Rows[newRow].Cells[1].Value = item.EmployeeName;
                dgvEmploy.Rows[newRow].Cells[2].Value = item.PhoneNumber;
                dgvEmploy.Rows[newRow].Cells[3].Value = item.Email;
                dgvEmploy.Rows[newRow].Cells[4].Value = item.Address;
            }
        }

        private void EmployForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {
            Model1 db = new Model1();
            var listEmploys = db.Employees.ToList();
            dgvEmploy.Rows.Clear();

            foreach (var Employ in listEmploys)
            {
                int newRow = dgvEmploy.Rows.Add();
                dgvEmploy.Rows[newRow].Cells[0].Value = Employ.EmployeeID.ToString();
                dgvEmploy.Rows[newRow].Cells[1].Value = Employ.EmployeeName;
                dgvEmploy.Rows[newRow].Cells[2].Value = Employ.PhoneNumber;
                dgvEmploy.Rows[newRow].Cells[3].Value = Employ.Email;
                dgvEmploy.Rows[newRow].Cells[4].Value = Employ.Address;
            }
        }

        private void dgvEmploy_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Model1 db = new Model1();
            if (dgvEmploy.CurrentCell.OwningColumn.Name == "dgvDelete")
            {
                int id = Convert.ToInt32(dgvEmploy.CurrentRow.Cells[0].Value);

                var confirmResult = MessageBox.Show("Bạn có chắc muốn xoá?",
                                                    "Xác nhận xoá",
                                                    MessageBoxButtons.YesNo,
                                                    MessageBoxIcon.Warning);
                if (confirmResult == DialogResult.Yes)
                {
                    try
                    {
                        var Employ = db.Employees.FirstOrDefault(p => p.EmployeeID == id);
                        if (Employ != null)
                        {
                            db.Employees.Remove(Employ);
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
            if (dgvEmploy.CurrentCell.OwningColumn.Name == "dgvEdit")
            {
                int EmployId = Convert.ToInt32(dgvEmploy.CurrentRow.Cells[0].Value);
                string EmployName = dgvEmploy.CurrentRow.Cells[1].Value.ToString();
                string phoneNumber = dgvEmploy.CurrentRow.Cells[2].Value.ToString();
                string email = dgvEmploy.CurrentRow.Cells[3].Value.ToString();
                string address = dgvEmploy.CurrentRow.Cells[4].Value.ToString();

                EditEmploy editForm = new EditEmploy();
                editForm.txtID.Text = EmployId.ToString();
                editForm.txtName.Text = EmployName;
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
            EditEmploy f = new EditEmploy();
            f.FormClosed += new FormClosedEventHandler(EditForm_FormClosed);
            f.ShowDialog();
        }
    }
}
