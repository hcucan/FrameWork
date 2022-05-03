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
    public partial class BookScreen : Form
    {
        public BookScreen()
        {
            InitializeComponent();
        }
        LibraryDbContext dbContext = new LibraryDbContext();
        private void BookScreen_Load(object sender, EventArgs e)
        {
            getAuthors();
            getCategories();
        }

        private void getCategories()
        {
            var categories = dbContext.Categories.Select(c => new 
            {
                Id = c.Id,
                Name = c.Name,
            }).ToList();
            lbCategories.DataSource = categories;
            lbCategories.DisplayMember = "Name";
            lbCategories.ValueMember = "Id";
        }

        private void getAuthors()
        {
            var authors = dbContext.Authors.Select(a => new
            {
                Id = a.Id,
                Name = a.Name,  
            });
            cbAuthor.DataSource = authors.ToList();
            cbAuthor.DisplayMember = "Name";
            cbAuthor.ValueMember = "Id";
        }

        private void btnAddBook_Click(object sender, EventArgs e)
        {
            List<int> selectedCategories = new List<int>();
            foreach (dynamic item in lbCategories.SelectedItems)
            {
                selectedCategories.Add(item.Id);
            }

            Book book = new Book 
            {
                AuthorId = (int)cbAuthor.SelectedValue,
                Name = txtBookName.Text,
                PageCount = Convert.ToInt32(txtPageCount.Text),
                Year = dateTimeBookYear.Value
            }; 
            dbContext.Books.Add(book);
            dbContext.SaveChanges();

            selectedCategories.ForEach(s => book.BookCategories.Add(new BookCategory
            {
                BookId = book.Id,
                CategoryId = s,
            }));

            int result = dbContext.SaveChanges();
            string message = result > 0 ? "Başarılı" : "Başarısız";
            MessageBox.Show(message);
            this.Close();
        }
    }
}
