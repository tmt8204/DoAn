using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DoAN.Model;

namespace DoAN.childForm
{
    public partial class EditProduct : Form
    {
        private static Model1 db = new Model1();
        public string selectedImagePath = ""; // Biến nội bộ để lưu đường dẫn ảnh

        public EditProduct()
        {
            InitializeComponent();

            picProduct.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckInput())
                {
                    int productId = int.Parse(txtID.Text);
                    var existingProduct = db.Products.FirstOrDefault(p => p.ProductID == productId);

                    if (existingProduct != null)
                    {
                        // Cập nhật sản phẩm hiện tại
                        existingProduct.ProductName = txtName.Text;
                        existingProduct.UnitPrice = decimal.Parse(txtPrice.Text);
                        existingProduct.StockQuantity = int.Parse(txtQuantity.Text);
                        existingProduct.CategoryID = int.Parse(cmbCategory.SelectedValue.ToString());
                        existingProduct.SupplierID = int.Parse(cmbSuplier.SelectedValue.ToString());
                        existingProduct.ProductImage = selectedImagePath; // Lưu đường dẫn ảnh

                        db.SaveChanges();
                        MessageBox.Show("Cập nhật sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // Thêm sản phẩm mới
                        Product newProduct = new Product()
                        {
                            ProductID = productId,
                            ProductName = txtName.Text,
                            UnitPrice = decimal.Parse(txtPrice.Text),
                            StockQuantity = int.Parse(txtQuantity.Text),
                            CategoryID = int.Parse(cmbCategory.SelectedValue.ToString()),
                            SupplierID = int.Parse(cmbSuplier.SelectedValue.ToString()),
                            ProductImage = selectedImagePath // Lưu đường dẫn ảnh
                        };

                        db.Products.Add(newProduct);
                        db.SaveChanges();
                        MessageBox.Show("Thêm sản phẩm mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    this.Close(); // Đóng form sau khi lưu
                }
                else
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin và chọn hình ảnh!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            // Tạo OpenFileDialog để mở hộp thoại chọn file
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Chọn hình ảnh sản phẩm";
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Lấy đường dẫn ảnh đã chọn
                    selectedImagePath = openFileDialog.FileName;

                    // Hiển thị hình ảnh trong PictureBox
                    using (FileStream fs = new FileStream(selectedImagePath, FileMode.Open, FileAccess.Read))
                    {
                        picProduct.Image = Image.FromStream(fs);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra khi tải hình ảnh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool CheckInput()
        {
            return !string.IsNullOrEmpty(txtID.Text) &&
                   !string.IsNullOrEmpty(txtName.Text) &&
                   !string.IsNullOrEmpty(txtPrice.Text) &&
                   !string.IsNullOrEmpty(txtQuantity.Text) &&
                   !string.IsNullOrEmpty(selectedImagePath); // Đảm bảo ảnh đã được chọn
        }

        public void EditProduct_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {
            Model1 db = new Model1();
            var listCategory = db.Categories.ToList();
            var listSuplier = db.Suppliers.ToList();

            cmbCategory.DataSource = listCategory;
            cmbCategory.DisplayMember = "CategoryName";
            cmbCategory.ValueMember = "CategoryID";

            cmbSuplier.DataSource = listSuplier;
            cmbSuplier.DisplayMember = "SupplierName";
            cmbSuplier.ValueMember = "SupplierID";
        }
    }
}
