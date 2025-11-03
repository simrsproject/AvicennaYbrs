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
    public partial class UddItemDetail : BaseUserControl
    {
        protected string DisplayPrice
        {
            get { return AppSession.Parameter.IsShowPrescPriceOnDisplayDoctor ? "block" : "none"; }
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
            if (AppSession.Parameter.IsRasproEnable)
                customValidator.Validate();

            var reg = new Registration();
            reg.LoadByPrimaryKey(Request.QueryString["regno"]);

            var grr = new Guarantor();
            grr.LoadByPrimaryKey(reg.GuarantorID);

            cboDosageUnit.Items.Clear();

            var ipm = new ItemProductMedic();
            ipm.LoadByPrimaryKey(e.Value);

            txtItemUnit.Text = ipm.SRItemUnit;

            ViewState["CostPrice"] = ipm.CostPrice;

            if (!(chkIsCompound.Checked ?? false) & !string.IsNullOrEmpty(ipm.SRConsumeMethod))
                cboConsumeMethod.SelectedValue = ipm.SRConsumeMethod;

            // cboDosageUnit dientry jika tipe Compound untuk Formula Unit
            if (chkIsCompound.Checked ?? false)
            {
                cboDosageUnit.Items.Clear();
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
                cboDosageUnit.Items.Add(new RadComboBoxItem(ipm.SRItemUnit, ipm.SRItemUnit));
            }


            ViewState["InitialPrice"] = (double)Helper.Tariff.GetItemTariffNonMargin(grr.SRTariffType, DateTime.Now.Date, reg.ChargeClassID,
                e.Value, (chkIsCompound.Checked ?? false), ipm.SRItemUnit);

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
            consumes.LoadAll();

            //cboConsumeMethod.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (var consume in consumes)
            {
                //cboConsumeMethod.Items.Add(new RadComboBoxItem(consume.SRConsumeMethodName, consume.SRConsumeMethod));
                cboConsumeMethod.Items.Add(new RadComboBoxItem(consume.SygnaText + " (" + consume.SRConsumeMethodName + ")", consume.SRConsumeMethod));
            }

            //StandardReference.InitializeIncludeSpace(cboAcPcDc, AppEnum.StandardReference.MedicationConsume);


            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                var coll = (UddItemCollection)Session["collUddItem" + Request.UserHostName];
                if (!coll.HasData || coll.Count == 0)
                    ViewState["SequenceNo"] = "d01";
                else
                    ViewState["SequenceNo"] = "d" + string.Format("{0:00}", int.Parse(coll[coll.Count - 1].SequenceNo.Substring(1, 2)) + 1);

                txtStartDateTime.SelectedDate = DateTime.Now;
                ApplyIsCompound();
                return;
            }

            ViewState["IsNewRecord"] = false;
            ViewState["SequenceNo"] = DataBinder.Eval(DataItem, UddItemMetadata.ColumnNames.SequenceNo);

            chkIsCompound.Checked = (bool)DataBinder.Eval(DataItem, UddItemMetadata.ColumnNames.IsCompound);
            ApplyIsCompound();

            ComboBox.SelectedValue(cboParentNo, (String)DataBinder.Eval(DataItem, UddItemMetadata.ColumnNames.ParentNo));

            ComboBox.PopulateWithOneItem(cboItemID, (String)DataBinder.Eval(DataItem, UddItemMetadata.ColumnNames.ItemID));

            txtItemUnit.Display = !(chkIsCompound.Checked ?? false);
            txtItemUnit.Text = (String)DataBinder.Eval(DataItem, UddItemMetadata.ColumnNames.SRItemUnit);


            //if (cboEmbalace.Visible)
            if ((chkIsCompound.Checked ?? false))
                ComboBox.SelectedValue(cboEmbalace, (String)DataBinder.Eval(DataItem, UddItemMetadata.ColumnNames.EmbalaceID));

            txtQty.Text = (string)DataBinder.Eval(DataItem, UddItemMetadata.ColumnNames.ItemQtyInString);
            txtDosage.Text = (string)DataBinder.Eval(DataItem, UddItemMetadata.ColumnNames.DosageQty);
            ComboBox.SelectedValue(cboConsumeMethod, (String)DataBinder.Eval(DataItem, UddItemMetadata.ColumnNames.SRConsumeMethod));


            var ipm = new ItemProductMedic();
            ipm.LoadByPrimaryKey(cboItemID.SelectedValue);

            if (!string.IsNullOrEmpty(ipm.SRItemUnit))
                cboDosageUnit.Items.Add(new RadComboBoxItem(ipm.SRItemUnit, ipm.SRItemUnit));
            if (!string.IsNullOrEmpty(ipm.SRDosageUnit))
                cboDosageUnit.Items.Add(new RadComboBoxItem(ipm.SRDosageUnit, ipm.SRDosageUnit));

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

            ComboBox.SelectedValue(cboDosageUnit, (String)DataBinder.Eval(DataItem, UddItemMetadata.ColumnNames.SRDosageUnit));
            //ComboBox.SelectedValue(cboAcPcDc, (String)DataBinder.Eval(DataItem, UddItemMetadata.ColumnNames.AcPcDc));
            ComboBox.PopulateWithOneStandardReference(cboMedicationConsume,"MedicationConsume",(String)DataBinder.Eval(DataItem, UddItemMetadata.ColumnNames.AcPcDc));

            txtConsumeQty.Text = (String)DataBinder.Eval(DataItem, UddItemMetadata.ColumnNames.ConsumeQty);

            // Consume Unit
            var consumeUnit = (String)DataBinder.Eval(DataItem, UddItemMetadata.ColumnNames.SRConsumeUnit);
            var std = new AppStandardReferenceItemQuery();
            std.Select(std.ItemID, std.ItemName);
            std.Where
                (
                std.StandardReferenceID == AppEnum.StandardReference.DosageUnit,
                std.ItemID == consumeUnit
                );

            var dtb = std.LoadDataTable();

            if ((chkIsCompound.Checked ?? false))
            {
                var emb = new EmbalaceQuery();
                emb.Where(emb.EmbalaceID == consumeUnit);
                emb.Select(emb.EmbalaceID.As("ItemID"), emb.EmbalaceName.As("ItemName"));
                dtb.Merge(emb.LoadDataTable());
            }
            foreach (DataRow row in dtb.Rows)
            {
                cboConsumeUnit.Items.Add(new RadComboBoxItem(row["ItemName"].ToString(), row["ItemID"].ToString()));
            }
            ComboBox.SelectedValue(cboConsumeUnit, consumeUnit);

            var startDT = DataBinder.Eval(DataItem, UddItemMetadata.ColumnNames.StartDateTime);
            if (startDT != null)
                txtStartDateTime.SelectedDate = (DateTime)startDT;

            txtNotes.Text = (String)DataBinder.Eval(DataItem, UddItemMetadata.ColumnNames.Notes);
        }

        private void ApplyIsCompound()
        {
            var isCompound = (chkIsCompound.Checked ?? false);
            // Visible diganti display krn ctl nya dipakai javascript

            // Parent Compound
            cboParentNo.Style.Add("display", isCompound ? "block" : "none");

            txtItemUnit.Display = !isCompound;
            cboEmbalace.Style.Add("display", isCompound ? "block" : "none");

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
        }

        private string RegistrationNo
        {
            get
            {
                return Request.QueryString["regno"];
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

            // Check AB restriction
            if (AppSession.Parameter.IsRasproEnable)
            {
                var ipm = new ItemProductMedic();
                var abrForLine = AppParameter.GetParameterValue(AppParameter.ParameterItem.AntibioticRestrictionForLine);
                if (ipm.LoadByPrimaryKey(cboItemID.SelectedValue) && (ipm.IsAntibiotic ?? false) && (string.IsNullOrEmpty(abrForLine) || abrForLine == ipm.SRAntibioticLine))
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

            if (isFromSelectionItemValidation) return; // Return jika validate spesifik dipanggil dari cboItemID

            if ((new Fraction(txtConsumeQty.Text)).Result.ToDecimal() == 0)
            {
                args.IsValid = false;
                customValidator.ErrorMessage = "Consume Qty not valid";
                return;
            }
            if (string.IsNullOrEmpty(cboConsumeMethod.SelectedValue))
            {
                args.IsValid = false;
                customValidator.ErrorMessage = "Consume Method is not selected properly";
                return;
            }
            if (string.IsNullOrEmpty(cboConsumeUnit.SelectedValue))
            {
                args.IsValid = false;
                customValidator.ErrorMessage = "Consume Unit is not selected properly";
                return;
            }

            // Compound
            if (chkIsCompound.Checked ?? false)
            {
                if (string.IsNullOrEmpty(cboDosageUnit.SelectedValue))
                {
                    args.IsValid = false;
                    customValidator.ErrorMessage = "Please specify a Formula Dosage Unit";
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

            //if (ViewState["IsNewRecord"].Equals(true))
            //{
            // Cek double
            var coll = (UddItemCollection)Session["collUddItem" + Request.UserHostName];
            var isExist =
                coll.Any(
                    entity => entity.SequenceNo != SequenceNo &&
                    entity.ItemID.Equals(cboItemID.SelectedValue) &&
                    entity.ParentNo.Equals(cboParentNo.SelectedValue) &&
                    entity.IsCompound.Equals((chkIsCompound.Checked ?? false)) &&
                    entity.IsStop.Equals(false)); //ItemID yg sama bisa >1 jika berbeda

            if (isExist)
            {
                args.IsValid = false;
                customValidator.ErrorMessage = string.Format("Item: {0} has exist", cboItemID.Text);
                return;
            }
            //}
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

            var coll = (UddItemCollection)Session["collUddItem" + Request.UserHostName];
            foreach (var row in coll.Where(row => row.ParentNo == string.Empty && (row.IsCompound ?? false)))
            {
                cboParentNo.Items.Add(new RadComboBoxItem(row.ItemName, row.SequenceNo));
            }

            if (string.IsNullOrEmpty(cboParentNo.SelectedValue)) return;
            var entity = ((UddItemCollection)Session["collUddItem" + Request.UserHostName]).SingleOrDefault(t => t.SequenceNo == cboParentNo.SelectedValue);

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
            foreach (DataRow row in dtb.Rows)
            {
                cboConsumeUnit.Items.Add(new RadComboBoxItem(row["ItemName"].ToString(), row["ItemID"].ToString()));
            }
            ComboBox.SelectedValue(cboConsumeUnit, itemUnitID);
        }

        protected void cboParentNo_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Value)) return;
            var entity = ((UddItemCollection)Session["collUddItem" + Request.UserHostName]).SingleOrDefault(t => t.SequenceNo == e.Value);

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

            //ComboBox.SelectedValue(cboAcPcDc, entity.AcPcDc);
            //cboAcPcDc.Enabled = false;
            ComboBox.PopulateWithOneStandardReference(cboMedicationConsume,"MedicationConsume",entity.AcPcDc);
            cboMedicationConsume.Enabled = false;

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
            get { return string.Empty; }
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


    }
}