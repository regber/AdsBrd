using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AdsBoard.ViewModel.Windows
{
    class ContextEnterUC
    {
        List<Model.Account> accounts;

        public ToolTip validationLoginToolTip { get; set; } = new ToolTip();
        public ToolTip validationPasswordToolTip { get; set; } = new ToolTip();

        public ViewModelDBClass.AccountVM enteringAccountInform { get; set; } = new ViewModelDBClass.AccountVM();

        public ContextEnterUC()
        {
            System.Windows.Application.Current.MainWindow.Width = 600;
            System.Windows.Application.Current.MainWindow.Height = 450;

            //enteringAccountInform 
            accounts = Model.DBModel.GetDBModel().GetAccounts();
        }
        public Common.Commands.Command EnterCommand
        {
            get
            {
                return new Common.Commands.Command(obj =>
                {
                    var mainWindow = (ViewModel.ContextMainWindow)Window.GetWindow((UserControl)obj).DataContext;

                    //MessageBox.Show("change: "+Model.DBModel.GetDBModel().ChangeTracker.HasChanges().ToString());

                    accounts = Model.DBModel.GetDBModel().GetAccounts();

                    var currentAccounts = accounts.Where(a => a.Login == enteringAccountInform.Login);

                    if (currentAccounts.Count()>0)
                    {
                        var currentAccount = currentAccounts.First();

                        if (currentAccount.Password == enteringAccountInform.Password)
                        {
                            validationLoginToolTip.Content=string.Empty;
                            validationPasswordToolTip.Content = string.Empty;

                            //Заполняем свойства CurrentUser текущими данными пользователя и его логина
                            Common.CurrentUser.CurrentAccount = (ViewModelDBClass.AccountVM)currentAccount;
                            Common.CurrentUser.CurrentUserProfile= (ViewModelDBClass.UserProfileVM)currentAccount.UserProfile;

                            mainWindow.MainWindowContent = new View.Windows.AdsUC();
                        }
                        else
                        {
                            validationLoginToolTip.Content = string.Empty;
                            validationPasswordToolTip.Content = "Проверьте правильность введенного пароля";
                        }
                    }
                    else
                    {
                        validationPasswordToolTip.Content = string.Empty;
                        validationLoginToolTip.Content = "Пользователь с указанным логином не найден";
                    }
                });
            }
        }
        public Common.Commands.Command RegisterCommand
        {
            get
            {
                return new Common.Commands.Command(obj =>
                {
                    var mainWindow = (ViewModel.ContextMainWindow)Window.GetWindow((UserControl)obj).DataContext;
                    mainWindow.MainWindowContent = new View.Windows.RegisterUC();
                });
            }
        }
        public Common.Commands.Command MyCommand
        {
            get
            {
                return new Common.Commands.Command(obj =>
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
