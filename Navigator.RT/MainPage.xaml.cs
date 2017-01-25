using Navigator.RT.CustomReports;
using Navigator.RT.Reports;
using Stimulsoft.Base.Localization;
using Stimulsoft.Controls.UWP.Controls;
using Stimulsoft.Report;
using Stimulsoft.Report.Viewer.UWP;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace Navigator.RT
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled) return;
        }

        #region Fields

        private static StiViewerControl viewerControl;

        private RibbonGroup groupDemo;
        private TextBlock demoButtonLabel;
        //private TextBlock addReportButtonLabel;

        #endregion

        #region Handlers

        private void DemoButtonClick(object sender, RoutedEventArgs e)
        {
            ((Frame)Window.Current.Content).Navigate(typeof(StiDemoReportsPage), null);
        }

        private void AddReportButtonClick(object sender, RoutedEventArgs e)
        {
            ((Frame)Window.Current.Content).Navigate(typeof(StiCreateNewReportWizardPage), new object[] { null, viewerControl.Report });
        }

        private void ThisLocalizationChanged(object sender, EventArgs e)
        {
            Localize();
        }

        #endregion

        #region Methods

        private void Dispose()
        {
            this.Content = null;
            gridRoot = null;

            GC.Collect();
        }

        private StackPanel CreateButtonContent(ref TextBlock label, string text, string imagePath)
        {
            var image = new Image
            {
                Source = new BitmapImage(new Uri(imagePath, UriKind.RelativeOrAbsolute)),
                Stretch = Stretch.None
            };

            label = new TextBlock
            {
                MaxWidth = 100,
                TextWrapping = TextWrapping.Wrap,
                TextAlignment = TextAlignment.Center,
                Margin = new Thickness(4),
                Text = text
            };

            var panel = new StackPanel();
            panel.Children.Add(image);
            panel.Children.Add(label);

            return panel;
        }

        private void Localize()
        {
            if (demoButtonLabel == null) return;

            demoButtonLabel.Text = StiLocalization.Get("NavigatorRT", "ReportsDemo");
            demoButtonLabel.Text = StiLocalization.Get("NavigatorRT", "AddCustomReports");
            groupDemo.Header = StiLocalization.Get("NavigatorRT", "Demo");
        }

        #endregion

        #region Methods.override

        protected override void OnNavigatedTo(Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (viewerControl == null)
            {
                viewerControl = new StiViewerControl();

                #region Add Button

                if (viewerControl.ToolbarPanel != null)
                {
                    var stackPanel = new StackPanel
                    {
                        Orientation = Orientation.Horizontal,
                        Margin = new Thickness(8, 0, 8, 0)
                    };

                    var demoButton = new RibbonButton
                    {
                        VerticalAlignment = VerticalAlignment.Center,
                        MinWidth = 75,
                        Height = 92,
                        Content = CreateButtonContent(ref demoButtonLabel, StiLocalization.Get("NavigatorRT", "ReportsDemo"), "ms-appx:///Images/Home.png")
                    };
                    demoButton.Click += DemoButtonClick;

                    var addReportButton = new RibbonButton
                    {
                        VerticalAlignment = VerticalAlignment.Center,
                        MinWidth = 75,
                        Height = 92,
                        Content = CreateButtonContent(ref demoButtonLabel, StiLocalization.Get("NavigatorRT", "AddCustomReports"), "ms-appx:///Images/AddCustomReport.png")
                    };
                    addReportButton.Click += AddReportButtonClick;

                    stackPanel.Children.Add(demoButton);
                    stackPanel.Children.Add(addReportButton);

                    groupDemo = new RibbonGroup
                    {
                        Header = StiLocalization.Get("NavigatorRT", "Demo"),
                        Content = stackPanel
                    };

                    viewerControl.ToolbarPanel.Children.Insert(0, groupDemo);
                }

                #endregion
            }
            else
            {
                if (viewerControl.Parent != null)
                    ((Grid)viewerControl.Parent).Children.Remove(viewerControl);
            }

            var currentReport = e.Parameter as StiReport;
            gridRoot.Children.Add(viewerControl);
            viewerControl.Report = currentReport;

            StiLocalization.LocalizationChanged += ThisLocalizationChanged;
        }

        protected override void OnNavigatedFrom(Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            StiLocalization.LocalizationChanged -= ThisLocalizationChanged;
            Dispose();
        }

        #endregion
    }
}