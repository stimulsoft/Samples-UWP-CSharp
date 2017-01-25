using Stimulsoft.Report;
using Stimulsoft.Report.Viewer.UWP.Print;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace PrintingFromCode
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        #region Fields

        private StiPrintReport printReport;

        #endregion

        #region Methods.override

        protected override void OnNavigatedFrom(Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            if (printReport != null)
            {
                printReport.UnregisterForPrinting();
                printReport = null;
            }
        }

        #endregion

        #region Handlers

        async private void buttonPrint_Click(object sender, RoutedEventArgs e)
        {
            #region Get Report

            string fileName = null;
            switch (listBoxReports.SelectedIndex)
            {
                case 0:
                    fileName = "StimulsoftResources\\Reports\\SimpleList.mrt";
                    break;

                case 1:
                    fileName = "StimulsoftResources\\Reports\\MultiColumnList.mrt";
                    break;

                case 2:
                    fileName = "StimulsoftResources\\Reports\\MasterDetail.mrt";
                    break;

                case 3:
                    fileName = "StimulsoftResources\\Reports\\TwoMastersOnOneDetail.mrt";
                    break;
            }

            #endregion

            if (fileName == null) return;

            #region Load Report

            var storage = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFileAsync(fileName);
            var report = new StiReport();
            await report.LoadAsync(storage);

            report.RegBusinessObject("Data", "Data", new Data());
            await report.RenderAsync();

            #endregion

            #region Print report

            if (printReport != null)
            {
                printReport.UnregisterForPrinting();
                printReport = null;
            }

            if (printReport == null)
            {
                printReport = new StiPrintReport(report);
                printReport.RegisterForPrinting();
            }

            await printReport.PrintAsync();

            #endregion
        }

        #endregion
    }
}