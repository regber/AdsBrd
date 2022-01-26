using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AdsBoard
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            /*
            using (Model.DBModel db = new Model.DBModel())
            {
                var accounts = db.Accounts.Include("UserProfile").ToList();
                foreach (var a in accounts)
                {
                    MessageBox.Show(((ViewModel.AccountCt)a).UserProfile.FirstName);
                }
            }*/




            /*
            Model.Account acc = new Model.Account();
            acc.Password = "1241251251";

            MessageBox.Show(acc.Password);*/
        }
    }
}
