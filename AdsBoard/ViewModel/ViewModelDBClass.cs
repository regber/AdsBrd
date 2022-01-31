using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace AdsBoard.ViewModel
{
    namespace ViewModelDBClass
    {
        class AccountVM : Model.Account, IDataErrorInfo
        {
            public string this[string columnName]
            {
                get
                {
                    string error = String.Empty;
                    switch (columnName)
                    {
                        case "Login":
                            if (Login == string.Empty)
                            {
                                error = "Поле не должно быть пустым";
                            }
                            break;
                        case "Password":
                            if (Password == string.Empty)
                            {
                                error = "Поле не должно быть пустым";
                            }
                            break;
                    }
                    return error;
                }
            }
            public string Error
            {
                get { throw new NotImplementedException(); }
            }
        }

        class UserProfileVM : Model.UserProfile, IDataErrorInfo
        {

            public string this[string columnName]
            {
                get
                {
                    Regex regex = new Regex(@"\w*@\w*\.\w{1,3}$");

                    string error = String.Empty;
                    switch (columnName)
                    {
                        case "FirstName":
                            if (FirstName == string.Empty)
                            {
                                error = "Поле не может быть пустым";
                            }
                            break;
                        case "SecondName":
                            if (SecondName == string.Empty)
                            {
                                error = "Поле не может быть пустым";
                            }
                            break;
                        case "Birthday":
                            if (Birthday == null)
                            {
                                error = "Поле не может быть пустым";
                            }
                            if(Birthday.Date > DateTime.Now)
                            {
                                error = "Дата рождения должна быть в прошлом";
                            }

                            break;
                        case "PhoneNumber":
                            if (PhoneNumber == string.Empty || PhoneNumber==null)
                            {
                                error = "Поле не может быть пустым";
                            }
                            else if (PhoneNumber.Any(c => Char.IsLetter(c)))
                            {
                                error = "В номере телефона не могут быть указаны буквы";
                            }
                            break;
                        case "EMail":
                            if (EMail == string.Empty || EMail == null)
                            {
                                error = "Поле не может быть пустым";
                            }
                            else if (!regex.Match(EMail).Success)
                            {
                                error = "Указан не верный адрес электронной почты";
                            }
                            break;
                    }
                    return error;
                }

            }

            public string Error => throw new NotImplementedException();
        }

        class AdVM : Model.Ad, IDataErrorInfo
        {

            public string this[string columnName]
            {
                get
                {
                    string error = String.Empty;
                    switch (columnName)
                    {
                        case nameof(Header):
                            if(Header==string.Empty)
                            {
                                error = "Поле не может быть пустым";
                            }
                            break;
                        case nameof(Text):
                            if(Text == string.Empty)
                            {
                                error = "Поле не может быть пустым";
                            }
                            break;
                        case nameof(Price):
                            if (Price <=0)
                            {
                                error = "Цена должна быть больше нуля";
                            }
                            break;
                    }
                    return error;
                }
            }

            public string Error => throw new NotImplementedException();
        }
    }
}
