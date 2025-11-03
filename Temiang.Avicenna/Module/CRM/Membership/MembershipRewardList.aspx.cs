using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI.WebControls;
using System.Linq;

namespace Temiang.Avicenna.Module.CRM
{
    public partial class MembershipRewardList : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var pmd = new BusinessObject.MembershipDetail();
                pmd.LoadByPrimaryKey(Request.QueryString["mid"].ToInt());

                var pm = new Membership();
                pm.LoadByPrimaryKey(pmd.MembershipNo);

                var pat = new Patient();
                pat.LoadByPrimaryKey(pm.PatientID);

                Page.Title = "Payment Transaction List for Membership#: " + pm.MembershipNo + " [" + pat.PatientName + " / MRN: " + pat.MedicalNo + "]";

                (Helper.FindControlRecursive(this, "btnOk") as Button).Visible = false;
                (Helper.FindControlRecursive(this, "btnCancel") as Button).Visible = false;
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            ((RadGrid)source).DataSource = PaymentHistorys();
        }

        private DataTable PaymentHistorys()
        {
            var query = new TransPaymentQuery("a");
            var rq = new RegistrationQuery("b");
            var pq = new PatientQuery("c");
            var unitq = new ServiceUnitQuery("d");
            var parq = new ParamedicQuery("e");
            
            query.InnerJoin(rq).On(rq.RegistrationNo == query.RegistrationNo);
            query.InnerJoin(pq).On(pq.PatientID == rq.PatientID);
            query.InnerJoin(unitq).On(unitq.ServiceUnitID == rq.ServiceUnitID);
            query.LeftJoin(parq).On(parq.ParamedicID == rq.ParamedicID);
            query.Select
                (
                    query.PaymentNo,
                    query.PaymentDate,
                    query.PaymentTime,
                    query.TransactionCode,
                    query.RegistrationNo,
                    pq.MedicalNo,
                    pq.PatientName,
                    unitq.ServiceUnitName,
                    parq.ParamedicName,
                    "<(SELECT ISNULL(SUM(f.[Amount]),0) FROM TransPaymentItem f WHERE a.[PaymentNo] = f.[PaymentNo] AND f.[SRPaymentType] = 'PaymentType-002') AS 'TotalPaymentAmount'>",
                    "<0 AS RewardPoint>"
                );
            query.Where(query.TransactionCode.In(TransactionCode.Payment, TransactionCode.PaymentReturn), query.IsApproved == true, rq.MembershipDetailID == Request.QueryString["mid"].ToInt());
            query.OrderBy(query.PaymentDate.Descending, query.PaymentTime.Descending);
            
            var dtb = query.LoadDataTable();

            foreach (DataRow row in dtb.Rows)
            {
                var tcode = row["TransactionCode"].ToString();
                var div = AppSession.Parameter.MultipleForRewardPoints;
                var point = (Convert.ToDecimal(row["TotalPaymentAmount"]) / div).ToInt();

                row["RewardPoint"] = tcode == TransactionCode.Payment ? point : point * (-1);
            }

            dtb.AcceptChanges();

            return dtb;
        }

        public override bool OnButtonOkClicked()
        {
            Validate();

            if (!IsValid)
                return false;

            return true;
        }
    }
}