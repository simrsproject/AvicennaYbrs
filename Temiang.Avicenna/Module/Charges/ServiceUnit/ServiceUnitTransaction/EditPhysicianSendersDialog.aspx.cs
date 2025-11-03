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

namespace Temiang.Avicenna.Module.Charges
{
    public partial class EditPhysicianSendersDialog : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var tc = new TransCharges();
                tc.LoadByPrimaryKey(Request.QueryString["trn"]);

                var reg = new Registration();
                reg.LoadByPrimaryKey(tc.RegistrationNo);

                var pat = new Patient();
                pat.LoadByPrimaryKey(reg.PatientID);

                txtTransactionNo.Text = tc.TransactionNo;
                txtTransactionDate.SelectedDate = tc.TransactionDate;

                txtRegistrationNo.Text = tc.RegistrationNo;
                txtMedicalNo.Text = pat.MedicalNo;
                txtPatientName.Text = pat.PatientName;
                txtGender.Text = pat.Sex;

                var room = new ServiceRoom();
                room.LoadByPrimaryKey(reg.RoomID);
                txtRoom.Text = room.RoomName;
                txtBed.Text = reg.BedID;

                var unit = new ServiceUnit();
                unit.LoadByPrimaryKey(tc.FromServiceUnitID);
                txtServiceUnitName.Text = unit.ServiceUnitName;

                unit = new ServiceUnit();
                unit.LoadByPrimaryKey(tc.ToServiceUnitID);
                txtServiceUnitOrderName.Text = unit.ServiceUnitName;
                
                txtPhysicianSenders.Text = tc.PhysicianSenders;
            }
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = 'rebind'";
        }

        public override bool OnButtonOkClicked()
        {
            var tc = new TransCharges();
            tc.LoadByPrimaryKey(Request.QueryString["trn"]);
            tc.PhysicianSenders = txtPhysicianSenders.Text;
            tc.LastUpdateByUserID = AppSession.UserLogin.UserID;
            tc.LastUpdateDateTime = DateTime.Now;
            tc.Save();

            return true;
        }
    }
}
