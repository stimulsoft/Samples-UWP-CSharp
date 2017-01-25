using Navigator.RT.Splash;
using Stimulsoft.Controls.UWP;
using Stimulsoft.Controls.UWP.Helpers;
using Stimulsoft.Helper.UWP.Localization;
using Stimulsoft.Helper.UWP.SettingsPane;
using Stimulsoft.Report;
using Stimulsoft.Report.UWP.Helpers;
using Stimulsoft.Report.Viewer.UWP.Helpers;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;

namespace Navigator.RT
{
    sealed partial class App : Application
    {
        public App()
        {
            StiOptions.WinRT.AutomaticallyLoadThemeInApplication = false;
            this.InitializeComponent();
            this.Suspending += AppSuspending;
            this.UnhandledException += AppUnhandledException;
        }

        private static void AppSuspending(object sender, SuspendingEventArgs e)
        {
            StiViewerTrashHelper.Collect();
        }

        private void AppUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            e.Handled = true;
        }

        protected async override void OnLaunched(LaunchActivatedEventArgs args)
        {
            // Отключаем интеграцию в SettingsPane контролами
            StiSettingsInternalHelper.IntegrateToTheSettingsPane = false;
            // Сами инициализируем интеграцию в SettingsPane
            StiSettingsPaneHelper.Initialize();

            if (StiSettings.GetBool("NavigatorWinRT", "IsFirstLaunch", true))
            {
                StiSettings.Set("NavigatorWinRT", "IsFirstLaunch", false);

                var key = System.Globalization.CultureInfo.CurrentCulture.Name;

                #region Cultures
                if (key.StartsWith("de"))
                    key = "de";
                else if (key.StartsWith("ru"))
                    key = "ru";
                else if (key == "zh-Hans")
                    key = "zh-CHS";
                else if (key == "zh-Hant")
                    key = "zh-CHT";
                else
                    key = "en";
                #endregion

                StiSettings.Set("Localization", "Current", key);
            }

            StiThemesHelper.LoadTheme();
            await StiLocalizationHelper.LoadDefaulLocalizationAsync();

            Window.Current.Content = new StiSplashControl(args.Arguments, null);
            Window.Current.Activate();
        }

        protected override void OnFileActivated(FileActivatedEventArgs args)
        {
            base.OnFileActivated(args);

            if (args.Files.Count <= 0) return;
            Window.Current.Content = new StiSplashControl(null, args.Files[0]);
            Window.Current.Activate();
        }
    }
}
