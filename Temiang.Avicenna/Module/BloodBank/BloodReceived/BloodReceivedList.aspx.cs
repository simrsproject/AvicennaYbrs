using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using Temiang.Dal.Interfaces;
using System.Web.UI;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.BloodBank
{
    public partial class BloodReceivedList : BasePage
    {
        private string FormType
        {
            get
            {
                return string.IsNullOrEmpty(Request.QueryString["type"]) ? string.Empty : Request.QueryString["type"];
            }
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

            ProgramID = FormType == string.Empty ? AppConstant.Program.BloodBankReceived : AppConstant.Program.BloodBankReturn;

            if (!IsPostBack)
            {
                var query = new ServiceUnitQuery("a");
                query.Where(
                    query.SRRegistrationType.In(AppConstant.RegistrationType.InPatient, AppConstant.RegistrationType.OutPatient, AppConstant.RegistrationType.EmergencyPatient),
                    query.IsActive == true
                );

                if (!string.IsNullOrEmpty(FormType))
                {
                    var usr = new AppUserServiceUnitQuery("b");
                    query.InnerJoin(usr).On(usr.UserID == AppSession.UserLogin.UserID && usr.ServiceUnitID == query.ServiceUnitID);
                }

                var coll = new ServiceUnitCollection();
                coll.Load(query);

                cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (ServiceUnit entity in coll)
                {
                    cboServiceUnitID.Items.Add(new RadComboBoxItem(entity.ServiceUnitName, entity.ServiceUnitID));
                }

                grdList.MasterTableView.Columns[14].Visible = AppSession.Parameter.IsShowPrintLabelOnTransEntry;
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
                var isEmptyFilter = string.IsNullOrEmpty(FormType) && txtRequestDate1.IsEmpty && txtRequestDate2.IsEmpty && string.IsNullOrEmpty(txtTransactionNo.Text)
                    && string.IsNullOrEmpty(txtBloodBankNo.Text) && string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) && string.IsNullOrEmpty(txtRegistrationNo.Text)
                    && string.IsNullOrEmpty(txtPatientName.Text);
                if (!ValidateSearch(isEmptyFilter, "Blood Bank")) return null;

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
                        query.SRBloodGroupRequest,
                        bgroup.ItemName.As("BloodGroup"),
                        query.QtyBagRequest,
                        query.VolumeBag,
                        @"<ISNULL(bb.IsValidatedByCasemix, 1) AS IsValidatedByCasemix>"
                    );
                if (string.IsNullOrEmpty(FormType))
                    query.Select("<'BloodReceivedDetail.aspx?md=view&id='+bb.TransactionNo+'&regno='+bb.RegistrationNo+'&bg='+bb.SRBloodGroupRequest+'&type=' AS ReceivedUrl>");
                else
                    query.Select("<'BloodReceivedDetail.aspx?md=view&id='+bb.TransactionNo+'&regno='+bb.RegistrationNo+'&bg='+bb.SRBloodGroupRequest+'&type=ret' AS ReceivedUrl>");

                query.InnerJoin(qr).On(query.RegistrationNo == qr.RegistrationNo);
                query.InnerJoin(qp).On(qr.PatientID == qp.PatientID);
                query.LeftJoin(qm).On(qr.ParamedicID == qm.ParamedicID);
                query.LeftJoin(unit).On(qr.ServiceUnitID == unit.ServiceUnitID);
                query.LeftJoin(room).On(qr.RoomID == room.RoomID);
                query.InnerJoin(grr).On(qr.GuarantorID == grr.GuarantorID);
                query.LeftJoin(btype).On(qp.SRBloodType == btype.ItemID &&
                                     btype.StandardReferenceID == AppEnum.StandardReference.BloodType);
                query.InnerJoin(bgroup).On(query.SRBloodGroupRequest == bgroup.ItemID &&
                                          bgroup.StandardReferenceID == AppEnum.StandardReference.BloodGroup);
                query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & qp.SRSalutation == sal.ItemID);

                if (!string.IsNullOrEmpty(FormType))
                {
                    var usr = new AppUserServiceUnitQuery("usr");
                    query.InnerJoin(usr).On(usr.UserID == AppSession.UserLogin.UserID && usr.ServiceUnitID == qr.ServiceUnitID);
                    query.Where(bgroup.CustomField2 == "1");
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
                                 string.Format("<p.OldMedicalNo = '{0}'>", searchReg)
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

                if (FormType == "ret")
                {
                    var iquery = new BloodBankTransactionItemQuery("bbi");
                    iquery.Where(iquery.IsProceedToTransfusion == true, iquery.TransfusionStartDateTime.IsNull());
                    iquery.Select(iquery.TransactionNo);

                    query.Where(query.TransactionNo.In(iquery));
                }

                query.Where(query.IsApproved == true);

                query.OrderBy(query.TransactionNo.Descending);

                DataTable tbl = query.LoadDataTable();

                return tbl;
            }
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = e.DetailTableView.ParentItem;
            string tno = dataItem.GetDataKeyValue("TransactionNo").ToString();

            if (e.DetailTableView.Name.Equals("grdDetail"))
            {
                var query = new BloodBankTransactionQuery("a");
                var reg = new RegistrationQuery("b");
                var pat = new PatientQuery("c");
                var btype = new AppStandardReferenceItemQuery("d");
                var bgroup = new AppStandardReferenceItemQuery("e");
                var usr = new AppUserQuery("f");

                query.InnerJoin(reg).On(query.RegistrationNo == reg.RegistrationNo);
                query.InnerJoin(pat).On(reg.PatientID == pat.PatientID);
                query.LeftJoin(btype).On(pat.SRBloodType == btype.ItemID &&
                                         btype.StandardReferenceID == AppEnum.StandardReference.BloodType);
                query.InnerJoin(bgroup).On(query.SRBloodGroupRequest == bgroup.ItemID &&
                                          bgroup.StandardReferenceID == AppEnum.StandardReference.BloodGroup);
                query.InnerJoin(usr).On(query.OfficerByUserID == usr.UserID);
                query.Select
                    (
                        query.RegistrationNo,
                        query.TransactionNo,
                        query.TransactionDate,
                        query.RequestDate,
                        query.RequestTime,
                        query.BloodBankNo,
                        query.PdutNo,
                        btype.ItemName.As("BloodType"),
                        pat.BloodRhesus,
                        bgroup.ItemName.As("BloodGroup"),
                        query.QtyBagRequest,
                        query.VolumeBag,
                        usr.UserName.As("Officer"),
                        query.IsApproved,
                        query.IsVoid
                    );

                query.Where(query.TransactionNo == tno);

                e.DetailTableView.DataSource = query.LoadDataTable();
            }
            else
            {
                var query = new BloodBankTransactionItemQuery("a");
                var bgroup = new AppStandardReferenceItemQuery("b");
                var usr = new AppUserQuery("f");
                var bstatus = new AppStandardReferenceItemQuery("c");
                var bagno = new BloodBagNoQuery("d");

                query.InnerJoin(bgroup).On(query.SRBloodGroupReceived == bgroup.ItemID &&
                                          bgroup.StandardReferenceID == AppEnum.StandardReference.BloodGroup);
                query.LeftJoin(usr).On(query.ExaminerByUserID == usr.UserID);
                query.LeftJoin(bstatus).On(query.SRBloodBagStatus == bstatus.ItemID &&
                                          bstatus.StandardReferenceID == AppEnum.StandardReference.BloodBagStatus);
                query.InnerJoin(bagno).On(bagno.BagNo == query.BagNo);
                query.Select
                    (
                        query.TransactionNo,
                        query.BagNo,
                        query.BloodBagTemperature,
                        bagno.VolumeBag.Coalesce("0"),
                        bagno.ExpiredDateTime,
                        query.ReceivedDate,
                        query.ReceivedTime,
                        bgroup.ItemName.As("BloodGroupReceived"),
                        query.IsScreeningLabelPassedPmi,
                        query.IsExpiredDate,
                        query.IsLeak,
                        query.IsHemolysis,
                        query.IsCrossMatchingSuitability,
                        query.IsClotting,
                        query.IsBloodTypeCompatibility,
                        query.IsLabelDonorsMatchesWithPatientsForm,
                        query.IsScreeningLabelPassedBd,
                        usr.UserName.As("ExaminerByUserName"),
                        query.UnitOfficer,
                        query.IsProceedToTransfusion,
                        bstatus.ItemName.As("BloodBagStatusName"),
                        query.BloodBagNotes,
                        query.IsVoid
                    );

                query.Where(query.TransactionNo == tno);

                e.DetailTableView.DataSource = query.LoadDataTable();
            }
        }

        protected void grdList_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "PatientSticker")
            {
                var jobParameters = new PrintJobParameterCollection();

                var jobParameter = jobParameters.AddNew();
                jobParameter.Name = "p_RegistrationNo";
                jobParameter.ValueString = e.CommandArgument.ToString();

                AppSession.PrintJobParameters = jobParameters;
                AppSession.PrintJobReportID = AppConstant.Report.RegistrationLabel;
                var SuPrintLabelPatientID = AppSession.Parameter.AppProgramServiceUnitPatientLabel;
                if (!string.IsNullOrEmpty(SuPrintLabelPatientID)) AppSession.PrintJobReportID = SuPrintLabelPatientID;

                string script = @"openRpt();";
                RadAjaxPanel1.ResponseScripts.Add(script);
            }
        }
    }
}
