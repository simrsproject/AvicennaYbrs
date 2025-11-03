using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using System.Data;
using System.Text.RegularExpressions;
using System.Collections;
using System.Data.SqlTypes;
using System.Text;
using System.Web.Services;
using Temiang.Avicenna.BusinessObject.Common;
using Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.Medication
{
    public partial class MedicationChangeConsumeMethod : BasePageDialogEntry
    {
        public int MedicationReceiveNo
        {
            get
            {
                return Request.QueryString["medrecno"].ToInt();
            }
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ElectronicMedicalRecord;

            // Program Fiture
            IsSingleRecordMode = true; //Save then close
            ToolBar.NavigationVisible = false;
            ToolBar.ApprovalUnApprovalVisible = false;
            ToolBar.VoidUnVoidVisible = false;
            ToolBar.PrintVisible = false;
            ToolBar.DeleteVisible = false;

            ToolBar.EditVisible = false;
            ToolBar.AddVisible = false;
            // -------------------

            this.Title = "Medication Change Consume Method";
            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboNewSRMedicationConsume, AppEnum.StandardReference.MedicationConsume);
            }
            
        }


        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region override method

        protected override void OnPopulateEntryControl(ValidateArgs args)
        {
            PopulateEntryControl();
        }

        private MedicationReceive PopulateEntryControl()
        {
            // Hanya tuk single entry
            var med = new MedicationReceive();
            med.LoadByPrimaryKey(MedicationReceiveNo);
            lblItemDescription.Text = med.ItemDescription;
            txtItemUnit.Text = med.SRConsumeUnit;

            // ItemProductConsumeUnitMatrix
            var ipm = new ItemProductMedic();
            ipm.LoadByPrimaryKey(med.ItemID);

            var consUnit = new ItemProductConsumeUnitMatrixQuery("a");
            var stdri = new AppStandardReferenceItemQuery("b");
            consUnit.InnerJoin(stdri).On(consUnit.SRConsumeUnit == stdri.ItemID & stdri.StandardReferenceID == AppEnum.StandardReference.DosageUnit.ToString());
            consUnit.Select(consUnit.SRConsumeUnit, stdri.ItemName);
            consUnit.Where(consUnit.ItemID == med.ItemID);
            consUnit.OrderBy(stdri.ItemName.Ascending);
            var dtb = consUnit.LoadDataTable();

            cboNewSRConsumeUnit.Items.Clear();

            var isExistBaseUnit = false;
            var isExistDosageUnit = false;
            foreach (DataRow row in dtb.Rows)
            {
                if (!isExistBaseUnit)
                {
                    if (ipm.SRItemUnit.Equals(row["SRConsumeUnit"].ToString()))
                        isExistBaseUnit = true;
                }

                if (!isExistDosageUnit)
                {
                    if (ipm.SRDosageUnit.Equals(row["SRConsumeUnit"].ToString()))
                        isExistBaseUnit = true;
                }

                cboNewSRConsumeUnit.Items.Add(new RadComboBoxItem(row["ItemName"].ToString(), row["SRConsumeUnit"].ToString()));
            }
            if (!isExistDosageUnit && !string.IsNullOrWhiteSpace(ipm.SRDosageUnit))
            {
                cboNewSRConsumeUnit.Items.Insert(0, new RadComboBoxItem(StandardReference.GetItemName(AppEnum.StandardReference.DosageUnit, ipm.SRDosageUnit), ipm.SRDosageUnit));
            }
            if (!isExistBaseUnit && !string.IsNullOrWhiteSpace(ipm.SRItemUnit) && ipm.SRDosageUnit != ipm.SRItemUnit)
            {
                cboNewSRConsumeUnit.Items.Insert(0, new RadComboBoxItem(StandardReference.GetItemName(AppEnum.StandardReference.DosageUnit, ipm.SRItemUnit) , ipm.SRItemUnit));
            }
            ComboBox.SelectedValue(cboNewSRConsumeUnit, med.SRConsumeUnit);

            //Consume Method
            var cm = new ConsumeMethod();
            if (cm.LoadByPrimaryKey(med.SRConsumeMethod))
                txtConsumeMethod.Text = string.Format("{0} ({1}) @{2} {3}", cm.SygnaText, cm.SRConsumeMethodName, med.ConsumeQty, med.SRConsumeUnit);

            txtNewConsumeQty.Text = med.ConsumeQtyInString;
            ComboBox.SelectedValue(cboNewSRMedicationConsume, med.SRMedicationConsume);


            var reg = new Registration();
            reg.LoadByPrimaryKey(med.RegistrationNo);
            txtBed.Text = reg.BedID;

            var pat = new Patient();
            pat.LoadByPrimaryKey(reg.PatientID);
            txtPatientName.Text = pat.PatientName;

            var su = new ServiceUnit();
            su.LoadByPrimaryKey(reg.ServiceUnitID);
            txtServiceUnitName.Text = su.ServiceUnitName;

            var room = new ServiceRoom();
            room.LoadByPrimaryKey(reg.RoomID);
            txtRoomName.Text = room.RoomName;

            return med;
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
        }

        protected override void OnMenuNewClick()
        {
            // Populate
            var med = PopulateEntryControl();

            // Overwrite
            var timeNow = (new DateTime()).NowAtSqlServer();

            // Ambil tgl terakhir setup
            var qr = new MedicationReceiveUsedQuery();
            qr.Where(qr.MedicationReceiveNo == MedicationReceiveNo);
            qr.OrderBy(qr.ScheduleDateTime.Descending);
            qr.es.Top = 1;
            var ent = new MedicationReceiveUsed();
            if (ent.Load(qr) && ent.ScheduleDateTime > timeNow)
            {
                timeNow = ent.ScheduleDateTime.Value;
            }

            txtRealizedTime.SelectedDate = timeNow;

            txtNote.Text = string.Empty;
            txtBalanceRealQty.Value = Convert.ToDouble(med.BalanceRealQty);
            txtRealizedBy.Text = AppSession.UserLogin.UserName;
            txtNote.Text = "Change Consume Method";
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            Save();
        }

        private void Save()
        {
            using (var tr = new esTransactionScope())
            {
                // Habiskan balance
                var ent = new MedicationReceiveUsed();
                ent.MedicationReceiveNo = MedicationReceiveNo;
                ent.SequenceNo = NewSequenceNo();
                ent.Qty = Convert.ToDecimal(txtBalanceRealQty.Value); // Habiskan balance

                var date = txtRealizedTime.SelectedDate.Value;
                ent.RealizedDateTime = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, 0);
                ent.RealizedByUserID = AppSession.UserLogin.UserID;

                ent.SetupDateTime = ent.RealizedDateTime;
                ent.SetupByUserID = ent.RealizedByUserID;

                ent.VerificationDateTime = ent.RealizedDateTime;
                ent.VerificationByUserID = ent.RealizedByUserID;

                ent.ScheduleDateTime = ent.RealizedDateTime;

                ent.Note = txtNote.Text;
                ent.IsNotConsume = true;
                ent.Save();

                // Create New MedicationReceive()
                CreateNewMedicationReceive(MedicationReceiveNo, ent.Qty??0);

                tr.Complete();
            }
        }

        private void CreateNewMedicationReceive(int sourceMedicationReceiveNo, decimal sourceBalanceQty)
        {
            var fromMedReceive = new MedicationReceive();
            if (!fromMedReceive.LoadByPrimaryKey(sourceMedicationReceiveNo)) return;

            // Convertion
            decimal oriConvertionfactor = 1;
            decimal newConvertionfactor = 1;

            var ipm = new ItemProductMedic();
            ipm.LoadByPrimaryKey(fromMedReceive.ItemID);

            if (fromMedReceive.SRConsumeUnit.Equals(ipm.SRItemUnit))
                oriConvertionfactor = 1;
            else if (fromMedReceive.SRConsumeUnit.Equals(ipm.SRDosageUnit))
                oriConvertionfactor = ipm.Dosage??1;
            else
            {
                var consUnitMatrix = new ItemProductConsumeUnitMatrix();
                if (consUnitMatrix.LoadByPrimaryKey(fromMedReceive.ItemID, ipm.SRItemUnit, fromMedReceive.SRConsumeUnit))
                    oriConvertionfactor = consUnitMatrix.ConversionFactor??1;
            }

            var newSRConsumeUnit = cboNewSRConsumeUnit.SelectedValue;
            if (newSRConsumeUnit.Equals(ipm.SRItemUnit))
                newConvertionfactor = 1;
            else if (newSRConsumeUnit.Equals(ipm.SRDosageUnit))
                newConvertionfactor = ipm.Dosage ?? 1;
            else
            {
                var consUnitMatrix = new ItemProductConsumeUnitMatrix();
                if (consUnitMatrix.LoadByPrimaryKey(fromMedReceive.ItemID, ipm.SRItemUnit, newSRConsumeUnit))
                    newConvertionfactor = consUnitMatrix.ConversionFactor ?? 1;
            }

            var balanceQty = (sourceBalanceQty / oriConvertionfactor) * newConvertionfactor;

            // Cek Therapy
            var existTherapy = new MedicationReceive();
            existTherapy.Query.Where(existTherapy.Query.RegistrationNo == fromMedReceive.RegistrationNo,
                existTherapy.Query.ItemID == fromMedReceive.ItemID,
                existTherapy.Query.SRConsumeMethod == cboNewConsumeMethod.SelectedValue,
                existTherapy.Query.ConsumeQtyInString == fromMedReceive.SRConsumeUnit,
                existTherapy.Query.SRConsumeUnit == cboNewSRConsumeUnit.SelectedValue
                );
            existTherapy.Query.es.Top = 1;
            var isImported = (existTherapy.Query.Load() && existTherapy.MedicationReceiveNo != null);
            if (isImported)
            {
                // Tambahkan ke balancenya
                existTherapy.ReceiveQty = existTherapy.ReceiveQty + balanceQty;
                existTherapy.BalanceQty = existTherapy.BalanceQty + balanceQty;
                existTherapy.BalanceRealQty = existTherapy.BalanceRealQty + balanceQty;
                existTherapy.Save();
            }
            else
            {
                // Buat terapi baru
               var ent = new MedicationReceive
                {
                    RegistrationNo = fromMedReceive.RegistrationNo,
                    MedicationReceiveNo = NewMedicationReceiveNo(),
                    BalanceQty = balanceQty,
                    BalanceRealQty = balanceQty,
                    ReceiveDateTime = txtRealizedTime.SelectedDate,
                    StartDateTime = txtRealizedTime.SelectedDate,
                    ItemID = fromMedReceive.ItemID,
                    ItemDescription = fromMedReceive.ItemDescription,
                    ReceiveQty = balanceQty,
                    ConsumeQtyInString = txtNewConsumeQty.Text,
                    ConsumeQty = Convert.ToDecimal(new BusinessObject.Common.Fraction(txtNewConsumeQty.Text)),
                    SRConsumeUnit = cboNewSRConsumeUnit.SelectedValue,
                    SRConsumeMethod = cboNewConsumeMethod.SelectedValue,
                    RefTransactionNo = fromMedReceive.RefTransactionNo,
                    RefSequenceNo = fromMedReceive.RefSequenceNo,
                    SRMedicationConsume = cboNewSRMedicationConsume.SelectedValue,
                    IsVoid = false,
                    IsContinue = true
                };
                ent.Save(); 
            }
        }

        private int NewMedicationReceiveNo()
        {
            var qr = new MedicationReceiveQuery("a");
            var fb = new MedicationReceive();
            qr.es.Top = 1;
            qr.OrderBy(qr.MedicationReceiveNo.Descending);

            if (fb.Load(qr))
            {
                return fb.MedicationReceiveNo.ToInt() + 1;
            }
            return 1;
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            Save();
        }

        protected override void OnMenuPrintClick(ValidateArgs args, string programID, PrintJobParameterCollection printJobParameters)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
        }

        protected override void OnMenuEditClick()
        {
        }

        protected override void OnMenuAuditLogClick(AuditLogFilter auditLogFilter)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuMovePrevClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuMoveNextClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnBeforeMenuNewClick(ValidateArgs args)
        {

        }
        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuUnVoidClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }
        protected override void OnMenuRejournalClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override string OnGetScriptToolBarNewClicking()
        {
            return string.Empty;
        }
        public override string OnGetScriptToolBarSaveClicking()
        {
            return string.Empty;
        }
        public override bool OnGetStatusMenuEdit()
        {
            return true;
        }

        public override bool OnGetStatusMenuDelete()
        {
            return true;
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return true;
        }

        public override bool OnGetStatusMenuVoid()
        {
            return true;
        }

        protected override void OnInitializeAjaxManager(RadAjaxManager ajaxManager)
        {
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }
        #endregion

        private int NewSequenceNo()
        {
            var qr = new MedicationReceiveUsedQuery("a");
            var fb = new MedicationReceiveUsed();
            qr.es.Top = 1;
            qr.Where(qr.MedicationReceiveNo == MedicationReceiveNo);
            qr.OrderBy(qr.SequenceNo.Descending);

            if (fb.Load(qr))
            {
                return fb.SequenceNo.ToInt() + 1;
            }
            return 1;
        }


        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (txtBalanceRealQty.Value == 0)
            {
                args.IsValid = false;
                ((CustomValidator) source).ErrorMessage = @"Balance must > 0";
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNewConsumeQty.Text) 
                || (new BusinessObject.Common.Fraction(txtNewConsumeQty.Text)) == Double.NaN 
                || (new BusinessObject.Common.Fraction(txtNewConsumeQty.Text)) == 0)
            {
                args.IsValid = false;
                ((CustomValidator) source).ErrorMessage = @"Consume Qty must > 0";
                return;
            }


            if (string.IsNullOrEmpty(cboNewConsumeMethod.SelectedValue))
            {
                args.IsValid = false;
                ((CustomValidator) source).ErrorMessage = @"Consume Method must selected";
                return;
            }

            // Therapy must different
            var med = new MedicationReceive();
            med.LoadByPrimaryKey(MedicationReceiveNo);

            if (med.SRConsumeMethod.Equals(cboNewConsumeMethod.SelectedValue)
                && med.ConsumeQtyInString.Equals(txtNewConsumeQty.Text.Trim())
                && med.SRConsumeUnit.Equals(cboNewSRConsumeUnit.SelectedValue))
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = "Consume Method not changed";
                return;
            }
        }
    }
}
