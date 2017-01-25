#region Copyright (C) 2003-2013 Stimulsoft
/*
{*******************************************************************}
{																	}
{	Stimulsoft Reports.RT											}
{	                         										}
{																	}
{	Copyright (C) 2003-2013 Stimulsoft     							}
{	ALL RIGHTS RESERVED												}
{																	}
{	The entire contents of this file is protected by U.S. and		}
{	International Copyright Laws. Unauthorized reproduction,		}
{	reverse-engineering, and distribution of all or any portion of	}
{	the code contained in this file is strictly prohibited and may	}
{	result in severe civil and criminal penalties and will be		}
{	prosecuted to the maximum extent possible under the law.		}
{																	}
{	RESTRICTIONS													}
{																	}
{	THIS SOURCE CODE AND ALL RESULTING INTERMEDIATE FILES			}
{	ARE CONFIDENTIAL AND PROPRIETARY								}
{	TRADE SECRETS OF Stimulsoft										}
{																	}
{	CONSULT THE END USER LICENSE AGREEMENT FOR INFORMATION ON		}
{	ADDITIONAL RESTRICTIONS.										}
{																	}
{*******************************************************************}
*/
#endregion Copyright (C) 2003-2013 Stimulsoft

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Navigator.RT.Reports;
using Stimulsoft.Controls.UWP.Controls;
using Stimulsoft.Helper.UWP.Data;
using Stimulsoft.Report;
using Windows.Storage.Pickers;
using Windows.UI.Xaml.Automation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Stimulsoft.Base.Localization;
using Stimulsoft.Helper.UWP.MessageBox;
using Windows.UI.Xaml;

namespace Navigator.RT.CustomReports
{
    internal sealed partial class StiCreateNewReportWizardPage : StiPageBase
    {
        internal StiCreateNewReportWizardPage()
        {
            this.InitializeComponent();
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled) return;

            headerControl = new StiPageHeader(this);
            headerControl.BackButton.Click += BackButtonClick;
            this.rootGrid.Children.Insert(0, headerControl);

            Localize();
        }

        #region Fields

        private readonly StiPageHeader headerControl;

        private StiReport currentReport;
        private StiReport newReport;
        private DataSet dataSet;
        private List<StiReportData.StiGroupInfoList> groups;

        #endregion

        #region StiPageBase.override

        protected override void BlockPage(bool useDictionaryPanel)
        {
            bottomAppBar.IsEnabled = false;
            headerControl.BackButton.IsEnabled = false;
            scrollViewer.IsEnabled = false;
            progress.IsEnabled = false;
        }

        protected override void UnblockPage()
        {
            bottomAppBar.IsEnabled = true;
            headerControl.BackButton.IsEnabled = true;
            scrollViewer.IsEnabled = true;
            progress.IsEnabled = true;
        }

        #endregion

        #region Methods.override

        protected async override void OnNavigatedTo(Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var parameters = (object[])e.Parameter;

            // Groups
            groups = parameters[0] as List<StiReportData.StiGroupInfoList> ??
                      await StiReportData.GetReportsByCategoryAsync("CustomReports\\Reports.xml", true);

            // Report
            currentReport = parameters[1] as StiReport;
            if (currentReport == null)
            {
                radioCurrentReport.IsEnabled = false;
                radioNewDocument.IsChecked = true;
            }
            else
            {
                radioCurrentReport.IsChecked = true;
            }

            if (groups.Count > 0)
            {
                foreach (var group in groups)
                {
                    comboBoxCategory.Items.Add(group.Key);
                }

                comboBoxCategory.SelectedIndex = 0;
                radioCurrentCategory.IsChecked = true;
            }
            else
            {
                radioNewCategory.IsChecked = true;
                radioCurrentCategory.IsEnabled = false;
            }
        }

