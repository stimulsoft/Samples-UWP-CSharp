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
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using Stimulsoft.Helper.UWP.ResourcesHelper;
using Windows.Storage;
using Stimulsoft.Base.Localization;

namespace Navigator.RT.Reports
{
    internal static class StiReportData
    {
        #region StiGroupInfoList
        internal class StiGroupInfoList : List<StiReportItem>
        {
            #region Properties
            private readonly string desc;
            public string Desc
            {
                get
                {
                    return desc;
                }
            }

            private readonly string key;
            public string Key
            {
                get
                {
                    return key;
                }
            }

            public string LabelReportsInCategory
            {
                get
                {
                    return StiLocalization.Get("NavigatorRT", "ReportsInCategory");
                }
            }
            #endregion

            public StiGroupInfoList(string key, string desc)
            {
                this.key = key;
                this.desc = desc;
            }
        }
        #endregion

        #region Methods
        private async static Task<List<StiGroupInfoList>> LoadReportsAsync(StorageFile storageFile)
        {
            #region Read Xml
            var groups = new List<StiGroupInfoList>();
            StiGroupInfoList currentGroup = null;

            var stream = await storageFile.OpenStreamForReadAsync();
            var tr = XmlReader.Create(stream);

            string category = null;
            tr.Read();
            tr.Read();
            tr.Read();
            while (!tr.EOF)
            {
                if (tr.NodeType == XmlNodeType.EndElement)
                {
                    tr.Read();
                    tr.Read();
                    continue;
                }

                #region Category
                switch (tr.Name)
                {
                    case "Category":
                        {
                            category = tr.GetAttribute("name");
                            string desc = tr.GetAttribute("desc");

                            currentGroup = new StiGroupInfoList(category, desc);
                            groups.Add(currentGroup);

                            tr.Read();
                        }
                        break;

                    case "Report":
                        {
                            string name = tr.GetAttribute("name");
                            string file = tr.GetAttribute("file");
                            string reportDesc = tr.GetAttribute("desc");
                            tr.Read();

                            currentGroup.Add(new StiReportItem(category, name, reportDesc, file));
                        }
                        break;
                }
                #endregion

                tr.Read();
            }

            tr.Dispose();
            stream.Dispose();
            #endregion

            return groups;
        }

        async public static Task<List<StiGroupInfoList>> GetReportsByCategoryAsync(string fileName, bool useLocalFolder)
        {
            var result = new List<StiGroupInfoList>();
            StorageFile storageFile;

            fileName = "StimulsoftResources\\Navigator\\" + fileName;
            if (useLocalFolder)
                storageFile = await StiResourcesStorageHelper.LoadResourceFileFromLocalFolderAsync(fileName);
            else
                storageFile = await StiResourcesStorageHelper.LoadResourceFileAsync(fileName);

            if (storageFile == null)
                goto exitLabel;

            result = await LoadReportsAsync(storageFile);

        exitLabel:
            return result;
        }
        #endregion

        #region StiReportItem
        internal sealed class StiReportItem
        {
            #region Fields
            private readonly string Category;
            public readonly string FileName;
            #endregion

            #region Properties
            private readonly string header;
            public string Header
            {
                get
                {
                    return header;
                }
            }

            private readonly string desc;
            public string Desc
            {
                get
                {
                    return desc;
                }
            }
            #endregion

            public StiReportItem(string category, string header, string desc, string fileName)
            {
                this.header = header;
                this.desc = desc;
                this.Category = category;
                this.FileName = fileName;
            }
        }
        #endregion
    }
}