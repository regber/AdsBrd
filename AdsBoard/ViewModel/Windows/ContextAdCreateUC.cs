using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Forms;
using System.IO;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Documents;


namespace AdsBoard.ViewModel.Windows
{
    class ContextAdCreateUC : Common.Notify
    {
        public Model.Ad Ad { get; set; } = new Model.Ad() { ReleaseYear=1900 };

        public ObservableCollection<string> AdImages { get; set; } = new ObservableCollection<string>();

        public List<string> DriverList { get; set; } = new List<string> { "front-wheel drive", "rear-wheel drive", "four-wheel drive" };
        public List<string> FuelTypeList { get; set; } = new List<string> { "gasoline", "diesel" };
        public List<string> TransmissionTypeList { get; set; } = new List<string> { "automatic", "manual" };

        private string _SelectedMainAdImage;
        public string SelectedMainAdImage
        {
            get
            {
                return _SelectedMainAdImage;
            }
            set
            {
                _SelectedMainAdImage = value;
                OnPropertyChanged(nameof(SelectedMainAdImage));
            }

        }

        public ContextAdCreateUC()
        {
            //Если добавлять редактирование объявления сделать загрузку его параметров тут!!!!

            System.Windows.Application.Current.MainWindow.Width = 800;
            System.Windows.Application.Current.MainWindow.Height = 800;
        }


        public Common.Commands.Command DeleteImage
        {
            get
            {
                return new Common.Commands.Command(obj =>
                {
                    Viewbox viewBox = ((Viewbox)obj);

                    Image img = (Image)viewBox.Child;

                    string imgPath = img.Source.ToString().Remove(0, 8).Replace('/', '\\');

                    AdImages.Remove(imgPath);

                });
            }
        }
        /*private childItem FindVisualChild<childItem>(DependencyObject obj) where childItem : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is childItem)
                {
                    return (childItem)child;
                }
                else
                {
                    childItem childOfChild = FindVisualChild<childItem>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }*/
        public Common.Commands.Command AddImagesCommand
        {
            get
            {
                return new Common.Commands.Command(obj =>
                {

                    using (OpenFileDialog ofd = new OpenFileDialog())
                    {
                        ofd.Title = "Open Image";
                        ofd.Filter = "Image Files (*.bmp;*.jpg;*.jpeg,*.png)|*.BMP;*.JPG;*.JPEG;*.PNG";
                        ofd.Multiselect = true;

                        if (ofd.ShowDialog() == DialogResult.OK)
                        {
                            foreach (var img in ofd.FileNames)
                            {

                                if (!AdImages.Contains(img))
                                {
                                    //Add images
                                    AdImages.Add(img.ToString());
                                }
                            }
                        }
                    }
                });
            }
        }
        public Common.Commands.Command CancelCommand
        {
            get
            {
                return new Common.Commands.Command(obj =>
                {
                    ((ContextMainWindow)System.Windows.Application.Current.MainWindow.DataContext).MainWindowContent = new View.Windows.MyAdsUC();
                });
            }
        }
        public Common.Commands.Command OkCommand
        {
            get
            {
                return new Common.Commands.Command(obj =>
                {

                    //Create directory if not exist
                    Directory.CreateDirectory("AdsImages");

                    List<Model.Image> imageList = new List<Model.Image>();
                    Model.Image mainImage = new Model.Image();

                    Ad.Account = Common.CurrentUser.CurrentAccount;
                    var newAd = Ad;

                    //Проверяем что бы все поля объекта Ad были заполнены исключая поля валидации("Item" и "Error") и Id
                    if (Ad.GetType().GetProperties().Where(p => p.PropertyType == typeof(string))
                                                    .Where(p=>p.Name != "Item" && p.Name != "Error")
                                                    .Any(s => string.IsNullOrEmpty(s.GetValue(Ad)?.ToString())) 
                        || 
                        Ad.GetType().GetProperties()
                                                    .Where(p => p.PropertyType == typeof(int) || p.PropertyType == typeof(double))
                                                    .Where(p => p.Name != nameof(Ad.Id))
                                                    .Any(v=>double.Parse(v.GetValue(Ad).ToString())==0))
                    {

                        System.Windows.Forms.MessageBox.Show("Не все поля заполнены");
                    }
                    else
                    {
                        //add ad
                        Model.DBModel.GetDBModel().Ads.Add(newAd);
                        Model.DBModel.GetDBModel().SaveChanges();

                        //copying image file to directory AdsImages with owerwrite
                        string newImgPath = Directory.GetCurrentDirectory() + @"\AdsImages\" + Common.CurrentUser.CurrentAccount.Id + "_" + Common.CurrentUser.CurrentAccount.Login + "_" + newAd.Id + "_";
                        foreach (var imgPath in AdImages)
                        {
                            File.Copy(imgPath, newImgPath + Path.GetFileName(imgPath.ToString()), true);
                            imageList.Add(new Model.Image { ImagePath = newImgPath + Path.GetFileName(imgPath.ToString()), Ad = newAd });
                        }

                        //add images
                        Model.DBModel.GetDBModel().Images.AddRange(imageList);
                        Model.DBModel.GetDBModel().SaveChanges();


                        //Set mainImage
                        newAd.MainImage = imageList.Where(i => i.ImagePath == newImgPath + Path.GetFileName(SelectedMainAdImage)).FirstOrDefault();
                        Model.DBModel.GetDBModel().SaveChanges();

                        ((ContextMainWindow)System.Windows.Application.Current.MainWindow.DataContext).MainWindowContent = new View.Windows.AdsUC();
                    }

                });
            }
        }

    }
}
