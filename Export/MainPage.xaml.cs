using Stimulsoft.Report;
using Stimulsoft.Report.Export;
using System;
using System.Collections.Generic;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Export
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += BlankPage_Loaded;
        }

        #region Methods
        private void AddExportinMenu(StiExportService service)
        {
            var item = new ListBoxItem
            {
                Content = service.ExportNameInMenu.Replace("...", string.Empty),
                Tag = service
            };
            listBoxExports.Items.Add(item);
        }

        private void ShowProgress()
        {
            progress.Visibility = Windows.UI.Xaml.Visibility.Visible;
            progress.IsActive = true;

            listBoxExports.IsEnabled = false;
            listBoxReports.IsEnabled = false;
            buttonExport.IsEnabled = false;
        }

        private void HideProgress()
        {
            progress.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            progress.IsActive = false;

            listBoxExports.IsEnabled = true;
            listBoxReports.IsEnabled = true;
            buttonExport.IsEnabled = true;
        }
        #endregion

        #region Handlers
        private void buttonClose_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Windows.UI.Xaml.Window.Current.Close();
        }

        private void BlankPage_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            AddExportinMenu(new StiPdfExportService());
            AddExportinMenu(new StiCsvExportService());
            AddExportinMenu(new StiDbfExportService());
            AddExportinMenu(new StiExcelExportService());
            AddExportinMenu(new StiExcel2007ExportService());
            AddExportinMenu(new StiExcelXmlExportService());
            AddExportinMenu(new StiHtmlExportService());
            AddExportinMenu(new StiPpt2007ExportService());
            AddExportinMenu(new StiTxtExportService());
            AddExportinMenu(new StiWord2007ExportService());

            listBoxExports.SelectedIndex = 1;
        }

        async private void buttonExport_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (listBoxReports.SelectedIndex == -1 || listBoxExports.SelectedIndex == -1)
                return;

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

            ShowProgress();

            #region Load Report
            StorageFile storage = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFileAsync(fileName);

            StiReport report = new StiReport();
            await report.LoadAsync(storage);

            report.RegBusinessObject("Data", "Data", new Data());
            await report.RenderAsync();
            #endregion

            #region Create StorageFile
            ListBoxItem item = listBoxExports.SelectedItem as ListBoxItem;
            StiExportService service = item.Tag as StiExportService;
            StorageFile file = await service.GetFileStream(report.ReportName);
            #endregion

            #region Export
            if (file != null)
            {
                if (listBoxExports.SelectedIndex == 0)
                {
                    await ((StiPdfExportService)service).ExportPdfAsync(report, file, new StiPdfExportSettings());
                }
                else if (listBoxExports.SelectedIndex == 1)
                {
                    await ((StiCsvExportService)service).ExportCsvAsync(report, file, new StiCsvExportSettings());
                }
                else if (listBoxExports.SelectedIndex == 2)
                {
                    await ((StiDbfExportService)service).ExportDbfAsync(report, file, new StiDbfExportSettings());
                }
                else if (listBoxExports.SelectedIndex == 3)
                {
                    await ((StiExcelExportService)service).ExportExcelAsync(report, file, new StiExcelExportSettings());
                }
                else if (listBoxExports.SelectedIndex == 4)
                {
                    await ((StiExcel2007ExportService)service).ExportExcelAsync(report, file, new StiExcel2007ExportSettings());
                }
                else if (listBoxExports.SelectedIndex == 5)
                {
                    await ((StiExcelXmlExportService)service).ExportExcelAsync(report, file, new StiExcelXmlExportSettings());
                }
                else if (listBoxExports.SelectedIndex == 6)
                {
                    await ((StiHtmlExportService)service).ExportHtmlAsync(report, file, new StiHtmlExportSettings());
                }
                else if (listBoxExports.SelectedIndex == 7)
                {
                    await ((StiPpt2007ExportService)service).ExportPowerPointAsync(report, file, new StiPpt2007ExportSettings());
                }
                else if (listBoxExports.SelectedIndex == 8)
                {
                    await ((StiTxtExportService)service).ExportTxtAsync(report, file, new StiTxtExportSettings());
                }
                else if (listBoxExports.SelectedIndex == 9)
                {
                    await ((StiWord2007ExportService)service).ExportWordAsync(report, file, new StiWord2007ExportSettings());
                }
            }
            #endregion

            report.Dispose();

            HideProgress();
        }
        #endregion
    }
}
