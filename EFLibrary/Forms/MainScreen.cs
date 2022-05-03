using EFLibrary.Data;
using Microsoft.EntityFrameworkCore;
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
    public partial class MainScreen : Form
    {
        public MainScreen()
        {
            InitializeComponent();
        }
        LibraryDbContext libraryDbContext = new LibraryDbContext();

        private void MainScreen_Load(object sender, EventArgs e)
        {
            libraryDbContext.Database.EnsureCreated();

            var allContext = libraryDbContext.Books.Include(b=>b.Author)
                                                     .Include(b=>b.BookCategories)
                                                     .ThenInclude(b=>b.Category);

            var data = allContext.Select(b => new
            {
                Id = b.Id,
                bookName = b.Name,
                authorName = b.Author.Name,
                category = b.BookCategories.FirstOrDefault(c => c.BookId == b.Id).Category.Name
            });

            var list = data.ToList();
            dataGridView1.DataSource = list;

        }

        private void btnAddAuthor_Click(object sender, EventArgs e)
        {
            AuthorScreen authorScreen = new AuthorScreen();
            authorScreen.Show();
        }

        private void btnAddBook_Click(object sender, EventArgs e)
        {
            BookScreen bookScreen = new BookScreen();
            bookScreen.Show();
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            CategoryScreen categoryScreen = new CategoryScreen();
            categoryScreen.Show();
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = (int)dataGridView1.Rows[e.RowIndex].Cells[0].Value;
            BookDetailScreen bookDetailScreen = new BookDetailScreen(id);
            bookDetailScreen.Show();
        }

        private void bntRefresh_Click(object sender, EventArgs e)
        {
            var allContext = libraryDbContext.Books.Include(b => b.Author)
                                                    .Include(b => b.BookCategories)
                                                    .ThenInclude(b => b.Category);

            var data = allContext.Select(b => new
            {
                Id = b.Id,
                bookName = b.Name,
                authorName = b.Author.Name,
                category = b.BookCategories.FirstOrDefault(c => c.BookId == b.Id).Category.Name
            });

            var list = data.ToList();
            dataGridView1.DataSource = list;
        }
    }
}
