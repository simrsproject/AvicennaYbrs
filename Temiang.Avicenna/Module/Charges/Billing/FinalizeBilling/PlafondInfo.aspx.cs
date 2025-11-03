using System;
using System.Linq;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI.WebControls;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.Charges.Billing.FinalizeBilling
{
    public partial class PlafondInfo : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.VerificationFinalizeBilling;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var reg = new Registration();
                reg.LoadByPrimaryKey(Request.QueryString["regNo"]);
                var pat = new Patient();
                pat.LoadByPrimaryKey(reg.PatientID);

                Page.Title = "Plafond Information Detail for " + pat.PatientName;

                (Helper.FindControlRecursive(this, "btnOk") as Button).Visible = false;
                (Helper.FindControlRecursive(this, "btnCancel") as Button).Visible = false;
            }
        }

        protected void grdPlafondHistory_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            ((RadGrid)source).DataSource = RegistrationPlafondHistorys();
        }

        private DataTable RegistrationPlafondHistorys()
        {
            var query = new RegistrationPlafondHistoryQuery("a");
            var guarQ = new GuarantorQuery("b");
            var reg = new RegistrationQuery("c");

            query.Select(
                query.HistoryID,
                query.RegistrationNo,
                query.GuarantorID,
                query.PlafondAmount,
                query.LastUpdateDateTime,
                query.LastUpdateByUserID,
                guarQ.GuarantorName.As("GuarantorName"),
                reg.BpjsSepNo
                );
            query.InnerJoin(guarQ).On(query.GuarantorID == guarQ.GuarantorID);
            query.InnerJoin(reg).On(query.RegistrationNo == reg.RegistrationNo);
            query.Where(query.RegistrationNo == Request.QueryString["regNo"]);
            query.OrderBy(query.LastUpdateDateTime.Ascending, query.HistoryID.Ascending);

            var dtb = query.LoadDataTable();

            return dtb;
        }

        public override bool OnButtonOkClicked()
        {
            Validate();

            if (!IsValid)
                return false;

            return true;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = 'rebind'";
        }

        public static DataTable SelectGrouper(string noSEP)
        {
            if (noSEP == null)
                noSEP = string.Empty;

            var grouper = new BpjsSEPQuery();
            grouper.Select(grouper.NoSEP, grouper.KodeCBG, grouper.DeskripsiCBG, grouper.TariffCBG);
            grouper.Where(grouper.NoSEP == noSEP);
            return grouper.LoadDataTable();
        }

        public static DataTable SelectCmg(string noSEP)
        {
            if (noSEP == null)
                noSEP = string.Empty;

            var grouper = new BpjsCMGQuery();
            grouper.Select(grouper.NoSEP, grouper.KodeCMG, grouper.DeskripsiCMG, grouper.TariffCMG, grouper.TipeCMG);
            grouper.Where(grouper.NoSEP == noSEP, grouper.IsOptionCMG == false);
            return grouper.LoadDataTable();
        }

        public static DataTable SelectCmgOption(string noSEP)
        {
            if (noSEP == null)
                noSEP = string.Empty;

            var grouper = new BpjsCMGQuery();
            grouper.Select(grouper.NoSEP, grouper.KodeCMG, grouper.DeskripsiCMG, grouper.TipeCMG);
            grouper.Where(grouper.NoSEP == noSEP, grouper.IsOptionCMG == true);
            return grouper.LoadDataTable();
        }
    }
}
