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
using System.Threading.Tasks;
using Stimulsoft.Report;
using Navigator.RT.Reports;
using Windows.Storage;
using System.Text;

namespace Navigator.RT.CustomReports
{
    internal static class StiCustomReportsHelper
    {
        async public static Task AddCustomReportAsync(List<StiReportData.StiGroupInfoList> groups, StiReport report, string category, string caregoryDesc, string reportName, string reportDesc)
        {
            var fileName = Guid.NewGuid().ToString().Replace("-", string.Empty);

            #region Add Report
            var addReport = false;
            foreach (var g in groups.Where(g => g.Key == category))
            {
                g.Add(new StiReportData.StiReportItem(category, reportName, reportDesc, fileName));
                addReport = true;
            }

            if (!addReport)
            {
                var group = new StiReportData.StiGroupInfoList(category, caregoryDesc);

                group.Add(new StiReportData.StiReportItem(category, reportName, reportDesc, fileName));
                groups.Add(group);
            }
            #endregion

            #region Write Xml
            await SaveDataAsync(groups);
            #endregion

            var storageFile = await ApplicationData.Current.LocalFolder.CreateFileAsync("StimulsoftResources\\Navigator\\CustomReports\\Reports\\" + (fileName + ".mdc"), CreationCollisionOption.ReplaceExisting);
            await report.SaveDocumentAsync(storageFile);
        }

        public async static Task SaveDataAsync(List<StiReportData.StiGroupInfoList> groups)
        {
            #region Write Xml
            var writer = new StringBuilder();

            writer.Append("<Demo>\r\n");
            foreach (var group in groups)
            {
                writer.AppendFormat("<Category name=\"{0}\" desc=\"{1}\">\r\n", group.Key, group.Desc);

                foreach (var rpt in group)
                {
                    writer.AppendFormat("<Report name=\"{0}\" file=\"{1}\" desc=\"{2}\"/>\r\n", rpt.Header, rpt.FileName, rpt.Desc);
                }

                writer.Append("</Category>\r\n");
            }
            writer.Append("</Demo>");

            var storageFile = await ApplicationData.Current.LocalFolder.CreateFileAsync("StimulsoftResources\\Navigator\\CustomReports\\Reports.xml", CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(storageFile, writer.ToString());
            #endregion
        }
    }
}