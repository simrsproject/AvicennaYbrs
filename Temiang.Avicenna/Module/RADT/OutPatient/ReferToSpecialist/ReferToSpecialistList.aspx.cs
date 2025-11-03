using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Linq;
using Telerik.Web.UI;
using Temiang.Dal.Interfaces;
using System.Web.UI;

namespace Temiang.Avicenna.Module.RADT.OutPatient
{
    public partial class ReferToSpecialistList : BasePage
    {
        private bool _isHideEmptySearchMessage = false;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e); // !!Jika tidak dipanggil, tampilan jadi tidak rapih
            _isHideEmptySearchMessage = false;
            if (Page.IsPostBack)
            {
                if (Request["__EVENTTARGET"].Contains("grd") &&
                    Request["__EVENTARGUMENT"].Contains("rebind"))
                {
                    _isHideEmptySearchMessage = true;
                }
            }

            ProgramID = AppConstant.Program.RefferToSpecialist;

            if (!IsPostBack)
            {
                //service unit
                var unit = new ServiceUnitCollection();
                var query = new ServiceUnitQuery("a");
                //var qusr = new AppUserServiceUnitQuery("u");

                //query.InnerJoin(qusr).On(query.ServiceUnitID == qusr.ServiceUnitID);

                query.Where(query.SRRegistrationType == AppConstant.RegistrationType.OutPatient,
                    query.IsActive == true
                    //,
                    //qusr.UserID == AppSession.UserLogin.UserID
                    );

                query.OrderBy(unit.Query.ServiceUnitName.Ascending);
                unit.Load(query);

                cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (ServiceUnit entity in unit)
                {
                    cboServiceUnitID.Items.Add(new RadComboBoxItem(entity.ServiceUnitName, entity.ServiceUnitID));
                }

                ComboBox.PopulateWithParamedic(cboParamedicID);
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

        #region Registration Grid

        protected void grdRegistration_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var grd = (RadGrid)source;

            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grd.DataSource = new String[] { };
                return;
            }

