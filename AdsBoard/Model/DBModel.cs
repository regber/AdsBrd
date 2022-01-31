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
        public DBModel() : base("name=AdsDBConnection") { }

        private static DBModel thisDBModel;

        public static DBModel GetDBModel()
        {
            if (thisDBModel == null) 
            { 
                thisDBModel = new DBModel();
            }
            return thisDBModel;
        }


        public List<Account> GetAccounts()
        {
            var accounts = thisDBModel.Accounts.Include(a => a.UserProfile).Include(a => a.Ads).ToList();

            return accounts;
        }
        public List<Ad> GetAds()
        {
            var ads = thisDBModel.Ads.Include(ad=>ad.Account).Include(ad=>ad.MainImage).Include(ad=>ad.Images).ToList();

            return ads;
        }
        public List<Image> GetAdsImages()
        {
            var images = thisDBModel.Images.Include(a => a.Ad).ToList();

            return images;
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().Property(a => a.Login).IsRequired();
            modelBuilder.Entity<Account>().Property(a => a.EncryptPassword).IsRequired();
            modelBuilder.Entity<Account>().Ignore(a => a.Password);

            modelBuilder.Entity<UserProfile>().HasRequired(u => u.Account).WithOptional(a => a.UserProfile).WillCascadeOnDelete(true);
            modelBuilder.Entity<UserProfile>().Property(p => p.FirstName).IsRequired();
            modelBuilder.Entity<UserProfile>().Property(p => p.SecondName).IsRequired();
            modelBuilder.Entity<UserProfile>().Property(p => p.Birthday).IsRequired();
            modelBuilder.Entity<UserProfile>().Property(p => p.PhoneNumber).IsRequired();
            modelBuilder.Entity<UserProfile>().Property(p => p.EMail).IsRequired();

            modelBuilder.Entity<Ad>().HasRequired(ad => ad.Account).WithMany(a => a.Ads).WillCascadeOnDelete(true);
            modelBuilder.Entity<Ad>().Property(ad => ad.Price).IsRequired();

            modelBuilder.Entity<Image>().HasRequired(i => i.Ad).WithOptional(ad => ad.MainImage).WillCascadeOnDelete(true);
            modelBuilder.Entity<Image>().HasRequired(i => i.Ad).WithMany(ad => ad.Images);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<UserProfile> UsersProfiles { get; set; }
        public DbSet<Ad> Ads { get; set; }
        public DbSet<Image> Images { get; set; }
    }
    class Account
    {

        public int Id { get; set; }
        public string Login { get; set; }

        public string EncryptPassword { get; set; }
        public string Password 
        { 
            get
            {
                return Common.Cryprograpy.Decrypt(EncryptPassword);
            }
            set
            {
                EncryptPassword = Common.Cryprograpy.Encrypt(value);
            }
        }

        public UserProfile UserProfile { get; set; }
        public ICollection<Ad> Ads { get; set; }
    }

    class UserProfile
    {
        public UserProfile()
        {
            Birthday = new DateTime(2000, 1, 1);
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }

        public DateTime Birthday { get; set; }

        public string PhoneNumber { get; set; }
        public string EMail { get; set; }

        public Account Account { get; set; }
    }

    class Ad
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Text { get; set; }

        public int Price { get; set; }

        public Account Account { get; set; }

        public Image MainImage { get; set; }
        public ICollection<Image> Images { get; set; }

    }

    class Image
    {
        public int Id { get; set; }

        public string ImagePath { get; set; }

        public Ad Ad { get; set; }

    }
}
