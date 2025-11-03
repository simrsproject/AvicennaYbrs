using System;
using System.Linq;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI.WebControls;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class MergeBillingInfo : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var reg = new Registration();
                reg.LoadByPrimaryKey(Request.QueryString["regNo"]);
                var pat = new Patient();
                pat.LoadByPrimaryKey(reg.PatientID);

                Page.Title = "Merge Billing History for : " + pat.PatientName + " [MRN: " + pat.MedicalNo + " / Reg#: " + reg.RegistrationNo + "]";

                (Helper.FindControlRecursive(this, "btnOk") as Button).Visible = false;
                (Helper.FindControlRecursive(this, "btnCancel") as Button).Visible = false;
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            ((RadGrid)source).DataSource = MergeBillingHistorys();
        }

        private DataTable MergeBillingHistorys()
        {
            var query = new MergeBillingHistoryQuery("a");
            var rbq = new RegistrationQuery("b");
            var raq = new RegistrationQuery("c");
            var usr = new AppUserQuery("u");

            query.LeftJoin(rbq).On(query.FromRegistrationNoBefore == rbq.RegistrationNo);
            query.LeftJoin(raq).On(query.FromRegistrationNoAfter == raq.RegistrationNo);
            query.LeftJoin(usr).On(query.LastUpdateByUserID == usr.UserID);
            query.Select
                (
                    query.RegistrationNo,
                    query.FromRegistrationNoBefore,
                    query.FromRegistrationNoAfter,
                    query.LastUpdateDateTime,
                    usr.UserName.As("UsrUpdate")
                );
            query.Where(query.RegistrationNo == Request.QueryString["regNo"]);
            query.OrderBy(query.LastUpdateDateTime.Ascending);

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
    }
}
