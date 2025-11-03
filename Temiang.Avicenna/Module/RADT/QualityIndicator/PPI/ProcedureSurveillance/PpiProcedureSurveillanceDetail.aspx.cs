using System;
using System.Data;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.RADT.QualityIndicator
{
    public partial class PpiProcedureSurveillanceDetail : BasePageDetail
    {
        #region Page Event & Initialize
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            WindowSearch.Height = 300;
            // Url Search & List
            UrlPageSearch = "#";
            UrlPageList = "PpiProcedureSurveillanceList.aspx";

            ProgramID = AppConstant.Program.PpiProcedureSurveillance;

            //StandardReference Initialize
            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRProcedureClassification, AppEnum.StandardReference.ProcedureClassification);
                StandardReference.InitializeIncludeSpace(cboSRTypesOfSurgery, AppEnum.StandardReference.TypesOfSurgery);
                StandardReference.InitializeIncludeSpace(cboSRRiskCategory, AppEnum.StandardReference.RiskCategory);
                StandardReference.InitializeIncludeSpace(cboSRWoundClassification, AppEnum.StandardReference.WoundClassification);
                StandardReference.InitializeIncludeSpace(cboSRAsaScore, AppEnum.StandardReference.AsaScore);
                StandardReference.InitializeIncludeSpace(cboSRTTime, AppEnum.StandardReference.TTime);
                StandardReference.InitializeIncludeSpace(cboSRPatientInType, AppEnum.StandardReference.PatientInType, AppConstant.RegistrationType.InPatient);
            }
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {

        }
        #endregion

        #region Toolbar Menu Event
        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new Registration());
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new ServiceUnitBooking();
            if (entity.LoadByPrimaryKey(txtBookingNo.Text))
            {
                var surveillance = new PpiProcedureSurveillance();
                if (!surveillance.LoadByPrimaryKey(txtBookingNo.Text))
                {
                    surveillance.AddNew();
                    surveillance.BookingNo = txtBookingNo.Text;
                }

                SetEntityValue(surveillance);
                SaveEntity(surveillance);
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
            //auditLogFilter.PrimaryKeyData = string.Format("BookingNo='{0}'", txtBookingNo.Text.Trim());
            //auditLogFilter.TableName = "PpiProcedureSurveillance";
        }

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            printJobParameters.AddNew("BookingNo", txtBookingNo.Text);
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
        }

        #endregion

        #region ToolBar Menu Support
        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            ToolBarMenuDelete.Enabled = false;
        }

        public override bool OnGetStatusMenuEdit()
        {
            return txtBookingNo.Text != string.Empty;
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            RefreshCommandItemUseOfAntibiotics(newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new ServiceUnitBooking();
            if (parameters.Length > 0)
            {
                String bookingNo = (String)parameters[0];
                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(bookingNo);
            }
            else
                entity.LoadByPrimaryKey(txtBookingNo.Text);
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var booking = (ServiceUnitBooking) entity;
            txtBookingNo.Text = booking.BookingNo;
            txtRegistrationNo.Text = booking.RegistrationNo;

            var reg = new Registration();
            reg.LoadByPrimaryKey(txtRegistrationNo.Text);
            txtRegistrationNo.Text = reg.RegistrationNo;

            /*I. Medical Record*/
            var unit = new ServiceUnit();
            unit.LoadByPrimaryKey(reg.ServiceUnitID);
            txtServiceUnitName.Text = unit.ServiceUnitName;

            txtRealizationDateFrom.SelectedDate = booking.RealizationDateTimeFrom.Value.Date;
            txtRealizationTimeFrom.SelectedDate = booking.RealizationDateTimeFrom;
            txtRealizationDateTo.SelectedDate = booking.RealizationDateTimeTo.Value.Date;
            txtRealizationTimeTo.SelectedDate = booking.RealizationDateTimeTo;
            cboSRPatientInType.SelectedValue = reg.SRPatientInType;

            var pat = new Patient();
            pat.LoadByPrimaryKey(reg.PatientID);
            txtMedicalNo.Text = pat.MedicalNo;

            txtInitialDiagnose.Text = reg.InitialDiagnose;
            txtFinalDiagnose.Text = string.Empty;
            var epq = new EpisodeDiagnoseQuery("a");
            var dq = new DiagnoseQuery("b");
            epq.InnerJoin(dq).On(epq.DiagnoseID == dq.DiagnoseID);
            epq.Where(epq.RegistrationNo == txtRegistrationNo.Text,
                      epq.SRDiagnoseType == AppSession.Parameter.DiagnoseTypeMain, epq.IsVoid == false);
            epq.Select(dq.DiagnoseName);
            DataTable epdt = epq.LoadDataTable();
            if (epdt.Rows.Count > 0)
            {
                foreach (DataRow row in epdt.Rows)
                {
                    if (txtFinalDiagnose.Text == string.Empty)
                        txtFinalDiagnose.Text = row["DiagnoseName"].ToString();
                    else
                        txtFinalDiagnose.Text = "; " + row["DiagnoseName"].ToString();
                }
            }

            /*II. Patient Demographic*/
            txtPatientName.Text = pat.PatientName;
            txtDateOfBirth.SelectedDate = pat.DateOfBirth;
            txtSex.Text = pat.Sex;
            HitungUmur(pat.DateOfBirth, (new DateTime()).NowAtSqlServer().Date, txtAge);
            txtAddress.Text = pat.Address;
            txtPhoneNo.Text = pat.PhoneNo;

            /*III. Patient Risk Factors*/
            var surveillance = new PpiProcedureSurveillance();
            surveillance.LoadByPrimaryKey(txtBookingNo.Text);

            chkIsRiskFactorAge.Checked = surveillance.IsRiskFactorAge ?? false;
            chkIsRiskFactorNutrient.Checked = surveillance.IsRiskFactorNutrient ?? false;
            chkIsRiskFactorObesity.Checked = surveillance.IsRiskFactorObesity ?? false;
            chkIsDiabetes.Checked = surveillance.IsDiabetes ?? false;
            chkIsHypertension.Checked = surveillance.IsHypertension ?? false;
            chkIsHiv.Checked = surveillance.IsHiv ?? false;
            chkIsHbv.Checked = surveillance.IsHbv ?? false;
            chkIsHcv.Checked = surveillance.IsHcv ?? false;

            /*IV. The Surgery Room Attendant*/
            var par = new Paramedic();
            txtSurgeon1.Text = par.LoadByPrimaryKey(booking.ParamedicID) ? par.ParamedicName : string.Empty;
            par = new Paramedic();
            txtSurgeon2.Text = par.LoadByPrimaryKey(booking.ParamedicID2) ? par.ParamedicName : string.Empty;
            par = new Paramedic();
            txtAssistant1.Text = par.LoadByPrimaryKey(booking.AssistantID1) ? par.ParamedicName : string.Empty;
            par = new Paramedic();
            txtAssistant2.Text = par.LoadByPrimaryKey(booking.AssistantID2) ? par.ParamedicName : string.Empty;
            cboSRProcedureClassification.SelectedValue = surveillance.SRProcedureClassification;
            cboSRTypesOfSurgery.SelectedValue = surveillance.SRTypesOfSurgery;

            /*V. Risk Category*/
            cboSRRiskCategory.SelectedValue = surveillance.SRRiskCategory;
            cboSRWoundClassification.SelectedValue = surveillance.SRWoundClassification;
            cboSRAsaScore.SelectedValue = surveillance.SRAsaScore;
            cboSRTTime.SelectedValue = surveillance.SRTTime;

            /*VI. Use Of Antibiotics*/
            PopulateUseOfAntibiotictGrid();

            /*VII. Ancillary Services*/
            //PopulateLaboratoriumGrid();
            //PopulateRadilogyGrid();

            /*VIII. Cultures*/
            txtCultures.Text = surveillance.Culturs;
        }

        private void HitungUmur(DateTime? DateOfBirth, DateTime? dNow, RadTextBox txt)
        {
            if (!DateOfBirth.HasValue) return;
            if (!dNow.HasValue) return;
            var y = Helper.GetAgeInYear(DateOfBirth.Value.Date, dNow.Value.Date).ToString();
            var m = Helper.GetAgeInMonth(DateOfBirth.Value.Date, dNow.Value.Date).ToString();
            var d = Helper.GetAgeInDay(DateOfBirth.Value.Date, dNow.Value.Date).ToString();

            txt.Text = y + " yr " + m + " mth " + d + " dy ";
        }
        #endregion

        #region Private Method Standard
        private void SetEntityValue(PpiProcedureSurveillance surveillance)
        {
            surveillance.IsRiskFactorAge = chkIsRiskFactorAge.Checked;
            surveillance.IsRiskFactorNutrient = chkIsRiskFactorNutrient.Checked;
            surveillance.IsRiskFactorObesity = chkIsRiskFactorObesity.Checked;
            surveillance.IsDiabetes = chkIsDiabetes.Checked;
            surveillance.IsHypertension = chkIsHypertension.Checked;
            surveillance.IsHiv = chkIsHiv.Checked;
            surveillance.IsHbv = chkIsHbv.Checked;
            surveillance.IsHcv = chkIsHcv.Checked;
            surveillance.SRProcedureClassification = cboSRProcedureClassification.SelectedValue;
            surveillance.SRTypesOfSurgery = cboSRTypesOfSurgery.SelectedValue;
            surveillance.SRRiskCategory = cboSRRiskCategory.SelectedValue;
            surveillance.SRWoundClassification = cboSRWoundClassification.SelectedValue;
            surveillance.SRAsaScore = cboSRAsaScore.SelectedValue;
            surveillance.SRTTime = cboSRTTime.SelectedValue;
            surveillance.Culturs = txtCultures.Text;

            surveillance.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            surveillance.LastUpdateByUserID = AppSession.UserLogin.UserID;

            foreach (var item in PpiProcedureSurveillanceUseOfAntibioticss)
            {
                item.BookingNo = txtBookingNo.Text;
            }
        }

        private void SaveEntity(PpiProcedureSurveillance surveillance)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                surveillance.Save();
                PpiProcedureSurveillanceUseOfAntibioticss.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new ServiceUnitBookingQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.BookingNo > txtBookingNo.Text, que.IsApproved == true);
                que.OrderBy(que.BookingNo.Ascending);
            }
            else
            {
                que.Where(que.BookingNo < txtBookingNo.Text, que.IsApproved == true);
                que.OrderBy(que.BookingNo.Descending);
            }
            var entity = new ServiceUnitBooking();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }
        #endregion

        #region Override Method & Function

        #endregion

        #region Record Detail Method Function of PpiProcedureSurveillanceUseOfAntibiotics

        private PpiProcedureSurveillanceUseOfAntibioticsCollection PpiProcedureSurveillanceUseOfAntibioticss
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collPpiProcedureSurveillanceUseOfAntibiotics"];
                    if (obj != null)
                    {
                        return ((PpiProcedureSurveillanceUseOfAntibioticsCollection)(obj));
                    }
                }

                var coll = new PpiProcedureSurveillanceUseOfAntibioticsCollection();
                var query = new PpiProcedureSurveillanceUseOfAntibioticsQuery("a");
                var item = new ItemQuery("b");
                
                query.Select
                    (
                        query,
                        item.ItemName.As("refToItem_ItemName")
                    );
                query.InnerJoin(item).On(query.ItemID == item.ItemID);
                query.Where(query.BookingNo == txtBookingNo.Text);
                query.OrderBy(query.StartDate.Ascending, query.ItemID.Ascending);
                coll.Load(query);

                Session["collPpiProcedureSurveillanceUseOfAntibiotics"] = coll;
                return coll;
            }
            set
            {
                Session["collPpiProcedureSurveillanceUseOfAntibiotics"] = value;
            }
        }

        private void RefreshCommandItemUseOfAntibiotics(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdUseOfAntibiotict.Columns[0].Visible = isVisible;
            grdUseOfAntibiotict.Columns[grdUseOfAntibiotict.Columns.Count - 1].Visible = isVisible;

            grdUseOfAntibiotict.MasterTableView.CommandItemDisplay = isVisible
                                                                             ? GridCommandItemDisplay.Top
                                                                             : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdUseOfAntibiotict.Rebind();
        }

        private void PopulateUseOfAntibiotictGrid()
        {
            //Display Data Detail
            PpiProcedureSurveillanceUseOfAntibioticss = null; //Reset Record Detail
            grdUseOfAntibiotict.DataSource = PpiProcedureSurveillanceUseOfAntibioticss; //Requery
            grdUseOfAntibiotict.MasterTableView.IsItemInserted = false;
            grdUseOfAntibiotict.MasterTableView.ClearEditItems();
            grdUseOfAntibiotict.DataBind();
        }

        private PpiProcedureSurveillanceUseOfAntibiotics FindItemUseOfAntibiotics(String itemId, DateTime startDate)
        {
            PpiProcedureSurveillanceUseOfAntibioticsCollection coll = PpiProcedureSurveillanceUseOfAntibioticss;
            PpiProcedureSurveillanceUseOfAntibiotics retEntity = null;
            foreach (PpiProcedureSurveillanceUseOfAntibiotics rec in coll)
            {
                if (rec.ItemID.Equals(itemId) && rec.StartDate.Value.Date.Equals(startDate.Date))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        protected void grdUseOfAntibiotict_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdUseOfAntibiotict.DataSource = PpiProcedureSurveillanceUseOfAntibioticss;
        }

        protected void grdUseOfAntibiotict_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String itemId =
                Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][PpiProcedureSurveillanceUseOfAntibioticsMetadata.ColumnNames.ItemID]);
            DateTime startDate =
                Convert.ToDateTime(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][PpiProcedureSurveillanceUseOfAntibioticsMetadata.ColumnNames.StartDate]);
            PpiProcedureSurveillanceUseOfAntibiotics entity = FindItemUseOfAntibiotics(itemId, startDate);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdUseOfAntibiotict_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            String itemId =
                Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][PpiProcedureSurveillanceUseOfAntibioticsMetadata.ColumnNames.ItemID]);
            DateTime startDate =
                Convert.ToDateTime(item.OwnerTableView.DataKeyValues[item.ItemIndex][PpiProcedureSurveillanceUseOfAntibioticsMetadata.ColumnNames.StartDate]);
            PpiProcedureSurveillanceUseOfAntibiotics entity = FindItemUseOfAntibiotics(itemId, startDate);
            if (entity != null)
            {
                entity.MarkAsDeleted();
            }
        }

        protected void grdUseOfAntibiotict_InsertCommand(object source, GridCommandEventArgs e)
        {
            PpiProcedureSurveillanceUseOfAntibiotics entity = PpiProcedureSurveillanceUseOfAntibioticss.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdUseOfAntibiotict.Rebind();
        }

        private void SetEntityValue(PpiProcedureSurveillanceUseOfAntibiotics entity, GridCommandEventArgs e)
        {
            var userControl = (PpiProcedureSurveillanceUseOfAntibiotictDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ItemID = userControl.ItemID;
                entity.ItemName = userControl.ItemName;
                if (userControl.StartDate == null)
                    entity.str.StartDate = string.Empty;
                else
                    entity.StartDate = userControl.StartDate;
                if (userControl.EndDate == null)
                    entity.str.EndDate = string.Empty;
                else
                    entity.EndDate = userControl.EndDate;
                
                //Last Update Status
                if (entity.es.IsAdded || entity.es.IsModified)
                {
                    entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                }
            }
        }

        #endregion

        #region Record Detail Method Function of AncillaryServices
        protected void grdRadTestResult_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdRadTestResult.DataSource = RadTestResults;
        }

        private DataTable RadTestResults
        {
            get
            {
                var qChargesItem = new TransChargesItemQuery("a");
                var qCharges = new TransChargesQuery("t");
                var qTestResult = new TestResultQuery("res");
                var qItem = new ItemQuery("c");
                var qToUnit = new ServiceUnitQuery("su");

                qChargesItem.InnerJoin(qCharges).On(qCharges.TransactionNo == qChargesItem.TransactionNo);
                qChargesItem.LeftJoin(qTestResult).On(
                    qChargesItem.TransactionNo == qTestResult.TransactionNo &&
                    qChargesItem.ItemID == qTestResult.ItemID
                    );
                qChargesItem.InnerJoin(qItem).On(
                    qChargesItem.ItemID == qItem.ItemID &&
                    qItem.IsHasTestResults == true
                    );
                qChargesItem.InnerJoin(qToUnit).On(qCharges.ToServiceUnitID == qToUnit.ServiceUnitID);

                qChargesItem.Select(
                    qCharges.RegistrationNo,
                    qChargesItem.TransactionNo,
                    qChargesItem.SequenceNo,
                    qChargesItem.ItemID,
                    qTestResult.ParamedicID,
                    qTestResult.TestResultDateTime,
                    qChargesItem.ParamedicCollectionName.As("ParamedicName"),
                    qItem.ItemName,
                    qChargesItem.IsCito,
                    qToUnit.ServiceUnitName,
                    qChargesItem.RealizationDateTime.As("RealizationDateTime"),
                    qTestResult.TestResult.Substring(100).As("TestResult")
                );

                qChargesItem.Where(qCharges.RegistrationNo == txtRegistrationNo.Text, qChargesItem.IsBillProceed == true,
                                   qCharges.IsCorrection == false, qItem.SRItemType == ItemType.Radiology);
                
                qChargesItem.OrderBy(qCharges.TransactionDate.Ascending, qChargesItem.TransactionNo.Ascending,
                                     qChargesItem.SequenceNo.Ascending);

                DataTable dtb = qChargesItem.LoadDataTable();

                return dtb;
            }
        }

        protected void grdLabTestResult_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
                grdLabTestResult.DataSource = LabTestResults;
        }

        private DataTable LabTestResults
        {
            get
            {
                var query = new TransChargesQuery("a");

                query.Where(
                    query.RegistrationNo == txtRegistrationNo.Text,
                    query.ToServiceUnitID == AppSession.Parameter.ServiceUnitLaboratoryID,
                    query.IsApproved == true
                    );

                query.OrderBy(query.TransactionNo.Ascending);

                DataTable dtb = query.LoadDataTable();

                return dtb;
            }
        }

        protected void grdLabTestResult_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = e.DetailTableView.ParentItem;
            string transNo = dataItem.GetDataKeyValue("TransactionNo").ToString();

            var result = new TransChargesItemCollection();
            e.DetailTableView.DataSource = result.LaboratoryResultByTransactionNo(transNo);
        }
        #endregion
    }
}
