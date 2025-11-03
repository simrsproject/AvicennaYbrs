using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using System.Data;
using System.Text.RegularExpressions;
using System.Collections;
using System.Data.SqlTypes;
using System.Text;
using System.Web.Services;
using Temiang.Avicenna.BusinessObject.Common;
using Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.Emr
{
    public partial class CpptVerification : BasePageDialog
    {

        public string ReferFromRegistrationNo
        {
            get { return Request.QueryString["fregno"]; }
        }
        public string ParamedicID
        {
            get { return Request.QueryString["parid"]; }
        }
        public bool IsDpjp
        {
            get { return Request.QueryString["isdpjp"].ToBoolean(); }
        }

        private string CpptID
        {
            get
            {
                var keys = Request.QueryString["cpptid"].Split('_');
                return keys[0];
            }
        }

        private bool IsFromAskep
        {
            get
            {
                var keys = Request.QueryString["cpptid"].Split('_');
                return keys[1] == "True"; // From Askep
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ElectronicMedicalRecord;
            FooterVisible = false;
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (IsFromAskep) // From Askep
                {
                    var ndt = new NursingDiagnosaTransDT();
                    ndt.LoadByPrimaryKey(CpptID.ToInt());

                    txtDpjpNotes.Text = ndt.DpjpNotes;
                }
                else
                {
                    var rim = new RegistrationInfoMedic();
                    rim.LoadByPrimaryKey(CpptID);
                    txtDpjpNotes.Text = rim.DpjpNotes;
                }
            }
        }



        protected void btnVerif_Click(object sender, EventArgs e)
        {
            SaveNote(true);

            var script = "<script type='text/javascript'>CloseAndApply();</script>";
            //Create Startup Javascript for close window              
            Page.ClientScript.RegisterStartupScript(this.GetType(), "closeMe", script);

        }

        protected void btnSaveNote_Click(object sender, EventArgs e)
        {
            SaveNote(false);

            var script = "<script type='text/javascript'>CloseAndApply();</script>";
            //Create Startup Javascript for close window              
            Page.ClientScript.RegisterStartupScript(this.GetType(), "closeMe", script);

        }
        protected void btnVerifAll_Click(object sender, EventArgs e)
        {
            SaveNote(false);
            ApproveWithAllPreviouse(RegistrationNo, ReferFromRegistrationNo, ParamedicID,IsDpjp);

            var script = "<script type='text/javascript'>CloseAndApply();</script>";
            //Create Startup Javascript for close window              
            Page.ClientScript.RegisterStartupScript(this.GetType(), "closeMe", script);
        }
        private void SaveNote(bool iswithApprove)
        {
            if (IsFromAskep) // From Askep
            {
                var ndt = new NursingDiagnosaTransDT();
                ndt.LoadByPrimaryKey(CpptID.ToInt());

                ndt.DpjpNotes = txtDpjpNotes.Text;

                if (iswithApprove)
                {
                    ndt.IsApproved = true;
                    ndt.ApprovedDatetime = DateTime.Now;
                    ndt.ApprovedByUserID = AppSession.UserLogin.UserID;
                }

                ndt.Save();
            }
            else
            {
                var rim = new RegistrationInfoMedic();
                rim.LoadByPrimaryKey(CpptID);
                rim.DpjpNotes = txtDpjpNotes.Text;
                if (iswithApprove)
                {
                    rim.IsApproved = true;
                    rim.ApprovedDatetime = DateTime.Now;
                    rim.ApprovedByUserID = AppSession.UserLogin.UserID;
                }

                rim.Save();
            }
        }

        public void ApproveWithAllPreviouse(string registrationNo, string referFromRegistrationNo, string paramedicID, bool isDpjp)
        {
            DateTime? startTime = null;
            if (IsFromAskep) // From Askep
            {
                var ndt = new NursingDiagnosaTransDT();
                ndt.LoadByPrimaryKey(CpptID.ToInt());

                startTime = ndt.CreateDateTime;
            }
            else
            {
                var rim = new RegistrationInfoMedic();
                rim.LoadByPrimaryKey(CpptID);
                startTime = rim.DateTimeInfo;
            }

            var query = new NursingDiagnosaTransDTQuery("a");
            var nshd = new NursingTransHDQuery("b");
            query.InnerJoin(nshd).On(query.TransactionNo == nshd.TransactionNo);

            query.Where(query.Or(nshd.RegistrationNo == registrationNo,
                nshd.RegistrationNo == referFromRegistrationNo));
            query.Where(query.Or(query.SRNursingDiagnosaLevel == "31", query.SRNursingDiagnosaLevel == "40"));

            query.Where(query.CreateDateTime <= startTime);
            query.Where(query.Or(query.IsApproved.IsNull(), query.IsApproved == false));

            // Jika DPJP maka filter hanya utk dokter tsb dan yg tidak diset
            if (isDpjp)
                query.Where(query.Or(query.ParamedicID.IsNull(),query.ParamedicID==string.Empty, query.ParamedicID == paramedicID));
            else
            {
                // Jika bukan DPJP maka filter hanya utk dokter tsb
                query.Where(query.ParamedicID == paramedicID);
            }

            query.Select(query);

            var nurColl = new NursingDiagnosaTransDTCollection();
            nurColl.Load(query);

            foreach (var nur in nurColl)
            {
                nur.IsApproved = true;
                nur.ApprovedDatetime = DateTime.Now;
                nur.ApprovedByUserID = AppSession.UserLogin.UserID;
            }

            nurColl.Save();

            var rimColl = new RegistrationInfoMedicCollection();
            rimColl.Query.Where(rimColl.Query.Or(rimColl.Query.RegistrationNo == registrationNo,
                rimColl.Query.RegistrationNo == referFromRegistrationNo));
            rimColl.Query.Where(rimColl.Query.DateTimeInfo <= startTime);
            rimColl.Query.Where(rimColl.Query.Or(rimColl.Query.IsApproved.IsNull(), rimColl.Query.IsApproved == false));

            // Jika DPJP maka filter hanya utk dokter tsb dan yg tidak diset
            if (isDpjp)
                rimColl.Query.Where(rimColl.Query.Or(rimColl.Query.ParamedicID.IsNull(),rimColl.Query.ParamedicID==string.Empty, rimColl.Query.ParamedicID == paramedicID));
            else
            {
                // Jika bukan DPJP maka filter hanya utk dokter tsb
                rimColl.Query.Where(rimColl.Query.ParamedicID == paramedicID);
            }

            rimColl.LoadAll();

            foreach (var rim in rimColl)
            {
                // Cek tipe asesmen jangan di approved krn jadi idak bisa diedit lagi
                var pas = new PatientAssessment();
                if (!pas.LoadByPrimaryKey(rim.RegistrationInfoMedicID))
                {
                    rim.IsApproved = true;
                    rim.ApprovedDatetime = DateTime.Now;
                    rim.ApprovedByUserID = AppSession.UserLogin.UserID;
                    rim.ApprovedDatetime = DateTime.Now;
                }
            }

            rimColl.Save();
        }

    }
}
