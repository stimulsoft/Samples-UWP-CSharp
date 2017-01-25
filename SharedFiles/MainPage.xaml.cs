using Stimulsoft.Helper.UWP.ResourcesHelper;
using Stimulsoft.Report;
using Stimulsoft.Report.Export;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace SharedFiles
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        // Open 'SettingsPane' - Click 'Share'
        // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

        #region Fields

        private DataTransferManager dataTransferManager;

        private bool LockDeferredPDFRequestedHandler;
        private StorageFile exportfile;

        #endregion

        #region Methods.override

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            var storageFile = await StiResourcesStorageHelper.LoadResourceFileAsync("SimpleList.mdc");
            var report = new StiReport();
            await report.LoadDocumentAsync(storageFile);

            viewerControl.Report = report;

            this.dataTransferManager = DataTransferManager.GetForCurrentView();
            this.dataTransferManager.DataRequested += this.OnDataRequested;
        }


        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            // Unregister the current page as a share source.
            this.dataTransferManager.DataRequested -= this.OnDataRequested;
        }

        #endregion

        #region Handlers

        private void OnDataRequested(DataTransferManager sender, DataRequestedEventArgs e)
        {
            //pull pattern
            var requestData = e.Request.Data;
            requestData.Properties.Title = "Shared PDF";
            requestData.Properties.Description = "test_pull_sharing.pdf"; // 

            requestData.SetDataProvider(StandardDataFormats.StorageItems, new DataProviderHandler(this.OnDeferredPDFRequestedHandler));
            requestData.Properties.FileTypes.Add("*.pdf");
        }


        private async void OnDeferredPDFRequestedHandler(DataProviderRequest request)
        {
            LockDeferredPDFRequestedHandler = true;
            exportfile = await Windows.Storage.ApplicationData.Current.TemporaryFolder.CreateFileAsync("test_pull_sharing.pdf", CreationCollisionOption.ReplaceExisting);
            request.SetData(new List<IStorageItem> { exportfile });

            await this.Dispatcher.RunIdleAsync(async delegate (IdleDispatchedHandlerArgs args)
            {
                try
                {
                    var service = new StiPdfExportService();
                    await service.ExportPdfAsync(viewerControl.Report, exportfile, new StiPdfExportSettings());
                }
                finally
                {
                    LockDeferredPDFRequestedHandler = false;
                }
            });

            while (LockDeferredPDFRequestedHandler)
            {
                await Task.Delay(500);
            }

            var deferral = request.GetDeferral();
            deferral.Complete();
        }

        #endregion
    }
}
