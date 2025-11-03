using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.AssetManagement
{
    public partial class WorkOrderClosingDialog : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.Title = "Edit Accepted By for Order No : " + Request.QueryString["wono"];
                var wo = new AssetWorkOrder();
                wo.LoadByPrimaryKey(Request.QueryString["wono"]);
                txtAcceptedBy.Text = wo.AcceptedBy;
            }
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = 'rebind'";
        }

        public override bool OnButtonOkClicked()
        {
            if (!Page.IsValid)
                return false;

            if (string.IsNullOrEmpty(txtAcceptedBy.Text))
            {
                ShowInformationHeader("Accepted By is required.");
                return false;
            }

            if (txtAcceptedBy.Text.Length < 3)
            {
                ShowInformationHeader("Accepted By must be more than or equal 3 characters long.");
                return false;
            }

            var wo = new AssetWorkOrder();
            wo.LoadByPrimaryKey(Request.QueryString["wono"]);
            wo.SRWorkStatus = AppSession.Parameter.WorkStatusClosed;
            wo.AcceptedByUserID = AppSession.UserLogin.UserID;
            wo.AcceptedDateTime = (new DateTime()).NowAtSqlServer();
            wo.AcceptedBy = txtAcceptedBy.Text;
            wo.LastUpdateByUserID = AppSession.UserLogin.UserID;
            wo.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            wo.Save();

            return true;
        }
    }
}
