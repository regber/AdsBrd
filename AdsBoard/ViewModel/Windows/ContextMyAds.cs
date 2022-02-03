using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace AdsBoard.ViewModel.Windows
{
    class ContextMyAds
    {

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
