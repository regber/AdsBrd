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
            if (thisDBModel == null) { thisDBModel = new DBModel(); }

            return thisDBModel;
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            //modelBuilder.Entity<Account>().HasMany(a => a.Ads).WithRequired(ad => ad.Account).Map(m=>m.MapKey("AccountId"));//!

            //modelBuilder.Entity<UserProfile>().HasRequired(u => u.Account).WithRequiredDependent(a => a.UserProfile).WillCascadeOnDelete(true);//!

            //modelBuilder.Entity<Ad>().Property(ad => ad.Id).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);//!

            modelBuilder.Entity<UserProfile>().HasRequired(u => u.Account).WithRequiredDependent(a => a.UserProfile).WillCascadeOnDelete(true);
            modelBuilder.Entity<UserProfile>().Property(u => u.FirstName).IsRequired();
            modelBuilder.Entity<UserProfile>().Property(u => u.SecondName).IsRequired();
            modelBuilder.Entity<UserProfile>().Property(u => u.PhoneNumber).IsRequired();
            modelBuilder.Entity<UserProfile>().Property(u => u.EMail).IsRequired();

            modelBuilder.Entity<Account>().HasMany(a => a.Ads).WithRequired(ad => ad.Account).WillCascadeOnDelete(true);

            modelBuilder.Entity<Image>().Property(i => i.Id).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Image>().HasRequired(i => i.Ad).WithRequiredDependent(ad=>ad.MainImage).WillCascadeOnDelete(true);
            modelBuilder.Entity<Image>().HasRequired(i=>i.Ad).WithMany(ad=>ad.Images).WillCascadeOnDelete(true);


            //modelBuilder.Entity<Ad>().HasRequired(a=>a.MainImage).WithOptional().WillCascadeOnDelete(false);
            //modelBuilder.Entity<Ad>().HasMany(a => a.Images);

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
