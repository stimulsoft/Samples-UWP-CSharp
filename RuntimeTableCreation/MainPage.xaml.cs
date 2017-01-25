using Stimulsoft.Base.Drawing;
using Stimulsoft.Report;
using Stimulsoft.Report.Components;
using Stimulsoft.Report.Components.Table;
using System.Collections.Generic;
using System.Drawing;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace RuntimeTableCreation
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += BlankPage_Loaded;
            buttonResize.Content = "<<<";
        }

        #region Methods
        private List<DataItem> CreateBusinessObject()
        {
            List<DataItem> list = new List<DataItem>(panelItems.Children.Count);

            foreach (StiTableItem item in panelItems.Children)
            {
                list.Add(new DataItem(item.Column1, item.Column2, item.Column3));
            }

            return list;
        }
        #endregion

        #region Handlers
        private void buttonResize_Click(object sender, RoutedEventArgs e)
        {
            if (column1.Width.Value == 250)
            {
                column1.Width = new GridLength(450);
                buttonResize.Content = "<<<";
            }
            else
            {
                column1.Width = new GridLength(250);
                buttonResize.Content = ">>>";
            }
        }

        private void BlankPage_Loaded(object sender, RoutedEventArgs e)
        {
            panelItems.Children.Add(new StiTableItem("1", "One", true));
            panelItems.Children.Add(new StiTableItem("2", "Two", false));
            panelItems.Children.Add(new StiTableItem("3", "Three", false));
            panelItems.Children.Add(new StiTableItem("4", "Four", true));
        }

        private void buttonAddItem_Click(object sender, RoutedEventArgs e)
        {
            panelItems.Children.Add(new StiTableItem((panelItems.Children.Count + 1).ToString(), "test", true));
            buttonRemove.IsEnabled = true;
        }

        private void buttonRemove_Click(object sender, RoutedEventArgs e)
        {
            if (panelItems.Children.Count > 0)
            {
                panelItems.Children.RemoveAt(panelItems.Children.Count - 1);
            }

            buttonRemove.IsEnabled = (panelItems.Children.Count > 0);
        }

        async private void buttonCreateReport_Click(object sender, RoutedEventArgs e)
        {
            StiReport report = new StiReport();
            report.ScriptLanguage = StiReportLanguageType.CSharp;

            report.RegBusinessObject("view", "view", CreateBusinessObject());
            report.Dictionary.SynchronizeBusinessObjects();
            StiPage page = report.Pages[0];

            //Create Table
            StiTable table = new StiTable();
            table.Name = "Table1";
            if (rbAWNone.IsChecked == true)
                table.AutoWidth = StiTableAutoWidth.None;
            else if (rbAWPage.IsChecked == true)
                table.AutoWidth = StiTableAutoWidth.Page;
            else table.AutoWidth = StiTableAutoWidth.Table;

            if (rbAWTNone.IsChecked == true)
                table.AutoWidthType = StiTableAutoWidthType.None;
            else if (rbAWTFullTable.IsChecked == true)
                table.AutoWidthType = StiTableAutoWidthType.FullTable;
            else table.AutoWidthType = StiTableAutoWidthType.LastColumns;

            table.ColumnCount = 3;
            table.RowCount = 3;
            table.HeaderRowsCount = 1;
            table.FooterRowsCount = 1;
            table.Width = page.Width;
            table.Height = page.GridSize * 12;
            table.BusinessObjectGuid = report.Dictionary.BusinessObjects[0].Guid;
            page.Components.Add(table);
            table.CreateCell();
            table.TableStyle = StiTableStyle.Style59;

            int indexHeaderCell = 0;
            int indexDataCell = 3;

            string[] columns = new string[]
            {
                "Column1",
                "Column2",
                "Column3"
            };

            foreach (string column in columns)
            {
                //Set text on header
                StiTableCell headerCell = table.Components[indexHeaderCell] as StiTableCell;
                headerCell.Text.Value = column;
                headerCell.HorAlignment = StiTextHorAlignment.Center;
                headerCell.VertAlignment = StiVertAlignment.Center;

                StiTableCell dataCell = table.Components[indexDataCell] as StiTableCell;
                dataCell.Text.Value = "{view." + column + "}";
                dataCell.Border = new StiBorder(StiBorderSides.All, Color.FromArgb(255, 32, 178, 170), 1, StiPenStyle.Dash);

                indexHeaderCell++;
                indexDataCell++;
            }

            StiTableCell dataCheckBoxCell = table.Components[indexDataCell - 1] as StiTableCell;
            dataCheckBoxCell.CellType = StiTablceCellType.CheckBox;

            //Set text on footer
            StiTableCell footerCell = table.Components[table.Components.Count - 1] as StiTableCell;
            footerCell.Text.Value = "Count - {Count()}";
            footerCell.Font = new Font("Arial", 15, System.Drawing.FontStyle.Bold);
            footerCell.VertAlignment = StiVertAlignment.Center;
            footerCell.HorAlignment = StiTextHorAlignment.Center;

            //Render without progress bar
            await report.RenderAsync();
            viewerControl.Report = report;
        }
        #endregion
    }
}