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
    public partial class BookDetailScreen : Form
    {
        LibraryDbContext dbContext = new LibraryDbContext();
        int bookId;

        public BookDetailScreen(int bookId)
        {
            InitializeComponent();
            this.bookId = bookId;
        }
       
        private void BookDetailScreen_Load(object sender, EventArgs e)
        {
            loadStafs();
        }

        void loadStafs() 
        {
            txtBookName.Text = dbContext.Books.FirstOrDefault(b => b.Id == bookId).Name;
            txtPageCount.Text = dbContext.Books.FirstOrDefault(b => b.Id == bookId).PageCount.ToString();
            dateTimePicker1.Value = dbContext.Books.FirstOrDefault(b => b.Id == bookId).Year;
            getAuthors();
            cbAuthor.SelectedValue = dbContext.Books.FirstOrDefault(b => b.Id == bookId).AuthorId;
            getCategories();

            //List<BookCategory> bookCategories = new List<BookCategory>();
            //bookCategories = dbContext.BookCategories.Where(x => x.BookId == bookId).ToList();

            //List<Category> names = new List<Category>();
            //foreach (var item in bookCategories)
            //{
            //    names = dbContext.Categories.Where(x => x.Id == item.CategoryId).ToList();
            //}

            //foreach (dynamic item in lbCategories.Items)
            //{
            //    if (names.Any(x=>x.Name==item.Name))
            //    {
            //        lbCategories.SetSelected(item.Id - 1, true);
            //    }  
            //}
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            List<int> selectedCategories = new List<int>();
            foreach (dynamic item in lbCategories.SelectedItems)
            {
                selectedCategories.Add(item.Id);
            }

            Book book = dbContext.Books.FirstOrDefault(bk => bk.Id == bookId);

            book.AuthorId = (int)cbAuthor.SelectedValue;
            book.Name = txtBookName.Text;
            book.PageCount = Convert.ToInt32(txtPageCount.Text);
            book.Year = dateTimePicker1.Value;
            
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Book book = dbContext.Books.FirstOrDefault(bk => bk.Id == bookId);
            dbContext.Books.Remove(book);
            int result = dbContext.SaveChanges();
            string message = result > 0 ? "Başarılı" : "Başarısız";
            MessageBox.Show(message);
            this.Close();
        }
    }
}
