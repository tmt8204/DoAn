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
    public partial class EditCategory : Form
    {
        private static Model1 db = new Model1();

        public EditCategory()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckInput())
                {
                    int categoryId = int.Parse(txtID.Text);
                    var existingCategory = db.Categories.FirstOrDefault(c => c.CategoryID == categoryId);

                    if (existingCategory != null)
                    {
                        // Cập nhật danh mục sản phẩm
                        existingCategory.CategoryName = txtName.Text;

                        db.SaveChanges();
                        MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // Thêm danh mục mới
                        Category newCategory = new Category()
                        {
                            CategoryID = categoryId,
                            CategoryName = txtName.Text,
                        };

                        db.Categories.Add(newCategory);
                        db.SaveChanges();
                        MessageBox.Show("Thêm danh mục mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                   !string.IsNullOrEmpty(txtName.Text);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
