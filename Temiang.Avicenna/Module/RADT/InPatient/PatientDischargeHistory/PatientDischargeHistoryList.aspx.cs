using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.Module.RADT.InPatient
{
    public partial class PatientDischargeHistoryList : BasePage
    {
        private bool _isHideEmptySearchMessage = false;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            _isHideEmptySearchMessage = false;
            if (Page.IsPostBack)
            {
                if (Request["__EVENTTARGET"].Contains("grd") &&
                    Request["__EVENTARGUMENT"].Contains("rebind"))
                {
                    _isHideEmptySearchMessage = true;
                }
            }

            ProgramID = AppConstant.Program.PatientDischargeHistory;

            if (!IsPostBack)
            {
                if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSCH")
                    txtDischargeDate.SelectedDate = DateTime.Now;

                var coll = new ServiceUnitCollection();
                coll.Query.Where(
                    coll.Query.SRRegistrationType == AppConstant.RegistrationType.InPatient,
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
            var grd = (RadGrid)source;

            if (!IsPostBack && !IsListLoadRecordOnInit) {
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

        private DataTable Registrations
        {
            get
            {
                var isEmptyFilter = txtRegistrationDate1.IsEmpty && txtRegistrationDate2.IsEmpty && string.IsNullOrEmpty(txtRegistrationNo.Text) && txtDischargeDate.IsEmpty && string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) && string.IsNullOrEmpty(txtPatientName.Text);
                if (!ValidateSearch(isEmptyFilter, "Registration")) return null;

                var qr = new RegistrationQuery("r");
                var qp = new PatientQuery("p");
                var qm = new ParamedicQuery("m");
                var unit = new ServiceUnitQuery("s");
                var room = new ServiceRoomQuery("d");
                var mrg = new MergeBillingQuery("b");
                var grr = new GuarantorQuery("c");
                var sal = new AppStandardReferenceItemQuery("sal");
                var dm = new AppStandardReferenceItemQuery("dm");
                var dc = new AppStandardReferenceItemQuery("dc");
                
                qr.es.Top = AppSession.Parameter.MaxResultRecord;

                qr.Select
                    (
                        qr.RegistrationDate,
                        qr.DischargeDate,
                        qr.DischargeTime,
                        qr.RegistrationNo,
                        qp.MedicalNo,
                        qp.PatientName,
                        qp.Sex,
                        qm.ParamedicName,
                        unit.ServiceUnitName,
                        room.RoomName,
                        qr.BedID,
                        grr.GuarantorName,
                        sal.ItemName.As("SalutationName"),
                        dm.ItemName.As("DischargeMethod"),
                        dc.ItemName.As("DischargeCondition")
                    );

                qr.InnerJoin(qp).On(qr.PatientID == qp.PatientID);
                qr.LeftJoin(qm).On(qr.ParamedicID == qm.ParamedicID);
                qr.LeftJoin(unit).On(qr.ServiceUnitID == unit.ServiceUnitID);
                qr.LeftJoin(room).On(qr.RoomID == room.RoomID);
                qr.InnerJoin(mrg).On(qr.RegistrationNo == mrg.RegistrationNo);
                qr.InnerJoin(grr).On(qr.GuarantorID == grr.GuarantorID);
                qr.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & qp.SRSalutation == sal.ItemID);
                qr.LeftJoin(dm).On(dm.StandardReferenceID == AppEnum.StandardReference.DischargeMethod.ToString() & dm.ItemID == qr.SRDischargeMethod);
                qr.LeftJoin(dc).On(dc.StandardReferenceID == AppEnum.StandardReference.DischargeCondition.ToString() & dc.ItemID == qr.SRDischargeCondition);

                if (!txtRegistrationDate1.IsEmpty && !txtRegistrationDate2.IsEmpty)
                    qr.Where(qr.RegistrationDate.Between(txtRegistrationDate1.SelectedDate, txtRegistrationDate2.SelectedDate));
                if (cboServiceUnitID.SelectedValue != string.Empty)
                    qr.Where(qr.ServiceUnitID == cboServiceUnitID.SelectedValue);
                if (txtRegistrationNo.Text != string.Empty)
                {
                    string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                    //qr.Where
                    //    (qr.Or
                    //        (
                    //            string.Format("<r.RegistrationNo = '{0}' OR >", searchReg),
                    //            string.Format("<p.MedicalNo = '{0}' OR >", searchReg),
                    //            string.Format("<p.OldMedicalNo = '{0}'>", searchReg),
                    //            string.Format("< OR REPLACE(p.MedicalNo, '-', '') LIKE '%{0}%'>", searchReg),
                    //            string.Format("< OR REPLACE(p.OldMedicalNo, '-', '') LIKE '%{0}%'>", searchReg)
                    //         )
                    //    );
                    Helper.AddFilterMedNoOrRegNoOrPatName(qr, qp, searchReg, "registration");
                }
                if (!txtDischargeDate.IsEmpty)
                    qr.Where(qr.DischargeDate == txtDischargeDate.SelectedDate);

                if (txtPatientName.Text != string.Empty)
                {
                    string searchPatient = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";
                    qr.Where
                        (
                          string.Format("<LTRIM(RTRIM(LTRIM(p.FirstName + ' ' + p.MiddleName)) + ' ' + p.LastName) LIKE '{0}'>", searchPatient)
                        );
                }
                if (!chkIncludeClosedPatients.Checked)
                    qr.Where(qr.IsClosed == false);

                qr.Where(
                    qr.IsConsul == false,
                    qr.IsVoid == false,
                    qr.SRRegistrationType == AppConstant.RegistrationType.InPatient
                    );

                qr.OrderBy(qr.RegistrationNo.Descending);

                return qr.LoadDataTable();
            }
        }

        protected void btnSearchPatient_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            grdList.Rebind();
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            var query = new PatientDischargeHistoryQuery("a");
            var usrQ = new AppUserQuery("b");
            var dm = new AppStandardReferenceItemQuery("dm");
            var dc = new AppStandardReferenceItemQuery("dc");

            query.InnerJoin(usrQ).On(query.DischargeOperatorID == usrQ.UserID);
            query.LeftJoin(dm).On(dm.StandardReferenceID == AppEnum.StandardReference.DischargeMethod.ToString() & dm.ItemID == query.SRDischargeMethod);
            query.LeftJoin(dc).On(dc.StandardReferenceID == AppEnum.StandardReference.DischargeCondition.ToString() & dc.ItemID == query.SRDischargeCondition);

            query.Select
            (
                query.RegistrationNo,
                query.BedID,
                query.DischargeDate,
                query.DischargeTime,
                query.DischargeOperatorID,
                usrQ.UserName,
                query.IsCancel,
                query.LastUpdateDateTime,
                @"<CONVERT(VARCHAR(5), a.LastUpdateDateTime, 114) AS LastUpdateTime>",
                dm.ItemName.As("DischargeMethod"),
                dc.ItemName.As("DischargeCondition")
            );

            query.Where(query.RegistrationNo == e.DetailTableView.ParentItem.GetDataKeyValue("RegistrationNo").ToString());
            query.OrderBy(query.LastUpdateDateTime.Descending);
           
            e.DetailTableView.DataSource = query.LoadDataTable();
        }
    }
}
