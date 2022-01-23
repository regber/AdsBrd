using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


namespace AdsBoard.Model
{
    class DBModel:DbContext
    {
        private static DBModel thisDBModel;

        public static DBModel GetDBModel()
        {
            if (thisDBModel == null) { thisDBModel = new DBModel(); }

            return thisDBModel;
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().HasMany(a => a.Ads);
            modelBuilder.Entity<Account>().HasRequired(a => a.UserProfile).WithRequiredDependent(u=>u.Account);

            modelBuilder.Entity<Ad>().HasRequired(a=>a.MainImage);
            modelBuilder.Entity<Ad>().HasMany(a => a.Images);

            base.OnModelCreating(modelBuilder);
        }
    }
    class Account
    {

        public int Id { get; set; }
        public string Login { get; set; }

        private string _Password;
        public string Password 
        { 
            get
            {
                return Common.Cryprograpy.Decrypt(_Password);
            }
            set
            {
                _Password = Common.Cryprograpy.Encrypt(value);
            }
        }

        public UserProfile UserProfile { get; set; }
        public ICollection<Ad> Ads { get; set; }
    }

    class UserProfile
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }

        public string PhoneNumber { get; set; }
        public string EMail { get; set; }

        public Account Account { get; set; }
    }

    class Ad
    {
        public int Id { get; set; }

        public string Header { get; set; }
        public string Text { get; set; }

        public Image MainImage { get; set; }
        public ICollection<Image> Images { get; set; }

    }

    class Image
    {
        public int Id { get; set; }

        public string ImagePath { get; set; }

    }
}
