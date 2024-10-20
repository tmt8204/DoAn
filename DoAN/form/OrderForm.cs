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
    public partial class OrderForm : Form
    {
        public OrderForm()
        {
            InitializeComponent();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            Model1 db = new Model1();
            string searchText = txtSearch.Text.Trim();
            var searchResult = db.Orders
                .Where(order => order.OrderID.ToString().Contains(searchText) ||
                                order.CustomerID.ToString().Contains(searchText) ||
                                order.EmployeeID.ToString().Contains(searchText))
                .ToList();

            dgvOrder.Rows.Clear();

            foreach (var item in searchResult)
            {
                int newRow = dgvOrder.Rows.Add();
                dgvOrder.Rows[newRow].Cells[0].Value = item.OrderID.ToString();
                dgvOrder.Rows[newRow].Cells[1].Value = item.OrderDate.ToString();
                dgvOrder.Rows[newRow].Cells[2].Value = item.CustomerID.ToString();
                dgvOrder.Rows[newRow].Cells[3].Value = item.EmployeeID.ToString();
                dgvOrder.Rows[newRow].Cells[4].Value = item.TotalAmount.ToString();
            }
        }

        private void OrderForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {
            Model1 db = new Model1();
            var listOrders = db.Orders.ToList();
            dgvOrder.Rows.Clear();

            foreach (var order in listOrders)
            {
                int newRow = dgvOrder.Rows.Add();
                dgvOrder.Rows[newRow].Cells[0].Value = order.OrderID.ToString();
                dgvOrder.Rows[newRow].Cells[1].Value = order.OrderDate.ToString();
                dgvOrder.Rows[newRow].Cells[2].Value = order.CustomerID.ToString();
                dgvOrder.Rows[newRow].Cells[3].Value = order.EmployeeID.ToString();
                dgvOrder.Rows[newRow].Cells[4].Value = order.TotalAmount.ToString();
            }
        }

        private void dgvOrder_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Model1 db = new Model1();
            if (dgvOrder.CurrentCell.OwningColumn.Name == "dgvDelete")
            {
                int id = Convert.ToInt32(dgvOrder.CurrentRow.Cells[0].Value);

                var confirmResult = MessageBox.Show("Bạn có chắc muốn xoá?",
                                                    "Xác nhận xoá",
                                                    MessageBoxButtons.YesNo,
                                                    MessageBoxIcon.Warning);
                if (confirmResult == DialogResult.Yes)
                {
                    try
                    {
                        var order = db.Orders.FirstOrDefault(o => o.OrderID == id);
                        if (order != null)
                        {
                            db.Orders.Remove(order);
                            db.SaveChanges();

                            MessageBox.Show("Xoá thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadData();
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy đơn hàng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi xoá: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else if (dgvOrder.CurrentCell.OwningColumn.Name == "dgvEdit")
            {
                int orderId = Convert.ToInt32(dgvOrder.CurrentRow.Cells[0].Value);
                DateTime orderDate = Convert.ToDateTime(dgvOrder.CurrentRow.Cells[1].Value);
                int customerId = Convert.ToInt32(dgvOrder.CurrentRow.Cells[2].Value);
                int employeeId = Convert.ToInt32(dgvOrder.CurrentRow.Cells[3].Value);
                decimal totalAmount = Convert.ToDecimal(dgvOrder.CurrentRow.Cells[4].Value);

                EditOrder editForm = new EditOrder();
                editForm.txtID.Text = orderId.ToString();
                editForm.datetime.Value = orderDate;
                editForm.txtCustomerID.Text = customerId.ToString();
                editForm.txtEmployeeID.Text = employeeId.ToString();
                editForm.txtTotalAmount.Text = totalAmount.ToString();

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
            SaleForm saleForm = new SaleForm();
            saleForm.ShowDialog();
        }
    }
}
