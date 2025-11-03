using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.Module.Charges.Billing
{
    public partial class IntermBillStatementList : BasePage
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

            switch (Request.QueryString["type"])
            {
                case "usr":
                    ProgramID = AppConstant.Program.PrintOutIntermBill;
                    break;
                case "all":
                    ProgramID = AppConstant.Program.PrintOutIntermBillAll;
                    break;
            }

            if (!IsPostBack)
            {
                var unit = new ServiceUnitCollection();
                var query = new ServiceUnitQuery("a");

                if (Request.QueryString["type"] == "usr")
                {
                    var qusr = new AppUserServiceUnitQuery("u");
                    query.InnerJoin(qusr).On(query.ServiceUnitID == qusr.ServiceUnitID);
                    query.Where(qusr.UserID == AppSession.UserLogin.UserID);
                }

                query.Where(
                    query.SRRegistrationType.In(
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
                var isEmptyFilter = string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) && string.IsNullOrEmpty(cboParamedicID.SelectedValue) && string.IsNullOrEmpty(txtRegistrationNo.Text) &&
                    string.IsNullOrEmpty(txtPatientName.Text);
                if (!ValidateSearch(isEmptyFilter, "Registration")) return null;

                var query = new RegistrationQuery("a");
                var unit = new ServiceUnitQuery("b");
                var room = new ServiceRoomQuery("c");
                var medic = new ParamedicQuery("d");
                var patient = new PatientQuery("f");
                var grr = new GuarantorQuery("g");
                var sal = new AppStandardReferenceItemQuery("sal");

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                query.Select
                    (
                        query.ParamedicID,
                        medic.ParamedicName,

                        query.RegistrationNo,
                        query.RegistrationDate,
                        query.PatientID,
                        patient.MedicalNo,
                        patient.PatientName,
                        patient.Sex,
                        grr.GuarantorName,
                        unit.ServiceUnitID,
                        unit.ServiceUnitName,
                        room.RoomName,
                        query.BedID,
                        sal.ItemName.As("SalutationName")
                    );

                query.LeftJoin(room).On(query.RoomID == room.RoomID);
                query.LeftJoin(medic).On(query.ParamedicID == medic.ParamedicID);
                query.InnerJoin(patient).On(query.PatientID == patient.PatientID);
                query.InnerJoin(grr).On(query.GuarantorID == grr.GuarantorID);
                query.InnerJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);
                query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & patient.SRSalutation == sal.ItemID);

                if (Request.QueryString["type"] == "usr")
                {
                    var qusr = new AppUserServiceUnitQuery("u");
                    query.InnerJoin(qusr).On(query.ServiceUnitID == qusr.ServiceUnitID);
                    query.Where(qusr.UserID == AppSession.UserLogin.UserID);
                    query.Where(
                        query.Or(
                            query.SRRegistrationType != AppConstant.RegistrationType.InPatient,
                            query.DischargeNotes.IsNull()));
                }
                if (cboServiceUnitID.SelectedValue != string.Empty)
                    query.Where(query.ServiceUnitID == cboServiceUnitID.SelectedValue);
                if (cboParamedicID.SelectedValue != string.Empty)
                    query.Where(query.ParamedicID == cboParamedicID.SelectedValue);
                if (txtRegistrationNo.Text != string.Empty)
                {
                    string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                    //query.Where(
                    //    query.Or(
                    //        query.RegistrationNo == searchReg,
                    //        patient.MedicalNo == searchReg,
                    //        patient.OldMedicalNo == searchReg,
                    //        string.Format("< OR REPLACE(f.MedicalNo, '-', '') LIKE '%{0}%'>", searchReg),
                    //        string.Format("< OR REPLACE(f.OldMedicalNo, '-', '') LIKE '%{0}%'>", searchReg)
                    //        )
                    //    );
                    Helper.AddFilterMedNoOrRegNoOrPatName(query, patient, searchReg, "registration");
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
                        query.IsClosed == false,
                        query.IsHoldTransactionEntry == false,
                        query.IsVoid == false,
                        query.IsFromDispensary == false,
                        query.IsNonPatient == false
                    );

                query.OrderBy
                    (
                        query.ParamedicID.Ascending,
                        query.RegistrationNo.Ascending
                    );
                query.es.Distinct = true;

                return query.LoadDataTable();
            }
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            SaveValueToCookie();

            grdList.DataSource = Registrations;
            grdList.DataBind();
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            var query = new IntermBillQuery("a");
            var py = new TransPaymentItemIntermBillQuery("b");

            query.Select
                (
                    query.IntermBillNo,
                    query.RegistrationNo,
                    query.IntermBillDate,
                    query.StartDate,
                    query.EndDate,
                    query.PatientAmount,
                    query.GuarantorAmount,
                    query.IsApproved,
                    query.IsVoid,
                    @"<CASE WHEN b.PaymentNo IS NULL THEN CAST(0 AS BIT)
                    ELSE CAST(1 AS BIT) END AS 'IsPaid'>"
                );
            query.LeftJoin(py).On(query.IntermBillNo == py.IntermBillNo && py.IsPaymentProceed == true &&
                                  py.IsPaymentReturned == false);
            query.Where
                (
                    query.RegistrationNo == e.DetailTableView.ParentItem.GetDataKeyValue("RegistrationNo").ToString()
                );
            query.OrderBy(query.IntermBillNo.Ascending);

            e.DetailTableView.DataSource = query.LoadDataTable();

        }

        protected override void RaisePostBackEvent(System.Web.UI.IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if ((sourceControl is RadGrid) && (eventArgument == "rebind"))
                grdList.Rebind();
        }

        protected void grdList_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "PrintDeposit1")
            {
                var jobParameters = new PrintJobParameterCollection();

                var jobParameter = jobParameters.AddNew();
                jobParameter.Name = "RegistrationNo";
                jobParameter.ValueString = e.CommandArgument.ToString();

                var jobParameterUser = jobParameters.AddNew();
                jobParameterUser.Name = "UserName";
                jobParameterUser.ValueString = AppSession.UserLogin.UserName;

                AppSession.PrintJobParameters = jobParameters;
                AppSession.PrintJobReportID = AppConstant.Report.Deposit1Statement;

                string script = @"var oWnd = $find('" + winPrint.ClientID + "');" +
                "oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + "');" +
                "oWnd.Show();" +
                "oWnd.Maximize();";
                RadAjaxPanel1.ResponseScripts.Add(script);
            }
        }
    }
}
