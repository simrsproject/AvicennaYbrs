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
    public partial class PhysicianTeamInfo : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            //ProgramID = AppConstant.Program.RssaVerificationFinalizeBilling;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var reg = new Registration();
                reg.LoadByPrimaryKey(Request.QueryString["regNo"]);
                var pat = new Patient();
                pat.LoadByPrimaryKey(reg.PatientID);

                Title = "Physcian Team Information for : " + pat.PatientName + " [MRN: " + pat.MedicalNo + " / Reg#: " + reg.RegistrationNo + "]";

                (Helper.FindControlRecursive(this, "btnOk") as Button).Visible = false;
                (Helper.FindControlRecursive(this, "btnCancel") as Button).Visible = false;
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            ((RadGrid)source).DataSource = PhysicianTemas();
        }

        private DataTable PhysicianTemas()
        {
            var query = new ParamedicTeamQuery("a");
            var par = new ParamedicQuery("b");
            var std = new AppStandardReferenceItemQuery("c");

            query.InnerJoin(par).On(query.ParamedicID == par.ParamedicID);
            query.LeftJoin(std).On
                (
                    query.SRParamedicTeamStatus == std.ItemID &
                    std.StandardReferenceID == "ParamedicTeamStatus"
                );
            query.Select
                (
                    query.RegistrationNo,
                    query.ParamedicID,
                    par.ParamedicName,
                    std.ItemName.As("ParamedicTeamStatus"),
                    query.StartDate,
                    query.EndDate,
                    query.Notes
                );
            query.Where(query.RegistrationNo == Request.QueryString["regNo"]);

            query.OrderBy(query.StartDate.Ascending);

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
