using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdsBoard.Common
{
    class CurrentUser
    {
        public static Model.Account CurrentAccount { get; set; } = new Model.Account();
        public static Model.UserProfile CurrentUserProfile { get; set; } = new Model.UserProfile();
    }
}
