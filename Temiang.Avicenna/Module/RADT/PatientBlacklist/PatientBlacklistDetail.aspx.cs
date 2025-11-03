using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT
{
    public partial class PatientBlacklistDetail : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.PatientBlacklist;

            if (!IsPostBack)
            {
                
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //var pat = new Patient();
                //pat.LoadByPrimaryKey(Request.QueryString["patientId"]);
                //txtNotes.Text = pat.Notes;
            }
        }

        public override bool OnButtonOkClicked()
        {
            Validate();

            if (!IsValid) return false;

            using (esTransactionScope trans = new esTransactionScope())
            {
                //update Patient
                var entity = new Patient();
                entity.LoadByPrimaryKey(Request.QueryString["patientId"]);
                entity.IsBlackList = true;

                //save  
                entity.Save();

                var hist = new PatientBlackListHistory();
                hist.AddNew();
                hist.PatientID = Request.QueryString["patientId"];
                hist.IsBlackList = true;
                hist.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                hist.LastUpdateByUserID = AppSession.UserLogin.UserID;
                hist.Notes = txtNotes.Text;

                hist.Save();
               
                //Commit if success, Rollback if failed
                trans.Complete();
            }

            return true;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = 'rebind'";
        }
    }
}
