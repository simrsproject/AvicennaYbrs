using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;
using System.Data;

namespace Temiang.Avicenna.Module.Charges.ServiceUnitBookingStatus
{
    public partial class VoidDialog : BasePageDialog
    {
        private string errorMsg;

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ServiceUnitBookingStatus;

            if (!IsPostBack)
            {
            }

            //var btkOk = (Button)Helper.FindControlRecursive(Master, "btnOk");
            //btkOk.Visible = this.IsUserVoidAble || this.IsUserDeleteAble;
        }

        private void VoidBooking(string bookingNo)
        {
            if (txtVoidNotes.Text == string.Empty)
            {
                errorMsg = "Void Reason required.";
                ViewState["result" + Request.UserHostName] = string.Empty;
                return;
            }

            var sub = new ServiceUnitBooking();
            sub.LoadByPrimaryKey(bookingNo);

            //sering ada double journal untuk void
            if (sub.IsVoid == true)
            {
                errorMsg = "Void failed. This transaction has been voided.";
                ViewState["result" + Request.UserHostName] = string.Empty;
                return;
            }

            if (sub.IsApproved == true)
            {
                errorMsg = "Void failed. This transaction has been approved.";
                ViewState["result" + Request.UserHostName] = string.Empty;
                return;
            }

            using (var trans = new esTransactionScope())
            {
                sub.IsVoid = true;
                sub.VoidReason = txtVoidNotes.Text;
                sub.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                sub.LastUpdateByUserID = AppSession.UserLogin.UserID;
                sub.Save();

                trans.Complete();
            }
            errorMsg = string.Empty;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = 'rebind'";
        }

        public override bool OnButtonOkClicked()
        {
            VoidBooking(Request.QueryString["id"]);
            if (!string.IsNullOrEmpty(errorMsg))
            {
                ShowInformationHeader(errorMsg);
                return false;
            }

            return true;
        }
    }
}