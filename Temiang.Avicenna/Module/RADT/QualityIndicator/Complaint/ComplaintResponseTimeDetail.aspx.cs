using System;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Temiang.Dal.DynamicQuery;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.RADT.QualityIndicator.Complaint
{
    public partial class ComplaintResponseTimeDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "ComplaintResponseTimeSearch.aspx";
            UrlPageList = "ComplaintResponseTimeList.aspx";

            ProgramID = AppConstant.Program.ComplaintResponseTime;

            this.WindowSearch.Height = 400;

            if (!IsPostBack)
            {
            }
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            //ToolBarMenuSearch.Visible = false;
            //ToolBarMenuAdd.Visible = false;
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(txtComplaintDate, txtComplaintDate);
            ajax.AddAjaxSetting(txtComplaintDate, txtComplaintNo);
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new ComplaintResponseTime());

            txtComplaintDate.SelectedDate = (new DateTime()).NowAtSqlServer();
            txtComplaintNo.Text = PopulateNewNo();

            ViewState["IsApproved"] = false;
            ViewState["IsVoid"] = false;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {

            var entity = new ComplaintResponseTime();
            entity.AddNew();

            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new ComplaintResponseTime();
            if (entity.LoadByPrimaryKey(txtComplaintNo.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
            }
        }

        protected override void OnMenuMoveNextClick(ValidateArgs args)
        {
            MoveRecord(true);
        }

        protected override void OnMenuMovePrevClick(ValidateArgs args)
        {
            MoveRecord(false);
        }

        protected override void OnMenuAuditLogClick(AuditLogFilter auditLogFilter)
        {
            auditLogFilter.PrimaryKeyData = string.Format("ComplaintNo='{0}'", txtComplaintNo.Text.Trim());
            auditLogFilter.TableName = "ComplaintResponseTime";
        }

        #endregion

        #region ToolBar Menu Support
        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            var entity = new ComplaintResponseTime();
            if (entity.LoadByPrimaryKey(txtComplaintNo.Text))
            {
                if (!IsApprovedOrVoid(entity, args))
                    return;
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
            }
        }

        private bool IsApprovedOrVoid(ComplaintResponseTime entity, ValidateArgs args)
        {
            if (entity.IsApproved ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasApproved;
                args.IsCancel = true;
                return false;
            }

            if (entity.IsVoid ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return false;
            }

            return true;
        }

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            printJobParameters.AddNew("p_ComplaintNo", txtComplaintNo.Text);
            printJobParameters.AddNew("p_UserID", AppSession.UserLogin.UserID);
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return ViewState["IsApproved"] == null ? false : !(bool)ViewState["IsApproved"];
        }

        public override bool OnGetStatusMenuVoid()
        {
            return !(bool)ViewState["IsVoid"];
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new ComplaintResponseTime();
            if (parameters.Length > 0)
            {
                var tno = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(tno);
            }
            else
                entity.LoadByPrimaryKey(txtComplaintNo.Text);

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var c = (ComplaintResponseTime)entity;

            txtComplaintNo.Text = c.ComplaintNo;
            txtComplaintDate.SelectedDate = c.ComplaintDate;

            if (!string.IsNullOrEmpty(c.PatientID))
            {
                var patient = new PatientQuery();
                patient.Where(patient.PatientID == c.PatientID);
                patient.Select(patient.PatientID, patient.MedicalNo, patient.PatientName, patient.DateOfBirth, patient.Address);
                var dtb = patient.LoadDataTable();
                cboPatientID.DataSource = dtb;
                cboPatientID.DataBind();
                cboPatientID.SelectedValue = c.PatientID.ToString();
            }
            else
            {
                cboPatientID.Items.Clear();
                cboPatientID.SelectedValue = string.Empty;
                cboPatientID.Text = string.Empty;
            }

            txtCustomerName.Text = c.CustomerName;

            var p = new Patient();
            if (p.LoadByPrimaryKey(cboPatientID.SelectedValue))
            {
                txtMedicalNo.Text = p.MedicalNo;
                var std = new AppStandardReferenceItem();
                txtSalutation.Text = std.LoadByPrimaryKey("Salutation", p.SRSalutation) ? std.ItemName : string.Empty;
                txtPatientName.Text = p.PatientName;
                txtGender.Text = p.Sex;
                txtPlaceDOB.Text = string.Format("{0}, {1}", p.CityOfBirth, Convert.ToDateTime(p.DateOfBirth).ToString("dd-MMM-yyyy"));
                txtAgeDay.Value = Helper.GetAgeInDay(p.DateOfBirth.Value);
                txtAgeMonth.Value = Helper.GetAgeInMonth(p.DateOfBirth.Value);
                txtAgeYear.Value = Helper.GetAgeInYear(p.DateOfBirth.Value);
            }
            else
            {
                txtMedicalNo.Text = string.Empty;
                txtSalutation.Text = string.Empty;
                txtPatientName.Text = string.Empty;
                txtGender.Text = string.Empty;
                txtPlaceDOB.Text = string.Empty;
                txtAgeDay.Value = 0;
                txtAgeMonth.Value = 0;
                txtAgeYear.Value = 0;
            }
            txtCustomerAddress.Text = c.CustomerAddress;
            txtPhoneNo.Text = c.PhoneNo;
            if (!string.IsNullOrEmpty(c.ServiceUnitID))
            {
                var unit = new ServiceUnitQuery();
                unit.Where(unit.ServiceUnitID == c.ServiceUnitID);
                cboServiceUnitID.DataSource = unit.LoadDataTable();
                cboServiceUnitID.DataBind();
                cboServiceUnitID.SelectedValue = c.ServiceUnitID;
            }
            else
            {
                cboServiceUnitID.Items.Clear();
                cboServiceUnitID.SelectedValue = string.Empty;
                cboServiceUnitID.Text = string.Empty;
            }
            
            txtComplaintDescription.Text = c.ComplaintDescription;
            if (!string.IsNullOrEmpty(c.SRComplaintRiskGrading))
                rblSRComplaintRiskGrading.SelectedValue = c.SRComplaintRiskGrading;
            else
                rblSRComplaintRiskGrading.ClearSelection();
            if (c.ReportReceivedMarketingDate.HasValue)
                txtReportReceivedMarketingDate.SelectedDate = c.ReportReceivedMarketingDate;
            else
                txtReportReceivedMarketingDate.Clear();
            txtReportReceivedMarketingBy.Text = c.ReportReceivedMarketingBy;
            if (c.ReportReceivedUnitDate.HasValue)
                txtReportReceivedUnitDate.SelectedDate = c.ReportReceivedUnitDate;
            else
                txtReportReceivedUnitDate.Clear();
            txtReportReceivedUnitBy.Text = c.ReportReceivedUnitBy;
            if (c.CorrectiveActionDate.HasValue)
                txtCorrectiveActionDate.SelectedDate = c.CorrectiveActionDate;
            else
                txtCorrectiveActionDate.Clear();
            txtCorrectiveActionBy.Text = c.CorrectiveActionBy;
            txtCorrectiveAction.Text = c.CorrectiveAction;
            txtPreventiveAction.Text = c.PreventiveAction;

            ViewState["IsApproved"] = c.IsApproved ?? false;
            ViewState["IsVoid"] = c.IsVoid ?? false;
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            var entity = new ComplaintResponseTime();
            if (!entity.LoadByPrimaryKey(txtComplaintNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }

            if (entity.IsApproved ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasApproved;
                args.IsCancel = true;
                return;
            }

            if (entity.IsVoid ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return;
            }

            SetApproval(entity, true, args);
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            var entity = new ComplaintResponseTime();
            if (!entity.LoadByPrimaryKey(txtComplaintNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }

            if (entity.IsVoid ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return;
            }

            if (entity.IsApproved == false)
            {
                args.MessageText = AppConstant.Message.RecordHasNotApproved;
                args.IsCancel = true;
                return;
            }

            SetApproval(entity, false, args);
        }

        private void SetApproval(ComplaintResponseTime entity, bool isApproval, ValidateArgs args)
        {
            using (var trans = new esTransactionScope())
            {
                entity.IsApproved = isApproval;

                if (isApproval)
                {
                    entity.ApprovedByUserID = AppSession.UserLogin.UserID;
                    entity.ApprovedDateTime = (new DateTime()).NowAtSqlServer();
                }
                else
                {
                    entity.ApprovedByUserID = null;
                    entity.ApprovedDateTime = null;
                }

                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                entity.Save();

                trans.Complete();
            }
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            var entity = new ComplaintResponseTime();
            if (!entity.LoadByPrimaryKey(txtComplaintNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }
            if (entity.IsVoid ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return;
            }

            SetVoid(entity, true);
        }

        protected override void OnMenuUnVoidClick(ValidateArgs args)
        {
            var entity = new ComplaintResponseTime();
            if (!entity.LoadByPrimaryKey(txtComplaintNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }

            SetVoid(entity, false);
        }

        private void SetVoid(ComplaintResponseTime entity, bool isVoid)
        {
            //header
            entity.IsVoid = isVoid;
            if (isVoid)
            {
                entity.VoidByUserID = AppSession.UserLogin.UserID;
                entity.VoidDateTime = (new DateTime()).NowAtSqlServer();
            }
            else
            {
                entity.VoidByUserID = null;
                entity.VoidDateTime = null;
            }
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            entity.Save();
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(ComplaintResponseTime entity)
        {
            if (DataModeCurrent == AppEnum.DataMode.New)
            {
                txtComplaintNo.Text = PopulateNewNo();
                // save autonumber immediately to decrease time gap between create and save
                _autoNumber.Save();
            }
            entity.ComplaintNo = txtComplaintNo.Text;
            entity.ComplaintDate = txtComplaintDate.SelectedDate;
            entity.CustomerName = txtCustomerName.Text;
            entity.PatientID = cboPatientID.SelectedValue;
            entity.CustomerAddress = txtCustomerAddress.Text;
            entity.ServiceUnitID = cboServiceUnitID.SelectedValue;
            entity.PhoneNo = txtPhoneNo.Text;
            entity.ComplaintDescription = txtComplaintDescription.Text;
            entity.SRComplaintRiskGrading = rblSRComplaintRiskGrading.SelectedValue;
            entity.ReportReceivedMarketingDate = txtReportReceivedMarketingDate.SelectedDate;
            entity.ReportReceivedMarketingBy = txtReportReceivedMarketingBy.Text;
            entity.ReportReceivedUnitDate = txtReportReceivedUnitDate.SelectedDate;
            entity.ReportReceivedUnitBy = txtReportReceivedUnitBy.Text;
            entity.CorrectiveActionDate = txtCorrectiveActionDate.SelectedDate;
            entity.CorrectiveActionBy = txtCorrectiveActionBy.Text;
            entity.CorrectiveAction = txtCorrectiveAction.Text;
            entity.PreventiveAction = txtPreventiveAction.Text;
            entity.IsApproved = false;
            entity.IsVoid = false;
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
        }

        private void SaveEntity(ComplaintResponseTime entity)
        {
            using (var trans = new esTransactionScope())
            {
                entity.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new ComplaintResponseTimeQuery("a");

            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.ComplaintNo > txtComplaintNo.Text);
                que.OrderBy(que.ComplaintNo.Ascending);
            }
            else
            {
                que.Where(que.ComplaintNo < txtComplaintNo.Text);
                que.OrderBy(que.ComplaintNo.Descending);
            }

            var entity = new ComplaintResponseTime();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        #endregion

        protected void txtComplaintDate_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            txtComplaintNo.Text = PopulateNewNo();
        }

        private string PopulateNewNo()
        {
            _autoNumber = Helper.GetNewAutoNumber(txtComplaintDate.SelectedDate.Value, AppEnum.AutoNumber.ComplaintNo);

            return _autoNumber.LastCompleteNumber;
        }

        protected void cboPatientID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Value))
            {
                var p = new Patient();
                if (p.LoadByPrimaryKey(e.Value))
                {
                    txtMedicalNo.Text = p.MedicalNo;
                    var std = new AppStandardReferenceItem();
                    txtSalutation.Text = std.LoadByPrimaryKey("Salutation", p.SRSalutation) ? std.ItemName : string.Empty;
                    txtPatientName.Text = p.PatientName;
                    txtGender.Text = p.Sex;
                    txtPlaceDOB.Text = string.Format("{0}, {1}", p.CityOfBirth, Convert.ToDateTime(p.DateOfBirth).ToString("dd-MMM-yyyy"));
                    txtAgeDay.Value = Helper.GetAgeInDay(p.DateOfBirth.Value);
                    txtAgeMonth.Value = Helper.GetAgeInMonth(p.DateOfBirth.Value);
                    txtAgeYear.Value = Helper.GetAgeInYear(p.DateOfBirth.Value);
                }
                else
                {
                    txtMedicalNo.Text = string.Empty;
                    txtSalutation.Text = string.Empty;
                    txtPatientName.Text = string.Empty;
                    txtGender.Text = string.Empty;
                    txtPlaceDOB.Text = string.Empty;
                    txtAgeDay.Value = 0;
                    txtAgeMonth.Value = 0;
                    txtAgeYear.Value = 0;
                }
            }
            else
            {
                txtMedicalNo.Text = string.Empty;
                txtSalutation.Text = string.Empty;
                txtPatientName.Text = string.Empty;
                txtGender.Text = string.Empty;
                txtPlaceDOB.Text = string.Empty;
                txtAgeDay.Value = 0;
                txtAgeMonth.Value = 0;
                txtAgeYear.Value = 0;
            }
        }

        protected void cboPatientID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var dtbPatient = (new PatientCollection()).PatientRegisterAble(e.Text, string.Empty, string.Empty, string.Empty, 10);
            cboPatientID.DataSource = dtbPatient;
            cboPatientID.DataBind();
        }

        protected void cboPatientID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["PatientID"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PatientID"].ToString();
        }

        protected void cboServiceUnitID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var searchLike = string.Format("%{0}%", e.Text);

            var query = new ServiceUnitQuery();
            query.Where(query.ServiceUnitName.Like(searchLike), query.IsActive == true);
            query.Select(query.ServiceUnitID, query.ServiceUnitName);
            query.OrderBy(query.ServiceUnitName.Ascending);
            query.es.Top = 20;
            cboServiceUnitID.DataSource = query.LoadDataTable();
            cboServiceUnitID.DataBind();
        }

        protected void cboServiceUnitID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ServiceUnitName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ServiceUnitID"].ToString();
        }
    }
}