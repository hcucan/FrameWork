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
    public partial class CategoryScreen : Form
    {
        public CategoryScreen()
        {
            InitializeComponent();
        }
        LibraryDbContext context = new LibraryDbContext();

        private void CategoryScreen_Load(object sender, EventArgs e)
        {
            getCategories();
        }

        void getCategories()
        {
            var categories = context.Categories.ToList();
            dataGridView1.DataSource = categories;
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            Category category = new Category 
            {
                Name = txtCatName.Text
            };

            context.Categories.Add(category);
            int result = context.SaveChanges();
            string message = result > 0 ? "Başarılı" : "Başarısız";
            MessageBox.Show(message);
            getCategories();
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = (int)dataGridView1.Rows[e.RowIndex].Cells[0].Value;
            CategoryDetailScreen categoryDetailScreen= new CategoryDetailScreen(id);
            categoryDetailScreen.Show();
        }
    }
}
