using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class MergeBillingDetail : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSCH")
                {
                    trCboRegNo.Visible = false;
                    rfvCboRegNo.Visible = false;
                }
                else
                {
                    trTxtRegNo.Visible = false;
                    rfvTxtRegNo.Visible = false;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtRegistrationNo2.Text = Request.QueryString["regno"];
                var reg = new Registration();
                reg.LoadByPrimaryKey(txtRegistrationNo2.Text);

                var pat = new Patient();
                pat.LoadByPrimaryKey(reg.PatientID);
                txtMedicalNo2.Text = pat.MedicalNo;
                var std = new AppStandardReferenceItem();
                txtSalutation2.Text = std.LoadByPrimaryKey("Salutation", pat.SRSalutation) ? std.ItemName : string.Empty;
                txtGender2.Text = pat.Sex;
                txtPatientName2.Text = ((((pat.FirstName + " " + pat.MiddleName).TrimStart()).TrimEnd() + " " + pat.LastName).TrimStart()).TrimEnd();
                txtPlaceDOB2.Text = string.Format("{0}, {1}", pat.CityOfBirth, Convert.ToDateTime(pat.DateOfBirth).ToString("dd-MMM-yyyy"));
                txtAgeInYear2.Text = Convert.ToString(reg.AgeInYear);
                txtAgeInMonth2.Text = Convert.ToString(reg.AgeInMonth);
                txtAgeInDay2.Text = Convert.ToString(reg.AgeInDay);

                var su = new ServiceUnit();
                su.LoadByPrimaryKey(reg.ServiceUnitID);
                txtServiceUnit2.Text = su.ServiceUnitName;

                var sr = new ServiceRoom();
                sr.LoadByPrimaryKey(reg.RoomID);
                txtRoomName2.Text = sr.RoomName;
                txtBedID2.Text = reg.BedID;

                var cls = new Class();
                cls.LoadByPrimaryKey(reg.str.ChargeClassID);
                txtClassName2.Text = cls.ClassName;

                cls = new Class();
                cls.LoadByPrimaryKey(reg.str.CoverageClassID);
                txtCoverageClassName2.Text = cls.ClassName;

                var par = new Paramedic();
                if (par.LoadByPrimaryKey(reg.ParamedicID ?? string.Empty))
                    txtParamedic2.Text = par.ParamedicName;
                else txtParamedic2.Text = string.Empty;

                hdnGuarantorId2.Value = reg.GuarantorID;
                var guar = new Guarantor();
                if (guar.LoadByPrimaryKey(reg.GuarantorID ?? string.Empty))
                    txtGuarantor2.Text = guar.GuarantorName;
                else txtGuarantor2.Text = string.Empty;
            }
        }

        public override bool OnButtonOkClicked()
        {
            Validate();

            if (!IsValid)
                return false;

            string regNo = AppSession.Parameter.HealthcareInitialAppsVersion == "RSCH"
                               ? txtRegistrationNo.Text
                               : cboRegistrationNo.SelectedValue;

            if (txtRegistrationNo2.Text == regNo)
            {
                ShowInformationHeader("The selected Registration Number is equal to parent number.");
                return false;
            }

            var r = new Registration();
            if (r.LoadByPrimaryKey(regNo))
            {
                if (r.IsClosed ?? false)
                {
                    ShowInformationHeader("The selected Registration Number already closed.");
                    return false;
                }
                if (r.IsVoid ?? false)
                {
                    ShowInformationHeader("The selected Registration Number already void.");
                    return false;
                }

                var merge = new MergeBilling();
                if (merge.LoadByPrimaryKey(regNo) && !string.IsNullOrEmpty(merge.FromRegistrationNo))
                {
                    ShowInformationHeader("The selected Registration Number has been merged into another number (" + merge.FromRegistrationNo + ").");
                    return false;
                }

                var parent = new MergeBilling();
                parent.LoadByPrimaryKey(regNo);

                var entity = new MergeBilling();
                entity.LoadByPrimaryKey(txtRegistrationNo2.Text);
                string prefFrom = entity.FromRegistrationNo;
                entity.FromRegistrationNo = string.IsNullOrEmpty(parent.FromRegistrationNo) ? regNo : parent.FromRegistrationNo;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;

                var hist = new MergeBillingHistory();
                hist.AddNew();
                hist.RegistrationNo = txtRegistrationNo2.Text;
                hist.FromRegistrationNoBefore = string.Empty;
                hist.FromRegistrationNoAfter = entity.FromRegistrationNo;
                hist.LastUpdateDateTime = DateTime.Now;
                hist.LastUpdateByUserID = AppSession.UserLogin.UserID;

                //biaya admin dikosongkan, ikut parent registration
                var reg = new Registration();
                reg.LoadByPrimaryKey(txtRegistrationNo2.Text);
                reg.AdministrationAmount = 0;
                if (reg.SRRegistrationType != AppConstant.RegistrationType.InPatient &&
                    r.SRRegistrationType == AppConstant.RegistrationType.InPatient &&
                    !string.IsNullOrEmpty(parent.FromRegistrationNo))
                    reg.IsTransferedToInpatient = true;

                using (var trans = new esTransactionScope())
                {
                    entity.Save();
                    hist.Save();

                    var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
                    feeColl.SetMergeBilling(entity, AppSession.Parameter.IsFeeCalculatedOnTransaction);
                    feeColl.Save();

                    reg.Save();

                    // level ri
                    var mrgs = new MergeBillingCollection();
                    mrgs.Query.Where(mrgs.Query.FromRegistrationNo == txtRegistrationNo2.Text);
                    mrgs.LoadAll();

                    foreach (var mrg in mrgs)
                    {
                        var r2 = new Registration();
                        r2.LoadByPrimaryKey(mrg.RegistrationNo);
                        if (reg.SRRegistrationType != AppConstant.RegistrationType.InPatient &&
                            r.SRRegistrationType == AppConstant.RegistrationType.InPatient &&
                            !string.IsNullOrEmpty(parent.FromRegistrationNo))
                        {
                            r2.IsTransferedToInpatient = reg.IsTransferedToInpatient;
                            r2.Save();
                        }

                        mrg.FromRegistrationNo = entity.FromRegistrationNo;
                        mrg.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        mrg.LastUpdateDateTime = DateTime.Now;

                        // history
                        var hist2 = new MergeBillingHistory();
                        hist2.AddNew();
                        hist2.RegistrationNo = mrg.RegistrationNo;
                        hist2.FromRegistrationNoBefore = txtRegistrationNo2.Text;
                        hist2.FromRegistrationNoAfter = entity.FromRegistrationNo;
                        hist2.LastUpdateDateTime = DateTime.Now;
                        hist2.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        hist2.Save();
                    }

                    mrgs.Save();

                    ////level rj
                    //var mrgs2 = new MergeBillingCollection();
                    //mrgs2.Query.Where(
                    //    mrgs2.Query.Or(
                    //        mrgs2.Query.RegistrationNo == prefFrom,
                    //        mrgs2.Query.FromRegistrationNo == prefFrom
                    //        )
                    //    );
                    //mrgs2.LoadAll();

                    //foreach (var mrgg in mrgs2)
                    //{
                    //    var r2 = new Registration();
                    //    r2.LoadByPrimaryKey(mrgg.RegistrationNo);
                    //    if (r2.SRRegistrationType != AppConstant.RegistrationType.InPatient &&
                    //        r.SRRegistrationType == AppConstant.RegistrationType.InPatient &&
                    //        !string.IsNullOrEmpty(parent.FromRegistrationNo))
                    //    {
                    //        r2.IsTransferedToInpatient = reg.IsTransferedToInpatient;
                    //        r2.Save();
                    //    }

                    //    mrgg.FromRegistrationNo = entity.FromRegistrationNo;
                    //    mrgg.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    //    mrgg.LastUpdateDateTime = DateTime.Now;
                    //}

                    //mrgs2.Save();

                    //Commit if success, Rollback if failed
                    trans.Complete();
                }
            }
            else
                return false;

            return true;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = 'rebind'";
        }

        protected void cboRegistrationNo_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var reg = new RegistrationQuery("a");
            var pat = new PatientQuery("b");
            var unit = new ServiceUnitQuery("c");
            var room = new ServiceRoomQuery("d");
            var mrg = new MergeBillingQuery("e");

            reg.es.Top = 5;
            reg.Select(
                reg.RegistrationNo,
                reg.BedID,
                pat.PatientID,
                pat.MedicalNo,
                pat.PatientName,
                unit.ServiceUnitName,
                room.RoomName
                );
            reg.InnerJoin(pat).On(reg.PatientID == pat.PatientID);
            reg.InnerJoin(unit).On(reg.ServiceUnitID == unit.ServiceUnitID);
            reg.LeftJoin(room).On(reg.RoomID == room.RoomID);
            reg.InnerJoin(mrg).On(
                reg.RegistrationNo == mrg.RegistrationNo &&
                mrg.FromRegistrationNo == string.Empty
                );
            reg.Where(
                reg.IsClosed == false,
                reg.IsVoid == false,
                reg.IsConsul == false,
                reg.RegistrationNo != txtRegistrationNo2.Text
                );

            var r = new Registration();
            r.LoadByPrimaryKey(txtRegistrationNo2.Text);
            if (r.SRRegistrationType == AppConstant.RegistrationType.InPatient)
                reg.Where(reg.SRRegistrationType.In(AppConstant.RegistrationType.InPatient));

            if (e.Text.Trim().Contains(" "))
            {
                var searchs = e.Text.Trim().Split(' ');
                foreach (var search in searchs)
                {
                    var searchLike = "%" + search + "%";
                    reg.Where(
                        reg.Or(
                        //pat.PatientID.Like(searchLike),
                            pat.FirstName.Like(searchLike),
                            pat.LastName.Like(searchLike),
                            pat.MiddleName.Like(searchLike),
                            pat.MedicalNo.Like(searchLike),
                            reg.RegistrationNo.Like(searchLike)
                            )
                        );
                }
            }
            else
            {
                string searchTextContain = string.Format("%{0}%", e.Text);
                reg.Where(
                    reg.Or(
                        //pat.PatientID.Like(searchTextContain),
                        pat.MedicalNo.Like(searchTextContain),
                        pat.FirstName.Like(searchTextContain),
                        pat.MiddleName.Like(searchTextContain),
                        pat.LastName.Like(searchTextContain),
                        reg.RegistrationNo.Like(searchTextContain)
                        )
                );
            }

            cboRegistrationNo.DataSource = reg.LoadDataTable();
            cboRegistrationNo.DataBind();
        }

        protected void cboRegistrationNo_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["RegistrationNo"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["RegistrationNo"].ToString();
        }

        protected void cboRegistrationNo_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var reg = new Registration();
            if (reg.LoadByPrimaryKey(cboRegistrationNo.SelectedValue))
            {
                txtRegistrationNo.Text = cboRegistrationNo.SelectedValue;

                var pat = new Patient();
                pat.LoadByPrimaryKey(reg.PatientID);
                txtMedicalNo.Text = pat.MedicalNo;
                var std = new AppStandardReferenceItem();
                txtSalutation.Text = std.LoadByPrimaryKey("Salutation", pat.SRSalutation) ? std.ItemName : string.Empty;
                txtGender.Text = pat.Sex;
                txtPatientName.Text = ((((pat.FirstName + " " + pat.MiddleName).TrimStart()).TrimEnd() + " " + pat.LastName).TrimStart()).TrimEnd();
                txtPlaceDOB.Text = string.Format("{0}, {1}", pat.CityOfBirth, Convert.ToDateTime(pat.DateOfBirth).ToString("dd-MMM-yyyy"));
                txtAgeInYear.Text = Convert.ToString(reg.AgeInYear);
                txtAgeInMonth.Text = Convert.ToString(reg.AgeInMonth);
                txtAgeInDay.Text = Convert.ToString(reg.AgeInDay);

                var su = new ServiceUnit();
                su.LoadByPrimaryKey(reg.ServiceUnitID);
                txtServiceUnit.Text = su.ServiceUnitName;

                var sr = new ServiceRoom();
                sr.LoadByPrimaryKey(reg.RoomID);
                txtRoomName.Text = sr.RoomName;
                txtBedID.Text = reg.BedID;

                var cls = new Class();
                cls.LoadByPrimaryKey(reg.str.ChargeClassID);
                txtClassName.Text = cls.ClassName;

                cls = new Class();
                cls.LoadByPrimaryKey(reg.str.CoverageClassID);
                txtCoverageClassName.Text = cls.ClassName;

                var par = new Paramedic();
                if (par.LoadByPrimaryKey(reg.str.ParamedicID))
                    txtParamedic.Text = par.ParamedicName;
                else txtParamedic.Text = string.Empty;

                var guar = new Guarantor();
                if (guar.LoadByPrimaryKey(reg.str.GuarantorID))
                    txtGuarantor.Text = guar.GuarantorName;
                else txtGuarantor.Text = string.Empty;
                
                if (txtGuarantor.Text != txtGuarantor2.Text)
                {
                    if ((reg.GuarantorID == AppSession.Parameter.SelfGuarantor || reg.GuarantorID == AppSession.Parameter.DefaultGuarantorKiosk) && 
                        (hdnGuarantorId2.Value == AppSession.Parameter.SelfGuarantor || hdnGuarantorId2.Value == AppSession.Parameter.DefaultGuarantorKiosk))
                    {
                        pnlInfo.Visible = false;
                        lblInfo.Text = string.Empty;
                    }
                    else
                    {
                        // show notif
                        pnlInfo.Visible = true;
                        lblInfo.Text = "Info: different guarantors.";
                    }
                }
                else
                {
                    pnlInfo.Visible = false;
                    lblInfo.Text = string.Empty;
                }
            }
        }

        protected void txtRegistrationNo_TextChanged(object sender, EventArgs e)
        {
            var reg = new Registration();
            if (reg.LoadByPrimaryKey(txtRegistrationNo.Text))
            {
                var pat = new Patient();
                pat.LoadByPrimaryKey(reg.PatientID);
                txtMedicalNo.Text = pat.MedicalNo;
                var std = new AppStandardReferenceItem();
                txtSalutation.Text = std.LoadByPrimaryKey("Salutation", pat.SRSalutation) ? std.ItemName : string.Empty;
                txtGender.Text = pat.Sex;
                txtPatientName.Text = ((((pat.FirstName + " " + pat.MiddleName).TrimStart()).TrimEnd() + " " + pat.LastName).TrimStart()).TrimEnd();
                txtPlaceDOB.Text = string.Format("{0}, {1}", pat.CityOfBirth, Convert.ToDateTime(pat.DateOfBirth).ToString("dd-MMM-yyyy"));
                txtAgeInYear.Text = Convert.ToString(reg.AgeInYear);
                txtAgeInMonth.Text = Convert.ToString(reg.AgeInMonth);
                txtAgeInDay.Text = Convert.ToString(reg.AgeInDay);

                var su = new ServiceUnit();
                su.LoadByPrimaryKey(reg.ServiceUnitID);
                txtServiceUnit.Text = su.ServiceUnitName;

                var sr = new ServiceRoom();
                sr.LoadByPrimaryKey(reg.RoomID);
                txtRoomName.Text = sr.RoomName;
                txtBedID.Text = reg.BedID;

                var cls = new Class();
                cls.LoadByPrimaryKey(reg.str.ChargeClassID);
                txtClassName.Text = cls.ClassName;

                cls = new Class();
                cls.LoadByPrimaryKey(reg.str.CoverageClassID);
                txtCoverageClassName.Text = cls.ClassName;

                var par = new Paramedic();
                if (par.LoadByPrimaryKey(reg.str.ParamedicID))
                    txtParamedic.Text = par.ParamedicName;
                else txtParamedic.Text = string.Empty;

                var guar = new Guarantor();
                if (guar.LoadByPrimaryKey(reg.str.GuarantorID))
                    txtGuarantor.Text = guar.GuarantorName;
                else txtGuarantor.Text = string.Empty;

                if (txtGuarantor.Text != txtGuarantor2.Text)
                {
                    if ((reg.GuarantorID == AppSession.Parameter.SelfGuarantor || reg.GuarantorID == AppSession.Parameter.DefaultGuarantorKiosk) &&
                        (hdnGuarantorId2.Value == AppSession.Parameter.SelfGuarantor || hdnGuarantorId2.Value == AppSession.Parameter.DefaultGuarantorKiosk))
                    {
                    }
                    else
                    {
                        // show notif
                        ShowInformationHeader("Info: different guarantors.");
                    }
                }
            }
        }
    }
}
