using Stimulsoft.Base.Drawing;
using Stimulsoft.Report;
using Stimulsoft.Report.Components;
using Windows.UI;
using Windows.UI.Xaml.Controls;

namespace RuntimeBuildReport
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        #region Handlers
        async private void buttonBuildReport_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            viewerControl.ShowProgressBar("Create Report...");
            StiReport report = new StiReport();

            //Add data to datastore
            report.RegBusinessObject("Customers", "Customers", new Data().Customers);

            //Fill dictionary
            report.Dictionary.SynchronizeBusinessObjects();

            StiPage page = report.Pages[0];

            //Create HeaderBand
            var headerBand = new StiHeaderBand();
            headerBand.Height = 0.5;
            headerBand.Name = "HeaderBand";
            page.Components.Add(headerBand);

            //Create text on header
            var headerText = new StiText(new RectangleD(0, 0, 5, 0.5));
            headerText.Text = "CompanyName";
            headerText.HorAlignment = StiTextHorAlignment.Center;
            headerText.Name = "HeaderText";
            headerText.Brush = new StiSolidBrush(Colors.LightGreen);
            headerBand.Components.Add(headerText);

            //Create Databand
            StiDataBand dataBand = new StiDataBand();
            dataBand.BusinessObjectGuid = report.Dictionary.BusinessObjects[0].Guid;
            dataBand.Height = 0.5;
            dataBand.Name = "DataBand";
            page.Components.Add(dataBand);

            //Create text
            StiText dataText = new StiText(new RectangleD(0, 0, 5, 0.5));
            dataText.Text = "{Line}.{Customers.CompanyName}";
            dataText.Name = "DataText";
            dataBand.Components.Add(dataText);

            //Create FooterBand
            StiFooterBand footerBand = new StiFooterBand();
            footerBand.Height = 0.5;
            footerBand.Name = "FooterBand";
            page.Components.Add(footerBand);

            //Create text on footer
            StiText footerText = new StiText(new RectangleD(0, 0, 5, 0.5));
            footerText.Text = "Count - {Count()}";
            footerText.HorAlignment = StiTextHorAlignment.Right;
            footerText.Name = "FooterText";
            footerText.Brush = new StiSolidBrush(Colors.LightGreen);
            footerBand.Components.Add(footerText);

            await report.RenderAsync();
            viewerControl.Report = report;

            viewerControl.HideProgressBar();
        }
        #endregion
    }
}