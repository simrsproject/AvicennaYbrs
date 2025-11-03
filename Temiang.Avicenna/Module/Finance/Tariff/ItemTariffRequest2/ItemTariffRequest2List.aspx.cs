using System;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.Finance.Tariff
{
    public partial class ItemTariffRequest2List : BasePageList
    {
        private string getPageID
        {
            get
            {
                return string.IsNullOrEmpty(Request.QueryString["type"]) ? string.Empty : Request.QueryString["type"];
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "ItemTariffRequest2Search.aspx?type=" + getPageID;
            UrlPageDetail = "ItemTariffRequest2Detail.aspx?type=" + getPageID;
            UrlPageDetailImport = "openWinImport();";

            this.WindowSearch.Height = 400;

            ProgramID = getPageID == "" ? AppConstant.Program.ItemServiceTariffRequest2 :
                (getPageID == "import" ? AppConstant.Program.ItemServiceTariffRequestImport : AppConstant.Program.ItemServiceTariffRequestImportNew);

            if (!IsPostBack)
            {
                grdList.Columns[7].Visible = getPageID != "";
            }
        }

        public override string OnGetScriptToolBarNewClicking()
        {
            string script = string.Format(@"gotoAddUrl('{0}'); args.set_cancel(true);", getPageID);
            return script;
        }

        public override void OnMenuEditClick(GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "edit");
        }

        public override void OnMenuViewClick(GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "view");
        }

        private void RedirectToPageDetail(GridDataItem dataItem, string mode)
        {
            string id = dataItem.GetDataKeyValue(ItemTariffRequest2Metadata.ColumnNames.TariffRequestNo).ToString();
            Page.Response.Redirect("ItemTariffRequest2Detail.aspx?md=" + mode + "&id=" + id + "&type=" + getPageID, true);
        }

        private DataTable ItemTariffRequest2s
        {
            get
            {
                object obj = Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }

                ItemTariffRequest2Query query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (ItemTariffRequest2Query)Session[SessionNameForQuery];
                }
                else
                {
                    query = new ItemTariffRequest2Query("a");
                    var tariffType = new AppStandardReferenceItemQuery("b");
                    var itemType = new AppStandardReferenceItemQuery("c");
                    var igroup = new ItemGroupQuery("d");
                    query.InnerJoin(tariffType).On(query.SRTariffType == tariffType.ItemID &
                                                   tariffType.StandardReferenceID ==
                                                   AppEnum.StandardReference.TariffType.ToString());
                    query.InnerJoin(itemType).On(query.SRItemType == itemType.ItemID &
                                                 itemType.StandardReferenceID ==
                                                 AppEnum.StandardReference.ItemType.ToString());
                    query.LeftJoin(igroup).On(igroup.ItemGroupID == query.ItemGroupID);

                    query.Select(query.SelectAllExcept(), itemType.ItemName.As("ItemTypeName"),
                                 tariffType.ItemName.As("TariffTypeName"), igroup.ItemGroupName);
                    query.Where(query.SRItemType.NotIn(ItemType.Medical, ItemType.NonMedical, ItemType.Kitchen));
                    if (getPageID == "")
                        query.Where(query.IsImport == false);
                    else if (getPageID == "import")
                        query.Where(query.IsImport == true, query.IsNew == false);
                    else
                        query.Where(query.IsImport == true, query.IsNew == true);
                    query.OrderBy(query.TariffRequestNo.Descending);
                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;
                return dtb;
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grdList.DataSource = new String[] { };
                return;
            }

            if (!e.IsFromDetailTable)
            {
                grdList.DataSource = ItemTariffRequest2s;
            }
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = e.DetailTableView.ParentItem;
            string tariffRequestNo = dataItem.GetDataKeyValue("TariffRequestNo").ToString();

            switch (e.DetailTableView.Name)
            {
                case "grdItem":
                    //Load record
                    var query = new ItemTariffRequest2ItemQuery("a");
                    var itemQuery = new ItemQuery("b");
                    var classQuery = new ClassQuery("c");
                    query.InnerJoin(itemQuery).On(query.ItemID == itemQuery.ItemID);
                    query.InnerJoin(classQuery).On(query.ClassID == classQuery.ClassID);
                    query.Select(query.SelectAllExcept(), classQuery.ClassName, itemQuery.ItemName);
                    query.Where(query.TariffRequestNo == tariffRequestNo);
                    DataTable dtb = query.LoadDataTable();

                    //Apply
                    e.DetailTableView.DataSource = dtb;
                    break;

                case "grdItemTariffRequestItemComp":
                    string itemId = dataItem.GetDataKeyValue("ItemID").ToString();
                    string classId = dataItem.GetDataKeyValue("ClassID").ToString();
                    //Load record
                    var itemCompQuery = new ItemTariffRequest2ItemCompQuery("a");
                    var compQuery = new TariffComponentQuery("b");
                    itemCompQuery.InnerJoin(compQuery).On(itemCompQuery.TariffComponentID == compQuery.TariffComponentID);
                    itemCompQuery.Select(itemCompQuery.SelectAllExcept(), compQuery.TariffComponentName);
                    itemCompQuery.Where(itemCompQuery.TariffRequestNo == tariffRequestNo,
                                        itemCompQuery.ClassID == classId, itemCompQuery.ItemID == itemId);
                    DataTable dtb2 = itemCompQuery.LoadDataTable();

                    //Apply
                    e.DetailTableView.DataSource = dtb2;
                    break;
            }
        }

        #region export / import
        private DataTable GetDataGridDataTable(string transactionNo)
        {
            esDynamicQuery query = GetQueryDetail(transactionNo);
            DataTable dtb = query.LoadDataTable();
            return dtb;
        }

        private esDynamicQuery GetQueryDetail(string transactionNo)
        {
            var itr = new ItemTariffRequest2();
            if (itr.LoadByPrimaryKey(transactionNo) && itr.IsImport == true)
            {
                if (itr.IsNew == false)
                {
                    ItemTariffRequestItemToImport.InsertToTableProcess(itr.TariffRequestNo, itr.StartingDate.Value.Date,
                                                                       itr.SRItemType, itr.ItemGroupID, itr.SRTariffType,
                                                                       itr.ImportFromDate ?? DateTime.Now.Date, AppSession.UserLogin.UserID);
                }
                else
                {
                    ItemTariffRequestItemToImport.InsertToTableProcessNew(itr.TariffRequestNo, itr.StartingDate.Value.Date,
                                                                      itr.ItemGroupID, itr.SRTariffType, AppSession.UserLogin.UserID);
                }
                    
            }

            var query = new ItemTariffRequestItemToImportQuery("a");
            var cls = new ClassQuery("c");
            var tcomp = new TariffComponentQuery("d");
            var tType = new AppStandardReferenceItemQuery("e");
            var igroup = new ItemGroupQuery("f");

            query.Select
                (
                    query.ReferenceNo,
                    query.StartingDate,
                    query.SRTariffType,
                    tType.ItemName.As("TariffTypeName"),
                    query.ItemGroupID,
                    igroup.ItemGroupName,
                    query.ItemID,
                    query.ItemName,
                    query.ClassID,
                    cls.ClassName,
                    query.TariffComponentID,
                    tcomp.TariffComponentName
                );

            if (getPageID == "import")
            {
                query.Select(
                    query.GeneralPrice,
                    query.OldPrice,
                    query.NewPrice);
            }
            else
            {
                query.Select(query.NewPrice);
            }

            query.InnerJoin(igroup).On(igroup.ItemGroupID == query.ItemGroupID);
            query.InnerJoin(cls).On(query.ClassID == cls.ClassID);
            query.InnerJoin(tcomp).On(query.TariffComponentID == tcomp.TariffComponentID);
            query.InnerJoin(tType).On(query.SRTariffType == tType.ItemID &&
                                     tType.StandardReferenceID == AppEnum.StandardReference.TariffType);

            query.Where(query.ReferenceNo == transactionNo);
            query.OrderBy(query.ItemGroupID.Ascending, query.ItemID.Ascending);

            return query;
        }

        public override void OnMenuExportToExcelClick(ValidateArgs args)
        {
            try
            {
                var items = grdList.MasterTableView.GetSelectedItems()[0];
                var table = GetDataGridDataTable(items.GetDataKeyValue("TariffRequestNo").ToString());
                if (table.Rows.Count > 0)
                {
                    var transNo = items.GetDataKeyValue("TariffRequestNo").ToString();

                    //Common.CreateExcelFile.CreateExcelDocument(table, transNo.Replace('/', '_') + ".xls", this.Response);
                    Common.CreateExcelFile.CreateExcelDocument(table, transNo.Replace('/', '_') + AppSession.Parameter.ExcelFileExtension, this.Response);
                }
            }
            catch (Exception e)
            {
                var error = e.Message;
                throw new Exception(error);
            }
            args.IsCancel = true;
        }
        #endregion
    }
}
