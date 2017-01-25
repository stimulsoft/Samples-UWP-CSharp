using Stimulsoft.Base.Localization;
using Stimulsoft.Report;
using Stimulsoft.Report.Components;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace WorkWithComponents
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            StiOptions.Viewer.Elements.ReportOpenButtonVisibility = Visibility.Collapsed;
            this.InitializeComponent();

            this.Loaded += BlankPage_Loaded;
        }

        #region Fields
        private const string reportStr = "77u/PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0idXRmLTgiIHN0YW5kYWxvbmU9InllcyI/Pg0KPFN0aVNlcmlhbGl6ZXIgdmVyc2lvbj0iMS4wIiBhcHBsaWNhdGlvbj0iU3RpUmVwb3J0Ij4NCiAgPERpY3Rpb25hcnkgUmVmPSIxIiB0eXBlPSJEaWN0aW9uYXJ5IiBpc0tleT0idHJ1ZSI+DQogICAgPERhdGFTb3VyY2VzIGlzTGlzdD0idHJ1ZSIgY291bnQ9IjAiIC8+DQogICAgPFJlbGF0aW9ucyBpc0xpc3Q9InRydWUiIGNvdW50PSIwIiAvPg0KICAgIDxSZXBvcnQgaXNSZWY9IjAiIC8+DQogICAgPFZhcmlhYmxlcyBpc0xpc3Q9InRydWUiIGNvdW50PSIwIiAvPg0KICA8L0RpY3Rpb25hcnk+DQogIDxMYW5ndWFnZSBSZWY9IjIiIHR5cGU9IkNTaGFycCIgaXNLZXk9InRydWUiIC8+DQogIDxNb2RpZmllcnMgaXNMaXN0PSJ0cnVlIiBjb3VudD0iMCIgLz4NCiAgPFBhZ2VzIGlzTGlzdD0idHJ1ZSIgY291bnQ9IjEiPg0KICAgIDxQYWdlMSBSZWY9IjMiIHR5cGU9IlBhZ2UiIGlzS2V5PSJ0cnVlIj4NCiAgICAgIDxDb21wb25lbnRzIGlzTGlzdD0idHJ1ZSIgY291bnQ9IjEiPg0KICAgICAgICA8VGV4dDEgUmVmPSI0IiB0eXBlPSJUZXh0IiBpc0tleT0idHJ1ZSI+DQogICAgICAgICAgPEJvcmRlcj5BbGw7QmxhY2s7MTtTb2xpZDtGYWxzZTs0O0JsYWNrPC9Cb3JkZXI+DQogICAgICAgICAgPEJydXNoPlRyYW5zcGFyZW50PC9CcnVzaD4NCiAgICAgICAgICA8Q2xpZW50UmVjdGFuZ2xlPjIuNCwyLjYsMTMuMiwxMC40PC9DbGllbnRSZWN0YW5nbGU+DQogICAgICAgICAgPEZvbnQ+QXJpYWwsMTY8L0ZvbnQ+DQogICAgICAgICAgPEhpZ2hsaWdodENvbmRpdGlvbiBSZWY9IjUiIHR5cGU9IkhpZ2hsaWdodENvbmRpdGlvbiIgaXNLZXk9InRydWUiPg0KICAgICAgICAgICAgPEJydXNoPlRyYW5zcGFyZW50PC9CcnVzaD4NCiAgICAgICAgICAgIDxGb250PkFyaWFsLDg8L0ZvbnQ+DQogICAgICAgICAgICA8VGV4dEJydXNoPlJlZDwvVGV4dEJydXNoPg0KICAgICAgICAgIDwvSGlnaGxpZ2h0Q29uZGl0aW9uPg0KICAgICAgICAgIDxIb3JBbGlnbm1lbnQ+Q2VudGVyPC9Ib3JBbGlnbm1lbnQ+DQogICAgICAgICAgPE1vZGlmaWVycyBpc0xpc3Q9InRydWUiIGNvdW50PSIwIiAvPg0KICAgICAgICAgIDxOYW1lPlRleHQxPC9OYW1lPg0KICAgICAgICAgIDxQYWdlIGlzUmVmPSIzIiAvPg0KICAgICAgICAgIDxQYXJlbnQgaXNSZWY9IjMiIC8+DQogICAgICAgICAgPFRleHQ+dGVzdDwvVGV4dD4NCiAgICAgICAgICA8VGV4dEJydXNoPkJsYWNrPC9UZXh0QnJ1c2g+DQogICAgICAgICAgPFRleHRGb3JtYXQgUmVmPSI2IiB0eXBlPSJHZW5lcmFsRm9ybWF0IiBpc0tleT0idHJ1ZSIgLz4NCiAgICAgICAgPC9UZXh0MT4NCiAgICAgIDwvQ29tcG9uZW50cz4NCiAgICAgIDxNYXJnaW5zPjAuOTksMC45OSwwLjk5LDAuOTk8L01hcmdpbnM+DQogICAgICA8TW9kaWZpZXJzIGlzTGlzdD0idHJ1ZSIgY291bnQ9IjAiIC8+DQogICAgICA8TmFtZT5QYWdlMTwvTmFtZT4NCiAgICAgIDxQYWdlIGlzUmVmPSIzIiAvPg0KICAgICAgPFBhZ2VIZWlnaHQ+MjkuNjk8L1BhZ2VIZWlnaHQ+DQogICAgICA8UGFnZVdpZHRoPjIxLjAxPC9QYWdlV2lkdGg+DQogICAgICA8UmVwb3J0IGlzUmVmPSIwIiAvPg0KICAgIDwvUGFnZTE+DQogIDwvUGFnZXM+DQogIDxSZWZlcmVuY2VkQXNzZW1ibGllcyBpc0xpc3Q9InRydWUiIGNvdW50PSI3Ij4NCiAgICA8dmFsdWU+U3lzdGVtLkRsbDwvdmFsdWU+DQogICAgPHZhbHVlPlN5c3RlbS5EcmF3aW5nLkRsbDwvdmFsdWU+DQogICAgPHZhbHVlPlN5c3RlbS5XaW5kb3dzLkZvcm1zLkRsbDwvdmFsdWU+DQogICAgPHZhbHVlPlN5c3RlbS5EYXRhLkRsbDwvdmFsdWU+DQogICAgPHZhbHVlPlN5c3RlbS5YbWwuRGxsPC92YWx1ZT4NCiAgICA8dmFsdWU+U3RpbXVsc29mdC5CYXNlLkRsbDwvdmFsdWU+DQogICAgPHZhbHVlPlN0aW11bHNvZnQuUmVwb3J0LkRsbDwvdmFsdWU+DQogIDwvUmVmZXJlbmNlZEFzc2VtYmxpZXM+DQogIDxSZW5kZXJQcm92aWRlciBSZWY9IjciIHR5cGU9IlJlbmRlclByb3ZpZGVyIiBpc0tleT0idHJ1ZSIgLz4NCiAgPFJlcG9ydEFsaWFzPlJlcG9ydDwvUmVwb3J0QWxpYXM+DQogIDxSZXBvcnRBdXRob3IgaXNOdWxsPSJ0cnVlIiAvPg0KICA8UmVwb3J0Q2hhbmdlZD4xMi8xNC8yMDA0IDEyOjI1IEFNPC9SZXBvcnRDaGFuZ2VkPg0KICA8UmVwb3J0Q3JlYXRlZD4xMi8xNC8yMDA0IDEyOjE4IEFNPC9SZXBvcnRDcmVhdGVkPg0KICA8UmVwb3J0RGVzY3JpcHRpb24gaXNOdWxsPSJ0cnVlIiAvPg0KICA8UmVwb3J0TmFtZT5SZXBvcnQ8L1JlcG9ydE5hbWU+DQogIDxTY3JpcHQ+dXNpbmcgU3lzdGVtOw0KdXNpbmcgU3lzdGVtLkRyYXdpbmc7DQp1c2luZyBTeXN0ZW0uV2luZG93cy5Gb3JtczsNCnVzaW5nIFN5c3RlbS5EYXRhOw0KdXNpbmcgU3RpbXVsc29mdC5CYXNlLkRyYXdpbmc7DQp1c2luZyBTdGltdWxzb2Z0LlJlcG9ydDsNCnVzaW5nIFN0aW11bHNvZnQuUmVwb3J0LkNvbXBvbmVudHM7DQoNCm5hbWVzcGFjZSBTdGlSZXBvcnRzDQp7DQogICAgDQogICAgcHVibGljIGNsYXNzIFJlcG9ydCA6IFN0aW11bHNvZnQuUmVwb3J0LlN0aVJlcG9ydA0KICAgIHsNCiAgICAgICAgDQogICAgICAgIHB1YmxpYyBSZXBvcnQoKQ0KICAgICAgICB7DQogICAgICAgICAgICB0aGlzLkluaXRpYWxpemVDb21wb25lbnQoKTsNCiAgICAgICAgfQ0KICAgICAgICANCiAgICAgICAgI3JlZ2lvbiBTdGlSZXBvcnQgRGVzaWduZXIgZ2VuZXJhdGVkIGNvZGUgLSBkbyBub3QgbW9kaWZ5I2VuZHJlZ2lvbiBTdGlSZXBvcnQgRGVzaWduZXIgZ2VuZXJhdGVkIGNvZGUgLSBkbyBub3QgbW9kaWZ5DQogICAgfQ0KfQ0KPC9TY3JpcHQ+DQogIDxVbml0IFJlZj0iOCIgdHlwZT0iY20iIGlzS2V5PSJ0cnVlIiAvPg0KPC9TdGlTZXJpYWxpemVyPg==";
        #endregion

        #region Handlers
        async private void BlankPage_Loaded(object sender, RoutedEventArgs e)
        {
            viewerCotnrol.ShowProgressBar(StiLocalization.Get("DesignerFx", "LoadingReport"));

            byte[] buffer = Convert.FromBase64String(reportStr);
            StiReport report = new StiReport();
            await report.LoadAsync(buffer);
            await report.RenderAsync();

            viewerCotnrol.Report = report;

            viewerCotnrol.HideProgressBar();
        }

        async private void btPreview_Click(object sender, RoutedEventArgs e)
        {
            viewerCotnrol.ShowProgressBar(StiLocalization.Get("DesignerFx", "LoadingReport"));

            StiReport report = viewerCotnrol.Report;
            if (report != null)
            {
                viewerCotnrol.Report = null;

                ((StiText)report.Pages["Page1"].Components["Text1"]).Text.Value = textBox1.Text;
                await report.RenderAsync();

                viewerCotnrol.Report = report;
            }

            viewerCotnrol.HideProgressBar();
        }
        #endregion
    }
}