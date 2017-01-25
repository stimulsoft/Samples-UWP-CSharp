using Stimulsoft.Helper.UWP.MessageBox;
using Stimulsoft.Report;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml.Controls;

namespace ReportManager
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += BlankPage_Loaded;
        }

        #region Fields
        private static List<string> listFiles = null;
        #endregion

        #region Methods
        async private Task WriteListFilesAsync()
        {
            StorageFolder folder = await ApplicationData.Current.TemporaryFolder.CreateFolderAsync("Reports", CreationCollisionOption.OpenIfExists);
            StorageFile file = await folder.CreateFileAsync("reports.txt", CreationCollisionOption.OpenIfExists);

            await FileIO.WriteLinesAsync(file, listFiles);
        }
        #endregion

        #region Handlers
        async private void BlankPage_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (listFiles == null)
            {
                StorageFolder folder = await ApplicationData.Current.TemporaryFolder.CreateFolderAsync("Reports", CreationCollisionOption.OpenIfExists);
                StorageFile file = await folder.CreateFileAsync("reports.txt", CreationCollisionOption.OpenIfExists);

                IList<string> files = await FileIO.ReadLinesAsync(file);
                listFiles = new List<string>(files);
            }

            foreach (string fileStr in listFiles)
            {
                listBox.Items.Add(fileStr);
            }
        }

        async private void buttonAddReport_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var openPicker = new FileOpenPicker();
            openPicker.FileTypeFilter.Add(".mdc");
            openPicker.SuggestedStartLocation = PickerLocationId.ComputerFolder;

            StorageFile storageFile = await openPicker.PickSingleFileAsync();
            if (storageFile != null)
            {
                bool isFind = false;
                foreach (string fName in listFiles)
                {
                    if (fName == storageFile.DisplayName)
                    {
                        isFind = true;
                        break;
                    }
                }

                if (isFind)
                {
                    await StiMessageBox.ShowAsync("The report of the same name already exists.", "ReportManager", StiMessageBoxButton.Ok);
                }
                else
                {
                    StorageFolder folder = await ApplicationData.Current.TemporaryFolder.CreateFolderAsync("Reports", CreationCollisionOption.OpenIfExists);
                    await storageFile.CopyAsync(folder);

                    listFiles.Add(storageFile.DisplayName);
                    listBox.Items.Add(storageFile.DisplayName);

                    await WriteListFilesAsync();
                }
            }
        }

        async private void buttonDeleteReport_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            int index = listBox.SelectedIndex;
            if (index != -1)
            {
                string fileName = listFiles[index] + ".mdc";
                StorageFolder folder = await ApplicationData.Current.TemporaryFolder.CreateFolderAsync("Reports", CreationCollisionOption.OpenIfExists);
                StorageFile file = await folder.GetFileAsync(fileName);
                await file.DeleteAsync();

                listBox.Items.RemoveAt(index);
                listFiles.RemoveAt(index);

                await WriteListFilesAsync();
            }
        }

        async private void buttonPreviewReport_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            int index = listBox.SelectedIndex;
            if (index != -1)
            {
                string fileName = listFiles[index] + ".mdc";
                StorageFolder folder = await ApplicationData.Current.TemporaryFolder.CreateFolderAsync("Reports", CreationCollisionOption.OpenIfExists);
                StorageFile file = await folder.GetFileAsync(fileName);

                StiReport report = new StiReport();
                await report.LoadDocumentAsync(file);

                viewerControl.Report = report;
            }
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool enabled = (listBox.SelectedIndex != -1);

            buttonDeleteReport.IsEnabled = enabled;
            buttonPreviewReport.IsEnabled = enabled;
        }
        #endregion
    }
}
