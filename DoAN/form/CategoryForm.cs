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
    public partial class CategoryForm : Form
    {
        public CategoryForm()
        {
            InitializeComponent();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            Model1 db = new Model1();
            string searchText = txtSearch.Text.Trim();
            var searchResult = db.Categories
                .Where(cat => cat.CategoryName.Contains(searchText) ||
                              cat.CategoryID.ToString().Contains(searchText))
                .ToList();

            dgvCategory.Rows.Clear();

            foreach (var item in searchResult)
            {
                int newRow = dgvCategory.Rows.Add();
                dgvCategory.Rows[newRow].Cells[0].Value = item.CategoryID.ToString();
                dgvCategory.Rows[newRow].Cells[1].Value = item.CategoryName;
            }
        }

        private void CategoryForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {
            Model1 db = new Model1();
            var listCategories = db.Categories.ToList();
            dgvCategory.Rows.Clear();

            foreach (var category in listCategories)
            {
                int newRow = dgvCategory.Rows.Add();
                dgvCategory.Rows[newRow].Cells[0].Value = category.CategoryID.ToString();
                dgvCategory.Rows[newRow].Cells[1].Value = category.CategoryName;
            }
        }

        private void dgvCategory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Model1 db = new Model1();
            if (dgvCategory.CurrentCell.OwningColumn.Name == "dgvDelete")
            {
                int id = Convert.ToInt32(dgvCategory.CurrentRow.Cells[0].Value);

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
            else if (dgvCategory.CurrentCell.OwningColumn.Name == "dgvEdit")
            {
                // Lấy thông tin danh mục từ DataGridView
                int categoryId = Convert.ToInt32(dgvCategory.CurrentRow.Cells[0].Value);
                string categoryName = dgvCategory.CurrentRow.Cells[1].Value.ToString();

                EditCategory editForm = new EditCategory();
                editForm.txtID.Text = categoryId.ToString();
                editForm.txtName.Text = categoryName;

                editForm.FormClosed += new FormClosedEventHandler(EditForm_FormClosed);
                editForm.ShowDialog();
            }
        }

        private void EditForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            LoadData(); // Gọi lại phương thức LoadData để làm mới DataGridView
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            EditCategory f = new EditCategory();
            f.FormClosed += new FormClosedEventHandler(EditForm_FormClosed);
            f.ShowDialog();
        }
    }
}
