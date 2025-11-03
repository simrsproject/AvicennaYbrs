using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Charges.Dispensary.PrescriptionSales
{
    public partial class PrescriptionSalesPosList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            UrlPageSearch = "##";//"PrescriptionSalesPosSearch.aspx";
            UrlPageDetail = "PrescriptionSalesDetail.aspx?md=new&regno=&type=" + Request.QueryString["type"] +
                            "&mode=pos&rt=" + Request.QueryString["rt"] + "&ono=";

            this.WindowSearch.Height = 400;

            ProgramID = AppConstant.Program.PrescriptionSalesPos;

            // Quick Search
            ToolBarMenuQuickSearch.Visible = true;
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
            string id = dataItem.GetDataKeyValue(TransPrescriptionMetadata.ColumnNames.PrescriptionNo).ToString();
            Page.Response.Redirect("PrescriptionSalesDetail.aspx?md=" + mode + "&id=" + id + "&regno=&type=" + Request.QueryString["type"] +
                            "&mode=pos&rt=" + Request.QueryString["rt"] + "&ono=", true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = TransPrescriptions;
        }

        private DataTable TransPrescriptions
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                TransPrescriptionQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (TransPrescriptionQuery)Session[SessionNameForQuery];
                else
                {
                    query = new TransPrescriptionQuery("a");
                    var unit = new ServiceUnitQuery("b");
                    query.InnerJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);
                    query.Select
                        (
                            query.PrescriptionNo,
                            query.PrescriptionDate,
                            query.AdditionalNote,
                            query.ServiceUnitID,
                            unit.ServiceUnitName,
                            query.IsApproval,
                            query.IsVoid
                        );
                    query.Where(query.IsPrescriptionReturn == false, query.IsPos.IsNotNull(), query.IsPos == true);
                    query.OrderBy(query.PrescriptionNo.Descending);

                    //Quick Search
                    ApplyQuickSearch(query, "AdditionalNote", "PrescriptionNo");
                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}
