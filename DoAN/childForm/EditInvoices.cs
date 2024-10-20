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
    public partial class EditInvoices : Form
    {
        public EditInvoices()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Model1 db = new Model1();
            try
            {
                if (CheckInput())
                {
                    int orderId = int.Parse(txtID.Text);
                    var existingOrder = db.OrderDetails.FirstOrDefault(o => o.OrderDetailID == orderId);

                    if (existingOrder != null)
                    {
                        // Cập nhật đơn hàng
                        existingOrder.Product.ProductName = txtProID.Text;
                        existingOrder.Quantity = int.Parse(txtQuantity.Text);
                        existingOrder.UnitPrice = int.Parse(txtUnitPrice.Text);

                        db.SaveChanges();
                        MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // Thêm đơn hàng mới
                        OrderDetail newOrder = new OrderDetail()
                        {
                            OrderDetailID = orderId,
                            Quantity = int.Parse(txtQuantity.Text),
                            UnitPrice = int.Parse(txtUnitPrice.Text),
                            ProductID = int.Parse(txtProID.Text)
                        };

                        db.OrderDetails.Add(newOrder);
                        db.SaveChanges();
                        MessageBox.Show("Thêm đơn hàng mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    this.Close(); // Đóng form sau khi lưu
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
                   !string.IsNullOrEmpty(txtProID.Text) &&
                   !string.IsNullOrEmpty(txtQuantity.Text) &&
                   !string.IsNullOrEmpty(txtUnitPrice.Text);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
