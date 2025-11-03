using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using Temiang.Dal.Interfaces;
using System.Web.UI;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.Nutrient.Transaction
{
    public partial class NonPatientMealOrderList : BasePage
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

            ProgramID = AppConstant.Program.NonPatientCustomerMealOrder;

            if (!IsPostBack)
            {
                var query = new ServiceUnitQuery("a");
                var usrq = new AppUserServiceUnitQuery("b");
                query.InnerJoin(usrq).On(usrq.ServiceUnitID == query.ServiceUnitID &&
                                         usrq.UserID == AppSession.UserLogin.UserID);
                query.Where(
                    query.SRRegistrationType.In(AppConstant.RegistrationType.InPatient,
                        AppConstant.RegistrationType.EmergencyPatient, AppConstant.RegistrationType.OutPatient,
                        AppConstant.RegistrationType.MedicalCheckUp), query.IsActive == true);
                query.OrderBy(query.DepartmentID.Ascending);

                var coll = new ServiceUnitCollection();
                coll.Load(query);

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
            var dataSource = Registrations;
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

        private DataTable Registrations
        {
            get
            {
                var isEmptyFilter = txtOrderDate1.IsEmpty && txtOrderDate2.IsEmpty && string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) 
                    && string.IsNullOrEmpty(txtRegistrationNo.Text) && string.IsNullOrEmpty(txtPatientName.Text);
                if (!ValidateSearch(isEmptyFilter, "Meal Non Patient")) return null;

                var qr = new RegistrationQuery("r");
                var qp = new PatientQuery("p");
                var qm = new ParamedicQuery("m");
                var unit = new ServiceUnitQuery("s");
                var room = new ServiceRoomQuery("d");
                var grr = new GuarantorQuery("c");
                var sal = new AppStandardReferenceItemQuery("sal");
                var usrunit = new AppUserServiceUnitQuery("un");

                qr.es.Top = AppSession.Parameter.MaxResultRecord;

                qr.Select
                    (
                        qr.RegistrationNo,
                        qr.RegistrationDate,
                        qr.RegistrationTime,
                        qp.MedicalNo,
                        qp.PatientName,
                        qp.Sex,
                        qm.ParamedicName,
                        unit.ServiceUnitName,
                        room.RoomName,
                        qr.BedID,
                        qr.IsTransferedToInpatient,
                        qr.SRRegistrationType,
                        grr.GuarantorName,
                        sal.ItemName.As("SalutationName")
                    );

                qr.InnerJoin(qp).On(qp.PatientID == qr.PatientID);
                qr.LeftJoin(qm).On(qm.ParamedicID == qr.ParamedicID);
                qr.LeftJoin(unit).On(unit.ServiceUnitID == qr.ServiceUnitID);
                qr.LeftJoin(room).On(room.RoomID == qr.RoomID);
                qr.InnerJoin(grr).On(grr.GuarantorID == qr.GuarantorID);
                qr.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & sal.ItemID == qp.SRSalutation);
                qr.InnerJoin(usrunit).On(usrunit.UserID == AppSession.UserLogin.UserID &
                                         usrunit.ServiceUnitID == qr.ServiceUnitID);

                if (!txtOrderDate1.IsEmpty && !txtOrderDate2.IsEmpty)
                    qr.Where(qr.RegistrationDate.Between(txtOrderDate1.SelectedDate, txtOrderDate2.SelectedDate));
                if (cboServiceUnitID.SelectedValue != string.Empty)
                    qr.Where(qr.ServiceUnitID == cboServiceUnitID.SelectedValue);
                if (txtRegistrationNo.Text != string.Empty)
                {
                    string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                    qr.Where
                        (qr.Or
                             (
                                 string.Format("<r.RegistrationNo = '{0}' OR >", searchReg),
                                 string.Format("<p.MedicalNo = '{0}' OR >", searchReg),
                                 string.Format("<p.OldMedicalNo = '{0}'>", searchReg),
                                 string.Format("< OR REPLACE(p.MedicalNo, '-', '') LIKE '%{0}%'>", searchReg),
                                 string.Format("< OR REPLACE(p.OldMedicalNo, '-', '') LIKE '%{0}%'>", searchReg)
                             )
                        );
                }

                if (txtPatientName.Text != string.Empty)
                {
                    string searchPatient = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";
                    qr.Where
                        (
                          string.Format("<LTRIM(RTRIM(LTRIM(p.FirstName + ' ' + p.MiddleName)) + ' ' + p.LastName) LIKE '{0}'>", searchPatient)
                        );
                }

                qr.Where(
                    qr.IsClosed == false,
                    qr.IsVoid == false,
                    qr.IsHoldTransactionEntry == false,
                    qr.IsNonPatient == false,
                    qr.SRDischargeMethod == string.Empty);
                qr.OrderBy(qr.RegistrationNo.Descending);

                DataTable tbl = qr.LoadDataTable();

                return tbl;
            }
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            var query = new MealOrderNonPatientQuery("a");
            query.es.Top = AppSession.Parameter.MaxResultRecord;
            query.Select
                (
                    query.RegistrationNo,
                    query.TransactionNo,
                    query.TransactionDate,
                    query.IsApproved,
                    query.IsVoid,
                    query.IsDistributed,
                    "<'NonPatientMealOrderDetail.aspx?md=view&id='+a.TransactionNo+'&regno='+a.RegistrationNo AS RequestUrl>"
                );

            query.Where(query.RegistrationNo == e.DetailTableView.ParentItem.GetDataKeyValue("RegistrationNo").ToString());

            e.DetailTableView.DataSource = query.LoadDataTable();
        }
    }
}