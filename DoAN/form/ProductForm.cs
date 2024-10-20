using DoAN.childForm;
using DoAN.Model;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

namespace DoAN.form
{
    public partial class ProductForm : Form
    {
        public ProductForm()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            EditProduct f = new EditProduct();
            f.FormClosed += new FormClosedEventHandler(EditForm_FormClosed);
            f.ShowDialog();
        }

        private void ProductForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            Model1 db = new Model1();
            var listProduct = db.Products.ToList();

            dgvProduct.Rows.Clear();

            foreach (var item in listProduct)
            {
                int newRow = dgvProduct.Rows.Add();
                dgvProduct.Rows[newRow].Cells[0].Value = item.ProductID.ToString();
                dgvProduct.Rows[newRow].Cells[1].Value = item.ProductName;
                dgvProduct.Rows[newRow].Cells[2].Value = item.Category.CategoryName;
                dgvProduct.Rows[newRow].Cells[3].Value = item.Supplier.SupplierName;
                dgvProduct.Rows[newRow].Cells[4].Value = item.StockQuantity.ToString();
                dgvProduct.Rows[newRow].Cells[5].Value = item.UnitPrice.ToString();
            }
        }

        private void dgvProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvProduct.CurrentCell.OwningColumn.Name == "dgvEdit")
            {
                // Lấy ProductID từ dòng hiện tại
                int productId = Convert.ToInt32(dgvProduct.CurrentRow.Cells[0].Value);

                // Truy vấn cơ sở dữ liệu để lấy thông tin đầy đủ của sản phẩm
                Model1 db = new Model1();
                var product = db.Products.FirstOrDefault(p => p.ProductID == productId);

                if (product != null)
                {
                    EditProduct editForm = new EditProduct();
                    editForm.txtID.Text = product.ProductID.ToString();
                    editForm.txtName.Text = product.ProductName;
                    editForm.txtPrice.Text = product.UnitPrice.ToString();
                    editForm.txtQuantity.Text = product.StockQuantity.ToString();
                    editForm.cmbCategory.SelectedValue = product.Category.CategoryID;
                    editForm.cmbSuplier.SelectedValue = product.Supplier.SupplierID;

                    if (!string.IsNullOrEmpty(product.ProductImage) && File.Exists(product.ProductImage))
                    {
                        using (FileStream fs = new FileStream(product.ProductImage, FileMode.Open, FileAccess.Read))
                        {
                            editForm.picProduct.Image = Image.FromStream(fs);
                        }
                        editForm.selectedImagePath = product.ProductImage;
                    }

                    editForm.FormClosed += new FormClosedEventHandler(EditForm_FormClosed);
                    editForm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy sản phẩm!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
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

            dgvProduct.Rows.Clear();

            foreach (var item in searchResult)
            {
                int newRow = dgvProduct.Rows.Add();
                dgvProduct.Rows[newRow].Cells[0].Value = item.OrderID.ToString();
                dgvProduct.Rows[newRow].Cells[1].Value = item.OrderDate.ToString();
                dgvProduct.Rows[newRow].Cells[2].Value = item.CustomerID.ToString();
                dgvProduct.Rows[newRow].Cells[3].Value = item.EmployeeID.ToString();
                dgvProduct.Rows[newRow].Cells[4].Value = item.TotalAmount.ToString();
            }
        }


        private void EditForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            LoadData();
        }
    }
}
