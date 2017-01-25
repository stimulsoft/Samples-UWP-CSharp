using Stimulsoft.Helper.UWP.Data;
using Stimulsoft.Report;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace UsingDataSetAsBusinessObject
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            if (isFirst)
            {
                isFirst = false;
                this.Loaded += MainPage_Loaded;
            }
        }

        #region Fields
        private static bool isFirst = true;
        #endregion

        #region Handlers
        async private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            StorageFile storageFileData = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFileAsync("Items\\DemoNet.data");
            StorageFile storageFileReport = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFileAsync("Items\\MasterDetail.mrt");

            DataSet dataSet = new DataSet();
            await dataSet.LoadAsync(storageFileData);

            StiReport report = new StiReport();
            await report.LoadAsync(storageFileReport);

            report.RegBusinessObject("Demo", dataSet);
            report.Dictionary.SynchronizeBusinessObjects(3);
            await report.RenderAsync();

            viewerControl.Report = report;
        }
        #endregion
    }
}