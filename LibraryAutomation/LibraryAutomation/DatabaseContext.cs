using LibraryAutomation.Data;
using LibraryAutomation.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LibraryAutomation
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : this("name=DatabaseContext")
        {
        }

        public DatabaseContext(string connectionString) : base(connectionString)
        {
            this.Configuration.ValidateOnSaveEnabled = false;
        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserLoginToken> UserLoginTokens { get; set; }
        
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<BookType> BookTypes { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
    }

}