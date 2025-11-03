using System;
using System.Data;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.QualityIndicator
{
    public partial class PPIDetail : BasePageDetail
    {
        #region Page Event & Initialize
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            WindowSearch.Height = 300;
            // Url Search & List
            UrlPageSearch = "PPISearch.aspx";
            UrlPageList = "PPIList.aspx";

            ProgramID = AppConstant.Program.PpiInfectionSurveillance;

            //StandardReference Initialize
            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRDiseaseFactorsHbsAg, AppEnum.StandardReference.DiseaseFactors);
                StandardReference.InitializeIncludeSpace(cboSRDiseaseFactorsAntiHcv, AppEnum.StandardReference.DiseaseFactors);
                StandardReference.InitializeIncludeSpace(cboSRDiseaseFactorsAntiHiv, AppEnum.StandardReference.DiseaseFactors);
                StandardReference.InitializeIncludeSpace(cboSRDischargeMethod, AppEnum.StandardReference.DischargeMethod);
                StandardReference.InitializeIncludeSpace(cboSRDischargeCondition, AppEnum.StandardReference.DischargeCondition);
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
            var entity = new Registration();
            if (entity.LoadByPrimaryKey(txtRegistrationNo.Text))
            {
                var disease = new PpiDiseaseFactors();
                if (!disease.LoadByPrimaryKey(txtRegistrationNo.Text))
                {
                    disease.AddNew();
                    disease.RegistrationNo = txtRegistrationNo.Text;
                    disease.CreatedDateTime = (new DateTime()).NowAtSqlServer();
                    disease.CreatedByUserID = AppSession.UserLogin.UserID;
                }
                
                SetEntityValue(disease);
                SaveEntity(disease);
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
            //auditLogFilter.PrimaryKeyData = string.Format("RegistrationNo='{0}'", txtRegistrationNo.Text.Trim());
            //auditLogFilter.TableName = "Registration";
        }

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            printJobParameters.AddNew("RegistrationNo", txtRegistrationNo.Text);
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
            return txtRegistrationNo.Text != string.Empty;
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            RefreshCommandItemRiskFactors(newVal);
            RefreshCommandItemProcedureSurveillance(newVal);
            RefreshCommandItemInfection(newVal);
            RefreshCommandItemAntimicrobial(newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new Registration();
            if (parameters.Length > 0)
            {
                String regno = (String)parameters[0];
                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(regno);
            }
            else
                entity.LoadByPrimaryKey(txtRegistrationNo.Text);
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var reg = (Registration)entity;
            txtRegistrationNo.Text = reg.RegistrationNo;

            var unit = new ServiceUnit();
            unit.LoadByPrimaryKey(reg.ServiceUnitID);
            txtServiceUnitName.Text = unit.ServiceUnitName;

            var pat = new Patient();
            pat.LoadByPrimaryKey(reg.PatientID);
            txtMedicalNo.Text = pat.MedicalNo;

            txtRegistrationDate.SelectedDate = reg.RegistrationDate;
            txtRegistrationTime.Text = reg.RegistrationTime;
            cboSRPatientInType.SelectedValue = reg.SRPatientInType;

            /*I. Patient Identity*/
            txtPatientName.Text = pat.PatientName;
            txtSex.Text = pat.Sex;

            HitungUmur(pat.DateOfBirth, (new DateTime()).NowAtSqlServer().Date, txtAge);
            txtAddress.Text = pat.Address;
            txtPhoneNo.Text = pat.PhoneNo;

            /*II. Initial Diagnose*/
            txtInitialDiagnose.Text = reg.InitialDiagnose;

            /*III. Patient Transfer*/

            /*IV. Risk Factors*/
            PopulateRiskFactorsGrid();

            var df = new PpiDiseaseFactors();
            if (df.LoadByPrimaryKey(txtRegistrationNo.Text))
            {
                cboSRDiseaseFactorsHbsAg.SelectedValue = df.SRDiseaseFactorsHbsAg;
                cboSRDiseaseFactorsAntiHcv.SelectedValue = df.SRDiseaseFactorsAntiHcv;
                cboSRDiseaseFactorsAntiHiv.SelectedValue = df.SRDiseaseFactorsAntiHiv;
                txtOtherDiseaseFactors.Text = df.OtherDiseaseFactors;
                txtLeukocyte.Text = df.Leukocyte;
                txtLed.Text = df.Led;
                txtGds.Text = df.Gds;
                txtRadiologyResult.Text = df.RadiologyResult;
            }

            /*V. Procedure*/
            PopulateProcedureSurveillanceGrid();

            /*VI. Infections*/
            PopulateInfectionGrid();

            /*VII. Antimicrobial Applications*/
            PopulateAntimicrobialGrid();

            /*VIII. Discharge*/
            cboSRDischargeMethod.SelectedValue = reg.SRDischargeMethod;
            txtDischargeDate.SelectedDate = reg.DischargeDate;
            txtDischargeTime.Text = reg.DischargeTime;
            cboSRDischargeCondition.SelectedValue = reg.SRDischargeCondition;
            txtDeathCertificateNo.Text = reg.DeathCertificateNo;
            txtDischargeMedicalNotes.Text = reg.DischargeMedicalNotes;
            txtDischargeNotes.Text = reg.DischargeNotes;
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
        private void SetEntityValue(PpiDiseaseFactors disease)
        {
            disease.SRDiseaseFactorsHbsAg = cboSRDiseaseFactorsHbsAg.SelectedValue;
            disease.SRDiseaseFactorsAntiHcv = cboSRDiseaseFactorsAntiHcv.SelectedValue;
            disease.SRDiseaseFactorsAntiHiv = cboSRDiseaseFactorsAntiHiv.SelectedValue;
            disease.OtherDiseaseFactors = txtOtherDiseaseFactors.Text;
            disease.Leukocyte = txtLeukocyte.Text;
            disease.Led = txtLed.Text;
            disease.Gds = txtGds.Text;
            disease.RadiologyResult = txtRadiologyResult.Text;
            disease.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            disease.LastUpdateByUserID = AppSession.UserLogin.UserID;

            foreach (var item in PpiRiskFactorss)
            {
                item.RegistrationNo = txtRegistrationNo.Text;
            }

            foreach (var item in PpiInfections)
            {
                item.RegistrationNo = txtRegistrationNo.Text;
            }

            foreach (var item in PpiAntimicrobialApplicationss)
            {
                item.RegistrationNo = txtRegistrationNo.Text;
            }
        }

        private void SaveEntity(PpiDiseaseFactors disease)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                disease.Save();

                PpiRiskFactorss.Save();
                PpiProcedureSurveillances.Save();
                PpiInfections.Save();
                PpiAntimicrobialApplicationss.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new RegistrationQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.RegistrationNo > txtRegistrationNo.Text);
                que.OrderBy(que.RegistrationNo.Ascending);
            }
            else
            {
                que.Where(que.RegistrationNo < txtRegistrationNo.Text);
                que.OrderBy(que.RegistrationNo.Descending);
            }
            var entity = new Registration();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }
        #endregion

        #region Override Method & Function

        #endregion

        #region Record Detail Method Function of PatientTransfer

        private DataTable PatientTransfers
        {
            get
            {
                var query = new PatientTransferQuery("a");
                var fromUnit = new ServiceUnitQuery("b");
                var toUnit = new ServiceUnitQuery("c");
                var fromRoom = new ServiceRoomQuery("d");
                var toRoom = new ServiceRoomQuery("e");
                query.Select
                    (
                        query,
                        fromUnit.ServiceUnitName.As("FromServiceUnit"),
                        toUnit.ServiceUnitName.As("ToServiceUnit"),
                        fromRoom.RoomName.As("FromRoomName"),
                        toRoom.RoomName.As("ToRoomName")
                    );
                query.InnerJoin(fromUnit).On(query.FromServiceUnitID == fromUnit.ServiceUnitID);
                query.InnerJoin(toUnit).On(query.ToServiceUnitID == toUnit.ServiceUnitID);
                query.InnerJoin(fromRoom).On(query.FromRoomID == fromRoom.RoomID);
                query.InnerJoin(toRoom).On(query.ToRoomID == toRoom.RoomID);

                query.Where(query.RegistrationNo == txtRegistrationNo.Text);
                query.OrderBy(query.TransferNo.Ascending);
            
                DataTable dtb = query.LoadDataTable();
                return dtb;
            }
        }

        protected void grdPatientTransfer_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdPatientTransfer.DataSource = PatientTransfers;
        }

        #endregion

        #region Record Detail Method Function of RiskFactors

        private PpiRiskFactorsCollection PpiRiskFactorss
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collPpiRiskFactors"];
                    if (obj != null)
                    {
                        return ((PpiRiskFactorsCollection)(obj));
                    }
                }

                var coll = new PpiRiskFactorsCollection();
                var query = new PpiRiskFactorsQuery("a");
                var std1 = new AppStandardReferenceItemQuery("b");
                var rf = new RiskFactorsQuery("c");
                var std2 = new AppStandardReferenceItemQuery("d");

                query.Select
                    (
                        query,
                        std1.ItemName.As("refToAppStdRef_RiskFactorsType"),
                        rf.RiskFactorsName.As("refToRiskFactors_RiskFactorsName"),
                        std2.ItemName.As("refToAppStdRef_RiskFactorsLocation")
                    );
                query.InnerJoin(std1).On(query.SRRiskFactorsType == std1.ItemID &&
                                         std1.StandardReferenceID == AppEnum.StandardReference.RiskFactorsType);
                query.InnerJoin(rf).On(query.RiskFactorsID == rf.RiskFactorsID &&
                                       query.SRRiskFactorsType == rf.SRRiskFactorsType);
                query.InnerJoin(std2).On(query.SRRiskFactorsLocation == std2.ItemID &&
                                         std2.StandardReferenceID == AppEnum.StandardReference.RiskFactorsLocation);
                query.Where(query.RegistrationNo == txtRegistrationNo.Text);
                query.OrderBy(query.SequenceNo.Ascending);
                coll.Load(query);

                Session["collPpiRiskFactors"] = coll;
                return coll;
            }
            set
            {
                Session["collPpiRiskFactors"] = value;
            }
        }

        private void RefreshCommandItemRiskFactors(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdRiskFactors.Columns[0].Visible = isVisible;
            grdRiskFactors.Columns[grdRiskFactors.Columns.Count - 1].Visible = isVisible;
            grdRiskFactors.Columns[grdRiskFactors.Columns.Count - 2].Visible = isVisible;

            grdRiskFactors.MasterTableView.CommandItemDisplay = isVisible
                                                                             ? GridCommandItemDisplay.Top
                                                                             : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdRiskFactors.Rebind();
        }

        private void PopulateRiskFactorsGrid()
        {
            //Display Data Detail
            PpiRiskFactorss = null; //Reset Record Detail
            grdRiskFactors.DataSource = PpiRiskFactorss; //Requery
            grdRiskFactors.MasterTableView.IsItemInserted = false;
            grdRiskFactors.MasterTableView.ClearEditItems();
            grdRiskFactors.DataBind();
        }

        private PpiRiskFactors FindItemRiskFactors(String seqNo)
        {
            PpiRiskFactorsCollection coll = PpiRiskFactorss;
            PpiRiskFactors retEntity = null;
            foreach (PpiRiskFactors rec in coll)
            {
                if (rec.SequenceNo.Equals(seqNo))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        protected void grdRiskFactors_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdRiskFactors.DataSource = PpiRiskFactorss;
        }

        protected void grdRiskFactors_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String seqNo =
                Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][PpiRiskFactorsMetadata.ColumnNames.SequenceNo]);
            PpiRiskFactors entity = FindItemRiskFactors(seqNo);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdRiskFactors_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            String seqNo =
                Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][PpiRiskFactorsMetadata.ColumnNames.SequenceNo]);
            PpiRiskFactors entity = FindItemRiskFactors(seqNo);
            if (entity != null)
            {
                entity.IsVoid = true;
                entity.VoidByUserID = AppSession.UserLogin.UserID;
                entity.VoidDateTime = (new DateTime()).NowAtSqlServer();
            }
        }

        protected void grdRiskFactors_InsertCommand(object source, GridCommandEventArgs e)
        {
            PpiRiskFactors entity = PpiRiskFactorss.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdRiskFactors.Rebind();
        }

        protected void grdRiskFactors_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                PpiRiskFactors item = PpiRiskFactorss[e.Item.DataSetIndex];
                if (item != null)
                {
                    if (item.IsVoid ?? false)
                    {
                        for (int i = 0; i < e.Item.Cells.Count; i++)
                        {
                            if (i > 0 && i < e.Item.Cells.Count)
                                e.Item.Cells[i].Font.Strikeout = true;
                        }
                    }
                }
            }
        }

        private void SetEntityValue(PpiRiskFactors entity, GridCommandEventArgs e)
        {
            var userControl = (PpiRiskFactorsDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.SequenceNo = userControl.SequenceNo;
                entity.SRRiskFactorsType = userControl.SRRiskFactorsType;
                entity.RiskFactorsTypeName = userControl.RiskFactorsTypeName;
                entity.RiskFactorsID = userControl.RiskFactorsID;
                entity.RiskFactorsName = userControl.RiskFactorsName;
                entity.SRRiskFactorsLocation = userControl.SRRiskFactorsLocation;
                entity.RiskFactorsLocationName = userControl.RiskFactorsLocationName;
                if (userControl.DateOfInitialInstallation == null)
                    entity.str.DateOfInitialInstallation = string.Empty;
                else
                    entity.DateOfInitialInstallation = userControl.DateOfInitialInstallation;
                if (userControl.DateOfFinalInstallation == null)
                    entity.str.DateOfFinalInstallation = string.Empty;
                else
                    entity.DateOfFinalInstallation = userControl.DateOfFinalInstallation;
                entity.IsVoid = false;

                if (entity.es.IsAdded)
                {
                    entity.CreatedByUserID = AppSession.UserLogin.UserID;
                    entity.CreatedDateTime = (new DateTime()).NowAtSqlServer();
                }

                //Last Update Status
                if (entity.es.IsAdded || entity.es.IsModified)
                {
                    entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                }
            }
        }

        #endregion

        #region Record Detail Method Function of PpiProcedureSurveillance

        private PpiProcedureSurveillanceCollection PpiProcedureSurveillances
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collPpiProcedureSurveillance"];
                    if (obj != null)
                    {
                        return ((PpiProcedureSurveillanceCollection)(obj));
                    }
                }

                var coll = new PpiProcedureSurveillanceCollection();
                var query = new PpiProcedureSurveillanceQuery("a");
                var booking = new ServiceUnitBookingQuery("b");
                var std1 = new AppStandardReferenceItemQuery("c");
                var std2 = new AppStandardReferenceItemQuery("d");

                query.Select
                    (
                        query,
                        booking.Diagnose.As("refToServiceBooking_Diagnose"),
                        booking.RealizationDateTimeFrom.As("refToServiceBooking_RealizationDateTimeFrom"),
                        booking.RealizationDateTimeTo.As("refToServiceBooking_RealizationDateTimeTo"),
                        booking.IsCito.As("refToServiceBooking_IsCito"),
                        std1.ItemName.As("refToAppStdRef_WoundClassification"),
                        std2.ItemName.As("refToAppStdRef_AsaScore")
                    );
                query.LeftJoin(booking).On(query.BookingNo == booking.BookingNo);
                query.LeftJoin(std1).On(query.SRWoundClassification == std1.ItemID &&
                                        std1.StandardReferenceID == AppEnum.StandardReference.WoundClassification);
                query.LeftJoin(std2).On(query.SRAsaScore == std2.ItemID &&
                                        std2.StandardReferenceID == AppEnum.StandardReference.AsaScore);
                query.Where(booking.RegistrationNo == txtRegistrationNo.Text, booking.IsApproved == true);
                coll.Load(query);

                Session["collPpiProcedureSurveillance"] = coll;
                return coll;
            }
            set
            {
                Session["collPpiProcedureSurveillance"] = value;
            }
        }

        private void RefreshCommandItemProcedureSurveillance(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdProcedure.Columns[0].Visible = isVisible;
            
            //Perbaharui tampilan dan data
            grdProcedure.Rebind();
        }

        private void PopulateProcedureSurveillanceGrid()
        {
            //Display Data Detail
            PpiProcedureSurveillances = null; //Reset Record Detail
            grdProcedure.DataSource = PpiProcedureSurveillances; //Requery
            grdProcedure.MasterTableView.IsItemInserted = false;
            grdProcedure.MasterTableView.ClearEditItems();
            grdProcedure.DataBind();
        }

        private PpiProcedureSurveillance FindItemProcedureSurveillance(String bookingNo)
        {
            PpiProcedureSurveillanceCollection coll = PpiProcedureSurveillances;
            PpiProcedureSurveillance retEntity = null;
            foreach (PpiProcedureSurveillance rec in coll)
            {
                if (rec.BookingNo.Equals(bookingNo))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        protected void grdProcedure_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdProcedure.DataSource = PpiProcedureSurveillances;
        }

        protected void grdProcedure_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String bookingNo =
                Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][PpiProcedureSurveillanceMetadata.ColumnNames.BookingNo]);
            PpiProcedureSurveillance entity = FindItemProcedureSurveillance(bookingNo);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        private void SetEntityValue(PpiProcedureSurveillance entity, GridCommandEventArgs e)
        {
            var userControl = (PpiProcedureItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.SRWoundClassification = userControl.SRWoundClassification;
                entity.WoundClassificationName = userControl.WoundClassificationName;
                entity.SRAsaScore = userControl.SRAsaScore;
                entity.AsaScoreName = userControl.AsaScoreName;

                //Last Update Status
                if (entity.es.IsAdded || entity.es.IsModified)
                {
                    entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                }
            }
        }

        #endregion

        #region Record Detail Method Function of Infection

        private PpiInfectionCollection PpiInfections
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collPpiInfection"];
                    if (obj != null)
                    {
                        return ((PpiInfectionCollection)(obj));
                    }
                }

                var coll = new PpiInfectionCollection();
                var query = new PpiInfectionQuery("a");
                var std1 = new AppStandardReferenceItemQuery("b");

                query.Select
                    (
                        query,
                        std1.ItemName.As("refToAppStdRef_InfectionType")
                    );
                query.InnerJoin(std1).On(query.SRInfectionType == std1.ItemID &&
                                         std1.StandardReferenceID == AppEnum.StandardReference.InfectionType);
                query.Where(query.RegistrationNo == txtRegistrationNo.Text);
                query.OrderBy(query.SRInfectionType.Ascending);
                coll.Load(query);

                Session["collPpiInfection"] = coll;
                return coll;
            }
            set
            {
                Session["collPpiInfection"] = value;
            }
        }

        private void RefreshCommandItemInfection(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdInfection.Columns[0].Visible = isVisible;
            grdInfection.Columns[grdInfection.Columns.Count - 1].Visible = isVisible;

            grdInfection.MasterTableView.CommandItemDisplay = isVisible
                                                                             ? GridCommandItemDisplay.Top
                                                                             : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdInfection.Rebind();
        }

        private void PopulateInfectionGrid()
        {
            //Display Data Detail
            PpiInfections = null; //Reset Record Detail
            grdInfection.DataSource = PpiInfections; //Requery
            grdInfection.MasterTableView.IsItemInserted = false;
            grdInfection.MasterTableView.ClearEditItems();
            grdInfection.DataBind();
        }

        private PpiInfection FindItemInfection(String id)
        {
            PpiInfectionCollection coll = PpiInfections;
            PpiInfection retEntity = null;
            foreach (PpiInfection rec in coll)
            {
                if (rec.SRInfectionType.Equals(id))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        protected void grdInfection_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdInfection.DataSource = PpiInfections;
        }

        protected void grdInfection_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String id =
                Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][PpiInfectionMetadata.ColumnNames.SRInfectionType]);
            PpiInfection entity = FindItemInfection(id);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdInfection_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            String id =
                Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][PpiInfectionMetadata.ColumnNames.SRInfectionType]);
            PpiInfection entity = FindItemInfection(id);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdInfection_InsertCommand(object source, GridCommandEventArgs e)
        {
            PpiInfection entity = PpiInfections.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdInfection.Rebind();
        }

        private void SetEntityValue(PpiInfection entity, GridCommandEventArgs e)
        {
            var userControl = (PpiInfectionDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.SRInfectionType = userControl.SRInfectionType;
                entity.InfectionTypeName = userControl.InfectionTypeName;
                entity.DaysTo = userControl.DaysTo;
                entity.Cultures = userControl.Cultures;
                if (entity.es.IsAdded)
                {
                    entity.CreatedByUserID = AppSession.UserLogin.UserID;
                    entity.CreatedDateTime = (new DateTime()).NowAtSqlServer();
                }

                //Last Update Status
                if (entity.es.IsAdded || entity.es.IsModified)
                {
                    entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                }
            }
        }

        #endregion

        #region Record Detail Method Function of PpiAntimicrobialApplications

        private PpiAntimicrobialApplicationsCollection PpiAntimicrobialApplicationss
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collPpiAntimicrobialApplications"];
                    if (obj != null)
                    {
                        return ((PpiAntimicrobialApplicationsCollection)(obj));
                    }
                }

                var coll = new PpiAntimicrobialApplicationsCollection();
                var query = new PpiAntimicrobialApplicationsQuery("a");
                var std1 = new AppStandardReferenceItemQuery("b");
                var therapy = new TherapyQuery("c");
                var std2 = new AppStandardReferenceItemQuery("d");

                query.Select
                    (
                        query, 
                        std1.ItemName.As("refToAppStdRef_TherapyGroup"),
                        therapy.TherapyName.As("refToTherapy_TherapyName"),
                        std2.ItemName.As("refToAppStdRef_Timing")
                    );
                query.InnerJoin(std1).On(query.SRTherapyGroup == std1.ItemID &&
                                         std1.StandardReferenceID == AppEnum.StandardReference.TherapyGroup);
                query.LeftJoin(therapy).On(query.TherapyID == therapy.TherapyID &&
                                            query.SRTherapyGroup == therapy.SRTherapyGroup);
                query.LeftJoin(std2).On(query.SRAntimicrobialApplicationTiming == std2.ItemID &&
                                         std2.StandardReferenceID ==
                                         AppEnum.StandardReference.AntimicrobialApplicationTiming);
                query.Where(query.RegistrationNo == txtRegistrationNo.Text);
                query.OrderBy(query.StartDate.Ascending);
                coll.Load(query);

                Session["collPpiAntimicrobialApplications"] = coll;
                return coll;
            }
            set
            {
                Session["collPpiAntimicrobialApplications"] = value;
            }
        }

        private void RefreshCommandItemAntimicrobial(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdAntimicrobial.Columns[0].Visible = isVisible;
            grdAntimicrobial.Columns[grdAntimicrobial.Columns.Count - 1].Visible = isVisible;

            grdAntimicrobial.MasterTableView.CommandItemDisplay = isVisible
                                                                             ? GridCommandItemDisplay.Top
                                                                             : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdAntimicrobial.Rebind();
        }

        private void PopulateAntimicrobialGrid()
        {
            //Display Data Detail
            PpiAntimicrobialApplicationss = null; //Reset Record Detail
            grdAntimicrobial.DataSource = PpiAntimicrobialApplicationss; //Requery
            grdAntimicrobial.MasterTableView.IsItemInserted = false;
            grdAntimicrobial.MasterTableView.ClearEditItems();
            grdAntimicrobial.DataBind();
        }

        private PpiAntimicrobialApplications FindAntimicrobial(String groupId, String therapyId)
        {
            PpiAntimicrobialApplicationsCollection coll = PpiAntimicrobialApplicationss;
            PpiAntimicrobialApplications retEntity = null;
            foreach (PpiAntimicrobialApplications rec in coll)
            {
                if (rec.SRTherapyGroup.Equals(groupId) && rec.TherapyID.Equals(therapyId))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        protected void grdAntimicrobial_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdAntimicrobial.DataSource = PpiAntimicrobialApplicationss;
        }

        protected void grdAntimicrobial_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String groupId =
                Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][PpiAntimicrobialApplicationsMetadata.ColumnNames.SRTherapyGroup]);
            String therapyId =
                Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][PpiAntimicrobialApplicationsMetadata.ColumnNames.TherapyID]);
            PpiAntimicrobialApplications entity = FindAntimicrobial(groupId, therapyId);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdAntimicrobial_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            String groupId =
                Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][PpiAntimicrobialApplicationsMetadata.ColumnNames.SRTherapyGroup]);
            String therapyId =
                Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][PpiAntimicrobialApplicationsMetadata.ColumnNames.TherapyID]);
            PpiAntimicrobialApplications entity = FindAntimicrobial(groupId, therapyId);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdAntimicrobial_InsertCommand(object source, GridCommandEventArgs e)
        {
            PpiAntimicrobialApplications entity = PpiAntimicrobialApplicationss.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdAntimicrobial.Rebind();
        }

        private void SetEntityValue(PpiAntimicrobialApplications entity, GridCommandEventArgs e)
        {
            var userControl = (PpiAntimicrobialItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.SRTherapyGroup = userControl.SRTherapyGroup;
                entity.TherapyGroupName = userControl.TherapyGroupName;
                entity.TherapyID = userControl.TherapyID;
                entity.TherapyName = userControl.TherapyName;
                entity.Dosage = userControl.Dosage;
                entity.SRDosageUnit = userControl.SRDosageUnit;
                if (userControl.StartDate == null)
                    entity.str.StartDate = string.Empty;
                else
                    entity.StartDate = userControl.StartDate;
                if (userControl.EndDate == null)
                    entity.str.EndDate = string.Empty;
                else
                    entity.EndDate = userControl.EndDate;
                entity.SRAntimicrobialApplicationTiming = userControl.SRAntimicrobialApplicationTiming;
                entity.AntimicrobialApplicationTimingName = userControl.AntimicrobialApplicationTimingName;

                if (entity.es.IsAdded)
                {
                    entity.CreatedByUserID = AppSession.UserLogin.UserID;
                    entity.CreatedDateTime = (new DateTime()).NowAtSqlServer();
                }
                //Last Update Status
                if (entity.es.IsAdded || entity.es.IsModified)
                {
                    entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                }
            }
        }

        #endregion

        #region Record Detail Method Function of EpisodeDiagnose

        private DataTable EpisodeDiagnoses
        {
            get
            {
                var query = new EpisodeDiagnoseQuery("a");
                var diag = new DiagnoseQuery("b");
                var std = new AppStandardReferenceItemQuery("c");
                
                query.Select
                    (
                        query,
                        diag.DiagnoseName.As("DiagnoseName"),
                        std.ItemName.As("DiagnoseType")
                    );
                query.InnerJoin(diag).On(query.DiagnoseID == diag.DiagnoseID);
                query.InnerJoin(std).On(query.SRDiagnoseType == std.ItemID &&
                                        std.StandardReferenceID == AppEnum.StandardReference.DiagnoseType);

                query.Where(query.RegistrationNo == txtRegistrationNo.Text, query.IsVoid == false);
                query.OrderBy(query.SequenceNo.Ascending);

                DataTable dtb = query.LoadDataTable();
                return dtb;
            }
        }

        protected void grdEpisodeDiagnose_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdEpisodeDiagnose.DataSource = EpisodeDiagnoses;
        }

        #endregion
    }
}
