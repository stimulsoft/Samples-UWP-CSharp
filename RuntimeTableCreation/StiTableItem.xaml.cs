#region Copyright (C) 2003-2013 Stimulsoft
/*
{*******************************************************************}
{																	}
{	Stimulsoft Reports.RT											}
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
using Windows.UI.Xaml.Controls;

namespace RuntimeTableCreation
{
    public sealed partial class StiTableItem : UserControl
    {
        #region Properties
        public string Column1
        {
            get
            {
                return tbColumn1.Text;
            }
        }
        
        public string Column2
        {
            get
            {
                return tbColumn2.Text;
            }
        }

        public bool Column3
        {
            get
            {
                return checkBoxColumn3.IsChecked.Value;
            }
        }
        #endregion

        public StiTableItem() : this(string.Empty, string.Empty, false)
        {

        }

        public StiTableItem(string column1, string column2, bool column3)
        {
            this.InitializeComponent();

            tbColumn1.Text = column1;
            tbColumn2.Text = column2;
            checkBoxColumn3.IsChecked = column3;
        }
    }
}