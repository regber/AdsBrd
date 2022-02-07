using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;

namespace AdsBoard.ViewModel.Windows
{
    class ContextAdsUC
    {
        public ObservableCollection<Model.Ad> AdsList { get; set; } = new ObservableCollection<Model.Ad>();
        public ObservableCollection<string> producers { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> producerHintList { get; set; } = new ObservableCollection<string>();

        public ObservableCollection<string> carModels { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> carModelsHintList { get; set; } = new ObservableCollection<string>();

        public ObservableCollection<string> TransmissionTypeList { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> FuelTypeList { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> DriverList { get; set; } = new ObservableCollection<string>();

        public AdFilter AdFilter { get; set; } = new AdFilter();

        public ContextAdsUC()
        {
            System.Windows.Application.Current.MainWindow.Width = 900;
            System.Windows.Application.Current.MainWindow.Height = 600;

            foreach (var ad in Model.DBModel.GetDBModel().GetAds())
            {
                AdsList.Add(ad);
            }


            foreach (var producer in AdsList.GroupBy(x => x.Producer).Select(y => y.Key).ToList())
            {
                producers.Add(producer);
            }

            foreach (var carModel in AdsList.GroupBy(c => c.Model).ToList())
            {
                carModels.Add(carModel.Key);
            }

            foreach (var trans in AdsList.GroupBy(c => c.Transmission).Select(s => s.Key).ToList())
            {
                TransmissionTypeList.Add(trans);
            }
            foreach (var fuel in AdsList.GroupBy(c => c.FuelType).Select(s => s.Key).ToList())
            {
                FuelTypeList.Add(fuel);
            }
            foreach (var driver in AdsList.GroupBy(c => c.Drive).Select(s => s.Key).ToList())
            {
                DriverList.Add(driver);
            }
        }

        public Common.Commands.Command ProducerHintListChange
        {
            get
            {
                return new Common.Commands.Command(obj =>
                {
                    var text = (string)obj;

                    producerHintList.Clear();

                    if (text.Length > 0)
                    {
                        foreach (var producer in producers.Where(str => str != null && str.StartsWith(text.Substring(0, 1).ToUpper() + text.Substring(1)) || str.StartsWith(text.Substring(0, 1).ToLower() + text.Substring(1))))
                        {
                            producerHintList.Add(producer);
                        }
                    }
                    else
                    {
                        foreach (var producer in producers.Where(str => str != null))
                        {
                            producerHintList.Add(producer);
                        }
                    }

                });
            }
        }

        public Common.Commands.CommandMultParam ProducerHintListSelected
        {
            get
            {
                return new Common.Commands.CommandMultParam(obj =>
                {

                    var obje = (object[])obj;
                    TextBox tb = (TextBox)obje[0];
                    string str = (string)obje[1];

                    if (!string.IsNullOrEmpty(str))
                    {
                        tb.Text = str;
                    }

                    producerHintList.Clear();
                });
            }
        }

        public Common.Commands.CommandMultParam CarModelHintListChange
        {
            get
            {
                return new Common.Commands.CommandMultParam(obj =>
                {
                    var obje = (object[])obj;
                    var carModelTitle = (string)obje[0];
                    var producerTitle = (string)obje[1];

                    carModelsHintList.Clear();

                    foreach (IGrouping<string, Model.Ad> ad in AdsList.GroupBy(c => c.Model).ToList())
                    {
                        if (carModelTitle.Length > 0)
                        {
                            foreach (var carModel in ad.Where(str => str != null && str.Producer == producerTitle && str.Model.StartsWith(carModelTitle.Substring(0, 1).ToUpper() + carModelTitle.Substring(1)) || str.Model.StartsWith(carModelTitle.Substring(0, 1).ToLower() + carModelTitle.Substring(1))).GroupBy(c => c.Model).ToList())
                            {
                                carModelsHintList.Add(carModel.Key);
                            }
                        }
                        else
                        {
                            foreach (var carModel in ad.Where(str => str != null && str.Producer == producerTitle).GroupBy(c => c.Model).ToList())
                            {
                                carModelsHintList.Add(carModel.Key);
                            }
                        }

                    }

                });
            }
        }

        public Common.Commands.CommandMultParam CarModelHintListSelected
        {
            get
            {
                return new Common.Commands.CommandMultParam(obj =>
                {

                    var obje = (object[])obj;
                    TextBox tb = (TextBox)obje[0];
                    string str = (string)obje[1];

                    if (!string.IsNullOrEmpty(str))
                    {
                        tb.Text = str;
                    }

                    carModelsHintList.Clear();
                });
            }
        }

        public Common.Commands.Command SearchCommand
        {
            get
            {
                return new Common.Commands.Command(obj =>
                {
                    //Запрос через SQL
                    List<object> param = new List<object>();
                    List<Model.Ad> ads = new List<Model.Ad>();

                    //Выгрузка информации связующей таблицы
                    string adsIdQuery = "SELECT Ads.Id FROM Ads";
                    var adsIdList=Model.DBModel.GetDBModel().Database.SqlQuery<int>(adsIdQuery).ToList();

                    string mainImageIdQuery = "SELECT Ads.MainImage_Id FROM Ads";
                    var mainImageIdList = Model.DBModel.GetDBModel().Database.SqlQuery<int>(mainImageIdQuery).ToList();

                    Dictionary<int,int> dictMainImageId = new System.Collections.Generic.Dictionary<int,int>();

                    for(int i=0;i< adsIdList.Count();i++ )
                    {
                        dictMainImageId.Add(adsIdList[i], mainImageIdList[i]);
                    }

                    string sqlQuery = "SELECT * FROM Ads";

                    //Формирование sql запроса
                    //check producer
                    if (!string.IsNullOrEmpty(AdFilter.Producer))
                    {
                        if (param.Count == 0)
                            sqlQuery += " WHERE Producer = @Param1 ";
                        else
                            sqlQuery += " AND Producer = @Param1 ";

                        param.Add(new System.Data.SqlClient.SqlParameter("@Param1", AdFilter.Producer));
                    }

                    //check model
                    if (!string.IsNullOrEmpty(AdFilter.Model))
                    {
                        if (param.Count == 0)
                            sqlQuery += " WHERE Model = @Param2 ";
                        else
                            sqlQuery += " AND Model = @Param2 ";

                        param.Add(new System.Data.SqlClient.SqlParameter("@Param2", AdFilter.Model));
                    }

                    //check Price
                    if (AdFilter.LowerPrice!=0 || AdFilter.TopPrice != 0)
                    {
                        if (param.Count == 0)
                            sqlQuery += " WHERE Price BETWEEN @Param3 AND @Param4 ";
                        else
                            sqlQuery += " AND Price BETWEEN @Param3 AND @Param4 ";

                        param.Add(new System.Data.SqlClient.SqlParameter("@Param3", AdFilter.LowerPrice));
                        param.Add(new System.Data.SqlClient.SqlParameter("@Param4", AdFilter.TopPrice));
                    }


                    //check ReleaseYear 
                    if (AdFilter.LowerReleaseYear != 0 || AdFilter.TopReleaseYear != 0)
                    {
                        if (param.Count == 0)
                            sqlQuery += " WHERE ReleaseYear BETWEEN @Param5 AND @Param6 ";
                        else
                            sqlQuery += " AND ReleaseYear BETWEEN @Param5 AND @Param6 ";

                        param.Add(new System.Data.SqlClient.SqlParameter("@Param5", AdFilter.LowerReleaseYear));
                        param.Add(new System.Data.SqlClient.SqlParameter("@Param6", AdFilter.TopReleaseYear));
                    }


                    //check EngineVolume 
                    if (AdFilter.LowerEngineVolume != 0 || AdFilter.TopEngineVolume != 0)
                    {
                        if (param.Count == 0)
                            sqlQuery += " WHERE EngineVolume BETWEEN @Param7 AND @Param8 ";
                        else
                            sqlQuery += " AND EngineVolume BETWEEN @Param7 AND @Param8 ";

                        param.Add(new System.Data.SqlClient.SqlParameter("@Param7", AdFilter.LowerEngineVolume));
                        param.Add(new System.Data.SqlClient.SqlParameter("@Param8", AdFilter.TopEngineVolume));
                    }

                    //check transmission
                    if (!string.IsNullOrEmpty(AdFilter.Transmission))
                    {
                        if (param.Count == 0)
                            sqlQuery += " WHERE Transmission = @Param9 ";
                        else
                            sqlQuery += " AND Transmission = @Param9 ";

                        param.Add(new System.Data.SqlClient.SqlParameter("@Param9", AdFilter.Transmission));
                    }

                    //check FuelType
                    if (!string.IsNullOrEmpty(AdFilter.FuelType))
                    {
                        if (param.Count == 0)
                            sqlQuery += " WHERE FuelType = @Param10 ";
                        else
                            sqlQuery += " AND FuelType = @Param10 ";

                        param.Add(new System.Data.SqlClient.SqlParameter("@Param10", AdFilter.FuelType));
                    }

                    //check Drive
                    if (!string.IsNullOrEmpty(AdFilter.Drive))
                    {
                        if (param.Count == 0)
                            sqlQuery += " WHERE Drive = @Param11 ";
                        else
                            sqlQuery += " AND Drive = @Param11 ";

                        param.Add(new System.Data.SqlClient.SqlParameter("@Param11", AdFilter.Drive));
                    }
                    

                    if (param.Count > 0)
                        ads = Model.DBModel.GetDBModel().Database.SqlQuery<Model.Ad>(sqlQuery, param.ToArray()).ToList();
                    else
                        ads = Model.DBModel.GetDBModel().Database.SqlQuery<Model.Ad>(sqlQuery).ToList();

                    
                    var images = Model.DBModel.GetDBModel().GetAdsImages();
                    foreach (var ad in ads)
                    {
                        ad.MainImage = images.First(i => i.Id == dictMainImageId[ad.Id]);
                        ad.Images = images.Where(i => i.Ad.Id == ad.Id).ToList();
                    }

                    AdsList.Clear();
                    foreach (var ad in ads)
                    {
                        AdsList.Add(ad);
                    }
                });
            }
        }

        public Common.Commands.Command NewCommand
        {
            get
            {
                return new Common.Commands.Command(obj =>
                {
                    var mainWindow = (ViewModel.ContextMainWindow)Window.GetWindow((UserControl)obj).DataContext;
                    mainWindow.MainWindowContent = new View.Windows.AdCreateUC();
                });
            }
        }

        public Common.Commands.Command MyAdsCommand
        {
            get
            {
                return new Common.Commands.Command(obj =>
                {
                    var mainWindow = (ViewModel.ContextMainWindow)Window.GetWindow((UserControl)obj).DataContext;
                    mainWindow.MainWindowContent = new View.Windows.MyAdsUC();
                });
            }
        }
    }

    public class AdFilter
    {
        public int LowerPrice { get; set; }
        public int TopPrice { get; set; }


        public string Producer { get; set; }
        public string Model { get; set; }

        public double LowerEngineVolume { get; set; }
        public double TopEngineVolume { get; set; }

        public int LowerReleaseYear { get; set; }
        public int TopReleaseYear { get; set; }

        public string Drive { get; set; }
        public string Transmission { get; set; }
        public string FuelType { get; set; }
    }
}
