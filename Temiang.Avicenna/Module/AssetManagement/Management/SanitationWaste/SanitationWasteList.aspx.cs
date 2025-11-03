using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.AssetManagement.Management
{
    public partial class SanitationWasteList : BasePageList
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
            UrlPageSearch = "SanitationWasteSearch.aspx?type=" + getPageID;
            UrlPageDetail = "SanitationWasteDetail.aspx?type=" + getPageID;

            ProgramID = getPageID == "rec" ? AppConstant.Program.SanitationWasteReceipt : AppConstant.Program.SanitationWasteDisposal;

            this.WindowSearch.Height = 400;

            if (!IsPostBack)
            {
                grdList.Columns[2].Visible = getPageID == "rec";
                grdList.Columns[3].Visible = getPageID == "dis";
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
            string id = dataItem.GetDataKeyValue(SanitationWasteTransMetadata.ColumnNames.TransactionNo).ToString();
            Page.Response.Redirect("SanitationWasteDetail.aspx?md=" + mode + "&id=" + id + "&type=" + getPageID, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
                grdList.DataSource = SanitationWasteTranss;
        }

        private DataTable SanitationWasteTranss
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                SanitationWasteTransQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (SanitationWasteTransQuery)Session[SessionNameForQuery];
                else
                {
                    query = new SanitationWasteTransQuery("a");
                    var fromunit = new ServiceUnitQuery("b");
                    var supp = new SupplierQuery("c");

                    query.Select
                        (
                            query.TransactionNo,
                            query.TransactionDate,
                            fromunit.ServiceUnitName.As("FromServiceUnitName"),
                            supp.SupplierName,
                            query.Notes,
                            query.IsApproved,
                            query.IsVoid
                        );

                    query.LeftJoin(fromunit).On(fromunit.ServiceUnitID == query.FromServiceUnitID);
                    query.LeftJoin(supp).On(supp.SupplierID == query.SupplierID);

                    if (getPageID == "rec")
                    {
                        query.Select(@"<'SanitationWasteDetail.aspx?md=view&id='+a.TransactionNo+'&type=rec' AS RUrl>");
                        query.Where(query.TransactionCode == "R");
                    }
                    else
                    {
                        query.Select(@"<'SanitationWasteDetail.aspx?md=view&id='+a.TransactionNo+'&type=dis' AS RUrl>");
                        query.Where(query.TransactionCode == "D");
                    }

                    query.OrderBy(query.TransactionDate.Descending, query.TransactionNo.Descending);
                }

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;

                return dtb;
            }
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = e.DetailTableView.ParentItem;
            string transNo = dataItem.GetDataKeyValue("TransactionNo").ToString();

            var query = new SanitationWasteTransItemQuery("a");
            var typeq = new AppStandardReferenceItemQuery("b");

            query.Select
                (
                    query.TransactionNo,
                    query.SRWasteType,
                    typeq.ItemName.As("WasteTypeName"),
                    query.Qty
                );
            query.InnerJoin(typeq).On(typeq.StandardReferenceID == AppEnum.StandardReference.WasteType && typeq.ItemID == query.SRWasteType);
            query.Where(query.TransactionNo == transNo);
            query.OrderBy(query.SRWasteType.Ascending);

            DataTable dtb = query.LoadDataTable();

            //Apply
            e.DetailTableView.DataSource = dtb;
        }
    }
}