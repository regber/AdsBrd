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


    }
}
