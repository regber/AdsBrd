using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel;
using System.Text.RegularExpressions;


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
            var ads = thisDBModel.Ads.Include(ad=>ad.Account).Include(ad=>ad.MainImage).Include(ad=>ad.Images).Include(ad=>ad.Account.UserProfile).ToList();

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
    class Account: IDataErrorInfo
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

        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case "Login":
                        if (Login == string.Empty)
                        {
                            error = "Поле не должно быть пустым";
                        }
                        break;
                    case "Password":
                        if (Password == string.Empty)
                        {
                            error = "Поле не должно быть пустым";
                        }
                        break;
                }
                return error;
            }
        }
        public string Error
        {
            get { throw new NotImplementedException(); }
        }
    }

    class UserProfile: IDataErrorInfo
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


        public string this[string columnName]
        {
            get
            {
                Regex regex = new Regex(@"\w*@\w*\.\w{1,3}$");

                string error = String.Empty;
                switch (columnName)
                {
                    case "FirstName":
                        if (FirstName == string.Empty)
                        {
                            error = "Поле не может быть пустым";
                        }
                        break;
                    case "SecondName":
                        if (SecondName == string.Empty)
                        {
                            error = "Поле не может быть пустым";
                        }
                        break;
                    case "Birthday":
                        if (Birthday == null)
                        {
                            error = "Поле не может быть пустым";
                        }
                        if (Birthday.Date > DateTime.Now)
                        {
                            error = "Дата рождения должна быть в прошлом";
                        }

                        break;
                    case "PhoneNumber":
                        if (PhoneNumber == string.Empty || PhoneNumber == null)
                        {
                            error = "Поле не может быть пустым";
                        }
                        else if (PhoneNumber.Any(c => Char.IsLetter(c)))
                        {
                            error = "В номере телефона не могут быть указаны буквы";
                        }
                        break;
                    case "EMail":
                        if (EMail == string.Empty || EMail == null)
                        {
                            error = "Поле не может быть пустым";
                        }
                        else if (!regex.Match(EMail).Success)
                        {
                            error = "Указан не верный адрес электронной почты";
                        }
                        break;
                }
                return error;
            }

        }

        public string Error => throw new NotImplementedException();
    }

    class Ad: IDataErrorInfo
    {
        public int Id { get; set; }
        //public string Header { get; set; }
        public string Text { get; set; }

        public int Price { get; set; }
        public string Producer { get; set; }
        public string Model { get; set; }
        public double EngineVolume { get; set; }
        public int ReleaseYear { get; set; }
        public string Drive { get; set; }
        public string Transmission { get; set; }
        public string FuelType { get; set; }



        public Account Account { get; set; }

        public Image MainImage { get; set; }
        public ICollection<Image> Images { get; set; }


        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    /*case nameof(Header):
                        if (Header == string.Empty)
                        {
                            error = "Поле не может быть пустым";
                        }
                        break;*/
                    case nameof(Text):
                        if (Text == string.Empty)
                        {
                            error = "Поле не может быть пустым";
                        }
                        break;
                    case nameof(Price):
                        if (Price <= 0)
                        {
                            error = "Цена должна быть больше нуля";
                        }
                        break;
                    case nameof(Producer):
                        if (Producer == string.Empty)
                        {
                            error = "Поле не может быть пустым";
                        }
                        break;
                    case nameof(Model):
                        if (Model == string.Empty)
                        {
                            error = "Поле не может быть пустым";
                        }
                        break;
                    case nameof(EngineVolume):
                        if (EngineVolume <=0)
                        {
                            error = "Значение должно быть больше нуля";
                        }
                        break;
                    case nameof(ReleaseYear):
                        if (ReleaseYear == 0)
                        {
                            error = "Укажите значение";
                        }
                        break;
                    case nameof(Drive):
                        if (Drive == string.Empty)
                        {
                            error = "Поле не может быть пустым";
                        }
                        break;
                    case nameof(Transmission):
                        if (Transmission == string.Empty)
                        {
                            error = "Поле не может быть пустым";
                        }
                        break;
                    case nameof(FuelType):
                        if (FuelType == string.Empty)
                        {
                            error = "Поле не может быть пустым";
                        }
                        break;
                }
                return error;
            }
        }

        public string Error => throw new NotImplementedException();

    }

    class Image
    {
        public int Id { get; set; }

        public string ImagePath { get; set; }

        public Ad Ad { get; set; }

    }
}
