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


namespace AdsBoard.ViewModel.Windows
{
    class ContextAdCreateUC
    {

        public ObservableCollection<string> AdImages { get; set; } = new ObservableCollection<string>();

        public ContextAdCreateUC()
        {

            System.Windows.Application.Current.MainWindow.Width = 800;
            System.Windows.Application.Current.MainWindow.Height = 800;
        }


        public Common.Commands.Command DeleteImage
        {
            get 
            {
                return new Common.Commands.Command(obj=>
                {
                    Viewbox vibox = ((Viewbox)obj);
                    Image img = (Image)vibox.Child;

                    //AdImages.Remove(img.Source.ToString());

                    System.Windows.Forms.MessageBox.Show(img.Source.ToString()+"\n"+ AdImages[0]);
                    //System.Windows.Forms.MessageBox.Show(AdImages[0]);
                });
            }
        }
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
                            foreach (var i in ofd.FileNames)
                            {
                                //new BitmapImage(new Uri("Creek.jpg", UriKind.Relative));
                                AdImages.Add(i.ToString());
                                /*
                                AdImages.Add(new Image
                                {
                                    Source = new BitmapImage
                                    {
                                        UriSource = new Uri(i.ToString())
                                    }
                                });*/
                                //System.Windows.Forms.MessageBox.Show(AdImages.First().Source.); 
                                //System.Windows.Forms.MessageBox.Show(Path.GetFileName(i.ToString()));
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

                });
            }
        }
        public Common.Commands.Command OkCommand
        {
            get
            {
                return new Common.Commands.Command(obj =>
                {
                    //System.Windows.Forms.MessageBox.Show(Common.CurrentUser.CurrentAccount.Login);
                });
            }
        }

    }
}