            var dataSource = Registrations;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
            }
        }

        protected void grdRegistration_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            var query = new ServiceUnitQueQuery("a");

            var qs = new ServiceUnitQuery("s");
            query.InnerJoin(qs).On(query.ServiceUnitID == qs.ServiceUnitID);

            var qm = new ParamedicQuery("m");
            query.LeftJoin(qm).On(query.ParamedicID == qm.ParamedicID);

            var merge = new MergeBillingQuery("a");
            var reg = new RegistrationQuery("b");

            merge.Select(merge.RegistrationNo);
            merge.InnerJoin(reg).On(merge.RegistrationNo == reg.RegistrationNo);
            merge.Where(
                merge.FromRegistrationNo == e.DetailTableView.ParentItem.GetDataKeyValue("RegistrationNo").ToString(),
                reg.IsClosed == false,
                reg.IsVoid == false,
                reg.IsHoldTransactionEntry == false
                );
            var tab = merge.LoadDataTable();

            query.Select
                (
                    string.Format("<'{0}' AS RegistrationNo>", e.DetailTableView.ParentItem.GetDataKeyValue("RegistrationNo").ToString()),
                    query.ServiceUnitID,
                    query.ParamedicID,
                    query.QueDate,
                    query.QueNo,
                    query.RegistrationNo.As("ReferNo"),
                    query.IsClosed,
                    query.Notes,
                    qs.ServiceUnitName,
                    qm.ParamedicName
                );

            if (tab.Rows.Count > 0)
                query.Where(
                        query.RegistrationNo.In((from t in tab.AsEnumerable()
                                                 select t.Field<string>("RegistrationNo")).ToArray())
                    );
            else
                query.Where(query.RegistrationNo == string.Empty);

            query.Where(query.IsFromReferProcess == true);

            query.OrderBy(query.QueDate.Descending);

            e.DetailTableView.DataSource = query.LoadDataTable();
        }

        private DataTable Registrations
        {
            get
            {
                var isEmptyFilter = string.IsNullOrEmpty(txtMedicalNo.Text) && string.IsNullOrEmpty(txtPatientSearch.Text) && string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) && string.IsNullOrEmpty(cboParamedicID.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "Registration")) return null;

                var qr = new RegistrationQuery("r");
                var qp = new PatientQuery("p");
                var qm = new ParamedicQuery("m");
                var unit = new ServiceUnitQuery("s");
                var room = new ServiceRoomQuery("d");
                var sal = new AppStandardReferenceItemQuery("sal");

                qr.InnerJoin(qp).On(qr.PatientID == qp.PatientID);
                qr.LeftJoin(qm).On(qr.ParamedicID == qm.ParamedicID);
                qr.InnerJoin(unit).On(qr.ServiceUnitID == unit.ServiceUnitID);
                qr.LeftJoin(room).On(qr.RoomID == room.RoomID);
                qr.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & qp.SRSalutation == sal.ItemID);

                qr.Where
                    (
                        qr.SRRegistrationType == AppConstant.RegistrationType.OutPatient,
                        qr.IsClosed == false,
                        qr.IsHoldTransactionEntry == false,
                        qr.IsConsul == false,
                        qr.IsFromDispensary == false
                    );

                if (!txtPatientSearch.Text.Trim().Equals(string.Empty))
                {
                    string searchPatient = "%" + Helper.EscapeQuery(txtPatientSearch.Text) + "%";
                    qr.Where
                         (
                             string.Format("<LTRIM(RTRIM(LTRIM(p.FirstName + ' ' + p.MiddleName)) + ' ' + p.LastName) LIKE '{0}'>", searchPatient)
                         );
                }
                if (!txtMedicalNo.Text.Trim().Equals(string.Empty))
                {
                    string searchMedNo = Helper.EscapeQuery(txtMedicalNo.Text);
                    if (AppSession.Parameter.IsMedicalNoContainStrip)
                        qr.Where(
                            qr.Or(
                                qp.MedicalNo == searchMedNo,
                                qp.OldMedicalNo == searchMedNo,
                                string.Format("< OR REPLACE(p.MedicalNo, '-', '') LIKE '%{0}%'>", searchMedNo),
                                string.Format("< OR REPLACE(p.OldMedicalNo, '-', '') LIKE '%{0}%'>", searchMedNo)
                                )
                            );
                    else
                        qr.Where(
                            qr.Or(
                                qp.MedicalNo == searchMedNo,
                                qp.OldMedicalNo == searchMedNo,
                                string.Format("< OR p.MedicalNo LIKE '%{0}%'>", searchMedNo),
                                string.Format("< OR p.OldMedicalNo LIKE '%{0}%'>", searchMedNo)
                                )
                            );
                }
                if (cboParamedicID.SelectedValue != string.Empty)
                    qr.Where(qr.ParamedicID == cboParamedicID.SelectedValue);

                if (cboServiceUnitID.SelectedValue != string.Empty)
                    qr.Where(qr.ServiceUnitID == cboServiceUnitID.SelectedValue);

                qr.Select
                    (
                        qr.PatientID,
                        qr.RegistrationDate,
                        qr.RegistrationTime,
                        qr.RegistrationNo,
                        qp.MedicalNo,
                        qp.PatientName,
                        qp.Sex,
                        qm.ParamedicName,
                        unit.ServiceUnitName,
                        room.RoomName,
                        sal.ItemName.As("SalutationName")
                    );

                qr.OrderBy(qr.RegistrationDate.Descending);

                qr.es.Top = AppSession.Parameter.MaxResultRecord;

                return qr.LoadDataTable();
            }
        }

        #endregion

        protected void cboServiceUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (e.Value != string.Empty)
            {
                var medic = new ParamedicQuery("a");
                var unit = new ServiceUnitParamedicQuery("b");

                medic.InnerJoin(unit).On(medic.ParamedicID == unit.ParamedicID);
                medic.Where
                    (
                        medic.IsActive == true,
                        unit.ServiceUnitID == e.Value);

                medic.OrderBy(medic.ParamedicName.Ascending);

                var param = new ParamedicCollection();
                param.Load(medic);

                cboParamedicID.Items.Clear();
                cboParamedicID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (Paramedic entity in param)
                {
                    cboParamedicID.Items.Add(new RadComboBoxItem(entity.ParamedicName, entity.ParamedicID));
                }
            }
            else
                cboParamedicID.Items.Clear();
        }

        protected void btnSearchPatient_Click(object sender, ImageClickEventArgs e)
        {
            SaveValueToCookie();

            grdRegistration.Rebind();
        }
    }
}
