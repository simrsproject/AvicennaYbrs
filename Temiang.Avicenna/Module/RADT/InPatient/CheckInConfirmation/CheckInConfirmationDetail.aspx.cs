using System;
using System.Linq;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Configuration;
using System.Data;

namespace Temiang.Avicenna.Module.RADT.InPatient
{
    public partial class CheckInConfirmationDetail : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.CheckInConfirmation;

            if (!IsPostBack)
            {
                ComboBox.PopulateWithSmf(cboSmfID);
                StandardReference.InitializeIncludeSpace(cboSRTransferredPatientWith, AppEnum.StandardReference.TransferredPatientWith);
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

                cboSmfID.SelectedValue = reg.SmfID;
                txtArrivedDate.SelectedDate = (new DateTime()).NowAtSqlServer().Date;
                txtArrivedTime.Text = (new DateTime()).NowAtSqlServer().ToString("HH:mm");
                this.UserRequested(cboReceivedByUserID, AppSession.UserLogin.UserID);
                cboReceivedByUserID.SelectedValue = AppSession.UserLogin.UserID;
            }
        }

        public override bool OnButtonOkClicked()
        {
            Validate();
            if (!IsValid) return false;

            if (string.IsNullOrEmpty(cboSmfID.SelectedValue))
            {
                ShowInformationHeader("SMF required.");
                return false;
            }

            if (string.IsNullOrEmpty(cboSRTransferredPatientWith.SelectedValue))
            {
                ShowInformationHeader("Transferred With required.");
                return false;
            }

            if (string.IsNullOrEmpty(cboTransferredByUserID.SelectedValue))
            {
                ShowInformationHeader("Transferred By required.");
                return false;
            }

            if (string.IsNullOrEmpty(cboReceivedByUserID.SelectedValue))
            {
                ShowInformationHeader("Received By required.");
                return false;
            }

            bool isFileReceived = chkIsFileReceived.Checked;
            var registrationNo = txtRegistrationNo.Text;
            var transferNo = Request.QueryString["tno"];

            //registration
            var reg = new Registration();
            reg.LoadByPrimaryKey(registrationNo);
            reg.SmfID = cboSmfID.SelectedValue;
            reg.LastUpdateByUserID = AppSession.UserLogin.UserID;
            reg.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            //history
            var hist = new PatientTransferHistory();
            bool isHist = false;

            if (hist.LoadByPrimaryKey(reg.RegistrationNo, transferNo))
            {
                hist.SmfIDBefore = hist.SmfID;
                hist.SmfID = reg.SmfID;
                hist.LastUpdateByUserID = AppSession.UserLogin.UserID;
                hist.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                hist.ArrivedDateTime = DateTime.Parse(txtArrivedDate.SelectedDate.Value.ToShortDateString() + " " + txtArrivedTime.TextWithLiterals);
                hist.SRTransferredPatientWith = cboSRTransferredPatientWith.SelectedValue;
                hist.TransferredByUserID = cboTransferredByUserID.SelectedValue;
                hist.ReceivedByUserID = cboReceivedByUserID.SelectedValue;

                isHist = true;
            }

            var cch = new CheckinConfirmHistory();
            cch.AddNew();
            cch.RegistrationNo = reg.RegistrationNo;
            cch.TransferNo = transferNo;
            cch.BedID = reg.BedID;
            cch.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            cch.LastUpdateByUserID = AppSession.UserLogin.UserID;

            using (esTransactionScope trans = new esTransactionScope())
            {
                reg.Save();

                if (!string.IsNullOrEmpty(transferNo))
                {
                    var pt = new PatientTransfer();
                    if (pt.LoadByPrimaryKey(transferNo))
                    {
                        pt.IsValidated = true;
                        pt.ValidatedByUserID = AppSession.UserLogin.UserID;
                        pt.ValidatedDateTime = (new DateTime()).NowAtSqlServer();
                        pt.Save();
                    }
                }
                else
                {
                    if (AppSession.Parameter.IsAutoClosedRegFromOnCheckinConfirmed)
                    {
                        var regColl = new RegistrationCollection();
                        regColl.Query.Where(regColl.Query.RegistrationNo.In(MergeRegistrationList(reg.RegistrationNo)));
                        regColl.LoadAll();
                        if (regColl.Count > 0)
                        {
                            foreach (var r in regColl)
                            {
                                r.IsClosed = true;
                            }
                            regColl.Save();
                        }
                    }
                }

                if (isHist)
                    hist.Save();

                //update bed status
                if (reg.IsRoomIn ?? false)
                {
                    var bedRoomingIn = new BedRoomInCollection();
                    bedRoomingIn.Query.Where(
                        bedRoomingIn.Query.BedID == reg.BedID,
                        bedRoomingIn.Query.RegistrationNo == registrationNo);
                    bedRoomingIn.LoadAll();
                    foreach (var x in bedRoomingIn)
                    {
                        x.SRBedStatus = AppSession.Parameter.BedStatusOccupied;
                        x.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        x.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                    }
                    bedRoomingIn.Save();
                }
                else
                {
                    var bed = new Bed();
                    if (bed.LoadByPrimaryKey(reg.BedID))
                    {
                        bed.SRBedStatus = AppSession.Parameter.BedStatusOccupied;
                        bed.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        bed.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        bed.Save();
                    }
                }
                cch.Save();

                if (isFileReceived)
                {
                    var file = new MedicalRecordFileStatusMovement();
                    if (!file.LoadByPrimaryKey(reg.ServiceUnitID, reg.RegistrationNo))
                        file.AddNew();

                    file.RegistrationNo = reg.RegistrationNo;
                    file.LastPositionServiceUnitID = reg.ServiceUnitID;
                    file.LastPositionDateTime = DateTime.Now;
                    file.LastPositionUserID = AppSession.UserLogin.UserID;

                    file.Save();
                }

                var bedmanagColl = new BedManagementCollection();
                bedmanagColl.Query.Where(bedmanagColl.Query.BedID == reg.BedID,
                    bedmanagColl.Query.RegistrationNo == reg.RegistrationNo,
                    bedmanagColl.Query.IsReleased == false);
                bedmanagColl.LoadAll();
                foreach (var bm in bedmanagColl)
                {
                    bm.IsReleased = true;
                    bm.ReleasedByUserID = AppSession.UserLogin.UserID;
                    bm.ReleasedDateTime = (new DateTime()).NowAtSqlServer();
                }
                if (bedmanagColl != null)
                    bedmanagColl.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }

            return true;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = 'rebind'";
        }

        protected void cboTransferredByUserID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            this.UserRequested(cboTransferredByUserID, e.Text);
        }

        protected void cboReceivedByUserID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            this.UserRequested(cboReceivedByUserID, e.Text);
        }

        protected void cboUserID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["UserName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["UserID"].ToString();
        }

        private void UserRequested(RadComboBox comboBox, string textSearch)
        {
            if (textSearch == null)
                textSearch = string.Empty;

            string searchText = string.Format("%{0}%", textSearch);

            var query = new AppUserQuery("a");
            query.es.Top = 20;
            query.Where
                (query.Or(query.UserName.Like(searchText),
                          query.UserID.Like(searchText)),
                 query.ExpireDate >= DateTime.Now.Date);
            query.OrderBy(query.UserName.Ascending);
            DataTable dtb = query.LoadDataTable();

            comboBox.DataSource = dtb;
            comboBox.DataBind();

            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0]["UserName"].ToString();
                comboBox.SelectedValue = dtb.Rows[0]["UserID"].ToString();
            }
        }

        private string[] MergeRegistrationList(string regno)
        {
            var mrgs = new MergeBillingCollection();
            mrgs.Query.Where(mrgs.Query.FromRegistrationNo == regno);
            mrgs.LoadAll();
            if (mrgs.Count == 0)
            {
                var arr = new string[1];
                arr.SetValue(string.Empty, 0);
                return arr;
            }

            return mrgs.Select(m => m.RegistrationNo).ToArray();
        }
    }
}
