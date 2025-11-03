using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Emr
{
    public partial class PrescriptionItemDetail : BaseUserControl
    {
        protected string DisplayPrice
        {
            get { return AppSession.Parameter.IsShowPrescPriceOnDisplayDoctor ? "block" : "none"; }
        }

        protected RadTextBox txtPrescriptionNo
        {
            get {
                return Helper.FindControlRecursive(Page, "txtPrescriptionNo") as RadTextBox;
            }
        }

        protected RadComboBox cboServiceUnitID
        {
            get
            {
                return Helper.FindControlRecursive(Page, "cboServiceUnitIDPresc") as RadComboBox;
            }
        }

        #region cboItemID

        protected void cboItemID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var reg = new Registration();
            reg.LoadByPrimaryKey(Request.QueryString["regno"]);

            // Show Antibiotik warning
            if (IsRasproEnableApplied)
                customValidator.Validate();

            var grr = new Guarantor();
            grr.LoadByPrimaryKey(reg.GuarantorID);

            cboDosageUnit.Items.Clear();
            cboDosageUnit.Text = string.Empty;
            cboDosageUnit.SelectedValue = string.Empty;

            var ipm = new ItemProductMedic();
            ipm.LoadByPrimaryKey(e.Value);

            txtItemUnit.Text = ipm.SRItemUnit;
            lblFornasRestrictionNotes.Text = ipm.FornasRestrictionNotes;

            ViewState["CostPrice"] = ipm.CostPrice;

            if (!(chkIsCompound.Checked ?? false))
            {
                cboConsumeMethod.SelectedValue = string.Empty;
                cboConsumeMethod.Text = string.Empty;

                if (!string.IsNullOrEmpty(ipm.SRConsumeMethod))
                    ComboBox.SelectedValue(cboConsumeMethod, ipm.SRConsumeMethod);
            }


            // cboDosageUnit dientry jika tipe Compound untuk Formula Unit
            if (chkIsCompound.Checked ?? false)
            {
                //cboDosageUnit.Items.Clear();
                // Tambahkan SRItemUnit
                if (!string.IsNullOrEmpty(ipm.SRItemUnit))
                {
                    cboDosageUnit.Items.Add(new RadComboBoxItem(ipm.SRItemUnit, ipm.SRItemUnit));
                }

                // Tambahkan ipm.SRDosageUnit
                if (!string.IsNullOrEmpty(ipm.SRDosageUnit) && (ipm.SRItemUnit != ipm.SRDosageUnit))
                {
                    cboDosageUnit.Items.Add(new RadComboBoxItem(ipm.SRDosageUnit, ipm.SRDosageUnit));
                    cboDosageUnit.SelectedValue = ipm.SRDosageUnit;
                }

                // Tambahkan DosageUnit lainnya
                var dosages = new ItemProductDosageDetailCollection();
                dosages.Query.Where(dosages.Query.ItemID == e.Value);
                dosages.LoadAll();

                foreach (var dosage in dosages)
                {
                    // Tambahkan yg belum terdaftar saja
                    if (!cboDosageUnit.Items.Select(c => c.Value).Contains(dosage.SRDosageUnit))
                    {
                        cboDosageUnit.Items.Add(new RadComboBoxItem(dosage.SRDosageUnit, dosage.SRDosageUnit));
                    }
                }

                // Set default value
                if (!string.IsNullOrEmpty(ipm.SRItemUnit))
                    ComboBox.SelectedValue(cboDosageUnit, ipm.SRItemUnit);

                // Default Consume Method untuk obat racikan diset saat pilih Embalace Unit
            }
            else
            {
                // Default Consume Method untuk obat paten
                if (!string.IsNullOrEmpty(ipm.SRItemUnit))
                    PopulateConsumeUnitWith(ipm.SRItemUnit);

                // Isi Formula Unit hanya untuk mencegah validator memunculkan pesan
                cboDosageUnit.Items.Clear();
                cboDosageUnit.Text = string.Empty;
                cboDosageUnit.SelectedValue = string.Empty;
                cboDosageUnit.Items.Add(new RadComboBoxItem(ipm.SRItemUnit, ipm.SRItemUnit));
            }


            ViewState["InitialPrice"] = (double)Helper.Tariff.GetItemTariffNonMargin(grr.SRTariffType, DateTime.Now.Date, reg.ChargeClassID,
                e.Value, (chkIsCompound.Checked ?? false), ipm.SRItemUnit);

            ShowPrevBuy(RegistrationNo, e.Value);

            if ((chkIsCompound.Checked ?? false) && !string.IsNullOrEmpty(cboParentNo.SelectedValue))
            {
                txtDosage.Focus();
            }
            else
            {
                txtQty.Focus();
            }
        }
        #endregion

        #region cboConsumeUnit
        protected void cboConsumeUnit_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            cboConsumeUnit.Items.Clear();

            var query = new AppStandardReferenceItemQuery();
            query.Select(query.ItemID, query.ItemName);
            query.Where
                (
                query.StandardReferenceID == AppEnum.StandardReference.DosageUnit,
                query.IsActive == true,
                query.Or(
                    query.ItemID.Like(string.Format("{0}%", e.Text)),
                    query.ItemName.Like(string.Format("{0}%", e.Text))
                    )
                );

            var dtb = query.LoadDataTable();

            // Jika obat racikan maka tambahkan unitnya dg master Embalace
            if ((chkIsCompound.Checked ?? false))
            {
                var emb = new EmbalaceQuery();
                emb.Select(emb.EmbalaceID.As("ItemID"), emb.EmbalaceName.As("ItemName"));
                dtb.Merge(emb.LoadDataTable());
            }

            cboConsumeUnit.DataSource = dtb;
            cboConsumeUnit.DataBind();
        }

        protected void cboConsumeUnit_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }
        #endregion

        public object DataItem { get; set; }

        protected override void OnDataBinding(EventArgs e)
        {
            var embs = new EmbalaceCollection();
            embs.LoadAll();

            foreach (var emb in embs)
            {
                cboEmbalace.Items.Add(new RadComboBoxItem(emb.EmbalaceLabel, emb.EmbalaceID));
            }

            var consumes = new ConsumeMethodCollection();
            consumes.Query.OrderBy(consumes.Query.LineNumber.Ascending, "<LEFT(SRConsumeMethodName, 5)>");
            consumes.Query.Where(consumes.Query.IsActive == true);

            //StandardReference.InitializeIncludeSpace(cboAcPcDc, AppEnum.StandardReference.MedicationConsume);
            //StandardReference.InitializeIncludeSpaceSortByLineNumber(cboAcPcDc, AppEnum.StandardReference.MedicationConsume);

            rfvAcPcDc.Visible = AppSession.Parameter.IsMandatoryConsTime;
            hdnQty23Days.Value = "0";

            if (DataItem is GridInsertionObject)
            {
                consumes.Query.Where(consumes.Query.IsActive == true);
                consumes.LoadAll();
                foreach (var consume in consumes)
                {
                    //cboConsumeMethod.Items.Add(new RadComboBoxItem(consume.SRConsumeMethodName, consume.SRConsumeMethod));
                    cboConsumeMethod.Items.Add(new RadComboBoxItem(consume.SygnaText + " (" + consume.SRConsumeMethodName + ")", consume.SRConsumeMethod));
                }

                ViewState["IsNewRecord"] = true;

                var coll = (TransPrescriptionItemCollection)Session["collTransPrescriptionItem" + Request.UserHostName];
                if (!coll.HasData || coll.Count == 0)
                    ViewState["SequenceNo"] = "d01";
                else
                    ViewState["SequenceNo"] = "d" + string.Format("{0:00}", int.Parse(coll[coll.Count - 1].SequenceNo.Substring(1, 2)) + 1);

                txtStartDateTime.SelectedDate = DateTime.Now;

                ApplyIsCompound();
                return;
            }

            consumes.LoadAll();
            foreach (var consume in consumes)
            {
                //cboConsumeMethod.Items.Add(new RadComboBoxItem(consume.SRConsumeMethodName, consume.SRConsumeMethod));
                cboConsumeMethod.Items.Add(new RadComboBoxItem(consume.SygnaText + " (" + consume.SRConsumeMethodName + ")", consume.SRConsumeMethod));
            }

            ViewState["IsNewRecord"] = false;
            ViewState["SequenceNo"] = DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.SequenceNo);

            chkIsCompound.Checked = (bool)DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.IsCompound);
            ApplyIsCompound();

            ComboBox.SelectedValue(cboParentNo, (String)DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.ParentNo));

            ComboBox.PopulateWithOneItem(cboItemID, (String)DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.ItemID));

            txtItemUnit.Display = !(chkIsCompound.Checked ?? false);
            txtItemUnit.Text = (String)DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.SRItemUnit);


            //if (cboEmbalace.Visible)
            if ((chkIsCompound.Checked ?? false))
                ComboBox.SelectedValue(cboEmbalace, (String)DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.EmbalaceID));

            txtQty.Text = (string)DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.ItemQtyInString);
            txtDosage.Text = (string)DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.DosageQty);
            ComboBox.SelectedValue(cboConsumeMethod, (String)DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.SRConsumeMethod));

            ViewState["CostPrice"] = Convert.ToDouble(DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.CostPrice));
            ViewState["InitialPrice"] = Convert.ToDouble(DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.InitialPrice));


            var ipm = new ItemProductMedic();
            ipm.LoadByPrimaryKey(cboItemID.SelectedValue);

            if (!string.IsNullOrEmpty(ipm.SRItemUnit))
                cboDosageUnit.Items.Add(new RadComboBoxItem(ipm.SRItemUnit, ipm.SRItemUnit));
            if (!string.IsNullOrEmpty(ipm.SRDosageUnit))
                cboDosageUnit.Items.Add(new RadComboBoxItem(ipm.SRDosageUnit, ipm.SRDosageUnit));

            lblFornasRestrictionNotes.Text = ipm.FornasRestrictionNotes;

            decimal? factor = 1;
            if (!string.IsNullOrEmpty(ipm.SRDosageUnit))
            {
                cboDosageUnit.Items.Add(new RadComboBoxItem(ipm.SRDosageUnit, ipm.SRDosageUnit));
                ComboBox.SelectedValue(cboDosageUnit, ipm.SRDosageUnit);
                factor = ipm.Dosage;
            }

            var dosages = new ItemProductDosageDetailCollection();
            dosages.Query.Where(dosages.Query.ItemID == cboItemID.SelectedValue);
            dosages.LoadAll();

            foreach (var dosage in dosages)
            {
                cboDosageUnit.Items.Add(new RadComboBoxItem(dosage.SRDosageUnit, dosage.SRDosageUnit));
            }

            ComboBox.SelectedValue(cboDosageUnit, (String)DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.SRDosageUnit));
            //ComboBox.SelectedValue(cboAcPcDc, (String)DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.Acpcdc));
            ComboBox.PopulateWithOneStandardReference(cboMedicationConsume,"MedicationConsume",(String)DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.Acpcdc));


            txtConsumeQty.Text = (String)DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.ConsumeQty);


            // Consume Unit
            var consumeUnit = (String)DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.SRConsumeUnit);
            PopulateConsumeUnitWith(consumeUnit);
            //var std = new AppStandardReferenceItemQuery();
            //std.Select(std.ItemID, std.ItemName);
            //std.Where
            //    (
            //    std.StandardReferenceID == AppEnum.StandardReference.DosageUnit,
            //    std.ItemID == consumeUnit
            //    );

            //var dtb = std.LoadDataTable();

            //if ((chkIsCompound.Checked ?? false))
            //{
            //    var emb = new EmbalaceQuery();
            //    emb.Where(emb.EmbalaceID == consumeUnit);
            //    emb.Select(emb.EmbalaceID.As("ItemID"), emb.EmbalaceName.As("ItemName"));
            //    dtb.Merge(emb.LoadDataTable());
            //}
            //foreach (DataRow row in dtb.Rows)
            //{
            //    cboConsumeUnit.Items.Add(new RadComboBoxItem(row["ItemName"].ToString(), row["ItemID"].ToString()));
            //}
            //ComboBox.SelectedValue(cboConsumeUnit, consumeUnit);

            txtIter.Text = (String)DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.IterText);
            txtNotes.Text = (String)DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.Notes);
            txtLineAmount.Value = Convert.ToDouble(DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.LineAmount));
            try
            {
                hdnQty23Days.Value = (String)DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.Qty23Days);
            }
            catch
            {
                
            }

            var startDT = DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.StartDateTime);
            if (startDT != null)
                txtStartDateTime.SelectedDate = (DateTime)startDT;

            ShowPrevBuy(Request.QueryString["regno"], cboItemID.SelectedValue);

        }

        private void ApplyIsCompound()
        {
            var isCompound = (chkIsCompound.Checked ?? false);
            // Visible diganti display krn ctl nya dipakai javascript

            // Parent Compound
            cboParentNo.Style.Add("display", isCompound ? "block" : "none");

            txtItemUnit.Display = !isCompound;
            cboEmbalace.Style.Add("display", isCompound ? "block" : "none");

            cboItemID.Width = isCompound ? new Unit(350, UnitType.Pixel) : new Unit(400, UnitType.Pixel);

            // Formula
            txtDosage.Display = isCompound;
            cboDosageUnit.Style.Add("display", isCompound ? "block" : "none");
            valFormulaQty.Enabled = isCompound;
            valFormulaDosageUnit.Enabled = isCompound;

            lblHeader.Text = isCompound ? "Header" : string.Empty;
            lblFormula.Text = isCompound ? "Formula" : string.Empty;

            if ((chkIsCompound.Checked ?? false))
            {
                PopulateParentNo();

                var emb = new EmbalaceQuery();
                emb.Select(emb.EmbalaceID.As("ItemID"), emb.EmbalaceName.As("ItemName"));
                var dtb = emb.LoadDataTable();
                foreach (DataRow row in dtb.Rows)
                {
                    cboConsumeUnit.Items.Add(new RadComboBoxItem(row["ItemName"].ToString(), row["ItemID"].ToString()));
                }
            }

            pnlInfoLastBuy.Visible = false;


        }

        private string RegistrationNo
        {
            get
            {
                return Request.QueryString["regno"];
            }
        }
        private string RegistrationType
        {
            get
            {
                return Request.QueryString["rt"];
            }
        }

        bool? _isRasproEnableApplied = null;
        private bool IsRasproEnableApplied
        {
            get
            {
                if (_isRasproEnableApplied==null)
                    _isRasproEnableApplied = Convert.ToBoolean((Helper.FindControlRecursive(Page, "hdnIsRasproEnableApplied") as HiddenField).Value);

                return _isRasproEnableApplied ?? false;
            }
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (string.IsNullOrEmpty(cboItemID.SelectedValue))
            {
                args.IsValid = false;
                customValidator.ErrorMessage = "Item name is not selected properly";
                return;
            }

            var isFromSelectionItemValidation = Request.Form["__EVENTTARGET"].Contains("$cboItemID");

            var reg = new Registration();
            reg.LoadByPrimaryKey(Request.QueryString["regno"]);

            // Check AB restriction
            if (IsRasproEnableApplied)
            {
                var presType = (Helper.FindControlRecursive(Page, "cboSRPrescriptionCategory") as RadComboBox).SelectedValue;

                if (!(AppParameter.GetParameterValue(AppParameter.ParameterItem.PrescriptionCategoryHomePresID).Equals(presType))) // Obat pulang AB nya tidak dibatasi
                {
                    var ipm = new ItemProductMedic();
                    var abrForLine = AppParameter.GetParameterValue(AppParameter.ParameterItem.AntibioticRestrictionForLine);
                    if (ipm.LoadByPrimaryKey(cboItemID.SelectedValue) && (ipm.IsAntibiotic ?? false) && (string.IsNullOrEmpty(abrForLine) || abrForLine==ipm.SRAntibioticLine))
                    {
                            var abLevel = (Helper.FindControlRecursive(Page, "hdnAbLevel") as HiddenField).Value.ToInt();
                            var abRestrictionID = (Helper.FindControlRecursive(Page, "hdnAbRestrictionID") as HiddenField).Value;
                            var isRasproIsNew = (Helper.FindControlRecursive(Page, "hdnRasproIsNew") as HiddenField).Value.Equals("1");
                            var rasproSeqNo = (Helper.FindControlRecursive(Page, "hdnRasproSeqNo4Filter") as HiddenField).Value.ToInt();

                            var abValidationMesg = string.Empty;
                            var rr = new RegistrationRaspro();
                            if (rasproSeqNo > 0)
                                rr.LoadByPrimaryKey(RegistrationNo, rasproSeqNo);

                            var isValid = AbRestriction.IsAllowed(RegistrationNo, cboItemID.SelectedValue, rr, isRasproIsNew, AppSession.Parameter.IsAntibioticRestriction, abrForLine, ref abValidationMesg);

                            args.IsValid = isValid;
                            if (!string.IsNullOrWhiteSpace(abValidationMesg))
                            {
                                customValidator.ErrorMessage = abValidationMesg;
                                if (isFromSelectionItemValidation)
                                    args.IsValid = false; // Override supaya mesage di validationsummary nya muncul
                            }

                            if (!isValid) return;

                    }
                }
            }

            if (isFromSelectionItemValidation) return; // Return jika validate spesifik dipanggil dari cboItemID


            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (TransPrescriptionItemCollection)Session["collTransPrescriptionItem" + Request.UserHostName];
                var isExist =
                    coll.Any(
                        entity =>
                        entity.ItemID.Equals(cboItemID.SelectedValue) &&
                        entity.ParentNo.Equals(cboParentNo.SelectedValue) &&
                        entity.IsCompound.Equals((chkIsCompound.Checked ?? false)));
                if (isExist)
                {
                    args.IsValid = false;
                    customValidator.ErrorMessage = string.Format("Item: {0} has exist", cboItemID.Text);
                    return;
                }
            }

            if (!string.IsNullOrWhiteSpace(txtConsumeQty.Text) && (new Fraction(txtConsumeQty.Text)).Result.ToDecimal() == 0)
            {
                args.IsValid = false;
                customValidator.ErrorMessage = "Consume Qty not valid";
                return;
            }
            if (string.IsNullOrEmpty(cboConsumeMethod.SelectedValue))
            {
                args.IsValid = false;
                customValidator.ErrorMessage = "Please choose one of Consume Method";
                return;
            }
            if (string.IsNullOrEmpty(cboConsumeUnit.SelectedValue))
            {
                args.IsValid = false;
                customValidator.ErrorMessage = "Please choose one of Consume Unit";
                return;
            }
            if (AppSession.Parameter.IsMandatoryConsTime && string.IsNullOrEmpty(cboMedicationConsume.SelectedValue))
            {
                args.IsValid = false;
                customValidator.ErrorMessage = "Please choose one of Cons. Time";
                return;
            }

            // Compound
            if (chkIsCompound.Checked ?? false)
            {
                if (string.IsNullOrEmpty(cboDosageUnit.SelectedValue))
                {
                    args.IsValid = false;
                    customValidator.ErrorMessage = "Please choose one of Formula Dosage Unit";
                    return;
                }

                // Fixed decimal separator
                txtDosage.Text = txtDosage.Text.Replace(',', '.');
                if (double.IsNaN(txtDosage.Text.ParseToDouble()))
                {
                    args.IsValid = false;
                    customValidator.ErrorMessage = "Please specify a digit for Formula Qty";
                    return;
                }
            }
            else
            {
                // Fixed decimal separator
                txtQty.Text = txtQty.Text.Replace(',', '.');
                if (double.IsNaN(txtQty.Text.ParseToDouble()))
                {
                    args.IsValid = false;
                    customValidator.ErrorMessage = "Please specify a digit for Dispense Qty";
                    return;
                }
            }

            //db:20240628 - pengecekan peresepan maksimal khusus pasien BPJS yg disesuaikan dg settingan di master item product medic
            if (Helper.GuarantorBpjsCasemix.Contains(reg.GuarantorID))
            {
                var maxOrderQty = 0;
                bool isChronic = false;

                var ipm = new ItemProductMedic();
                if (ipm.LoadByPrimaryKey(cboItemID.SelectedValue))
                {
                    isChronic = ipm.IsChronic ?? false;
                    switch (reg.SRRegistrationType)
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
                        args.IsValid = false;
                        customValidator.ErrorMessage = string.Format("The maximum qty of {0} drugs that can be prescribed are {1} {2}.", c, maxOrderQty.ToString(), txtItemUnit.Text);
                        return;
                    }

                    //2. pengecekan khusus pasien rawat jalan u/ obat kronis jumlahnya tidak boleh melebihi qty dalam periode 1 bulan terakhir
                    if (reg.SRRegistrationType == "OPR" && isChronic)
                    {
                        var i = AppSession.Parameter.MaxChronicDrugPrescriptionInDays;
                        if (i > 0)
                        {
                            DateTime fdate = DateTime.Now.AddDays(-1 * i).Date;
                            var tpiq = new TransPrescriptionItemQuery("a");
                            var tpq = new TransPrescriptionQuery("b");
                            var rq = new RegistrationQuery("c");
                            tpiq.InnerJoin(tpq).On(tpq.PrescriptionNo == tpiq.PrescriptionNo && tpq.IsVoid == false && tpq.IsPrescriptionReturn == false);
                            tpiq.InnerJoin(rq).On(rq.RegistrationNo == tpq.RegistrationNo && rq.PatientID == reg.PatientID && rq.SRRegistrationType == "OPR");
                            tpiq.Where(tpq.PrescriptionNo != txtPrescriptionNo.Text, tpq.PrescriptionDate >= fdate, tpq.PrescriptionDate <= DateTime.Now, tpiq.ItemID == cboItemID.SelectedValue);
                            tpiq.Select(tpiq.ItemID, tpiq.TakenQty.Sum().Coalesce("0").As("Qty"));
                            tpiq.GroupBy(tpiq.ItemID);

                            DataTable dtb = tpiq.LoadDataTable();
                            if (dtb.Rows.Count > 0)
                            {
                                var takenQty = Convert.ToDecimal(dtb.Rows[0]["Qty"]);
                                if (takenQty + ResultQty > maxOrderQty)
                                {
                                    args.IsValid = false;
                                    customValidator.ErrorMessage = string.Format("The maximum qty of {0} drugs that can be prescribed for {3} days are {1} {2}  (*previously prescribed: {4} {2}).", "chronic", maxOrderQty.ToString(), txtItemUnit.Text, i.ToString(), takenQty.ToInt().ToString());
                                    return;
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
                        tpiq.InnerJoin(rq).On(rq.RegistrationNo == tpq.RegistrationNo && rq.PatientID == reg.PatientID);
                        tpiq.Where(tpq.PrescriptionNo != txtPrescriptionNo.Text, tpq.RegistrationNo == RegistrationNo, tpiq.ItemID == cboItemID.SelectedValue);
                        tpiq.Select(tpiq.ItemID, tpiq.TakenQty.Sum().Coalesce("0").As("Qty"));
                        tpiq.GroupBy(tpiq.ItemID);

                        DataTable dtb = tpiq.LoadDataTable();
                        if (dtb.Rows.Count > 0)
                        {
                            var takenQty = Convert.ToDecimal(dtb.Rows[0]["Qty"]);
                            if (takenQty + ResultQty > maxOrderQty)
                            {
                                args.IsValid = false;
                                customValidator.ErrorMessage = string.Format("The maximum qty of {0} drugs that can be prescribed for this registration are {1} {2}  (*already prescribed: {3} {2}).", "non chronic", maxOrderQty.ToString(), txtItemUnit.Text, takenQty.ToInt().ToString());
                                return;
                            }
                        }
                    }
                }
            }
        }

        protected void chkIsCompound_CheckedChanged(object sender, EventArgs e)
        {
            ApplyIsCompound();
        }

        private void PopulateParentNo()
        {
            cboParentNo.Items.Clear();

            //if (chkIsCompound.Checked) return;
            cboParentNo.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));

            var coll = (TransPrescriptionItemCollection)Session["collTransPrescriptionItem" + Request.UserHostName];
            foreach (var row in coll.Where(row => row.ParentNo == string.Empty && (row.IsCompound ?? false)))
            {
                cboParentNo.Items.Add(new RadComboBoxItem(row.ItemName, row.SequenceNo));
            }

            if (string.IsNullOrEmpty(cboParentNo.SelectedValue)) return;
            var entity = ((TransPrescriptionItemCollection)Session["collTransPrescriptionItem" + Request.UserHostName]).SingleOrDefault(t => t.SequenceNo == cboParentNo.SelectedValue);

            txtQty.Text = (chkIsCompound.Checked ?? false) ? entity.EmbalaceQty : entity.ResultQty.ToString();
            txtQty.ReadOnly = true;

            if ((chkIsCompound.Checked ?? false))
            {
                cboEmbalace.SelectedValue = entity.EmbalaceID;
                cboEmbalace.Enabled = false;
            }

            cboConsumeMethod.SelectedValue = entity.SRConsumeMethod;
            cboConsumeMethod.Enabled = false;

            PopulateConsumeUnitWith(entity.SRConsumeUnit);
        }

        private void PopulateConsumeUnitWith(string itemUnitID)
        {
            var std = new AppStandardReferenceItemQuery();
            std.Select(std.ItemID, std.ItemName);
            std.Where
            (
                std.StandardReferenceID == AppEnum.StandardReference.DosageUnit,
                std.ItemID == itemUnitID
            );

            var dtb = std.LoadDataTable();
            if (dtb.Rows.Count == 0)
            {

                if ((chkIsCompound.Checked ?? false))
                {
                    var emb = new EmbalaceQuery();
                    emb.Where(emb.EmbalaceID == itemUnitID);
                    emb.Select(emb.EmbalaceID.As("ItemID"), emb.EmbalaceName.As("ItemName"));
                    dtb = emb.LoadDataTable();
                }
            }

            if (dtb.Rows.Count == 0)
            {
                var std2 = new AppStandardReferenceItemQuery();
                std2.Select(std2.ItemID, std2.ItemName);
                std2.Where(std2.StandardReferenceID == AppEnum.StandardReference.GlobalConsumeUnit, std2.ItemID == itemUnitID);
                dtb = std2.LoadDataTable();
            }

            if (dtb.Rows.Count == 0)
            {
                var std3 = new AppStandardReferenceItemQuery();
                std3.Select(std3.ItemID, std3.ItemName);
                std3.Where(std3.StandardReferenceID == AppEnum.StandardReference.ItemUnit, std3.ItemID == itemUnitID);
                dtb = std3.LoadDataTable();
            }

            cboConsumeUnit.Items.Clear();
            cboConsumeUnit.SelectedValue = string.Empty;
            cboConsumeUnit.Text = string.Empty;
            foreach (DataRow row in dtb.Rows)
            {
                cboConsumeUnit.Items.Add(new RadComboBoxItem(row["ItemName"].ToString(), row["ItemID"].ToString()));
            }
            ComboBox.SelectedValue(cboConsumeUnit, itemUnitID);
        }

        protected void cboParentNo_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Value)) return;
            var entity = ((TransPrescriptionItemCollection)Session["collTransPrescriptionItem" + Request.UserHostName]).SingleOrDefault(t => t.SequenceNo == e.Value);

            txtQty.Text = entity.EmbalaceQty;
            txtQty.ReadOnly = true;

            cboEmbalace.SelectedValue = entity.EmbalaceID;
            cboEmbalace.Enabled = false;

            cboConsumeMethod.SelectedValue = entity.SRConsumeMethod;
            cboConsumeMethod.Enabled = false;

            txtConsumeQty.Text = entity.ConsumeQty;
            txtConsumeQty.ReadOnly = true;

            // cboConsumeUnit
            DataTable dtbConsumeUnit = null;
            if ((chkIsCompound.Checked ?? false))
            {
                var emb = new EmbalaceQuery();
                emb.Where(emb.EmbalaceID == entity.SRConsumeUnit);
                emb.Select(emb.EmbalaceID.As("ItemID"), emb.EmbalaceName.As("ItemName"));
                dtbConsumeUnit = emb.LoadDataTable();
            }
            if (dtbConsumeUnit == null || dtbConsumeUnit.Rows.Count == 0)
            {
                var std = new AppStandardReferenceItemQuery();
                std.Select(std.ItemID, std.ItemName);
                std.Where
                    (
                    std.StandardReferenceID == AppEnum.StandardReference.DosageUnit,
                    std.ItemID == entity.SRConsumeUnit
                    );

                dtbConsumeUnit.Merge(std.LoadDataTable());
            }

            foreach (DataRow row in dtbConsumeUnit.Rows)
            {
                cboConsumeUnit.Items.Add(new RadComboBoxItem(row["ItemName"].ToString(), row["ItemID"].ToString()));
            }
            ComboBox.SelectedValue(cboConsumeUnit, entity.SRConsumeUnit);
            cboConsumeUnit.Enabled = false;

            // Direname menjadi cboMedicationConsume supaya lebih mudah dipahami (Handono 230325)
            //ComboBox.SelectedValue(cboAcPcDc, entity.Acpcdc);
            //cboAcPcDc.Enabled = false;
            ComboBox.PopulateWithOneStandardReference(cboMedicationConsume,"MedicationConsume",entity.Acpcdc);
            cboMedicationConsume.Enabled = false;

            txtIter.Text = entity.IterText;
            txtIter.Enabled = false;

            txtNotes.Text = AppParameter.IsYes(AppParameter.ParameterItem.IsNotesforCompoundPresc) ? entity.Notes : string.Empty;
            txtNotes.Enabled = false;

            cboItemID.Focus();
        }

        #region Properties for return entry value

        public String SRConsumeUnit
        {
            get { return cboConsumeUnit.SelectedValue; }
        }

        public String QtyConsume
        {
            get { return txtConsumeQty.Text; }
        }

        public String SequenceNo
        {
            get { return (string)ViewState["SequenceNo"]; }
        }

        public Boolean IsCompound
        {
            get { return (chkIsCompound.Checked ?? false); }
        }

        public String ParentNo
        {
            get { return cboParentNo.SelectedValue; }
        }

        public String ItemID
        {
            get { return cboItemID.SelectedValue; }
        }

        public String ItemName
        {
            get { return cboItemID.Text; }
        }
        public String AcPcDc
        {
            get { return cboMedicationConsume.SelectedValue; }
        }
        public String AcPcDcName
        {
            get { return cboMedicationConsume.Text; }
        }
        public String ItemUnit
        {
            get { return txtItemUnit.Text; }
        }

        public DateTime? StartDateTime
        {
            get { return txtStartDateTime.SelectedDate; }
        }

        public Decimal CostPrice
        {
            get { return Convert.ToDecimal(ViewState["CostPrice"]); }
        }

        public Decimal InitialPrice
        {
            get { return Convert.ToDecimal(ViewState["InitialPrice"]); }
        }

        public Decimal ResultQty
        {
            get
            {
                try
                {
                    return ResultQtyFrom(IsCompound, txtDosage.Text, txtQty.Text, DosageUnit, ItemID);

                }
                catch (Exception e)
                {
                    customValidator.ErrorMessage = e.Message;
                }

                return 0;
            }
        }

        public Decimal Qty23Days
        {
            get
            {
                try
                {
                    return Convert.ToDecimal(hdnQty23Days.Value);
                }
                catch (Exception e)
                {
                    
                }

                return 0;
            }
        }

        public static Decimal ResultQtyFrom(bool isCompound, string dosageQtyInString, string qtyInString, string dosageUnit, string itemID)
        {
            if (!isCompound) return Convert.ToDecimal(new Fraction(qtyInString));

            var dosageQty = DosageQtyFrom(isCompound, dosageQtyInString, qtyInString);

            if (string.IsNullOrEmpty(dosageQty)) return Convert.ToDecimal(new Fraction(qtyInString));

            var item = new ItemProductMedic();
            if (item.LoadByPrimaryKey(itemID))
            {
                if (item.SRItemUnit == dosageUnit)
                    return Convert.ToDecimal(new Fraction(dosageQty)) * Convert.ToDecimal(new Fraction(qtyInString));
                if (item.SRDosageUnit == dosageUnit)
                    return (Convert.ToDecimal(new Fraction(dosageQty)) / item.Dosage ?? 0) * Convert.ToDecimal(new Fraction(qtyInString));

                var detail = new ItemProductDosageDetailCollection();
                detail.Query.Where(detail.Query.ItemID == item.ItemID);
                detail.LoadAll();

                var dosage = detail.SingleOrDefault(d => d.SRDosageUnit == dosageUnit);
                if (dosage != null)
                    return (Convert.ToDecimal(new Fraction(dosageQty)) / dosage.Dosage ?? 0) * Convert.ToDecimal(new Fraction(qtyInString));

                return Convert.ToDecimal(new Fraction(qtyInString));
            }

            return Convert.ToDecimal(new Fraction(qtyInString));

        }


        //public Decimal ResultQty
        //{
        //    get
        //    {
        //        if (!IsCompound) return Convert.ToDecimal(new Fraction(txtQty.Text));
        //        if (string.IsNullOrEmpty(DosageQty)) return Convert.ToDecimal(new Fraction(txtQty.Text));

        //        var item = new ItemProductMedic();
        //        if (item.LoadByPrimaryKey(ItemID))
        //        {
        //            if (item.SRItemUnit == DosageUnit) 
        //                return Convert.ToDecimal(new Fraction(DosageQty)) * Convert.ToDecimal(new Fraction(txtQty.Text));
        //            if (item.SRDosageUnit == DosageUnit) 
        //                return (Convert.ToDecimal(new Fraction(DosageQty)) / item.Dosage ?? 0) * Convert.ToDecimal(new Fraction(txtQty.Text));

        //            var detail = new ItemProductDosageDetailCollection();
        //            detail.Query.Where(detail.Query.ItemID == item.ItemID);
        //            detail.LoadAll();

        //            var dosage = detail.SingleOrDefault(d => d.SRDosageUnit == DosageUnit);
        //            if (dosage != null) 
        //                return (Convert.ToDecimal(new Fraction(DosageQty)) / dosage.Dosage ?? 0) * Convert.ToDecimal(new Fraction(txtQty.Text));

        //            return Convert.ToDecimal(new Fraction(txtQty.Text));
        //        }

        //        return Convert.ToDecimal(new Fraction(txtQty.Text));

        //    }
        //}

        public string DosageQty
        {
            get { return DosageQtyFrom((chkIsCompound.Checked ?? false), txtDosage.Text, txtQty.Text); }
        }

        public static string DosageQtyFrom(bool isCompound, string qtyDosageInString, string qtyInString)
        {
            return isCompound ? qtyDosageInString : qtyInString;
        }

        public String ConsumeMethod
        {
            get { return cboConsumeMethod.SelectedValue; }
        }

        public String Iter
        {
            get { return txtIter.Text; }
        }

        public String ConsumeMethodName
        {
            get { return cboConsumeMethod.Text; }
        }

        public String DosageUnit
        {
            get { return cboDosageUnit.SelectedValue; }
        }

        public String Notes
        {
            get { return txtNotes.Text; }
        }

        public String EmbalaceID
        {
            get
            {
                return IsCompound ? cboEmbalace.SelectedValue : string.Empty;
            }
        }

        public decimal EmbalaceAmount
        {
            get
            {
                if (string.IsNullOrEmpty(EmbalaceID)) return 0;

                // Hanya dikenakan 1 embalace setiap 1 racikan dan embalace amount ditambahkan di item pertama saja
                if (IsCompound && !string.IsNullOrEmpty(ParentNo)) return 0;

                var emb = new Embalace();
                emb.LoadByPrimaryKey(EmbalaceID);
                return emb.EmbalaceFeeAmount ?? 0;
            }
        }

        public string EmbalaceQty
        {
            get
            {
                return IsCompound ? txtQty.Text : "0";
            }
        }

        public String EmbalaceLabel
        {
            get
            {
                return IsCompound ? cboEmbalace.Text : string.Empty;
            }
        }

        public string ItemQtyInString
        {
            get { return txtQty.Text; }
        }

        #endregion

        //private decimal PopulateResultQtyAndLineAmount()
        //{
        //    try
        //    {
        //        var resultQty = ResultQtyFrom(IsCompound, txtDosage.Text, txtQty.Text, DosageUnit, ItemID);
        //        var reg = new Registration();
        //        reg.LoadByPrimaryKey(Request.QueryString["regno"]);

        //        var grr = new Guarantor();
        //        grr.LoadByPrimaryKey(reg.GuarantorID);

        //        var price = Helper.Tariff.GetItemTariff(grr.SRTariffType, DateTime.Now.Date, reg.ClassID, reg.ChargeClassID,
        //            ItemID, IsCompound, ItemUnit, grr.GuarantorID, reg.SRRegistrationType);

        //        decimal recipeAmount = 0;
        //        if (!IsCompound)
        //        {
        //            recipeAmount = Convert.ToDecimal(AppParameter.GetParameterValue(AppParameter.ParameterItem.RecipeMarginValueNonCompound));
        //            return Helper.Rounding((resultQty * price) + recipeAmount, AppEnum.RoundingType.Prescription);
        //        }

        //        if (string.IsNullOrEmpty(ParentNo))
        //        {
        //            var margin = new RecipeMarginValue();
        //            margin.Query.Where(string.Format("<{0} BETWEEN StartingValue AND EndingValue>", resultQty));
        //            if (margin.Query.Load()) recipeAmount += margin.RecipeAmount ?? 0;
        //        }

        //        return Helper.Rounding((resultQty * price) + recipeAmount + EmbalaceAmount, AppEnum.RoundingType.Prescription);
        //    }
        //    catch (Exception e)
        //    {
        //        customValidator.ErrorMessage = e.Message;
        //    }

        //    return 0;
        //}


        #region Prescription Item
        //public static decimal GetResult(bool IsCompound, string ItemID, string Qty, string DosageQty, string DosageUnit)
        //{
        //    if (!IsCompound) return Convert.ToDecimal(new Fraction(Qty));
        //    if (string.IsNullOrEmpty(DosageQty)) return Convert.ToDecimal(new Fraction(Qty));

        //    var item = new ItemProductMedic();
        //    if (item.LoadByPrimaryKey(ItemID))
        //    {
        //        if (item.SRItemUnit == DosageUnit)
        //            return Convert.ToDecimal(new Fraction(DosageQty)) * Convert.ToDecimal(new Fraction(Qty));
        //        if (item.SRDosageUnit == DosageUnit)
        //            return (Convert.ToDecimal(new Fraction(DosageQty)) / item.Dosage ?? 0) * Convert.ToDecimal(new Fraction(Qty));

        //        //if (item.IsActualDeduct ?? false)
        //        //    return Convert.ToDecimal(new Fraction(Qty));

        //        var detail = new ItemProductDosageDetailCollection();
        //        detail.Query.Where(detail.Query.ItemID == item.ItemID);
        //        detail.LoadAll();

        //        var dosage = detail.SingleOrDefault(d => d.SRDosageUnit == DosageUnit);
        //        if (dosage != null)
        //            return (Convert.ToDecimal(new Fraction(DosageQty)) / dosage.Dosage ?? 0) * Convert.ToDecimal(new Fraction(Qty));
        //        return Convert.ToDecimal(new Fraction(Qty));
        //    }

        //    return Convert.ToDecimal(new Fraction(Qty));
        //}
        #endregion

        #region Prev Buy Info
        private class PrevPrescriptionInfo
        {
            public string ItemName;
            public string Qty;
            public string SRItemUnit;
            public string Date;
            public int DaysOfUsage;
            public System.Drawing.Color Color;
        }
        private void ShowPrevBuy(string registrationNo, string itemID)
        {
            if (AppSession.Parameter.IsPrescriptionLoadLastBought)
            {
                pnlInfoLastBuy.Visible = false;
                if (Request.QueryString["mode"] != "otc")
                {
                    //var txtPrescriptionNo = (RadTextBox)Helper.FindControlRecursive(Page, "txtPrescriptionNo");
                    var prev = CekPrevBuy(registrationNo, txtPrescriptionNo.Text, itemID);
                    if (string.IsNullOrEmpty(prev.ItemName))
                    {

                    }
                    else
                    {
                        pnlInfoLastBuy.Visible = true;
                        lblPrevItemName.ForeColor = prev.Color;
                        lblPrevItemQty.ForeColor = prev.Color;
                        lblPrevItemSRUnit.ForeColor = prev.Color;
                        lblPrevDate.ForeColor = prev.Color;

                        lblPrevItemName.Text = prev.ItemName;
                        lblPrevItemQty.Text = prev.Qty;
                        lblPrevItemSRUnit.Text = prev.SRItemUnit;
                        lblPrevDate.Text = prev.Date;
                    }
                }
            }
        }

        private static PrevPrescriptionInfo CekPrevBuy(string registrationNo, string prescriptionNo, string itemID)
        {
            // PREV TRANS
            var tp = new TransPrescriptionQuery("tp");
            var tpi = new TransPrescriptionItemQuery("tpi");
            var r1 = new RegistrationQuery("r1");
            var p1 = new PatientQuery("p1");
            var r2 = new RegistrationQuery("r2");
            var i = new ItemQuery("i");

            r1.InnerJoin(p1).On(r1.PatientID.Equal(p1.PatientID) && r1.RegistrationNo.Equal(registrationNo))
                .InnerJoin(r2).On(p1.PatientID.Equal(r2.PatientID) && r2.RegistrationDate <= r1.RegistrationDate)
                .InnerJoin(tp).On(tp.RegistrationNo.Equal(r2.RegistrationNo))
                .InnerJoin(tpi).On(tp.PrescriptionNo.Equal(tpi.PrescriptionNo))
                .InnerJoin(i).On(tpi.ItemID.Equal(i.ItemID))
                .Where(tpi.ItemID == itemID, tp.IsApproval.Equal(true), r2.IsVoid.Equal(false), tp.PrescriptionNo != prescriptionNo)
                .OrderBy(tp.PrescriptionDate.Descending)
                .Select(i.ItemID, i.ItemName, tpi.TakenQty, tpi.SRItemUnit,
                    tp.PrescriptionDate,
                    "<ISNULL(tpi.DaysOfUsage,0) DaysOfUsage>");
            r1.es.Top = 1;
            var dttbl = r1.LoadDataTable();

            var ret = new PrevPrescriptionInfo();

            foreach (System.Data.DataRow row in dttbl.Rows)
            {
                ret.ItemName = row["ItemName"].ToString();
                ret.Qty = row["TakenQty"].ToString();
                ret.SRItemUnit = row["SRItemUnit"].ToString();
                ret.Date = ((DateTime)row["PrescriptionDate"]).ToString("dd/MM/yyyy");
                ret.DaysOfUsage = (int)row["DaysOfUsage"];

                var cl = new System.Drawing.Color();
                if ((int)row["DaysOfUsage"] > 0 && ((DateTime)row["PrescriptionDate"]).AddDays((int)row["DaysOfUsage"]) > DateTime.Now)
                {
                    // RED TEXT
                    cl = System.Drawing.Color.Red;
                }
                else
                {
                    cl = System.Drawing.Color.Black;
                }
                ret.Color = cl;
            }

            return ret;
        }

        #endregion

    }
}