using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Avicenna.Common;
using Temiang.Dal.Core;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.Charges.Billing
{
    public partial class IntermBillDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;

        private void PopulateNewNo()
        {
            _autoNumber = Helper.GetNewAutoNumber(txtIntermBillDate.SelectedDate.Value.Date, AppEnum.AutoNumber.IntermBill);
            txtIntermBillNo.Text = _autoNumber.LastCompleteNumber;
        }

        private void PopulateRegistrationNo()
        {
            var reg = new Registration();
            if (reg.LoadByPrimaryKey(txtRegistrationNo.Text))
            {
                var pat = new Patient();
                pat.LoadByPrimaryKey(reg.PatientID);
                txtMedicalNo.Text = pat.MedicalNo;
                txtPatientName.Text = pat.PatientName;
                var std = new AppStandardReferenceItem();
                txtSalutation.Text = std.LoadByPrimaryKey("Salutation", pat.SRSalutation) ? std.ItemName : string.Empty;
                txtGender.Text = pat.Sex;
                txtPlaceDOB.Text = string.Format("{0}, {1}", pat.CityOfBirth, Convert.ToDateTime(pat.DateOfBirth).ToString("dd-MMM-yyyy"));
                txtAgeInYear.Text = Convert.ToString(reg.AgeInYear);
                txtAgeInMonth.Text = Convert.ToString(reg.AgeInMonth);
                txtAgeInDay.Text = Convert.ToString(reg.AgeInDay);

                var su = new ServiceUnit();
                su.LoadByPrimaryKey(reg.ServiceUnitID);
                txtServiceUnitName.Text = su.ServiceUnitName;

                var sr = new ServiceRoom();
                sr.LoadByPrimaryKey(reg.RoomID);
                txtRoomName.Text = sr.RoomName;
                txtBedID.Text = reg.BedID;

                var cl = new Class();
                cl.LoadByPrimaryKey(reg.ChargeClassID);
                txtClassName.Text = cl.ClassName;
                cl = new Class();
                cl.LoadByPrimaryKey(reg.CoverageClassID);
                txtCoverageClassName.Text = cl.ClassName;
                var par = new Paramedic();
                par.LoadByPrimaryKey(reg.ParamedicID);
                txtParamedicName.Text = par.ParamedicName;

                var guar = new Guarantor();
                guar.LoadByPrimaryKey(reg.GuarantorID);
                txtGuarantorName.Text = guar.GuarantorName;
            }
            else
            {
                txtMedicalNo.Text = string.Empty;
                txtPatientName.Text = string.Empty;
                txtSalutation.Text = string.Empty;
                txtGender.Text = string.Empty;
                txtPlaceDOB.Text = string.Empty;
                txtAgeInYear.Text = string.Empty;
                txtAgeInMonth.Text = string.Empty;
                txtAgeInDay.Text = string.Empty;
                txtServiceUnitName.Text = string.Empty;
                txtRoomName.Text = string.Empty;
                txtBedID.Text = string.Empty;
                txtClassName.Text = string.Empty;
                txtCoverageClassName.Text = string.Empty;
                txtParamedicName.Text = string.Empty;
                txtGuarantorName.Text = string.Empty;
            }
        }

        private void InitializeCboFilterByServiceUnitID()
        {
            string[] regList = Helper.MergeBilling.GetMergeRegistration(Page.Request.QueryString["regno"]);

            var suQuery = new ServiceUnitQuery("a");
            var trQuery = new TransChargesQuery("b");
            suQuery.InnerJoin(trQuery).On(suQuery.ServiceUnitID == trQuery.ToServiceUnitID &&
                                            trQuery.RegistrationNo.In(regList));
            suQuery.es.Distinct = true;
            suQuery.Select(suQuery.ServiceUnitID, suQuery.ServiceUnitName);
            suQuery.Where
                (
                    suQuery.SRRegistrationType.In(
                        AppConstant.RegistrationType.ClusterPatient,
                        AppConstant.RegistrationType.EmergencyPatient,
                        AppConstant.RegistrationType.InPatient,
                        AppConstant.RegistrationType.OutPatient
                    ),
                    suQuery.IsActive == true
                );
            DataTable dtb1 = suQuery.LoadDataTable();

            var suQuery2 = new ServiceUnitQuery("a");
            var trQuery2 = new TransPrescriptionQuery("b");
            suQuery2.InnerJoin(trQuery2).On(suQuery2.ServiceUnitID == trQuery2.ServiceUnitID &&
                                          trQuery2.RegistrationNo.In(regList));
            suQuery2.es.Distinct = true;
            suQuery2.Select(suQuery2.ServiceUnitID, suQuery2.ServiceUnitName);
            suQuery2.Where(suQuery2.IsActive == true);
            DataTable dtb2 = suQuery2.LoadDataTable();

            DataTable tbl = dtb1;
            tbl.Merge(dtb2);

            cboFilterByServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (DataRow row in tbl.Rows)
            {
                cboFilterByServiceUnitID.Items.Add(new RadComboBoxItem(row["ServiceUnitName"].ToString(), row["ServiceUnitID"].ToString()));
            }
        }

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "#";
            UrlPageList = "IntermBillList.aspx?type=" + Request.QueryString["type"];

            switch (Request.QueryString["type"])
            {
                case "usr":
                    ProgramID = AppConstant.Program.IntermBill;
                    break;
                case "all":
                    ProgramID = AppConstant.Program.IntermBillAll;
                    break;
            }

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboFilterByItemType, AppEnum.StandardReference.ItemType);
                InitializeCboFilterByServiceUnitID();
            }
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            ToolBarMenuSearch.Enabled = false;
            ToolBarMenuAdd.Enabled = false;
            ToolBarMenuEdit.Enabled = false;
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(grdItem, txtPatientAmount);
            ajax.AddAjaxSetting(grdItem, txtGuarantorAmount);
            ajax.AddAjaxSetting(grdItem, txtPatientAdmAmount);
            ajax.AddAjaxSetting(grdItem, txtGuarantorAdmAmount);
            ajax.AddAjaxSetting(grdItem, grdItem);
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            string[] intermBillNoList = IntermBillList();
            string ib = string.Empty;

            foreach (var str in intermBillNoList)
            {
                ib += str + ",";
            }
            ib = ib.Substring(0, ib.Length - 1);

            string[] registrationNoList = MergeRegistrationList();
            string rn = string.Empty;

            foreach (var str in registrationNoList)
            {
                rn += str + ",";
            }
            rn = rn.Substring(0, rn.Length - 1);

            printJobParameters.AddNew("IntermBillNoList", ib);
            printJobParameters.AddNew("RegistrationNoList", rn);
            printJobParameters.AddNew("RegNo", txtRegistrationNo.Text);
            printJobParameters.AddNew("UserID", AppSession.UserLogin.UserID);
            printJobParameters.AddNew("UserName", AppSession.UserLogin.UserName);

            var r = new Registration();
            r.LoadByPrimaryKey(txtRegistrationNo.Text);

            printJobParameters.AddNew("Plafond", r.PlavonAmount.ToString());
            printJobParameters.AddNew("StartDate", Convert.ToDateTime("1900-01-01 00:00:00"));
            printJobParameters.AddNew("EndDate", Convert.ToDateTime("1900-01-01 00:00:00"));
            printJobParameters.AddNew("AskesGuarantor", AppSession.Parameter.SelfGuarantor);
            printJobParameters.AddNew("SelfGuarantor", string.Empty);// AppSession.Parameter.GuarantorAskesID);
        }

        protected override void OnMenuEditClick()
        {

        }

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            var entity = new IntermBill();
            if (entity.LoadByPrimaryKey(txtIntermBillNo.Text))
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

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            var entity = new IntermBill();

            if (!entity.LoadByPrimaryKey(txtIntermBillNo.Text))
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
            var entity = new IntermBill();

            if (!entity.LoadByPrimaryKey(txtIntermBillNo.Text))
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

            var py = new TransPaymentItemIntermBillQuery();
            py.Where(py.IntermBillNo == txtIntermBillNo.Text, py.IsPaymentProceed == true, py.IsPaymentReturned == false);
            if (py.LoadDataTable().Rows.Count > 0)
            {
                args.MessageText = "This transaction can't be canceled, this data has been proceed to payment process.";
                args.IsCancel = true;
                return;
            }

            SetApproval(entity, false, args);
        }

        private void SetApproval(IntermBill entity, bool isApproval, ValidateArgs args)
        {
            using (var trans = new esTransactionScope())
            {
                //header
                entity.IsApproved = isApproval;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;

                entity.Save();

                //Commit if success, Rollback if failed
                trans.Complete();

            }
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            var entity = new IntermBill();
            if (!entity.LoadByPrimaryKey(txtIntermBillNo.Text))
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

            var py = new TransPaymentItemIntermBillQuery();
            py.Where(py.IntermBillNo == txtIntermBillNo.Text, py.IsPaymentProceed == true, py.IsPaymentReturned == false);
            if (py.LoadDataTable().Rows.Count > 0)
            {
                args.MessageText = "This transaction can't be canceled, this data has been proceed to payment process.";
                args.IsCancel = true;
                return;
            }

            SetVoid(entity, true);
        }

        private void SetVoid(IntermBill entity, bool isVoid)
        {
            //header
            entity.IsVoid = isVoid;
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = DateTime.Now;

            foreach (CostCalculation item in CostCalculations)
            {
                item.IntermBillNo = null;
            }

            //--- hitung ulang intermbill
            double patAdm = 0;
            double guarAdm = 0;
            var ibcoll = new IntermBillCollection();
            ibcoll.Query.Where(ibcoll.Query.RegistrationNo == txtRegistrationNo.Text,
                               ibcoll.Query.IsVoid == false, ibcoll.Query.IntermBillNo != txtIntermBillNo.Text);
            ibcoll.LoadAll();
            if (ibcoll.Count > 0)
            {
                foreach (var ib in ibcoll)
                {
                    var cc = new CostCalculationCollection();
                    cc.Query.Where(cc.Query.IntermBillNo == ib.IntermBillNo);
                    cc.LoadAll();
                    if (cc.Count > 0)
                    {
                        patAdm += Convert.ToDouble(ib.AdministrationAmount);
                        guarAdm += Convert.ToDouble(ib.GuarantorAdministrationAmount);
                    }
                }
            }

            var reg = new Registration();
            reg.LoadByPrimaryKey(entity.RegistrationNo);
            reg.AdministrationAmount = Convert.ToDecimal(patAdm + guarAdm);
            reg.PatientAdm = Convert.ToDecimal(patAdm);
            reg.GuarantorAdm = Convert.ToDecimal(guarAdm);

            using (var trans = new esTransactionScope())
            {
                entity.Save();
                CostCalculations.Save();
                reg.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnMenuUnVoidClick(ValidateArgs args)
        {
            var entity = new IntermBill();
            if (!entity.LoadByPrimaryKey(txtIntermBillNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }

            SetVoid(entity, false);
        }

        private bool IsApprovedOrVoid(IntermBill entity, ValidateArgs args)
        {
            if (entity.IsApproved.Value)
            {
                args.MessageText = AppConstant.Message.RecordHasApproved;
                args.IsCancel = true;
                return false;
            }

            if (entity.IsVoid.Value)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return false;
            }
            return true;
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new IntermBill());
            txtIntermBillDate.SelectedDate = DateTime.Now.Date;
            PopulateNewNo();
            txtStartDate.SelectedDate = DateTime.Now.Date;
            txtEndDate.SelectedDate = DateTime.Now.Date;
            txtRegistrationNo.Text = Request.QueryString["regno"];
            txtPatientAmount.Value = 0;
            txtGuarantorAmount.Value = 0;
            txtPatientAdmAmount.Value = 0;
            txtGuarantorAdmAmount.Value = 0;
            btnGetItem.Enabled = true;
            btnReset.Enabled = true;
            PopulateRegistrationNo();
            cboFilterByItemType.Text = string.Empty;
            cboFilterByItemType.SelectedValue = string.Empty;
            cboFilterByServiceUnitID.Text = string.Empty;
            cboFilterByServiceUnitID.SelectedValue = string.Empty;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new IntermBill();
            if (entity.LoadByPrimaryKey(txtIntermBillNo.Text))
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
            PopulateNewNo();
            var entity = new IntermBill();
            if (entity.LoadByPrimaryKey(txtIntermBillNo.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }
            entity = new IntermBill();
            entity.AddNew();
            SetEntityValue(entity);
            if (CostCalculations.Count == 0)
            {
                args.MessageText = AppConstant.Message.RecordDetailEmpty;
                args.IsCancel = true;
                return;
            }
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new IntermBill();
            if (entity.LoadByPrimaryKey(txtIntermBillNo.Text))
            {
                SetEntityValue(entity);
                if (CostCalculations.Count == 0)
                {
                    args.MessageText = AppConstant.Message.RecordDetailEmpty;
                    args.IsCancel = true;
                    return;
                }
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
            auditLogFilter.PrimaryKeyData = string.Format("IntermBillNo='{0}'", txtIntermBillNo.Text.Trim());
            auditLogFilter.TableName = "IntermBill";
        }

        #endregion

        #region ToolBar Menu Support

        public override bool OnGetStatusMenuEdit()
        {
            return txtIntermBillNo.Text != string.Empty;
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return !chkIsApproved.Checked;
        }

        public override bool OnGetStatusMenuVoid()
        {
            return !chkIsVoid.Checked;
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            btnGetItem.Enabled = newVal != AppEnum.DataMode.Read;
            btnReset.Enabled = newVal != AppEnum.DataMode.Read;
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new IntermBill();
            if (parameters.Length > 0)
            {
                String intermBillNo = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(intermBillNo);
            }
            else
            {
                entity.LoadByPrimaryKey(txtIntermBillNo.Text);
            }

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var ib = (IntermBill)entity;
            txtIntermBillNo.Text = ib.IntermBillNo;
            txtIntermBillDate.SelectedDate = ib.IntermBillDate;
            txtRegistrationNo.Text = ib.RegistrationNo;
            txtStartDate.SelectedDate = ib.StartDate;
            txtEndDate.SelectedDate = ib.EndDate;
            txtPatientAmount.Value = Convert.ToDouble(ib.PatientAmount);
            txtGuarantorAmount.Value = Convert.ToDouble(ib.GuarantorAmount);
            txtPatientAdmAmount.Value = Convert.ToDouble(ib.AdministrationAmount);
            txtGuarantorAdmAmount.Value = Convert.ToDouble(ib.GuarantorAdministrationAmount);

            chkIsVoid.Checked = ib.IsVoid ?? false;
            chkIsApproved.Checked = ib.IsApproved ?? false;

            //Display Data Detail
            PopulateGridDetail();
            PopulateRegistrationNo();
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(IntermBill entity)
        {
            entity.IntermBillNo = txtIntermBillNo.Text;
            entity.IntermBillDate = txtIntermBillDate.SelectedDate;
            entity.RegistrationNo = txtRegistrationNo.Text;
            entity.StartDate = txtStartDate.SelectedDate;
            entity.EndDate = txtEndDate.SelectedDate;
            entity.PatientAmount = Convert.ToDecimal(txtPatientAmount.Value);
            entity.GuarantorAmount = Convert.ToDecimal(txtGuarantorAmount.Value);
            entity.AdministrationAmount = Convert.ToDecimal(txtPatientAdmAmount.Value);
            entity.GuarantorAdministrationAmount = Convert.ToDecimal(txtGuarantorAdmAmount.Value);
            entity.DiscAdmPatient = 0;
            entity.DiscAdmGuarantor = 0;
            entity.IsVoid = false;
            entity.IsApproved = false;
            entity.AskesCoveredSeqNo = string.Empty;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }

            //Update Detil
            foreach (CostCalculation item in CostCalculations)
            {
                item.IntermBillNo = txtIntermBillNo.Text;
            }
        }

        private void SaveEntity(IntermBill entity)
        {
            var reg = new Registration();
            reg.LoadByPrimaryKey(txtRegistrationNo.Text);
            reg.AdministrationAmount += (entity.AdministrationAmount + entity.GuarantorAdministrationAmount);
            reg.PatientAdm += entity.AdministrationAmount;
            reg.GuarantorAdm += entity.GuarantorAdministrationAmount;

            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                CostCalculations.Save();
                reg.Save();

                if (DataModeCurrent == AppEnum.DataMode.New)
                    _autoNumber.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new IntermBillQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.IntermBillNo > txtIntermBillNo.Text);
                que.OrderBy(que.IntermBillNo.Ascending);
            }
            else
            {
                que.Where(que.IntermBillNo < txtIntermBillNo.Text);
                que.OrderBy(que.IntermBillNo.Descending);
            }
            var entity = new IntermBill();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #endregion

        #region Method & Event TextChanged

        private void CalculateAmount()
        {
            decimal? totalPatient = 0;
            decimal? totalGuarantor = 0;
            decimal? tdiscount = 0;
            decimal? tdiscount2 = 0;

            if (CostCalculations.Count > 0)
            {
                foreach (CostCalculation item in CostCalculations)
                {
                    if (item.IntermBillNo != null)
                    {
                        totalPatient += (item.PatientAmount);
                        totalGuarantor += (item.GuarantorAmount);
                        tdiscount += (item.DiscountAmount);
                        tdiscount2 += (item.DiscountAmount2);
                    }
                }
            }

            totalPatient = Convert.ToDecimal(string.Format("{0:n2}", (totalPatient)));
            totalGuarantor = Convert.ToDecimal(string.Format("{0:n2}", (totalGuarantor)));
            txtPatientAmount.Value = Convert.ToDouble(totalPatient);
            txtGuarantorAmount.Value = Convert.ToDouble(totalGuarantor);

            //--- update nilai biaya admin
            var reg = new Registration();
            reg.LoadByPrimaryKey(txtRegistrationNo.Text);

            var grr = new Guarantor();
            grr.LoadByPrimaryKey(reg.GuarantorID);

            decimal admin = 0, admingr = 0;
            if (grr.IsIncludeAdminValue ?? false)
            {
                if (grr.IsAdminFromTotal ?? false)
                {
                    if (grr.IsAdminCalcBeforeDiscount ?? false)
                    {
                        if (grr.SRGuarantorType == AppSession.Parameter.GuarantorTypeSelf)
                        {
                            admin = Helper.CostCalculation.GetAdminValue(reg.GuarantorID,
                                                                         (totalPatient ?? 0) + (tdiscount ?? 0),
                                                                         reg.SRRegistrationType);
                            admingr = Helper.CostCalculation.GetAdminValue(reg.GuarantorID, totalGuarantor ?? 0,
                                                                           reg.SRRegistrationType);
                        }
                        else
                        {
                            admin = Helper.CostCalculation.GetAdminValue(reg.GuarantorID,
                                                                         (totalPatient ?? 0) + (tdiscount2 ?? 0),
                                                                         reg.SRRegistrationType);
                            admingr = Helper.CostCalculation.GetAdminValue(reg.GuarantorID,
                                                                           (totalGuarantor ?? 0) + (tdiscount ?? 0),
                                                                           reg.SRRegistrationType);
                        }
                    }
                    else
                    {
                        admin = Helper.CostCalculation.GetAdminValue(reg.GuarantorID, (totalPatient ?? 0),
                                                                     reg.SRRegistrationType);
                        admingr = Helper.CostCalculation.GetAdminValue(reg.GuarantorID, (totalGuarantor ?? 0),
                                                                       reg.SRRegistrationType);
                    }
                }
                else
                {
                    admin = Helper.CostCalculation.GetAdminValue(reg.GuarantorID, CostCalculations,
                                                                 reg.SRRegistrationType, true);
                    admingr = Helper.CostCalculation.GetAdminValue(reg.GuarantorID, CostCalculations,
                                                                   reg.SRRegistrationType, false);
                }
                if (grr.IsCoverAllAdminCosts ?? false)
                {
                    admingr += admin;
                    admin = 0;
                }
            }
            else
            {
                if (grr.IsAdminFromTotal ?? false)
                {
                    if (grr.IsAdminCalcBeforeDiscount ?? false)
                        admin = Helper.CostCalculation.GetAdminValue(reg.GuarantorID,
                                                                     (totalPatient ?? 0) + (totalGuarantor ?? 0) +
                                                                     (tdiscount ?? 0) + (tdiscount2 ?? 0),
                                                                     reg.SRRegistrationType);
                    else
                        admin = Helper.CostCalculation.GetAdminValue(reg.GuarantorID,
                                                                     (totalPatient ?? 0) + (totalGuarantor ?? 0),
                                                                     reg.SRRegistrationType);
                }
                else
                {
                    admin = Helper.CostCalculation.GetAdminValue(reg.GuarantorID, CostCalculations,
                                                                 reg.SRRegistrationType);
                }
            }

            admin = Convert.ToDecimal(string.Format("{0:n2}", (admin)));
            admingr = Convert.ToDecimal(string.Format("{0:n2}", (admingr)));

            if (admin + admingr != 0)
            {
                var tpat = Helper.Rounding(Convert.ToDecimal(admin) + Convert.ToDecimal(txtPatientAmount.Value), AppEnum.RoundingType.GlobalTransaction);
                var tguar = Helper.Rounding(Convert.ToDecimal(admingr) + Convert.ToDecimal(txtGuarantorAmount.Value), AppEnum.RoundingType.GlobalTransaction);

                admin += tpat - (admin + Convert.ToDecimal(txtPatientAmount.Value));
                admingr += tguar - (admingr + Convert.ToDecimal(txtGuarantorAmount.Value));
            }

            txtPatientAdmAmount.Value = Convert.ToDouble(admin);
            txtGuarantorAdmAmount.Value = Convert.ToDouble(admingr);
        }

        #endregion

        #region Record Detail Method Function

        #region Cost Calculation
        private CostCalculationCollection CostCalculations
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["IntermBill:collCostCalculation" + Request.UserHostName];
                    if (obj != null)
                        return ((CostCalculationCollection)(obj));
                }

                var coll = new CostCalculationCollection();

                var query = new CostCalculationQuery("a");
                var transQ = new VwTransactionQuery("b");
                var itemQ = new ItemQuery("c");
                var suQ = new ServiceUnitQuery("d");

                query.InnerJoin(transQ).On(query.TransactionNo == transQ.TransactionNo);
                query.InnerJoin(itemQ).On(query.ItemID == itemQ.ItemID);
                query.InnerJoin(suQ).On(transQ.ServiceUnitID == suQ.ServiceUnitID);

                query.Where(query.IntermBillNo == txtIntermBillNo.Text);

                query.Select
                    (
                        query,
                        itemQ.ItemName.As("refToItem_ItemName"),
                        transQ.TransactionDate.As("refToTransaction_TransactionDate"),
                        suQ.ServiceUnitName.As("refToServiceUnit_ServiceUnitName")
                    );
                query.OrderBy(suQ.ServiceUnitName.Ascending, transQ.TransactionDate.Ascending);

                coll.Load(query);

                Session["IntermBill:collCostCalculation" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["IntermBill:collCostCalculation" + Request.UserHostName] = value; }
        }

        private void PopulateGridDetail()
        {
            //Display Data Detail
            CostCalculations = null; //Reset Record Detail
            grdItem.DataSource = CostCalculations;
            grdItem.MasterTableView.IsItemInserted = false;
            grdItem.MasterTableView.ClearEditItems();
            grdItem.DataBind();
        }

        protected void grdItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItem.DataSource = CostCalculations;
        }

        private CostCalculation FindItemCostCalculation(String sequenceNo, String transactionNo)
        {
            CostCalculationCollection coll = CostCalculations;
            CostCalculation retEntity = null;
            foreach (CostCalculation rec in coll)
            {
                if ((rec.SequenceNo.Equals(sequenceNo)) && (rec.TransactionNo.Equals(transactionNo)))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        protected void grdItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String sequenceNo = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][CostCalculationMetadata.ColumnNames.SequenceNo]);
            String transactionNo = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][CostCalculationMetadata.ColumnNames.TransactionNo]);

            CostCalculation entity = FindItemCostCalculation(sequenceNo, transactionNo);
            if (entity != null)
                entity.IntermBillNo = null;

            grdItem.Rebind();

            CalculateAmount();
        }
        #endregion

        #endregion

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (!(sourceControl is RadGrid))
                return;

            if (eventArgument == "rebind")
            {
                if ((sourceControl as RadGrid).ID == grdItem.ID)
                {
                    CalculateAmount();

                    foreach (var entity in CostCalculations)
                    {
                        var cc = new CostCalculation();
                        cc.LoadByPrimaryKey(entity.RegistrationNo, entity.TransactionNo, entity.SequenceNo);

                        var item = new Item();
                        item.LoadByPrimaryKey(cc.ItemID);
                        entity.ItemName = item.ItemName;

                        string unitId;
                        var transCharges = new TransCharges();
                        if (transCharges.LoadByPrimaryKey(entity.TransactionNo))
                        {
                            unitId = transCharges.ToServiceUnitID;
                            entity.TransactionDate = transCharges.TransactionDate;
                        }
                        else
                        {
                            var transPresc = new TransPrescription();
                            transPresc.LoadByPrimaryKey(entity.TransactionNo);
                            unitId = transPresc.ServiceUnitID;
                            entity.TransactionDate = transPresc.PrescriptionDate;
                        }
                        var su = new ServiceUnit();
                        su.LoadByPrimaryKey(unitId);
                        entity.ServiceUnitName = su.ServiceUnitName;
                    }

                    grdItem.Rebind();
                }
            }
        }

        private string[] IntermBillList()
        {
            var arr = new string[1];
            arr.SetValue(txtIntermBillNo.Text, 0);

            return arr;
        }

        private string[] MergeRegistrationList()
        {
            if (ViewState["IntermBill:MergeRegistration" + Request.UserHostName] == null)
                ViewState["IntermBill:MergeRegistration" + Request.UserHostName] = Helper.MergeBilling.GetMergeRegistration(txtRegistrationNo.Text);

            return (string[])ViewState["IntermBill:MergeRegistration" + Request.UserHostName];
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            if (CostCalculations.Count > 0)
                CostCalculations.MarkAllAsDeleted();
            grdItem.DataSource = CostCalculations;
            grdItem.DataBind();

            txtPatientAmount.Value = 0;
            txtGuarantorAmount.Value = 0;
            txtPatientAdmAmount.Value = 0;
            txtGuarantorAdmAmount.Value = 0;
        }
    }
}
