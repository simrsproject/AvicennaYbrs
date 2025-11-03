using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using System.Web.UI;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class MergeBillingList : BasePage
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

            ProgramID = AppConstant.Program.MergeBilling;

            if (!IsPostBack)
            {
                var coll = new ServiceUnitCollection();
                var unit = new ServiceUnitQuery("a");

                unit.Where
                    (
                        unit.SRRegistrationType.In(
                            AppConstant.RegistrationType.EmergencyPatient,
                            AppConstant.RegistrationType.InPatient,
                            AppConstant.RegistrationType.OutPatient,
                            AppConstant.RegistrationType.MedicalCheckUp
                        ),
                        unit.IsActive == true
                    );
                unit.OrderBy(unit.DepartmentID.Ascending);

                coll.Load(unit);

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

            if (AppSession.Parameter.HealthcareInitialAppsVersion != "RSSA")
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

        protected void grdRegisterOpenList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
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

        private DataTable Registrations
        {
            get
            {
                var isEmptyFilter = txtOrderDate1.IsEmpty && txtOrderDate2.IsEmpty && string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) && string.IsNullOrEmpty(txtRegistrationNo.Text) &&
                   string.IsNullOrEmpty(txtPatientName.Text);
                if (!ValidateSearch(isEmptyFilter, "Registration")) return null;

                var qr = new RegistrationQuery("r");
                var qp = new PatientQuery("p");
                var qm = new ParamedicQuery("m");
                var unit = new ServiceUnitQuery("s");
                var room = new ServiceRoomQuery("d");
                var mrg = new MergeBillingQuery("b");
                var guar = new GuarantorQuery("g");
                var sal = new AppStandardReferenceItemQuery("sal");

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
                        qr.IsHoldTransactionEntry,
                        guar.GuarantorName,
                        sal.ItemName.As("SalutationName")
                    );

                qr.InnerJoin(qp).On(qr.PatientID == qp.PatientID);
                qr.LeftJoin(qm).On(qr.ParamedicID == qm.ParamedicID);
                qr.LeftJoin(unit).On(qr.ServiceUnitID == unit.ServiceUnitID);
                qr.LeftJoin(room).On(qr.RoomID == room.RoomID);
                qr.InnerJoin(mrg).On(qr.RegistrationNo == mrg.RegistrationNo);
                qr.InnerJoin(guar).On(qr.GuarantorID == guar.GuarantorID);
                qr.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & qp.SRSalutation == sal.ItemID);

                if (!txtOrderDate1.IsEmpty && !txtOrderDate2.IsEmpty)
                    qr.Where(qr.RegistrationDate >= txtOrderDate1.SelectedDate, qr.RegistrationDate <= txtOrderDate2.SelectedDate);
                if (cboServiceUnitID.SelectedValue != string.Empty)
                    qr.Where(qr.ServiceUnitID == cboServiceUnitID.SelectedValue);
                if (txtRegistrationNo.Text != string.Empty)
                {
                    string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                    //qr.Where(
                    //    qr.Or(
                    //        qr.RegistrationNo == searchReg,
                    //        qp.MedicalNo == searchReg,
                    //        qp.OldMedicalNo == searchReg,
                    //        string.Format("< OR REPLACE(p.MedicalNo, '-', '') LIKE '%{0}%'>", searchReg),
                    //        string.Format("< OR REPLACE(p.OldMedicalNo, '-', '') LIKE '%{0}%'>", searchReg)
                    //        )
                    //    );
                    Helper.AddFilterMedNoOrRegNoOrPatName(qr, qp, searchReg, "registration");
                }
                if (txtPatientName.Text != string.Empty)
                {
                    string searchPatient = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";
                    qr.Where(string.Format("<LTRIM(RTRIM(LTRIM(p.FirstName + ' ' + p.MiddleName)) + ' ' + p.LastName) LIKE '{0}'>", searchPatient));
                }

                qr.Where(
                    //qr.IsConsul == false,
                    qr.IsClosed == false,
                    mrg.FromRegistrationNo == string.Empty
                    );
                qr.OrderBy(qr.RegistrationNo.Descending);

                return qr.LoadDataTable();
            }
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (AppSession.Parameter.HealthcareInitialAppsVersion != "RSSA")
                SaveValueToCookie();

            grdRegisterOpenList.DataSource = Registrations;
            grdRegisterOpenList.DataBind();
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
        {
            base.RaisePostBackEvent(source, eventArgument);

            if (!(source is RadGrid))
                return;

            if (eventArgument == "rebind")
                grdRegisterOpenList.Rebind();
            else if (eventArgument.Split('|')[0] == "unmerge")
            {
                var regNo = eventArgument.Split('|')[1];
                var mrg = new MergeBilling();
                if (mrg.LoadByPrimaryKey(regNo))
                {
                    using (var trans = new esTransactionScope())
                    {
                        // history
                        var hist = new MergeBillingHistory();
                        hist.AddNew();
                        hist.RegistrationNo = regNo;
                        hist.FromRegistrationNoBefore = mrg.FromRegistrationNo;
                        hist.FromRegistrationNoAfter = string.Empty;
                        hist.LastUpdateDateTime = DateTime.Now;
                        hist.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        hist.Save();

                        // unmerge jasmed
                        var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
                        feeColl.UnSetMergeBilling(mrg.FromRegistrationNo, AppSession.Parameter.IsFeeCalculatedOnTransaction);
                        feeColl.Save();

                        mrg.FromRegistrationNo = string.Empty;
                        mrg.Save();

                        var reg = new Registration();
                        if (reg.LoadByPrimaryKey(regNo))
                        {
                            reg.IsTransferedToInpatient = false;
                            reg.Save();
                        }

                        trans.Complete();
                    }
                }

                grdRegisterOpenList.Rebind();
            }
        }

        protected void grdRegisterOpenList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            var qr = new RegistrationQuery("r");
            var qp = new PatientQuery("p");
            var qm = new ParamedicQuery("m");
            var unit = new ServiceUnitQuery("s");
            var room = new ServiceRoomQuery("d");
            var mrg = new MergeBillingQuery("b");
            var guar = new GuarantorQuery("g");

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
                    qr.IsHoldTransactionEntry,
                    guar.GuarantorName,
                    qr.IsConsul,
                    mrg.LastUpdateByUserID,
                    mrg.LastUpdateDateTime,
                    mrg.FromRegistrationNo,
                    @"<CAST(1 AS BIT) AS IsVisible>"
                );

            qr.InnerJoin(qp).On(qr.PatientID == qp.PatientID);
            qr.LeftJoin(qm).On(qr.ParamedicID == qm.ParamedicID);
            qr.LeftJoin(unit).On(qr.ServiceUnitID == unit.ServiceUnitID);
            qr.LeftJoin(room).On(qr.RoomID == room.RoomID);
            qr.InnerJoin(mrg).On(
                qr.RegistrationNo == mrg.RegistrationNo &&
                mrg.FromRegistrationNo == e.DetailTableView.ParentItem.GetDataKeyValue("RegistrationNo").ToString()
                );
            qr.InnerJoin(guar).On(qr.GuarantorID == guar.GuarantorID);
            qr.Where(qr.IsVoid == false);
            qr.OrderBy(qr.RegistrationNo.Descending);

            DataTable dtb = qr.LoadDataTable();

            if (AppSession.Parameter.IsUnmergeBillingCheckingIntermbillProcess)
            {
                foreach (DataRow row in dtb.Rows)
                {
                    var ibColl = new IntermBillCollection();
                    ibColl.Query.Where(ibColl.Query.RegistrationNo == row["FromRegistrationNo"].ToString(), ibColl.Query.IsVoid == false);
                    ibColl.LoadAll();
                    if (ibColl.Count > 0)
                    {
                        foreach (var ib in ibColl)
                        { 
                            var cost = new CostCalculationCollection();
                            cost.Query.Where(cost.Query.RegistrationNo == row["RegistrationNo"].ToString(), cost.Query.IntermBillNo == ib.IntermBillNo);
                            cost.LoadAll();
                            if (cost.Count > 0)
                                row["IsVisible"] = false;
                        }
                    }
                }

                dtb.AcceptChanges();
            }

            e.DetailTableView.DataSource = dtb;
        }
    }
}
