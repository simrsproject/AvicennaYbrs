using System;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Common
{
    public class BasePageList : BasePage
    {
        private RadGrid _firstGrid;
        private RadWindow _windowSearch;

        public virtual string OnGetScriptToolBarNewClicking()
        {
            var script = string.Empty;
            if (!string.IsNullOrEmpty(UrlPageList))
                script = "window.location.href= '" + UrlPageList + "'; args.set_cancel(true);";
            else
                script = "window.location.href= '" + (UrlPageDetailNew == string.Empty ? UrlPageDetail + "?md=new" : UrlPageDetailNew) + "'; args.set_cancel(true);";
            return script;
        }

        //public virtual string OnGetScriptToolBarExportClicking()
        //{
        //    return UrlPageDetailImport;
        //}

        public virtual string OnGetScriptToolBarImportClicking()
        {
            return UrlPageDetailImport;
        }

        protected RadWindow WindowSearch
        {
            get
            {
                return _windowSearch ?? (_windowSearch = (RadWindow)Helper.FindControlRecursive(this.Master, "fw_WinSearch"));
            }
        }

        public string UrlPageDetailImport
        {
            get
            {
                if (ViewState["fw_UrlPageDetailImport"] == null)
                    ViewState["fw_UrlPageDetailImport"] = string.Empty;
                return (string)ViewState["fw_UrlPageDetailImport"];
            }
            set { ViewState["fw_UrlPageDetailImport"] = value; }
        }

        public string UrlPageDetail
        {
            get
            {
                if (ViewState["fw_UrlPageDetail"] == null)
                    ViewState["fw_UrlPageDetail"] = string.Empty;
                return (string)ViewState["fw_UrlPageDetail"];
            }
            set { ViewState["fw_UrlPageDetail"] = value; }
        }

        public string UrlPageDetailNew
        {
            get
            {
                if (ViewState["fw_UrlPageDetailNew"] == null)
                    ViewState["fw_UrlPageDetailNew"] = string.Empty;
                return (string)ViewState["fw_UrlPageDetailNew"];
            }
            set { ViewState["fw_UrlPageDetailNew"] = value; }
        }

        public string UrlPageSearch
        {
            get
            {
                if (ViewState["fw_UrlPageSearch"] == null)
                    ViewState["fw_UrlPageSearch"] = string.Empty;
                return (string)ViewState["fw_UrlPageSearch"];
            }
            set { ViewState["fw_UrlPageSearch"] = value; }
        }

        public string UrlPageRejournal
        {
            get
            {
                if (ViewState["fw_UrlPageRejournal"] == null)
                    ViewState["fw_UrlPageRejournal"] = string.Empty;
                return (string)ViewState["fw_UrlPageRejournal"];
            }
            set { ViewState["fw_UrlPageRejournal"] = value; }
        }

        public string UrlPageRebalanceJournal
        {
            get
            {
                if (ViewState["fw_UrlPageRebalanceJournal"] == null)
                    ViewState["fw_UrlPageRebalanceJournal"] = string.Empty;
                return (string)ViewState["fw_UrlPageRebalanceJournal"];
            }
            set { ViewState["fw_UrlPageRebalanceJournal"] = value; }
        }

        public string UrlPageBPJSSearch
        {
            get
            {
                if (ViewState["fw_UrlPageBPJSSearch"] == null)
                    ViewState["fw_UrlPageBPJSSearch"] = string.Empty;
                return (string)ViewState["fw_UrlPageBPJSSearch"];
            }
            set { ViewState["fw_UrlPageBPJSSearch"] = value; }
        }

        public string UrlPageList
        {
            get
            {
                if (ViewState["fw_UrlPageList"] == null)
                    ViewState["fw_UrlPageList"] = "";
                return (string)ViewState["fw_UrlPageList"];
            }
            set { ViewState["fw_UrlPageList"] = value; }
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

        public RadToolBarItem ToolBarMenuList
        {
            get
            {
                return ToolBarMenu.Items[11];
            }
        }

        public RadToolBarItem ToolBarMenuView
        {
            get
            {
                return ToolBarMenu.Items[0];
            }
        }

        public RadToolBarItem ToolBarMenuAdd
        {
            get
            {
                return ToolBarMenu.Items[1];
            }
        }

        public RadToolBarItem ToolBarMenuEdit
        {
            get
            {
                return ToolBarMenu.Items[2];
            }
        }

        public RadToolBarItem ToolBarMenuExport
        {
            get
            {
                return ToolBarMenu.Items[3];
            }
        }

        public RadToolBarItem ToolBarMenuImport
        {
            get
            {
                return ToolBarMenu.Items[4];
            }
        }

        public RadToolBarItem ToolBarMenuHome
        {
            get
            {
                return ToolBarMenu.Items[7];
            }
        }

        public RadToolBarItem ToolBarMenuSearch
        {
            get
            {
                return ToolBarMenu.Items[5];
            }
        }
        public RadToolBarItem ToolBarMenuQuickSearch
        {
            get { return ToolBarMenu.Items[8]; }
        }
        public RadTextBox ToolBarMenuQuickSearchText
        {
            get { return (RadTextBox)ToolBarMenu.Items[8].FindControl("txtQuickSearch"); }
        }
        public RadToolBarItem ToolBarMenuRejournal
        {
            get
            {
                return ToolBarMenu.Items[9];
            }
        }

        public RadToolBarItem ToolBarMenuRebalance
        {
            get
            {
                return ToolBarMenu.Items[10];
            }
        }

        public RadToolBarItem ToolBarMenuBPJSSearch
        {
            get
            {
                return ToolBarMenu.Items[12];
            }
        }

        protected override void OnInitComplete(EventArgs e)
        {
            base.OnInitComplete(e);
            ToolBarMenu.ButtonClick += tbar_ButtonClick;

            //Grid List
            FirstGrid.AllowPaging = true;
            FirstGrid.PageSize = 18;
            FirstGrid.AllowSorting = true;
            FirstGrid.AutoGenerateColumns = false;
            FirstGrid.ClientSettings.Selecting.AllowRowSelect = true;
            FirstGrid.ClientSettings.Resizing.AllowColumnResize = true;
            FirstGrid.PagerStyle.Mode = GridPagerMode.NextPrevNumericAndAdvanced;
            FirstGrid.ShowStatusBar = true;
            var loadingPanel = (RadAjaxLoadingPanel)Helper.FindControlRecursive(this.Master, "fw_ajxLoadingPanel");
            AjaxManager.AjaxSettings.AddAjaxSetting(FirstGrid, FirstGrid, loadingPanel);
            AjaxManager.AjaxSettings.AddAjaxSetting(FirstGrid, FirstGrid);
            OnInitializeAjaxManager(AjaxManager);
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            //User Access
            UserAccess access = this.UserAccess;
            if (access.IsExist)
            {
                ToolBarMenuView.Enabled = true; // access.IsProgramEditAble; //View
                ToolBarMenuAdd.Enabled = access.IsProgramAddAble && access.IsAddAble; //New
                ToolBarMenuEdit.Enabled = access.IsProgramEditAble && access.IsEditAble; //Edit
                ToolBarMenuExport.Enabled = access.IsExportAble; //Export
                ToolBarMenuImport.Enabled = access.IsExportAble; //Export
                ToolBarMenuHome.Visible = access.IsMenuHomeVisible; // Home
                ToolBarMenuAdd.Visible = access.IsMenuAddVisible; // New
                ToolBarMenuExport.Visible = access.IsProgramExportAble; //Export --> ditambahkan kondisi ini supaya tidak membingungkan user krn dianggap semua list bisa diexport kalo hanya enabled, tinggal diaktifkan saja
                ToolBarMenuImport.Visible = access.IsProgramExportAble; //Export
            }
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
                case "home":
                    Page.Response.Redirect(UserAccess.NavigateUrl);
                    break;

                case "view":
                    items = FirstGrid.MasterTableView.GetSelectedItems();
                    if (items.Length == 0 && FirstGrid.MasterTableView.DetailTables.Count > 0)
                        items = FirstGrid.MasterTableView.DetailTables[0].GetSelectedItems();

                    if (items.Length > 0)
                        OnMenuViewClick(items);
                    break;
                case "edit":
                    items = FirstGrid.MasterTableView.GetSelectedItems();
                    if (items.Length == 0 && FirstGrid.MasterTableView.DetailTables.Count > 0)
                        items = FirstGrid.MasterTableView.DetailTables[0].GetSelectedItems();

                    if (items.Length > 0)
                        OnMenuEditClick(items);
                    break;
                case "refresh":
                    break;
                case "exporttotxt":
                    OnMenuExportToTextClick(args);
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
                case "rebalance":
                    OnMenuRebalanceClick(args);
                    if (args.MessageText == "success")
                        ScriptManager.RegisterStartupScript(this, GetType(), "rebalanced", "alert('Rebalance process completed');", true);
                    break;
                default:
                    OnToolBarMenuDataAdditionalButtonClick(args, e.Item.Value);
                    break;
            }
        }
        protected virtual void OnToolBarMenuDataAdditionalButtonClick(ValidateArgs args, string value)
        {

        }
        public virtual void OnMenuEditClick(GridDataItem[] dataItems)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public virtual void OnMenuViewClick(GridDataItem[] dataItems)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
        {
            base.RaisePostBackEvent(source, eventArgument);
            if (source == FirstGrid)
            {
                if (eventArgument.Equals("quicksearch"))
                {
                    Session.Remove(SessionNameForQuery);
                    Session.Remove(SessionNameForList);
                    FirstGrid.CurrentPageIndex = 0;
                    FirstGrid.Rebind();
                }
                else if (eventArgument.Equals("refresh"))
                {
                    Session.Remove(SessionNameForQuery);
                    Session.Remove(SessionNameForList);
                    FirstGrid.CurrentPageIndex = 0;
                    FirstGrid.Rebind();
                }
                else if (eventArgument.Equals("rebind"))
                {
                    Session.Remove(SessionNameForList);
                    FirstGrid.CurrentPageIndex = 0;
                    FirstGrid.Rebind();
                }
            }
        }

        public virtual void OnMenuRebalanceClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public virtual void OnMenuExportToExcelClick(ValidateArgs args)
        {

        }
        public virtual void OnMenuExportToTextClick(ValidateArgs args)
        {

        }
        public virtual string OnGetAdditionalMenuScript()
        {
            return String.Empty;
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

        protected void ApplyQuickSearch(esDynamicQuery query, params string[] fieldNames)
        {
            var quickSearch = ToolBarMenuQuickSearchText.Text;
            if (quickSearch.Trim() != string.Empty && quickSearch.Contains(" "))
            {
                var searchs = quickSearch.Trim().Split(' ');
                foreach (var search in searchs)
                    ApplyQuickSearch(search, query, fieldNames);
            }
            else
                ApplyQuickSearch(quickSearch, query, fieldNames);
        }
        private void ApplyQuickSearch(string search, esDynamicQuery query, params string[] fieldNames)
        {
            var i = 0;
            var comparisons = new object[fieldNames.Length];
            foreach (string fieldName in fieldNames)
            {
                var qitem = new esQueryItem(query, fieldName, esSystemType.String);
                var searchLike = string.Format("%{0}%", search);
                comparisons[i] = qitem.Like(searchLike);
                i++;
            }
            if (i > 0)
                query.Where(query.Or(comparisons));
        }
    }
}