        protected override void Localize()
        {
            this.headerControl.Title = StiLocalization.Get("NavigatorRT", "PageCreateNewReport");

            AutomationProperties.SetName(buttonSave, StiLocalization.Get("NavigatorRT", "SaveReport"));
            labelRequiredFields.Text = StiLocalization.Get("NavigatorRT", "RequiredFields");
            label1.Text = StiLocalization.Get("NavigatorRT", "AddCustomReportLabel1");
            labelStep1.Text = StiLocalization.Get("NavigatorRT", "AddCustomReportStep1");
            labelCategory.Text = StiLocalization.Get("NavigatorRT", "Category");
            radioCurrentCategory.Content = StiLocalization.Get("NavigatorRT", "AddCustomReportCurrentCategory");
            radioNewCategory.Content = StiLocalization.Get("NavigatorRT", "AddCustomReportCreateNewCategory");
            labelCategoryDescription.Text = StiLocalization.Get("NavigatorRT", "AddCustomReportCategoryDesc");
            labelStep2.Text = StiLocalization.Get("NavigatorRT", "AddCustomReportStep2");
            labelReport.Text = StiLocalization.Get("Components", "StiReport");
            labelReportName.Text = StiLocalization.Get("NavigatorRT", "ReportName");
            labelReportDescription.Text = StiLocalization.Get("PropertyMain", "ReportDescription");
            labelDataName.Text = StiLocalization.Get("NavigatorRT", "AddCustomReportDataName");
            labelStep3.Text = StiLocalization.Get("NavigatorRT", "AddCustomReportStep3");
            radioCurrentReport.Content = StiLocalization.Get("NavigatorRT", "AddCustomReportUseCurrentReport");
            radioNewDocument.Content = StiLocalization.Get("NavigatorRT", "AddCustomReportNewDocument");
            radioNewReport.Content = StiLocalization.Get("NavigatorRT", "AddCustomReportNewReport");
            labelLoadDocument.Text = StiLocalization.Get("NavigatorRT", "AddCustomReportLoadDocument");
            buttonLoadDocument.Content = StiLocalization.Get("NavigatorRT", "LoadDocument");
            labelNewReport.Text = StiLocalization.Get("NavigatorRT", "AddCustomReportLabelNewReport");
            buttonLoadReport.Content = StiLocalization.Get("NavigatorRT", "LoadReport");
            buttonLoadData.Content = StiLocalization.Get("NavigatorRT", "LoadData");
        }

        #endregion

        #region Methods

        private static string CheckName(string text)
        {
            var builder = new StringBuilder(text);
            builder.Replace("&", string.Empty);
            builder.Replace("\"", string.Empty);
            
            return builder.ToString();
        }

        private bool CheckReportName(string category, string repName)
        {
            foreach (var g in groups.Where(g => g.Key == category))
            {
                if (g.Any(reportItem => reportItem.Header == repName))
                    return true;

                break;
            }

            return false;
        }
        
        private void CheckState()
        {
            var isEnalbed = false;
            if (radioCurrentReport.IsChecked == true)
            {
                isEnalbed = true;
            }
            else if ((radioNewDocument.IsChecked == true || radioNewReport.IsChecked == true) && newReport != null)
            {
                isEnalbed = true;
            }

            buttonSave.IsEnabled = isEnalbed;
        }

        private void ShowProgress()
        {
            scrollViewer.IsEnabled = false;
            progress.IsActive = true;
            progress.Visibility = Visibility.Visible;
        }

        private void HideProgress()
        {
            scrollViewer.IsEnabled = true;
            progress.IsActive = false;
            progress.Visibility = Visibility.Collapsed;
        }

        #endregion

