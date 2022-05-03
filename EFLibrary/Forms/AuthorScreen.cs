using EFLibrary.Data;
using EFLibrary.Models;
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
    public partial class AuthorScreen : Form
    {
        public AuthorScreen()
        {
            InitializeComponent();
        }

        LibraryDbContext context = new LibraryDbContext();
        
        private void btnAddAuthor_Click(object sender, EventArgs e)
        {
            Author author = new Author
            {
               Name = txtName.Text,
               Informations = txtInfos.Text,
               Birthday = txtBirth.Text
            };
            
            context.Authors.Add(author);
            int result = context.SaveChanges();
            string message = result > 0 ? "Başarılı" : "Başarısız";
            MessageBox.Show(message);
            getAuthors();
        }

        void getAuthors() 
        {
            var authors = context.Authors.ToList();
            dataGridView1.DataSource = authors;
        }

        private void AuthorScreen_Load(object sender, EventArgs e)
        {
            getAuthors();
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = (int)dataGridView1.Rows[e.RowIndex].Cells[0].Value;
            AuthorDetailScreen authorDetailScreen = new AuthorDetailScreen(id);
            authorDetailScreen.Show();
        }
    }
}
