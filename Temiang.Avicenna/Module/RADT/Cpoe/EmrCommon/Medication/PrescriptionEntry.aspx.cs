using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Interfaces;
using DevExpress.PivotGrid.OLAP.AdoWrappers;

namespace Temiang.Avicenna.Module.RADT.Emr
{
    public partial class PrescriptionEntry : BasePageDialogHistEntry
    {
        protected string RegistrationNo
        {
            get
            {
                return Request.QueryString["regno"];
            }
        }
        protected string GuarantorID
        {
            get
            {
                return RegistrationCurrent.GuarantorID;
            }
        }
        private string ParamedicID
        {
            get
            {
                return Request.QueryString["parid"];
            }
        }

        private string _abrForLine = null;
        protected string AntibioticRestrictionForLine
        {
            get
            {
                if (_abrForLine == null)
                    _abrForLine = AppParameter.GetParameterValue(AppParameter.ParameterItem.AntibioticRestrictionForLine);

                return _abrForLine;
            }
        }

        private Registration _registration;
        protected Registration RegistrationCurrent
        {
            get
            {
                if (_registration == null)
                {
                    _registration = new Registration();
                    _registration.LoadByPrimaryKey(RegistrationNo);

                }

                return _registration;
            }
        }

        protected bool IsFornas
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(GuarantorID))
                {
                    var guar = new Guarantor();
                    if (guar.LoadByPrimaryKey(GuarantorID))
                        return guar.IsItemRestrictionsFornas ?? false;
                    else
                        return false;
                }
                return false;
            }
        }

        private string _patientID;
        protected string PatientID
        {
            get
            {
                // Jangan ambil dari QueryString krn bisa jadi utk PatientID yg berbeda tetapi masih pasien yg sama (PatientRelated)
                //return Request.QueryString["patid"];
                if (!string.IsNullOrEmpty(RegistrationNo) && string.IsNullOrEmpty(_patientID))
                {
                    _patientID = RegistrationCurrent.PatientID;
                }

                return _patientID;
            }
        }

        #region override method

        protected override void OnPopulateEntryControl(ValidateArgs args)
        {
            txtPrescriptionNo.Text = Request.QueryString["presno"];
            var header = new TransPrescription();

            if (!string.IsNullOrEmpty(txtPrescriptionNo.Text) && header.LoadByPrimaryKey(txtPrescriptionNo.Text))
            {
                // Raspro info
                if (pnlRaspro.Visible)
                {
                    hdnRasproSeqNo.Value = (header.RasproSeqNo ?? 0).ToString();
                    var rr = new RegistrationRaspro();
                    if (rr.LoadByPrimaryKey(RegistrationNo, hdnRasproSeqNo.Value.ToInt()))
                    {
                        hdnRasproRefNo.Value = rr.ReferenceNo; //Exam Order Lab Tr No (Rspro Culture)
                        hdnRasproType.Value = rr.SRRaspro;
                    }
                }
            }

            txtPrescriptionDate.SelectedDate = header.PrescriptionDate;

            cboServiceUnitID.SelectedValue = string.IsNullOrEmpty(header.ServiceUnitID) ? AppSession.Parameter.ServiceUnitPharmacyID : header.ServiceUnitID;
            if (!string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
            {
                ComboBox.PopulateWithServiceUnitForLocation(cboLocationID, cboServiceUnitID.SelectedValue);
                if (!string.IsNullOrEmpty(header.LocationID))
                    cboLocationID.SelectedValue = header.LocationID;
                else cboLocationID.SelectedIndex = 1;
            }
            chkIsCitoPresc.Checked = header.IsCito ?? false;
            txtNotesPresc.Text = header.Note;
            txtQtyR.Value = header.QtyR;
            chkIsUnitDosePresc.Checked = header.IsUnitDosePrescription ?? false;
            ComboBox.SelectedValue(cboSRPrescriptionCategory, header.SRPrescriptionCategory);

            tblTemporaryBill.Visible = false;
            var reg = new Registration();
            if (reg.LoadByPrimaryKey(RegistrationNo))
            {
                var grr = new Guarantor();
                if (grr.LoadByPrimaryKey(reg.GuarantorID))
                {
                    tblTemporaryBill.Visible = reg.SRRegistrationType == AppConstant.RegistrationType.OutPatient && grr.SRGuarantorType == AppSession.Parameter.GuarantorTypeBPJS;
                }
            }

            if (tblTemporaryBill.Visible)
            {
                txtTemporaryBillPlafond.Value = Convert.ToDouble(reg.PlavonAmount);
                txtTemporaryBillTotal.Value = Convert.ToDouble(GetTotalTemporaryBill());
            }
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            RefreshMenuRaspro(newVal);
            cboServiceUnitID.Enabled = (newVal == AppEnum.DataMode.New);
            if (AppSession.Parameter.IsLockLocationPharmacy)
            {
                cboServiceUnitID.Enabled = false;
                cboLocationID.Enabled = false;
            }

            if (pnlRaspro.Visible == true && newVal == AppEnum.DataMode.Read)
            {
                // Refresh info raspro setelah user klik cancel
                var rr = new RegistrationRaspro();
                rr.Query.es.Top = 1;
                rr.Query.OrderBy(rr.Query.SeqNo.Descending);
                rr.Query.Where(rr.Query.RegistrationNo == RegistrationNo);
                if (rr.Query.Load())
                {
                    var usedRasproSeqno = 0;
                    litAntibioticSuggest.Text = AbRestriction.AntibioticSuggestion(rr, ref usedRasproSeqno);
                }
                else
                    litAntibioticSuggest.Text = String.Empty;
            }

            if (newVal == AppEnum.DataMode.Read)
            {
                grdTransPrescriptionItem.EditIndexes.Clear();
                grdTransPrescriptionItem.MasterTableView.IsItemInserted = false;
                grdTransPrescriptionItem.MasterTableView.ClearEditItems();
            }

            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdTransPrescriptionItem.Columns[0].Visible = isVisible;
            grdTransPrescriptionItem.Columns[grdTransPrescriptionItem.Columns.Count - 2].Visible = isVisible;

            grdTransPrescriptionItem.MasterTableView.CommandItemDisplay = isVisible
                                                                             ? GridCommandItemDisplay.Top
                                                                             : GridCommandItemDisplay.None;
        }
        private void RefreshMenuRaspro(AppEnum.DataMode dataMode)
        {
            if (pnlRaspro.Visible == false) return;

            // Default
            lnkNewRasal.Enabled = false;
            lnkNewRaslan.Enabled = false;
            lnkNewRaspatur.Enabled = false;
            lnkNewRaspraja.Enabled = false;
            lnkNewProphylaxis.Enabled = false;

            //var rasproEnabled = dataMode == AppEnum.DataMode.Read && IsUserInParamedicTeam(RegistrationCurrent);
            var rasproEnabled = IsUserInParamedicTeam(RegistrationCurrent);
            if (rasproEnabled)
            {
                lnkNewRasal.Enabled = true;

                // Cek jika sudah dibuat rasal yg terisi infeksi nya maka disabled
                var lastrr = new RegistrationRaspro();
                lastrr.Query.Where(lastrr.Query.RegistrationNo == RegistrationNo, lastrr.Query.SRRaspro == "RASAL", lastrr.Query.AbRestrictionID.IsNotNull());
                lastrr.Query.OrderBy(lastrr.Query.SeqNo.Descending);
                lastrr.Query.es.Top = 1;
                if (lastrr.Query.Load())
                    lnkNewRasal.Enabled = false;

                //RASLAN 
                lnkNewRaslan.Enabled = !lnkNewRasal.Enabled || !string.IsNullOrWhiteSpace(AntibioticRestrictionForLine);

                //RASPRAJA
                if (!lnkNewRasal.Enabled)
                {
                    // Cek raspraja exist
                    var rr = new RegistrationRaspro();
                    rr.Query.Where(rr.Query.RegistrationNo == RegistrationNo, rr.Query.SRRaspro == AppConstant.RasproType.Raspraja);
                    rr.Query.es.Top = 1;
                    if (!rr.Query.Load())
                    {
                        // Cek item timeout di raspro terakhir yg bukan profilaxis (raspro lama dioverride oleh raspro yg baru)
                        rr = new RegistrationRaspro();
                        rr.Query.Where(rr.Query.RegistrationNo == RegistrationNo, rr.Query.SRRaspro != AppConstant.RasproType.Prophylaxis);
                        rr.Query.es.Top = 1;
                        rr.Query.OrderBy(rr.Query.SeqNo.Descending);
                        if (rr.Query.Load())
                        {
                            var antibioticMaxConsumeDay = AppParameter.GetParameterValue(AppParameter.ParameterItem.AntibioticMaxConsumeDay).ToInt();
                            var rriColl = new RegistrationRasproItemCollection();
                            rriColl.Query.Where(rriColl.Query.RegistrationNo == rr.RegistrationNo, rriColl.Query.RasproSeqNo == rr.SeqNo, rriColl.Query.StopDateTime.IsNull());
                            rriColl.LoadAll();
                            foreach (var item in rriColl)
                            {
                                var consumeDayNo = MedicationReceiveUsed.ConsumedDay(item.RegistrationNo, item.ItemID,
                                    item.SRConsumeMethod, item.ConsumeQty, item.SRConsumeUnit);
                                if (consumeDayNo >= antibioticMaxConsumeDay)
                                {
                                    lnkNewRaspraja.Enabled = true;
                                    break;
                                }
                            }
                        }
                    }
                }

                //lnkNewRaspatur.Enabled = rasproEnabled && PrescriptionEntry.LabTestCultureResultExist(MergeRegistrations); //TODO: Enabled jika sudah ada hasil lab kultur atau ada dok attacth hasil kultur
                lnkNewRaspatur.Enabled = true; // Dibuat true dulu, pikirkan cara untuk hasil kultur dari lab luar

                // Request Profilaksis muncul setelah ada booking kamar OK / VK
                var subq = new ServiceUnitBookingQuery("a");
                subq.Where(subq.RegistrationNo == RegistrationNo);
                subq.es.Top = 1;
                var sub = new ServiceUnitBooking();
                if (sub.Load(subq) && !string.IsNullOrWhiteSpace(sub.RegistrationNo))
                    lnkNewProphylaxis.Enabled = true;

                // Profilaksis hanya utuk AB yg akan dikonsumsi sebelum operasi bukan untuk UDD
            }

        }

        internal static bool LabTestCultureResultExist(List<string> registrations)
        {
            var tcq = new TransChargesQuery("b");
            var itemLab = new ItemLaboratoryQuery("ilb");
            var tci = new TransChargesItemQuery("a");
            tcq.InnerJoin(tci).On(tcq.TransactionNo == tci.TransactionNo);

            tcq.InnerJoin(itemLab).On(tci.ItemID == itemLab.ItemID);

            tcq.Where(tcq.RegistrationNo.In(registrations), itemLab.IsCulture == true);

            tcq.es.Top = 1;
            tcq.OrderBy(tcq.TransactionDate.Descending, tcq.TransactionNo.Descending);
            tcq.Select(tcq);

            var lastLabCulture = new TransCharges();

            if (lastLabCulture.Load(tcq))
            {
                var dtb = MainContent.ExamOrderHistCtl.LaboratoryResult(lastLabCulture.RegistrationNo, lastLabCulture.TransactionNo);

                return dtb.Rows.Count > 0;
            }

            return false;
        }
        protected override void OnMenuNewClick()
        {
            txtPrescriptionDate.SelectedDate = (new DateTime()).NowAtSqlServer();
            txtPrescriptionNo.Text = string.Empty;

            cboServiceUnitID.Enabled = true;

            chkIsCitoPresc.Checked = false;
            if (chkIsUnitDosePresc.Visible) chkIsUnitDosePresc.Checked = false;
            txtNotesPresc.Text = string.Empty;
            txtQtyR.Value = 0;

            // Location
            if (!string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
            {
                ComboBox.PopulateWithServiceUnitForLocation(cboLocationID, cboServiceUnitID.SelectedValue);
                SetMainLocationID(cboServiceUnitID.SelectedValue);
                var su = new ServiceUnit();
                if (AppSession.Parameter.IsLockLocationPharmacy)
                {
                    cboServiceUnitID.Enabled = false;
                    cboLocationID.Enabled = false;
                }
            }
            else
            {
                cboLocationID.Items.Clear();
                cboLocationID.Text = string.Empty;
            }

            // Override ServiceUnit
            SetPrescUnitAndLocationFromGuar();

            tblTemporaryBill.Visible = false;
            var reg = new Registration();
            if (reg.LoadByPrimaryKey(RegistrationNo))
            {
                var grr = new Guarantor();
                if (grr.LoadByPrimaryKey(reg.GuarantorID))
                {
                    tblTemporaryBill.Visible = reg.SRRegistrationType == AppConstant.RegistrationType.OutPatient && grr.SRGuarantorType == AppSession.Parameter.GuarantorTypeBPJS;
                }
            }

            if (tblTemporaryBill.Visible)
            {
                txtTemporaryBillPlafond.Value = Convert.ToDouble(reg.PlavonAmount);
                txtTemporaryBillTotal.Value = Convert.ToDouble(GetTotalTemporaryBill());
            }

            TransPrescriptionItems = null;
            grdTransPrescriptionItem.MasterTableView.IsItemInserted = false;
            grdTransPrescriptionItem.MasterTableView.ClearEditItems();
            grdTransPrescriptionItem.Rebind();
        }

        private bool CheckRequeiredEntry(ValidateArgs args)
        {
            if (string.IsNullOrEmpty(txtNotesPresc.Text) && (!TransPrescriptionItems.Any()))
            {
                args.MessageText = "Please fill prescription notes if no select item";
                args.IsCancel = true;
                return false;
            }
            var r = RegistrationCurrent;
            if (AppSession.Parameter.IsMandatoryPrescriptionCategory && string.IsNullOrEmpty(cboSRPrescriptionCategory.SelectedValue))
            {
                if (r.SRRegistrationType == AppConstant.RegistrationType.InPatient)
                {
                    args.MessageText = "Prescription Type is required";
                    args.IsCancel = true;
                    return false;
                }
            }

            if (AppSession.Parameter.IsMandatoryDrugAllergen)
            {
                var drugAllergen = new PatientAllergyCollection();
                drugAllergen.Query.Where(drugAllergen.Query.PatientID == PatientID, drugAllergen.Query.AllergyGroup == "DrugAllergen");
                drugAllergen.LoadAll();
                if (drugAllergen.Count == 0)
                {
                    args.MessageText = "Drug Allergen is required";
                    args.IsCancel = true;
                    return false;
                }
            }

            //db:20240127 - no longer needed, sudah divalidasi saat pemilihan item obat
            //bool isCasemixValidation = false;
            //var casemixListItem = new CasemixCoveredDetailCollection();

            //if (Helper.GuarantorBpjsCasemix.Contains(r.GuarantorID) && AppSession.Parameter.CasemixValidationRegistrationType.Contains(r.SRRegistrationType))
            //{
            //    isCasemixValidation = true;

            //    var casemixGuar = new CasemixCoveredGuarantorQuery();
            //    casemixGuar.Where(casemixGuar.GuarantorID == r.GuarantorID);
            //    casemixGuar.Select(casemixGuar.CasemixCoveredID);

            //    var query = new CasemixCoveredDetailQuery("a");
            //    var itm = new ItemQuery("b");
            //    var asri = new AppStandardReferenceItemQuery("c");

            //    query.Select(query, itm.ItemName.As("refToItem_ItemName"));
            //    query.InnerJoin(itm).On(query.ItemID == itm.ItemID);
            //    query.InnerJoin(asri).On(asri.StandardReferenceID == AppEnum.StandardReference.ItemType && asri.ItemID == itm.SRItemType);

            //    query.Where(query.IsAllowedToOrder == false, asri.ReferenceID == "Product", query.CasemixCoveredID.In(casemixGuar.Select()));

            //    query.OrderBy(itm.ItemName.Ascending);

            //    casemixListItem.Load(query);
            //}

            var inActiveList = string.Empty;
            //var inCasemixValidationList = string.Empty;
            var exceedQtyList = string.Empty;
            
            foreach (var item in TransPrescriptionItems)
            {
                if (item.IsActive == false)
                {
                    if (inActiveList == string.Empty)
                        inActiveList = item.ItemName;
                    else
                        inActiveList += ", " + item.ItemName;
                }
                //else if (isCasemixValidation)
                //{
                //    if (casemixListItem.Count > 0)
                //    {
                //        try
                //        {
                //            var details = (casemixListItem.Where(b => b.ItemID == item.ItemID)).Take(1).Single();
                //            if (inCasemixValidationList == string.Empty)
                //                inCasemixValidationList = details.ItemName;
                //            else
                //                inCasemixValidationList += ", " + details.ItemName;
                //        }
                //        catch (Exception)
                //        { }
                //    }
                //}

                //db:20241217 - pengecekan peresepan maksimal khusus pasien BPJS yg disesuaikan dg settingan di master item product medic
                if (item.IsFromTemplate == true && Helper.GuarantorBpjsCasemix.Contains(r.GuarantorID))
                {
                    var maxOrderQty = 0;
                    bool isChronic = false;
                    var itemId = item.ItemID;
                    decimal ResultQty = item.ResultQty ?? 0;

                    var ipm = new ItemProductMedic();
                    if (ipm.LoadByPrimaryKey(itemId))
                    {
                        isChronic = ipm.IsChronic ?? false;
                        switch (r.SRRegistrationType)
                        {
                            case "IPR":
                                maxOrderQty = ipm.BpjsMaxQtyOrderIpr ?? 0;
                                break;

                            case "EMR":
                                maxOrderQty = ipm.BpjsMaxQtyOrderEmr ?? 0;
                                break;

                            default:
                                maxOrderQty = ipm.BpjsMaxQtyOrderOpr ?? 0;
                                break;
                        }
                    }
                    if (maxOrderQty > 0)
                    {
                        //1. pengecekan tidak boleh melebih qty yg ditentukan
                        if (ResultQty > maxOrderQty)
                        {
                            var c = isChronic ? "chronic" : "non chronic";
                            
                            if (exceedQtyList == string.Empty)
                                exceedQtyList = string.Format("The maximum qty of {0} drugs ({3}) that can be prescribed are {1} {2}.", c, maxOrderQty.ToString(), item.SRItemUnit, item.ItemName);
                            else
                                exceedQtyList = " " + string.Format("The maximum qty of {0} drugs ({3}) that can be prescribed are {1} {2}.", c, maxOrderQty.ToString(), item.SRItemUnit, item.ItemName);

                        }

                        //2. pengecekan khusus pasien rawat jalan u/ obat kronis jumlahnya tidak boleh melebihi qty dalam periode 1 bulan terakhir
                        if (r.SRRegistrationType == "OPR" && isChronic)
                        {
                            var i = AppSession.Parameter.MaxChronicDrugPrescriptionInDays;
                            if (i > 0)
                            {
                                DateTime fdate = DateTime.Now.AddDays(-1 * i).Date;
                                var tpiq = new TransPrescriptionItemQuery("a");
                                var tpq = new TransPrescriptionQuery("b");
                                var rq = new RegistrationQuery("c");
                                tpiq.InnerJoin(tpq).On(tpq.PrescriptionNo == tpiq.PrescriptionNo && tpq.IsVoid == false && tpq.IsPrescriptionReturn == false);
                                tpiq.InnerJoin(rq).On(rq.RegistrationNo == tpq.RegistrationNo && rq.PatientID == r.PatientID && rq.SRRegistrationType == "OPR");
                                tpiq.Where(tpq.PrescriptionNo != txtPrescriptionNo.Text, tpq.PrescriptionDate >= fdate, tpq.PrescriptionDate <= DateTime.Now, tpiq.ItemID == itemId);
                                tpiq.Select(tpiq.ItemID, tpiq.TakenQty.Sum().Coalesce("0").As("Qty"));
                                tpiq.GroupBy(tpiq.ItemID);

                                DataTable dtb = tpiq.LoadDataTable();
                                if (dtb.Rows.Count > 0)
                                {
                                    var takenQty = Convert.ToDecimal(dtb.Rows[0]["Qty"]);
                                    if (takenQty + ResultQty > maxOrderQty)
                                    {
                                        if (exceedQtyList == string.Empty)
                                            exceedQtyList = string.Format("The maximum qty of {0} drugs ({5}) that can be prescribed for {3} days are {1} {2}  (*previously prescribed: {4} {2}).", "chronic", maxOrderQty.ToString(), item.SRItemUnit, i.ToString(), takenQty.ToInt().ToString(), item.ItemName);
                                        else
                                            exceedQtyList = " " + string.Format("The maximum qty of {0} drugs ({5}) that can be prescribed for {3} days are {1} {2}  (*previously prescribed: {4} {2}).", "chronic", maxOrderQty.ToString(), item.SRItemUnit, i.ToString(), takenQty.ToInt().ToString(), item.ItemName);
                                    }
                                }
                            }
                        }

                        //3. pengecekan obat non kronis, jumlahnya tidak boleh melebihi qty dalam 1 registrasi
                        if (!isChronic)
                        {
                            var tpiq = new TransPrescriptionItemQuery("a");
                            var tpq = new TransPrescriptionQuery("b");
                            var rq = new RegistrationQuery("c");
                            tpiq.InnerJoin(tpq).On(tpq.PrescriptionNo == tpiq.PrescriptionNo && tpq.IsVoid == false && tpq.IsPrescriptionReturn == false);
                            tpiq.InnerJoin(rq).On(rq.RegistrationNo == tpq.RegistrationNo && rq.PatientID == r.PatientID);
                            tpiq.Where(tpq.PrescriptionNo != txtPrescriptionNo.Text, tpq.RegistrationNo == RegistrationNo, tpiq.ItemID == itemId);
                            tpiq.Select(tpiq.ItemID, tpiq.TakenQty.Sum().Coalesce("0").As("Qty"));
                            tpiq.GroupBy(tpiq.ItemID);

                            DataTable dtb = tpiq.LoadDataTable();
                            if (dtb.Rows.Count > 0)
                            {
                                var takenQty = Convert.ToDecimal(dtb.Rows[0]["Qty"]);
                                if (takenQty + ResultQty > maxOrderQty)
                                {
                                    if (exceedQtyList == string.Empty)
                                        exceedQtyList = string.Format("The maximum qty of {0} drugs ({4}) that can be prescribed for this registration are {1} {2}  (*already prescribed: {3} {2}).", "non chronic", maxOrderQty.ToString(), item.SRItemUnit, takenQty.ToInt().ToString(), item.ItemName);
                                    else
                                        exceedQtyList = " " + string.Format("The maximum qty of {0} drugs ({4}) that can be prescribed for this registration are {1} {2}  (*already prescribed: {3} {2}).", "non chronic", maxOrderQty.ToString(), item.SRItemUnit, takenQty.ToInt().ToString(), item.ItemName);
                                }
                            }
                        }
                    }
                }

            }
            
            if (inActiveList.Length > 0)
            {
                args.MessageText = "The following items are no longer active : " + inActiveList + ".";
                args.IsCancel = true;
                return false;
            }

            //if (inCasemixValidationList.Length > 0)
            //{
            //    args.MessageText = "The following items are included in the Casemix list that are not allowed to order : " + inCasemixValidationList;
            //    args.IsCancel = true;
            //    return false;
            //}

            if (exceedQtyList.Length > 0)
            {
                args.MessageText = exceedQtyList;
                args.IsCancel = true;
                return false;
            }

            return true;
        }
        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            if (CheckRequeiredEntry(args))
            {
                var newPrescriptionNo = SaveNewPrescription();
                if (!string.IsNullOrEmpty(newPrescriptionNo) && AppSession.Parameter.IsAutoPrintPrescriptionOrder)
                    PrintPrescriptionOrderViaDirectPrinter(newPrescriptionNo);
            }
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            if (CheckRequeiredEntry(args))
                SaveEditedPrescription();
        }

        protected override void OnMenuPrintClick(ValidateArgs args, string programID, PrintJobParameterCollection printJobParameters)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            var header = new TransPrescription();
            header.LoadByPrimaryKey(txtPrescriptionNo.Text);
            if (header.IsApproval == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "validated", "alert('Prescription already validated by dispensary');", true);
                args.IsCancel = true;
                args.MessageText = "Prescription already validated by dispensary";
            }
        }

        protected override void OnMenuEditClick()
        {
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
            return !string.IsNullOrWhiteSpace(txtPrescriptionNo.Text);
        }

        public override bool OnGetStatusMenuDelete()
        {
            return true;
        }

        public override bool? OnGetStatusMenuApproval()
        {
            if (!string.IsNullOrWhiteSpace(txtPrescriptionNo.Text))
            {
                var presc = new TransPrescription();
                presc.LoadByPrimaryKey(txtPrescriptionNo.Text);
                return !presc.IsApproval ?? false;
            }
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
            ajax.AddAjaxSetting(grdTransPrescriptionItem, lblZatActiveInteraction);

            // Hanya In Patient dan jika AntibioticRestriction diterapkan
            if (IsRasproEnableApplied)
            {
                // Raspro control
                ajax.AddAjaxSetting(grdTransPrescriptionItem, litRasproInfo); // Refresh menu
            }

            if (tblTemporaryBill.Visible)
            {
                ajax.AddAjaxSetting(grdTransPrescriptionItem, txtTemporaryBillTotal);
            }
        }
        #endregion

        protected bool IsRasproEnableApplied
        {
            get
            {
                if (string.IsNullOrWhiteSpace(hdnIsRasproEnableApplied.Value))
                {
                    var othRegTypes = AppParameter.GetParameterValue(AppParameter.ParameterItem.RasproEnableForRegistrationTypes);
                    if (!string.IsNullOrWhiteSpace(othRegTypes))
                        hdnIsRasproEnableApplied.Value = (AppSession.Parameter.IsRasproEnable && (RegistrationCurrent.SRRegistrationType == AppConstant.RegistrationType.InPatient || othRegTypes.Contains(RegistrationCurrent.SRRegistrationType))).ToString();
                    else
                        hdnIsRasproEnableApplied.Value = (AppSession.Parameter.IsRasproEnable && RegistrationCurrent.SRRegistrationType == AppConstant.RegistrationType.InPatient).ToString();
                }
                return Convert.ToBoolean(hdnIsRasproEnableApplied.Value);
            }
        }

        protected bool IsAntibioticRestrictionApplied
        {
            get
            {
                return AppSession.Parameter.IsRasproEnable && AppParameter.IsYes(AppParameter.ParameterItem.IsAntibioticRestriction) && RegistrationCurrent.SRRegistrationType == AppConstant.RegistrationType.InPatient;
            }
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ElectronicMedicalRecord;

            // Program Fiture
            IsSingleRecordMode = true; //Save then close

            ToolBar.ApprovalUnApprovalVisible = false;
            ToolBar.VoidUnVoidVisible = false;
            ToolBar.PrintVisible = false;
            ToolBar.DeleteVisible = false;
            // -------------------

            //PaneEntry.Width = new Unit(900, UnitType.Pixel);
            PaneEntry.Width = new Unit(1100, UnitType.Pixel);

            if (!IsPostBack)
            {
                // Hanya In Patient dan jika AntibioticRestriction diterapkan
                pnlRaspro.Visible = IsRasproEnableApplied;
                clpAntibioticSuggest.Visible = IsRasproEnableApplied;

                var reg = RegistrationCurrent;

                var pat = new Patient();
                if (pat.LoadByPrimaryKey(PatientID))
                {
                    Title = string.Format("Prescription for : {0} (MRN: {1}), Reg No : {2}, (Age : {3} Y {4} M {5} D) ", pat.PatientName, pat.MedicalNo, RegistrationNo, reg.AgeInYear, reg.AgeInMonth, reg.AgeInDay);


                    txtAgeInYear.Text = reg.AgeInYear.ToString() + "Y   "  + reg.AgeInMonth.ToString() + "M   " + reg.AgeInDay.ToString() + "D";

                    PopulatePatientImage(PatientID, pat.Sex);
                }

                // Use for query history 
                hdnRegistrationType.Value = reg.SRRegistrationType;
                hdnFromRegistrationNo.Value = reg.FromRegistrationNo;

                // ServiceUnit
                ComboBox.PopulateWithServiceUnitForTransaction(cboServiceUnitID, BusinessObject.Reference.TransactionCode.Prescription, false);
                cboServiceUnitID.Items.Remove(cboServiceUnitID.Items.Single(c => string.IsNullOrEmpty(c.Value)));

                var su = new ServiceUnit();
                su.LoadByPrimaryKey(reg.ServiceUnitID);
                if ((su.ServiceUnitPharmacyID ?? string.Empty) == string.Empty)
                    cboServiceUnitID.SelectedValue = reg.SRRegistrationType == AppConstant.RegistrationType.InPatient ? AppSession.Parameter.ServiceUnitPharmacyID : AppSession.Parameter.ServiceUnitPharmacyIdOpr;
                else
                    cboServiceUnitID.SelectedValue = su.ServiceUnitPharmacyID;

                ComboBox.PopulateWithServiceUnitForLocation(cboLocationID, cboServiceUnitID.SelectedValue);
                SetMainLocationID(cboServiceUnitID.SelectedValue);

                // Override ServiceUnit Location
                SetPrescUnitAndLocationFromGuar();

                chkIsUnitDosePresc.Visible = false; // Sudah tidak dipakai (Hermawan 2019/08/22)
                StandardReference.InitializeIncludeSpace(cboSRPrescriptionCategory, AppEnum.StandardReference.PrescriptionCategory);

                TransChargesItemTemporaries = null;
                var tciTemps = TransChargesItemTemporaries;
                TransPrescriptionItemTemporaries = null;
                var tpiTemps = TransPrescriptionItemTemporaries;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Add menu Hist Medication Cons
                var tbi = new RadToolBarButton();
                tbi.Text = "Medication Consumption History";
                tbi.Value = "medhist";
                tbi.ImageUrl = "~/Images/Toolbar/ordering16.png";

                ToolBarMenuData.Items.Add(tbi);

                litPatientAllergy.Text = PatientAllergy(PatientID);


                if (pnlRaspro.Visible)
                {
                    // JIKA ADA PERUBAHAN DISINI MAKA SESUAIKAN JUGA YANG DI UddItem.aspx.cs
                    var rr = new RegistrationRaspro();
                    if (string.IsNullOrWhiteSpace(hdnRasproSeqNo.Value) || Session["RasproSeqNo"] != null) // Jika belum diload atau setelah create RASPRO baru
                    {
                        if (Session["RasproSeqNo"] != null)
                        {
                            // Ambil value RasproSeqNo, akan ada nilainya jika sehabis mengisi RASPRO Form
                            hdnRasproSeqNo.Value = Session["RasproSeqNo"].ToString();
                            hdnRasproType.Value = Session["SRRaspro"].ToString();

                            if (hdnRasproType.Value == "RASPRAJA")
                                TransPrescriptionItems = null; //Reset supaya reload

                            if (Session["RasproRefNo"] != null)
                            {
                                hdnRasproRefNo.Value = Session["RasproRefNo"].ToString();
                                Session.Remove("RasproRefNo");
                            }
                            //Reset
                            Session.Remove("RasproSeqNo");
                            Session.Remove("SRRaspro");


                            rr.LoadByPrimaryKey(RegistrationNo, hdnRasproSeqNo.Value.ToInt());

                            if (rr.AntibioticLevel == Temiang.Avicenna.BusinessObject.AbRestriction.AntibioticLevel.UseAbNonProphylaxis)
                            {
                                // Popup
                                this.ShowMessageAfterPostback("Using antibiotics from RASAL");
                            }

                            // Info dari form raspro baru
                            hdnAbLevel.Value = rr.AntibioticLevel == 0 ? string.Empty : rr.AntibioticLevel.ToString();
                            hdnAbRestrictionID.Value = rr.AbRestrictionID.ToString();
                            hdnRasproSeqNo4Filter.Value = "0"; //Harus "0" krn dipakai di lookUp Item
                            hdnRasproIsFirstPrescriptionNo.Value = "1";
                            hdnRasproIsNew.Value = "1";
                        }
                        else
                        {
                            // Ambil raspro form terakhir
                            rr.Query.es.Top = 1;
                            rr.Query.OrderBy(rr.Query.SeqNo.Descending);
                            rr.Query.Where(rr.Query.RegistrationNo == RegistrationNo);
                            if (rr.Query.Load())
                            {
                                hdnRasproSeqNo.Value = rr.SeqNo.ToString();
                                hdnRasproSeqNo4Filter.Value = hdnRasproSeqNo.Value;
                                hdnRasproType.Value = rr.SRRaspro;
                                hdnRasproRefNo.Value = rr.ReferenceNo;
                                hdnAbLevel.Value = rr.AntibioticLevel == 0 ? string.Empty : rr.AntibioticLevel.ToString();
                                hdnAbRestrictionID.Value = rr.AbRestrictionID.ToString();
                                hdnRasproIsFirstPrescriptionNo.Value = txtPrescriptionNo.Text.Equals(rr.PrescriptionNo) ? "1" : "0";

                                // 1. Cek jika belum ada AB Itemnya anggap sebagai mode AB selection (from raspro baru)
                                var ritem = new RegistrationRasproItem();
                                ritem.Query.Where(ritem.Query.RegistrationNo == rr.RegistrationNo);
                                if (rr.SRRaspro == AppConstant.RasproType.Raspraja)
                                    ritem.Query.Where(ritem.Query.RasprajaSeqNo == rr.SeqNo);
                                else
                                    ritem.Query.Where(ritem.Query.RasproSeqNo == rr.SeqNo);
                                ritem.Query.es.Top = 1;
                                if (ritem.Query.Load())
                                {
                                    // 2. Cek jika AB belum di approve resepnya maka anggap sebagai mode AB selection (from raspro baru)
                                    var ritems = new RegistrationRasproItemCollection();
                                    ritems.Query.Where(ritem.Query.RegistrationNo == rr.RegistrationNo, ritem.Query.RasproSeqNo == rr.SeqNo);
                                    ritems.LoadAll();
                                    var rasproItems = new List<string>();
                                    foreach (var item in ritems)
                                    {
                                        rasproItems.Add(item.ItemID);
                                    }

                                    var tp = new TransPrescriptionQuery("tp");
                                    var tpi = new TransPrescriptionItemQuery("tpi");
                                    tp.InnerJoin(tpi).On(tp.PrescriptionNo == tpi.PrescriptionNo);
                                    tp.Where(tp.RegistrationNo == RegistrationNo, tp.PrescriptionDate > rr.RasproDateTime, tp.IsApproval == true, tpi.ItemID.In(rasproItems));
                                    tp.Select(tp.PrescriptionNo);
                                    tp.es.Top = 1;
                                    var dtbCheck = tp.LoadDataTable();
                                    if (dtbCheck.Rows.Count > 0)
                                        hdnRasproSeqNo4Filter.Value = hdnRasproSeqNo.Value; // Close mode new raspro
                                    else
                                        hdnRasproSeqNo4Filter.Value = "0"; // mode new raspro form
                                }
                                else
                                    hdnRasproSeqNo4Filter.Value = "0"; // mode new raspro form

                            }
                            else
                                hdnRasproSeqNo4Filter.Value = "0"; // mode new raspro form

                            hdnRasproIsNew.Value = hdnRasproSeqNo4Filter.Value == "0" ? "1" : "0";
                        }
                    }
                    else
                    {
                        rr.LoadByPrimaryKey(RegistrationNo, hdnRasproSeqNo.Value.ToInt());
                    }

                    // Populate disini krn dijalankan juga dari psotback setelah isi popup raspro
                    if (!string.IsNullOrEmpty(hdnRasproSeqNo.Value))
                    {
                        var usedRasproSeqno = 0;
                        litAntibioticSuggest.Text = AbRestriction.AntibioticSuggestion(rr, ref usedRasproSeqno);
                        hdnRasproSeqNo4Filter.Value = usedRasproSeqno.ToString(); // Untuk filter lookup item

                        //Exam Order Lab Tr No (Rspro Culture)
                        grdLaboratoryCultureResult.Visible = false;
                        if (hdnRasproType.Value.Equals("RASPATUR") && !string.IsNullOrWhiteSpace(hdnRasproRefNo.Value))
                        {
                            grdLaboratoryCultureResult.Visible = true;
                            switch (AppSession.Parameter.LisInterop)
                            {
                                case "LINK_LIS":
                                    //string labNo = Convert.ToString(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ResultValue"]);
                                    //grdLaboratoryCultureResult.DataSource = MainContent.ExamOrderHistCtl.LaboratoryResult(RegistrationNo, transactionNo, labNo);
                                    break;
                                default:
                                    grdLaboratoryCultureResult.DataSource = MainContent.ExamOrderHistCtl.LaboratoryResult(RegistrationNo, hdnRasproRefNo.Value);
                                    break;
                            }
                            grdLaboratoryCultureResult.Rebind();
                        }

                    }
                }

            }
        }
        public override string OnGetScriptToolBarAdditional()
        {
            return "case \"medhist\" :  openMedicationHist(); args.set_cancel(true); break;";
        }
        private string CurrentParamedicID
        {
            get
            {
                return Request.QueryString["parid"];
            }
        }

        #region Record Detail Method Function TransPrescriptionItem

        private TransPrescriptionItemCollection TransPrescriptionItems
        {
            get
            {
                if (IsPostBack)
                {
                    var obj = Session["collTransPrescriptionItem" + Request.UserHostName];
                    if (obj != null)
                        return ((TransPrescriptionItemCollection)(obj));
                }

                var coll = new TransPrescriptionItemCollection();

                var query = new TransPrescriptionItemQuery("a");
                var qItem = new ItemQuery("b");
                var qItemIntervention = new ItemQuery("c");
                var cons = new ConsumeMethodQuery("cm");
                var emb = new EmbalaceQuery("e");
                var acdcpc = new AppStandardReferenceItemQuery("acdcpc");
                var ipm = new ItemProductMedicQuery("ipm");

                query.Select
                    (
                        query,
                        qItem.ItemName.As("refToItem_ItemName"),
                        qItem.IsActive.As("refToItem_IsActive"),
                        qItemIntervention.ItemName.Coalesce("''").As("refToItem_ItemInterventionName"),
                        "<(a.ParentNo + a.SequenceNo) as ORDERKEY>",
                        "<COALESCE(cm.SRConsumeMethodName,'') + ' ' + COALESCE(acdcpc.ItemName,'') as refToConsumeMethod_SRConsumeMethodName>",
                        emb.EmbalaceLabel.Coalesce("''").As("refToEmbalace_EmbalaceLabel"),
                        ipm.FornasRestrictionNotes.Coalesce("''").As("refToItem_FornasRestrictionNotes"),
                        @"<CAST(0 AS BIT) AS refTo_IsFromTemplate>"
                    );
                query.InnerJoin(qItem).On(query.ItemID == qItem.ItemID);
                query.LeftJoin(qItemIntervention).On(query.ItemInterventionID == qItemIntervention.ItemID);
                query.LeftJoin(cons).On(query.SRConsumeMethod == cons.SRConsumeMethod);
                query.LeftJoin(emb).On(query.EmbalaceID == emb.EmbalaceID);
                query.LeftJoin(acdcpc).On(query.Acpcdc == acdcpc.ItemID &&
                                          acdcpc.StandardReferenceID == AppEnum.StandardReference.MedicationConsume);
                query.LeftJoin(ipm).On(ipm.ItemID == query.ItemID);

                query.Where(query.PrescriptionNo == txtPrescriptionNo.Text);
                query.OrderBy("ORDERKEY", esOrderByDirection.Ascending);
                coll.Load(query);

                Session["collTransPrescriptionItem" + Request.UserHostName] = coll;
                return coll;
            }
            set
            { Session["collTransPrescriptionItem" + Request.UserHostName] = value; }
        }

        protected void grdTransPrescriptionItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            // ZatActiveInteraction
            var itemIds = new List<string>();
            foreach (var presItem in TransPrescriptionItems)
            {
                if (!presItem.es.IsDeleted)
                    itemIds.Add(presItem.ItemID);
            }
            lblZatActiveInteraction.Text = ZatActiveInteraction(itemIds);

            grdTransPrescriptionItem.DataSource = TransPrescriptionItems;

            if (pnlRaspro.Visible)
            {
                var rr = new RegistrationRaspro();
                if (rr.LoadByPrimaryKey(RegistrationNo, hdnRasproSeqNo.Value.ToInt()))
                {
                    string linkEdit = TransPrescriptionItems.Count > 0 ? String.Empty : string.Format("&nbsp;&nbsp;<a href=\"#\" onclick=\"javascript:showRaspro('{0}', '{1}','edit'); return false;\"><img src=\"{2}/Images/Toolbar/edit16.png\"  alt=\"View\" /></a>", rr.SRRaspro, rr.SeqNo, Helper.UrlRoot());

                    var abr = new AbRestriction();
                    abr.LoadByPrimaryKey(rr.AbRestrictionID);

                    //TODO: Untuk info Profilaksis
                    litRasproInfo.Text = string.Format(@"<fieldset>
                <legend><b>LAST RASPRO FORM {4}</b></legend>
<strong>Type:</strong>&nbsp;{0}&nbsp;&nbsp;<strong>Create Date:</strong>&nbsp;{1}&nbsp;<strong>No:</strong>&nbsp;{2}&nbsp;<strong>Focus Infection:</strong>&nbsp;{3}<br />
            </fieldset>", rr.SRRaspro, rr.RasproDateTime.Value.ToString(AppConstant.DisplayFormat.DateShortMonthHourMinute), rr.SeqNo, abr.AbRestrictionName, linkEdit);
                }
            }
        }

        protected void grdTransPrescriptionItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            var sequenceNo = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][TransPrescriptionItemMetadata.ColumnNames.SequenceNo]);
            var entity = FindTransPrescriptionItem(sequenceNo);
            if (entity != null)
            {
                SetEntityValue(entity, e);

                if (tblTemporaryBill.Visible)
                {
                    txtTemporaryBillTotal.Value = Convert.ToDouble(GetTotalTemporaryBill());
                }
            }
        }

        protected void grdTransPrescriptionItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            var sequenceNo = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][TransPrescriptionItemMetadata.ColumnNames.SequenceNo]);
            var entity = FindTransPrescriptionItem(sequenceNo);
            if (entity != null)
            {
                // Delete compound
                foreach (var prescItem in TransPrescriptionItems.Where(prescItem => sequenceNo.Equals(prescItem.ParentNo)))
                {
                    prescItem.MarkAsDeleted();
                }

                entity.MarkAsDeleted();

                if (tblTemporaryBill.Visible)
                {
                    txtTemporaryBillTotal.Value = Convert.ToDouble(GetTotalTemporaryBill());
                }
            }

            grdTransPrescriptionItem.Rebind();
            cboServiceUnitID.Enabled = !TransPrescriptionItems.Any();
            if (AppSession.Parameter.IsLockLocationPharmacy)
            {
                cboServiceUnitID.Enabled = false;
                cboLocationID.Enabled = false;
            }

        }

        internal static string ZatActiveInteraction(List<string> itemIds)
        {
            if (itemIds.Count == 0)
            {
                return String.Empty;
            }
            var ipmz = new ItemProductMedicZatActiveQuery("zam");
            var item = new ItemQuery("i");
            ipmz.InnerJoin(item).On(ipmz.ItemID == item.ItemID);
            var za = new ZatActiveQuery("za");
            ipmz.InnerJoin(za).On(ipmz.ZatActiveID == za.ZatActiveID);
            ipmz.Where(ipmz.ItemID.In(itemIds));
            ipmz.Select(ipmz.ZatActiveID, ipmz.ItemID, item.ItemName, za.ZatActiveName);
            ipmz.es.Distinct = true;
            var dtbZa = ipmz.LoadDataTable();

            if (dtbZa.Rows == null || dtbZa.Rows.Count == 0)
                return String.Empty;

            var strb = new StringBuilder();
            strb.AppendLine("<fieldset><legend><b>Drug Interaction</b></legend>");
            strb.AppendLine("<table id=\"druginteraction\"><tr><th>Drug 1</th><th>Drug 2</th><th>Interaction</th></tr>");
            foreach (DataRow row1 in dtbZa.Rows)
            {
                foreach (DataRow row2 in dtbZa.Rows)
                {
                    var zaid1 = row1[0].ToString();
                    var zaid2 = row2[0].ToString();

                    if (zaid1 != zaid2)
                    {
                        var zai = new ZatActiveInteraction();
                        if (zai.LoadByPrimaryKey(zaid1, zaid2))
                        {
                            strb.AppendLine(string.Format("<tr><td>{0} ({1})</td><td>{2} ({3})</td><td>{4}</td></tr>", row1["ItemName"], row1["ZatActiveName"], row2["ItemName"], row2["ZatActiveName"], zai.Interaction));
                        }
                    }
                }
            }

            strb.AppendLine("</table>");
            strb.AppendLine("</fieldset>");
            return strb.ToString();
        }

        protected void grdTransPrescriptionItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            var entity = TransPrescriptionItems.AddNew();
            SetEntityValue(entity, e);

            if (tblTemporaryBill.Visible)
            {
                txtTemporaryBillTotal.Value = Convert.ToDouble(GetTotalTemporaryBill());
            }

            e.Canceled = true;
            grdTransPrescriptionItem.Rebind();

            cboServiceUnitID.Enabled = !TransPrescriptionItems.Any();
            if (AppSession.Parameter.IsLockLocationPharmacy)
            {
                cboServiceUnitID.Enabled = false;
                cboLocationID.Enabled = false;
            }
        }

        private TransPrescriptionItem FindTransPrescriptionItem(String sequenceNo)
        {
            return TransPrescriptionItems.FirstOrDefault(rec => rec.SequenceNo.Equals(sequenceNo));
        }

        private void SetEntityValue(TransPrescriptionItem entity, GridCommandEventArgs e)
        {
            var userControl = (PrescriptionItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.PrescriptionNo = txtPrescriptionNo.Text;
                entity.SequenceNo = userControl.SequenceNo;
                entity.ParentNo = userControl.ParentNo;
                entity.IsRFlag = string.IsNullOrEmpty(userControl.ParentNo);
                entity.IsCompound = userControl.IsCompound;
                entity.ItemID = userControl.ItemID;
                entity.ItemName = userControl.ItemName;
                entity.ItemInterventionID = string.Empty;
                entity.ItemInterventionName = string.Empty;
                entity.SRItemUnit = userControl.ItemUnit;
                entity.ItemQtyInString = userControl.ItemQtyInString;
                entity.IsUsingDosageUnit = false;
                entity.SRDosageUnit = userControl.DosageUnit;
                entity.FrequencyOfDosing = 0;
                entity.DosingPeriod = string.Empty;
                entity.NumberOfDosage = 0;
                entity.DurationOfDosing = 0;
                entity.Acpcdc = userControl.AcPcDc;
                entity.IterText = userControl.Iter;
                entity.SRMedicationRoute = string.Empty;
                entity.PrescriptionQty = userControl.ResultQty;

                if (userControl.StartDateTime != null)
                    entity.StartDateTime = userControl.StartDateTime;
                else
                    entity.str.StartDateTime = string.Empty;

                var itm = new Item();
                if (itm.LoadByPrimaryKey(entity.ItemID))
                    entity.IsActive = itm.IsActive ?? false;
                else entity.IsActive = false;

                bool isActualDeduct = true;
                bool isNoPrescriptionFee = false;
                var item = new ItemProductMedic();
                if (item.LoadByPrimaryKey(entity.ItemID))
                {
                    isActualDeduct = item.IsActualDeduct ?? false;
                    isNoPrescriptionFee = item.IsNoPrescriptionFee ?? false;
                    entity.FornasRestrictionNotes = item.FornasRestrictionNotes;
                }
                else
                    entity.FornasRestrictionNotes = string.Empty;

                if (isActualDeduct)
                    entity.TakenQty = entity.PrescriptionQty;
                else
                {
                    var x = Convert.ToDecimal(userControl.ResultQty) - Math.Floor(Convert.ToDecimal(userControl.ResultQty));

                    var deduct = new ItemProductDeductionDetail();
                    deduct.Query.Where(string.Format("<{0} BETWEEN MinAmount AND MaxAmount>", x));
                    if (deduct.Query.Load())
                    {
                        entity.TakenQty = decimal.Truncate(userControl.ResultQty) + deduct.DeductionAmount;
                    }
                    else
                    {
                        entity.TakenQty = entity.PrescriptionQty;
                    }
                }

                entity.ResultQty = entity.TakenQty;
                entity.CostPrice = userControl.CostPrice;
                entity.InitialPrice = userControl.InitialPrice;
                entity.DiscountAmount = 0;
                entity.EmbalaceID = userControl.EmbalaceID;
                entity.EmbalaceAmount = userControl.EmbalaceAmount;
                entity.IsUseSweetener = false;
                entity.SweetenerAmount = 0;

                decimal lineAmount, recipeAmount;

                var reg = new Registration();
                reg.LoadByPrimaryKey(Request.QueryString["regno"]);

                var grr = new Guarantor();
                grr.LoadByPrimaryKey(reg.GuarantorID);

                entity.Price = Helper.Tariff.GetItemTariff(grr.SRTariffType, DateTime.Now.Date, reg.ChargeClassID,
                    entity.ItemID, entity.IsCompound ?? false, entity.SRItemUnit, grr.GuarantorID, reg.SRRegistrationType);

                //PopulateResultQtyAndLineAmount(entity.IsCompound ?? false, entity.PrescriptionQty ?? 0, entity.ResultQty ?? 0, entity.Price ?? 0,
                //    out lineAmount, out recipeAmount, entity.DiscountAmount ?? 0, entity.EmbalaceAmount ?? 0, entity.ParentNo, isNoPrescriptionFee, grr.GuarantorID);

                PopulateResultQtyAndLineAmount(entity.IsCompound ?? false, entity.ItemQtyInString, entity.ResultQty ?? 0, entity.Price ?? 0,
                out lineAmount, out recipeAmount, entity.DiscountAmount ?? 0, entity.EmbalaceAmount ?? 0, entity.ParentNo, isNoPrescriptionFee, grr.GuarantorID, entity.ItemID);

                entity.LineAmount = lineAmount;
                entity.RecipeAmount = recipeAmount;
                entity.DiscountAmount = 0;
                entity.Total = ((entity.ResultQty ?? 0) * ((entity.Price ?? 0) - (entity.DiscountAmount ?? 0))) + (entity.RecipeAmount ?? 0) + (entity.EmbalaceAmount ?? 0);
                entity.Notes = userControl.Notes;
                entity.SRDiscountReason = string.Empty;
                entity.IsApprove = false;
                entity.IsVoid = false;
                entity.IsBillProceed = false;
                entity.DurationRelease = 0;
                entity.SRConsumeMethod = userControl.ConsumeMethod;
                entity.SRConsumeMethodName = string.Format("{0} {1}", userControl.ConsumeMethodName, userControl.AcPcDcName);
                entity.DosageQty = userControl.DosageQty;
                entity.EmbalaceQty = userControl.EmbalaceQty;
                entity.EmbalaceLabel = userControl.EmbalaceLabel;

                entity.ConsumeQty = userControl.QtyConsume;
                entity.SRConsumeUnit = userControl.SRConsumeUnit;

                //set ori value
                entity.OriPrescriptionQty = entity.PrescriptionQty;
                entity.OriConsumeQty = entity.ConsumeQty;
                entity.OriSRConsumeUnit = entity.SRConsumeUnit;
                entity.OriResultQty = entity.ResultQty;
                entity.OriItemQtyInString = entity.ItemQtyInString;
                entity.OriSRItemUnit = entity.SRItemUnit;
                entity.OriDosageQty = entity.DosageQty;
                entity.OriSRDosageUnit = entity.SRDosageUnit;
                entity.OriSRConsumeMethod = entity.SRConsumeMethod;
                entity.Qty23Days = userControl.Qty23Days;

                if (Helper.GuarantorBpjsCasemix.Contains(reg.GuarantorID) && AppSession.Parameter.CasemixValidationRegistrationType.Contains(reg.SRRegistrationType))
                {
                    var itemId = (string.IsNullOrWhiteSpace(entity.ItemInterventionID) ? entity.ItemID : entity.ItemInterventionID);
                    entity.IsCasemixApproved = Helper.IsCasemixApproved(itemId, entity.ResultQty ?? 0, reg.RegistrationNo, entity.PrescriptionNo, reg.GuarantorID, true);
                }
                else
                    entity.IsCasemixApproved = true;

                entity.IsFromTemplate = false;
            }
        }

        #endregion

        private static string[] GuarantorBPJS
        {
            get
            {
                var grr = new GuarantorBridgingCollection();
                grr.Query.es.Distinct = true;
                grr.Query.Where(grr.Query.SRBridgingType.In(AppEnum.BridgingType.BPJS.ToString(), AppEnum.BridgingType.BPJS_TNI_POLRI_PNS.ToString()));
                if (grr.Query.Load()) return grr.Select(g => g.GuarantorID).ToArray();
                else return new string[] { string.Empty };
            }
        }

        private static void PopulateResultQtyAndLineAmount(bool isCompound, string prescQty, decimal takenQty, decimal price, out decimal lineAmount,
    out decimal recipeAmount, decimal discount, decimal embalaceAmount, string parentNo, bool isNoPrescriptionFee, string guarantorId,
            string itemID)
        {
            recipeAmount = 0;
            if (!isCompound)
            {
                var recipe = new TransPrescription();
                if (!isNoPrescriptionFee)                    
                    recipeAmount = (decimal)recipe.RecipeAmount(null, guarantorId, itemID, takenQty, parentNo, prescQty, isCompound);
                //recipeAmount = Convert.ToDecimal(AppParameter.GetParameterValue(AppParameter.ParameterItem.RecipeMarginValueNonCompound));
                //lineAmount = (takenQty * (price - discount)) + recipeAmount;

                if (AppSession.Parameter.GuarantorIdExeptionForRecipeAmount.Contains(guarantorId))
                    recipeAmount = 0;

                lineAmount = Helper.Rounding((takenQty * (price - discount)) + recipeAmount, AppEnum.RoundingType.Prescription);
            }
            else
            {
                var recipe = new TransPrescription();
                recipeAmount = (decimal)recipe.RecipeAmount(null, guarantorId, itemID, takenQty, parentNo, prescQty, isCompound);
                //if (string.IsNullOrEmpty(parentNo) || AppParameter.IsYes(AppParameter.ParameterItem.IsRecipeMarginValueForEachItemCompound))
                //{
                //    var margin = new RecipeMarginValue();
                //    margin.Query.Where(string.Format("<{0} BETWEEN StartingValue AND EndingValue>", new Fraction(string.IsNullOrEmpty(prescQty) ? "0" : prescQty)));
                //    if (margin.Query.Load()) recipeAmount += margin.RecipeAmount ?? 0;
                //}

                if (AppSession.Parameter.GuarantorIdExeptionForRecipeAmount.Contains(guarantorId))
                    recipeAmount = 0;

                lineAmount = Helper.Rounding((takenQty * (price - discount)) + recipeAmount + embalaceAmount, AppEnum.RoundingType.Prescription);
            }
        }
        protected string GetItemName(object isRFlag, object itemName)
        {
            if (isRFlag.Equals(true))
                return itemName.ToString();
            return "&nbsp;&nbsp;&nbsp;" + itemName;
        }

        private string SaveNewPrescription()
        {
            var reg = new Registration();
            reg.LoadByPrimaryKey(RegistrationNo);

            string lastCompleteNumber;
            //if (AppSession.Parameter.ServiceUnitPharmacyID != AppSession.Parameter.ServiceUnitPharmacyIdOpr)
            //{
            //    var autoNumber = Helper.GetNewAutoNumber(DateTime.Now.Date,
            //    cboServiceUnitID.SelectedValue ==
            //    AppSession.Parameter.ServiceUnitPharmacyID
            //        ? AppEnum.AutoNumber.PrescIpNo
            //        : AppEnum.AutoNumber.PrescOpNo);
            //    autoNumber.Save();
            //    lastCompleteNumber = autoNumber.LastCompleteNumber;
            //}
            //else
            //{
            //    var autoNumber = Helper.GetNewAutoNumber(DateTime.Now.Date,
            //    reg.SRRegistrationType == AppConstant.RegistrationType.InPatient
            //        ? AppEnum.AutoNumber.PrescIpNo
            //        : AppEnum.AutoNumber.PrescOpNo);
            //    autoNumber.Save();
            //    lastCompleteNumber = autoNumber.LastCompleteNumber;
            //}

            //db:20231128 - penomoran disesuaikan dg asal pasien (disamakan dg di prescription sales)
            var autoNumber = Helper.GetNewAutoNumber(DateTime.Now.Date,
                reg.SRRegistrationType == AppConstant.RegistrationType.InPatient
                    ? AppEnum.AutoNumber.PrescIpNo
                    : AppEnum.AutoNumber.PrescOpNo);
            autoNumber.Save();
            lastCompleteNumber = autoNumber.LastCompleteNumber;

            var tp = new TransPrescription();
            tp.PrescriptionNo = lastCompleteNumber; //autoNumber.LastCompleteNumber;
            tp.PrescriptionDate = (new DateTime()).NowAtSqlServer();
            tp.RegistrationNo = RegistrationNo;
            tp.ServiceUnitID = cboServiceUnitID.SelectedValue;
            tp.LocationID = cboLocationID.SelectedValue;
            tp.ClassID = reg.ChargeClassID;
            tp.ParamedicID = string.IsNullOrEmpty(AppSession.UserLogin.ParamedicID) ? reg.ParamedicID : AppSession.UserLogin.ParamedicID;
            tp.IsApproval = false;
            tp.IsVoid = false;
            tp.Note = txtNotesPresc.Text;
            tp.IsPrescriptionReturn = false;
            tp.ReferenceNo = string.Empty;
            tp.IsFromSOAP = true;
            tp.IsBillProceed = false;
            tp.IsUnitDosePrescription = chkIsUnitDosePresc.Checked;
            tp.IsCito = chkIsCitoPresc.Checked;
            tp.IsClosed = (tp.IsUnitDosePrescription ?? false) ? true : false;
            tp.str.ApprovalDateTime = string.Empty;
            tp.str.DeliverDateTime = string.Empty;
            tp.TextPrescription = string.Empty;
            tp.QtyR = Convert.ToInt16(txtQtyR.Value);
            //tp.IsForTakeItHome = chkIsForTakeItHome.Checked;
            tp.IsForTakeItHome = AppParameter.GetParameterValue(AppParameter.ParameterItem.PrescriptionCategoryHomePresID).Equals(cboSRPrescriptionCategory.SelectedValue);
            tp.SRPrescriptionCategory = cboSRPrescriptionCategory.SelectedValue;
            if (IsRasproEnableApplied && !string.IsNullOrWhiteSpace(hdnRasproSeqNo.Value))
                tp.RasproSeqNo = hdnRasproSeqNo.Value.ToInt();
            else
                tp.str.RasproSeqNo = string.Empty; //set null

            var room = new ServiceRoom();
            if (room.LoadByPrimaryKey(reg.RoomID))
                tp.SRFloor = room.SRFloor;

            //var summary = " || " + tp.Note;

            foreach (var entity in TransPrescriptionItems)
            {
                entity.PrescriptionNo = tp.PrescriptionNo;
            }

            using (var trans = new esTransactionScope())
            {
                tp.Save();
                TransPrescriptionItems.Save();
                trans.Complete();
            }


            // Update berikutnya setelah record Prescription tersimpan di DB
            using (var trans = new esTransactionScope())
            {
                // Update Prescription Hist in SOAP
                TransPrescription.SoapeUpdatePrescriptionHist(tp.ParamedicID, RegistrationNo, tp.PrescriptionDate.Value);

                if (IsRasproEnableApplied && tp.RasproSeqNo > 0 && hdnRasproIsFirstPrescriptionNo.Value == "1")
                {
                    var rr = new RegistrationRaspro();
                    rr.LoadByPrimaryKey(tp.RegistrationNo, tp.RasproSeqNo ?? 0);
                    if (string.IsNullOrEmpty(rr.PrescriptionNo))
                    {
                        rr.PrescriptionNo = tp.PrescriptionNo; // Update info first prescription
                        rr.Save();

                        // Save Raspro item drug
                        SaveNewRegistrationRasproItem(tp.RegistrationNo, tp.PrescriptionNo, tp.RasproSeqNo ?? 0);
                    }
                }

                // Langsung Export to MedicationReceive untuk data recon discharge, discharge summary & Edukasi obat dibawa pulang
                if (tp.IsForTakeItHome ?? false)
                    MedicationReceive.ImportFromPrescriptionBaseOnTherapy(tp.PrescriptionNo, RegistrationNo,
                        tp.PrescriptionDate.Value);

                trans.Complete();
            }

            #region apol
            //apol
            if (Helper.IsApotekOnlineIntegration && reg.GuarantorID == AppSession.Parameter.GuarantorAskesID[0])
            {
                var bpjsapol = new BpjsApol();
                bpjsapol.Query.Where(bpjsapol.Query.RegistrationNo == RegistrationNo, bpjsapol.Query.PrescriptionNo == tp.PrescriptionNo);
                if (!bpjsapol.Query.Load())
                {
                    using (var trans = new esTransactionScope())
                    {
                        bpjsapol.RegistrationNo = RegistrationNo;
                        bpjsapol.PrescriptionNo = tp.PrescriptionNo;
                        bpjsapol.REFASALSJP = reg.BpjsSepNo;
                        bpjsapol.IDUSERSJP = AppSession.UserLogin.UserID;
                        bpjsapol.TGLSJP = tp.CreatedDateTime;
                        bpjsapol.TGLPELRSP = tp.LastUpdateDateTime;
                        bpjsapol.TGLRSP = tp.PrescriptionDate;
                        bpjsapol.MetadataCode = "ORDER";
                        bpjsapol.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        bpjsapol.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        bpjsapol.Save();

                        // Insert data tpi ke bad
                        foreach (var it in TransPrescriptionItems)
                        {
                            // jns robit racikan: "R.{NN}" -> R.01
                            var seqStr = (Convert.ToString(it.SequenceNo) ?? "").Trim();
                            var m = System.Text.RegularExpressions.Regex.Match(seqStr, @"\d{1,2}(?!.*\d)");
                            string jnsrobt;
                            if (it.IsCompound == true)
                            {
                                var seq = Convert.ToString(it.SequenceNo) ?? "";
                                var lastDigit = seq.Reverse().FirstOrDefault(char.IsDigit);
                                jnsrobt = (lastDigit != default(char)) ? ("R.0" + lastDigit) : "R.01"; // fallback aman
                            }
                            else
                            {
                                jnsrobt = "N";
                            }

                            string kdobt = null, nmobat = null;
                            var map = new ItemBridging();
                            map.Query.Where(
                                map.Query.ItemID == it.ItemID,
                                map.Query.SRBridgingType == AppEnum.BridgingType.APOTEKONLINE.ToString()
                            );
                            if (map.Query.Load()) { kdobt = map.BridgingID; nmobat = map.BridgingName; }

                            int signa2 = 0;
                            var cm = new ConsumeMethod();
                            cm.Query.Where(cm.Query.SRConsumeMethod == it.SRConsumeMethod);
                            if (cm.Query.Load()) signa2 = cm.IterationQty ?? 0;

                            var det = new BpjsApolDetail
                            {
                                PrescriptionNo = tp.PrescriptionNo,
                                SequenceNo = it.SequenceNo,
                                BpjsApolID = bpjsapol.ID,
                                JNSROBT = jnsrobt,
                                KDOBT = kdobt,
                                NMOBAT = nmobat,
                                SIGNA1OBT = it.DosageQty.ToInt(),
                                SIGNA2OBT = signa2,
                                PERMINTAAN = it.PrescriptionQty.ToInt(), // penting utk racikan
                                JMLOBT = it.TakenQty.ToInt(),
                                JHO = it.DaysOfUsage,
                                CATKHSOBT = it.Notes,
                                MetadataCode = "ORDER",
                                LastUpdateByUserID = AppSession.UserLogin.UserID,
                                LastUpdateDateTime = (new DateTime()).NowAtSqlServer()
                            };

                            det.Save();
                        }

                        trans.Complete();
                    }
                }
            }
            #endregion


            return tp.PrescriptionNo;
        }

        private void SaveNewRegistrationRasproItem(string registrationNo, string prescriptionNo, int rasproSeqNo)
        {
            var udItem = new TransPrescriptionItemQuery("p");
            var udItems = new TransPrescriptionItemCollection();
            var qItemMedic = new ItemProductMedicQuery("im");
            udItem.InnerJoin(qItemMedic).On(udItem.ItemID == qItemMedic.ItemID);

            udItem.Where(udItem.PrescriptionNo == prescriptionNo, qItemMedic.IsAntibiotic == true);
            udItems.Load(udItem);

            foreach (var udi in udItems)
            {
                // Cek apakah sudah di import

                var za = new ItemProductMedicZatActive();
                za.Query.Where(za.Query.ItemID == udi.ItemID);
                za.Query.es.Top = 1; //TODO: bagaimana yg za nya >1
                if (za.Query.Load())
                {
                    //Jika ada Zat Active
                    var ritem = new RegistrationRasproItem();
                    if (!ritem.LoadByPrimaryKey(registrationNo, rasproSeqNo, udi.ItemID))
                    {
                        ritem = new RegistrationRasproItem();
                        ritem.ZatActiveID = za.ZatActiveID;
                        ritem.RegistrationNo = RegistrationNo;
                        ritem.RasproSeqNo = rasproSeqNo;
                        ritem.ItemID = udi.ItemID;
                    }
                    ritem.ConsumeQty = udi.ConsumeQty;
                    ritem.SRConsumeUnit = udi.SRConsumeUnit;
                    ritem.SRItemUnit = udi.SRItemUnit;
                    ritem.SRDosageUnit = udi.SRDosageUnit;
                    ritem.AcPcDc = udi.Acpcdc;
                    ritem.StartDateTime = udi.StartDateTime;
                    ritem.SRConsumeMethod = udi.SRConsumeMethod;
                    ritem.SRMedicationRoute = udi.SRMedicationRoute;
                    ritem.DosageQty = udi.DosageQty;
                    ritem.EmbalaceQty = udi.EmbalaceQty;
                    ritem.StartDateTime = udi.StartDateTime;
                    ritem.Save();
                }
            }
        }

        private int RegistrationGyssensNewSeqNo(string regNo)
        {
            var qr = new RegistrationGyssensQuery("a");
            var ent = new RegistrationGyssens();
            qr.es.Top = 1;
            qr.Where(qr.RegistrationNo == regNo);
            qr.OrderBy(qr.SeqNo.Descending);

            if (ent.Load(qr))
            {
                return ent.SeqNo.ToInt() + 1;
            }
            return 1;
        }
        private string SaveEditedPrescription()
        {
            var header = new TransPrescription();
            header.LoadByPrimaryKey(txtPrescriptionNo.Text);
            if (header.IsApproval == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "validated", "alert('Prescription already validated by dispensary');", true);
                return txtPrescriptionNo.Text;
            }

            // Sebelum update IsForTakeItHome, cek dan kurangi receive qty MedicationReceive jika resep yg diedit adalah tipe hom presc
            if (header.IsForTakeItHome ?? false)
                MedicationReceive.ImportFromPrescriptionBaseOnTherapy(txtPrescriptionNo.Text, RegistrationNo,null,true);

            // Update with edit data
            header.ServiceUnitID = cboServiceUnitID.SelectedValue;
            header.LocationID = cboLocationID.SelectedValue;
            header.IsUnitDosePrescription = chkIsUnitDosePresc.Checked;
            header.IsCito = chkIsCitoPresc.Checked;
            header.IsClosed = (header.IsUnitDosePrescription ?? false) ? true : false;
            header.Note = txtNotesPresc.Text;
            header.QtyR = Convert.ToInt16(txtQtyR.Value);

            header.IsForTakeItHome = AppParameter.GetParameterValue(AppParameter.ParameterItem.PrescriptionCategoryHomePresID).Equals(cboSRPrescriptionCategory.SelectedValue);
            header.SRPrescriptionCategory = cboSRPrescriptionCategory.SelectedValue;
            using (var trans = new esTransactionScope())
            {
                header.Save();

                //Cari AB yg berubah terapinya
                if (IsRasproEnableApplied)
                {
                    foreach (var item in TransPrescriptionItems)
                    {
                        var ipm = new ItemProductMedic();

                        if (item.es.IsModified && ipm.LoadByPrimaryKey(item.GetOriginalColumnValue("ItemID").ToString())
                            && true.Equals(ipm.IsAntibiotic) &&
                            (!item.GetOriginalColumnValue("ItemID").Equals(item.ItemID)
                            || !item.GetOriginalColumnValue("SRConsumeMethod").Equals(item.SRConsumeMethod)
                            || !item.GetOriginalColumnValue("ConsumeQty").Equals(item.ConsumeQty)
                            || !item.GetOriginalColumnValue("SRConsumeUnit").Equals(item.SRConsumeUnit)))
                        {
                            // Update RegistrationRasproItem
                            var ritem = new RegistrationRasproItem();
                            ritem.Query.Where(ritem.Query.RegistrationNo == RegistrationNo,
                                ritem.Query.RasproSeqNo == header.RasproSeqNo,
                                ritem.Query.ItemID == item.GetOriginalColumnValue("ItemID").ToString(),
                                ritem.Query.SRConsumeMethod == item.GetOriginalColumnValue("SRConsumeMethod").ToString(),
                                ritem.Query.ConsumeQty == item.GetOriginalColumnValue("ConsumeQty").ToString(),
                                ritem.Query.SRConsumeUnit == item.GetOriginalColumnValue("SRConsumeUnit").ToString()
                                );
                            if (ritem.Query.Load())
                            {
                                ritem.ItemID = item.ItemID;
                                ritem.SRConsumeMethod = item.SRConsumeMethod;
                                ritem.ConsumeQty = item.ConsumeQty;
                                ritem.SRConsumeUnit = item.SRConsumeUnit;
                                ritem.Save();
                            }

                            // Update RegistrationGyssens
                            var gyssens = new RegistrationGyssens();
                            gyssens.Query.Where(gyssens.Query.RegistrationNo == RegistrationNo,
                                gyssens.Query.RasproSeqNo == header.RasproSeqNo,
                                gyssens.Query.ItemID == item.GetOriginalColumnValue("ItemID").ToString(),
                                gyssens.Query.SRConsumeMethod == item.GetOriginalColumnValue("SRConsumeMethod").ToString(),
                                gyssens.Query.ConsumeQty == item.GetOriginalColumnValue("ConsumeQty").ToString(),
                                gyssens.Query.SRConsumeUnit == item.GetOriginalColumnValue("SRConsumeUnit").ToString()
                                );
                            if (gyssens.Query.Load())
                            {
                                gyssens.ItemID = item.ItemID;
                                gyssens.SRConsumeMethod = item.SRConsumeMethod;
                                gyssens.ConsumeQty = item.ConsumeQty;
                                gyssens.SRConsumeUnit = item.SRConsumeUnit;
                                gyssens.Save();
                            }
                        }
                    }
                }


                TransPrescriptionItems.Save();
                trans.Complete();
            }

            // Update Prescription Hist in SOAP
            using (var trans = new esTransactionScope())
            {
                TransPrescription.SoapeUpdatePrescriptionHist(header.ParamedicID, RegistrationNo, header.PrescriptionDate.Value);
                trans.Complete();
            }

            // Langsung Export to MedicationReceive untuk resep dibawa pulang
            if (header.IsForTakeItHome ?? false)
                MedicationReceive.ImportFromPrescriptionBaseOnTherapy(header.PrescriptionNo, RegistrationNo,
                    header.PrescriptionDate.Value);

            return txtPrescriptionNo.Text;
        }

        private void PrintPrescriptionOrderViaDirectPrinter(string prescriptionNo)
        {
            PrintJobParameterCollection jobParameters = new PrintJobParameterCollection();

            // Check report type
            string rptType;
            var ph = new AppProgramHealthcare();
            if (ph.LoadByPrimaryKey(AppConstant.Report.PrescriptionOrderSlip, AppSession.Parameter.HealthcareInitial))
            {
                rptType = ph.ProgramType;
            }
            else
            {
                var prg = new AppProgram();
                prg.LoadByPrimaryKey(AppConstant.Report.PrescriptionOrderSlip);
                rptType = prg.ProgramType;
            }

            if (rptType.ToLower() == "rslip")
            {
                // Memerlukan parameter HealthcareID di  printJobParameters[0]
                var jobParameter0 = jobParameters.AddNew();
                jobParameter0.Name = "p_HealthCareID";
                jobParameter0.ValueString = AppSession.Parameter.HealthcareID;
            }

            var jobParameter2 = jobParameters.AddNew();
            jobParameter2.Name = "p_PrescriptionNo";
            jobParameter2.ValueString = prescriptionNo;

            AppSession.PrintJobParameters = jobParameters;
            AppSession.PrintJobReportID = AppConstant.Report.PrescriptionOrderSlip;

            string printerName = PrintManager.CreatePrintJob(AppSession.PrintJobReportID, AppSession.PrintJobParameters);
            string script = printerName != string.Empty ? string.Format("<script type='text/javascript'>alert('Print Prescription Slip has order to printer {0}');</script>", printerName) : "<script type='text/javascript'>alert('Please contact IT support for defined printer address for print direct');</script>";
            if (!Page.ClientScript.IsStartupScriptRegistered("msgPrint"))
                Page.ClientScript.RegisterStartupScript(this.GetType(), "msgPrint", script);
        }

        private void PopulatePatientImage(string patientID, string sex)
        {
            // Patient Photo
            imgPatientPhoto.ImageUrl = string.Empty;

            // Load from database
            var patientImg = new PatientImage();
            if (patientImg.LoadByPrimaryKey(patientID))
            {
                // Show Image
                if (patientImg.Photo != null)
                {
                    imgPatientPhoto.ImageUrl = string.Format("data:image/jpg;base64,{0}",
                        Convert.ToBase64String(patientImg.Photo));
                }
                else
                {
                    imgPatientPhoto.ImageUrl = sex == "M" ? "~/Images/Asset/Patient/ManVector.png" : "~/Images/Asset/Patient/WomanVector.png";
                }
            }
            else
                imgPatientPhoto.ImageUrl = sex == "M" ? "~/Images/Asset/Patient/ManVector.png" : "~/Images/Asset/Patient/WomanVector.png";

        }

        #region Prescription History

        protected void grdPrescriptionHist_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!string.IsNullOrEmpty(PatientID) && !IsPostBack)
            {
                grdPrescriptionHist.DataSource = PrescriptionHistDataTable();
            }
        }
        private List<string> _patientRelateds;
        private List<string> PatientRelateds
        {
            get
            {
                if (_patientRelateds == null)
                {
                    _patientRelateds = Patient.PatientRelateds(PatientID);
                }
                return _patientRelateds;
            }
        }
        private DataTable PrescriptionHistDataTable()
        {
            if (IsPostBack && Session["histpresc"] != null)
            {
                return (DataTable)Session["histpresc"];
            }
            var qr = new RegistrationQuery("r");
            var qrPar = new ParamedicQuery("pr");
            qr.InnerJoin(qrPar).On(qr.ParamedicID == qrPar.ParamedicID);

            qr.Select(qr.RegistrationNo, qr.RegistrationDate, qr.RegistrationDateTime, qrPar.ParamedicName, qr.IsConsul, qr.IsNewPatient);

            if (hdnRegistrationType.Value == AppConstant.RegistrationType.InPatient)
            {
                qr.Where(qr.Or(qr.RegistrationNo == RegistrationNo, qr.RegistrationNo == hdnFromRegistrationNo.Value));
            }
            else
            {
                if (PatientRelateds.Count == 1)
                    qr.Where(qr.PatientID == PatientID);
                else
                    qr.Where(qr.PatientID.In(PatientRelateds));
            }

            qr.Where(qr.IsVoid == false);
            qr.OrderBy(qr.RegistrationDate.Descending, qr.RegistrationNo.Descending);

            qr.es.Top = 20;

            var prescQr = new TransPrescriptionQuery("p");
            prescQr.Select(prescQr.RegistrationNo);
            prescQr.Where(prescQr.RegistrationNo == qr.RegistrationNo);

            qr.Where(qr.Exists(prescQr));

            var dtbEpisodeDiagnose = qr.LoadDataTable();
            dtbEpisodeDiagnose.Columns.Add(new DataColumn("Diagnosis", typeof(string)));
            dtbEpisodeDiagnose.Columns.Add(new DataColumn("ICD10", typeof(string)));
            dtbEpisodeDiagnose.Columns.Add(new DataColumn("Prescription", typeof(string)));

            var rowSorted = dtbEpisodeDiagnose.Select("", "RegistrationDateTime DESC");

            // Get detail diagnose
            foreach (DataRow rowHd in rowSorted)
            {
                var regNo = rowHd["RegistrationNo"].ToString();

                var presc = PrescriptionDetailInHTML(regNo);
                if (string.IsNullOrEmpty(presc))
                {
                    continue;
                }
                rowHd["Prescription"] = presc;

                // Diagnose entri oleh dokter
                var rimQr = new RegistrationInfoMedicQuery();
                rimQr.Where(rimQr.RegistrationNo == regNo);
                rimQr.es.Top = 1;
                rimQr.Select(rimQr.Info3);
                var rim = new RegistrationInfoMedic();
                if (rim.Load(rimQr))
                {
                    rowHd["Diagnosis"] = rim.Info3;
                }
                else
                {
                    // Cari didata lama
                    var soapQr = new EpisodeSOAPEQuery();
                    soapQr.Where(soapQr.RegistrationNo == regNo);
                    soapQr.es.Top = 1;
                    soapQr.Select(soapQr.Assesment);
                    var soap = new EpisodeSOAPE();
                    if (soap.Load(soapQr))
                        rowHd["Diagnosis"] = soap.Assesment;
                }

                rowHd["ICD10"] = EpisodeDiagnose.DiagnoseSummaryHtml(regNo);
            }


            // Delete empty prescription
            DataRow[] deleteRows = dtbEpisodeDiagnose.Select("Prescription is null OR Prescription = ''");
            foreach (DataRow deleteRow in deleteRows)
            {
                deleteRow.Delete();
            }
            dtbEpisodeDiagnose.AcceptChanges();

            Session["histpresc"] = dtbEpisodeDiagnose;
            return dtbEpisodeDiagnose;
        }

        private string PrescriptionDetailInHTML(string registrationNo)
        {

            var query = new TransPrescriptionItemQuery("a");
            var presc = new TransPrescriptionQuery("b");
            var medic = new ParamedicQuery("d");
            var item = new ItemQuery("c");
            var consume = new ConsumeMethodQuery("e");
            var emb = new EmbalaceQuery("g");
            var oriconsume = new ConsumeMethodQuery("h");

            presc.Select(
                presc.PrescriptionNo,
                query.SequenceNo,
                presc.PrescriptionDate,
                medic.ParamedicName,
                presc.ParamedicID,
                presc.CreatedByUserID,
                item.ItemName,
                @"<ISNULL(a.OriResultQty, a.ResultQty) AS ResultQty>",
                @"<ISNULL(a.OriSRItemUnit, a.SRItemUnit) AS SRItemUnit>",
                @"<ISNULL(h.SRConsumeMethodName, e.SRConsumeMethodName) AS SRConsumeMethodName>",
                presc.IsUnitDosePrescription,
                query.IsRFlag.Coalesce("CAST(0 AS BIT)").As("IsRFlag"),
                query.IsCompound.Coalesce("CAST(0 AS BIT)").As("IsCompound"),
                emb.EmbalaceLabel,
                @"<ISNULL(a.OriDosageQty, a.DosageQty) AS DosageQty>",
                @"<ISNULL(a.OriSRDosageUnit, a.SRDosageUnit) AS SRDosageUnit>",
                query.EmbalaceQty,
                presc.Note,
                @"<ISNULL(a.OriConsumeQty, a.ConsumeQty) AS ConsumeQty>",
                @"<ISNULL(a.OriSRConsumeUnit, a.SRConsumeUnit) AS SRConsumeUnit>",
                "<(a.ParentNo + a.SequenceNo) as ORDERKEY>",
                query.LineAmount,
                query.Notes
                );

            presc.LeftJoin(query).On(query.PrescriptionNo == presc.PrescriptionNo);
            presc.InnerJoin(medic).On(presc.ParamedicID == medic.ParamedicID);
            presc.LeftJoin(item).On(query.ItemID == item.ItemID);
            presc.LeftJoin(consume).On(query.SRConsumeMethod == consume.SRConsumeMethod);
            presc.LeftJoin(oriconsume).On(query.OriSRConsumeMethod == oriconsume.SRConsumeMethod);
            presc.LeftJoin(emb).On(query.EmbalaceID == emb.EmbalaceID);

            presc.Where(
                presc.RegistrationNo == registrationNo,
                presc.IsVoid == false
                );


            presc.OrderBy(presc.PrescriptionDate.Descending, presc.PrescriptionNo.Descending);
            presc.OrderBy("ORDERKEY", esOrderByDirection.Ascending);

            var table = presc.LoadDataTable();


            // Ambil list PrescriptionNo
            var prescs = from t in table.AsEnumerable()
                         group t by new
                         {
                             PrescriptionNo = t.Field<string>("PrescriptionNo"),
                             ParamedicID = t.Field<string>("ParamedicID")
                         }
                             into g
                         select new
                         {
                             g.Key.PrescriptionNo,
                             g.Key.ParamedicID
                         };


            var displayTotal = AppSession.Parameter.IsShowPrescPriceOnDisplayDoctor;

            var prescriptionHeader = "";
            var sbPresciption = new StringBuilder();
            sbPresciption.AppendLine("<table width='100%' cellpadding='0' cellspacing='0'>");

            foreach (var p in prescs)
            {
                int i = 0;
                double total = 0;
                var sbItem = new StringBuilder();
                foreach (DataRow r in table.AsEnumerable().Where(t => t.Field<string>("PrescriptionNo") == p.PrescriptionNo))
                {

                    if (i == 0)
                    {
                        prescriptionHeader = string.Format("{0} - {1}", r["PrescriptionNo"], Convert.ToDateTime(r["PrescriptionDate"]).ToString(AppConstant.DisplayFormat.DateShortMonth));
                    }
                    i++;

                    if (r["SequenceNo"] == DBNull.Value) continue;

                    if (!Convert.ToBoolean(r["IsCompound"]))
                    {
                        sbItem.AppendFormat("{0} {1} {2} {3} ({4} @ {5} {6} {7})<br />",
                            Convert.ToBoolean(r["IsRFlag"]) ? string.Format("<b>{0}</b>", @"R/") : "&nbsp;&nbsp;&nbsp;&nbsp;",
                            r["ItemName"],
                            r["ResultQty"],
                            StandardReference.GetItemName(AppEnum.StandardReference.ItemUnit, r["SRItemUnit"].ToString()),
                            r["SRConsumeMethodName"],
                            r["ConsumeQty"],
                            StandardReference.GetItemName(AppEnum.StandardReference.DosageUnit, r["SRConsumeUnit"].ToString()),
                            r["Notes"]);
                    }
                    else
                    {
                        sbItem.AppendFormat("{0} {1} {2} {3} @ {4} {5} ({6} @ {7} {8} {9})<br />",
                            Convert.ToBoolean(r["IsRFlag"]) ? string.Format("<b>{0}</b>", @"R/") : "&nbsp;&nbsp;&nbsp;&nbsp;",
                            r["ItemName"],
                            r["EmbalaceQty"],
                            r["EmbalaceLabel"],
                            r["DosageQty"],
                            StandardReference.GetItemName(AppEnum.StandardReference.DosageUnit, r["SRDosageUnit"].ToString()),
                            r["SRConsumeMethodName"],
                            r["ConsumeQty"],
                            StandardReference.GetItemName(AppEnum.StandardReference.DosageUnit, r["SRConsumeUnit"].ToString()),
                            r["Notes"]);
                    }

                    total += Convert.ToDouble(r["LineAmount"]);
                }

                if (displayTotal)
                    sbItem.AppendFormat("<b>{0}</b>", " (Rp. " + string.Format("{0:n2}", (total)) + ")");

                sbPresciption.AppendLine("<tr><td align='left' style='width:20px;padding-top: 4px;vertical-align:top;'>"); ;

                if (AppSession.UserLogin.SRUserType == AppUser.UserType.Doctor)
                {
                    if (DataModeCurrent == AppEnum.DataMode.New)
                    {
                        if (AppParameter.IsYes(AppParameter.ParameterItem.IsAllowCopyPrescOther) || AppSession.UserLogin.ParamedicID == p.ParamedicID)
                        {
                            // Mode new bisa copy dari history prescription
                            sbPresciption.AppendFormat(
                                "<div title='Copy to Current Entry'><a href='javascript:void(0);' onclick=\"javascript:showCopyPrescription('{0}',false)\"><img src='{1}/Images/Toolbar/ordering16.png' alt='Copy' /></a></div></br>",
                                p.PrescriptionNo, Helper.UrlRoot());
                        }
                    }

                    if (AppParameter.IsYes(AppParameter.ParameterItem.IsAllowCopyPrescOther) || AppSession.UserLogin.ParamedicID == p.ParamedicID)
                    {
                        sbPresciption.AppendFormat(
                            "<div title='Save As New Template'><a href='javascript:void(0);' onclick=\"javascript:openSaveAsNewTemplate('{0}',false)\"><img src='{1}/Images/Toolbar/copy16.png' alt='Copy' /></a></div>",
                            p.PrescriptionNo, Helper.UrlRoot());
                    }
                    sbPresciption.AppendLine("</td>");
                }
                sbPresciption.AppendFormat("<td align='left'><fieldset><legend>{0}</legend>{1}</fieldset></br></td>", prescriptionHeader, sbItem);
                sbPresciption.AppendLine("</tr>");

            }
            sbPresciption.AppendLine("</table>");
            return sbPresciption.ToString();
        }


        #endregion

        #region Allergy
        internal static string PatientAllergy(string patientID)
        {
            var paQ = new PatientAllergyQuery("a");
            paQ.Select(paQ.AllergenName, paQ.DescAndReaction);
            paQ.Where(paQ.PatientID == patientID);
            var dtb = paQ.LoadDataTable();
            var sb = new StringBuilder();
            sb.AppendLine("<table style='width:100%'>");
            if (dtb.Rows.Count > 0)
                foreach (DataRow dataRow in dtb.Rows)
                {
                    sb.AppendLine("<tr>");
                    sb.AppendFormat("<td style='width:100px;font-weight: bold;'>{0}</td>", dataRow["AllergenName"]);
                    sb.AppendLine("<td style='width:5px'>:</td>");
                    sb.AppendFormat("<td style='color: red;'>{0}</td>", dataRow["DescAndReaction"]);
                    sb.AppendLine("</tr>");
                }
            else
                sb.AppendLine("<tr><td style='width:100px;font-weight: bold;'>-No Allergies-</td></tr>");
            sb.AppendLine("</table>");
            return sb.ToString();
        }
        #endregion

        #region Question Form
        protected void grdVitalSign_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (txtPrescriptionDate.SelectedDate != null)
                grdVitalSign.DataSource = VitalSign.VitalSignLastValue(RegistrationNo, hdnFromRegistrationNo.Value, true,
                    txtPrescriptionDate.SelectedDate.Value);
            else
            {
                grdVitalSign.DataSource = VitalSign.VitalSignLastValue(RegistrationNo, hdnFromRegistrationNo.Value, true,
                    DateTime.Now);
            }
        }
        #endregion

        #region Prescription Template
        public static void SetPrescriptionEntityValue(TransPrescriptionItem entity, string registrationNo,
        string prescriptionNo, string sequenceNo, string parentNo, bool isCompound,
        string itemID, string itemUnit, string itemQtyInString, string srDosageUnit, decimal resultQty,
        decimal costPrice, decimal initialPrice,
        string embalaceID, decimal embalaceAmount,
        string notes, string srConsumeMethod, string consumeMethodName,
        string dosageQty, string embalaceQty, string embalaceLabel,
        string qtyConsume, string srConsumeUnit, string srMedicationConsume, string guarantorId, bool isFromTemplate = false)
        {
            entity.PrescriptionNo = prescriptionNo;
            entity.SequenceNo = sequenceNo;
            entity.ParentNo = parentNo;
            entity.IsRFlag = string.IsNullOrEmpty(parentNo);
            entity.IsCompound = isCompound;
            entity.ItemID = itemID;

            var item = new Item();
            if (item.LoadByPrimaryKey(itemID))
            {
                entity.ItemName = item.ItemName;
                entity.IsActive = item.IsActive ?? false;
            }
            else
            {
                entity.IsActive = false;
            }

            entity.ItemInterventionID = string.Empty;
            entity.ItemInterventionName = string.Empty;
            entity.SRItemUnit = itemUnit;
            entity.ItemQtyInString = itemQtyInString;
            entity.IsUsingDosageUnit = false;
            entity.SRDosageUnit = srDosageUnit;
            entity.FrequencyOfDosing = 0;
            entity.DosingPeriod = string.Empty;
            entity.NumberOfDosage = 0;
            entity.DurationOfDosing = 0;
            entity.Acpcdc = srMedicationConsume;
            entity.SRMedicationRoute = string.Empty;
            entity.PrescriptionQty = resultQty;

            bool isActualDeduct = true;
            bool isNoPrescriptionFee = false;
            var im = new ItemProductMedic();
            if (im.LoadByPrimaryKey(entity.ItemID))
            {
                isActualDeduct = im.IsActualDeduct ?? false;
                isNoPrescriptionFee = im.IsNoPrescriptionFee ?? false;
            }

            if (isActualDeduct)
                entity.TakenQty = entity.PrescriptionQty;
            else
            {
                var x = Convert.ToDecimal(resultQty) - Math.Floor(Convert.ToDecimal(resultQty));

                var deduct = new ItemProductDeductionDetail();
                deduct.Query.Where(string.Format("<{0} BETWEEN MinAmount AND MaxAmount>", x));
                if (deduct.Query.Load())
                {
                    entity.TakenQty = decimal.Truncate(resultQty) + deduct.DeductionAmount;
                }
                else
                {
                    entity.TakenQty = entity.PrescriptionQty;
                }
            }

            entity.ResultQty = entity.TakenQty;

            // jika parameter costprice -1 maka ambil lagi costprice dari master
            if (costPrice == -1)
            {
                var vItemQ = new VwItemProductMedicNonMedicQuery();
                vItemQ.Where(vItemQ.ItemID == itemID);

                var ipm = new VwItemProductMedicNonMedic();
                ipm.Load(vItemQ);
                costPrice = ipm.CostPrice.Value;
            }
            entity.CostPrice = costPrice;

            //db:20231030 - declare parameter (u/ direct prescription yg gak ada noreg)
            var parGuarantorId = guarantorId;
            var parChargeClassId = AppSession.Parameter.DefaultTariffClass;
            var parSrRegistrationType = "OPR";

            var reg = new Registration();
            if (reg.LoadByPrimaryKey(registrationNo))
            {
                parGuarantorId = reg.GuarantorID;
                parChargeClassId = reg.ChargeClassID;
                parSrRegistrationType = reg.SRRegistrationType;
            }
             
            var grr = new Guarantor();
            grr.LoadByPrimaryKey(parGuarantorId); //reg.GuarantorID);

            // jika parameter InitialPrice -1 maka ambil lagi dari tarif
            if (initialPrice == -1)
            {
                initialPrice = (decimal)Helper.Tariff.GetItemTariffNonMargin(
                    grr.SRTariffType, DateTime.Now.Date, parChargeClassId, //reg.ChargeClassID,
                    itemID, isCompound, itemUnit);
            }
            entity.InitialPrice = initialPrice;
            entity.DiscountAmount = 0;
            entity.EmbalaceID = embalaceID;

            if (embalaceAmount == -1)
            {
                if (string.IsNullOrEmpty(embalaceID)) { embalaceAmount = 0; }
                else
                {
                    if (isCompound && !string.IsNullOrEmpty(parentNo))
                        embalaceAmount = 0;
                    else
                    {
                        var emb = new Embalace();
                        emb.LoadByPrimaryKey(embalaceID);
                        embalaceAmount = emb.EmbalaceFeeAmount ?? 0;
                    }
                }
            }
            entity.EmbalaceAmount = embalaceAmount;
            entity.IsUseSweetener = false;
            entity.SweetenerAmount = 0;

            decimal lineAmount, recipeAmount;

            entity.Price = Helper.Tariff.GetItemTariff(grr.SRTariffType, DateTime.Now.Date, parChargeClassId, //reg.ChargeClassID,
                entity.ItemID, entity.IsCompound ?? false, entity.SRItemUnit, grr.GuarantorID, parSrRegistrationType); //reg.SRRegistrationType);

            //PopulateResultQtyAndLineAmount(entity.IsCompound ?? false, entity.PrescriptionQty ?? 0, entity.ResultQty ?? 0, entity.Price ?? 0,
            //    out lineAmount, out recipeAmount, entity.DiscountAmount ?? 0, entity.EmbalaceAmount ?? 0, entity.ParentNo, isNoPrescriptionFee, grr.GuarantorID);

            PopulateResultQtyAndLineAmount(entity.IsCompound ?? false, entity.ItemQtyInString, entity.ResultQty ?? 0, entity.Price ?? 0,
                out lineAmount, out recipeAmount, entity.DiscountAmount ?? 0, entity.EmbalaceAmount ?? 0, entity.ParentNo, isNoPrescriptionFee, grr.GuarantorID, entity.ItemID);

            entity.LineAmount = lineAmount;
            entity.RecipeAmount = recipeAmount;
            entity.DiscountAmount = 0;
            entity.Total = ((entity.ResultQty ?? 0) * ((entity.Price ?? 0) - (entity.DiscountAmount ?? 0))) + (entity.RecipeAmount ?? 0) + (entity.EmbalaceAmount ?? 0);
            entity.Notes = notes;
            entity.SRDiscountReason = string.Empty;
            entity.IsApprove = false;
            entity.IsVoid = false;
            entity.IsBillProceed = false;
            entity.DurationRelease = 0;
            entity.SRConsumeMethod = srConsumeMethod;
            entity.SRConsumeMethodName = consumeMethodName;
            entity.DosageQty = dosageQty;
            entity.EmbalaceQty = embalaceQty;
            entity.EmbalaceLabel = embalaceLabel;

            entity.ConsumeQty = qtyConsume;
            entity.SRConsumeUnit = srConsumeUnit;

            //set ori value
            entity.OriPrescriptionQty = entity.PrescriptionQty;
            entity.OriConsumeQty = entity.ConsumeQty;
            entity.OriSRConsumeUnit = entity.SRConsumeUnit;
            entity.OriResultQty = entity.ResultQty;
            entity.OriItemQtyInString = entity.ItemQtyInString;
            entity.OriSRItemUnit = entity.SRItemUnit;
            entity.OriDosageQty = entity.DosageQty;
            entity.OriSRDosageUnit = entity.SRDosageUnit;
            entity.OriSRConsumeMethod = entity.SRConsumeMethod;

            //if (Helper.GuarantorBpjsCasemix.Contains(reg.GuarantorID) && AppSession.Parameter.CasemixValidationRegistrationType.Contains(reg.SRRegistrationType))
            //db:20231030 - ganti reg.GuarantorID & reg.SRRegistrationType jd parameter yg di-declare di atas (u/ direct prescription yg gak ada noreg)
            if (Helper.GuarantorBpjsCasemix.Contains(parGuarantorId) && AppSession.Parameter.CasemixValidationRegistrationType.Contains(parSrRegistrationType))
            {
                var itemId = (string.IsNullOrWhiteSpace(entity.ItemInterventionID) ? entity.ItemID : entity.ItemInterventionID);
                //entity.IsCasemixApproved = Helper.IsCasemixApproved(itemId, entity.ResultQty ?? 0, reg.RegistrationNo, entity.PrescriptionNo, reg.GuarantorID, true);
                entity.IsCasemixApproved = Helper.IsCasemixApproved(itemId, entity.ResultQty ?? 0, registrationNo, entity.PrescriptionNo, parGuarantorId, true);
            }
            else
                entity.IsCasemixApproved = true;

            entity.IsFromTemplate = isFromTemplate;
        }

        #endregion

        #region Prescription Template
        protected void grdPrescriptionTemplate_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;
            using (var trans = new esTransactionScope())
            {
                var templateNo = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][TransPrescriptionTemplateMetadata.ColumnNames.TemplateNo]);
                var entity = new TransPrescriptionTemplate();
                if (entity.LoadByPrimaryKey(templateNo))
                {
                    var tpit = new TransPrescriptionItemTemplateCollection();
                    tpit.Query.Where(tpit.Query.TemplateNo == templateNo);
                    tpit.LoadAll();
                    tpit.MarkAllAsDeleted();
                    tpit.Save();

                    entity.MarkAsDeleted();
                    entity.Save();

                    trans.Complete();
                }

            }
            Session["prescTemplate"] = null;
            grdPrescriptionTemplate.DataSource = null;
            grdPrescriptionTemplate.Rebind();
        }
        protected void grdPrescriptionTemplate_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (AppSession.UserLogin.SRUserType == AppUser.UserType.Doctor && !string.IsNullOrEmpty(AppSession.UserLogin.ParamedicID))
            {
                grdPrescriptionTemplate.DataSource = PrescriptionTemplateDataTable(AppSession.UserLogin.ParamedicID);
            }
        }

        private DataTable PrescriptionTemplateDataTable(string paramedicID)
        {
            if (IsPostBack && Session["prescTemplate"] != null)
            {
                return (DataTable)Session["prescTemplate"];
            }
            var qr = new TransPrescriptionTemplateQuery("r");
            qr.Select(qr.TemplateNo, qr.TemplateName);
            qr.Where(qr.ParamedicID == paramedicID);
            qr.OrderBy(qr.TemplateName.Ascending);

            qr.Select(qr.TemplateNo, qr.TemplateName);
            var dtbTemplate = qr.LoadDataTable();
            dtbTemplate.Columns.Add(new DataColumn("PrescriptionTemplate", typeof(string)));

            foreach (DataRow rowHd in dtbTemplate.Rows)
            {
                var tno = rowHd["TemplateNo"].ToString();
                var tname = rowHd["TemplateName"].ToString();

                var template = PrescriptionTemplateDetailInHTML(tno, tname);
                if (string.IsNullOrEmpty(template))
                {
                    continue;
                }
                rowHd["PrescriptionTemplate"] = template;
            }

            //Jangan diganti nama sessionnya krn dipakai pada page new template
            Session["prescTemplate"] = dtbTemplate;
            return dtbTemplate;
        }

        private string PrescriptionTemplateDetailInHTML(string templateNo, string templateName)
        {
            var query = new TransPrescriptionItemTemplateQuery("a");
            var item = new ItemQuery("c");
            var consume = new ConsumeMethodQuery("e");
            var emb = new EmbalaceQuery("g");

            query.Select(query.TemplateNo, query.SequenceNo, query.ItemID, item.ItemName,
                query.ResultQty, query.SRItemUnit, consume.SRConsumeMethodName,
                query.IsRFlag.Coalesce("CAST(0 AS BIT)").As("IsRFlag"),
                query.IsCompound.Coalesce("CAST(0 AS BIT)").As("IsCompound"),
                emb.EmbalaceLabel, query.DosageQty, query.SRDosageUnit,
                query.EmbalaceQty, query.ConsumeQty, query.SRConsumeUnit,
                "<(a.ParentNo + a.SequenceNo) as ORDERKEY>",
                query.Notes
                );


            query.LeftJoin(item).On(query.ItemID == item.ItemID);
            query.LeftJoin(consume).On(query.SRConsumeMethod == consume.SRConsumeMethod);
            query.LeftJoin(emb).On(query.EmbalaceID == emb.EmbalaceID);
            query.Where(query.TemplateNo == templateNo);
            query.OrderBy("ORDERKEY", esOrderByDirection.Ascending);

            var dtbTemplateItem = query.LoadDataTable();

            var sbPresciption = new StringBuilder();
            var sbItem = new StringBuilder();
            sbPresciption.AppendLine("<table width='100%' cellpadding='0' cellspacing='0'>");
            foreach (DataRow row in dtbTemplateItem.Rows)
            {
                if (!Convert.ToBoolean(row["IsCompound"]))
                {
                    sbItem.AppendFormat("{0} {1} {2} {3} ({4} @ {5} {6} {7})<br />",
                        Convert.ToBoolean(row["IsRFlag"]) ? string.Format("<b>{0}</b>", @"R/") : "&nbsp;&nbsp;&nbsp;&nbsp;",
                        row["ItemName"],
                        row["ResultQty"],
                        StandardReference.GetItemName(AppEnum.StandardReference.ItemUnit, row["SRItemUnit"].ToString()),
                        row["SRConsumeMethodName"],
                        row["ConsumeQty"],
                        StandardReference.GetItemName(AppEnum.StandardReference.DosageUnit, row["SRConsumeUnit"].ToString()),
                        row["Notes"]);
                }
                else
                {
                    sbItem.AppendFormat("{0} {1} {2} {3} @ {4} {5} ({6} @ {7} {8} {9})<br />",
                        Convert.ToBoolean(row["IsRFlag"]) ? string.Format("<b>{0}</b>", @"R/") : "&nbsp;&nbsp;&nbsp;&nbsp;",
                        row["ItemName"],
                        row["EmbalaceQty"],
                        row["EmbalaceLabel"],
                        row["DosageQty"],
                        StandardReference.GetItemName(AppEnum.StandardReference.DosageUnit, row["SRDosageUnit"].ToString()),
                        row["SRConsumeMethodName"],
                        row["ConsumeQty"],
                        StandardReference.GetItemName(AppEnum.StandardReference.DosageUnit, row["SRConsumeUnit"].ToString()),
                        row["Notes"]);
                }
            }

            sbPresciption.AppendLine("<tr><td align='left' style='width:20px;padding-top: 4px;vertical-align:top;'>"); ;

            if (DataModeCurrent == AppEnum.DataMode.New)
            {
                // Mode new bisa copy dari history prescription
                sbPresciption.AppendFormat("<div title='Copy to Current Entry'><a href='javascript:void(0);' onclick=\"javascript:showCopyPrescription('{0}',true)\"><img src='../../../../../Images/Toolbar/ordering16.png' alt='Copy' /></a></div></br>", templateNo);
            }
            sbPresciption.AppendLine("</td><td align='left'>");
            sbPresciption.AppendFormat("<fieldset><legend>{0}</legend>{1}</fieldset></br>", templateName, sbItem);
            sbPresciption.AppendLine("</td></tr>");

            sbPresciption.AppendLine("</table>");


            return sbPresciption.ToString();
        }
        #endregion

        protected void cboServiceUnitID_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ComboBox.PopulateWithServiceUnitForLocation(cboLocationID, e.Value);
            SetMainLocationID(e.Value);
        }

        private void SetMainLocationID(string serviceUnitID)
        {
            var locID = string.Empty;
            if (!AppParameter.IsYes(AppParameter.ParameterItem.IsPrescOrderLocUseMainLoc))
            {
                var reg = RegistrationCurrent;

                var su = new ServiceUnit();
                if (su.LoadByPrimaryKey(reg.ServiceUnitID))

                    locID = su.LocationPharmacyID;
            }

            if (string.IsNullOrWhiteSpace(locID))
                locID = (new ServiceUnit()).GetMainLocationId(serviceUnitID);

            if (string.IsNullOrEmpty(locID))
                cboLocationID.SelectedIndex = 0;
            else
                ComboBox.SelectedValue(cboLocationID, locID);
        }

        public void SetPrescUnitAndLocationFromGuar()
        {
            var reg = RegistrationCurrent;

            var guar = new Guarantor();
            if (guar.LoadByPrimaryKey(reg.GuarantorID))
            {
                string suid = "", locid = "";
                switch (reg.SRRegistrationType.ToUpper())
                {
                    case "IPR":
                        {
                            if (!string.IsNullOrEmpty(guar.PrescriptionServiceUnitIdIPR) &&
                                !string.IsNullOrEmpty(guar.PrescriptionLocationIdIPR))
                            {
                                suid = guar.PrescriptionServiceUnitIdIPR;
                                locid = guar.PrescriptionLocationIdIPR;
                            }
                            break;
                        }
                    case "EMR":
                        {
                            if (!string.IsNullOrEmpty(guar.PrescriptionServiceUnitIdEMR) &&
                                !string.IsNullOrEmpty(guar.PrescriptionLocationIdEMR))
                            {
                                suid = guar.PrescriptionServiceUnitIdEMR;
                                locid = guar.PrescriptionLocationIdEMR;
                            }
                            break;
                        }
                    default:
                        {
                            if (!string.IsNullOrEmpty(guar.PrescriptionServiceUnitIdOPR) &&
                                !string.IsNullOrEmpty(guar.PrescriptionLocationIdOPR))
                            {
                                suid = guar.PrescriptionServiceUnitIdOPR;
                                locid = guar.PrescriptionLocationIdOPR;
                            }
                            break;
                        }
                }
                if (!string.IsNullOrEmpty(suid))
                {
                    cboServiceUnitID.SelectedValue = suid;
                    cboServiceUnitID_SelectedIndexChanged(cboServiceUnitID,
                        new RadComboBoxSelectedIndexChangedEventArgs(
                            cboServiceUnitID.Text, cboServiceUnitID.Text,
                            cboServiceUnitID.SelectedValue, cboServiceUnitID.SelectedValue));
                    cboLocationID.SelectedValue = locid;
                }
            }
        }

        protected void grdTransPrescriptionItem_ItemCommand(object sender, GridCommandEventArgs e)
        {
            // Hanya 1 row edit atau insert
            if (e.CommandName == RadGrid.EditCommandName)
            {
                grdTransPrescriptionItem.MasterTableView.IsItemInserted = false;
            }
            if (e.CommandName == RadGrid.InitInsertCommandName)
            {
                grdTransPrescriptionItem.MasterTableView.ClearEditItems();
            }
        }


        protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
        {
            base.RaisePostBackEvent(source, eventArgument);

            if (string.IsNullOrEmpty(eventArgument))
                return;
            if (!(source is RadGrid))
                return;
            var grd = (RadGrid)source;

            if (grd.ID == "grdTransPrescriptionItem")
            {
                if (tblTemporaryBill.Visible)
                {
                    txtTemporaryBillTotal.Value = Convert.ToDouble(GetTotalTemporaryBill());
                }

                grd.Rebind();
            }
        }

        #region Temporary Bill

        private decimal GetTotalTemporaryBill()
        {
            var tpiColl = TransPrescriptionItems;
            var tpiTemps = TransPrescriptionItemTemporaries;
            var tciTemps = TransChargesItemTemporaries;
           
            decimal tbilling = 0;

            foreach (var i in tpiColl)
            {
                tbilling = tbilling + (i.LineAmount ?? 0);
            }

            foreach (var i in tpiTemps)
            {
                tbilling = tbilling + (i.LineAmount ?? 0);
            }

            foreach (var i in tciTemps)
            {
                tbilling = tbilling + (((i.ChargeQuantity ?? 0) * (i.Price ?? 0)) - (i.DiscountAmount ?? 0) + (i.CitoAmount ?? 0));
            }

            return tbilling;
        }


        private TransChargesItemCollection TransChargesItemTemporaries
        {
            get
            {
                if (IsPostBack)
                {
                    var obj = Session["collTransChargesItemTemporaries" + Request.UserHostName];
                    if (obj != null)
                        return ((TransChargesItemCollection)(obj));
                }

                var regnos = Helper.MergeBilling.GetMergeRegistration(Request.QueryString["regno"].ToString());

                var transNos = new TransChargesQuery();
                transNos.Where(transNos.RegistrationNo.In(regnos),
                    transNos.Or(transNos.PackageReferenceNo == string.Empty, transNos.PackageReferenceNo.IsNull()),
                    transNos.IsVoid == false);
                transNos.Select(transNos.TransactionNo);

                var coll = new TransChargesItemCollection();
                var query = new TransChargesItemQuery("a");

                query.Where(query.TransactionNo.In(transNos), query.IsVoid == false, query.Or(query.ParentNo == string.Empty, query.ParentNo.IsNull()));
                coll.Load(query);

                Session["collTransChargesItemTemporaries" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collTransChargesItemTemporaries" + Request.UserHostName] = value; }
        }

        private TransPrescriptionItemCollection TransPrescriptionItemTemporaries
        {
            get
            {
                if (IsPostBack)
                {
                    var obj = Session["collTransPrescriptionItemTemporaries" + Request.UserHostName];
                    if (obj != null)
                        return ((TransPrescriptionItemCollection)(obj));
                }

                var regnos = Helper.MergeBilling.GetMergeRegistration(Request.QueryString["regno"].ToString());

                var prescNos = new TransPrescriptionQuery();
                prescNos.Where(prescNos.PrescriptionNo != txtPrescriptionNo.Text, prescNos.RegistrationNo.In(regnos), prescNos.IsVoid == false);
                prescNos.Select(prescNos.PrescriptionNo);

                var coll = new TransPrescriptionItemCollection();
                var query = new TransPrescriptionItemQuery("a");

                query.Where(query.PrescriptionNo.In(prescNos), query.IsVoid == false);
                coll.Load(query);

                Session["collTransPrescriptionItemTemporaries" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collTransPrescriptionItemTemporaries" + Request.UserHostName] = value; }
        }
        #endregion
    }
}
