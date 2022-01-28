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
        public ViewModelDBClass.AccountVM registeredAccount { get; set; } = new ViewModelDBClass.AccountVM();
        public string retryPassword { get; set; }
        public ViewModelDBClass.UserProfileVM registeredUserProfile { get; set; } = new ViewModelDBClass.UserProfileVM();

        public ContextRegisterUC()
        {

        }

        public void CheckCoincidencePassword()
        {

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
                    MessageBox.Show(retryPassword);
                });
            }
        }
    }

}



