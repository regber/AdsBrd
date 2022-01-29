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
    }
}
