using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;


namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ParamedicScheduleList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            UrlPageSearch = "ParamedicScheduleSearch.aspx";
            UrlPageDetail = "ParamedicScheduleDetail.aspx";

            ProgramID = AppConstant.Program.ParamedicSchedule;

            this.WindowSearch.Height = 400;
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
            string serviceUnitID = dataItem.GetDataKeyValue(ParamedicScheduleMetadata.ColumnNames.ServiceUnitID).ToString();
            string paramedicID = dataItem.GetDataKeyValue(ParamedicScheduleMetadata.ColumnNames.ParamedicID).ToString();
            string periodYear = dataItem.GetDataKeyValue(ParamedicScheduleMetadata.ColumnNames.PeriodYear).ToString();
            Page.Response.Redirect("ParamedicScheduleDetail.aspx?md=" + mode + "&unit=" + serviceUnitID + "&pid=" + paramedicID + "&per=" + periodYear, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = ParamedicSchedules;
        }

        private DataTable ParamedicSchedules
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                ParamedicScheduleQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (ParamedicScheduleQuery)Session[SessionNameForQuery];
                else
                {
                    query = new ParamedicScheduleQuery("a");
                    var paramedicQuery = new ParamedicQuery("p");
                    var unit = new ServiceUnitQuery("c");
                    var tcode = new ServiceUnitTransactionCodeQuery("d");

                    query.Select
                        (
                            query.ServiceUnitID,
                            query.ParamedicID,
                            query.Notes,
                            query.PeriodYear,
                            unit.ServiceUnitName,
                            paramedicQuery.ParamedicName,
                            paramedicQuery.Address
                        );
                    query.InnerJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);
                    query.InnerJoin(paramedicQuery).On(query.ParamedicID == paramedicQuery.ParamedicID);
                    query.LeftJoin(tcode).On(tcode.ServiceUnitID == query.ServiceUnitID && tcode.SRTransactionCode == BusinessObject.Reference.TransactionCode.JobOrder.ToString());
                    query.Where(tcode.ServiceUnitID.IsNull());
                        
                    query.OrderBy(paramedicQuery.ParamedicName.Ascending);
                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;

                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;

                return dtb;
            }
        }
    }
}

