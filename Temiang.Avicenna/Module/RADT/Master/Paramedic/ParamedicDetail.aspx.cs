using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Common;
using Temiang.Avicenna.Common;
using System.Linq;
using System.Web.UI;
using System.Globalization;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ParamedicDetail : BasePageDetail
    {
        #region Page Event & Initialize

        private AppAutoNumberLast _autoNumber;

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "ParamedicSearch.aspx";
            UrlPageList = "ParamedicList.aspx";

            ProgramID = AppConstant.Program.Paramedic;

            trPcare.Visible = pcareReference.IsPCareValidation;

            //StandardReference Initialize
            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRParamedicType, AppEnum.StandardReference.ParamedicType);
                StandardReference.InitializeIncludeSpace(cboSRParamedicStatus, AppEnum.StandardReference.ParamedicStatus);
                StandardReference.InitializeIncludeSpace(cboSRReligion, AppEnum.StandardReference.Religion);
                StandardReference.InitializeIncludeSpace(cboSRNationality, AppEnum.StandardReference.Nationality);
                StandardReference.InitializeIncludeSpace(cboSRSpecialty, AppEnum.StandardReference.Specialty);
                ComboBox.PopulateWithSmf(cboSRParamedicRL1);

                #region "perhitungan jasmed skr cuma pake 1 versi"
                //if (AppSession.Parameter.IsPhysicianFeeCalcBasedOnGuarantorCategory)
                //{
                //    tabStrip.Tabs[1].Visible = false;//tab matrix fee 
                //    tabStrip.Tabs[2].Visible = true;//tab matrix fee - guarantor fee category
                //}

                //chkPhysicianFee.Visible = false;
                //var appprg = new AppProgram();
                //if (appprg.LoadByPrimaryKey("05.07.03"))
                //{
                //    if (appprg.NavigateUrl.Trim().IndexOf("ParamedicFeeVerificationByDischargeDateList.aspx") > 0)
                //    {
                //        // paramedic fee based on discharge date
                //        chkPhysicianFee.Visible = true;
                //        // sementara chkPhysicianFee hanya dimunculkan dan dipakai di
                //        // jasa medis by discharge date.
                //    }
                //}
                #endregion

                pnlPhyscianFeeGlobal.Visible = false;
                tabStrip.Tabs[1].Visible = false;//tab matrix fee 
                tabStrip.Tabs[2].Visible = false;//tab matrix fee - guarantor fee category

                trGuaranteeFee.Visible = !AppSession.Application.IsHisMode;
                trCoorporateGradeID.Visible = !AppSession.Application.IsHisMode;

                if (AppSession.Parameter.IsUsingParamedicFeeByTeam)
                {
                    chkIsPhysicianTeam.Visible = true;
                    tabStrip.Tabs[7].Visible = true;
                }

                if (AppSession.Parameter.IsUsingQueueCodeByPhysicianKioskV2)
                    trParamedicQueueCode.Visible = true;
            }

            //PopUp Search
            if (!IsCallback)
            {
                //For Grid Detail
                PopUpSearch.RegisterClientScript(AppEnum.PopUpSearch.ServiceUnitAutoBillItem, Page);
                PopUpSearch.RegisterClientScript(AppEnum.PopUpSearch.ServiceUnit, Page);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

            }
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {

        }

        #endregion

        #region Toolbar Menu Event

        private string GetNewId()
        {
            _autoNumber = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.ParamedicId);

            return _autoNumber.LastCompleteNumber;
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new Paramedic());

            var query = new ParamedicQuery();
            query.es.Top = 1;
            query.Select(query.ParamedicID);
            query.OrderBy(query.LastUpdateDateTime.Descending);

            var medic = new Paramedic();
            medic.Load(query);

            if (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsCreateParamedicIdAutomatic) == "Yes")
                txtParamedicID.Text = GetNewId();

            cboSRParamedicType.SelectedValue = string.Empty;
            cboSRParamedicType.Text = string.Empty;
            cboSRParamedicStatus.SelectedValue = string.Empty;
            cboSRParamedicStatus.Text = string.Empty;
            cboSRReligion.SelectedValue = string.Empty;
            cboSRReligion.Text = string.Empty;
            cboSRNationality.SelectedValue = string.Empty;
            cboSRNationality.Text = string.Empty;
            cboSRSpecialty.SelectedValue = string.Empty;
            cboSRSpecialty.Text = string.Empty;
            cboSRParamedicRL1.SelectedValue = string.Empty;
            cboSRParamedicRL1.Text = string.Empty;

            chkIsAvailable.Checked = true;
            chkIsActive.Checked = true;
            chkPhysicianFee.Checked = true;
            chkPrintInSlip.Checked = true;

            chkIsParamedicFeeUsingPercentage.Checked = true;
            txtParamedicFeeAmount.Value = 0;
            txtParamedicFeeAmountReferral.Value = 0;
            chkIsDeductionFeeUsePercentage.Checked = true;
            txtDeductionFeeAmount.Value = 0;
            txtDeductionFeeAmountReferral.Value = 0;

            chkIsUsingQue.Checked = false;
            chkIsPhysicianTeam.Checked = false;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            //Paramedic entity = new Paramedic();
            //if (entity.LoadByPrimaryKey(txtParamedicID.Text))
            //{
            //    entity.MarkAsDeleted();
            //    SaveEntity(entity);

            //}
            //else
            //{
            //    args.MessageText = AppConstant.Message.RecordNotExist;
            //}

            Paramedic entity = new Paramedic();
            if (entity.LoadByPrimaryKey(txtParamedicID.Text))
            {

                entity.MarkAsDeleted();
                ParamedicFeeItems.MarkAllAsDeleted();
                ParamedicAutoBillItems.MarkAllAsDeleted();
                ParamedicFeeGuarantorCategorys.MarkAllAsDeleted();

                using (esTransactionScope trans = new esTransactionScope())
                {
                    entity.Save();
                    ParamedicFeeItems.Save();
                    ParamedicAutoBillItems.Save();
                    ParamedicFeeGuarantorCategorys.Save();

                    //PCareReferenceItemMapping
                    pcareReference.Delete(entity.ParamedicID);

                    //Commit if success, Rollback if failed
                    trans.Complete();
                }
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            if (AppParameter.GetParameterValue(AppParameter.ParameterItem.HealthcareInitialAppsVersion) != "RSGPI")
            {
                if (string.IsNullOrEmpty(cboSRParamedicRL1.SelectedValue))
                {
                    args.MessageText = "SMF required.";
                    args.IsCancel = true;
                    return;
                }
            }
            Paramedic entity = new Paramedic();
            if (entity.LoadByPrimaryKey(txtParamedicID.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }

            entity = new Paramedic();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            if (AppParameter.GetParameterValue(AppParameter.ParameterItem.HealthcareInitialAppsVersion) != "RSGPI")
            {
                if (string.IsNullOrEmpty(cboSRParamedicRL1.SelectedValue))
                {
                    args.MessageText = "SMF required.";
                    args.IsCancel = true;
                    return;
                }
            }

            Paramedic entity = new Paramedic();
            if (entity.LoadByPrimaryKey(txtParamedicID.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
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
            auditLogFilter.PrimaryKeyData = string.Format("ParamedicID='{0}'", txtParamedicID.Text.Trim());
            auditLogFilter.TableName = "Paramedic";
        }

        #endregion

        #region ToolBar Menu Support

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            txtParamedicID.ReadOnly = (newVal != AppEnum.DataMode.New) || AppParameter.GetParameterValue(AppParameter.ParameterItem.IsCreateParamedicIdAutomatic) == "Yes";

            RefreshCommandItemParamedicFeeItem(newVal);
            RefreshCommandItemServiceUnitAutoBillItem(newVal);
            RefreshCommandItemParamedicFeeGuarantorCategory(newVal);
            RefreshCommandItemServiceUnitParamedic(newVal);
            RefreshCommandItemParamedicBridging(newVal);
            RefreshCommandItemParamedicOtherType(newVal);
            RefreshCommandItemParamedicTeam(newVal);

            btnUpload.Enabled = false;
            if (newVal == AppEnum.DataMode.Read && !string.IsNullOrEmpty(txtParamedicID.Text))
            {
                var par = new Paramedic();
                if (par.LoadByPrimaryKey(txtParamedicID.Text))
                {
                    btnUpload.Enabled = true;
                }
            }
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            Paramedic entity = new Paramedic();
            if (parameters.Length > 0)
            {
                String paramedicID = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(paramedicID);
            }
            else
            {
                entity.LoadByPrimaryKey(txtParamedicID.Text);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            Paramedic paramedic = (Paramedic)entity;
            txtParamedicID.Text = paramedic.ParamedicID;
            txtParamedicName.Text = paramedic.ParamedicName;
            txtParamedicInitial.Text = paramedic.ParamedicInitial;
            txtSsn1.Text = paramedic.Ssn;
            txtDateOfBirth.SelectedDate = paramedic.DateOfBirth;
            cboSRParamedicType.SelectedValue = paramedic.SRParamedicType;
            cboSRParamedicStatus.SelectedValue = paramedic.SRParamedicStatus;
            cboSRReligion.SelectedValue = paramedic.SRReligion;
            cboSRNationality.SelectedValue = paramedic.SRNationality;
            cboSRSpecialty.SelectedValue = paramedic.SRSpecialty;
            cboSRParamedicRL1.SelectedValue = paramedic.SRParamedicRL1;

            ctlAddress.StreetName = paramedic.StreetName;
            ctlAddress.District = paramedic.District;
            ctlAddress.City = paramedic.City;
            ctlAddress.County = paramedic.County;
            ctlAddress.State = paramedic.State;
            ctlAddress.ZipCode = paramedic.ZipCode;
            ctlAddress.PhoneNo = paramedic.PhoneNo;
            ctlAddress.MobilePhoneNo = paramedic.MobilePhoneNo;
            ctlAddress.Email = paramedic.Email;
            txtLicenseNo.Text = paramedic.LicenseNo;
            txtTaxRegistrationNo.Text = paramedic.TaxRegistrationNo;
            chkIsAvailable.Checked = paramedic.IsAvailable ?? false;
            txtNotAvailableUntil.SelectedDate = paramedic.NotAvailableUntil;
            chkIsActive.Checked = paramedic.IsActive ?? false;
            chkPhysicianFee.Checked = paramedic.ParamedicFee ?? false;
            chkPrintInSlip.Checked = paramedic.IsPrintInSlip ?? true;
            txtNotes.Text = paramedic.Notes;
            txtRegistrationNo.Text = paramedic.RegistrationNo;
            txtPeriodeStart.SelectedDate = paramedic.LicensePeriodeStart;
            txtPeriodeEnd.SelectedDate = paramedic.LicensePeriodeEnd;
            txtBank.Text = paramedic.Bank;
            txtBankAccount.Text = paramedic.BankAccount;
            txtBankAccountName.Text = paramedic.BankAccountName;
            txtBank2.Text = paramedic.Bank2;
            txtBankAccount2.Text = paramedic.BankAccount2;
            txtBankAccountName2.Text = paramedic.BankAccountName2;
            chkIsParamedicFeeUsingPercentage.Checked = paramedic.IsParamedicFeeUsePercentage ?? false;
            txtParamedicFeeAmount.Value = Convert.ToDouble(paramedic.ParamedicFeeAmount ?? 0);
            txtParamedicFeeAmountReferral.Value = Convert.ToDouble(paramedic.ParamedicFeeAmountReferral ?? 0);
            chkIsDeductionFeeUsePercentage.Checked = paramedic.IsDeductionFeeUsePercentage ?? false;
            txtDeductionFeeAmount.Value = Convert.ToDouble(paramedic.DeductionFeeAmount ?? 0);
            txtDeductionFeeAmountReferral.Value = Convert.ToDouble(paramedic.DeductionFeeAmountReferral ?? 0);
            chkIsUsingQue.Checked = paramedic.IsUsingQue ?? false;
            chkIsPhysicianTeam.Checked = paramedic.IsPhysicianTeam ?? false;
            txtParamedicQueueCode.Text = paramedic.ParamedicQueueCode;

            txtGuaranteeFee.Value = Convert.ToDouble(paramedic.GuaranteeFee ?? 0);
            int coorporateGradeId = (paramedic.CoorporateGradeID.HasValue ? paramedic.CoorporateGradeID.Value : 0);
            if (coorporateGradeId != 0)
                PopulateCboCoorporateGradeID(cboCoorporateGradeID, coorporateGradeId);
            else
                ClearCombobox(cboCoorporateGradeID);

            txtCoorporateValue.Value = Convert.ToDouble(paramedic.CoorporateGradeValue ?? 0);

            // --COA AP Paramedic Fee--
            int coaAPParaFee = (paramedic.ChartOfAccountIdAPParamedicFee.HasValue ? paramedic.ChartOfAccountIdAPParamedicFee.Value : 0);
            int slAPParaFee = (paramedic.SubledgerIdAPParamedicFee.HasValue ? paramedic.SubledgerIdAPParamedicFee.Value : 0);
            if (coaAPParaFee != 0)
            {
                PopulateCboChartOfAccount(cboChartOfAccountIdAPParamedicFee, coaAPParaFee);
                if (slAPParaFee != 0)
                    PopulateCboSubLedger(cboSubledgerIdAPParamedicFee, slAPParaFee);
                else
                    ClearCombobox(cboSubledgerIdAPParamedicFee);
            }
            else
            {
                ClearCombobox(cboChartOfAccountIdAPParamedicFee);
                ClearCombobox(cboSubledgerIdAPParamedicFee);
            }

            PopulateParamedicFeeItemGrid();
            PopulateServiceUnitAutoBillItemGrid();
            PopulateParamedicFeeGuarantorCategoryGrid();
            PopulateServiceUnitParamedicGrid();
            //var itemGuarantor = ParamedicFeeItemGuarantors;
            PopulateParamedicBirdgingGrid();
            PopulateParamedicOtherTypeGrid();
            PopulateParamedicTeamGrid();

            // foto
            if (paramedic.Foto == null)
            {
                imgFoto.ImageUrl = string.Empty;
            }
            else
            {
                imgFoto.ImageUrl = "data:image;base64," + Convert.ToBase64String(paramedic.Foto);
            }

            //var au = new Temiang.Avicenna.BusinessObject.AppUser();
            //au.Query.es.Distinct = true;
            //au.Query.Where(au.Query.ParamedicID == paramedic.ParamedicID && au.Query.SignatureImage.IsNotNull());
            //if (au.Query.Load())
            //{
            //    if (au.SignatureImage == null)
            //    {
            //        imgSignature.ImageUrl = string.Empty;
            //    }
            //    else
            //    {
            //        imgSignature.ImageUrl = "data:image;base64," + Convert.ToBase64String(au.SignatureImage);
            //    }
            //}
            //else imgSignature.ImageUrl = string.Empty;

            // Pcare Map
            pcareReference.Populate(paramedic.ParamedicID);

        }

        private void PopulateCboChartOfAccount(RadComboBox comboBox, int coaId)
        {
            ChartOfAccountsQuery coaQ = new ChartOfAccountsQuery();
            coaQ.Select(coaQ.ChartOfAccountId, coaQ.ChartOfAccountCode, coaQ.ChartOfAccountName);
            coaQ.Where(coaQ.ChartOfAccountId == coaId);
            DataTable dtbCoa = coaQ.LoadDataTable();
            comboBox.DataSource = dtbCoa;
            comboBox.DataBind();
            comboBox.SelectedValue = coaId.ToString();
        }

        private void PopulateCboSubLedger(RadComboBox comboBox, int subLedgerID)
        {
            SubLedgersQuery slQ = new SubLedgersQuery();
            slQ.Select(slQ.SubLedgerId, slQ.SubLedgerName, slQ.Description);
            slQ.Where(slQ.SubLedgerId == subLedgerID, slQ.GroupId == AppSession.Parameter.SubLedgerGroupIdParamedic);
            DataTable dtbSl = slQ.LoadDataTable();
            comboBox.DataSource = dtbSl;
            comboBox.DataBind();
            comboBox.SelectedValue = subLedgerID.ToString();
        }

        private void ClearCombobox(RadComboBox comboBox)
        {
            comboBox.Items.Clear();
            comboBox.Text = string.Empty;
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(Paramedic entity)
        {
            if (entity.es.IsAdded && AppParameter.GetParameterValue(AppParameter.ParameterItem.IsCreateParamedicIdAutomatic) == "Yes")
            {
                txtParamedicID.Text = GetNewId();
                _autoNumber.Save();
            }

            entity.ParamedicID = txtParamedicID.Text;
            entity.ParamedicName = txtParamedicName.Text;
            entity.ParamedicInitial = txtParamedicInitial.Text;
            entity.Ssn = txtSsn1.Text;
            entity.DateOfBirth = txtDateOfBirth.SelectedDate;
            entity.SRParamedicType = cboSRParamedicType.SelectedValue;
            entity.SRParamedicStatus = cboSRParamedicStatus.SelectedValue;
            entity.SRParamedicRL1 = cboSRParamedicRL1.SelectedValue;
            entity.SRReligion = cboSRReligion.SelectedValue;
            entity.SRNationality = cboSRNationality.SelectedValue;
            entity.SRSpecialty = cboSRSpecialty.SelectedValue;
            entity.StreetName = ctlAddress.StreetName;
            entity.District = ctlAddress.District;
            entity.City = ctlAddress.City;
            entity.County = ctlAddress.County;
            entity.State = ctlAddress.State;
            entity.ZipCode = ctlAddress.ZipCode;
            entity.PhoneNo = ctlAddress.PhoneNo;
            entity.MobilePhoneNo = ctlAddress.MobilePhoneNo;
            entity.Email = ctlAddress.Email;
            entity.LicenseNo = txtLicenseNo.Text;
            entity.TaxRegistrationNo = txtTaxRegistrationNo.Text;
            entity.IsAvailable = chkIsAvailable.Checked;
            entity.NotAvailableUntil = txtNotAvailableUntil.SelectedDate;
            entity.IsActive = chkIsActive.Checked;
            entity.ParamedicFee = chkPhysicianFee.Visible ? chkPhysicianFee.Checked : true;
            entity.IsPrintInSlip = chkPrintInSlip.Checked;
            entity.Notes = txtNotes.Text;
            entity.RegistrationNo = txtRegistrationNo.Text;
            entity.LicensePeriodeStart = txtPeriodeStart.SelectedDate;
            entity.LicensePeriodeEnd = txtPeriodeEnd.SelectedDate;
            entity.IsParamedicFeeUsePercentage = chkIsParamedicFeeUsingPercentage.Checked;
            entity.ParamedicFeeAmount = Convert.ToDecimal(txtParamedicFeeAmount.Value);
            entity.ParamedicFeeAmountReferral = Convert.ToDecimal(txtParamedicFeeAmountReferral.Value);
            entity.Bank = txtBank.Text;
            entity.BankAccount = txtBankAccount.Text;
            entity.BankAccountName = txtBankAccountName.Text;
            entity.Bank2 = txtBank2.Text;
            entity.BankAccount2 = txtBankAccount2.Text;
            entity.BankAccountName2 = txtBankAccountName2.Text;
            entity.IsUsingQue = chkIsUsingQue.Checked;
            entity.IsDeductionFeeUsePercentage = chkIsDeductionFeeUsePercentage.Checked;
            entity.DeductionFeeAmount = Convert.ToDecimal(txtDeductionFeeAmount.Value);
            entity.DeductionFeeAmountReferral = Convert.ToDecimal(txtDeductionFeeAmountReferral.Value);
            entity.IsPhysicianTeam = chkIsPhysicianTeam.Checked;
            entity.ParamedicQueueCode = txtParamedicQueueCode.Text;

            int chartOfAccountIdAPParaFee = 0;
            int subLegderIdAPParaFee = 0;
            int.TryParse(cboChartOfAccountIdAPParamedicFee.SelectedValue, out chartOfAccountIdAPParaFee);
            int.TryParse(cboSubledgerIdAPParamedicFee.SelectedValue, out subLegderIdAPParaFee);
            entity.ChartOfAccountIdAPParamedicFee = chartOfAccountIdAPParaFee;
            entity.SubledgerIdAPParamedicFee = subLegderIdAPParaFee;

            entity.GuaranteeFee = Convert.ToDecimal(txtGuaranteeFee.Value);

            int coorporateGradeId = 0;
            int.TryParse(cboCoorporateGradeID.SelectedValue, out coorporateGradeId);
            entity.CoorporateGradeID = coorporateGradeId;

            entity.CoorporateGradeValue = Convert.ToDecimal(txtCoorporateValue.Value);

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }

            foreach (var item in ParamedicFeeItems)
            {
                item.ParamedicID = txtParamedicID.Text;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = DateTime.Now;
            }

            foreach (var item in ParamedicFeeGuarantorCategorys)
            {
                item.ParamedicID = txtParamedicID.Text;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = DateTime.Now;
            }

            //foreach (var item in ParamedicFeeItemGuarantors)
            //{
            //    item.ParamedicID = txtParamedicID.Text;
            //    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
            //    item.LastUpdateDateTime = DateTime.Now;
            //}

            foreach (var item in ParamedicAutoBillItems)
            {
                item.ParamedicID = txtParamedicID.Text;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = DateTime.Now;
            }

            foreach (var item in ServiceUnitParamedics)
            {
                item.ParamedicID = txtParamedicID.Text;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = DateTime.Now;
            }

            foreach (var item in ParamedicBridgings)
            {
                item.ParamedicID = txtParamedicID.Text;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = DateTime.Now;
            }

            foreach (var item in ParamedicOtherTypes)
            {
                item.ParamedicID = txtParamedicID.Text;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = DateTime.Now;
            }

            foreach (var item in ParamedicTeams)
            {
                item.ParamedicID = txtParamedicID.Text;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(Paramedic entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                ParamedicFeeItems.Save();
                //ParamedicFeeItemGuarantors.Save();
                ParamedicAutoBillItems.Save();
                ParamedicFeeGuarantorCategorys.Save();
                ServiceUnitParamedics.Save();
                ParamedicBridgings.Save();
                ParamedicOtherTypes.Save();
                ParamedicTeams.Save();

                //subledger
                var subledgerGroupId = AppSession.Parameter.SubLedgerGroupIdParamedic;
                if (!string.IsNullOrEmpty(subledgerGroupId))
                {
                    var sub = new BusinessObject.SubLedgers();
                    sub.Query.Where(sub.Query.SubLedgerName == entity.ParamedicID, sub.Query.GroupId == subledgerGroupId);

                    if (sub.Query.Load())
                    {
                        sub.Description = entity.ParamedicName;
                        sub.LastUpdateDateTime = DateTime.Now;
                        sub.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    }
                    else
                    {
                        sub = new BusinessObject.SubLedgers()
                        {
                            GroupId = subledgerGroupId.ToInt(),
                            SubLedgerName = entity.ParamedicID,
                            Description = entity.ParamedicName,
                            DateCreated = DateTime.Now,
                            LastUpdateDateTime = DateTime.Now,
                            CreatedBy = AppSession.UserLogin.UserID,
                            LastUpdateByUserID = AppSession.UserLogin.UserID
                        };
                    }

                    sub.Save();
                }

                //PCareReferenceItemMapping
                pcareReference.Save(entity.ParamedicID);

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            ParamedicQuery que = new ParamedicQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.ParamedicID > txtParamedicID.Text);
                que.OrderBy(que.ParamedicID.Ascending);
            }
            else
            {
                que.Where(que.ParamedicID < txtParamedicID.Text);
                que.OrderBy(que.ParamedicID.Descending);
            }

            Paramedic entity = new Paramedic();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        #endregion

        #region ComboBox ChartOfAccountIdAPParamedicFee
        protected void cboChartOfAccountIdAPParamedicFee_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var selectedSubledgerid = cboSubledgerIdAPParamedicFee.SelectedValue;
            var selectedSubledgerText = cboSubledgerIdAPParamedicFee.Text;

            cboSubledgerIdAPParamedicFee.Items.Clear();
            cboSubledgerIdAPParamedicFee.Text = string.Empty;

            if (e.Value.ToString() != string.Empty)
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                if (!coa.LoadByPrimaryKey(Convert.ToInt32(e.Value)))
                {
                    cboChartOfAccountIdAPParamedicFee.Text = string.Empty;
                    return;
                }
                else
                {
                    if (!string.IsNullOrEmpty(selectedSubledgerid))
                    {
                        // select subledger
                        var ev = new RadComboBoxItemsRequestedEventArgs();
                        char[] c = { ' ' };
                        string[] sText = selectedSubledgerText.Split(c);
                        if (sText.Length > 0)
                        {
                            ev.Text = sText[0];
                        }
                        cboSubledgerIdAPParamedicFee_ItemsRequested(o, ev);
                        cboSubledgerIdAPParamedicFee.SelectedValue = selectedSubledgerid;
                    }
                }
            }
            else
            {
                cboChartOfAccountIdAPParamedicFee.Items.Clear();
                cboChartOfAccountIdAPParamedicFee.Text = string.Empty;
                return;
            }
        }

        protected void cboChartOfAccountIdAPParamedicFee_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ChartOfAccountsQuery();
            query.Select(query.ChartOfAccountId, query.ChartOfAccountCode, query.ChartOfAccountName);
            query.Where
                        (
                            query.Or
                            (
                                query.ChartOfAccountCode.Like(searchTextContain),
                                query.ChartOfAccountName.Like(searchTextContain)
                            )
                        );
            query.Where(query.IsDetail == 1);
            query.Where(query.IsActive == 1);
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboChartOfAccountIdAPParamedicFee.DataSource = dtb;
            cboChartOfAccountIdAPParamedicFee.DataBind();
        }

        protected void cboChartOfAccountIdAPParamedicFee_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ChartOfAccountCode"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["ChartOfAccountName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ChartOfAccountId"].ToString();
        }
        #endregion

        #region ComboBox SubledgerIdAPParamedicFee
        protected void cboSubledgerIdAPParamedicFee_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            int groupID;
            if (cboChartOfAccountIdAPParamedicFee.SelectedValue == string.Empty)
            {
                groupID = 0;
            }
            else
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                coa.LoadByPrimaryKey(Convert.ToInt32(cboChartOfAccountIdAPParamedicFee.SelectedValue));
                groupID = coa.SubLedgerId ?? 0;
            }
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new SubLedgersQuery();
            query.Select(query.SubLedgerId, query.SubLedgerName, query.Description);
            query.Where(query.GroupId == groupID);
            query.Where
                        (
                            query.Or
                            (
                                query.SubLedgerName.Like(searchTextContain),
                                query.Description.Like(searchTextContain)
                            )
                        );
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboSubledgerIdAPParamedicFee.DataSource = dtb;
            cboSubledgerIdAPParamedicFee.DataBind();
        }

        protected void cboSubledgerIdAPParamedicFee_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SubLedgerName"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["Description"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SubLedgerId"].ToString();
        }
        #endregion

        #region detail - Paramedic Fee Item
        private ParamedicFeeItemCollection ParamedicFeeItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collParamedicFeeItem"];
                    if (obj != null)
                    {
                        return ((ParamedicFeeItemCollection)(obj));
                    }
                }

                ParamedicFeeItemCollection coll = new ParamedicFeeItemCollection();
                ParamedicFeeItemQuery query = new ParamedicFeeItemQuery("a");
                ItemQuery vq = new ItemQuery("b");
                query.InnerJoin(vq).On(query.ItemID == vq.ItemID);
                query.Select
                    (
                        query,
                        vq.ItemName.As("refToItem_ItemName")
                    );
                query.Where(query.ParamedicID == txtParamedicID.Text);
                coll.Load(query);

                Session["collParamedicFeeItem"] = coll;
                return coll;
            }
            set
            {
                Session["collParamedicFeeItem"] = value;
            }
        }

        private void RefreshCommandItemParamedicFeeItem(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdParamedicFeeItem.Columns[0].Visible = isVisible;
            grdParamedicFeeItem.Columns[grdParamedicFeeItem.Columns.Count - 1].Visible = isVisible;
            grdParamedicFeeItem.Columns[grdParamedicFeeItem.Columns.Count - 2].Visible = isVisible;
            grdParamedicFeeItem.Columns[grdParamedicFeeItem.Columns.Count - 3].Visible = isVisible;

            grdParamedicFeeItem.MasterTableView.CommandItemDisplay = isVisible
                                                                             ? GridCommandItemDisplay.Top
                                                                             : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdParamedicFeeItem.Rebind();
        }

        private void PopulateParamedicFeeItemGrid()
        {
            //Display Data Detail
            ParamedicFeeItems = null; //Reset Record Detail
            grdParamedicFeeItem.DataSource = ParamedicFeeItems; //Requery
            grdParamedicFeeItem.MasterTableView.IsItemInserted = false;
            grdParamedicFeeItem.MasterTableView.ClearEditItems();
            grdParamedicFeeItem.DataBind();
        }

        private ParamedicFeeItem FindParamedicFeeItem(String itemID)
        {
            ParamedicFeeItemCollection coll = ParamedicFeeItems;
            ParamedicFeeItem retEntity = null;
            foreach (ParamedicFeeItem rec in coll)
            {
                if (rec.ItemID.Equals(itemID))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        protected void grdParamedicFeeItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdParamedicFeeItem.DataSource = ParamedicFeeItems;
        }

        protected void grdParamedicFeeItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String itemID =
                Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][ParamedicFeeItemMetadata.ColumnNames.ItemID]);
            ParamedicFeeItem entity = FindParamedicFeeItem(itemID);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdParamedicFeeItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String itemID =
                Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][ParamedicFeeItemMetadata.ColumnNames.ItemID]);
            ParamedicFeeItem entity = FindParamedicFeeItem(itemID);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdParamedicFeeItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            ParamedicFeeItem entity = ParamedicFeeItems.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdParamedicFeeItem.Rebind();
        }

        private void SetEntityValue(ParamedicFeeItem entity, GridCommandEventArgs e)
        {
            ParamedicFeeItemDetail userControl = (ParamedicFeeItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ItemID = userControl.ItemID;
                entity.ItemName = userControl.ItemName;
                entity.IsParamedicFeeUsePercentage = userControl.IsFeeUsingPercentage;
                entity.ParamedicFeeAmount = userControl.FeeAmount;
                entity.ParamedicFeeAmountReferral = userControl.FeeAmountReferral;
                entity.IsDeductionFeeUsePercentage = userControl.IsDeductionFeeUsePercentage;
                entity.DeductionFeeAmount = userControl.DeductionFeeAmount;
                entity.DeductionFeeAmountReferral = userControl.DeductionFeeAmountReferral;
            }
        }
        #endregion

        #region Record Detail Method Function of ParamedicAutoBillItem

        private ParamedicAutoBillItemCollection ParamedicAutoBillItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collParamedicAutoBillItem"];
                    if (obj != null)
                        return ((ParamedicAutoBillItemCollection)(obj));
                }

                ParamedicAutoBillItemCollection coll = new ParamedicAutoBillItemCollection();

                ParamedicAutoBillItemQuery query = new ParamedicAutoBillItemQuery("a");

                ItemQuery iq = new ItemQuery("b");
                query.InnerJoin(iq).On(query.ItemID == iq.ItemID);

                AppStandardReferenceItemQuery asriq = new AppStandardReferenceItemQuery("c");
                query.LeftJoin(asriq).On(query.SRItemUnit == asriq.ItemID &&
                                         asriq.StandardReferenceID == AppEnum.StandardReference.ItemUnit);

                var sq = new ServiceUnitQuery("x");
                query.InnerJoin(sq).On(query.ServiceUnitID == sq.ServiceUnitID);

                query.Where
                    (
                    query.ParamedicID == txtParamedicID.Text,
                    asriq.StandardReferenceID == AppEnum.StandardReference.ItemUnit
                    );
                query.OrderBy(query.ParamedicID.Ascending);

                query.Select
                    (
                    query,
                    iq.ItemName.As("refToItem_ItemName"),
                    asriq.ItemName.As("refToAppStandardReferenceItem_SRItemUnit"),
                    sq.ServiceUnitName.As("refToServiceUnit_ServiceUnitName")
                    );
                coll.Load(query);

                Session["collParamedicAutoBillItem"] = coll;
                return coll;
            }
            set { Session["collParamedicAutoBillItem"] = value; }
        }

        private void RefreshCommandItemServiceUnitAutoBillItem(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdServiceUnitAutoBillItem.Columns[0].Visible = isVisible;
            grdServiceUnitAutoBillItem.Columns[grdServiceUnitAutoBillItem.Columns.Count - 1].Visible = isVisible;

            grdServiceUnitAutoBillItem.MasterTableView.CommandItemDisplay = isVisible
                                                                                ? GridCommandItemDisplay.Top
                                                                                : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdServiceUnitAutoBillItem.Rebind();
        }

        private void PopulateServiceUnitAutoBillItemGrid()
        {
            //Display Data Detail
            ParamedicAutoBillItems = null; //Reset Record Detail
            grdServiceUnitAutoBillItem.DataSource = ParamedicAutoBillItems;
            grdServiceUnitAutoBillItem.MasterTableView.IsItemInserted = false;
            grdServiceUnitAutoBillItem.MasterTableView.ClearEditItems();
            grdServiceUnitAutoBillItem.DataBind();
        }

        protected void grdServiceUnitAutoBillItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdServiceUnitAutoBillItem.DataSource = ParamedicAutoBillItems;
        }

        protected void grdServiceUnitAutoBillItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            String serviceUnitID = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][ParamedicAutoBillItemMetadata.ColumnNames.ServiceUnitID]);
            String itemID = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][ParamedicAutoBillItemMetadata.ColumnNames.ItemID]);
            ParamedicAutoBillItem entity = FindItemGridOfServiceUnitAutoBillItem(serviceUnitID, itemID);
            if (entity != null)
            {
                entity.MarkAsDeleted();

                SetEntityValueOfParamedicAutoBillItem(ParamedicAutoBillItems.AddNew(), e);
            }
        }

        protected void grdServiceUnitAutoBillItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;

            String serviceUnitID = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][ParamedicAutoBillItemMetadata.ColumnNames.ServiceUnitID]);
            String itemID = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][ParamedicAutoBillItemMetadata.ColumnNames.ItemID]);
            ParamedicAutoBillItem entity = FindItemGridOfServiceUnitAutoBillItem(serviceUnitID, itemID);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdServiceUnitAutoBillItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            ParamedicAutoBillItem entity = ParamedicAutoBillItems.AddNew();
            SetEntityValueOfParamedicAutoBillItem(entity, e);
            //Stay in insert mode
            e.Canceled = true;
            grdServiceUnitAutoBillItem.Rebind();
        }

        private void SetEntityValueOfParamedicAutoBillItem(ParamedicAutoBillItem entity, GridCommandEventArgs e)
        {
            ParamedicAutoBillItemDetail userControl = (ParamedicAutoBillItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ParamedicID = txtParamedicID.Text;
                entity.ServiceUnitID = userControl.ServiceUnitID;
                entity.ServiceUnitName = userControl.ServiceUnitName;
                entity.ItemID = userControl.ItemID;
                entity.ItemName = userControl.ItemName;
                entity.Quantity = userControl.Quantity;
                entity.SRItemUnit = userControl.SRItemUnit;
                entity.ItemUnit = userControl.ItemUnit;
                entity.IsActive = userControl.IsActive;
                entity.IsGenerateOnRegistration = userControl.IsGenerateOnRegistration;
                entity.IsGenerateOnReferral = userControl.IsGenerateOnReferral;
            }
        }

        private ParamedicAutoBillItem FindItemGridOfServiceUnitAutoBillItem(string serviceUnitID, string itemID)
        {
            ParamedicAutoBillItemCollection coll = ParamedicAutoBillItems;
            return coll.FirstOrDefault(rec => rec.ParamedicID.Equals(txtParamedicID.Text) && rec.ServiceUnitID.Equals(serviceUnitID) && rec.ItemID.Equals(itemID));
        }

        #endregion

        #region detail - Paramedic Fee Guarantor Category
        private ParamedicFeeGuarantorCategoryCollection ParamedicFeeGuarantorCategorys
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collParamedicFeeGuarantorCategory"];
                    if (obj != null)
                    {
                        return ((ParamedicFeeGuarantorCategoryCollection)(obj));
                    }
                }

                var coll = new ParamedicFeeGuarantorCategoryCollection();
                var query = new ParamedicFeeGuarantorCategoryQuery("a");
                var std = new AppStandardReferenceItemQuery("b");
                query.InnerJoin(std).On(query.SRPhysicianFeeType == std.ItemID &&
                                        std.StandardReferenceID == AppEnum.StandardReference.PhysicianFeeType);
                query.Select
                    (
                        query,
                        std.ItemName.As("refToAppStandardReferenceItem_ItemName")
                    );
                query.Where(query.ParamedicID == txtParamedicID.Text);
                coll.Load(query);

                Session["collParamedicFeeGuarantorCategory"] = coll;
                return coll;
            }
            set
            {
                Session["collParamedicFeeGuarantorCategory"] = value;
            }
        }

        private void RefreshCommandItemParamedicFeeGuarantorCategory(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdFeeGuarantorCategory.Columns[0].Visible = isVisible;
            grdFeeGuarantorCategory.Columns[grdFeeGuarantorCategory.Columns.Count - 1].Visible = isVisible;
            grdFeeGuarantorCategory.Columns[grdFeeGuarantorCategory.Columns.Count - 2].Visible = isVisible;

            grdFeeGuarantorCategory.MasterTableView.CommandItemDisplay = isVisible
                                                                             ? GridCommandItemDisplay.Top
                                                                             : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdFeeGuarantorCategory.Rebind();
        }

        private void PopulateParamedicFeeGuarantorCategoryGrid()
        {
            //Display Data Detail
            ParamedicFeeGuarantorCategorys = null; //Reset Record Detail
            grdFeeGuarantorCategory.DataSource = ParamedicFeeGuarantorCategorys; //Requery
            grdFeeGuarantorCategory.MasterTableView.IsItemInserted = false;
            grdFeeGuarantorCategory.MasterTableView.ClearEditItems();
            grdFeeGuarantorCategory.DataBind();
        }

        private ParamedicFeeGuarantorCategory FindParamedicFeeGuarantorCategory(String feeType)
        {
            ParamedicFeeGuarantorCategoryCollection coll = ParamedicFeeGuarantorCategorys;
            ParamedicFeeGuarantorCategory retEntity = null;
            foreach (ParamedicFeeGuarantorCategory rec in coll)
            {
                if (rec.SRPhysicianFeeType.Equals(feeType))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        protected void grdFeeGuarantorCategory_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdFeeGuarantorCategory.DataSource = ParamedicFeeGuarantorCategorys;
        }

        protected void grdFeeGuarantorCategory_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String srPatientCategory =
                Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][ParamedicFeeGuarantorCategoryMetadata.ColumnNames.SRPhysicianFeeType]);
            ParamedicFeeGuarantorCategory entity = FindParamedicFeeGuarantorCategory(srPatientCategory);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdFeeGuarantorCategory_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String feeType =
                Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][ParamedicFeeGuarantorCategoryMetadata.ColumnNames.SRPhysicianFeeType]);
            ParamedicFeeGuarantorCategory entity = FindParamedicFeeGuarantorCategory(feeType);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdFeeGuarantorCategory_InsertCommand(object source, GridCommandEventArgs e)
        {
            ParamedicFeeGuarantorCategory entity = ParamedicFeeGuarantorCategorys.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdFeeGuarantorCategory.Rebind();
        }

        private void SetEntityValue(ParamedicFeeGuarantorCategory entity, GridCommandEventArgs e)
        {
            var userControl = (ParamedicFeeGuarantorCategoryDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.SRPhysicianFeeType = userControl.SRPhysicianFeeType;
                entity.PhysicianFeeType = userControl.PhysicianFeeType;
                entity.IsParamedicFeeUsePercentage = userControl.IsFeeUsingPercentage;
                entity.ParamedicFeeAmount = userControl.FeeAmount;
                entity.ParamedicFeeAmountReferral = userControl.FeeAmountReferral;
                entity.IsDeductionFeeUsePercentage = userControl.IsDeductionFeeUsePercentage;
                entity.DeductionFeeAmount = userControl.DeductionFeeAmount;
                entity.DeductionFeeAmountReferral = userControl.DeductionFeeAmountReferral;
            }
        }
        #endregion

        #region Record Detail Method Function ServiceUnitParamedic

        private class NewServiceUnitParamedicColl : ServiceUnitParamedic
        {
            public string RoomName { get; set; }
        }

        private ServiceUnitParamedicCollection ServiceUnitParamedics
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collServiceUnitParamedic"];
                    if (obj != null)
                    {
                        return ((ServiceUnitParamedicCollection)(obj));
                    }
                }

                var coll = new ServiceUnitParamedicCollection();
                var query = new ServiceUnitParamedicQuery("a");
                var suq = new ServiceUnitQuery("b");
                var srq = new ServiceRoomQuery("c");
                query.InnerJoin(suq).On(query.ServiceUnitID == suq.ServiceUnitID);
                query.LeftJoin(srq).On(query.DefaultRoomID == srq.RoomID);
                query.Select(query.SelectAllExcept(), suq.ServiceUnitName.As("refToServiceUnit_ServiceUnitName"), srq.RoomName.As("refToServiceRoom_RoomName"));

                query.Where(query.ParamedicID == txtParamedicID.Text);
                coll.Load(query);

                Session["collServiceUnitParamedic"] = coll;
                return coll;
            }
            set { Session["collServiceUnitParamedic"] = value; }
        }

        private void RefreshCommandItemServiceUnitParamedic(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdServiceUnitParamedic.Columns[0].Visible = isVisible;
            grdServiceUnitParamedic.Columns[grdServiceUnitParamedic.Columns.Count - 1].Visible = isVisible;
            grdServiceUnitParamedic.Columns[grdServiceUnitParamedic.Columns.Count - 2].Visible = !isVisible; //ScheduleTemplate

            grdServiceUnitParamedic.MasterTableView.CommandItemDisplay = isVisible
                                                                             ? GridCommandItemDisplay.Top
                                                                             : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdServiceUnitParamedic.Rebind();
        }

        private void PopulateServiceUnitParamedicGrid()
        {
            //Display Data Detail
            ServiceUnitParamedics = null; //Reset Record Detail
            grdServiceUnitParamedic.DataSource = ServiceUnitParamedics;
            grdServiceUnitParamedic.MasterTableView.IsItemInserted = false;
            grdServiceUnitParamedic.MasterTableView.ClearEditItems();
            grdServiceUnitParamedic.DataBind();
        }

        protected void grdServiceUnitParamedic_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdServiceUnitParamedic.DataSource = ServiceUnitParamedics;
        }

        protected void grdServiceUnitParamedic_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String unitId =
                Convert.ToString(
                    editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][
                        ServiceUnitParamedicMetadata.ColumnNames.ServiceUnitID]);
            ServiceUnitParamedic entity = FindServiceUnitParamedic(unitId);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdServiceUnitParamedic_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String unitId =
                Convert.ToString(
                    item.OwnerTableView.DataKeyValues[item.ItemIndex][
                        ServiceUnitParamedicMetadata.ColumnNames.ServiceUnitID]);
            ServiceUnitParamedic entity = FindServiceUnitParamedic(unitId);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdServiceUnitParamedic_InsertCommand(object source, GridCommandEventArgs e)
        {
            ServiceUnitParamedic entity = ServiceUnitParamedics.AddNew();
            SetEntityValue(entity, e);
            //Stay in insert mode
            e.Canceled = true;
            grdServiceUnitParamedic.Rebind();
        }

        private ServiceUnitParamedic FindServiceUnitParamedic(String unitId)
        {
            ServiceUnitParamedicCollection coll = ServiceUnitParamedics;
            return coll.FirstOrDefault(rec => rec.ServiceUnitID.Equals(unitId));
        }

        private void SetEntityValue(ServiceUnitParamedic entity, GridCommandEventArgs e)
        {
            var userControl = (ParamedicServiceUnitDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ParamedicID = txtParamedicID.Text;
                entity.ServiceUnitID = userControl.ServiceUnitID;
                entity.ServiceUnitName = userControl.ServiceUnitName;
                entity.DefaultRoomID = userControl.DefaultRoomID;
                entity.RoomName = userControl.DefaultRoomName;
                entity.IsUsingQue = userControl.IsUsingQue;
                entity.IsAcceptBPJS = userControl.IsAcceptBPJS;
                entity.IsAcceptNonBPJS = userControl.IsAcceptNonBPJS;
            }
        }

        #endregion

        #region Record Detail Method Function ParamedicBridging

        private ParamedicBridgingCollection ParamedicBridgings
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collParamedicBridging"];
                    if (obj != null) return ((ParamedicBridgingCollection)(obj));
                }

                ParamedicBridgingCollection coll = new ParamedicBridgingCollection();

                ParamedicBridgingQuery query = new ParamedicBridgingQuery("a");
                AppStandardReferenceItemQuery asri = new AppStandardReferenceItemQuery("b");

                query.Select(query, asri.ItemName.As("refToAppStandardReferenceItem_ItemName"));
                query.InnerJoin(asri).On(query.SRBridgingType == asri.ItemID && asri.StandardReferenceID == AppEnum.StandardReference.BridgingType.ToString());
                query.Where(query.ParamedicID == txtParamedicID.Text);
                coll.Load(query);

                Session["collParamedicBridging"] = coll;
                return coll;
            }
            set
            {
                Session["collParamedicBridging"] = value;
            }
        }

        private void RefreshCommandItemParamedicBridging(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdAliasName.Columns[0].Visible = isVisible;
            grdAliasName.Columns[grdAliasName.Columns.Count - 1].Visible = isVisible;

            grdAliasName.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Perbaharui tampilan dan data
            grdAliasName.Rebind();
        }

        private void PopulateParamedicBirdgingGrid()
        {
            //Display Data Detail
            ParamedicBridgings = null; //Reset Record Detail
            grdAliasName.DataSource = ParamedicBridgings; //Requery
            grdAliasName.MasterTableView.IsItemInserted = false;
            grdAliasName.MasterTableView.ClearEditItems();
            grdAliasName.DataBind();
        }

        protected void grdAliasName_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdAliasName.DataSource = ParamedicBridgings;
        }

        protected void grdAliasName_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String type = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][ParamedicBridgingMetadata.ColumnNames.SRBridgingType]);
            String id = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][ParamedicBridgingMetadata.ColumnNames.BridgingID]);

            var entity = FindParamedicBridging(type, id);
            if (entity != null) SetEntityValue(entity, e);
        }

        protected void grdAliasName_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String type = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][ParamedicBridgingMetadata.ColumnNames.SRBridgingType]);
            String id = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][ParamedicBridgingMetadata.ColumnNames.BridgingID]);

            var entity = FindParamedicBridging(type, id);
            if (entity != null) entity.MarkAsDeleted();
        }

        protected void grdAliasName_InsertCommand(object source, GridCommandEventArgs e)
        {
            var entity = ParamedicBridgings.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdAliasName.Rebind();
        }

        private ParamedicBridging FindParamedicBridging(String type, string id)
        {
            var coll = ParamedicBridgings;
            return coll.FirstOrDefault(rec => rec.SRBridgingType.Equals(type) && rec.BridgingID.Equals(id));
        }

        private void SetEntityValue(ParamedicBridging entity, GridCommandEventArgs e)
        {
            ParamedicAliasDetail userControl = (ParamedicAliasDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ParamedicID = txtParamedicID.Text;
                entity.SRBridgingType = userControl.BridgingType;
                entity.BridgingTypeName = userControl.BridgingTypeName;
                entity.BridgingID = userControl.BridgingID;
                entity.BridgingName = string.IsNullOrEmpty(userControl.BridgingName) ? txtParamedicName.Text : userControl.BridgingName;
                entity.IsActive = userControl.IsActive;
                entity.SpecialisticID = userControl.SpesialisticID;
                entity.SpesialisticName = userControl.SpesialisticName;
                entity.DutyType = userControl.DutyType;
            }
        }

        #endregion

        #region Record Detail Method Function ParamedicOtherType
        private ParamedicOtherTypeCollection ParamedicOtherTypes
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collParamedicOtherType"];
                    if (obj != null)
                    {
                        return ((ParamedicOtherTypeCollection)(obj));
                    }
                }

                var coll = new ParamedicOtherTypeCollection();
                var query = new ParamedicOtherTypeQuery("a");
                var std = new AppStandardReferenceItemQuery("b");
                query.InnerJoin(std).On(query.SRParamedicType == std.ItemID &&
                                        std.StandardReferenceID == AppEnum.StandardReference.ParamedicType);
                query.Select
                    (
                        query,
                        std.ItemName.As("refToAppStandardReferenceItem_ParamedicType")
                    );
                query.Where(query.ParamedicID == txtParamedicID.Text);
                coll.Load(query);

                Session["collParamedicOtherType"] = coll;
                return coll;
            }
            set
            {
                Session["collParamedicOtherType"] = value;
            }
        }

        private void RefreshCommandItemParamedicOtherType(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);

            grdPhysicianOtherType.Columns[grdPhysicianOtherType.Columns.Count - 2].Visible = isVisible;

            grdPhysicianOtherType.MasterTableView.CommandItemDisplay = isVisible
                                                                             ? GridCommandItemDisplay.Top
                                                                             : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdPhysicianOtherType.Rebind();
        }

        private void PopulateParamedicOtherTypeGrid()
        {
            //Display Data Detail
            ParamedicOtherTypes = null; //Reset Record Detail
            grdPhysicianOtherType.DataSource = ParamedicOtherTypes; //Requery
            grdPhysicianOtherType.MasterTableView.IsItemInserted = false;
            grdPhysicianOtherType.MasterTableView.ClearEditItems();
            grdPhysicianOtherType.DataBind();
        }

        private ParamedicOtherType FindParamedicOtherType(String id)
        {
            ParamedicOtherTypeCollection coll = ParamedicOtherTypes;
            ParamedicOtherType retEntity = null;
            foreach (ParamedicOtherType rec in coll)
            {
                if (rec.SRParamedicType.Equals(id))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        protected void grdPhysicianOtherType_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdPhysicianOtherType.DataSource = ParamedicOtherTypes;
        }

        protected void grdPhysicianOtherType_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String id =
                Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][ParamedicOtherTypeMetadata.ColumnNames.SRParamedicType]);
            ParamedicOtherType entity = FindParamedicOtherType(id);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdPhysicianOtherType_InsertCommand(object source, GridCommandEventArgs e)
        {
            ParamedicOtherType entity = ParamedicOtherTypes.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdPhysicianOtherType.Rebind();
        }

        private void SetEntityValue(ParamedicOtherType entity, GridCommandEventArgs e)
        {
            var userControl = (ParamedicOtherTypeDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.SRParamedicType = userControl.SRParamedicType;
                entity.ParamedicTypeName = userControl.ParamedicTypeName;
            }
        }
        #endregion

        #region Record Detail Method Function ParamedicTeam
        private ParamedicFeeByTeamCollection ParamedicTeams
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collParamedicTeam"];
                    if (obj != null)
                    {
                        return ((ParamedicFeeByTeamCollection)(obj));
                    }
                }

                var coll = new ParamedicFeeByTeamCollection();
                var query = new ParamedicFeeByTeamQuery("b");
                var parq = new ParamedicQuery("c");

                query.InnerJoin(parq).On(parq.ParamedicID == query.ParamedicMemberID);
                query.Select
                    (
                        query,
                        parq.ParamedicName.As("refToParamedic_ParamedicName")
                    );
                query.Where(query.ParamedicID == txtParamedicID.Text);
                coll.Load(query);

                Session["collParamedicTeam"] = coll;
                return coll;
            }
            set
            {
                Session["collParamedicTeam"] = value;
            }
        }

        private void RefreshCommandItemParamedicTeam(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);

            grdPhysicianTeam.Columns[grdPhysicianTeam.Columns.Count - 2].Visible = isVisible;

            grdPhysicianTeam.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            grdPhysicianTeam.Enabled = chkIsPhysicianTeam.Checked;

            //Perbaharui tampilan dan data
            grdPhysicianTeam.Rebind();
        }

        private void PopulateParamedicTeamGrid()
        {
            //Display Data Detail
            ParamedicTeams = null; //Reset Record Detail
            grdPhysicianTeam.DataSource = ParamedicTeams; //Requery
            grdPhysicianTeam.MasterTableView.IsItemInserted = false;
            grdPhysicianTeam.MasterTableView.ClearEditItems();
            grdPhysicianTeam.DataBind();
        }

        private ParamedicFeeByTeam FindParamedicTeam(String memberId)
        {
            ParamedicFeeByTeamCollection coll = ParamedicTeams;
            ParamedicFeeByTeam retEntity = null;
            foreach (ParamedicFeeByTeam rec in coll)
            {
                if (rec.ParamedicMemberID.Equals(memberId))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        protected void grdPhysicianTeam_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String memberId = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][ParamedicFeeByTeamMetadata.ColumnNames.ParamedicMemberID]);

            var entity = FindParamedicTeam(memberId);
            if (entity != null) SetEntityValue(entity, e);
        }

        protected void grdPhysicianTeam_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String memberId = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][ParamedicFeeByTeamMetadata.ColumnNames.ParamedicMemberID]);

            ParamedicFeeByTeam entity = FindParamedicTeam(memberId);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdPhysicianTeam_InsertCommand(object source, GridCommandEventArgs e)
        {
            ParamedicFeeByTeam entity = ParamedicTeams.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdPhysicianTeam.Rebind();
        }

        protected void grdPhysicianTeam_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdPhysicianTeam.DataSource = ParamedicTeams;
        }

        private void SetEntityValue(ParamedicFeeByTeam entity, GridCommandEventArgs e)
        {
            var userControl = (ParamedicFeeByTeamDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ParamedicMemberID = userControl.ParamedicMemberID;
                entity.ParamedicMemberName = userControl.ParamedicMemberName;
                entity.FeePercentage = userControl.FeePercentage;
            }
        }

        protected void chkIsPhysicianTeam_CheckedChanged(object sender, EventArgs e)
        {
            grdPhysicianTeam.Enabled = chkIsPhysicianTeam.Checked;
        }
        #endregion

        protected void cboCoorporateGradeID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new CoorporateGradeQuery();
            query.Select(query.CoorporateGradeID, query.CoorporateGradeLevel, query.CoorporateGradeMin, query.CoorporateGradeMax);
            query.Where(query.CoorporateGradeLevel.Like(searchTextContain));
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboCoorporateGradeID.DataSource = dtb;
            cboCoorporateGradeID.DataBind();
        }

        protected void cboCoorporateGradeID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["CoorporateGradeLevel"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["CoorporateGradeID"].ToString();
        }

        private void PopulateCboCoorporateGradeID(RadComboBox comboBox, int id)
        {
            var query = new CoorporateGradeQuery();
            query.Select(query.CoorporateGradeID, query.CoorporateGradeLevel, query.CoorporateGradeMin, query.CoorporateGradeMax);
            query.Where(query.CoorporateGradeID == id);

            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
            comboBox.SelectedValue = id.ToString();
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
        {
            base.RaisePostBackEvent(source, eventArgument);

            if (string.IsNullOrEmpty(eventArgument))
                return;

            if (eventArgument.Contains("AfterUpload"))
            {
                OnPopulateEntryControl(new string[] { });
            }
        }
    }
}
