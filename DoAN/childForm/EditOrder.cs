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
    public partial class EditOrder : Form
    {
        private static Model1 db = new Model1();

        public EditOrder()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckInput())
                {
                    int orderId = int.Parse(txtID.Text);
                    var existingOrder = db.Orders.FirstOrDefault(o => o.OrderID == orderId);

                    if (existingOrder != null)
                    {
                        // Cập nhật đơn hàng
                        existingOrder.OrderDate = datetime.Value;
                        existingOrder.CustomerID = int.Parse(txtCustomerID.Text);
                        existingOrder.EmployeeID = int.Parse(txtEmployeeID.Text);
                        existingOrder.TotalAmount = decimal.Parse(txtTotalAmount.Text);

                        db.SaveChanges();
                        MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // Thêm đơn hàng mới
                        Order newOrder = new Order()
                        {
                            OrderID = orderId,
                            OrderDate = datetime.Value,
                            CustomerID = int.Parse(txtCustomerID.Text),
                            EmployeeID = int.Parse(txtEmployeeID.Text),
                            TotalAmount = decimal.Parse(txtTotalAmount.Text),
                        };

                        db.Orders.Add(newOrder);
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
                   !string.IsNullOrEmpty(txtCustomerID.Text) &&
                   !string.IsNullOrEmpty(txtEmployeeID.Text) &&
                   !string.IsNullOrEmpty(txtTotalAmount.Text);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
