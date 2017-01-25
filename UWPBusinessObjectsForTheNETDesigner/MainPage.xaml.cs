using Stimulsoft.Report;
using System;
using Windows.UI.Xaml.Controls;

namespace UWPBusinessObjectsForTheNETDesigner
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        #region Properties
        private static Data data = null;
        private static Data GetData()
        {
            if (data == null)
                data = new Data();

            return data;
        }
        #endregion

        #region Handlers
        async private void buttonSaveDictionaryFile_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            // Call the FileSavePicker to select the file which will be saved to the result.
            var picker = new Windows.Storage.Pickers.FileSavePicker();
            picker.FileTypeChoices.Add("Files Dictionary (*.dct)", new System.Collections.Generic.List<string>() { ".dct" });
            picker.SuggestedFileName = "ReportDictionary1";
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.ComputerFolder;

            // Calls the FileSavePicker.
            var storageFile = await picker.PickSaveFileAsync();
            if (storageFile != null)
            {
                StiReport report = new StiReport();
                // Register a custom business object in the report.
                report.RegBusinessObject("Data", "Data", GetData());
                // Synchronize the list of business objects defined in the report.
                report.Dictionary.SynchronizeBusinessObjects(3);
                // Save the data dictionary to a file .dct for further working with them in the designer.
                await report.Dictionary.SaveAsync(storageFile);
            }
        }

        async private void buttonSaveReport_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            // Call the FileSavePicker to select the file which will be saved to the result.
            var picker = new Windows.Storage.Pickers.FileSavePicker();
            picker.FileTypeChoices.Add("Files report (*.mrt)", new System.Collections.Generic.List<string>() { ".mrt" });
            picker.SuggestedFileName = "Report1";
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.ComputerFolder;

            // Calls the FileSavePicker.
            var storageFile = await picker.PickSaveFileAsync();
            if (storageFile != null)
            {
                StiReport report = new StiReport();
                // Register a custom business object in the report.
                report.RegBusinessObject("Data", "Data", GetData());
                // Synchronize the list of business objects defined in the report.
                report.Dictionary.SynchronizeBusinessObjects(3);

                await report.SaveAsync(storageFile);
            }
        }
        #endregion
    }
}