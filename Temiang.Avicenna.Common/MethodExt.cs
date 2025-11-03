using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Common
{
    public static class MethodExt
    {
        #region RadWindow Extend
        public static void ShowAfterPostback(this RadWindow win, string url, string scriptKey, bool isMaximize)
        {
            string script = string.Concat("function f(){var oWnd = $find(\"" + win.ClientID + "\");oWnd.setUrl('", url, "'); oWnd.show();", isMaximize ? "oWnd.Maximize();" : string.Empty, "Sys.Application.remove_load(f);}Sys.Application.add_load(f);");
            ScriptManager.RegisterStartupScript(win.Page, win.Page.GetType(), scriptKey, script, true);
        }
        #endregion

        #region UserControl Extend
        public static void ShowMessageAfterPostback(this UserControl ctl, string message)
        {
            string script = string.Concat("function f(){", string.Format("alert('{0}');", message), "Sys.Application.remove_load(f);}Sys.Application.add_load(f);");
            ScriptManager.RegisterStartupScript(ctl, ctl.GetType(), "msg", script, true);
        }


        public static void InitializeCultureGrid(this RadGrid grd)
        {
            var dateCultureInfo = AppConstant.DisplayFormat.DateCultureInfo;
            var numericCultureInfo = AppConstant.DisplayFormat.NumericCultureInfo;

            InitializeCultureGrid(dateCultureInfo, numericCultureInfo, grd);
        }

        private static void InitializeCultureGrid(CultureInfo dateCultureInfo, CultureInfo numericCultureInfo, RadGrid grd)
        {
            string dateFormat;
            var colCount = grd.Columns.Count;
            for (var i = 0; i < colCount; i++)
            {
                var gridDateTimeColumn = grd.Columns[i] as GridDateTimeColumn;
                if (gridDateTimeColumn != null)
                {
                    if (!string.IsNullOrEmpty(gridDateTimeColumn.DataField) && gridDateTimeColumn.DataField.ToLower().Contains("datetime"))
                        dateFormat = string.Format("{0} {1}", dateCultureInfo.DateTimeFormat.ShortDatePattern, dateCultureInfo.DateTimeFormat.ShortTimePattern);
                    else
                        dateFormat = dateCultureInfo.DateTimeFormat.ShortDatePattern;

                    gridDateTimeColumn.DataFormatString = "{0:" + dateFormat + "}";
                }
            }
            if (grd.MasterTableView.DetailTables.Count > 0)
            {
                colCount = grd.MasterTableView.DetailTables[0].Columns.Count;
                for (var i = 0; i < colCount; i++)
                {
                    var gridDateTimeColumn = grd.MasterTableView.DetailTables[0].Columns[i] as GridDateTimeColumn;
                    if (gridDateTimeColumn != null)
                    {
                        if (!string.IsNullOrEmpty(gridDateTimeColumn.DataField) && gridDateTimeColumn.DataField.ToLower().Contains("datetime"))
                            dateFormat = string.Format("{0} {1}", dateCultureInfo.DateTimeFormat.ShortDatePattern, dateCultureInfo.DateTimeFormat.ShortTimePattern);
                        else
                            dateFormat = dateCultureInfo.DateTimeFormat.ShortDatePattern;

                        gridDateTimeColumn.DataFormatString = "{0:" + dateFormat + "}";
                    }
                }
            }
        }

        #endregion

        #region Page Extend
        public static void ShowMessageAfterPostback(this Page page, string message)
        {
            string script = string.Concat("function f(){", string.Format("alert('{0}');", message), "Sys.Application.remove_load(f);}Sys.Application.add_load(f);");
            ScriptManager.RegisterStartupScript(page, page.GetType(), "msg", script, true);
        }

        public static void RegisterStartupScriptExt(this Page page, string scriptKey, string script)
        {
            script = string.Concat("function f(){", script, ";Sys.Application.remove_load(f);}Sys.Application.add_load(f);");
            ScriptManager.RegisterStartupScript(page, page.GetType(), scriptKey, script, true);
        }
        #endregion

    }
}
