using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace AdsBoard.ViewModel.Windows
{
    class ContextRegisterUC
    {
        public Model.Account registeredAccount { get; set; } = new Model.Account();
        public string retryPassword { get; set; }
        public Model.UserProfile registeredUserProfile { get; set; } = new Model.UserProfile();

        public ContextRegisterUC()
        {
            System.Windows.Application.Current.MainWindow.Width = 700;
            System.Windows.Application.Current.MainWindow.Height = 300;
        }

        //костыли
        public Common.Commands.Command UpdatePasswordFild
        {
            get
            {
                return new Common.Commands.Command(obj =>
                {

                    TextBox passwordTextBox = (TextBox)obj;

                    passwordTextBox.Text = passwordTextBox.Text;

                });
            }
        }
        public Common.Commands.Command UpdateRetryPasswordFild
        {
            get
            {
                return new Common.Commands.Command(obj =>
                {

                    TextBox retryPasswordTextBox = (TextBox)obj;

                    retryPasswordTextBox.Text = retryPasswordTextBox.Text;

                });
            }
        }

        public Common.Commands.Command Cancel
        {
            get
            {
                return new Common.Commands.Command(obj =>
                {
                    var mainWindow = (ViewModel.ContextMainWindow)Window.GetWindow((UserControl)obj).DataContext;
                    mainWindow.MainWindowContent = new View.Windows.EnterUC();
                });
            }
        }
        public Common.Commands.Command Ok
        {
            get
            {
                return new Common.Commands.Command(obj=> 
                {
                    var mainWindow = (ViewModel.ContextMainWindow)Window.GetWindow((UserControl)obj).DataContext;

                    var accounts = Model.DBModel.GetDBModel().GetAccounts();

                    //Если такой логин отсутствует в базе
                    if (!accounts.Any(a=>a.Login == registeredAccount.Login))
                    {
                        //если пароль повторен правильно
                        if (registeredAccount.Password == retryPassword)
                        {
                            if (accounts.Where(a => a.UserProfile.EMail == registeredUserProfile.EMail).Count() == 0)
                            {
                                registeredAccount.UserProfile = registeredUserProfile;

                                accounts.Add(registeredAccount);

                                Model.DBModel.GetDBModel().Entry(registeredAccount).State = System.Data.Entity.EntityState.Added;

                                Model.DBModel.GetDBModel().SaveChanges();

                                mainWindow.MainWindowContent = new View.Windows.EnterUC();
                            }
                            else
                            {
                                MessageBox.Show("Указанная почта уже используется!");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Пароль повторен не верно!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Аккаунт с таким логином уже есть!");
                    }

                });
            }
        }
    }

}



