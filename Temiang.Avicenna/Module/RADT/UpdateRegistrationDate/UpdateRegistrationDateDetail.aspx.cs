using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT
{
    public partial class UpdateRegistrationDateDetail : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            var regType = Page.Request.QueryString["rt"];
            if (string.IsNullOrEmpty(regType))
                regType = AppConstant.RegistrationType.InPatient;

            switch (regType)
            {
                case AppConstant.RegistrationType.InPatient:
                    ProgramID = AppConstant.Program.InPatientUpdateRegistrationDate;
                    break;
                case AppConstant.RegistrationType.OutPatient:
                    ProgramID = AppConstant.Program.OutPatientUpdateRegistrationDate;
                    break;
                case AppConstant.RegistrationType.EmergencyPatient:
                    ProgramID = AppConstant.Program.EmergencyPatientUpdateRegistrationDate;
                    break;
                case AppConstant.RegistrationType.MedicalCheckUp:
                    ProgramID = AppConstant.Program.MedicalCheckupUpdateRegistrationDate;
                    break;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtRegistrationNo.Text = Request.QueryString["regno"];
                var reg = new Registration();
                reg.LoadByPrimaryKey(txtRegistrationNo.Text);

                var pat = new Patient();
                pat.LoadByPrimaryKey(reg.PatientID);
                txtMedicalNo.Text = pat.MedicalNo;
                var std = new AppStandardReferenceItem();
                txtSalutation.Text = std.LoadByPrimaryKey("Salutation", pat.SRSalutation) ? std.ItemName : string.Empty;
                txtPatientName.Text = pat.PatientName;
                txtGender.Text = pat.Sex;
                txtPlaceDOB.Text = string.Format("{0}, {1}", pat.CityOfBirth, Convert.ToDateTime(pat.DateOfBirth).ToString("dd-MMM-yyyy"));

                var su = new ServiceUnit();
                su.LoadByPrimaryKey(reg.str.ServiceUnitID);
                txtServiceUnitID.Text = su.ServiceUnitName;

                var sr = new ServiceRoom();
                sr.LoadByPrimaryKey(reg.str.RoomID);
                txtRoomID.Text = sr.RoomName;
                txtBedID.Text = reg.BedID;

                var c = new Class();
                c.LoadByPrimaryKey(reg.str.ClassID);
                txtClassID.Text = c.ClassName;

                var par = new Paramedic();
                par.LoadByPrimaryKey(reg.str.ParamedicID);
                txtParamedicID.Text = par.ParamedicName;

                var guar = new Guarantor();
                guar.LoadByPrimaryKey(reg.str.GuarantorID);
                txtGuarantorID.Text = guar.GuarantorName;

                txtRegistrationDate.SelectedDate = reg.RegistrationDate;
                txtRegistrationTime.Text = reg.RegistrationTime;
                txtDischargeDate.SelectedDate = reg.DischargeDate;
                txtDischargeTime.Text = reg.DischargeTime;

                pnlDischargeDate.Visible = reg.DischargeDate != null;
            }
        }

        public override bool OnButtonOkClicked()
        {
            Validate();

            if (!IsValid)
                return false;

            bool allowEdit = true;
            string msg = string.Empty;

            if (pnlDischargeDate.Visible)
            {
                if (txtDischargeDate.SelectedDate.Value.Date > (new DateTime()).NowAtSqlServer().Date)
                {
                    allowEdit = false;
                    msg = "Invalid discharge date. Discharge date cannot be more than system date.";
                }
                else if (txtRegistrationDate.SelectedDate > txtDischargeDate.SelectedDate)
                {
                    allowEdit = false;
                    msg = "Registration Date is greater than Discharge Date. Please make sure the data is valid.";
                }
                else
                {
                    if (txtRegistrationDate.SelectedDate == txtDischargeDate.SelectedDate)
                    {
                        string regTime = txtRegistrationTime.Text.Replace(":", "");
                        string dischargeTime = txtDischargeTime.Text.Replace(":", "");
                        if (regTime.ToInt() > dischargeTime.ToInt())
                        {
                            allowEdit = false;
                            msg = "Registration Time is greater than Discharge Time. Please make sure the data is valid.";
                        }
                    }
                }
            }

            if (allowEdit)
            {
                //update registration
                var reg = new Registration();
                reg.LoadByPrimaryKey(Request.QueryString["regno"]);

                DateTime regDate = reg.RegistrationDate ?? (new DateTime()).NowAtSqlServer();
                string regTime = reg.RegistrationTime;

                reg.RegistrationDate = txtRegistrationDate.SelectedDate;
                reg.RegistrationTime = txtRegistrationTime.TextWithLiterals;

                if (pnlDischargeDate.Visible)
                {
                    reg.DischargeDate = txtDischargeDate.SelectedDate;
                    reg.DischargeTime = txtDischargeTime.TextWithLiterals;

                    // update los
                    reg.LOSInDay = Convert.ToByte(Helper.GetAgeInDay(reg.RegistrationDate.Value, reg.DischargeDate.Value));
                    reg.LOSInMonth = Convert.ToByte(Helper.GetAgeInMonth(reg.RegistrationDate.Value, reg.DischargeDate.Value));
                    reg.LOSInYear = Convert.ToByte(Helper.GetAgeInYear(reg.RegistrationDate.Value, reg.DischargeDate.Value));
                }

                reg.LastUpdateByUserID = AppSession.UserLogin.UserID;
                reg.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                //update registration date
                var thuColl1 = new PatientTransferHistoryCollection();
                var thuQuery1 = new PatientTransferHistoryQuery();
                thuQuery1.Where(thuQuery1.RegistrationNo == reg.RegistrationNo);
                thuQuery1.es.Top = 1;
                thuQuery1.OrderBy(thuQuery1.TransferNo.Ascending);
                thuColl1.Load(thuQuery1);

                foreach (var item in thuColl1)
                {
                    item.DateOfEntry = reg.RegistrationDate;
                    item.TimeOfEntry = reg.RegistrationTime;
                    item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                }

                var bri = new BedRoomIn();
                if (reg.IsRoomIn == true)
                {
                    if (bri.LoadByPrimaryKey(reg.BedID, reg.RegistrationNo, regDate, regTime))
                    {
                        bri.DateOfEntry = reg.RegistrationDate;
                        bri.TimeOfEntry = reg.RegistrationTime;
                        bri.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        bri.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    }
                    else
                        bri = null;
                }
                else
                    bri = null;

                //update discharge date
                var thuColl2 = new PatientTransferHistoryCollection();
                var thuQuery2 = new PatientTransferHistoryQuery();
                thuQuery2.Where(thuQuery2.RegistrationNo == reg.RegistrationNo);
                thuQuery2.es.Top = 1;
                thuQuery2.OrderBy(thuQuery2.TransferNo.Descending);
                thuColl2.Load(thuQuery2);

                foreach (var item in thuColl2)
                {
                    if (pnlDischargeDate.Visible)
                    {
                        item.DateOfExit = reg.DischargeDate;
                        item.TimeOfExit = reg.DischargeTime;
                    }
                    else
                    {
                        item.DateOfExit = null;
                        item.TimeOfExit = null;
                    }
                    item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                }

                using (esTransactionScope trans = new esTransactionScope())
                {
                    reg.Save();

                    if (reg.SRRegistrationType == AppConstant.RegistrationType.InPatient)
                    {
                        thuColl1.Save();
                        thuColl2.Save();
                        if (bri != null)
                            bri.Save();
                    }

                    if (pnlDischargeDate.Visible)
                    {
                        // update tanggal pulang di jasa medis
                        var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
                        feeColl.SetDischargeDate(reg, AppSession.Parameter.IsFeeCalculatedOnTransaction);
                        feeColl.Save();
                    }

                    // Update Medical Discharge Summary
                    var mds = new MedicalDischargeSummary();
                    if (mds.LoadByPrimaryKey(reg.RegistrationNo))
                    {
                        if (mds.DischargeDate != reg.DischargeDate)
                        {
                            mds.DischargeDate = reg.DischargeDate;
                            mds.DischargeTime = reg.DischargeTime;
                        }
                        if (mds.DocumentDate != reg.RegistrationDate)
                        {
                            mds.DocumentDate = reg.RegistrationDate;
                        }
                        mds.Save();
                    }


                    //Commit if success, Rollback if failed
                    trans.Complete();
                }
            }
            else
                ShowInformationHeader(msg);

            return allowEdit;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = 'rebind'";
        }
    }
}
