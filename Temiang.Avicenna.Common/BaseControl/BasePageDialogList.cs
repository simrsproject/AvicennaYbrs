using System;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Common
{
    public class BasePageDialogList : BasePage
    {
        private RadGrid _firstGrid;
        private RadWindow _windowSearch;

        public virtual string OnGetScriptToolBarNewClicking()
        {
            var script = "openWindowEntry('mod=new');";
            return script;
        }

        public virtual string OnGetScriptToolBarExportClicking()
        {
            return string.Empty;
        }

        public virtual string UrlPageEntry
        {
            get { return string.Empty; }
        }

        public virtual int WidthWindowEntry
        {
            get { return 1000; }
        }
        public virtual int HeightWindowEntry
        {
            get { return 600; }
        }
        protected RadAjaxManager AjaxManager
        {
            get { return (RadAjaxManager)Helper.FindControlRecursive(this.Master, "fw_RadAjaxManager"); }
        }

        protected RadGrid FirstGrid
        {
            get { return _firstGrid ?? (_firstGrid = Helper.FindFirstRadGridControl(this)); }
        }

        private RadToolBar _toolBarMenu;
        public RadToolBar ToolBarMenu
        {
            get { return _toolBarMenu ?? (_toolBarMenu = (RadToolBar)Helper.FindControlRecursive(this, "fw_tbarMain")); }
        }


        public RadToolBarItem ToolBarMenuAdd
        {
            get
            {
                return ToolBarMenu.Items[0];
            }
        }

        public RadToolBarItem ToolBarMenuExport
        {
            get
            {
                return ToolBarMenu.Items[1];
            }
        }

        public RadToolBarItem ToolBarMenuQuickSearch
        {
            get { return ToolBarMenu.Items[3]; }
        }
        public RadTextBox ToolBarMenuQuickSearchText
        {
            get { return (RadTextBox)ToolBarMenu.Items[3].FindControl("txtQuickSearch"); }
        }

        protected override void OnInitComplete(EventArgs e)
        {
            base.OnInitComplete(e);
            ToolBarMenu.ButtonClick += tbar_ButtonClick;

            //Grid List
            //FirstGrid.AllowPaging = true;
            //FirstGrid.PageSize = 18;
            //FirstGrid.AllowSorting = true;
            //FirstGrid.AutoGenerateColumns = false;
            //FirstGrid.ClientSettings.Selecting.AllowRowSelect = true;
            //FirstGrid.ClientSettings.Resizing.AllowColumnResize = true;
            //FirstGrid.PagerStyle.Mode = GridPagerMode.NextPrevNumericAndAdvanced;
            //FirstGrid.ShowStatusBar = true;

            //var loadingPanel = (RadAjaxLoadingPanel)Helper.FindControlRecursive(this.Master, "fw_ajxLoadingPanel");
            //AjaxManager.AjaxSettings.AddAjaxSetting(ToolBarMenu, ToolBarMenu);
            AjaxManager.AjaxSettings.AddAjaxSetting(ToolBarMenu, FirstGrid);
            AjaxManager.AjaxSettings.AddAjaxSetting(FirstGrid, FirstGrid);
            OnInitializeAjaxManager(AjaxManager);
        }


        protected virtual void OnInitializeAjaxManager(RadAjaxManager ajaxManager)
        {
        }

        private void tbar_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            var args = new ValidateArgs();

            GridDataItem[] items;
            switch (e.Item.Value)
            {
                case "refresh":
                    break;
                case "exporttoexcel":
                    OnMenuExportToExcelClick(args);
                    if (!args.IsCancel)
                    {
                        FirstGrid.ExportSettings.IgnorePaging = true;
                        FirstGrid.MasterTableView.ExportToExcel();
                    }
                    break;
                case "exporttoword":
                    FirstGrid.ExportSettings.IgnorePaging = true;
                    FirstGrid.MasterTableView.ExportToWord();
                    break;
                case "exporttopdf":
                    FirstGrid.ExportSettings.IgnorePaging = true;
                    FirstGrid.MasterTableView.ExportToPdf();
                    break;
            }
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
        {
            base.RaisePostBackEvent(source, eventArgument);
            if (source == FirstGrid)
            {
                if (eventArgument.Equals("quicksearch") || eventArgument.Equals("refresh") || eventArgument.Equals("rebind"))
                {
                    FirstGrid.Rebind();
                }
            }
        }

        public virtual void OnMenuExportToExcelClick(ValidateArgs args)
        {

        }
        protected virtual void ApplyQuickSearch(esDynamicQuery query)
        {
            var quickSearch = ToolBarMenuQuickSearchText.Text;
            if (quickSearch.Trim() != string.Empty && quickSearch.Contains(" "))
            {
                var searchs = quickSearch.Trim().Split(' ');
                foreach (var search in searchs)
                    ApplyQuickSearch(search, query);
            }
            else
                ApplyQuickSearch(quickSearch, query);
        }
        private void ApplyQuickSearch(string search, esDynamicQuery query)
        {
            var grd = FirstGrid;
            var i = 0;
            var comparisons = new object[2];
            foreach (object col in grd.MasterTableView.Columns)
            {
                if (col is GridBoundColumn)
                {
                    var boundCol = (GridBoundColumn)col;
                    if (boundCol.Visible == true && boundCol.Display == true)
                    {
                        i++;
                        if (i > 2) // 2 kolom saja
                            break;

                        var qitem = new esQueryItem(query, boundCol.DataField, esSystemType.String);
                        var searchLike = string.Format("%{0}%", search);
                        comparisons[i - 1] = qitem.Like(searchLike);
                    }
                }
            }
            if (i > 0)
                query.Where(query.Or(comparisons));
        }

        protected void ApplyQuickSearch(esDynamicQuery mainQuery, params string[] fieldNames)
        {
            ApplyQuickSearch(mainQuery, mainQuery, fieldNames);
        }

        protected void ApplyQuickSearch(esDynamicQuery mainQuery, esDynamicQuery whereQuery, params string[] fieldNames)
        {
            var quickSearch = ToolBarMenuQuickSearchText.Text;
            if (string.IsNullOrWhiteSpace(quickSearch)) return;
            if (quickSearch.Contains(" "))
            {
                var searchs = quickSearch.Trim().Split(' ');
                foreach (var search in searchs)
                    ApplyQuickSearch(search, mainQuery, whereQuery, fieldNames);
            }
            else
                ApplyQuickSearch(quickSearch, mainQuery, whereQuery, fieldNames);
        }
        private void ApplyQuickSearch(string search, esDynamicQuery mainQuery, esDynamicQuery whereQuery, params string[] fieldNames)
        {
            var i = 0;
            var comparisons = new object[fieldNames.Length];
            foreach (string fieldName in fieldNames)
            {
                var qitem = new esQueryItem(whereQuery, fieldName, esSystemType.String);
                var searchLike = string.Format("%{0}%", search);
                comparisons[i] = qitem.Like(searchLike);
                i++;
            }
            if (i > 0)
                mainQuery.Where(whereQuery.Or(comparisons));
        }
    }
}