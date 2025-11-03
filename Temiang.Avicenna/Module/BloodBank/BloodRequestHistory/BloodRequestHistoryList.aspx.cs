using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using System.Web.UI;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.BloodBank
{
    public partial class BloodRequestHistoryList : BasePage
    {
        private bool _isHideEmptySearchMessage = false;
        protected void Page_Init(object sender, EventArgs e)
        {
            _isHideEmptySearchMessage = false;
            if (Page.IsPostBack)
            {
                if (Request["__EVENTTARGET"].Contains("grd") &&
                    Request["__EVENTARGUMENT"].Contains("rebind"))
                {
                    _isHideEmptySearchMessage = true;
                }
            }

            ProgramID = AppConstant.Program.BloodBankRequestHistory;

            if (!IsPostBack)
            {
                var query = new ServiceUnitQuery("a");
                if (!this.IsUserCrossUnitAble)
                {
                    var usr = new AppUserServiceUnitQuery("b");
                    query.InnerJoin(usr).On(usr.UserID == AppSession.UserLogin.UserID && usr.ServiceUnitID == query.ServiceUnitID);
                }
                query.Where(query.SRRegistrationType.In(AppConstant.RegistrationType.InPatient,
                    AppConstant.RegistrationType.EmergencyPatient,
                    AppConstant.RegistrationType.OutPatient), query.IsActive == true);
                var coll = new ServiceUnitCollection();
                coll.Load(query);

                cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (ServiceUnit entity in coll)
                {
                    cboServiceUnitID.Items.Add(new RadComboBoxItem(entity.ServiceUnitName, entity.ServiceUnitID));
                }

                StandardReference.InitializeIncludeSpace(cboRequestStatus, AppEnum.StandardReference.BloodBagStatus);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack) RestoreValueFromCookie();
        }

        private bool ValidateSearch(bool isEmptyFilter, string searchingLabel)
        {
            if (!IsListLoadRecordIfFiltered) return true;
            if (!IsPostBack) return false;
            if (!isEmptyFilter) return true;
            if (!_isHideEmptySearchMessage)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "invalid",
                    string.Format("alert('Please entry {0} searching criteria');", searchingLabel), true);
            }
            return false;
        }
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grdList.DataSource = new String[] { };
                return;
            }

            var grd = (RadGrid)source;
            var dataSource = BloodBankTransactions;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
            }            
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            SaveValueToCookie();

            grdList.Rebind();
        }

        private DataTable BloodBankTransactions
        {
            get
            {
                var isEmptyFilter = txtRequestDate1.IsEmpty && txtRequestDate2.IsEmpty && string.IsNullOrEmpty(txtTransactionNo.Text) && string.IsNullOrEmpty(txtBloodBankNo.Text) 
                    && string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) && string.IsNullOrEmpty(txtRegistrationNo.Text) && string.IsNullOrEmpty(txtPatientName.Text) 
                    && string.IsNullOrEmpty(txtBarcodeEntry.Text);
                if (!ValidateSearch(isEmptyFilter, "Blood History")) return null;

                var query = new BloodBankTransactionQuery("bb");
                var qr = new RegistrationQuery("r");
                var qp = new PatientQuery("p");
                var qm = new ParamedicQuery("m");
                var unit = new ServiceUnitQuery("s");
                var room = new ServiceRoomQuery("d");
                var grr = new GuarantorQuery("c");
                var btype = new AppStandardReferenceItemQuery("bt");
                var bgroup = new AppStandardReferenceItemQuery("bg");
                var sal = new AppStandardReferenceItemQuery("sal");

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                query.Select
                    (
                        query.TransactionNo,
                        query.TransactionDate,
                        query.RequestDate,
                        query.RequestTime,
                        query.BloodBankNo,
                        query.PdutNo,

                        query.RegistrationNo,
                        qr.RegistrationDate,
                        qp.MedicalNo,
                        qp.PatientName,
                        qp.Sex,
                        qm.ParamedicName,
                        unit.ServiceUnitName,
                        room.RoomName,
                        qr.BedID,
                        grr.GuarantorName,
                        sal.ItemName.As("SalutationName"),

                        btype.ItemName.As("BloodType"),
                        qp.BloodRhesus,
                        bgroup.ItemName.As("BloodGroup"),
                        query.QtyBagRequest,
                        query.VolumeBag,
                        @"<'0' AS SRBloodBagStatus>",
                        @"<'' AS BloodBagStatusName>"
                    );
                query.InnerJoin(qr).On(query.RegistrationNo == qr.RegistrationNo);
                query.InnerJoin(qp).On(qr.PatientID == qp.PatientID);
                query.LeftJoin(qm).On(qr.ParamedicID == qm.ParamedicID);
                query.LeftJoin(unit).On(qr.ServiceUnitID == unit.ServiceUnitID);
                query.LeftJoin(room).On(qr.RoomID == room.RoomID);
                query.InnerJoin(grr).On(qr.GuarantorID == grr.GuarantorID);
                query.LeftJoin(btype).On(qp.SRBloodType == btype.ItemID &&
                                     btype.StandardReferenceID == AppEnum.StandardReference.BloodType);
                query.LeftJoin(bgroup).On(query.SRBloodGroupRequest == bgroup.ItemID &&
                                          bgroup.StandardReferenceID == AppEnum.StandardReference.BloodGroup);
                query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & qp.SRSalutation == sal.ItemID);

                if (!txtRequestDate1.IsEmpty && !txtRequestDate2.IsEmpty)
                    query.Where(query.RequestDate >= txtRequestDate1.SelectedDate, query.RequestDate <= txtRequestDate2.SelectedDate);
                if (!string.IsNullOrEmpty(txtTransactionNo.Text))
                    query.Where(query.TransactionNo == txtTransactionNo.Text);
                if (!string.IsNullOrEmpty(txtBloodBankNo.Text))
                    query.Where(query.Or(query.BloodBankNo == txtBloodBankNo.Text, query.PdutNo == txtBloodBankNo.Text));
                if (cboServiceUnitID.SelectedValue != string.Empty)
                    query.Where(qr.ServiceUnitID == cboServiceUnitID.SelectedValue);
                if (txtRegistrationNo.Text != string.Empty)
                {
                    string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                    if (AppSession.Parameter.IsMedicalNoContainStrip)
                        query.Where
                            (qr.Or
                                 (
                                     string.Format("<r.RegistrationNo = '{0}' OR >", searchReg),
                                     string.Format("<p.MedicalNo = '{0}' OR >", searchReg),
                                     string.Format("<p.OldMedicalNo = '{0}'>", searchReg),
                                     string.Format("< OR REPLACE(p.MedicalNo, '-', '') LIKE '%{0}%'>", searchReg),
                                     string.Format("< OR REPLACE(p.OldMedicalNo, '-', '') LIKE '%{0}%'>", searchReg)
                                 )
                            );
                    else
                        query.Where
                            (qr.Or
                                 (
                                     string.Format("<r.RegistrationNo = '{0}' OR >", searchReg),
                                     string.Format("<p.MedicalNo = '{0}' OR >", searchReg),
                                     string.Format("<p.OldMedicalNo = '{0}'>", searchReg),
                                     string.Format("< OR p.MedicalNo LIKE '%{0}%'>", searchReg),
                                     string.Format("< OR p.OldMedicalNo LIKE '%{0}%'>", searchReg)
                                 )
                            );
                }

                if (txtPatientName.Text != string.Empty)
                {
                    string searchPatient = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";
                    query.Where
                        (
                          string.Format("<LTRIM(RTRIM(LTRIM(p.FirstName + ' ' + p.MiddleName)) + ' ' + p.LastName) LIKE '{0}'>", searchPatient)
                        );
                }
                if (!string.IsNullOrEmpty(txtBarcodeEntry.Text))
                {
                    string searchReg = Helper.EscapeQuery(txtBarcodeEntry.Text);
                    query.Where(
                        query.Or(
                            string.Format("< REPLACE(p.MedicalNo, '-', '') LIKE '{0}'>", searchReg),
                            string.Format("< OR REPLACE(p.OldMedicalNo, '-', '') LIKE '{0}'>", searchReg)
                            )
                        );
                }

                query.Where(query.IsApproved == true,
                    query.IsBloodSampleGiven == true);
                query.OrderBy(query.TransactionNo.Descending);

                DataTable tbl = query.LoadDataTable();

                foreach (DataRow row in tbl.Rows)
                {
                    var bti = new BloodBankTransactionItemQuery();
                    bti.Select(bti.SRBloodBagStatus);
                    bti.Where(bti.TransactionNo == row["TransactionNo"].ToString(), bti.IsVoid == false);
                    bti.OrderBy(bti.SRBloodBagStatus.Descending);
                    bti.es.Top = 1;
                    DataTable btidt = bti.LoadDataTable();
                    if (btidt.Rows.Count > 0)
                    {
                        row["SRBloodBagStatus"] = btidt.Rows[0]["SRBloodBagStatus"].ToString();
                    }

                    var std = new AppStandardReferenceItem();
                    if (std.LoadByPrimaryKey("BloodBagStatus", row["SRBloodBagStatus"].ToString()))
                        row["BloodBagStatusName"] = std.ItemName;

                    if (!string.IsNullOrEmpty(cboRequestStatus.SelectedValue) && cboRequestStatus.SelectedValue != row["SRBloodBagStatus"].ToString())
                        row.Delete();
                }
                tbl.AcceptChanges();

                return tbl;
            }
        }

        protected virtual void txtBarcodeEntry_OnTextChanged(object sender, System.EventArgs e)
        {
            if (txtBarcodeEntry.Text == "") return;
            grdList.Rebind();

            txtBarcodeEntry.Text = "";
            txtBarcodeEntry.Focus();
        }

        public System.Drawing.Color GetColorOfTriase(object srBloodBagStatus)
        {
            System.Drawing.Color color = System.Drawing.Color.Yellow;
            switch (srBloodBagStatus.ToString())
            {
                case "1":
                    {
                        color = System.Drawing.Color.Green;
                        break;
                    }
                case "2":
                    {
                        color = System.Drawing.Color.Blue;
                        break;
                    }
                case "3":
                    {
                        color = System.Drawing.Color.Red;
                        break;
                    }
            }

            return color;
        }
    }
}
