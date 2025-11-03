using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using System.Web.UI;
using Temiang.Dal.DynamicQuery;
using System.Collections.Generic;
using System.Web;

namespace Temiang.Avicenna.Module.HR.K3RS
{
    public partial class HealthTestResultList : BasePage
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

            ProgramID = AppConstant.Program.K3RS_EmployeeHealthTestResult;

            if (!IsPostBack)
            {
                var first = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).Date;
                var last = first.AddMonths(1).AddDays(-1).Date;

                txtRegistrationDateFrom.SelectedDate = first;
                txtRegistrationDateTo.SelectedDate = last;

                var grr = new GuarantorCollection();
                grr.Query.Where(grr.Query.IsActive == true);
                grr.Query.Load();
                cboGuarantorID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (var entity in grr)
                {
                    cboGuarantorID.Items.Add(new RadComboBoxItem(entity.GuarantorName, entity.GuarantorID));
                }
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (IsPostBack) return;

            RestoreValueFromCookie();
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
        protected void grdOutstanding_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grdOutstanding.DataSource = new String[] { };
                return;
            }

            var grd = (RadGrid)source;
            var dataSource = RegistrationOutstandings;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
            }          
        }

        private DataTable RegistrationOutstandings
        {
            get
            {
                var isEmptyFilter = txtRegistrationDateFrom.IsEmpty && txtRegistrationDateTo.IsEmpty && string.IsNullOrEmpty(txtRegistrationNo.Text) 
                    && string.IsNullOrEmpty(txtEmployeeName.Text) && string.IsNullOrEmpty(txtEmployeeNumber.Text) && string.IsNullOrEmpty(cboGuarantorID.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "Health Result List")) return null;

                var query = new RegistrationQuery("a");
                var qpat = new PatientQuery("b");
                var qemp = new VwEmployeeTableQuery("c");
                var qpos = new PositionQuery("d");
                var qsu = new ServiceUnitQuery("e");
                var qresult = new EmployeeHealthTestResultQuery("f");
                var guar = new GuarantorQuery("g");
                var org = new OrganizationUnitQuery("i");

                query.InnerJoin(qpat).On(qpat.PatientID == query.PatientID);
                query.InnerJoin(qemp).On(qemp.PersonID == qpat.PersonID);
                query.LeftJoin(qpos).On(qpos.PositionID == qemp.PositionID);
                query.InnerJoin(qsu).On(qsu.ServiceUnitID == query.ServiceUnitID);
                query.LeftJoin(qresult).On(qresult.RegistrationNo == query.RegistrationNo);
                query.InnerJoin(guar).On(guar.GuarantorID == query.GuarantorID);
                query.LeftJoin(org).On(org.OrganizationUnitID == qemp.ServiceUnitID.ToInt());

                query.OrderBy
                    (
                        query.RegistrationNo.Ascending
                    );

                query.Select(
                    query.RegistrationNo,
                    query.RegistrationDate,
                    qpat.MedicalNo,
                    qpat.PersonID,
                    qemp.EmployeeNumber,
                    qemp.EmployeeName,
                    qpos.PositionName,
                    qsu.ServiceUnitName,
                    guar.GuarantorName,
                     org.OrganizationUnitName.As("EmployeeServiceUnitName")
                    );

                if (rblHealthTest.SelectedIndex == 0)
                {
                    query.Select(@"<'HealthTestResultDetail.aspx?md=new&id=&rno=' + a.RegistrationNo + '&type=mcu' as NewUrl>", @"<'Medical Check Up' AS HealthTest>");
                    query.Where(query.ServiceUnitID == AppSession.Parameter.ServiceUnitMcuId);
                }
                else
                {
                    query.Select(@"<'HealthTestResultDetail.aspx?md=new&id=&rno=' + a.RegistrationNo + '&type=swab' as NewUrl>", @"<'Rectal Swab' AS HealthTest>");
                    query.Where(query.ServiceUnitID == AppSession.Parameter.ServiceUnitLaboratoryID);
                }

                query.Where(query.IsVoid == false, 
                    qpat.SREmployeeRelationship == AppSession.Parameter.EmployeeRelationshipSelf, 
                    qresult.ResultDate.IsNull(), 
                    guar.SRGuarantorType != AppSession.Parameter.GuarantorTypeSelf);

                if (!txtRegistrationDateFrom.IsEmpty && !txtRegistrationDateTo.IsEmpty)
                    query.Where(query.RegistrationDate >= txtRegistrationDateFrom.SelectedDate, query.RegistrationDate < txtRegistrationDateTo.SelectedDate.Value.AddDays(1));
                if (txtRegistrationNo.Text != string.Empty)
                {
                    string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                    query.Where(
                        query.Or(
                            query.RegistrationNo == searchReg,
                            qpat.MedicalNo == searchReg,
                            qpat.OldMedicalNo == searchReg,
                            string.Format("< OR REPLACE(b.MedicalNo, '-', '') LIKE '%{0}%'>", searchReg),
                            string.Format("< OR REPLACE(b.OldMedicalNo, '-', '') LIKE '%{0}%'>", searchReg)
                            )
                        );
                }
                if (!string.IsNullOrEmpty(txtEmployeeName.Text))
                {
                    string searchEmpName = string.Format("%{0}%", txtEmployeeName.Text);
                    query.Where(qemp.EmployeeName.Like(searchEmpName));
                }
                //if (txtEmployeeName.Text != string.Empty)
                //{
                //    string searchEmpName = "%" + txtEmployeeName.Text + "%";
                //    query.Where(string.Format("<c.EmployeeName LIKE '{0}' OR >", searchEmpName));
                //}
                if (txtEmployeeNumber.Text != string.Empty)
                {
                    string searchEmpNo = "%" + txtEmployeeNumber.Text + "%";
                    query.Where(string.Format("<c.EmployeeNumber LIKE '{0}'>", searchEmpNo));
                }
                if (!string.IsNullOrEmpty(cboGuarantorID.SelectedValue))
                    query.Where(query.GuarantorID == cboGuarantorID.SelectedValue);

                return query.LoadDataTable();
            }
        }

        protected void grdResult_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grdResult.DataSource = new String[] { };
                return;
            }

            var grd = (RadGrid)source;
            var dataSource = EmployeeHealthTestResults;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
            }            
        }

        private DataTable EmployeeHealthTestResults
        {
            get
            {
                var isEmptyFilter = txtRegistrationDateFrom.IsEmpty && txtRegistrationDateTo.IsEmpty && string.IsNullOrEmpty(txtRegistrationNo.Text)
                    && string.IsNullOrEmpty(txtEmployeeName.Text) && string.IsNullOrEmpty(txtEmployeeNumber.Text) && string.IsNullOrEmpty(cboGuarantorID.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "Health Result List")) return null;

                var query = new RegistrationQuery("a");
                var qpat = new PatientQuery("b");
                var qemp = new VwEmployeeTableQuery("c");
                var qpos = new PositionQuery("d");
                var qsu = new ServiceUnitQuery("e");
                var qresult = new EmployeeHealthTestResultQuery("f");
                var qconcl = new AppStandardReferenceItemQuery("g");
                var guar = new GuarantorQuery("h");
                var org = new OrganizationUnitQuery("i");

                query.InnerJoin(qpat).On(qpat.PatientID == query.PatientID);
                query.InnerJoin(qemp).On(qemp.PersonID == qpat.PersonID);
                query.LeftJoin(qpos).On(qpos.PositionID == qemp.PositionID);
                query.InnerJoin(qsu).On(qsu.ServiceUnitID == query.ServiceUnitID);
                query.InnerJoin(qresult).On(qresult.RegistrationNo == query.RegistrationNo);
                query.InnerJoin(qconcl).On(qconcl.StandardReferenceID == AppEnum.StandardReference.HealthDegreeConclusion && qconcl.ItemID == qresult.SRHealthDegreeConclusion);
                query.InnerJoin(guar).On(guar.GuarantorID == query.GuarantorID);
                query.LeftJoin(org).On(org.OrganizationUnitID == qemp.ServiceUnitID.ToInt());

                query.OrderBy
                    (
                        query.RegistrationNo.Ascending
                    );

                query.Select(
                    query.RegistrationNo,
                    query.RegistrationDate,
                    qpat.MedicalNo,
                    qpat.PersonID,
                    qemp.EmployeeNumber,
                    qemp.EmployeeName,
                    qpos.PositionName,
                    qsu.ServiceUnitName,
                    guar.GuarantorName,
                    qresult.ResultDate,
                    qresult.Result,
                    qconcl.ItemName.As("HealthDegreeConclusionName"),
                    org.OrganizationUnitName.As("EmployeeServiceUnitName")
                    );

                if (rblHealthTest.SelectedIndex == 0)
                {
                    query.Select(@"<'HealthTestResultDetail.aspx?md=view&id=' + a.RegistrationNo + '&rno=&type=mcu' as NewUrl>", @"<'Medical Check Up' AS HealthTest>");
                    query.Where(query.ServiceUnitID == AppSession.Parameter.ServiceUnitMcuId);
                }
                else
                {
                    query.Select(@"<'HealthTestResultDetail.aspx?md=view&id=' + a.RegistrationNo + '&rno=&type=swab' as NewUrl>", @"<'Rectal Swab' AS HealthTest>");
                    query.Where(query.ServiceUnitID == AppSession.Parameter.ServiceUnitLaboratoryID);
                }

                if (!txtRegistrationDateFrom.IsEmpty && !txtRegistrationDateTo.IsEmpty)
                    query.Where(query.RegistrationDate >= txtRegistrationDateFrom.SelectedDate, query.RegistrationDate < txtRegistrationDateTo.SelectedDate.Value.AddDays(1));
                if (txtRegistrationNo.Text != string.Empty)
                {
                    string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                    query.Where(
                        query.Or(
                            query.RegistrationNo == searchReg,
                            qpat.MedicalNo == searchReg,
                            qpat.OldMedicalNo == searchReg,
                            string.Format("< OR REPLACE(b.MedicalNo, '-', '') LIKE '%{0}%'>", searchReg),
                            string.Format("< OR REPLACE(b.OldMedicalNo, '-', '') LIKE '%{0}%'>", searchReg)
                            )
                        );
                }
                if (!string.IsNullOrEmpty(txtEmployeeName.Text))
                {
                    string searchEmpName = string.Format("%{0}%", txtEmployeeName.Text);
                    query.Where(qemp.EmployeeName.Like(searchEmpName));
                }
                //if (txtEmployeeName.Text != string.Empty)
                //{
                //    string searchEmpName = "%" + txtEmployeeName.Text + "%";
                //    query.Where(string.Format("<c.EmployeeName LIKE '{0}' OR >", searchEmpName));
                //}
                if (txtEmployeeNumber.Text != string.Empty)
                {
                    string searchEmpNo = "%" + txtEmployeeNumber.Text + "%";
                    query.Where(string.Format("<c.EmployeeNumber LIKE '{0}'>", searchEmpNo));
                }
                if (!string.IsNullOrEmpty(cboGuarantorID.SelectedValue))
                    query.Where(query.GuarantorID == cboGuarantorID.SelectedValue);

                return query.LoadDataTable();
            }
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            SaveValueToCookie();

            grdOutstanding.Rebind();
            grdResult.Rebind();
        }

        protected void rblHealthTest_OnTextChanged(object sender, EventArgs e)
        {
            SaveValueToCookie();

            grdOutstanding.Rebind();
            grdResult.Rebind();
        }
    }
}