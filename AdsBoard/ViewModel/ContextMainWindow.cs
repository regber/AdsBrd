using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows;


namespace AdsBoard.ViewModel
{
    class ContextMainWindow:Common.Notify
    {
        private System.Windows.Controls.UserControl _MainWindowContent;
        public System.Windows.Controls.UserControl MainWindowContent 
        { 
            get
            {
                return _MainWindowContent;
            }
            set
            {
                _MainWindowContent = value;
                OnPropertyChanged(nameof(MainWindowContent));
            }
        }

        public ContextMainWindow()
        {
            //MainWindowContent = new View.Windows.EnterUC();
            MainWindowContent = new View.Windows.AdsUC();
        }
    }
}
