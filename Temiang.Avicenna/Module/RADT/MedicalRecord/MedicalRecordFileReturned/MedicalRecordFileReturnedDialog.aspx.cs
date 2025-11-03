using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.RADT.MedicalRecord
{
    public partial class MedicalRecordFileReturnedDialog : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var fb = new MedicalRecordFileBorrowed();
                fb.LoadByPrimaryKey(Request.QueryString["trn"]);

                var pat = new Patient();
                pat.LoadByPrimaryKey(fb.PatientID);

                this.Title = pat.MedicalNo + " (" + (pat.FirstName + " " + pat.MiddleName + " " + pat.LastName).Trim() + ")";
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

            if (string.IsNullOrEmpty(txtReturnByName.Text))
            {
                ShowInformationHeader("Return By is required.");
                return false;
            }

            var fb = new MedicalRecordFileBorrowed();
            fb.LoadByPrimaryKey(Request.QueryString["trn"]);
            fb.ReturnByName = txtReturnByName.Text;
            fb.LastUpdateByUserID = AppSession.UserLogin.UserID;
            fb.LastUpdateDateTime = DateTime.Now;
            fb.Save();

            return true;
        }

    }
}
