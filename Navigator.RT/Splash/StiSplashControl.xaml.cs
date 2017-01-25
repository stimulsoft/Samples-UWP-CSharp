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
using Navigator.RT.Reports;
using Stimulsoft.Helper.UWP.ResourcesHelper;
using Stimulsoft.Report;
using Stimulsoft.Report.UWP.Helpers;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Stimulsoft.Base.Localization;

namespace Navigator.RT.Splash
{
    public sealed partial class StiSplashControl : UserControl
    {
        #region Fields
        private readonly string args;
        private readonly IStorageItem fileActivated;
        #endregion

        #region Handlers
        private async void StiSplashControlLoaded(object sender, RoutedEventArgs e)
        {
            panelProgress.Children.Clear();

            var item = new StiSplashLoadingItemControl(StiLocalization.Get("NavigatorRT", "ProgressInformations"), 0);
            panelProgress.Children.Add(item);

            item = new StiSplashLoadingItemControl(StiLocalization.Get("NavigatorRT", "ViewerImagesInitialization"), 1);
            panelProgress.Children.Add(item);
            bool result = await StiReportImagesStore.InitStoreAsync();

            if (!result)
                goto errorLabel;

            item = new StiSplashLoadingItemControl(StiLocalization.Get("NavigatorRT", "IndicatorImageInitialization"), 1);
            panelProgress.Children.Add(item);
            result = await StiIndicatorsImagesStore.InitStoreAsync();

            if (!result)
                goto errorLabel;

            #region Check

            StiReport report = null;

            if (!string.IsNullOrEmpty(args))
            {
                StorageFile storageFile;
                report = new StiReport();

                if (args.EndsWith(".mrt", System.StringComparison.Ordinal))
                {
                    storageFile = await StiResourcesStorageHelper.LoadResourceFileAsync("StimulsoftResources\\Navigator\\Reports\\" + args);
                    if (storageFile != null)
                    {
                        await report.LoadAsync(storageFile);
                        report.RegBusinessObject("Data", "Data", StiDemoCategoryPage.GetData());

                        await report.RenderAsync();
                    }
                }
                else if (args.EndsWith(".mdc", System.StringComparison.Ordinal))
                {
                    storageFile = await StiResourcesStorageHelper.LoadResourceFileFromLocalFolderAsync("StimulsoftResources\\Navigator\\CustomReports\\Reports\\" + args);
                    if (storageFile != null)
                    {
                        await report.LoadDocumentAsync(storageFile);
                    }
                }
            }
            else if (fileActivated != null)
            {
                var storageFile = (StorageFile) fileActivated;
                     
                report = new StiReport();
                await report.LoadDocumentAsync(storageFile);

                if (storageFile.DisplayName.EndsWith("DeleteFile", System.StringComparison.Ordinal))
                    await storageFile.DeleteAsync();
            }

            if (report != null)
            {
                var frame = Window.Current.Content as Frame;

                if (frame == null)
                {
                    frame = new Frame();
                    Window.Current.Content = frame;
                }

                frame.Navigate(typeof(MainPage), report);
                return;
            }
            #endregion

            item = new StiSplashLoadingItemControl(StiLocalization.Get("NavigatorRT", "Starting"), 0);
            panelProgress.Children.Add(item);

            var rootFrame = new Frame();
            rootFrame.Navigate(typeof(StiDemoReportsPage));
            Window.Current.Content = rootFrame;

            return;

        errorLabel:
            item = new StiSplashLoadingItemControl(StiLocalization.Get("NavigatorRT", "FailedLoadResources"), 2);
            panelProgress.Children.Add(item);
        }
        #endregion

        public StiSplashControl(string args, IStorageItem fileActivated)
        {
            this.args = args;
            this.fileActivated = fileActivated;

            InitializeComponent();
            this.Loaded += StiSplashControlLoaded;
        }
    }
}