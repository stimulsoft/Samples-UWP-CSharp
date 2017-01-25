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

using System.Collections.Generic;
using System.Threading.Tasks;
using Navigator.RT.CustomReports;
using Stimulsoft.Base.Localization;
using Stimulsoft.Base.UWP.Extensions;
using Stimulsoft.Controls.UWP.Controls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Automation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Navigator.RT.Reports
{
    internal sealed partial class StiDemoReportsPage :
        StiPageBase
    {
        public StiDemoReportsPage()
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
        internal static ReportsType NavigatorReportsType = ReportsType.Demo;

        #endregion

        #region StiPageBase.override

        protected override void BlockPage(bool useDictionaryPanel)
        {
            appBar.IsEnabled = false;
            itemsGrid.IsEnabled = false;
        }

        protected override void UnblockPage()
        {
            appBar.IsEnabled = true;
            itemsGrid.IsEnabled = true;
        }

        #endregion

        #region Methods.override

        protected async override void OnNavigatedTo(Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            await FillDataAsync();
        }

        protected override void Localize()
        {
            this.headerControl.Title = StiLocalization.Get("NavigatorRT", "PageCategoryReports");

            AutomationProperties.SetName(buttonUserReports, StiLocalization.Get("NavigatorRT", "CustomReports"));
            AutomationProperties.SetName(buttonAddReport, StiLocalization.Get("NavigatorRT", "AddReports"));
            AutomationProperties.SetName(buttonDeleteCategory, StiLocalization.Get("NavigatorRT", "DeleteCategory"));

            labelReportsNotFound.Text = StiLocalization.Get("NavigatorRT", "ReportsNotFound");
        }

        #endregion

        #region Methods

        private async Task FillDataAsync()
        {
            ShowProgress();

            var useLocalFolder = false;
            string path;
            if (NavigatorReportsType == ReportsType.Demo)
            {
                labelReportsNotFound.Visibility = Visibility.Collapsed;
                buttonAddReport.Visibility = Visibility.Collapsed;
                CheckSeparator();

                path = "ReportsEn.xml";
            }
            else
            {
                buttonAddReport.Visibility = Visibility.Visible;
                CheckSeparator();

                path = "CustomReports\\Reports.xml";
                useLocalFolder = true;
            }

            var reports = await StiReportData.GetReportsByCategoryAsync(path, useLocalFolder);
            cvs1.Source = reports;
            itemsGrid.ItemsSource = cvs1.View.CollectionGroups;

            labelReportsNotFound.Visibility = (NavigatorReportsType == ReportsType.Custom && reports.Count == 0).ToVisibility();

            HideProgress();
        }

        private void CheckSeparator()
        {
            separator1.Visibility = (buttonAddReport.Visibility == Visibility.Visible || buttonDeleteCategory.Visibility == Visibility.Visible) 
                ? Visibility.Visible : Visibility.Collapsed;
        }

        private void ShowProgress()
        {
            itemsGrid.IsEnabled = false;
            progressRing.IsActive = true;
            progressRing.Visibility = Visibility.Visible;
        }

        private void HideProgress()
        {
            progressRing.IsActive = false;
            progressRing.Visibility = Visibility.Collapsed;
            itemsGrid.IsEnabled = true;
        }

        #endregion

        #region Handlers

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            this.GoBack();
        }

        private async void ButtonUserReportsClick(object sender, RoutedEventArgs e)
        {
            if (NavigatorReportsType == ReportsType.Demo)
            {
                NavigatorReportsType = ReportsType.Custom;
                AutomationProperties.SetName(buttonUserReports, StiLocalization.Get("NavigatorRT", "CustomReports"));
            }
            else
            {
                NavigatorReportsType = ReportsType.Demo;
                AutomationProperties.SetName(buttonUserReports, StiLocalization.Get("NavigatorRT", "DemoReports"));
            }

            await FillDataAsync();
        }
        
        private void ItemsGridItemClick(object sender, ItemClickEventArgs e)
        {
            int index = itemsGrid.Items.IndexOf(e.ClickedItem);
            if (index == -1) return;

            var groups = (List<StiReportData.StiGroupInfoList>)cvs1.Source;

            var parameters = new object[]
                {
                    groups,
                    groups[index]
                };
            ((Frame)Window.Current.Content).Navigate(typeof(StiDemoCategoryPage), parameters);
        }

        private void ItemsGridSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            buttonDeleteCategory.Visibility = (NavigatorReportsType == ReportsType.Custom && itemsGrid.SelectedItem != null) ? Visibility.Visible : Visibility.Collapsed;
            CheckSeparator();
        }

        private void ButtonAddReportClick(object sender, RoutedEventArgs e)
        {
            var groups = (List<StiReportData.StiGroupInfoList>)cvs1.Source;

            var parameters = new object[]
                {
                    groups,
                    null
                };
            ((Frame)Window.Current.Content).Navigate(typeof(StiCreateNewReportWizardPage), parameters);
        }

        private async void ButtonDeleteCategoryClick(object sender, RoutedEventArgs e)
        {
            int index = itemsGrid.Items.IndexOf(itemsGrid.SelectedItem);
            if (index == -1) return;

            var groups = ((List<StiReportData.StiGroupInfoList>)cvs1.Source);
            groups.RemoveAt(index);

            await StiCustomReportsHelper.SaveDataAsync(groups);

            cvs1.Source = null;
            itemsGrid.ItemsSource = null;

            cvs1.Source = groups;
            itemsGrid.ItemsSource = cvs1.View.CollectionGroups;
        }

        #endregion
    }
}