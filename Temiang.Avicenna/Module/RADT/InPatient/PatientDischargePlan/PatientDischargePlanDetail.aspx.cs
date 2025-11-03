using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.InPatient
{
    public partial class PatientDischargePlanDetail : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.PatientDischargePlan;

            if (!IsPostBack)
            {
                
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtRegistrationNo.Text = Request.QueryString["regno"];
                var reg = new Registration();
                reg.LoadByPrimaryKey(txtRegistrationNo.Text);
                txtRegistrationDate.SelectedDate = reg.RegistrationDate;
                txtRegistrationTime.Text = reg.RegistrationTime;
                if (reg.DischargePlanDate.HasValue)
                {
                    txtDischargePlanDate.SelectedDate = reg.DischargePlanDate;
                    txtDischargePlanTime.Text = reg.DischargePlanDate.Value.ToString("HH:mm");
                }
                var pat = new Patient();
                pat.LoadByPrimaryKey(reg.PatientID);
                txtMedicalNo.Text = pat.MedicalNo;
                var std = new AppStandardReferenceItem();
                txtSalutation.Text = std.LoadByPrimaryKey("Salutation", pat.SRSalutation) ? std.ItemName : string.Empty;
                txtPatientName.Text = pat.PatientName;
                txtGender.Text = pat.Sex;
                txtPlaceDOB.Text = string.Format("{0}, {1}", pat.CityOfBirth, Convert.ToDateTime(pat.DateOfBirth).ToString("dd-MMM-yyyy"));
                var su = new ServiceUnit();
                su.LoadByPrimaryKey(reg.ServiceUnitID);
                txtServiceUnit.Text = su.ServiceUnitName;
                var sr = new ServiceRoom();
                sr.LoadByPrimaryKey(reg.RoomID);
                txtRoomBed.Text = sr.RoomName + " / " + reg.BedID;
                var cl = new Class();
                cl.LoadByPrimaryKey(reg.ClassID);
                txtClass.Text = cl.ClassName;
                chkIsRoomIn.Checked = reg.IsRoomIn ?? false;
            }
        }

        public override bool OnButtonOkClicked()
        {
            Validate();

            if (!IsValid)
                return false;
            
            using (esTransactionScope trans = new esTransactionScope())
            {
                //update registration
                var entity = new Registration();
                entity.LoadByPrimaryKey(Request.QueryString["regno"]);
                if (!txtDischargePlanDate.IsEmpty)
                    entity.DischargePlanDate =
                        DateTime.Parse(txtDischargePlanDate.SelectedDate.Value.ToShortDateString() + " " +
                                       txtDischargePlanTime.TextWithLiterals);
                else
                    entity.str.DischargePlanDate = string.Empty;
                entity.UsertInsertDischargePlan= AppSession.UserLogin.UserID;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                //save            
                entity.Save();

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
