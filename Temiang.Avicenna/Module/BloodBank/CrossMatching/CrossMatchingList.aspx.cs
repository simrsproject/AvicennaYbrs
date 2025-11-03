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
    public partial class CrossMatchingList : BasePage
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

            ProgramID = AppConstant.Program.BloodBankCrossMatching;

            if (!IsPostBack)
            {
                var coll = new ServiceUnitCollection();
                coll.Query.Where(
                    coll.Query.SRRegistrationType.In(AppConstant.RegistrationType.InPatient, AppConstant.RegistrationType.OutPatient, AppConstant.RegistrationType.EmergencyPatient),
                    coll.Query.IsActive == true
                );
                coll.Query.OrderBy(coll.Query.DepartmentID.Ascending);
                coll.LoadAll();

                cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (ServiceUnit entity in coll)
                {
                    cboServiceUnitID.Items.Add(new RadComboBoxItem(entity.ServiceUnitName, entity.ServiceUnitID));
                }
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
                var isEmptyFilter = txtRequestDate1.IsEmpty && txtRequestDate2.IsEmpty && string.IsNullOrEmpty(txtTransactionNo.Text) 
                    && string.IsNullOrEmpty(txtBloodBankNo.Text) && string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) && string.IsNullOrEmpty(txtRegistrationNo.Text) 
                    && string.IsNullOrEmpty(txtPatientName.Text);
                if (!ValidateSearch(isEmptyFilter, "Cross Matching")) return null;

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
                        @"<ISNULL(bb.IsValidatedByCasemix, 1) AS IsValidatedByCasemix>",
                        "<'CrossMatchingDetail.aspx?md=view&id='+bb.TransactionNo+'&regno='+bb.RegistrationNo AS ReceivedUrl>"
                    );
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

                query.Where(query.IsApproved == true, bgroup.CustomField == "1");
                if (AppSession.Parameter.IsNeedBloodSample)
                    query.Where(query.IsBloodSampleGiven == true);

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

                query.Where(query.TransactionNo == tno, bgroup.CustomField == "1");

                e.DetailTableView.DataSource = query.LoadDataTable();
            }
            else
            {
                var query = new BloodBankTransactionItemQuery("a");
                var bn = new BloodBagNoQuery("bn");
                var bgroup = new AppStandardReferenceItemQuery("b");
                var usr = new AppUserQuery("f");
                var bstatus = new AppStandardReferenceItemQuery("c");

                query.InnerJoin(bn).On(bn.BagNo == query.BagNo);
                query.LeftJoin(bgroup).On(query.SRBloodGroupReceived == bgroup.ItemID &&
                                          bgroup.StandardReferenceID == AppEnum.StandardReference.BloodGroup);
                query.LeftJoin(usr).On(usr.UserID == query.CrossMatchingByUserID);
                query.LeftJoin(bstatus).On(query.SRBloodBagStatus == bstatus.ItemID &&
                                          bstatus.StandardReferenceID == AppEnum.StandardReference.BloodBagStatus);
                query.Select
                    (
                        query.TransactionNo,
                        query.BagNo,
                        bn.VolumeBag.Coalesce("0"),
                        bn.ExpiredDateTime,
                        query.CrossmatchStartDateTime,
                        query.CrossmatchEndDateTime,
                        usr.UserName.As("ConductedByUserName"),
                        query.IsCrossMatchingSuitability,
                        query.CrossmatchCompatibleMajor,
                        query.CrossmatchCompatibleMinor,
                        query.CrossmatchCompatibleAutoControl,
                        query.CrossmatchCompatibleMinorLevel,
                        query.CrossmatchCompatibleAutoControlLevel,
                        query.IsCrossmatchingPassed,
                        bstatus.ItemName.As("BloodBagStatusName"),
                        query.IsVoid
                    );

                query.Where(query.TransactionNo == tno);

                e.DetailTableView.DataSource = query.LoadDataTable();
            }
        }
    }
}
