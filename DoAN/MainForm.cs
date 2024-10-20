using DoAN.form;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace DoAN
{
    public partial class MainForm : Form
    {

        public MainForm()
        {
            InitializeComponent();
        }

        private Form CurrenChildForm;

        public void addForm(Form child)
        {
            if(CurrenChildForm != null)
            {
                CurrenChildForm.Close();

            }
            CurrenChildForm = child;
            child.TopLevel = false;
            child.FormBorderStyle = FormBorderStyle.None;
            child.Dock = DockStyle.Fill;
            panelControls.Controls.Add(child);
            panelControls.Tag = child;
            child.BringToFront();
            child.Show();
        }

        private void Minimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnSignout_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Hide();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnEmployee_Click(object sender, EventArgs e)
        {
            EmployForm employForm = new EmployForm();
            addForm(employForm);
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            ProductForm productForm = new ProductForm();
            addForm(productForm);
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            CustomerForm customerForm = new CustomerForm();
            addForm(customerForm);
        }

        private void btnInvoices_Click(object sender, EventArgs e)
        {
            InvoicesForm f = new InvoicesForm();
            addForm(f);
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            OrderForm orderForm = new OrderForm();
            addForm(orderForm);
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            CategoryForm categoryForm = new CategoryForm();
            addForm(categoryForm);
        }

        private void btnSupplier_Click(object sender, EventArgs e)
        {
            SuplierForm supplierForm = new SuplierForm();
            addForm(supplierForm);
        }

        private void btnRenger_Click(object sender, EventArgs e)
        {

        }
    }
}
