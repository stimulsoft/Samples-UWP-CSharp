using Stimulsoft.Report;
using System;
using System.Collections.Generic;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace EditableReport
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += BlankPage_Loaded;

            StiOptions.Viewer.HighlightShowBorderColor = Colors.Orange;
            StiOptions.Viewer.HighlightActiveBorderColor = Colors.Green;

            StiOptions.Viewer.Elements.PageDeleteButtonVisibility = Visibility.Collapsed;
            StiOptions.Viewer.Elements.PageNewButtonVisibility = Visibility.Collapsed;
            StiOptions.Viewer.Elements.ReportOpenButtonVisibility = Visibility.Collapsed;
        }

        #region Handlers
        async private void btSave_Click(object sender, RoutedEventArgs e)
        {
            FileSavePicker savePicker = new FileSavePicker();
            savePicker.FileTypeChoices.Add("*.xml", new List<string> { ".xml" });
            savePicker.SuggestedFileName = "file";
            savePicker.SuggestedStartLocation = PickerLocationId.ComputerFolder;

            StorageFile storageFile = await savePicker.PickSaveFileAsync();
            if (storageFile != null)
            {
                viewerCotnrol.Report.SaveEditableFieldsAsync(storageFile);
            }
        }

        async private void btLoad_Click(object sender, RoutedEventArgs e)
        {
            var openPicker = new FileOpenPicker();
            openPicker.FileTypeFilter.Add(".xml");
            openPicker.SuggestedStartLocation = PickerLocationId.ComputerFolder;

            StorageFile storageFile = await openPicker.PickSingleFileAsync();
            if (storageFile != null)
            {
                StiReport report = viewerCotnrol.Report;
                viewerCotnrol.Report = null;

                await report.LoadEditableFieldsAsync(storageFile);

                viewerCotnrol.Report = report;
            }
        }

        async private void BlankPage_Loaded(object sender, RoutedEventArgs e)
        {
            StorageFile storage = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFileAsync("StimulsoftResources\\Reports\\EditableReport.mdc");

            StiReport report = new StiReport();
            await report.LoadDocumentAsync(storage);

            viewerCotnrol.Report = report;
            viewerCotnrol.OpenEditor();
        }
        #endregion
    }
}