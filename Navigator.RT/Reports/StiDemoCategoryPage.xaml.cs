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
using Navigator.RT.CustomReports;
using Stimulsoft.Base.Localization;
using Stimulsoft.Controls.UWP.Controls;
using Stimulsoft.Controls.UWP.Helpers;
using Stimulsoft.Helper.UWP.ResourcesHelper;
using Stimulsoft.Report;
using Windows.Storage;
using Windows.UI.StartScreen;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Automation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Navigator.RT.Reports
{
    internal sealed partial class StiDemoCategoryPage :
        StiPageBase
    {
        public StiDemoCategoryPage()
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

        private bool secondaryTileExits = true;
        private List<StiReportData.StiGroupInfoList> groups;

        #endregion

        #region StiPageBase.override

        protected override void BlockPage(bool useDictionaryPanel)
        {
            bottomAppBar.IsEnabled = false;
            headerControl.BackButton.IsEnabled = false;
            itemsGrid.IsEnabled = false;
        }

        protected override void UnblockPage()
        {
            bottomAppBar.IsEnabled = true;
            headerControl.BackButton.IsEnabled = true;
            itemsGrid.IsEnabled = true;
        }

        #endregion

        #region Properties

        private static Data _data;
        internal static Data GetData()
        {
            return _data ?? (_data = new Data());
        }

        #endregion

        #region Methods.override

        protected override void OnNavigatedTo(Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var parameters = (object[])e.Parameter;
            groups = (List<StiReportData.StiGroupInfoList>)parameters[0];
            var group = (StiReportData.StiGroupInfoList)parameters[1];

            cvs1.Source = group;

            this.textBlockKey.Text = group.Key;
            this.textBlockDesc.Text = group.Desc;

            if (StiDemoReportsPage.NavigatorReportsType != ReportsType.Demo) return;

            buttonRemove.Visibility = Visibility.Collapsed;
            buttonAddReport.Visibility = Visibility.Collapsed;
        }

        protected override void Localize()
        {
            this.headerControl.Title = StiLocalization.Get("NavigatorRT", "PageCategoryReports");

            AutomationProperties.SetName(buttonPinReport, StiLocalization.Get("NavigatorRT", "Pin"));
            AutomationProperties.SetName(buttonAddReport, StiLocalization.Get("NavigatorRT", "AddReports"));
            AutomationProperties.SetName(buttonRemove, StiLocalization.Get("NavigatorRT", "RemoveReport"));
        }

        #endregion

        #region Methods.SecondaryTile

        private void UpdateSecondaryTile(string tileId)
        {
            var exists = SecondaryTile.Exists(tileId);
            if (exists == secondaryTileExits) return;

            if (exists)
            {
                buttonPinReport.Content = "";
                AutomationProperties.SetName(buttonPinReport, StiLocalization.Get("NavigatorRT", "Unpin"));
            }
            else
            {
                buttonPinReport.Content = "";
                AutomationProperties.SetName(buttonPinReport, StiLocalization.Get("NavigatorRT", "Pin"));
            }

            secondaryTileExits = exists;
        }

        #endregion

        #region Handlers

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            ((Frame)Window.Current.Content).GoBack();
        }

        private async void ButtonRemoveClick(object sender, RoutedEventArgs e)
        {
            var group = (StiReportData.StiGroupInfoList)cvs1.Source;
            var item = itemsGrid.SelectedItem as StiReportData.StiReportItem;
            group.Remove(item);

            await StiCustomReportsHelper.SaveDataAsync(groups);

            cvs1.Source = null;
            itemsGrid.ItemsSource = null;

            cvs1.Source = group;
        }

        private void ItemsGridSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var isReport = false;

            var item = itemsGrid.SelectedItem as StiReportData.StiReportItem;
            if (item != null)
            {
                isReport = true;
                UpdateSecondaryTile(item.FileName);
            }

            buttonPinReport.IsEnabled = isReport;

            if (StiDemoReportsPage.NavigatorReportsType == ReportsType.Custom)
            {
                buttonRemove.IsEnabled = isReport;
            }
        }

        private async void ItemsGridItemClick(object sender, ItemClickEventArgs e)
        {
            StiReport reportTmp = null;
            var item = e.ClickedItem as StiReportData.StiReportItem;
            if (item != null && !string.IsNullOrEmpty(item.FileName))
            {
                itemsGrid.IsEnabled = false;
                bottomAppBar.IsEnabled = false;
                progressRing.IsActive = true;
                progressRing.Visibility = Visibility.Visible;

                StorageFile storageFile;

                if (StiDemoReportsPage.NavigatorReportsType == ReportsType.Demo)
                {
                    storageFile = await StiResourcesStorageHelper.LoadResourceFileAsync("StimulsoftResources\\Navigator\\Reports\\" + (item.FileName + ".mrt"));
                    if (storageFile != null)
                    {
                        reportTmp = new StiReport();
                        await reportTmp.LoadAsync(storageFile);
                        reportTmp.RegBusinessObject("Data", "Data", GetData());
                        await reportTmp.RenderAsync();
                    }
                }
                else
                {
                    storageFile = await StiResourcesStorageHelper.LoadResourceFileFromLocalFolderAsync("StimulsoftResources\\Navigator\\CustomReports\\Reports\\" + (item.FileName + ".mdc"));
                    if (storageFile != null)
                    {
                        reportTmp = new StiReport();
                        await reportTmp.LoadDocumentAsync(storageFile);
                    }
                }

                itemsGrid.IsEnabled = true;
                bottomAppBar.IsEnabled = true;
                progressRing.IsActive = false;
                progressRing.Visibility = Visibility.Collapsed;
            }

            ((Frame)Window.Current.Content).Navigate(typeof(MainPage), reportTmp);
        }

        private async void ButtonPinReportClick(object sender, RoutedEventArgs e)
        {
            var item = itemsGrid.SelectedItem as StiReportData.StiReportItem;
            if (item == null) return;

            SecondaryTile secondaryTile;
            var rect = StiGlobalHelper.GetElementRect(buttonPinReport);

            if (SecondaryTile.Exists(item.FileName))
            {
                secondaryTile = new SecondaryTile(item.FileName);
                await secondaryTile.RequestDeleteForSelectionAsync(rect, Windows.UI.Popups.Placement.Above);
            }
            else
            {
                var logo = new Uri("ms-appx:///Assets/LogoSecondary.png");
                var smallLogo = new Uri("ms-appx:///Assets/SmallLogo.png");

                if (StiDemoReportsPage.NavigatorReportsType == ReportsType.Demo)
                    secondaryTile = new SecondaryTile(item.FileName, item.FileName, item.FileName, item.FileName + ".mrt", TileOptions.ShowNameOnLogo, logo);
                else
                    secondaryTile = new SecondaryTile(item.FileName, item.Header, item.FileName, item.FileName + ".mdc", TileOptions.ShowNameOnLogo, logo);

                secondaryTile.ForegroundText = ForegroundText.Dark;
                secondaryTile.SmallLogo = smallLogo;
                await secondaryTile.RequestCreateForSelectionAsync(rect, Windows.UI.Popups.Placement.Above);
            }

            UpdateSecondaryTile(item.FileName);
        }

        private void ButtonAddReportClick(object sender, RoutedEventArgs e)
        {
            var parameters = new object[]
                {
                    groups,
                    null
                };
            ((Frame)Window.Current.Content).Navigate(typeof(StiCreateNewReportWizardPage), parameters);
        }

        #endregion
    }
}