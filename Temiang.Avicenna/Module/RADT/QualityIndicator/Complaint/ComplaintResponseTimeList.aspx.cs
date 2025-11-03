using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.RADT.QualityIndicator.Complaint
{
    public partial class ComplaintResponseTimeList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            WindowSearch.Height = 400;

            UrlPageSearch = "ComplaintResponseTimeSearch.aspx";
            UrlPageDetail = "ComplaintResponseTimeDetail.aspx";

            ProgramID = AppConstant.Program.ComplaintResponseTime;
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
            string id = dataItem.GetDataKeyValue(ComplaintResponseTimeMetadata.ColumnNames.ComplaintNo).ToString();
            Page.Response.Redirect("ComplaintResponseTimeDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = ComplaintResponseTimes;
        }

        private DataTable ComplaintResponseTimes
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }

                ComplaintResponseTimeQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (ComplaintResponseTimeQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new ComplaintResponseTimeQuery("a");
                    var qpat = new PatientQuery("b");
                    
                    query.LeftJoin(qpat).On(qpat.PatientID == query.PatientID);
                    
                    query.OrderBy
                        (
                            query.ComplaintDate.Descending, query.ComplaintNo.Descending
                        );

                    query.Select(
                        query.ComplaintNo,
                        query.ComplaintDate,
                        query.CustomerName,
                        query.PatientID,
                        qpat.MedicalNo,
                        qpat.PatientName,
                        query.CustomerAddress,
                        query.PhoneNo,
                        query.IsApproved,
                        query.IsVoid
                        );
                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}