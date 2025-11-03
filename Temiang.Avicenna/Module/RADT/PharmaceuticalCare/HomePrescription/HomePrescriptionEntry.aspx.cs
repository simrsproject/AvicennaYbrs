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
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.PharmaceuticalCare
{
    /// <summary>
    /// Drug list for discharge patients education
    /// </summary>
    /// Create by: Handono
    /// Client Req: RSI
    /// -----------------------------------------------
    public partial class HomePrescriptionEntry : BasePageDialogEntry
    {
        public override string RegistrationNo => Request.QueryString["regno"];
        public override string PatientID => Request.QueryString["patid"];
        private string IdUpd => Request.QueryString["idupd"];

        protected void Page_Init(object sender, EventArgs e)
        {
            if (AppConstant.Program.PharmaceuticalCare.Equals(Request.QueryString["prgid"]))
                ProgramID = AppConstant.Program.PharmaceuticalCare;
            else
                ProgramID = AppConstant.Program.ElectronicMedicalRecord;

            //ProgramReferenceID = "HPE"; //HomePrescription and Education

            // Program Fiture
            IsMedicalRecordEntry = true; //Activate deadline edit & add
            ToolBar.NavigationVisible = false;
            ToolBar.ApprovalUnApprovalVisible = false;
            ToolBar.VoidUnVoidVisible = false;
            ToolBar.PrintVisible = true;
            ToolBar.DeleteVisible = false;

            ToolBar.EditVisible = true;
            ToolBar.AddVisible = false;
            // -------------------

            if (!IsPostBack)
            {
                var pat = new Patient();
                if (pat.LoadByPrimaryKey(PatientID))
                {
                    this.Title = "Drug List For Discharge Patients Education (" + pat.PatientName + ", MRN: " + pat.MedicalNo + ")";
                }


                // CustomReportList
                IsCustomReportList = true;

                var tbarPrint = ToolBarMenuPrint;
                tbarPrint.Buttons.Clear();

                var btn = new RadToolBarButton("Home Prescription Information")
                {
                    Value = string.Format("rpt_{0}", "XML.PC.0005")
                };
                tbarPrint.Buttons.Add(btn);
            }

        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }


        #region override method
        protected override void OnPopulateEntryControl(ValidateArgs args)
        {
            var hpHd = new HomePrescriptionHd();
            if (hpHd.LoadByPrimaryKey(RegistrationNo))
            {
                txtEduDateTime.SelectedDate = hpHd.EduDateTime;
                txtStartDateTime.SelectedDate = hpHd.StartDateTime;
                txtFinishDateTime.SelectedDate = hpHd.FinishDateTime;
                txtRecipientName.Text = hpHd.RecipientName;
                chkIsRecipientAsPatient.Checked = hpHd.IsRecipientAsPatient ?? false;
                chkIsNgt.Checked = hpHd.IsNgt ?? false;
                chkIsOralHygiene.Checked = hpHd.IsOralHygiene ?? false;
            }
        }
        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            // Apply readonly entry
            grdEntry.Rebind();

            if (oldVal != AppEnum.DataMode.Read && newVal == AppEnum.DataMode.Read)
                AjaxManager.ResponseScripts.Add("UpdateAlertIconOnParent('" + IdUpd + "', 0);");

        }
        protected override void OnMenuNewClick()
        {
        }
        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            Save(args);
        }

        private bool Save(ValidateArgs args)
        {
            using (var trans = new esTransactionScope())
            {
                // Header
                var hpHd = new HomePrescriptionHd();
                if (!hpHd.LoadByPrimaryKey(RegistrationNo))
                {
                    hpHd = new HomePrescriptionHd();
                    hpHd.RegistrationNo = RegistrationNo;
                    hpHd.EduByUserID = AppSession.UserLogin.UserID;
                }
                hpHd.EduDateTime = txtEduDateTime.SelectedDate;
                hpHd.StartDateTime = txtStartDateTime.SelectedDate;
                hpHd.FinishDateTime = txtFinishDateTime.SelectedDate;
                hpHd.RecipientName = txtRecipientName.Text;
                hpHd.IsRecipientAsPatient = chkIsRecipientAsPatient.Checked;
                hpHd.IsNgt = chkIsNgt.Checked;
                hpHd.IsOralHygiene = chkIsOralHygiene.Checked;
                hpHd.ServiceUnitID = String.Empty; //Lupa field ini untuk apa
                hpHd.Save();


                // Detail 
                var mr = new MedicationReceiveQuery("mr");
                var hp = new HomePrescriptionQuery("hp");
                hp.InnerJoin(mr).On(hp.MedicationReceiveNo == mr.MedicationReceiveNo);
                hp.Where(hp.Or(mr.RegistrationNo == RegistrationNo, mr.RegistrationNo == FromRegistrationNo));

                var coll = new HomePrescriptionCollection();
                coll.Load(hp);

                coll.MarkAllAsDeleted();

                foreach (GridDataItem item in grdEntry.MasterTableView.Items)
                {
                    var mrno = item.GetDataKeyValue("MedicationReceiveNo").ToInt();
                    //var reqForm = Request.Form[string.Format("chkOnOff_{0}", mrno)];
                    //var isSelect = reqForm != null && "on".Equals(reqForm.ToLower());

                    //if (isSelect)
                    //{
                    var homPres = coll.AddNew();

                    homPres.MedicationReceiveNo = mrno;
                    homPres.Morning = ((RadTextBox)item.FindControl("txtMorning")).Text;
                    homPres.Noon = ((RadTextBox)item.FindControl("txtNoon")).Text;

                    homPres.Afternoon = ((RadTextBox)item.FindControl("txtAfternoon")).Text;
                    homPres.Night = ((RadTextBox)item.FindControl("txtNight")).Text;
                    homPres.Note = ((RadTextBox)item.FindControl("txtNote")).Text;
                    homPres.Indication = ((RadTextBox)item.FindControl("txtIndication")).Text;

                    var mre = new MedicationReceive();
                    mre.LoadByPrimaryKey(mrno);

                    // Utk keperluan dicetakan
                    homPres.DrugName = DrugName(mre.RefTransactionNo, mre.RefSequenceNo, mre.ItemDescription);
                    //    }
                }

                coll.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
            return true;
        }

        #region drugName
        private string DrugName(string presNo, string seqNo, string itemdesc)
        {

            if (!string.IsNullOrEmpty(presNo))
            {
                var prescItem = new TransPrescriptionItem();
                if (prescItem.LoadByPrimaryKey(presNo, seqNo))
                {
                    if (prescItem.IsCompound ?? false)
                    {
                        return PrescriptionDrugNameCompound(prescItem);
                    }
                    else
                    {
                        return string.Format("{0} {1}", ItemName(prescItem), prescItem.Notes);
                    }
                }
            }

            return itemdesc;
        }

        private string PrescriptionDrugNameCompound(TransPrescriptionItem prescItem)
        {
            // Obat Racikan
            var sbItem = new StringBuilder();

            var emb = new Embalace();
            emb.LoadByPrimaryKey(prescItem.EmbalaceID);
            sbItem.AppendFormat("<span>{0} {1}</span>",
                ItemName(prescItem), prescItem.Notes);

            // Detil racikan
            var coll = new TransPrescriptionItemCollection();
            coll.Query.Where(coll.Query.PrescriptionNo == prescItem.PrescriptionNo, coll.Query.ParentNo == prescItem.SequenceNo);
            coll.Query.OrderBy(coll.Query.SequenceNo.Ascending);
            coll.LoadAll();

            foreach (var entChild in coll)
            {
                sbItem.AppendFormat("<br/><span style=\"padding-left:10px;\">• {0}</span>", ItemName(entChild));
            }
            return sbItem.ToString();
        }
        private static string ItemName(TransPrescriptionItem prescItem)
        {
            var item = new Item();
            string itemID;
            if (prescItem.ItemInterventionID != null && !string.IsNullOrEmpty(prescItem.ItemInterventionID.ToString()) && !prescItem.ItemID.Equals(prescItem.ItemInterventionID))
            {
                itemID = prescItem.ItemInterventionID;
            }
            else
            {
                itemID = prescItem.ItemID;
            }

            item.LoadByPrimaryKey(itemID);
            return item.ItemName;
        }

        #endregion

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            Save(args);
        }

        protected override void OnMenuPrintClick(ValidateArgs args, string programID, PrintJobParameterCollection printJobParameters)
        {
            if (programID == "XML.PC.0005")
            {
                printJobParameters.AddNew("p_RegistrationNo", RegistrationNo);
            }
        }

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
        }

        protected override void OnMenuEditClick()
        {
            if (txtEduDateTime.IsEmpty)
            {
                txtEduDateTime.SelectedDate = DateTime.Now;
                txtStartDateTime.SelectedDate = DateTime.Now;
                txtFinishDateTime.SelectedDate = DateTime.Now.AddMinutes(30);
            }
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

        #region Medication Reconciliaion
        protected void grdEntry_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdEntry.DataSource = MedicationReceiveHasDischargeReconDataTable(RegistrationNo, FromRegistrationNo, DataModeCurrent != AppEnum.DataMode.Read);
        }

        private DataTable MedicationReceiveHasDischargeReconDataTable(string registrationNo, string fromRegistrationNo, bool isEditMode)
        {
            var lastRecon = new MedicationRecon();
            lastRecon.Query.Where(lastRecon.Query.RegistrationNo == RegistrationNo, lastRecon.Query.ReconType == "DCG");
            lastRecon.Query.es.Top = 1;
            lastRecon.Query.OrderBy(lastRecon.Query.ReconSeqNo.Descending);
            if (!lastRecon.Query.Load()) return null;

            var query = new MedicationReceiveQuery("a");
            var cm = new ConsumeMethodQuery("cm");
            query.LeftJoin(cm).On(query.SRConsumeMethod == cm.SRConsumeMethod);

            var recdiscLine = new MedicationReconLineQuery("recl");
            query.InnerJoin(recdiscLine).On(query.MedicationReceiveNo == recdiscLine.MedicationReceiveNo);

            var hp = new HomePrescriptionQuery("hp");
            query.LeftJoin(hp).On(query.MedicationReceiveNo == hp.MedicationReceiveNo);

            query.Select(query.MedicationReceiveNo, query.ItemDescription, query.ConsumeQty, query.SRConsumeUnit, query.BalanceQty,
                hp.Morning, hp.Noon, hp.Afternoon, hp.Night, hp.Note, hp.Indication,
                cm.SRConsumeMethodName, query.IsBroughtHome, "<CONVERT(BIT,CASE WHEN COALESCE(hp.Morning,'0')='0' THEN 0 ELSE 1 END) as IsSelect>",
                hp.MedicationReceiveNo.As("PrNo"));

            query.Where(
                recdiscLine.RegistrationNo == RegistrationNo,
                 recdiscLine.ReconSeqNo == lastRecon.ReconSeqNo,
                 recdiscLine.ReconStatus != "ST",
                query.Or(query.IsVoid.IsNull(), query.IsVoid == false),
                query.BalanceQty > 0,
                query.IsContinue == true,
                query.Or(query.RegistrationNo == fromRegistrationNo, query.RegistrationNo == registrationNo));

            query.OrderBy(query.MedicationReceiveNo.Descending);
            var dtb = query.LoadDataTable();

            var isExist = false;
            foreach (DataRow row in dtb.Rows)
            {
                row["ItemDescription"] = row["ItemDescription"].ToString().Replace(Environment.NewLine, "<br>");
                if (!isExist)
                {
                    isExist = (row["PrNo"] != DBNull.Value);
                }
            }

            if (isEditMode && !isExist)
            {
                foreach (DataRow row in dtb.Rows)
                {
                    row["IsSelect"] = row["IsBroughtHome"];
                }
            }
            return dtb;
        }


        #endregion

    }
}
