using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


namespace AdsBoard.Model
{
    class DBModel
    {

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
