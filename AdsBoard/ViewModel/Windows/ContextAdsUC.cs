using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AdsBoard.ViewModel.Windows
{
    class ContextAdsUC
    {
        public List<Model.Ad> adsList { get; set; }
        public List<string> salers { get; set; }
        public ContextAdsUC()
        {
            adsList = Model.DBModel.GetDBModel().GetAds();
            salers= adsList.GroupBy(x => x.Account.UserProfile.FirstName).Select(y => y.Key).ToList();
        }

        public Common.Commands.Command FilterCommand
        {
            get
            {
                return new Common.Commands.Command(obj =>
                {
                    var mainWindow = (ViewModel.ContextMainWindow)Window.GetWindow((UserControl)obj).DataContext;
                    mainWindow.MainWindowContent = new View.Windows.AdCreateUC();
                });
            }
        }

        public Common.Commands.Command NewCommand
        {
            get
            {
                return new Common.Commands.Command(obj=>
                {
                    var mainWindow = (ViewModel.ContextMainWindow)Window.GetWindow((UserControl)obj).DataContext;
                    mainWindow.MainWindowContent = new View.Windows.AdCreateUC();
                });
            }
        }

        public Common.Commands.Command MyAdsCommand
        {
            get
            {
                return new Common.Commands.Command(obj =>
                {
                    var mainWindow = (ViewModel.ContextMainWindow)Window.GetWindow((UserControl)obj).DataContext;
                    mainWindow.MainWindowContent = new View.Windows.MyAdsUC();
                });
            }
        }
    }
}
