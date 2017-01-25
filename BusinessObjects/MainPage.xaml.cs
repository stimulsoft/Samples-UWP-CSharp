using Stimulsoft.Base.Localization;
using Stimulsoft.Helper.UWP.MessageBox;
using Stimulsoft.Report;
using System;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace BusinessObjects
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            #region Add Reports

            listBoxReports.Items.Add("Simple List");
            listBoxReports.Items.Add("Multi Column List");
            listBoxReports.Items.Add("Master Detail");
            listBoxReports.Items.Add("Two Masters On One Detail");
            listBoxReports.SelectedIndex = 0;

            #endregion
        }

        #region Handlers

        private async void buttonLoadReport_Click(object sender, RoutedEventArgs e)
        {
            viewerCotnrol.ShowProgressBar(StiLocalization.Get("DesignerFx", "LoadingReport"));

            if (listBoxReports.SelectedIndex != -1)
            {
                string fileName = null;
                switch (listBoxReports.SelectedIndex)
                {
                    case 0:
                        fileName = "StimulsoftResources\\Sample\\SimpleList.mrt";
                        break;

                    case 1:
                        fileName = "StimulsoftResources\\Sample\\MultiColumnList.mrt";
                        break;

                    case 2:
                        fileName = "StimulsoftResources\\Sample\\MasterDetail.mrt";
                        break;

                    case 3:
                        fileName = "StimulsoftResources\\Sample\\TwoMastersOnOneDetail.mrt";
                        break;
                }

                if (fileName != null)
                {
                    StorageFile storage = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFileAsync(fileName);

                    var report = new StiReport();
                    await report.LoadAsync(storage);

                    report.RegBusinessObject("Data", "Data", new Data());
                    await report.RenderAsync();

                    viewerCotnrol.Report = report;
                }
            }
            else
            {
                await StiMessageBox.ShowAsync("Report is not selected!", "Error", StiMessageBoxButton.Ok);
            }

            viewerCotnrol.HideProgressBar();
        }
        
        #endregion
    }
}
