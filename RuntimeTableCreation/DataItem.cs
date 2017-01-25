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

namespace RuntimeTableCreation
{
    public class DataItem
    {
        #region Properties
        private string column1;
        public string Column1
        {
            get
            {
                return column1;
            }
            set
            {
                column1 = value;
            }
        }

        private string column2;
        public string Column2
        {
            get
            {
                return column2;
            }
            set
            {
                column2 = value;
            }
        }

        private bool column3;
        public bool Column3
        {
            get
            {
                return column3;
            }
            set
            {
                column3 = value;
            }
        }
        #endregion

        public DataItem(string column1, string column2, bool column3)
        {
            this.column1 = column1;
            this.column2 = column2;
            this.column3 = column3;
        }
    }
}