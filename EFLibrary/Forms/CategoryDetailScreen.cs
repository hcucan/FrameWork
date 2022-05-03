using EFLibrary.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EFLibrary.Forms
{
    public partial class CategoryDetailScreen : Form
    {

        LibraryDbContext dbContext = new LibraryDbContext();
        int categoryId;
        public CategoryDetailScreen(int categoryId)
        {
            InitializeComponent();
            this.categoryId = categoryId;
        }

        private void CategoryDetailScreen_Load(object sender, EventArgs e)
        {
            txtCatName.Text = dbContext.Categories.FirstOrDefault(c => c.Id == categoryId).Name;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Category category = dbContext.Categories.FirstOrDefault(c => c.Id == categoryId);
            category.Name = txtCatName.Text;

           
            int result = dbContext.SaveChanges();
            string message = result > 0 ? "Başarılı" : "Başarısız";
            MessageBox.Show(message);
            this.Close();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Category category = dbContext.Categories.FirstOrDefault(c => c.Id == categoryId);
            dbContext.Categories.Remove(category);
            int result = dbContext.SaveChanges();
            string message = result > 0 ? "Başarılı" : "Başarısız";
            MessageBox.Show(message);
            this.Close();
        }
    }
}
