using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Charges.DownPayment
{
    public partial class PatientList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "PatientSearch.aspx?type=" + Request.QueryString["type"];
            UrlPageDetailNew = "PatientDetail.aspx?md=new&type=" + Request.QueryString["type"];

            if (Request.QueryString["type"] == "deposit")
            {
                ProgramID = AppConstant.Program.PatientDepositReceive;
                grdList.Columns[0].HeaderText = "Payment No";
                grdList.Columns[4].HeaderText = "Payment Date";
            }
            else
            {
                ProgramID = AppConstant.Program.PatientDepositReturn;
                grdList.Columns[0].HeaderText = "Return No";
                grdList.Columns[4].HeaderText = "Return Date";
            }
            WindowSearch.Height = 300;
        }

        public override void OnMenuEditClick(Telerik.Web.UI.GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "edit");
        }

        public override void OnMenuViewClick(Telerik.Web.UI.GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "view");
        }

        private void RedirectToPageDetail(Telerik.Web.UI.GridDataItem dataItem, string mode)
        {
            string id = dataItem.GetDataKeyValue(TransPaymentPatientMetadata.ColumnNames.PaymentNo).ToString();
            Page.Response.Redirect("PatientDetail.aspx?md=" + mode + "&id=" + id + "&type=" + Request.QueryString["type"], true);
        }

        protected void grdList_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = DownPayments;
        }

        private System.Data.DataTable DownPayments
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null) return ((System.Data.DataTable)(obj));

                TransPaymentPatientQuery query;
                if (Session[SessionNameForQuery] != null) query = (TransPaymentPatientQuery)Session[SessionNameForQuery];
                else
                {
                    query = new TransPaymentPatientQuery("a");
                    var patient = new PatientQuery("b");
                    var sal = new AppStandardReferenceItemQuery("sal");
                    query.InnerJoin(patient).On(query.PatientID == patient.PatientID);
                    query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & patient.SRSalutation == sal.ItemID);
                    query.Select
                        (
                            query,
                            patient.MedicalNo,
                            patient.PatientName,
                            sal.ItemName.As("SalutationName"),
                            "<ISNULL((SELECT SUM(tppi.Amount) FROM TransPaymentPatientItem AS tppi WHERE tppi.PaymentNo = a.PaymentNo), 0) AS Amount>"
                        );
                    if (ProgramID == AppConstant.Program.PatientDepositReceive)
                        query.Where(query.TransactionCode == Temiang.Avicenna.BusinessObject.Reference.TransactionCode.DownPayment);
                    else
                        query.Where(query.TransactionCode == Temiang.Avicenna.BusinessObject.Reference.TransactionCode.DownPaymentReturn);
                    query.OrderBy(query.PaymentNo.Descending);
                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;

                System.Data.DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;

                return dtb;
            }
        }
    }
}
