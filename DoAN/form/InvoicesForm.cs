using DoAN.childForm;
using DoAN. Model;
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
    public partial class InvoicesForm : Form
    {
        public InvoicesForm()
        {
            InitializeComponent();
        }

        private void InvoicesForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            Model1 db = new Model1();
            var listInvoices = db.OrderDetails.ToList();

            dgvInvoices.Rows.Clear();

            foreach ( var item in listInvoices)
            {
                int newRow = dgvInvoices.Rows.Add();
                dgvInvoices.Rows[newRow].Cells[0].Value = item.OrderDetailID.ToString();
                dgvInvoices.Rows[newRow].Cells[1].Value = item.Product.ProductName;
                dgvInvoices.Rows[newRow].Cells[2].Value = item.Quantity.ToString();
                dgvInvoices.Rows[newRow].Cells[3].Value = item.UnitPrice.ToString();
                dgvInvoices.Rows[newRow].Cells[4].Value = item.TotalPrice;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            Model1 db = new Model1();
            string searchText = txtSearch.Text.Trim();
            var searchResult = db.OrderDetails
                .Where(emp => emp.OrderDetailID.ToString().Contains(searchText) ||
                              emp.Product.ProductName.Contains(searchText) ||
                              emp.TotalPrice.ToString().Contains(searchText))
                .ToList();

            dgvInvoices.Rows.Clear();

            foreach (var item in searchResult)
            {
                int newRow = dgvInvoices.Rows.Add();
                dgvInvoices.Rows[newRow].Cells[0].Value = item.OrderDetailID.ToString();
                dgvInvoices.Rows[newRow].Cells[1].Value = item.Product.ProductName;
                dgvInvoices .Rows[newRow].Cells[2].Value = item.Quantity;
                dgvInvoices.Rows[newRow].Cells[3].Value = item.UnitPrice;
                dgvInvoices.Rows[newRow].Cells[4].Value = item.TotalPrice;
            }
        }

        private void dgvCellClick(object sender, DataGridViewCellEventArgs e)
        {
            Model1 db = new Model1();
            if (dgvInvoices.CurrentCell.OwningColumn.Name == "dgvDelete")
            {
                int id = Convert.ToInt32(dgvInvoices.CurrentRow.Cells[0].Value);

                var confirmResult = MessageBox.Show("Bạn có chắc muốn xoá?",
                                                    "Xác nhận xoá",
                                                    MessageBoxButtons.YesNo,
                                                    MessageBoxIcon.Warning);
                if (confirmResult == DialogResult.Yes)
                {
                    try
                    {
                        var category = db.Categories.FirstOrDefault(p => p.CategoryID == id);
                        if (category != null)
                        {
                            db.Categories.Remove(category);
                            db.SaveChanges();

                            MessageBox.Show("Xoá thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadData();
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy danh mục!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi xoá: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else if (dgvInvoices.CurrentCell.OwningColumn.Name == "dgvEdit")
            {
                MessageBox.Show("Không thể sửa hóa đơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information );
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            
        }
    }
}
