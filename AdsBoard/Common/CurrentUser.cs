using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdsBoard.Common
{
    class CurrentUser
    {
        public static ViewModel.ViewModelDBClass.AccountVM CurrentAccount { get; set; }
        public static ViewModel.ViewModelDBClass.UserProfileVM CurrentUserProfile { get; set; }
    }
}
