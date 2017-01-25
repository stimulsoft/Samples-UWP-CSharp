using Stimulsoft.Base.UWP.Extensions;
using Stimulsoft.Report;
using System;
using System.Reflection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace CustomPreview
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += BlankPage_Loaded;
        }

        #region Fields
        private bool isInit = false;
        private Type optionsType = typeof(StiOptions.Viewer.Elements);
        #endregion

        #region Handlers
        private void CheckBox_CheckedUncked(object sender, RoutedEventArgs e)
        {
            if (isInit)
            {
                var checkBox = (CheckBox)sender;
                var fi = optionsType.GetTypeInfo().GetDeclaredField(checkBox.Tag as string);

                if (fi != null)
                    fi.SetValue(null, checkBox.IsChecked.Value.ToVisibility());

                buttonUpdateViewer.IsEnabled = true;
            }
        }

        private void buttonUpdateViewer_Click(object sender, RoutedEventArgs e)
        {
            buttonUpdateViewer.IsEnabled = false;
            viewerCotnrol.UpdateControlsState();
        }

        async private void BlankPage_Loaded(object sender, RoutedEventArgs e)
        {
            var storage = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFileAsync("StimulsoftResources\\Sample\\SimpleList.mdc");

            var report = new StiReport();
            await report.LoadDocumentAsync(storage);

            viewerCotnrol.Report = report;

            foreach (CheckBox checkBox in panel1.Children)
            {
                var fi = optionsType.GetTypeInfo().GetDeclaredField(checkBox.Tag as string);
                if (fi != null)
                {
                    var visibility = (Visibility)fi.GetValue(null);
                    checkBox.IsChecked = visibility == Visibility.Visible;

                    checkBox.Checked += CheckBox_CheckedUncked;
                    checkBox.Unchecked += CheckBox_CheckedUncked;
                }
            }

            isInit = true;
        }
        #endregion
    }
}
