using DoAN.Model;
using Guna.UI2.WinForms;
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
    public partial class SaleForm : Form
    {
        public SaleForm()
        {
            InitializeComponent();
        }

        private void SaleForm_Load(object sender, EventArgs e)
        {
            FillCmbCustomer();
            FillCmbEmploy();
            LoadProduct();
            dgvSale.DataError += dgvSale_DataError;
        }

        private void FillCmbEmploy()
        {
            Model1 db = new Model1();
            var listEmploy = db.Employees.ToList();

            cmbEmploy.Items.Clear();
            cmbEmploy.DataSource = listEmploy;
            cmbEmploy.DisplayMember = "EmployeeName";
            cmbEmploy.ValueMember = "EmployeeID";
            cmbEmploy.SelectedIndex = 0;
        }

        private void FillCmbCustomer()
        {
            Model1 db = new Model1();
            var listCustomer = db.Customers.ToList();

            cmbCustomer.Items.Clear();
            cmbCustomer.DataSource = listCustomer;
            cmbCustomer.DisplayMember = "CustomerName";
            cmbCustomer.ValueMember = "CustomerID";
            cmbCustomer.SelectedIndex = 0;
        }

        public void AddItem(string id, string name, string price, Image picProduct, string total)
        {
            var w = new UC_Product()
            {
                name = name,
                UniPrice = price,
                Image = picProduct,
                total = total,
                id = Convert.ToInt32(id)
            };

            flowLayoutPanel1.Controls.Add(w);

            w.onslect += (ss, ee) =>
            {
                var wdg = (UC_Product)ss;
                foreach (DataGridViewRow item in dgvSale.Rows)
                {
                    if (Convert.ToInt32(item.Cells["dgvProductID"].Value) == wdg.id)
                    {
                        item.Cells["dgvQty"].Value = int.Parse(item.Cells["dgvQty"].Value.ToString()) + 1;
                        item.Cells["dgvTotal"].Value = int.Parse(item.Cells["dgvQty"].Value.ToString()) *
                            decimal.Parse(item.Cells["dgvPrice"].Value.ToString());
                        GrandTotal();
                        return;
                    }
                }

                dgvSale.Rows.Add(new object[] {
                    wdg.id,
                    wdg.name,
                    1,
                    wdg.UniPrice,
                    Convert.ToDecimal(wdg.UniPrice) * 1
                });
                GrandTotal();
            };
        }

        private void GrandTotal()
        {
            double tot = 0;
            foreach (DataGridViewRow item in dgvSale.Rows)
            {
                tot += double.Parse(item.Cells["dgvTotal"].Value.ToString());
            }

            labelTotal.Text = tot.ToString("N2");
        }

        private void LoadProduct()
        {
            Model1 db = new Model1();
            var listProduct = db.Products.ToList();
            foreach (var product in listProduct)
            {
                AddItem(product.ProductID.ToString(), product.ProductName, product.UnitPrice.ToString(), Image.FromFile(product.ProductImage), product.UnitPrice.ToString());
            }
        }

        private void dgvSale_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvSale.Columns[e.ColumnIndex].Name == "dgvDelete")
            {
                dgvSale.Rows.RemoveAt(e.RowIndex);
                GrandTotal();
            }
        }

        private void dgvSale_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            dgvSale.Rows.Clear();
            dtpDate.Value = DateTime.Now;
            cmbCustomer.SelectedIndex = -1;
            labelTotal.Text = "0.00";
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim().ToLower();

            foreach (Control control in flowLayoutPanel1.Controls)
            {
                if (control is UC_Product product)
                {
                    product.Visible = product.name.ToLower().Contains(searchText);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbCustomer.SelectedIndex == -1 || dgvSale.Rows.Count == 0)
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin đơn hàng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Model1 db = new Model1();

                Order order = new Order
                {
                    OrderDate = dtpDate.Value,
                    CustomerID = (int)cmbCustomer.SelectedValue,
                    EmployeeID = (int)cmbEmploy.SelectedValue, // Sử dụng EmployeeID từ ComboBox
                    TotalAmount = decimal.Parse(labelTotal.Text)
                };
                db.Orders.Add(order);
                db.SaveChanges();

                foreach (DataGridViewRow row in dgvSale.Rows)
                {
                    if (row.IsNewRow) continue;

                    if (!int.TryParse(row.Cells["dgvProductID"].Value.ToString(), out int productId) ||
                        !int.TryParse(row.Cells["dgvQty"].Value.ToString(), out int quantity) ||
                        !decimal.TryParse(row.Cells["dgvPrice"].Value.ToString(), out decimal unitPrice) ||
                        !decimal.TryParse(row.Cells["dgvTotal"].Value.ToString(), out decimal totalPrice))
                    {
                        MessageBox.Show("Thông tin chi tiết đơn hàng không hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    OrderDetail orderDetail = new OrderDetail
                    {
                        OrderID = order.OrderID, 
                        ProductID = productId,
                        Quantity = quantity,
                        UnitPrice = unitPrice,
                        TotalPrice = totalPrice
                    };

                    db.OrderDetails.Add(orderDetail);
                }

                db.SaveChanges();

                MessageBox.Show("Lưu thông tin đơn hàng và chi tiết thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message + "\nChi tiết lỗi: " + ex.InnerException?.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
