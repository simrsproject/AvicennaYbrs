using System;
using System.Linq;
using System.Data;
using System.Web.UI;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Payroll.PayrollInfo
{
    public partial class EmployeeSalaryInfoDetail : BasePageDetail
    {
        #region Page Event & Initialize
        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "EmployeeSalaryInfoSearch.aspx";
            UrlPageList = "EmployeeSalaryInfoList.aspx";

            ProgramID = AppConstant.Program.EmployeeSalaryInfo; //TODO: Isi ProgramID
            //StandardReference Initialize
            if (!IsPostBack)
            {
                hdnPageId.Value = PageID;

                txtPersonID.Value = Request.QueryString["id"].ToInt();
                StandardReference.InitializeIncludeSpace(cboSRPaymentFrequency, AppEnum.StandardReference.PaySequent);
                StandardReference.InitializeIncludeSpace(cboSRRemunerationType, AppEnum.StandardReference.RemunerationType);
                StandardReference.InitializeIncludeSpace(cboSRTaxStatus, AppEnum.StandardReference.TaxStatus, "TAX");
                StandardReference.InitializeIncludeSpace(cboSRBankHRD, AppEnum.StandardReference.BankHRD);

                trEmployeeTypePayroll.Visible = false;
                trIsSalaryManaged.Visible = true;

                trSalaryScale.Visible = AppSession.Parameter.HealthcareInitialAppsVersion == "YBRSGKP";

                tabStrip.Tabs[1].Visible = AppSession.Parameter.HealthcareInitialAppsVersion != "RSMM" && AppSession.Parameter.HealthcareInitialAppsVersion != "RSMP";
                rfvSRTaxStatus.Visible= AppSession.Parameter.HealthcareInitialAppsVersion == "RSMM" || AppSession.Parameter.HealthcareInitialAppsVersion == "RSMP";
                tabStrip.Tabs[2].Visible = AppSession.Parameter.HealthcareInitialAppsVersion == "YBRSGKP";
                tabStrip.Tabs[3].Visible = AppSession.Parameter.HealthcareInitialAppsVersion == "YBRSGKP";
                tabStrip.Tabs[5].Visible = !AppSession.Parameter.IsThrIncludeInWageProcess;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(grdEmployeeSalaryMatrix, grdEmployeeSalaryMatrix);
        }
        #endregion

        #region Toolbar Menu Event
        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new EmployeeSalaryInfo());

            txtPersonID.Value = 0;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            EmployeeSalaryInfo entity = new EmployeeSalaryInfo();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtPersonID.Value)))
            {
                entity.MarkAsDeleted();
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new EmployeeSalaryInfo();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            PersonalInfo ceklist = new PersonalInfo();
            if (ceklist.LoadByPrimaryKey(Convert.ToInt32(txtPersonID.Value)))
            {
                EmployeeSalaryInfo entity = new EmployeeSalaryInfo();
                if (entity.LoadByPrimaryKey(Convert.ToInt32(txtPersonID.Value)))
                {
                    SetEntityValue(entity);
                    SaveEntity(entity);
                }
                else
                {
                    entity = new EmployeeSalaryInfo();
                    entity.AddNew();
                    SetEntityValue(entity);
                    SaveEntity(entity);
                }
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
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
            //TODO: Betulkan PrimaryKeyData nya
            auditLogFilter.PrimaryKeyData = string.Format("PersonID='{0}'", txtPersonID.Value.ToString().Trim());
            auditLogFilter.TableName = "EmployeeSalaryInfo";
        }
        #endregion

        #region ToolBar Menu Support
        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //TODO: Set status entry control
            //txtPersonID.Value = Request.QueryString["id"];
            //txtPersonID.Enabled = (newVal == AppEnum.DataMode.New);

            RefreshCommandItemEmployeeSalaryMatrix(newVal);
            RefreshCommandItemEmployeeTaxStatus(newVal);
            RefreshCommandItemIncentivePosition(newVal);

            cboPayrollPeriodID.Enabled = true;
            cboThrPayrollPeriodID.Enabled = true;

            bool isVisible = (newVal != AppEnum.DataMode.Read);
            cboSRTaxStatus.Enabled = isVisible && (AppSession.Parameter.HealthcareInitialAppsVersion == "RSMM" || AppSession.Parameter.HealthcareInitialAppsVersion == "RSMP");
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            EmployeeSalaryInfo entity = new EmployeeSalaryInfo();
            if (parameters.Length > 0)
            {
                string personID = (string)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(Convert.ToInt32(personID));
            }
            else
            {
                entity.LoadByPrimaryKey(Convert.ToInt32(txtPersonID.Value));
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var employeeSalaryInfo = (EmployeeSalaryInfo)entity;

            txtPersonID.Value = Convert.ToInt32(employeeSalaryInfo.PersonID);

            var employeeInfo = new VwEmployeeTable();
            var employeeInfoQ = new VwEmployeeTableQuery();
            employeeInfoQ.es.Top = 1;
            employeeInfoQ.Where(employeeInfoQ.PersonID == Convert.ToInt32(txtPersonID.Value ?? 0));
            employeeInfo.Load(employeeInfoQ);
            if (employeeInfo != null)
            {
                txtEmployeeNumber.Text = employeeInfo.EmployeeNumber;
                txtEmployeeName.Text = employeeInfo.EmployeeName;
                Int32 organizationID = Convert.ToInt32(employeeInfo.OrganizationUnitID);
                Int32 positionID = Convert.ToInt32(employeeInfo.PositionID);
                Int32 positionGradeID = Convert.ToInt32(employeeInfo.PositionGradeID);
                var gradeYear = Convert.ToDouble(employeeInfo.GradeYear);
                string employeeStatus = employeeInfo.SREmployeeStatus;
                string employmentType = employeeInfo.SREmploymentType;

                var organizationUnit = new OrganizationUnit();
                organizationUnit.LoadByPrimaryKey(Convert.ToInt32(organizationID));
                txtOrganizationName.Text = organizationUnit.OrganizationUnitName;

                var position = new Position();
                position.LoadByPrimaryKey(Convert.ToInt32(positionID));
                txtPositionTitle.Text = position.PositionName;

                var grade = new PositionGrade();
                grade.LoadByPrimaryKey(Convert.ToInt32(positionGradeID));
                txtPositionGrade.Text = grade.PositionGradeName;
                txtGradeYear.Value = gradeYear;

                var ss = new SalaryScale();
                if (ss.LoadByPrimaryKey(employeeInfo.SalaryScaleID.ToInt()))
                    txtSalaryScaleCode.Text = ss.SalaryScaleName;
                else txtSalaryScaleCode.Text = string.Empty;

                var employType = new AppStandardReferenceItem();
                if (employType.LoadByPrimaryKey("EmployeeStatus", employeeStatus))
                    txtSREmployeeStatus.Text = employType.ItemName;

                employType = new AppStandardReferenceItem();
                if (employType.LoadByPrimaryKey("EmploymentType", employmentType))
                    txtSREmploymentType.Text = employType.ItemName;

                cboSRTaxStatus.SelectedValue = employeeInfo.SRTaxStatus;

                PopulateEmployeeImage(Convert.ToInt32(txtPersonID.Text), employeeInfo.SRGenderType);
            }
            else
                cboSRTaxStatus.SelectedValue = employeeSalaryInfo.SRTaxStatus;

            var iden = new PersonalIdentificationCollection();
            iden.Query.Where(iden.Query.PersonID == txtPersonID.Value.ToInt(), iden.Query.SRIdentificationType.In("5", "6")); //npwp, jamsostek (bpjs ketenagakerjaan)
            if (iden.Query.Load())
            {
                //var id = iden.Where(i => i.SRIdentificationType == "6").SingleOrDefault();
                var id = (iden.Where(i => i.SRIdentificationType == "6" && i.ValidFrom <= DateTime.Now).OrderByDescending(i => i.ValidFrom)).Take(1).SingleOrDefault();
                if (id != null) txtNPWP.Text = id.IdentificationValue;
                else txtNPWP.Text = string.Empty; //employeeSalaryInfo.Npwp;

                //id = iden.Where(i => i.SRIdentificationType == "5").SingleOrDefault();
                id = (iden.Where(i => i.SRIdentificationType == "5" && i.ValidFrom <= DateTime.Now).OrderByDescending(i => i.ValidFrom)).Take(1).SingleOrDefault();
                if (id != null) txtJamsostekNo.Text = id.IdentificationValue;
                else txtJamsostekNo.Text = string.Empty; //employeeSalaryInfo.JamsostekNo;
            }
            else
            {
                txtNPWP.Text = string.Empty; //employeeSalaryInfo.Npwp;
                txtJamsostekNo.Text = string.Empty; //employeeSalaryInfo.JamsostekNo;
            }

            txtNoOfDependent.Value = Convert.ToDouble(employeeSalaryInfo.NoOfDependent);
            cboSRPaymentFrequency.SelectedValue = employeeSalaryInfo.SRPaymentFrequency;
            cboSRRemunerationType.SelectedValue = employeeSalaryInfo.SRRemunerationType;

            cboSRBankHRD.SelectedValue = employeeSalaryInfo.BankID;
            txtBankAccountNo.Text = employeeSalaryInfo.BankAccountNo;
            txtBankAccountName.Text = employeeSalaryInfo.BankAccountName;
            if (trEmployeeTypePayroll.Visible) cboSREmployeeTypePayroll.SelectedValue = employeeInfo.EmployeeTypePayroll;
            if (trIsSalaryManaged.Visible) chkIsSalaryManaged.Checked = employeeSalaryInfo.IsSalaryManaged ?? false;
            //Display Data Detail
            PopulateEmployeeSalaryMatrixGrid();
            PopulateEmployeeTaxStatusGrid();
            PopulateEmployeeIncentivePositionGrid();
        }

        #endregion

        #region Private Method Standard
        private void SetEntityValue(EmployeeSalaryInfo entity)
        {
            entity.PersonID = Convert.ToInt32(txtPersonID.Value);
            entity.Npwp = txtNPWP.Text;
            entity.JamsostekNo = txtJamsostekNo.Text;
            entity.NoOfDependent = Convert.ToInt32(txtNoOfDependent.Value);
            entity.SRPaymentFrequency = cboSRPaymentFrequency.SelectedValue;
            entity.SRRemunerationType = cboSRRemunerationType.SelectedValue;
            entity.SRTaxStatus = cboSRTaxStatus.SelectedValue;
            entity.BankID = cboSRBankHRD.SelectedValue;
            entity.BankAccountNo = txtBankAccountNo.Text;
            entity.BankAccountName = txtBankAccountName.Text;
            if (trEmployeeTypePayroll.Visible) entity.SREmployeeType = cboSREmployeeTypePayroll.SelectedValue;
            if (trIsSalaryManaged.Visible) entity.IsSalaryManaged = chkIsSalaryManaged.Checked;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }

            //--> Employe Salary Matrix
            foreach (EmployeeSalaryMatrix matrix in EmployeeSalaryMatrixs)
            {
                //matrix.PersonID = Convert.ToInt32(txtPersonID.Text);
                //Last Update Status
                if (matrix.es.IsAdded || matrix.es.IsModified)
                {
                    matrix.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    matrix.LastUpdateDateTime = DateTime.Now;
                }
            }

            foreach (EmployeeTaxStatus taxstatus in EmployeeTaxStatuss)
            {
                if (taxstatus.es.IsAdded)
                {
                    taxstatus.CreatedByUserID = AppSession.UserLogin.UserID;
                    taxstatus.CreatedDateTime = DateTime.Now;
                }
                if (taxstatus.es.IsAdded || taxstatus.es.IsModified)
                {
                    taxstatus.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    taxstatus.LastUpdateDateTime = DateTime.Now;
                }
            }

            foreach (EmployeeIncentivePosition item in EmployeeIncentivePositions)
            {
                if (item.es.IsAdded || item.es.IsModified)
                {
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = DateTime.Now;
                }
            }
        }

        private void SaveEntity(EmployeeSalaryInfo entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                EmployeeSalaryMatrixs.Save();

                //if (EmployeeTaxStatuss.Count == 0)
                //{
                //    var ets = EmployeeTaxStatuss.AddNew();
                //    ets.PersonID = entity.PersonID;
                //    ets.SPTYear = DateTime.Now.Year;
                //    ets.SRTaxStatus = entity.SRTaxStatus;
                //    ets.CreatedByUserID = AppSession.UserLogin.UserID;
                //    ets.CreatedDateTime = DateTime.Now;
                //    ets.LastUpdateByUserID = AppSession.UserLogin.UserID;
                //    ets.LastUpdateDateTime = DateTime.Now;
                //    ets.IsClosed = false;
                //}

                EmployeeTaxStatuss.Save();
                EmployeeIncentivePositions.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            EmployeeSalaryInfoQuery que = new EmployeeSalaryInfoQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.PersonID > txtPersonID.Value.ToInt());
                que.OrderBy(que.PersonID.Ascending);
            }
            else
            {
                que.Where(que.PersonID < txtPersonID.Value.ToInt());
                que.OrderBy(que.PersonID.Descending);
            }
            EmployeeSalaryInfo entity = new EmployeeSalaryInfo();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }
        #endregion

        #region Method & Event TextChanged

        #endregion

        #region Record Detail Method Function EmployeeSalaryMatrix
        //TODO: Isi ulang field untuk relasi child ke parent nya di method SetEntity
        //TODO: Tambahkan perintah EmployeeSalaryMatrixs.Save(); di method SaveEntity
        //TODO: Panggil method RefreshCommandItemGrid dari OnDataModeChanged
        private void RefreshCommandItemEmployeeSalaryMatrix(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdEmployeeSalaryMatrix.Columns[0].Visible = isVisible;
            grdEmployeeSalaryMatrix.Columns[grdEmployeeSalaryMatrix.Columns.Count - 1].Visible = isVisible;

            grdEmployeeSalaryMatrix.MasterTableView.CommandItemDisplay = isVisible
                                                                              ? GridCommandItemDisplay.Top
                                                                              : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdEmployeeSalaryMatrix.Rebind();
        }

        private EmployeeSalaryMatrixCollection EmployeeSalaryMatrixs
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collEmployeeSalaryMatrix" + Request.UserHostName + hdnPageId.Value];
                    if (obj != null)
                    {
                        return ((EmployeeSalaryMatrixCollection)(obj));
                    }
                }

                EmployeeSalaryMatrixCollection coll = new EmployeeSalaryMatrixCollection();
                SalaryComponentQuery salary = new SalaryComponentQuery("b");
                EmployeeSalaryMatrixQuery query = new EmployeeSalaryMatrixQuery("a");

                query.Select
                    (
                      query.PersonID,
                      query.EmployeeSalaryMatrixID,
                      query.SalaryComponentID,
                      salary.SalaryComponentName.As("refTo_SalaryComponentName"),
                      query.Qty,
                      query.NominalAmount,
                      query.SRCurrencyCode,
                      query.LastUpdateDateTime,
                      query.LastUpdateByUserID
                    );
                query.InnerJoin(salary).On
                        (
                            query.SalaryComponentID == salary.SalaryComponentID
                        );

                query.Where(query.PersonID == (txtPersonID.Value ?? 0)); //TODO: Betulkan parameternya
                query.OrderBy(salary.SalaryComponentCode.Ascending); //TODO: Betulkan ordernya
                coll.Load(query);

                Session["collEmployeeSalaryMatrix" + Request.UserHostName + hdnPageId.Value] = coll;
                return coll;
            }
            set { Session["collEmployeeSalaryMatrix" + Request.UserHostName + hdnPageId.Value] = value; }
        }

        private void PopulateEmployeeSalaryMatrixGrid()
        {
            //Display Data Detail
            EmployeeSalaryMatrixs = null; //Reset Record Detail
            grdEmployeeSalaryMatrix.DataSource = EmployeeSalaryMatrixs; //Requery
            grdEmployeeSalaryMatrix.MasterTableView.IsItemInserted = false;
            grdEmployeeSalaryMatrix.MasterTableView.ClearEditItems();
            grdEmployeeSalaryMatrix.DataBind();
        }

        protected void grdEmployeeSalaryMatrix_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "Insert")
            {
                var cmdItem = grdEmployeeSalaryMatrix.MasterTableView.GetItems(GridItemType.CommandItem)[0];
                if (!string.IsNullOrEmpty((cmdItem.FindControl("cboSalaryTemplateID") as RadComboBox).SelectedValue))
                {
                    EmployeeSalaryMatrixs.MarkAllAsDeleted();

                    var template = new SalaryTemplateItemCollection();
                    template.Query.Where(template.Query.SalaryTemplateID == (cmdItem.FindControl("cboSalaryTemplateID") as RadComboBox).SelectedValue);
                    template.LoadAll();

                    foreach (var t in template)
                    {
                        var mtx = EmployeeSalaryMatrixs.AddNew();
                        mtx.PersonID = txtPersonID.Value.ToInt();
                        mtx.SalaryComponentID = t.SalaryComponentID;

                        var sc = new SalaryComponent();
                        sc.LoadByPrimaryKey(t.SalaryComponentID.ToInt());

                        mtx.SalaryComponentName = sc.SalaryComponentName;
                        mtx.Qty = 1;
                        mtx.NominalAmount = 0;
                        mtx.SRCurrencyCode = "IDR";
                        mtx.LastUpdateDateTime = DateTime.Now;
                        mtx.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    }
                }

                (source as RadGrid).Rebind();
            }
        }
        protected void grdEmployeeSalaryMatrix_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdEmployeeSalaryMatrix.DataSource = EmployeeSalaryMatrixs;
        }

        protected void grdEmployeeSalaryMatrix_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            Int64 employeeSalaryMatrixID = Convert.ToInt64(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][EmployeeSalaryMatrixMetadata.ColumnNames.EmployeeSalaryMatrixID]);
            EmployeeSalaryMatrix entity = FindEmployeeSalaryMatrix(employeeSalaryMatrixID);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdEmployeeSalaryMatrix_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            Int64 employeeSalaryMatrixID = Convert.ToInt64(item.OwnerTableView.DataKeyValues[item.ItemIndex][EmployeeSalaryMatrixMetadata.ColumnNames.EmployeeSalaryMatrixID]);
            EmployeeSalaryMatrix entity = FindEmployeeSalaryMatrix(employeeSalaryMatrixID);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdEmployeeSalaryMatrix_InsertCommand(object source, GridCommandEventArgs e)
        {
            EmployeeSalaryMatrix entity = EmployeeSalaryMatrixs.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdEmployeeSalaryMatrix.Rebind();
        }
        private EmployeeSalaryMatrix FindEmployeeSalaryMatrix(Int64 employeeSalaryMatrixID)
        {
            EmployeeSalaryMatrixCollection coll = EmployeeSalaryMatrixs;
            EmployeeSalaryMatrix retEntity = null;
            foreach (EmployeeSalaryMatrix rec in coll)
            {
                if (rec.EmployeeSalaryMatrixID.Equals(employeeSalaryMatrixID))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }
        private void SetEntityValue(EmployeeSalaryMatrix entity, GridCommandEventArgs e)
        {
            EmployeeSalaryMatrixDetail userControl = (EmployeeSalaryMatrixDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                //TODO: Lengkapi field yg belum terisi, untuk ref key ke header di set di SetEntity
                entity.EmployeeSalaryMatrixID = userControl.EmployeeSalaryMatrixID;
                entity.PersonID = Convert.ToInt32(txtPersonID.Value);
                entity.SalaryComponentID = userControl.SalaryComponentID;
                entity.SalaryComponentName = userControl.SalaryComponentName;
                entity.Qty = userControl.Qty;
                entity.NominalAmount = userControl.NominalAmount;
                entity.SRCurrencyCode = userControl.SRCurrencyCode;

            }
        }

        protected void cboSalaryTemplateID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var st = new SalaryTemplateQuery();
            st.es.Top = 20;
            st.Select(
                st.SalaryTemplateID,
                st.SalaryTemplateName
                );
            st.Where(st.SalaryTemplateName.Like(searchTextContain), st.IsActive == true);

            (o as RadComboBox).DataSource = st.LoadDataTable();
            (o as RadComboBox).DataBind();
        }

        protected void cboSalaryTemplateID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SalaryTemplateName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SalaryTemplateID"].ToString();
        }

        #endregion

        #region Record Detail Method Function EmployeeTaxStatus
        private void RefreshCommandItemEmployeeTaxStatus(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdEmployeeTaxStatus.Columns[0].Visible = isVisible;

            grdEmployeeTaxStatus.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Perbaharui tampilan dan data
            grdEmployeeTaxStatus.Rebind();
        }

        private EmployeeTaxStatusCollection EmployeeTaxStatuss
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collEmployeeTaxStatus" + Request.UserHostName + hdnPageId.Value];
                    if (obj != null)
                    {
                        return ((EmployeeTaxStatusCollection)(obj));
                    }
                }

                var coll = new EmployeeTaxStatusCollection();
                var query = new EmployeeTaxStatusQuery("a");
                var ts = new AppStandardReferenceItemQuery("b");

                query.Select
                    (
                      query,
                      ts.ItemName.As("refToAppStdRef_TaxStatusName"),
                      @"<ISNULL((SELECT TOP 1 cwt.IsClosed FROM PayrollPeriod AS pp
INNER JOIN ClosingWageTransaction AS cwt ON cwt.PayrollPeriodID = pp.PayrollPeriodID
WHERE pp.SPTYear = a.SPTYear AND cwt.IsClosed = 1), CAST(0 AS BIT)) AS 'refToClosingWageTransaction_IsClosed'>"
                    );
                query.InnerJoin(ts).On
                        (
                            ts.StandardReferenceID == AppEnum.StandardReference.TaxStatus.ToString() && ts.ItemID == query.SRTaxStatus
                        );

                query.Where(query.PersonID == (txtPersonID.Value ?? 0));
                query.OrderBy(query.SPTYear.Descending);
                coll.Load(query);

                Session["collEmployeeTaxStatus" + Request.UserHostName + hdnPageId.Value] = coll;
                return coll;
            }
            set { Session["collEmployeeTaxStatus" + Request.UserHostName + hdnPageId.Value] = value; }
        }

        private void PopulateEmployeeTaxStatusGrid()
        {
            //Display Data Detail
            EmployeeTaxStatuss = null; //Reset Record Detail
            grdEmployeeTaxStatus.DataSource = EmployeeTaxStatuss; //Requery
            grdEmployeeTaxStatus.MasterTableView.IsItemInserted = false;
            grdEmployeeTaxStatus.MasterTableView.ClearEditItems();
            grdEmployeeTaxStatus.DataBind();
        }

        protected void grdEmployeeTaxStatus_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdEmployeeTaxStatus.DataSource = EmployeeTaxStatuss;
        }

        protected void grdEmployeeTaxStatus_InsertCommand(object source, GridCommandEventArgs e)
        {
            EmployeeTaxStatus entity = EmployeeTaxStatuss.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdEmployeeTaxStatus.Rebind();
        }

        protected void grdEmployeeTaxStatus_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            Int32 id = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][EmployeeTaxStatusMetadata.ColumnNames.SPTYear]);
            EmployeeTaxStatus entity = FindEmployeeTaxStatus(id);
            if (entity != null && entity.IsClosed == false)
                SetEntityValue(entity, e);
        }

        private EmployeeTaxStatus FindEmployeeTaxStatus(Int32 id)
        {
            EmployeeTaxStatusCollection coll = EmployeeTaxStatuss;
            EmployeeTaxStatus retEntity = null;
            foreach (EmployeeTaxStatus rec in coll)
            {
                if (rec.SPTYear.Equals(id))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }
        private void SetEntityValue(EmployeeTaxStatus entity, GridCommandEventArgs e)
        {
            var userControl = (EmployeeTaxStatusDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                //TODO: Lengkapi field yg belum terisi, untuk ref key ke header di set di SetEntity
                entity.PersonID = Convert.ToInt32(txtPersonID.Value);
                entity.SPTYear = userControl.SPTYear;
                entity.SRTaxStatus = userControl.SRTaxStatus;
                entity.TaxStatusName = userControl.TaxStatusName;
                entity.IsClosed = false;
            }
        }
        #endregion

        #region Detail Methode Function Salary Info
        protected void grdHistoryItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdHistoryItem.DataSource = EmployeeSalaryHistory;
        }

        private DataTable EmployeeSalaryHistory
        {
            get
            {
                var query = new WageTransactionItemQuery("a");
                var wtq = new WageTransactionQuery("b");
                var salary = new SalaryComponentQuery("c");

                query.Select
                    (query,
                        salary.SalaryComponentCode,
                        salary.SalaryComponentName
                    );
                query.InnerJoin(wtq).On(wtq.WageTransactionID == query.WageTransactionID);
                query.InnerJoin(salary).On(query.SalaryComponentID == salary.SalaryComponentID);

                query.Where(wtq.WageProcessTypeID == 1, wtq.PersonID == (Convert.ToInt32(txtPersonID.Text)));

                if (string.IsNullOrEmpty(cboPayrollPeriodID.SelectedValue))
                    query.Where(wtq.PayrollPeriodID == -1);
                else
                {
                    var payrollPeriod = new PayrollPeriod();
                    if (payrollPeriod.LoadByPrimaryKey(Convert.ToInt32(cboPayrollPeriodID.SelectedValue)))
                        query.Where(wtq.PayrollPeriodID == payrollPeriod.PayrollPeriodID);
                    else
                        query.Where(wtq.PayrollPeriodID == -1);
                }

                query.OrderBy(salary.SalaryComponentCode.Ascending);

                DataTable dtb = query.LoadDataTable();
                return dtb;
            }
        }

        protected void btnFilterHistory_Click(object sender, ImageClickEventArgs e)
        {
            grdHistoryItem.Rebind();
        }

        protected void grdThr_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdThr.DataSource = EmployeeThrHistory;
        }

        private DataTable EmployeeThrHistory
        {
            get
            {
                var query = new WageTransactionItemQuery("a");
                var wtq = new WageTransactionQuery("b");
                var salary = new SalaryComponentQuery("c");

                query.Select
                    (query,
                        salary.SalaryComponentCode,
                        salary.SalaryComponentName
                    );
                query.InnerJoin(wtq).On(wtq.WageTransactionID == query.WageTransactionID);
                query.InnerJoin(salary).On(query.SalaryComponentID == salary.SalaryComponentID);

                query.Where(wtq.WageProcessTypeID == 2, wtq.PersonID == (Convert.ToInt32(txtPersonID.Text)));

                if (string.IsNullOrEmpty(cboThrPayrollPeriodID.SelectedValue))
                    query.Where(wtq.PayrollPeriodID == -1);
                else
                {
                    var payrollPeriod = new PayrollPeriod();
                    if (payrollPeriod.LoadByPrimaryKey(Convert.ToInt32(cboThrPayrollPeriodID.SelectedValue)))
                        query.Where(wtq.PayrollPeriodID == payrollPeriod.PayrollPeriodID);
                    else
                        query.Where(wtq.PayrollPeriodID == -1);
                }

                query.OrderBy(salary.SalaryComponentCode.Ascending);

                DataTable dtb = query.LoadDataTable();
                return dtb;
            }
        }

        protected void btnFilterThr_Click(object sender, ImageClickEventArgs e)
        {
            grdThr.Rebind();
        }

        protected void cboPayrollPeriodID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new PayrollPeriodQuery();
            query.es.Top = 12;
            query.Select
                (
                    query.PayrollPeriodID,
                    query.PayrollPeriodCode,
                    query.PayrollPeriodName
                );
            query.Where
                (
                    query.Or
                        (
                            query.PayrollPeriodCode.Like(searchTextContain),
                            query.PayrollPeriodName.Like(searchTextContain)
                        )
                );
            if (string.IsNullOrEmpty(e.Text))
                query.Where(query.SPTYear == DateTime.Now.Year);

            query.OrderBy(query.PayrollPeriodCode.Ascending);

            cboPayrollPeriodID.DataSource = query.LoadDataTable();
            cboPayrollPeriodID.DataBind();
        }

        protected void cboThrPayrollPeriodID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new PayrollPeriodQuery("a");
            var thr = new ClosingThrTransactionQuery("b");
            query.InnerJoin(thr).On(thr.PayrollPeriodID == query.PayrollPeriodID);
            query.es.Top = 10;
            query.Select
                (
                    query.PayrollPeriodID,
                    query.PayrollPeriodCode,
                    query.PayrollPeriodName
                );
            query.Where
                (
                    query.Or
                        (
                            query.PayrollPeriodCode.Like(searchTextContain),
                            query.PayrollPeriodName.Like(searchTextContain)
                        )
                );
            query.OrderBy(query.PayrollPeriodCode.Ascending);

            cboThrPayrollPeriodID.DataSource = query.LoadDataTable();
            cboThrPayrollPeriodID.DataBind();
        }

        protected void cboPayrollPeriodID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["PayrollPeriodName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PayrollPeriodID"].ToString();
        }
        #endregion

        #region Detail Methode Function EmployeeWageStructureAndScale
        protected void grdWageScalePosition_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
                grdWageScalePosition.DataSource = EmployeeWageStructureAndScales;
        }

        private DataTable EmployeeWageStructureAndScales
        {
            get
            {
                var query = new EmployeeWageStructureAndScaleQuery("a");
                var wtq = new EmployeeWageStructureAndScalePositionQuery("b");
                var wgroupq = new AppStandardReferenceItemQuery("c");
                var wsubgroupq = new AppStandardReferenceItemQuery("d");
                var jobposq = new AppStandardReferenceItemQuery("e");

                query.Select
                    (query, 
                        wgroupq.ItemName.As("EmployeeWorkGroupName"),
                        wsubgroupq.ItemName.As("EmployeeWorkSubGroupName"),
                        jobposq.ItemName.As("EmployeeJobPositionName")
                    );
                query.LeftJoin(wtq).On(wtq.WageStructureAndScalePositionID == query.WageStructureAndScalePositionID);
                query.LeftJoin(wgroupq).On(wgroupq.StandardReferenceID == AppEnum.StandardReference.EmployeeWorkGroup.ToString() && wgroupq.ItemID == wtq.SREmployeeWorkGroup);
                query.LeftJoin(wsubgroupq).On(wsubgroupq.StandardReferenceID == AppEnum.StandardReference.EmployeeWorkSubGroup.ToString() && wsubgroupq.ItemID == wtq.SREmployeeWorkSubGroup);
                query.LeftJoin(jobposq).On(jobposq.StandardReferenceID == AppEnum.StandardReference.EmployeeJobPosition.ToString() && jobposq.ItemID == wtq.SREmployeeJobPosition);

                query.Where(query.PersonID == (Convert.ToInt32(txtPersonID.Text)));

                query.OrderBy(query.ValidFrom.Descending);

                DataTable dtb = query.LoadDataTable();
                return dtb;
            }
        }

        protected void grdWageScalePosition_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            var query = new EmployeeWageStructureAndScalePositionItemQuery("a");
            var type = new AppStandardReferenceItemQuery("b");
            var scale = new WageStructureAndScaleQuery("c");
            var scaleitm = new WageStructureAndScaleItemQuery("d");
            var scaleitmdef = new AppStandardReferenceItemQuery("e");

            query.Select
                (
                   query,
                   type.ItemName.As("WageStructureAndScaleTypeName"),
                   scale.WageStructureAndScaleName.As("WageStructureAndScaleName"),
                   scaleitmdef.ItemName.As("WageStructureAndScaleItemName")
                );

            query.InnerJoin(type).On(type.StandardReferenceID == AppEnum.StandardReference.WageStructureAndScaleType.ToString() && type.ItemID == query.SRWageStructureAndScaleType);
            query.InnerJoin(scale).On(scale.WageStructureAndScaleID == query.WageStructureAndScaleID);
            query.InnerJoin(scaleitm).On(scaleitm.WageStructureAndScaleItemID == query.WageStructureAndScaleItemID);
            query.InnerJoin(scaleitmdef).On(scaleitmdef.StandardReferenceID == AppEnum.StandardReference.WageStructureAndScaleItem.ToString() && scaleitmdef.ItemID == scaleitm.SRWageStructureAndScaleItem);

            query.Where(query.WageStructureAndScalePositionID == Convert.ToInt32(e.DetailTableView.ParentItem.GetDataKeyValue("WageStructureAndScalePositionID")));
            query.OrderBy(query.SRWageStructureAndScaleType.Ascending);

            e.DetailTableView.DataSource = query.LoadDataTable();
        }

        #endregion

        #region Detail Methode Function Incentive Position
        private void RefreshCommandItemIncentivePosition(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdIncentivePosition.Columns[0].Visible = isVisible;
            grdIncentivePosition.Columns[grdIncentivePosition.Columns.Count - 1].Visible = isVisible;

            grdIncentivePosition.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Perbaharui tampilan dan data
            grdIncentivePosition.Rebind();
        }

        private EmployeeIncentivePositionCollection EmployeeIncentivePositions
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collEmployeeIncentivePosition" + Request.UserHostName + hdnPageId.Value];
                    if (obj != null)
                    {
                        return ((EmployeeIncentivePositionCollection)(obj));
                    }
                }

                var coll = new EmployeeIncentivePositionCollection();
                var query = new EmployeeIncentivePositionQuery("a");
                var isug = new AppStandardReferenceItemQuery("b");
                var ipg = new AppStandardReferenceItemQuery("c");
                var ip = new AppStandardReferenceItemQuery("d");
                query.Select
                    (
                      query,
                      isug.ItemName.As("refToAppStdRef_IncentiveServiceUnitGroupName"),
                      ipg.ItemName.As("refToAppStdRef_IncentivePositionGroupName"),
                      ip.ItemName.As("refToAppStdRef_IncentivePositionName")
                    );
                query.LeftJoin(isug).On(isug.StandardReferenceID == AppEnum.StandardReference.IncentiveServiceUnitGroup.ToString() && isug.ItemID == query.SRIncentiveServiceUnitGroup);
                query.LeftJoin(ipg).On(ipg.StandardReferenceID == AppEnum.StandardReference.IncentivePositionGroup.ToString() && ipg.ItemID == query.SRIncentivePositionGroup);
                query.LeftJoin(ip).On(ip.StandardReferenceID == AppEnum.StandardReference.IncentivePosition.ToString() && ip.ItemID == query.SRIncentivePosition);

                query.Where(query.PersonID == (txtPersonID.Value ?? 0));
                query.OrderBy(query.ValidFrom.Descending);
                coll.Load(query);

                Session["collEmployeeIncentivePosition" + Request.UserHostName + hdnPageId.Value] = coll;
                return coll;
            }
            set { Session["collEmployeeIncentivePosition" + Request.UserHostName + hdnPageId.Value] = value; }
        }

        private void PopulateEmployeeIncentivePositionGrid()
        {
            //Display Data Detail
            EmployeeIncentivePositions = null; //Reset Record Detail
            grdIncentivePosition.DataSource = EmployeeIncentivePositions; //Requery
            grdIncentivePosition.MasterTableView.IsItemInserted = false;
            grdIncentivePosition.MasterTableView.ClearEditItems();
            grdIncentivePosition.DataBind();
        }

        protected void grdIncentivePosition_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdIncentivePosition.DataSource = EmployeeIncentivePositions;
        }

        protected void grdIncentivePosition_InsertCommand(object source, GridCommandEventArgs e)
        {
            EmployeeIncentivePosition entity = EmployeeIncentivePositions.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdIncentivePosition.Rebind();
        }

        protected void grdIncentivePosition_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            Int32 id = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][EmployeeIncentivePositionMetadata.ColumnNames.IncentivePositionID]);
            EmployeeIncentivePosition entity = FindEmployeeIncentivePosition(id);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdIncentivePosition_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            Int32 id = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][EmployeeIncentivePositionMetadata.ColumnNames.IncentivePositionID]);
            EmployeeIncentivePosition entity = FindEmployeeIncentivePosition(id);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        private EmployeeIncentivePosition FindEmployeeIncentivePosition(Int32 id)
        {
            EmployeeIncentivePositionCollection coll = EmployeeIncentivePositions;
            EmployeeIncentivePosition retEntity = null;
            foreach (EmployeeIncentivePosition rec in coll)
            {
                if (rec.IncentivePositionID.Equals(id))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }
        private void SetEntityValue(EmployeeIncentivePosition entity, GridCommandEventArgs e)
        {
            var userControl = (IncentivePositionDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                //TODO: Lengkapi field yg belum terisi, untuk ref key ke header di set di SetEntity
                entity.PersonID = Convert.ToInt32(txtPersonID.Value);
                entity.SRIncentiveServiceUnitGroup = userControl.SRIncentiveServiceUnitGroup;
                entity.IncentiveServiceUnitGroupName = userControl.IncentiveServiceUnitGroupName;
                entity.SRIncentivePositionGroup = userControl.SRIncentivePositionGroup;
                entity.IncentivePositionGroupName = userControl.IncentivePositionGroupName;
                entity.SRIncentivePosition = userControl.SRIncentivePosition;
                entity.IncentivePositionName = userControl.IncentivePositionName;
                entity.IncentivePositionPoints = userControl.IncentivePositionPoints;
                entity.ValidFrom = userControl.ValidFrom;
                entity.ValidTo = userControl.ValidTo;
            }
        }
        #endregion
        private void PopulateEmployeeImage(int personId, string gender)
        {
            // Load from database
            var personalImg = new PersonalImage();
            if (personalImg.LoadByPrimaryKey(personId))
            {
                // Show Image
                if (personalImg.Photo != null)
                {
                    imgPhoto.ImageUrl = string.Format("data:image/jpg;base64,{0}",
                        Convert.ToBase64String(personalImg.Photo));
                }
                else
                {
                    imgPhoto.ImageUrl = gender == "M" ? "~/Images/Asset/Patient/ManVector.png" : "~/Images/Asset/Patient/WomanVector.png";
                }
            }
            else
                imgPhoto.ImageUrl = gender == "M" ? "~/Images/Asset/Patient/ManVector.png" : "~/Images/Asset/Patient/WomanVector.png";
        }
    }
}
