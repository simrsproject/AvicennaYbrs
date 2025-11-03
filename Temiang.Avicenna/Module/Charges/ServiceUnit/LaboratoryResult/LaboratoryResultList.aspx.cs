using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;
using Telerik.Web.UI;
using System.Data;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class LaboratoryResultList : BasePage
    {
        private bool _isHideEmptySearchMessage = false;

        private string _healthcareInitial;

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

            ProgramID = AppConstant.Program.LaboratoryResult;

            if (!IsPostBack)
            {
                _healthcareInitial = AppSession.Parameter.HealthcareInitialAppsVersion;

                //service unit
                var unit = new ServiceUnitCollection();
                var query = new ServiceUnitQuery("a");

                query.Where(
                    query.SRRegistrationType.In(
                        AppConstant.RegistrationType.ClusterPatient,
                        AppConstant.RegistrationType.EmergencyPatient,
                        AppConstant.RegistrationType.InPatient,
                        AppConstant.RegistrationType.OutPatient,
                        AppConstant.RegistrationType.MedicalCheckUp
                        ),
                    query.IsActive == true
                    );

                query.OrderBy(unit.Query.ServiceUnitName.Ascending);
                unit.Load(query);

                cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (ServiceUnit entity in unit)
                {
                    cboServiceUnitID.Items.Add(new RadComboBoxItem(entity.ServiceUnitName, entity.ServiceUnitID));
                }

                //paramedic
                var param = new ParamedicCollection();
                param.Query.Where
                    (
                        param.Query.IsActive == true,
                        param.Query.IsAvailable == true
                    );
                param.Query.OrderBy(param.Query.ParamedicName.Ascending);
                param.LoadAll();

                cboParamedicID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (Paramedic entity in param)
                {
                    cboParamedicID.Items.Add(new RadComboBoxItem(entity.ParamedicName, entity.ParamedicID));
                }

                txtTransactionDate.SelectedDate = DateTime.Now.Date;
                txtTransactionDate2.SelectedDate = DateTime.Now.Date;
                grdList.Columns[grdList.Columns.Count - 1].Visible = AppSession.Parameter.HealthcareInitialAppsVersion == "RSMM";
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (_healthcareInitial != "RSSA")
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
            var grd = (RadGrid)source;
            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grd.DataSource = new String[] { };
                return;
            }

            var dataSource = TransCharges;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                grd.DataSource = dataSource;
            }
        }

        private DataTable TransCharges
        {
            get
            {
                var isEmptyFilter = txtTransactionDate.IsEmpty && txtTransactionDate2.IsEmpty && string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) && string.IsNullOrEmpty(cboParamedicID.SelectedValue) &&
                    string.IsNullOrEmpty(txtRegistrationNo.Text) && string.IsNullOrEmpty(txtPatientName.Text);
                if (!ValidateSearch(isEmptyFilter, "Transaction")) return null;

                DataTable dtb;
                dtb = TransChargesPatient;

                //foreach (DataRow row in dtb.Rows)
                //{
                //    var count = 0;
                //    var tcics = new TransChargesItemCollection();
                //    tcics.Query.Where(tcics.Query.TransactionNo == row["TransactionNo"].ToString(), tcics.Query.IsApprove == true, tcics.Query.IsBillProceed == true, tcics.Query.IsOrderRealization == true);
                //    if (tcics.Query.Load())
                //    {
                //        tcics = new TransChargesItemCollection();
                //        count = tcics.TransChargesItemWithCorrection(row["TransactionNo"].ToString()).Rows.Count;
                //    }
                //    if (count == 0) row.Delete();
                //}

                //dtb.AcceptChanges();

                return dtb;
            }
        }

        private DataTable TransChargesPatient
        {
            get 
            {
                var query = new RegistrationQuery("e");
                var tc = new TransChargesQuery("x");
                var tci = new TransChargesItemQuery("xx");
                var unit = new ServiceUnitQuery("b");
                var room = new ServiceRoomQuery("c");
                var medic = new ParamedicQuery("d");
                var patient = new PatientQuery("f");
                var grr = new GuarantorQuery("g");
                var sal = new AppStandardReferenceItemQuery("sal");

                query.Select
                    (
                        room.RoomName,
                        query.RegistrationDate,
                        unit.ServiceUnitID,
                        query.ParamedicID,
                        medic.ParamedicName,
                        query.RegistrationNo,
                        patient.MedicalNo,
                        patient.PatientName,
                        patient.FirstName,
                        patient.MiddleName,
                        patient.LastName,
                        patient.Sex,
                        grr.GuarantorName,
                        query.PatientID,
                        query.IsConsul,
                        query.BedID,
                        "<CAST(0 AS BIT) AS IsInpatient>",
                        "<'' AS SRTriage>",
                        tc.TransactionNo,
                        tc.TransactionDate,
                        tc.Notes,
                        tc.IsValidated,
                        tc.ValidatedByUserID,
                        tc.ValidatedDateTime,
                        tc.IsVoid,
                        sal.ItemName.As("SalutationName"),
                        @"<CAST(0 AS BIT) AS IsMergeVisible>",
                        @"<b.ServiceUnitName + ', ' + c.RoomName + (CASE WHEN ISNULL(e.BedID, '') = '' THEN '' ELSE ', ' + e.BedID END) AS 'Group'>"
                    );

                query.InnerJoin(tc).On(query.RegistrationNo == tc.RegistrationNo && tc.ToServiceUnitID == AppSession.Parameter.ServiceUnitLaboratoryID && tc.IsApproved == true && tc.IsCorrection == false);
                query.InnerJoin(tci).On(tci.TransactionNo == tc.TransactionNo && tci.IsApprove == true && tci.IsBillProceed == true && tci.IsOrderRealization == true && tci.IsCorrection == false);
                query.InnerJoin(unit).On(tc.FromServiceUnitID == unit.ServiceUnitID);
                query.LeftJoin(room).On(tc.RoomID == room.RoomID);
                query.LeftJoin(medic).On(query.ParamedicID == medic.ParamedicID);
                query.InnerJoin(patient).On(query.PatientID == patient.PatientID);
                query.InnerJoin(grr).On(query.GuarantorID == grr.GuarantorID);
                query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & patient.SRSalutation == sal.ItemID);

                if (!txtTransactionDate.IsEmpty && !txtTransactionDate2.IsEmpty)
                    query.Where(tc.TransactionDate >= txtTransactionDate.SelectedDate, tc.TransactionDate < txtTransactionDate2.SelectedDate.Value.AddDays(1));

                if (cboServiceUnitID.SelectedValue != string.Empty)
                    query.Where(tc.FromServiceUnitID == cboServiceUnitID.SelectedValue);
                if (cboParamedicID.SelectedValue != string.Empty)
                    query.Where(query.ParamedicID == cboParamedicID.SelectedValue);
                if (txtRegistrationNo.Text != string.Empty)
                {
                    string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                    query.Where(
                            query.Or(
                                query.RegistrationNo == searchReg,
                                patient.MedicalNo == searchReg,
                                string.Format("< OR REPLACE(f.MedicalNo, '-', '') LIKE '%{0}%'>", searchReg)
                                )
                            );
                }

                if (txtPatientName.Text != string.Empty)
                {
                    string searchPatient = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";
                    query.Where
                        (
                         string.Format("<RTRIM(f.FirstName+' '+f.MiddleName)+' '+f.LastName LIKE '{0}'>", searchPatient)
                        );
                }

                query.Where
                    (
                        query.IsVoid == false,
                        query.IsNonPatient == false, 
                        query.IsFromDispensary == false
                    );

                query.OrderBy
                    (
                        tc.TransactionNo.Ascending,
                        query.ParamedicID.Ascending,
                        query.RegistrationNo.Ascending
                    );

                query.GroupBy(room.RoomName,
                        query.RegistrationDate,
                        unit.ServiceUnitID,
                        query.ParamedicID,
                        medic.ParamedicName,
                        query.RegistrationNo,
                        patient.MedicalNo,
                        patient.PatientName,
                        patient.FirstName,
                        patient.MiddleName,
                        patient.LastName,
                        patient.Sex,
                        grr.GuarantorName,
                        query.PatientID,
                        query.IsConsul,
                        query.BedID,
                        tc.TransactionNo,
                        tc.TransactionDate,
                        tc.Notes,
                        tc.IsValidated,
                        tc.ValidatedByUserID,
                        tc.ValidatedDateTime,
                        tc.IsVoid,
                        sal.ItemName,
                        unit.ServiceUnitName, 
                        query.BedID);

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                DataTable dtb = query.LoadDataTable();

                return dtb;
            }
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            SaveValueToCookie();

            grdList.Rebind();
        }

        protected void grdList_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "Print")
            {
                var jobParameters = new PrintJobParameterCollection();
                jobParameters.AddNew("p_TransactionNo", e.CommandArgument.ToString());

                AppSession.PrintJobParameters = jobParameters;
                AppSession.PrintJobReportID = AppConstant.Report.LaboratoryResult;

                string script = @"var oWnd = $find('" + winPrint.ClientID + "');" +
                                "oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + "');" +
                                "oWnd.Show();" +
                                "oWnd.Maximize();";
                RadAjaxPanel1.ResponseScripts.Add(script);
            }
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
        {
            base.RaisePostBackEvent(source, eventArgument);

            if (!(source is RadGrid))
                return;

            if (eventArgument == "rebind")
                grdList.Rebind();
        }
    }
}
