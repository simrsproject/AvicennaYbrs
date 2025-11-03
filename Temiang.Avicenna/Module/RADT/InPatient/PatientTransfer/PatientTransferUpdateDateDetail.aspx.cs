using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.RADT.InPatient
{
    public partial class PatientTransferUpdateDateDetail : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtTransferNo.Text = Request.QueryString["tno"];
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

                var par = new Paramedic();
                par.LoadByPrimaryKey(reg.ParamedicID);
                txtPhysicianID.Text = par.ParamedicName;

                var pt = new PatientTransfer();
                pt.LoadByPrimaryKey(txtTransferNo.Text);
                
                txtTransferDate.SelectedDate = pt.TransferDate.Value.Date;
                txtTransferTime.Text = pt.TransferTime;

                var su = new ServiceUnit();
                su.LoadByPrimaryKey(pt.FromServiceUnitID);
                txtFromServiceUnitID.Text = su.ServiceUnitName;

                su = new ServiceUnit();
                su.LoadByPrimaryKey(pt.ToServiceUnitID);
                txtToServiceUnitID.Text = su.ServiceUnitName;

                var sr = new ServiceRoom();
                sr.LoadByPrimaryKey(pt.FromRoomID);
                txtFromBedID.Text = sr.RoomName + " / " + pt.FromBedID;

                sr = new ServiceRoom();
                sr.LoadByPrimaryKey(pt.FromRoomID);
                txtToBedID.Text = sr.RoomName + " / " + pt.ToBedID;

                var c = new Class();
                c.LoadByPrimaryKey(pt.FromClassID);
                var fcls = c.ClassName;

                c = new Class();
                c.LoadByPrimaryKey(pt.FromChargeClassID);
                var fccls = c.ClassName;

                txtFromClassID.Text = fcls + " / " + fccls;

                c = new Class();
                c.LoadByPrimaryKey(pt.ToClassID);
                var tcls = c.ClassName;

                c = new Class();
                c.LoadByPrimaryKey(pt.ToChargeClassID);
                var tccls = c.ClassName;

                txtToClassID.Text = tcls + " / " + tccls;

                ComboBox.PopulateWithSmf(cboFromSpecialtyID);
                ComboBox.PopulateWithSmf(cboToSpecialityID);
                cboFromSpecialtyID.SelectedValue = pt.FromSpecialtyID;
                cboToSpecialityID.SelectedValue = pt.ToSpecialtyID;
            }
        }

        public override bool OnButtonOkClicked()
        {
            Validate();

            if (!IsValid)
                return false;

            var pth = new PatientTransferHistory();
            pth.LoadByPrimaryKey(txtRegistrationNo.Text, txtTransferNo.Text);
            if (pth.DateOfExit.HasValue)
            {
                if (pth.DateOfExit.Value.Date < txtToTransferDate.SelectedDate.Value.Date)
                {
                    ShowInformationHeader("Transfer date is greater than data transfer date after. Please make sure the date is valid.");
                    return false;
                }
                if (pth.DateOfExit.Value.Date == txtToTransferDate.SelectedDate.Value.Date)
                {
                    string timeOfExit = pth.TimeOfExit.Replace(":", "");
                    string timeOfTransfer = txtToTransferTime.Text.Replace(":", "");
                    if (timeOfExit.ToInt() < timeOfTransfer.ToInt())
                    {
                        ShowInformationHeader("Transfer time is greater than data transfer time after. Please make sure the time is valid.");
                        return false;
                    }
                }
            }

            var pthBeforeq = new PatientTransferHistoryQuery();
            pthBeforeq.Where(pthBeforeq.RegistrationNo == txtRegistrationNo.Text, pthBeforeq.TransferNo < txtTransferNo.Text);
            pthBeforeq.es.Top = 1;
            pthBeforeq.OrderBy(pthBeforeq.TransferNo.Descending);
            DataTable pthdtb = pthBeforeq.LoadDataTable();

            if (pthdtb.Rows.Count > 0)
            {
                if (Convert.ToDateTime(pthdtb.Rows[0]["DateOfEntry"]) > txtToTransferDate.SelectedDate.Value.Date)
                {
                    ShowInformationHeader(string.IsNullOrEmpty(pthdtb.Rows[0]["TransferNo"].ToString())
                                              ? "Transfer date is greater than registration date. Please make sure the date is valid."
                                              : "Transfer date is greater than data transfer date before. Please make sure the date is valid.");
                    return false;
                }

                if (Convert.ToDateTime(pthdtb.Rows[0]["DateOfEntry"]) == txtToTransferDate.SelectedDate.Value.Date)
                {
                    string timeOfEntry = pthdtb.Rows[0]["TimeOfEntry"].ToString().Replace(":", "");
                    string timeOfTransfer = txtToTransferTime.Text.Replace(":", "");
                    if (timeOfEntry.ToInt() > timeOfTransfer.ToInt())
                    {
                        ShowInformationHeader("Transfer time is less than data registration time. Please make sure the time is valid.");
                        return false;
                    }
                }
            }

            //update patient transfer
            var entity = new PatientTransfer();
            entity.LoadByPrimaryKey(txtTransferNo.Text);

            var bris = new BedRoomInCollection();
            if (entity.IsRoomInTo ?? false)
            {
                bris.Query.Where(bris.Query.BedID == entity.ToBedID, 
                    bris.Query.RegistrationNo == entity.RegistrationNo, 
                    bris.Query.DateOfEntry == entity.TransferDate,
                    bris.Query.TimeOfEntry == entity.TransferTime,
                    bris.Query.IsVoid == false);
                bris.LoadAll();
                if (bris.Count > 0)
                {
                    foreach (var bri in bris)
                    {
                        bri.DateOfExit = txtToTransferDate.SelectedDate;
                        bri.TimeOfExit = txtToTransferTime.TextWithLiterals;
                        bri.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        bri.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    }
                }
                else bris = null;
            }
            else bris = null;

            entity.TransferDate = txtToTransferDate.SelectedDate;
            entity.TransferTime= txtToTransferTime.TextWithLiterals;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            //update patient transfer history before
            var pthBefores = new PatientTransferHistoryCollection();
            pthBefores.Load(pthBeforeq);

            foreach (var item in pthBefores)
            {
                item.DateOfExit = entity.TransferDate;
                item.TimeOfExit = entity.TransferTime;
                item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
            }

            //update patient transfer history
            var pths = new PatientTransferHistoryCollection();
            var pthq = new PatientTransferHistoryQuery();
            pthq.Where(pthq.RegistrationNo == entity.RegistrationNo, pthq.TransferNo == entity.TransferNo);
            pths.Load(pthq);

            foreach (var item in pths)
            {
                item.DateOfEntry = entity.TransferDate;
                item.TimeOfEntry = entity.TransferTime;
                item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
            }

            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                pthBefores.Save();
                pths.Save();
                if (bris != null)
                    bris.Save();
                
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
