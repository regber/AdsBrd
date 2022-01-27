using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

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
                            if (Login=="")
                            {
                                error = "Поле не должно быть пустым";
                            }
                            break;
                        case "Password":
                            if (Password == "")
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


            //public AccountVM Account { get; set; }

            public string this[string columnName]
            {
                get
                {
                    string error = String.Empty;
                    switch (columnName)
                    {
                        case "FirstName":
                            break;
                        case "SecondName":
                            break;
                        case "Birthday":
                            break;
                        case "PhoneNumber":
                            break;
                        case "EMail":
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
                        case "FirstName":
                            break;
                        case "SecondName":
                            break;
                        case "Birthday":
                            break;
                        case "PhoneNumber":
                            break;
                        case "EMail":
                            break;
                    }
                    return error;
                }

            }

            public string Error => throw new NotImplementedException();
            /*
            public int Id { get; set; }

            public string Header { get; set; }
            public string Text { get; set; }

            public AccountVM Account { get; set; }

            public Image MainImage { get; set; }
            public ICollection<Image> Images { get; set; }*/

        }

        class ImageVM : Model.Image, IDataErrorInfo
        {
            public string this[string columnName]
            {
                get
                {
                    string error = String.Empty;
                    switch (columnName)
                    {
                        case "FirstName":
                            break;
                        case "SecondName":
                            break;
                        case "Birthday":
                            break;
                        case "PhoneNumber":
                            break;
                        case "EMail":
                            break;
                    }
                    return error;
                }

            }

            public string Error => throw new NotImplementedException();
            /*
            public int Id { get; set; }

            public string ImagePath { get; set; }

            public Ad Ad { get; set; }*/

        }
    }
}
