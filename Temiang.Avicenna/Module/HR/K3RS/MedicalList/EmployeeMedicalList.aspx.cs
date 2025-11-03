using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.HR.K3RS
{
    public partial class EmployeeMedicalList : BasePage
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

            ProgramID = AppConstant.Program.K3RS_EmployeeMedicalList;

            if (IsPostBack) return;

            var first = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).Date;
            var last = first.AddMonths(1).AddDays(-1).Date;

            txtRegistrationDateFrom.SelectedDate = first;
            txtRegistrationDateTo.SelectedDate = last;

            var unit = new ServiceUnitCollection();
            unit.Query.Where(
                unit.Query.SRRegistrationType.In(
                    AppConstant.RegistrationType.ClusterPatient,
                            AppConstant.RegistrationType.EmergencyPatient,
                            AppConstant.RegistrationType.InPatient,
                            AppConstant.RegistrationType.OutPatient,
                            AppConstant.RegistrationType.MedicalCheckUp
                            ),
                unit.Query.IsActive == true
            );
            unit.Query.OrderBy(unit.Query.ServiceUnitName.Ascending);
            unit.Query.Load();
            cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (var u in unit)
            {
                cboServiceUnitID.Items.Add(new RadComboBoxItem(u.ServiceUnitName, u.ServiceUnitID));
            }

            var param = new ParamedicCollection();
            param.Query.Where
                (
                    param.Query.IsActive == true,
                    param.Query.IsAvailable == true
                );
            param.Query.OrderBy(param.Query.ParamedicName.Ascending);
            param.Query.Load();
            cboParamedicID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (var p in param)
            {
                cboParamedicID.Items.Add(new RadComboBoxItem(p.ParamedicName, p.ParamedicID));
            }

            StandardReference.InitializeIncludeSpace(cboSRVisitReason, AppEnum.StandardReference.VisitReason);

            var grr = new GuarantorCollection();
            grr.Query.Where(grr.Query.IsActive == true);
            grr.Query.Load();
            cboGuarantorID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (var entity in grr)
            {
                cboGuarantorID.Items.Add(new RadComboBoxItem(entity.GuarantorName, entity.GuarantorID));
            }

            StandardReference.InitializeIncludeSpace(cboSRRegistrationType, AppEnum.StandardReference.RegistrationType);
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
            var dataSource = EmployeeMedicals;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
            }
        }

        private DataTable EmployeeMedicals
        {
            get
            {
                var isEmptyFilter = txtRegistrationDateFrom.IsEmpty && txtRegistrationDateTo.IsEmpty && string.IsNullOrEmpty(cboServiceUnitID.SelectedValue)
                && string.IsNullOrEmpty(cboParamedicID.SelectedValue) && string.IsNullOrEmpty(cboDiagnoseID.SelectedValue) && string.IsNullOrEmpty(txtEmployeeNumber.Text)
                && string.IsNullOrEmpty(txtPatientName.Text) && string.IsNullOrEmpty(cboSRVisitReason.SelectedValue) && string.IsNullOrEmpty(cboGuarantorID.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "Employee Medical List")) return null;

                var reg = new RegistrationQuery("a");
                var pat = new PatientQuery("b");
                var unit = new ServiceUnitQuery("c");
                var param = new ParamedicQuery("d");
                var ed = new EpisodeDiagnoseQuery("e");
                var diag = new DiagnoseQuery("f");
                var pi = new PersonalInfoQuery("g");
                //var org = new OrganizationUnitQuery("h");
                //var su = new OrganizationUnitQuery("i");
                var std = new AppStandardReferenceItemQuery("j");
                var guar = new GuarantorQuery("k");

                reg.Select(
                    pi.EmployeeNumber.Coalesce("''"),
                    pi.EmployeeName,
                    pat.MedicalNo,
                    pat.PatientName,
                    pat.Sex,
                    reg.AgeInYear,
                    //org.OrganizationUnitName.Coalesce("''"),
                    //su.OrganizationUnitName.As("SubOrganizationUnit"),
                    @"<ISNULL((SELECT TOP 1 ou.OrganizationUnitName FROM EmployeeOrganization AS eo
                INNER JOIN OrganizationUnit AS ou ON ou.OrganizationUnitID = eo.ServiceUnitID 
                WHERE eo.PersonID = g.PersonID AND eo.ValidFrom <= GETDATE()
                ORDER BY eo.ValidFrom DESC), '') AS SubOrganizationUnit>",
                    reg.RegistrationNo,
                    reg.RegistrationDate,
                    reg.RegistrationTime,
                    unit.ServiceUnitName,
                    param.ParamedicName,
                    (diag.DiagnoseID.Coalesce("''") + " - " + diag.DiagnoseName.Coalesce("''")).As("DiagnoseName"),
                    std.ItemName.As("VisitReasonName"),
                    guar.GuarantorName
                );
                reg.InnerJoin(pat).On(pat.PatientID == reg.PatientID);
                reg.InnerJoin(unit).On(reg.ServiceUnitID == unit.ServiceUnitID);
                reg.InnerJoin(param).On(reg.ParamedicID == param.ParamedicID);
                reg.LeftJoin(ed).On(
                    reg.RegistrationNo == ed.RegistrationNo &&
                    ed.SRDiagnoseType == AppSession.Parameter.DiagnoseTypeMain &&
                    ed.IsVoid == false
                );
                reg.LeftJoin(diag).On(ed.DiagnoseID == diag.DiagnoseID);
                reg.InnerJoin(pi).On(pi.PatientID == reg.PatientID);
                //reg.LeftJoin(org).On(org.OrganizationUnitID == pi.OrganizationUnitID);
                //reg.LeftJoin(su).On(su.OrganizationUnitID == pi.ServiceUnitID.ToInt());
                reg.LeftJoin(std).On(
                    reg.SRVisitReason == std.ItemID &&
                    std.StandardReferenceID == AppEnum.StandardReference.VisitReason
                    );
                reg.InnerJoin(guar).On(guar.GuarantorID == reg.GuarantorID);
                reg.Where(
                    reg.IsConsul == false,
                    reg.IsFromDispensary == false,
                    reg.IsVoid == false,
                    reg.IsDirectPrescriptionReturn == false,
                    reg.IsNonPatient == false
                //,
                //pat.SREmployeeRelationship == AppSession.Parameter.EmployeeRelationshipSelf
                );
                if (!txtRegistrationDateFrom.IsEmpty && !txtRegistrationDateTo.IsEmpty)
                    reg.Where(reg.RegistrationDate >= txtRegistrationDateFrom.SelectedDate, reg.RegistrationDate < txtRegistrationDateTo.SelectedDate.Value.AddDays(1));
                if (!string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
                    reg.Where(reg.ServiceUnitID == cboServiceUnitID.SelectedValue);
                if (!string.IsNullOrEmpty(cboParamedicID.SelectedValue))
                    reg.Where(reg.ParamedicID == cboParamedicID.SelectedValue);
                if (!string.IsNullOrEmpty(cboDiagnoseID.SelectedValue))
                    reg.Where(ed.DiagnoseID == cboDiagnoseID.SelectedValue);
                if (!string.IsNullOrEmpty(txtEmployeeNumber.Text))
                    reg.Where(reg.Or(pat.MedicalNo == txtEmployeeNumber.Text, pi.EmployeeNumber == txtEmployeeNumber.Text));
                if (!string.IsNullOrEmpty(txtPatientName.Text))
                {
                    var searchPatient = "%" + txtPatientName.Text + "%";
                    reg.Where(
                        string.Format("<(LTRIM(RTRIM(LTRIM(b.FirstName + ' ' + b.MiddleName)) + ' ' + b.LastName) LIKE '{0}' OR LTRIM(RTRIM(LTRIM(g.FirstName + ' ' + g.MiddleName)) + ' ' + g.LastName) LIKE '{0}')>", searchPatient)
                    );
                }
                if (!string.IsNullOrEmpty(cboSRVisitReason.SelectedValue))
                    reg.Where(reg.SRVisitReason == cboSRVisitReason.SelectedValue);
                if (!string.IsNullOrEmpty(cboGuarantorID.SelectedValue))
                    reg.Where(reg.GuarantorID == cboGuarantorID.SelectedValue);

                reg.OrderBy(reg.RegistrationDate.Ascending, reg.RegistrationTime.Ascending, reg.ServiceUnitID.Ascending);
                reg.es.Top = AppSession.Parameter.MaxResultRecord;

                DataTable dtb = reg.LoadDataTable();
                return dtb;
                //grdList.DataSource = reg.LoadDataTable();
            }
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            grdList.Rebind();
            grdList2.Rebind();
        }

        protected void btnFilter2_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            grdList2.Rebind();
        }

        protected void grdList2_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grdList2.DataSource = new String[] { };
                return;
            }

            var grd = (RadGrid)source;
            var dataSource = EmployeeMedicalDiagnoses;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
            }
        }
        private DataTable EmployeeMedicalDiagnoses
        {
            get
            {
                var isEmptyFilter = txtRegistrationDateFrom.IsEmpty && txtRegistrationDateTo.IsEmpty && string.IsNullOrEmpty(cboServiceUnitID.SelectedValue)
                && string.IsNullOrEmpty(cboParamedicID.SelectedValue) && string.IsNullOrEmpty(cboDiagnoseID.SelectedValue) && string.IsNullOrEmpty(txtEmployeeNumber.Text)
                && string.IsNullOrEmpty(txtPatientName.Text) && string.IsNullOrEmpty(cboSRVisitReason.SelectedValue) && string.IsNullOrEmpty(cboGuarantorID.SelectedValue)
                    && string.IsNullOrEmpty(cboSRRegistrationType.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "Employee Medical List")) return null;

                var reg = new RegistrationQuery("a");
                var pat = new PatientQuery("b");
                var unit = new ServiceUnitQuery("c");
                var param = new ParamedicQuery("d");
                var ed = new EpisodeDiagnoseQuery("e");
                var diag = new DiagnoseQuery("f");
                var pi = new PersonalInfoQuery("g");

                reg.es.Top = 10;
                reg.Select(
                    (diag.DiagnoseID.Coalesce("''") + " - " + diag.DiagnoseName.Coalesce("''")).As("DiagnoseName"),
                    diag.DiagnoseID.Count().As("Count")
                );

                reg.InnerJoin(pat).On(reg.PatientID == pat.PatientID);
                reg.InnerJoin(unit).On(reg.ServiceUnitID == unit.ServiceUnitID);
                reg.InnerJoin(param).On(reg.ParamedicID == param.ParamedicID);
                reg.InnerJoin(ed).On(
                    reg.RegistrationNo == ed.RegistrationNo &&
                    ed.SRDiagnoseType == AppSession.Parameter.DiagnoseTypeMain &&
                    ed.IsVoid == false
                );
                reg.InnerJoin(diag).On(ed.DiagnoseID == diag.DiagnoseID);
                reg.InnerJoin(pi).On(pat.PatientID == pi.PatientID);
                reg.Where(
                    reg.IsFromDispensary == false,
                    reg.IsVoid == false,
                    reg.IsDirectPrescriptionReturn == false,
                    reg.IsNonPatient == false
                //,
                //pat.SREmployeeRelationship == AppSession.Parameter.EmployeeRelationshipSelf
                );
                if (!txtRegistrationDateFrom.IsEmpty && !txtRegistrationDateTo.IsEmpty)
                    reg.Where(reg.RegistrationDate.Between(txtRegistrationDateFrom.SelectedDate.Value.Date, txtRegistrationDateTo.SelectedDate.Value.Date));
                if (!string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
                    reg.Where(reg.ServiceUnitID == cboServiceUnitID.SelectedValue);
                if (!string.IsNullOrEmpty(cboSRRegistrationType.SelectedValue))
                    reg.Where(reg.SRRegistrationType == cboSRRegistrationType.SelectedValue);

                reg.GroupBy(diag.DiagnoseID, diag.DiagnoseName);
                reg.OrderBy(diag.DiagnoseID.Count().Descending);
                reg.es.Top = AppSession.Parameter.MaxResultRecord;

                DataTable dtb = reg.LoadDataTable();
                return dtb;
            }
            //grdList2.DataSource = reg.LoadDataTable();
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            grdList.MasterTableView.ExportToExcel();
        }

        protected void btnPrint2_Click(object sender, EventArgs e)
        {
            grdList2.MasterTableView.ExportToExcel();
        }

        protected void cboDiagnoseID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new DiagnoseQuery("a");

            query.es.Top = 10;
            query.Where(
                query.Or(
                    query.DiagnoseID.Like(searchTextContain),
                    query.DiagnoseName.Like(searchTextContain)
                    ),
                query.IsActive == true
                );
            query.OrderBy(query.DiagnoseID.Ascending);

            cboDiagnoseID.DataSource = query.LoadDataTable();
            cboDiagnoseID.DataBind();
        }

        protected void cboDiagnoseID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["DiagnoseName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["DiagnoseID"].ToString();
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            var regNo = e.DetailTableView.ParentItem.GetDataKeyValue("RegistrationNo").ToString();

            var tc = new TransChargesQuery("a");
            var tci = new TransChargesItemQuery("b");
            var item = new ItemQuery("c");
            var ipm = new ItemProductMedicQuery("d");

            tc.Select(tc.RegistrationNo, item.ItemName, tci.ChargeQuantity.Sum(), ipm.SRItemUnit);
            tc.InnerJoin(tci).On(tci.TransactionNo == tc.TransactionNo && tci.IsApprove == true);
            tc.InnerJoin(item).On(item.ItemID == tci.ItemID);
            tc.InnerJoin(ipm).On(ipm.ItemID == item.ItemID);
            tc.Where(tc.RegistrationNo == regNo, tc.IsApproved == true);
            tc.GroupBy(tc.RegistrationNo, item.ItemName, ipm.SRItemUnit);

            var table = tc.LoadDataTable();

            var tp = new TransPrescriptionQuery("a");
            var tpi = new TransPrescriptionItemQuery("b");
            item = new ItemQuery("c");
            ipm = new ItemProductMedicQuery("d");

            tp.Select(tp.RegistrationNo, item.ItemName, tpi.ResultQty.Sum().As("ChargeQuantity"), ipm.SRItemUnit);
            tp.InnerJoin(tpi).On(tpi.PrescriptionNo == tp.PrescriptionNo && tpi.IsApprove == true);
            tp.InnerJoin(item).On(item.ItemID == tpi.ItemID);
            tp.InnerJoin(ipm).On(ipm.ItemID == item.ItemID);
            tp.Where(tp.RegistrationNo == regNo, tp.IsApproval == true);
            tp.GroupBy(tp.RegistrationNo, item.ItemName, ipm.SRItemUnit);

            table.Merge(tp.LoadDataTable());

            e.DetailTableView.DataSource = table;
        }
    }
}