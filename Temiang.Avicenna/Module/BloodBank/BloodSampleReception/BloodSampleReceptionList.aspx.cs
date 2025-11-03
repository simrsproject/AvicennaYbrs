using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using System.Web.UI;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.BloodBank
{
    public partial class BloodSampleReceptionList : BasePage
    {
        private void MessageShow(string msg)
        {
            fw_PanelInfo.Visible = true;
            fw_LabelInfo.Text = msg;
        }
        private void MessageHide()
        {
            fw_PanelInfo.Visible = false;
            fw_LabelInfo.Text = "";
        }

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

            ProgramID = Request.QueryString["tr"].ToString() == "nrs" ? AppConstant.Program.BloodBankSampleSubmitted : AppConstant.Program.BloodBankSampleReception;

            if (!IsPostBack)
            {
                var query = new ServiceUnitQuery("a");
                if (Request.QueryString["tr"].ToString() == "nrs")
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

                cboStatus.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                cboStatus.Items.Add(new RadComboBoxItem("Outstanding", "0"));
                cboStatus.Items.Add(new RadComboBoxItem("Taken", "1"));
                cboStatus.Items.Add(new RadComboBoxItem("Submitted", "2"));
                cboStatus.Items.Add(new RadComboBoxItem("Received (By Analyst)", "3"));

                txtRequestDate1.SelectedDate = DateTime.Now;
                txtRequestDate2.SelectedDate = DateTime.Now;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack) RestoreValueFromCookie();

            MessageHide();
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
                var isEmptyFilter = txtRequestDate1.IsEmpty && txtRequestDate2.IsEmpty && string.IsNullOrEmpty(txtTransactionNo.Text) 
                    && string.IsNullOrEmpty(txtBloodBankNo.Text) && string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) 
                    && string.IsNullOrEmpty(txtRegistrationNo.Text) && string.IsNullOrEmpty(txtPatientName.Text) && string.IsNullOrEmpty(txtBarcodeEntry.Text) 
                    && string.IsNullOrEmpty(cboStatus.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "Blood Sample")) return null;

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
                        qr.ServiceUnitID,
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
                        @"<ISNULL(bb.IsValidatedByCasemix, 1) AS IsValidatedByCasemix>",
                        @"<CASE WHEN bb.BloodSampleReceivedDateTime IS NULL THEN (CASE WHEN bb.BloodSampleSubmittedDateTime IS NULL THEN (CASE WHEN bb.BloodSampleTakenDateTime IS NULL THEN '0' ELSE '1' END) ELSE '2' END) ELSE '3' END AS 'Status'>"
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
                if (Request.QueryString["tr"].ToString() == "nrs")
                {
                    var usr = new AppUserServiceUnitQuery("usr");
                    query.InnerJoin(usr).On(usr.UserID == AppSession.UserLogin.UserID && usr.ServiceUnitID == qr.ServiceUnitID);
                }

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

                if (!string.IsNullOrEmpty(cboStatus.SelectedValue))
                {
                    switch (cboStatus.SelectedValue)
                    {
                        case "0":
                            query.Where(query.BloodSampleTakenDateTime.IsNull());
                            break;
                        case "1":
                            query.Where(query.BloodSampleTakenDateTime.IsNotNull(), query.BloodSampleSubmittedDateTime.IsNull());
                            break;
                        case "2":
                            query.Where(query.BloodSampleTakenDateTime.IsNotNull(), query.BloodSampleSubmittedDateTime.IsNotNull(), query.BloodSampleReceivedDateTime.IsNull());
                            break;
                        case "3":
                            query.Where(query.BloodSampleReceivedDateTime.IsNotNull());
                            break;
                    }
                }

                query.Where(query.IsApproved == true);
                query.OrderBy(query.TransactionNo.Descending);

                DataTable tbl = query.LoadDataTable();

                return tbl;
            }
        }

        protected virtual void txtBarcodeEntry_OnTextChanged(object sender, System.EventArgs e)
        {
            if (txtBarcodeEntry.Text == "") return;
            grdList.Rebind();
            if (grdList.MasterTableView.Items.Count == 0)
            {
                // record not found
                MessageShow("Data not found");
            }
            else if (grdList.MasterTableView.Items.Count == 1)
            {
                BloodSamplesProcess(
                    grdList.MasterTableView.Items[0].GetDataKeyValue("TransactionNo").ToString(), string.Empty);
                grdList.DataSource = null;
                grdList.Rebind();
            }
            else
            {
                // multiple registration
                MessageShow("Multiple request have been found, please mark the blood sample received manually");
            }

            txtBarcodeEntry.Text = "";
            txtBarcodeEntry.Focus();
        }

        private void BloodSamplesProcess(string transactionNo, string status)
        {
            var tc = new BloodBankTransaction();
            if (tc.LoadByPrimaryKey(transactionNo))
            {
                switch (status)
                {
                    case "0":
                        tc.BloodSampleTakenDateTime = (new DateTime()).NowAtSqlServer();
                        tc.BloodSampleTakenByUserID = AppSession.UserLogin.UserID;
                        break;
                    case "1":
                        tc.BloodSampleSubmittedDateTime = (new DateTime()).NowAtSqlServer();
                        tc.BloodSampleSubmittedByUserID = AppSession.UserLogin.UserID;
                        break;
                    case "2":
                        tc.IsBloodSampleGiven = true;
                        tc.BloodSampleReceivedDateTime = (new DateTime()).NowAtSqlServer();
                        tc.BloodSampleReceivedByUserID = AppSession.UserLogin.UserID;
                        break;
                    case "3":
                        //skip
                        break;
                    default: //barcode
                        if (Request.QueryString["tr"].ToString() == "nrs")
                        {
                            if (string.IsNullOrEmpty(tc.BloodSampleTakenByUserID))
                            {
                                tc.BloodSampleTakenDateTime = (new DateTime()).NowAtSqlServer();
                                tc.BloodSampleTakenByUserID = AppSession.UserLogin.UserID;
                            }
                            else if (string.IsNullOrEmpty(tc.BloodSampleSubmittedByUserID))
                            {
                                tc.BloodSampleSubmittedDateTime = (new DateTime()).NowAtSqlServer();
                                tc.BloodSampleSubmittedByUserID = AppSession.UserLogin.UserID;
                            }
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(tc.BloodSampleReceivedByUserID))
                            {
                                tc.BloodSampleReceivedDateTime = (new DateTime()).NowAtSqlServer();
                                tc.BloodSampleReceivedByUserID = AppSession.UserLogin.UserID;
                            }
                        }
                        break;
                }
                tc.Save();
            }
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (string.IsNullOrEmpty(eventArgument))
                return;

            if (!(sourceControl is RadGrid))
                return;

            if (eventArgument.Contains("|"))
            {
                var param = eventArgument.Split('|');

                BloodSamplesProcess(param[0], param[1]);

                grdList.DataSource = null;
                grdList.Rebind();
            }
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = e.DetailTableView.ParentItem;
            string tno = dataItem.GetDataKeyValue("TransactionNo").ToString();

            var query = new BloodBankTransactionQuery("a");
            var usrt = new AppUserQuery("b");
            var usrs = new AppUserQuery("c");
            var usrr = new AppUserQuery("d");

            query.LeftJoin(usrt).On(usrt.UserID == query.BloodSampleTakenByUserID);
            query.LeftJoin(usrs).On(usrs.UserID == query.BloodSampleSubmittedByUserID);
            query.LeftJoin(usrr).On(usrr.UserID == query.BloodSampleReceivedByUserID);

            query.Select
                (
                    query.TransactionNo,
                    query.BloodSampleTakenDateTime,
                    query.BloodSampleTakenByUserID,
                    usrt.UserName.As("BloodSampleTakenByUserName"),
                    query.BloodSampleSubmittedDateTime,
                    query.BloodSampleSubmittedByUserID,
                    usrs.UserName.As("BloodSampleSubmittedByUserName"),
                    query.BloodSampleReceivedDateTime,
                    query.BloodSampleReceivedByUserID,
                    usrr.UserName.As("BloodSampleReceivedByUserName")
                );

            query.Where(query.TransactionNo == tno);

            e.DetailTableView.DataSource = query.LoadDataTable();
        }
    }
}
