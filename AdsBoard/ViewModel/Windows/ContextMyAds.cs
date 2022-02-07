using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Collections.ObjectModel;

namespace AdsBoard.ViewModel.Windows
{
    class ContextMyAds
    {

        public ObservableCollection<Model.Ad> AdsList { get; set; } = new ObservableCollection<Model.Ad>();


        public AdFilter AdFilter { get; set; } = new AdFilter();

        public ContextMyAds()
        {
            System.Windows.Application.Current.MainWindow.Width = 900;
            System.Windows.Application.Current.MainWindow.Height = 600;

            foreach (var ad in Model.DBModel.GetDBModel().GetAds().Where(ad=>ad.Account.Login==Common.CurrentUser.CurrentAccount.Login))
            {
                AdsList.Add(ad);
            }
        }



        public Common.Commands.Command DeleteAd
        {
            get
            {
                return new Common.Commands.Command(obj =>
                {
                    var lbi = (ListBoxItem)obj;
                    Model.Ad ad = (Model.Ad)lbi.Content;

                    //Удаляем связанные изображения
                    Model.DBModel.GetDBModel().Images.RemoveRange(ad.Images);
                    Model.DBModel.GetDBModel().SaveChanges();

                    //а затем удаляем само объявление
                    Model.DBModel.GetDBModel().Ads.Remove(ad);
                    Model.DBModel.GetDBModel().SaveChanges();

                    AdsList.Clear();
                    foreach (var a in Model.DBModel.GetDBModel().GetAds().Where(a => a.Account.Login == Common.CurrentUser.CurrentAccount.Login))
                    {
                        AdsList.Add(a);
                    }

                });
            }
        }



        public Common.Commands.Command Back
        {
            get
            {
                return new Common.Commands.Command(obj =>
                {
                    var mainWindow = (ViewModel.ContextMainWindow)Window.GetWindow((UserControl)obj).DataContext;
                    mainWindow.MainWindowContent = new View.Windows.AdsUC();
                });
            }
        }
        public Common.Commands.Command CreateNewAd
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
    }
}