        #region Handlers

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            ((Frame)Window.Current.Content).GoBack();
        }

        private async void ButtonLoadDocumentClick(object sender, RoutedEventArgs e)
        {
            var openPicker = new FileOpenPicker
                                 {
                                     SuggestedStartLocation = PickerLocationId.ComputerFolder
                                 };
            openPicker.FileTypeFilter.Add(".mdc");

            var storageFile = await openPicker.PickSingleFileAsync();
            if (storageFile != null)
            {
                ShowProgress();

                if (newReport != null)
                {
                    newReport.Dispose();
                    newReport = null;
                }

                try
                {
                    newReport = new StiReport();
                    await newReport.LoadDocumentAsync(storageFile);
                }
                catch
                {
                    newReport = null;
                }
                finally
                {
                    labelLoadDocumentOK.Visibility = Visibility.Visible;

                    if (textBoxReportName.Text.Length == 0)
                        textBoxReportName.Text = newReport.ReportName;
                    if (textBoxReportDescription.Text.Length == 0)
                        textBoxReportDescription.Text = newReport.ReportDescription;
                }

                HideProgress();
            }

            CheckState();
        }

        private async void ButtonLoadReportClick(object sender, RoutedEventArgs e)
        {
            var openPicker = new FileOpenPicker
                                 {
                                     SuggestedStartLocation = PickerLocationId.ComputerFolder
                                 };
            openPicker.FileTypeFilter.Add(".mrt");

            var storageFile = await openPicker.PickSingleFileAsync();
            if (storageFile != null)
            {
                ShowProgress();

                if (newReport != null)
                {
                    newReport.Dispose();
                    newReport = null;
                }

                try
                {
                    newReport = new StiReport();
                    await newReport.LoadAsync(storageFile);
                }
                catch
                {
                    newReport = null;
                }
                finally
                {
                    labelNewReport1OK.Visibility = Visibility.Visible;

                    if (textBoxReportName.Text.Length == 0)
                        textBoxReportName.Text = newReport.ReportName;
                    if (textBoxReportDescription.Text.Length == 0)
                        textBoxReportDescription.Text = newReport.ReportDescription;
                }

                HideProgress();
            }

            CheckState();
        }

        private async void ButtonLoadDataClick(object sender, RoutedEventArgs e)
        {
            var openPicker = new FileOpenPicker();
            openPicker.FileTypeFilter.Add(".data");
            openPicker.SuggestedStartLocation = PickerLocationId.ComputerFolder;

            var storageFile = await openPicker.PickSingleFileAsync();
            if (storageFile != null)
            {
                ShowProgress();
                dataSet = null;

                try
                {
                    dataSet = new DataSet();
                    await dataSet.LoadAsync(storageFile);
                }
                catch
                {
                    dataSet = null;
                }
                finally
                {
                    labelNewReport2OK.Visibility = Visibility.Visible;
                }

                HideProgress();
            }

            CheckState();
        }

        private void RadioButtonsChecked(object sender, RoutedEventArgs e)
        {
            CheckState();
        }

        private async void ButtonSaveClick(object sender, RoutedEventArgs e)
        {
            #region Check

            string categoryStr;
            if (radioNewCategory.IsChecked == true)
            {
                textBoxCategory.Text = CheckName(textBoxCategory.Text);
                if (textBoxCategory.Text.Length == 0)
                {
                    textBoxCategory.Focus(FocusState.Pointer);
                    return;
                }

                categoryStr = textBoxCategory.Text;
            }
            else
            {
                categoryStr = (string)comboBoxCategory.SelectedItem;
            }

            textBoxReportName.Text = CheckName(textBoxReportName.Text);
            if (textBoxReportName.Text.Length == 0)
            {
                textBoxReportName.Focus(FocusState.Pointer);
                return;
            }

            if (CheckReportName(textBoxCategory.Text, textBoxReportName.Text))
            {
                var message = string.Format(StiLocalization.Get("NavigatorRT", "FailedAddReport"), textBoxCategory.Text);
                await StiMessageBox.ShowAsync(message, StiLocalization.Get("PropertyMain", "ReportName"), StiMessageBoxButton.Ok);
                textBoxReportName.Focus(FocusState.Pointer);
                return;
            }

            textBoxDataName.Text = CheckName(textBoxDataName.Text);
            if (radioNewReport.IsChecked == true && textBoxDataName.Text.Length == 0)
            {
                textBoxDataName.Focus(FocusState.Pointer);
                return;
            }

            #endregion

            ShowProgress();
            StiReport reportTemp = null;
            if (radioCurrentReport.IsChecked == true)
            {
                reportTemp = currentReport;
            }
            else if (radioNewDocument.IsChecked == true)
            {
                reportTemp = newReport;
            }
            else if (radioNewReport.IsChecked == true)
            {
                if (dataSet != null)
                    newReport.RegBusinessObject(textBoxDataName.Text, dataSet);

                var error = false;
                try
                {
                    await newReport.RenderAsync();
                }
                catch
                {
                    error = true;
                }

                if (error)
                {
                    await StiMessageBox.ShowAsync(StiLocalization.Get("NavigatorRT", "RenderingError"), 
                        StiLocalization.Get("NavigatorRT", "Error"), StiMessageBoxButton.Ok);

                    HideProgress();
                    return;
                }

                reportTemp = newReport;
            }

            await StiCustomReportsHelper.AddCustomReportAsync(groups, reportTemp, categoryStr, textBoxCategoryDescriptino.Text, textBoxReportName.Text, textBoxReportDescription.Text);
            HideProgress();

            BackButtonClick(null, null);
        }

        private void RadioNewReportChecked(object sender, RoutedEventArgs e)
        {
            labelDataName.Visibility = Visibility.Visible;
            textBoxDataName.Visibility = Visibility.Visible;
            panelNewReport.Visibility = Visibility.Visible;
        }

        private void RadioNewReportUnchecked(object sender, RoutedEventArgs e)
        {
            labelDataName.Visibility = Visibility.Collapsed;
            textBoxDataName.Visibility = Visibility.Collapsed;
            panelNewReport.Visibility = Visibility.Collapsed;
        }

        private void RadioNewDocumentChecked(object sender, RoutedEventArgs e)
        {
            panelNewDocument.Visibility = Visibility.Visible;
        }

        private void RadioNewDocumentUnchecked(object sender, RoutedEventArgs e)
        {
            panelNewDocument.Visibility = Visibility.Collapsed;
        }
        #endregion
    }
}