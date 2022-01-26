using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows;


namespace AdsBoard.ViewModel
{
    class ContextMainWindow
    {
        public Common.Command MyCommand
        {
            get
            {
                return new Common.Command(obj=>
                {
                    var accounts = Model.DBModel.GetDBModel().GetAccounts();

                    /*
                    var newUserProfile = new UserProfileVM { FirstName="Slava", SecondName="Shishkin", Birthday=new DateTime(1988,2,9), EMail="abrakadabra@mail.ru", PhoneNumber="123123123" };
                    var newAccount = new AccountVM { Login = "Regber", Password = "56456456", UserProfile= newUserProfile };
                    
                    accounts.Add(newAccount);

                    Model.DBModel.GetDBModel().Entry(newAccount).State = System.Data.Entity.EntityState.Added;

                    Model.DBModel.GetDBModel().SaveChanges();*/

                    foreach (var a in accounts)
                    {
                        MessageBox.Show($"{a.Login}   {a.Password} {a.UserProfile.FirstName} {a.UserProfile.Birthday}");
                    }
                });
            }
        }
    }


    

}
