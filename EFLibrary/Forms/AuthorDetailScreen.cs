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
    public partial class AuthorDetailScreen : Form
    {
        LibraryDbContext dbContext = new LibraryDbContext();
        int authorId;

        public AuthorDetailScreen(int authorId)
        {
            InitializeComponent();
            this.authorId = authorId;
        }

        private void AuthorDetailScreen_Load(object sender, EventArgs e)
        {
            txtName.Text = dbContext.Authors.FirstOrDefault(a => a.Id == authorId).Name;
            txtBirth.Text = dbContext.Authors.FirstOrDefault(a => a.Id == authorId).Birthday;
            txtInfos.Text = dbContext.Authors.FirstOrDefault(a => a.Id == authorId).Informations;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Author author = dbContext.Authors.FirstOrDefault(a => a.Id == authorId);

            author.Name = txtName.Text;
            author.Birthday = txtBirth.Text;
            author.Informations = txtInfos.Text;

           
            int result = dbContext.SaveChanges();
            string message = result > 0 ? "Başarılı" : "Başarısız";
            MessageBox.Show(message);
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Author author = dbContext.Authors.FirstOrDefault(a => a.Id == authorId);
            dbContext.Authors.Remove(author);
            int result = dbContext.SaveChanges();
            string message = result > 0 ? "Başarılı" : "Başarısız";
            MessageBox.Show(message);
            this.Close();
        }
    }
}
