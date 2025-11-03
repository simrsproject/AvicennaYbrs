using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject.Reference;
using System.Collections.Generic;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class PrescriptionSalesItemDetail : BaseUserControl
    {
        private string PageId
        {
            get { return ((HiddenField)Helper.FindControlRecursive(Page, "hdnPageId")).Value; }
        }

        private string UniqueID()
        {
            return Request.QueryString["regno"];
        }

        private CheckBox ChkIs23Days
        {
            get
            { return (CheckBox)Helper.FindControlRecursive(Page, "chkIs23Days"); }
        }

        private bool IsGuarantorBpjs
        {
            get
            {
                var g = new Guarantor();
                if (g.LoadByPrimaryKey(CboGuarantorId.SelectedValue) && g.SRGuarantorType == AppSession.Parameter.GuarantorTypeBPJS)
                    return true;
                return false;
            }
        }

        #region cboItemID
        protected void cboItemID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            var loc = new Location();
            if (!loc.LoadByPrimaryKey(((RadComboBox)Helper.FindControlRecursive(this.Page, "cboLocationID")).SelectedValue))
                return;

            //var isFornas = false;
            //var guar = new Guarantor();
            //if (guar.LoadByPrimaryKey(CboGuarantorId.SelectedValue))
            //    isFornas = guar.IsItemRestrictionsFornas ?? false;

            string searchTextContain = string.Format("%{0}%", e.Text);

            var query = new ItemQuery("a");
            var bal = new ItemBalanceQuery("b");
            var bal2 = new ItemBalanceQuery("b2");
            var ipm = new VwItemProductMedicNonMedicQuery("c");
            var zai = new ItemProductMedicZatActiveQuery("d");
            var za = new ZatActiveQuery("e");

            query.es.Top = 50;
            query.es.Distinct = true;
            query.InnerJoin(bal).On(
                    query.ItemID == bal.ItemID &&
                    bal.LocationID == loc.LocationID
                );
            query.LeftJoin(bal2).On(
                query.ItemID == bal2.ItemID &&
                bal2.LocationID != loc.LocationID
            );
            query.InnerJoin(ipm).On(
                query.ItemID == ipm.ItemID
                );
            query.LeftJoin(zai).On(query.ItemID == zai.ItemID);
            query.LeftJoin(za).On(zai.ZatActiveID == za.ZatActiveID);
            query.Where(
                query.Or(
                    query.ItemName.Like(searchTextContain),
                    ipm.BrandName.Like(searchTextContain),
                    za.ZatActiveName.Like(searchTextContain)
                    ),
                query.IsActive == true, ipm.IsSalesAvailable == true
                );
            //if (isFornas)
            //    query.Where(ipm.IsFornas == true);
            //else
            //{
            //    var restrictions = new GuarantorItemRestrictionsQuery("rest");
            //    var itmrest = new ItemQuery("itmrest");
            //    restrictions.InnerJoin(itmrest).On(restrictions.ItemID == itmrest.ItemID);
            //    restrictions.Where(restrictions.GuarantorID == CboGuarantorId.SelectedValue,
            //                       itmrest.SRItemType.In(ItemType.Medical, ItemType.NonMedical, ItemType.Kitchen));
            //    DataTable dtRest = restrictions.LoadDataTable();
            //    if (dtRest.Rows.Count > 0)
            //        query.InnerJoin(restrictions).On(query.ItemID == restrictions.ItemID &&
            //                                         restrictions.GuarantorID == CboGuarantorId.SelectedValue);
            //}
            if (AppSession.Parameter.IsListItemForTxOnlyInStock)
                query.Where(bal.Balance > 0);
            query.Select
                (
                    string.Format("<CASE WHEN  CHARINDEX('{0}', a.ItemName) = 0 THEN 1000 ELSE CHARINDEX('{0}', a.ItemName) END  SearchIdx>", e.Text),
                    query.ItemID,
                    query.ItemName,
                    //(bal.Balance.Coalesce("0") - bal.Booking.Coalesce("0")).As("Balance"),
                    (bal.Balance.Coalesce("0")).As("Balance"),
                    ipm.SRItemUnit,
                    //string.Format("<CASE(CHARINDEX('par', a.ItemName)) when 0 then 1000 else CHARINDEX('{0}', a.ItemName) end SearchIdx>", e.Text),
                    "<CASE WHEN ISNULL(c.IsFornas, CAST(0 AS BIT)) = 0 THEN '' ELSE 'FORNAS' END AS Fornas>",
                    ipm.FornasRestrictionNotes,
                    (bal2.Balance.Coalesce("0")).Sum().As("BalanceAll")
                );
            query.GroupBy(query.ItemID, query.ItemName, (bal.Balance.Coalesce("0")), ipm.SRItemUnit, ipm.IsFornas, ipm.FornasRestrictionNotes);
            //query.OrderBy(string.Format("<CASE(CHARINDEX('par', a.ItemName)) when 0 then 1000 else CHARINDEX('{0}', a.ItemName) end>", e.Text),
            //    Temiang.Dal.DynamicQuery.esOrderByDirection.Ascending);
            query.OrderBy("<1,3>", Temiang.Dal.DynamicQuery.esOrderByDirection.Ascending); //Order by search text position then ItemName (Handono 22-08-27)
            cboItemID.DataSource = query.LoadDataTable();
            cboItemID.DataBind();
        }

        protected void cboItemID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void cboItemID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var item = new Item();
            if (!item.LoadByPrimaryKey(e.Value))
            {
                cboItemID.Text = string.Empty;
                return;
            }

            PopulateItemInfo(e.Value);

            DateTime transactionDate = (new DateTime()).NowAtSqlServer().Date;//DateTime.Now.Date;

            if (string.IsNullOrEmpty(Request.QueryString["mode"]))
            {
                var reg = new Registration();
                reg.LoadByPrimaryKey(Request.QueryString["regno"]);

                var grr = new Guarantor();
                grr.LoadByPrimaryKey((Helper.FindControlRecursive(Page, "cboGuarantorID") as RadComboBox).SelectedValue);

                //if (grr.TariffCalculationMethod == 1) 
                //    transactionDate = reg.RegistrationDate.Value.Date; ==> u/ obat gak perlu liat settingan ini

                //kalo parameter RecipeMarginValueCompound > 0, untuk margin tidak ditambah margin per item tapi lgsg ditambah margin parameter value tsb
                if (chkIsCompound.Checked && AppSession.Parameter.RecipeMarginValueCompound != 0)
                {
                    txtPrice.Value = (double)Helper.Tariff.GetItemTariffNonMargin(grr.SRTariffType, transactionDate,
                                                                                         reg.ChargeClassID, e.Value, chkIsCompound.Checked,
                                                                                         cboItemUnit.SelectedValue);
                    txtPrice.Value += Convert.ToDouble(AppSession.Parameter.RecipeMarginValueCompound / 100) * txtPrice.Value;
                }
                else
                {
                    txtPrice.Value = (double)Helper.Tariff.GetItemTariff(grr.SRTariffType, transactionDate, reg.ChargeClassID,
                        e.Value, chkIsCompound.Checked, cboItemUnit.SelectedValue, reg.GuarantorID, reg.SRRegistrationType);
                }

                //txtDiscountAmount.MaxValue = txtLineAmount.Value ?? 0;

                ViewState["InitialPrice"] = (double)Helper.Tariff.GetItemTariffNonMargin(grr.SRTariffType, transactionDate,
                                                                                         reg.ChargeClassID, e.Value, chkIsCompound.Checked,
                                                                                         cboItemUnit.SelectedValue);
            }
            else
            {
                //kalo parameter RecipeMarginValueCompound > 0, untuk margin tidak ditambah margin per item tapi lgsg ditambah margin parameter value tsb
                if (chkIsCompound.Checked && AppSession.Parameter.RecipeMarginValueCompound != 0)
                {
                    txtPrice.Value = (double)Helper.Tariff.GetItemTariffNonMargin(AppSession.Parameter.DefaultTariffType,
                                                                                         transactionDate, AppSession.Parameter.DefaultTariffClass,
                                                                                         e.Value, chkIsCompound.Checked,
                                                                                         cboItemUnit.SelectedValue);
                    txtPrice.Value += Convert.ToDouble(AppSession.Parameter.RecipeMarginValueCompound / 100) * txtPrice.Value;
                }
                else
                {
                    txtPrice.Value = (double)Helper.Tariff.GetItemTariff(AppSession.Parameter.DefaultTariffType, transactionDate,
                                                                     AppSession.Parameter.DefaultTariffClass,
                                                                     e.Value, chkIsCompound.Checked, cboItemUnit.SelectedValue,
                                                                     CboGuarantorId.SelectedValue, Request.QueryString["mode"]);
                }
                //txtDiscountAmount.MaxValue = txtLineAmount.Value ?? 0;

                ViewState["InitialPrice"] = (double)Helper.Tariff.GetItemTariffNonMargin(AppSession.Parameter.DefaultTariffType,
                                                                                         transactionDate, AppSession.Parameter.DefaultTariffClass,
                                                                                         e.Value, chkIsCompound.Checked,
                                                                                         cboItemUnit.SelectedValue);
            }

            var conmethod = string.Empty;
            if (item.SRItemType == ItemType.Medical)
            {
                var ipm = new ItemProductMedic();
                ipm.LoadByPrimaryKey(e.Value);
                conmethod = ipm.str.SRConsumeMethod;
                lblFornasRestrictionNotes.Text = ipm.FornasRestrictionNotes;

                ViewState["CostPrice"] = ipm.CostPrice;
            }
            else if (item.SRItemType == ItemType.NonMedical)
            {
                var ipn = new ItemProductNonMedic();
                ipn.LoadByPrimaryKey(e.Value);
                lblFornasRestrictionNotes.Text = string.Empty;

                ViewState["CostPrice"] = ipn.CostPrice;
            }
            else
            {
                var ipk = new ItemKitchen();
                ipk.LoadByPrimaryKey(e.Value);
                lblFornasRestrictionNotes.Text = string.Empty;

                ViewState["CostPrice"] = ipk.CostPrice;
            }

            PopulateResultQtyAndLineAmount();

            lblMsg.Text = GetCoveredItem(e.Value);


            if (chkIsCompound.Checked && !string.IsNullOrEmpty(cboParentNo.SelectedValue))
                txtDosage.Focus();
            else
                txtPrescriptionQty.Focus();

            ShowPrevBuy(PatientIds, e.Value, e.Text, cboItemInterventionID.SelectedValue, cboItemInterventionID.Text);

            if (!chkIsCompound.Checked & !string.IsNullOrEmpty(conmethod))
            {
                PopulateCboConsumeMethod(conmethod, false);
                ComboBox.SelectedValue(cboConsumeMethod, conmethod);
            }

            if (ChkIs23Days.Checked && AppSession.Parameter.IsUsingDefaultConsumeMethodFor23DaysPrescription)
            {
                PopulateCboConsumeMethod(AppSession.Parameter.DefaultConsumeMethod, false);
                ComboBox.SelectedValue(cboConsumeMethod, AppSession.Parameter.DefaultConsumeMethod);
            }
        }

        private List<string> _patiendIds = null;
        private List<string> PatientIds
        {
            get
            {
                if (_patiendIds == null)
                {
                    var cboPatientID = (RadComboBox)Helper.FindControlRecursive(Page, "cboPatientID");
                    _patiendIds = Patient.PatientRelateds(cboPatientID.SelectedValue);
                }
                return _patiendIds;
            }
        }

        private void ShowPrevBuy(List<string> patientIds, string itemID, string itemName, string itemInterventionID, string itemInterventionName)
        {
            if (AppSession.Parameter.IsPrescriptionLoadLastBought)
            {
                pnlInfoLastBuy.Visible = false;
                if (Request.QueryString["mode"] != "otc")
                {
                    var prev = CekPrevBuy(patientIds, TxtPrescriptionNo.Text, itemID, itemName, itemInterventionID, itemInterventionName);
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
                        lblTotalDays.ForeColor = prev.Color;

                        lblPrevItemName.Text = prev.ItemName;
                        lblPrevItemQty.Text = prev.Qty;
                        lblPrevItemSRUnit.Text = prev.SRItemUnit;
                        lblPrevDate.Text = prev.Date;
                        lblTotalDays.Text = prev.TotalDays;

                        lblPrevConsumMethod.Text = prev.ConsumeMethod;
                    }
                }
            }
        }

        public class PrevPrescriptionInfo
        {
            public string ItemName;
            public string Qty;
            public string SRItemUnit;
            public string Date;
            public string TotalDays;
            public int DaysOfUsage;
            public System.Drawing.Color Color;
            public string ConsumeMethod;
        }

        // Prev CekPrevBuy
        //public static PrevPrescriptionInfo CekPrevBuy(string RegistrationNo, string PrescriptionNo,
        //    string ItemID, string ItemInterventionID)
        //{
        //    // PREV TRANS
        //    var tp = new TransPrescriptionQuery("tp");
        //    var tpi = new TransPrescriptionItemQuery("tpi");
        //    var reg = new RegistrationQuery("r1");
        //    var pat = new PatientQuery("p1");
        //    var prevReg = new RegistrationQuery("r2");
        //    var i = new ItemQuery("i");

        //    reg.InnerJoin(pat).On(reg.PatientID.Equal(pat.PatientID) && reg.RegistrationNo.Equal(RegistrationNo))
        //        .InnerJoin(prevReg).On(pat.PatientID.Equal(prevReg.PatientID) && prevReg.RegistrationDate <= reg.RegistrationDate)
        //        .InnerJoin(tp).On(tp.RegistrationNo.Equal(prevReg.RegistrationNo))
        //        .InnerJoin(tpi).On(tp.PrescriptionNo.Equal(tpi.PrescriptionNo))
        //        .InnerJoin(i).On(tpi.ItemID.Equal(i.ItemID))
        //        .Where(tpi.ItemID.In(new string[] { ItemID, ItemInterventionID }),
        //            tp.IsApproval.Equal(true),
        //            prevReg.IsVoid.Equal(false), tp.PrescriptionNo != PrescriptionNo)
        //        .OrderBy(tp.PrescriptionDate.Descending)
        //        .Select(i.ItemID, i.ItemName, tpi.TakenQty, tpi.SRItemUnit,
        //            tp.PrescriptionDate,
        //            "<ISNULL(tpi.DaysOfUsage,0) DaysOfUsage>", tpi.SRConsumeMethod, tpi.ConsumeQty, tpi.SRConsumeUnit);
        //    reg.es.Top = 1;
        //    var dttbl = reg.LoadDataTable();

        //    var ret = new PrevPrescriptionInfo();

        //    foreach (System.Data.DataRow row in dttbl.Rows)
        //    {
        //        ret.ItemName = row["ItemName"].ToString();
        //        ret.Qty = row["TakenQty"].ToString();
        //        ret.SRItemUnit = row["SRItemUnit"].ToString();
        //        ret.Date = ((DateTime)row["PrescriptionDate"]).ToString("dd/MM/yyyy");
        //        ret.TotalDays = (DateTime.Now.Date - ((DateTime)row["PrescriptionDate"])).TotalDays.ToString();
        //        ret.DaysOfUsage = (int)row["DaysOfUsage"];

        //        var cm = new ConsumeMethod();
        //        cm.LoadByPrimaryKey(row["SRConsumeMethod"].ToString());

        //        var cmUnit = new AppStandardReferenceItem();
        //        cmUnit.LoadByPrimaryKey("DosageUnit", row["SRConsumeUnit"].ToString());
        //        ret.ConsumeMethod = String.Format("{0} @{1} {2}",cm.SRConsumeMethodName, row["ConsumeQty"], cmUnit.ItemName);

        //        var cl = new System.Drawing.Color();
        //        if ((int)row["DaysOfUsage"] > 0 && ((DateTime)row["PrescriptionDate"]).AddDays((int)row["DaysOfUsage"]) > DateTime.Now)
        //        {
        //            // RED TEXT
        //            cl = System.Drawing.Color.Red;
        //        }
        //        else
        //        {
        //            cl = System.Drawing.Color.Black;
        //        }
        //        ret.Color = cl;
        //    }

        //    return ret;
        //}
        // End prev CekPrevBuy

        // New CekPrevBuy Tune up (Handono 231121)
        public static PrevPrescriptionInfo CekPrevBuy(List<string> patiendIds, string prescriptionNo, string itemID, string itemName, string itemInterventionID, string itemInterventionName)
        {
            // PREV TRANS atau terakhir beli
            var tp = new TransPrescriptionQuery("tp");
            tp.es.WithNoLock = true;

            var reg = new RegistrationQuery("r1");
            reg.es.WithNoLock = true;
            tp.InnerJoin(reg).On(tp.RegistrationNo == reg.RegistrationNo);

            var tpi = new TransPrescriptionItemQuery("tpi");
            tpi.es.WithNoLock = true;
            tp.InnerJoin(tpi).On(tp.PrescriptionNo == tpi.PrescriptionNo);

            //db:20231226 - ditest selisih waktu 5 detik lebih cepat kalo urutannya diganti jd yg bawah
            //tp.Where(reg.PatientID.In(patiendIds), tp.IsApproval.Equal(true), tp.PrescriptionNo != prescriptionNo);
            tp.Where(reg.PatientID.In(patiendIds), tp.PrescriptionNo != prescriptionNo, tp.IsApproval.Equal(true));

            if (!string.IsNullOrWhiteSpace(itemInterventionID))
                tp.Where(tpi.ItemID.In(new string[] { itemID, itemInterventionID }));
            else
                tp.Where(tpi.ItemID == itemID);

            tp.OrderBy(tp.PrescriptionDate.Descending);
            tp.Select(tpi.ItemID, tpi.TakenQty, tpi.SRItemUnit,
                tp.PrescriptionDate,
                "<ISNULL(tpi.DaysOfUsage,0) DaysOfUsage>", tpi.SRConsumeMethod, tpi.ConsumeQty, tpi.SRConsumeUnit);
            tp.es.Top = 1;
            var dttbl = tp.LoadDataTable();

            var ret = new PrevPrescriptionInfo();

            foreach (System.Data.DataRow row in dttbl.Rows)
            {
                ret.ItemName = itemID.Equals(row["ItemID"]) ? itemName : itemInterventionName;
                ret.Qty = Helper.RemoveZeroDigits(Convert.ToDecimal(row["TakenQty"]));
                ret.SRItemUnit = row["SRItemUnit"].ToString();
                ret.Date = ((DateTime)row["PrescriptionDate"]).ToString("dd/MM/yyyy");
                ret.TotalDays = (DateTime.Now.Date - ((DateTime)row["PrescriptionDate"])).TotalDays.ToString();
                ret.DaysOfUsage = (int)row["DaysOfUsage"];

                var cm = new ConsumeMethod();
                cm.LoadByPrimaryKey(row["SRConsumeMethod"].ToString());

                var cmUnit = new AppStandardReferenceItem();
                cmUnit.LoadByPrimaryKey("DosageUnit", row["SRConsumeUnit"].ToString());
                ret.ConsumeMethod = String.Format("{0} @{1} {2}", cm.SRConsumeMethodName, row["ConsumeQty"], cmUnit.ItemName);

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

        #region cboItemInterventionID

        protected void cboItemInterventionID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            var loc = new Location();
            if (!loc.LoadByPrimaryKey(((RadComboBox)Helper.FindControlRecursive(this.Page, "cboLocationID")).SelectedValue))
                return;

            //var isFornas = false;
            //var guar = new Guarantor();
            //if (guar.LoadByPrimaryKey(CboGuarantorId.SelectedValue))
            //    isFornas = guar.IsItemRestrictionsFornas ?? false;

            string searchTextContain = string.Format("%{0}%", e.Text);

            var query = new ItemQuery("a");
            var bal = new ItemBalanceQuery("b");
            var bal2 = new ItemBalanceQuery("b2");
            var ipm = new VwItemProductMedicNonMedicQuery("c");
            var zai = new ItemProductMedicZatActiveQuery("d");
            var za = new ZatActiveQuery("e");

            query.es.Top = 50;
            query.es.Distinct = true;
            query.InnerJoin(bal).On(
                    query.ItemID == bal.ItemID &&
                    bal.LocationID == loc.LocationID
                );
            query.LeftJoin(bal2).On(
                    query.ItemID == bal2.ItemID &&
                    bal2.LocationID != loc.LocationID
                );
            query.InnerJoin(ipm).On(
                query.ItemID == ipm.ItemID
                );
            query.LeftJoin(zai).On(query.ItemID == zai.ItemID);
            query.LeftJoin(za).On(zai.ZatActiveID == za.ZatActiveID);
            query.Where(
                query.Or(
                    query.ItemName.Like(searchTextContain),
                    ipm.BrandName.Like(searchTextContain),
                    za.ZatActiveName.Like(searchTextContain)
                    ),
                query.IsActive == true, ipm.IsSalesAvailable == true
                );
            //if (isFornas)
            //    query.Where(ipm.IsFornas == true);
            //else
            //{
            //    var restrictions = new GuarantorItemRestrictionsQuery("rest");
            //    var itmrest = new ItemQuery("itmrest");
            //    restrictions.InnerJoin(itmrest).On(restrictions.ItemID == itmrest.ItemID);
            //    restrictions.Where(restrictions.GuarantorID == CboGuarantorId.SelectedValue,
            //                       itmrest.SRItemType.In(ItemType.Medical, ItemType.NonMedical, ItemType.Kitchen));
            //    DataTable dtRest = restrictions.LoadDataTable();
            //    if (dtRest.Rows.Count > 0)
            //        query.InnerJoin(restrictions).On(query.ItemID == restrictions.ItemID,
            //                                         restrictions.GuarantorID == CboGuarantorId.SelectedValue);
            //}
            if (AppSession.Parameter.IsListItemForTxOnlyInStock)
                query.Where(bal.Balance > 0);
            query.Select
                (
                    query.ItemID,
                    query.ItemName,
                    //(bal.Balance.Coalesce("0") - bal.Booking.Coalesce("0")).As("Balance"),
                    (bal.Balance.Coalesce("0")).As("Balance"),
                    ipm.SRItemUnit,
                    string.Format("<CASE(CHARINDEX('par', a.ItemName)) when 0 then 1000 else CHARINDEX('{0}', a.ItemName) end SearchIdx>", e.Text),
                    "<CASE WHEN ISNULL(c.IsFornas, CAST(0 AS BIT)) = 0 THEN '' ELSE 'FORNAS' END AS Fornas>",
                    (bal2.Balance.Coalesce("0")).Sum().As("BalanceAll"),
                    ipm.FornasRestrictionNotes
                );
            query.GroupBy(query.ItemID, query.ItemName, (bal.Balance.Coalesce("0")), ipm.SRItemUnit, ipm.IsFornas, ipm.FornasRestrictionNotes);
            query.OrderBy(string.Format("<CASE(CHARINDEX('par', a.ItemName)) when 0 then 1000 else CHARINDEX('{0}', a.ItemName) end>", e.Text),
                Temiang.Dal.DynamicQuery.esOrderByDirection.Ascending);

            cboItemInterventionID.DataSource = query.LoadDataTable();
            cboItemInterventionID.DataBind();
        }

        protected void cboItemInterventionID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void cboItemInterventionID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var item = new Item();
            if (!item.LoadByPrimaryKey(e.Value))
            {
                cboItemInterventionID.SelectedValue = string.Empty;
                cboItemInterventionID.Text = string.Empty;
                cboItemInterventionID.Items.Clear();

                // hitung lagi harga item aslinya
                cboItemID_SelectedIndexChanged(cboItemID, new RadComboBoxSelectedIndexChangedEventArgs(
                    cboItemID.Text, cboItemID.Text, cboItemID.SelectedValue, cboItemID.SelectedValue));

                return;
            }

            PopulateItemInfo(e.Value);

            var reg = new Registration();
            reg.LoadByPrimaryKey(Request.QueryString["regno"]);

            var grr = new Guarantor();
            grr.LoadByPrimaryKey((Helper.FindControlRecursive(Page, "cboGuarantorID") as RadComboBox).SelectedValue);

            var transactionDate = DateTime.Now.Date;
            //if (grr.TariffCalculationMethod == 1) transactionDate = reg.RegistrationDate.Value.Date;

            if (string.IsNullOrEmpty(Request.QueryString["mode"]))
            {
                txtPrice.Value = (double)Helper.Tariff.GetItemTariff(grr.SRTariffType, transactionDate, reg.ChargeClassID,
                    e.Value, chkIsCompound.Checked, cboItemUnit.SelectedValue, reg.GuarantorID, reg.SRRegistrationType);

                //txtPrice.Value += Convert.ToDouble((chkIsCompound.Checked ? AppSession.Parameter.RecipeMarginValueCompound :
                //                                                            AppSession.Parameter.RecipeMarginValueNonCompound) / 100) * txtPrice.Value;

                ViewState["InitialPrice"] = (double)Helper.Tariff.GetItemTariffNonMargin(grr.SRTariffType, transactionDate,
                                                                                         reg.ChargeClassID, e.Value, chkIsCompound.Checked,
                                                                                         cboItemUnit.SelectedValue);
            }
            else
            {
                txtPrice.Value = (double)Helper.Tariff.GetItemTariff(AppSession.Parameter.DefaultTariffType, transactionDate,
                                                                     AppSession.Parameter.DefaultTariffClass,
                                                                     e.Value, chkIsCompound.Checked, cboItemUnit.SelectedValue,
                                                                     CboGuarantorId.SelectedValue, Request.QueryString["mode"]);

                //txtPrice.Value += Convert.ToDouble((chkIsCompound.Checked ? AppSession.Parameter.RecipeMarginValueCompound :
                //                                                            AppSession.Parameter.RecipeMarginValueNonCompound) / 100) * txtPrice.Value;

                ViewState["InitialPrice"] = (double)Helper.Tariff.GetItemTariffNonMargin(AppSession.Parameter.DefaultTariffType,
                                                                                         transactionDate, AppSession.Parameter.DefaultTariffClass,
                                                                                         e.Value, chkIsCompound.Checked,
                                                                                         cboItemUnit.SelectedValue);
            }

            //txtDiscountAmount.MaxValue = txtLineAmount.Value ?? 0;

            switch (item.SRItemType)
            {
                case ItemType.Medical:
                    var ipm = new ItemProductMedic();
                    ipm.LoadByPrimaryKey(e.Value);
                    lblFornasRestrictionNotes.Text = ipm.FornasRestrictionNotes;

                    ViewState["CostPrice"] = ipm.CostPrice;
                    break;
                case ItemType.NonMedical:
                    var ipn = new ItemProductNonMedic();
                    ipn.LoadByPrimaryKey(e.Value);
                    lblFornasRestrictionNotes.Text = string.Empty;

                    ViewState["CostPrice"] = ipn.CostPrice;
                    break;
                case ItemType.Kitchen:
                    var ipk = new ItemKitchen();
                    ipk.LoadByPrimaryKey(e.Value);
                    lblFornasRestrictionNotes.Text = string.Empty;

                    ViewState["CostPrice"] = ipk.CostPrice;
                    break;
            }

            PopulateResultQtyAndLineAmount();

            if (chkIsCompound.Checked && !string.IsNullOrEmpty(cboParentNo.SelectedValue))
                txtDosage.Focus();
            else
                txtPrescriptionQty.Focus();

            ShowPrevBuy(PatientIds, cboItemID.SelectedValue, cboItemID.Text, e.Value, e.Text);
        }

        #endregion

        #region cboConsumeMethod
        protected void cboConsumeMethod_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulateCboConsumeMethod(e.Text, true);
        }

        private void PopulateCboConsumeMethod(string searchtext, bool isNew)
        {
            cboConsumeMethod.Items.Clear();
            var query = new ConsumeMethodQuery("a");
            query.Select(query.SRConsumeMethod, query.SRConsumeMethodName, query.SygnaText);
            if (isNew)
                query.Where(query.IsActive == true);
            query.Where
                (
                query.Or(
                    query.SRConsumeMethod == searchtext,
                    query.SRConsumeMethodName.Like(string.Format("{0}%", searchtext)),
                    query.SygnaText.Like(string.Format("{0}%", searchtext))
                    )
                );

            query.OrderBy(query.LineNumber.Ascending, "<LEFT(a.SRConsumeMethodName, 5)>");

            var dtb = query.LoadDataTable();

            cboConsumeMethod.DataSource = dtb;
            cboConsumeMethod.DataBind();
        }

        protected void cboConsumeMethod_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SygnaText"].ToString() + " (" + ((DataRowView)e.Item.DataItem)["SRConsumeMethodName"].ToString() + ")";
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SRConsumeMethod"].ToString();
        }
        #endregion

        #region cboConsumeUnit
        protected void cboConsumeUnit_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            //if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSCH")
            {
                var itemID = string.IsNullOrEmpty(cboItemInterventionID.SelectedValue) ? cboItemID.SelectedValue : cboItemInterventionID.SelectedValue;
                var searchText = string.Format("{0}%", e.Text);
                var MaxQueryRecord = 20;
                var srItemUnit = string.Empty;
                var srDosageUnit = string.Empty;

                var vwItem = new VwItemProductMedicNonMedicQuery("vwi");
                var iu = new AppStandardReferenceItemQuery("iu");
                vwItem.InnerJoin(iu).On(iu.StandardReferenceID == "ItemUnit" && iu.ItemID == vwItem.SRItemUnit);
                vwItem.Select(vwItem.SRItemUnit.As("ItemID"), iu.ItemName.As("ItemName"), vwItem.SRDosageUnit);
                vwItem.Where(vwItem.ItemID == itemID, iu.ItemName.Like(searchText));

                var dtb = vwItem.LoadDataTable();
                if (dtb.Rows.Count > 0)
                {
                    srItemUnit = dtb.Rows[0]["ItemID"].ToString();
                    srDosageUnit = !string.IsNullOrEmpty(dtb.Rows[0]["SRDosageUnit"].ToString()) ? dtb.Rows[0]["SRDosageUnit"].ToString() : string.Empty;
                }

                if (srDosageUnit != srItemUnit && !string.IsNullOrEmpty(srDosageUnit))
                {
                    var vwItem2 = new VwItemProductMedicNonMedicQuery("vwi2");
                    var iu2 = new AppStandardReferenceItemQuery("iu2");
                    vwItem2.InnerJoin(iu2).On(iu2.StandardReferenceID == "DosageUnit" && iu2.ItemID == vwItem2.SRDosageUnit);
                    vwItem2.Select(vwItem2.SRDosageUnit.As("ItemID"), iu2.ItemName.As("ItemName"), @"<'' AS SRDosageUnit>");
                    vwItem2.Where(vwItem2.ItemID == itemID, iu2.ItemName.Like(searchText));
                    dtb.Merge(vwItem2.LoadDataTable());
                }

                var dosages = new ItemProductDosageDetailQuery("dosages");
                var iu3 = new AppStandardReferenceItemQuery("iu3");
                dosages.InnerJoin(iu3).On(iu3.StandardReferenceID == "DosageUnit" && iu3.ItemID == dosages.SRDosageUnit);
                dosages.Select(dosages.SRDosageUnit.As("ItemID"), iu3.ItemName.As("ItemName"), @"<'' AS SRDosageUnit>");
                dosages.Where(dosages.ItemID == itemID, dosages.SRDosageUnit.NotIn(srItemUnit, srDosageUnit), iu3.ItemName.Like(searchText));
                dosages.es.Top = MaxQueryRecord - dtb.Rows.Count;
                dtb.Merge(dosages.LoadDataTable());

                var std = new AppStandardReferenceItemQuery("std");
                std.Select(std.ItemID.As("ItemID"), std.ItemName.As("ItemName"), @"<'' AS SRDosageUnit>");
                std.Where(std.StandardReferenceID == AppEnum.StandardReference.GlobalConsumeUnit, std.IsActive == true, std.ItemName.Like(searchText));
                std.es.Top = MaxQueryRecord - dtb.Rows.Count;
                dtb.Merge(std.LoadDataTable());

                var emb = new EmbalaceQuery("emb");
                emb.Select(emb.EmbalaceID.As("ItemID"), emb.EmbalaceName.As("ItemName"), @"<'' AS SRDosageUnit>");
                emb.Where(emb.EmbalaceName.Like(searchText));

                emb.es.Top = MaxQueryRecord - dtb.Rows.Count;
                dtb.Merge(emb.LoadDataTable());

                dtb.AsEnumerable().Distinct(System.Data.DataRowComparer.Default).ToList();


                //var query = new AppStandardReferenceItemQuery();
                //query.Select(query.ItemID, query.ItemName);
                //query.Where
                //    (
                //    query.StandardReferenceID == AppEnum.StandardReference.DosageUnit,
                //    query.IsActive == true,
                //    query.Or(
                //        query.ItemID.Like(string.Format("{0}%", e.Text)),
                //        query.ItemName.Like(string.Format("{0}%", e.Text))
                //        )
                //    );

                //var dtb = query.LoadDataTable();

                //if (chkIsCompound.Checked)
                //{
                //    var emb = new EmbalaceQuery();
                //    emb.Select(emb.EmbalaceID.As("ItemID"), emb.EmbalaceName.As("ItemName"));
                //    dtb.Merge(emb.LoadDataTable());
                //}

                cboConsumeUnit.DataSource = dtb;
                cboConsumeUnit.DataBind();
            }
        }

        protected void cboConsumeUnit_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }
        #endregion

        public object DataItem { get; set; }

        private RadComboBox CboGuarantorId
        {
            get
            { return (RadComboBox)Helper.FindControlRecursive(Page, "cboGuarantorID"); }
        }

        private RadTextBox TxtRegistrationNo
        {
            get
            { return (RadTextBox)Helper.FindControlRecursive(Page, "txtRegistrationNo"); }
        }

        private RadTextBox TxtPrescriptionNo
        {
            get
            { return (RadTextBox)Helper.FindControlRecursive(Page, "txtPrescriptionNo"); }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            CboGuarantorId.Enabled = false;

            chkIsPending.Visible =
                AppSession.Parameter.GuarantorAskesID.Contains(CboGuarantorId.SelectedValue) &&
                AppSession.Parameter.IsPrescriptionPendingDelivery;
            chkIsCasemixApproved.Visible = false;
            chkIsVoid.Visible = false;

            //StandardReference.InitializeIncludeSpace(cboSRMedicationRoute, AppEnum.StandardReference.Route);
            StandardReference.InitializeIncludeSpace(cboDiscountReason, AppEnum.StandardReference.DiscountReason);
            //StandardReference.InitializeIncludeSpace(cboAcPcDc, AppEnum.StandardReference.MedicationConsume);
            StandardReference.InitializeIncludeSpaceSortByLineNumber(cboAcPcDc, AppEnum.StandardReference.MedicationConsume);
            StandardReference.InitializeIncludeSpace(cboSRInterventionReason, AppEnum.StandardReference.InterventionReason);

            var embs = new EmbalaceCollection();
            embs.LoadAll();

            foreach (var emb in embs)
            {
                cboEmbalace.Items.Add(new RadComboBoxItem(emb.EmbalaceLabel, emb.EmbalaceID));
            }

            //var consumes = new ConsumeMethodCollection();
            //consumes.Query.OrderBy(consumes.Query.LineNumber.Ascending, "<LEFT(SRConsumeMethodName, 5)>");
            //consumes.LoadAll();
            //foreach (var consume in consumes)
            //{
            //    cboConsumeMethod.Items.Add(new RadComboBoxItem(consume.SygnaText + " (" + consume.SRConsumeMethodName + ")", consume.SRConsumeMethod));
            //}

            cboItemID.Enabled = DataItem is GridInsertionObject;
            trIntervention.Visible = Request.QueryString["type"] != "sales";
            trIntervention2.Visible = Request.QueryString["type"] != "sales";
            tdInterventionReasonHd.Visible = trIntervention.Visible;
            tdInterventionReasonDt.Visible = trIntervention.Visible;

            //StandardReference.InitializeIncludeSpace(cboConsumeUnit, AppEnum.StandardReference.DosageUnit);
            txtDiscountAmount.ReadOnly = AppSession.Parameter.IsReadOnlyDiscountOnPrescription;
            txtDiscountPercent.ReadOnly = AppSession.Parameter.IsReadOnlyDiscountOnPrescription;
            cboDiscountReason.Enabled = !AppSession.Parameter.IsReadOnlyDiscountOnPrescription;

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                var coll = (TransPrescriptionItemCollection)Session["collTransPrescriptionItem" + UniqueID() + PageId];
                if (coll.Count == 0)
                {
                    ViewState["SequenceNo"] = "e01";
                    txtDiscountPercent.Value = 0;
                }
                else
                {
                    ViewState["SequenceNo"] = "e" + string.Format("{0:00}", int.Parse(coll[coll.Count - 1].SequenceNo.Substring(1, 2)) + 1);

                    var c = coll[coll.Count - 1];
                    if (c != null)
                    {
                        if (c.DiscountAmount > 0 && !string.IsNullOrEmpty(c.SRDiscountReason))
                        {
                            if (c.LineAmount == 0) txtDiscountPercent.Value = 100;
                            ComboBox.SelectedValue(cboDiscountReason, c.SRDiscountReason);
                        }
                        else
                            txtDiscountPercent.Value = 0;
                    }
                    else
                        txtDiscountPercent.Value = 0;
                }

                //txtFrequencyOfDosing.Value = 1;
                //txtNumberOfDosage.Value = 1;
                //txtDurationOfDosing.Value = 1;
                //txtPrescriptionQty.Text = "1";
                txtTakenQty.Value = 1;
                txtDosage.Text = "1";
                txtResultQty.Value = 1;
                //cboDiscountReason.Enabled = false;
                txtQtyConsume.Text = "1";
                txtPrice.Value = 0;
                txtRecipeAmount.Value = 0;
                txtLineAmount.Value = 0;
                txtDiscountAmount.Value = 0;
                txtQty23Days.Value = 0;

                cboConsumeMethod.SelectedValue = string.Empty;
                cboConsumeMethod.Text = string.Empty;

                tdlblQty23Days.Visible = !ChkIs23Days.Checked && IsGuarantorBpjs && Request.QueryString["rt"] == "opr" && (Request.QueryString["type"] == "realization" || Request.QueryString["type"] == "sales");
                tdTxtQty23Days.Visible = tdlblQty23Days.Visible;
                lblFornasRestrictionNotes.Text = string.Empty;

                cboItemID.Focus();
                return;
            }

            txtDiscountPercent.Value = 0;

            ViewState["IsNewRecord"] = false;
            ViewState["SequenceNo"] = DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.SequenceNo);

            chkIsCompound.Checked = (bool)DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.IsCompound);
            trHeader.Visible = chkIsCompound.Checked;
            trHeader2.Visible = chkIsCompound.Checked;
            txtItemUnit.Visible = !chkIsCompound.Checked;
            cboEmbalace.Visible = chkIsCompound.Checked;

            PopulateParentNo();
            ComboBox.SelectedValue(cboParentNo, (String)DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.ParentNo));
            SetEnabledControl(cboParentNo.SelectedValue == string.Empty);

            //Populate Item
            var loc = new Location();
            if (!loc.LoadByPrimaryKey(((RadComboBox)Helper.FindControlRecursive(Page, "cboLocationID")).SelectedValue))
                return;

            //// Start remark: Untuk efisiensi query dirubah hanya isi id dan nama item saja (Handono 2022 06)
            //var query = new ItemQuery("a");
            //var bal = new ItemBalanceQuery("b");
            ////var medic = new ItemProductMedicQuery("c");
            //var medic = new VwItemProductMedicNonMedicQuery("c");

            //query.Select
            //    (
            //        query.ItemID,
            //        query.ItemName,
            //        bal.Balance.Coalesce("0"),
            //        medic.SRItemUnit, "<0 AS Fornas>", "<0 AS BalanceAll>"
            //    );
            //query.LeftJoin(bal).On
            //    (
            //        query.ItemID == bal.ItemID &&
            //        bal.LocationID == loc.LocationID
            //    );
            //query.InnerJoin(medic).On(query.ItemID == medic.ItemID);
            //query.Where(query.ItemID == (String)DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.ItemID));

            //cboItemID.DataSource = query.LoadDataTable();
            //cboItemID.DataBind();

            //ComboBox.SelectedValue(cboItemID, (String)DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.ItemID));

            //var queryI = new ItemQuery("a");
            //var balI = new ItemBalanceQuery("b");
            ////var medicI = new ItemProductMedicQuery("c");
            //var medicI = new VwItemProductMedicNonMedicQuery("c");

            //queryI.Select
            //    (
            //        queryI.ItemID,
            //        queryI.ItemName,
            //        balI.Balance.Coalesce("0"),
            //        medicI.SRItemUnit, "<'' AS Fornas>", "<0 AS BalanceAll>"
            //    );
            //queryI.LeftJoin(balI).On
            //    (
            //        queryI.ItemID == balI.ItemID &
            //        balI.LocationID == loc.LocationID
            //    );
            //queryI.InnerJoin(medicI).On(queryI.ItemID == medicI.ItemID);
            //queryI.Where(queryI.ItemID == (String)DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.ItemInterventionID));

            //cboItemInterventionID.DataSource = queryI.LoadDataTable();
            //cboItemInterventionID.DataBind();

            //ComboBox.SelectedValue(cboItemInterventionID, (String)DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.ItemInterventionID));
            //// End remark: Untuk efisiensi query dirubah hanya isi id dan nama item saja (Handono 2022 06)

            //// Add: Untuk efisiensi query dirubah hanya isi id dan nama item saja (Handono 2022 06)
            ComboBox.PopulateWithOneItem(cboItemID, (String)DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.ItemID));
            ComboBox.PopulateWithOneItem(cboItemInterventionID, (String)DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.ItemInterventionID));
            //// End Add: Untuk efisiensi query dirubah hanya isi id dan nama item saja (Handono 2022 06)


            PopulateItemInfo(string.IsNullOrEmpty(cboItemInterventionID.SelectedValue) ? cboItemID.SelectedValue : cboItemInterventionID.SelectedValue);

            ComboBox.SelectedValue(cboItemUnit, (String)DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.SRDosageUnit));

            //txtItemQtyInString.Text = (String)DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.ItemQtyInString);
            //txtFrequencyOfDosing.Value = Convert.ToDouble(DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.FrequencyOfDosing));
            //cboDosingPeriod.SelectedValue = (String)DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.DosingPeriod);
            //txtNumberOfDosage.Value = Convert.ToDouble(DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.NumberOfDosage));
            //txtDurationOfDosing.Value = Convert.ToDouble(DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.DurationOfDosing));
            //cboAcpcdc.SelectedValue = (String)DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.Acpcdc);
            //cboSRMedicationRoute.SelectedValue = (String)DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.SRMedicationRoute);
            PopulateCboConsumeMethod((String)DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.SRConsumeMethod), false);
            ComboBox.SelectedValue(cboConsumeMethod, (String)DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.SRConsumeMethod));
            txtPrescriptionQty.Text = (String)(DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.ItemQtyInString));
            //txtTakenQty.MaxValue = txtPrescriptionQty.Value ?? 70368744177664;
            txtTakenQty.Value = Convert.ToDouble(DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.TakenQty));
            txtResultQty.Value = Convert.ToDouble(DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.ResultQty));

            ViewState["CostPrice"] = Convert.ToDouble(DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.CostPrice));
            ViewState["InitialPrice"] = Convert.ToDouble(DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.InitialPrice));
            txtPrice.Value = Convert.ToDouble(DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.Price));
            txtRecipeAmount.Value = Convert.ToDouble(DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.RecipeAmount));
            txtEmbalaceAmount.Value = Convert.ToDouble(DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.EmbalaceAmount));
            txtDiscountAmount.Value = Convert.ToDouble(DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.DiscountAmount));
            ComboBox.SelectedValue(cboDiscountReason, (String)DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.SRDiscountReason));
            //chkIsCalcPercentage.Checked = ((bool?)DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.IsCalcPercentage)) ?? false;
            //chkIsUseSweetener.Checked = (bool)DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.IsUseSweetener);
            txtLineAmount.Value = Convert.ToDouble(DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.LineAmount));
            txtNotes.Text = (String)DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.Notes);
            ComboBox.SelectedValue(cboEmbalace, (String)DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.EmbalaceID));
            txtDosage.Text = (String)DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.DosageQty);
            txtOrder.Text = (String)DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.OrderText);
            txtIter.Text = (String)DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.IterText);
            txtQtyConsume.Text = (String)DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.ConsumeQty);
            txtDaysOfUsage.Value = Convert.ToDouble(DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.DaysOfUsage));
            ComboBox.SelectedValue(cboSRInterventionReason, (String)DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.SRInterventionReason));

            chkIsCasemixApproved.Checked = Convert.ToBoolean(DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.IsCasemixApproved));
            chkIsVoid.Checked = Convert.ToBoolean(DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.IsVoid));
            var casemixApprovedByUserId = (String)DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.CasemixApprovedByUserID);
            var casemixNotes = (String)DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.CasemixNotes);
            hdnCasemixApprovedUserId.Value = casemixApprovedByUserId;
            hdnCasemixNotes.Value = casemixNotes;
            if (!string.IsNullOrEmpty(casemixApprovedByUserId) && !string.IsNullOrEmpty(casemixNotes))
            {
                txtCasemixApprovedByUserID.Text = casemixNotes + " (by:" + casemixApprovedByUserId + ")";
            }
            else
                txtCasemixApprovedByUserID.Text = casemixApprovedByUserId;
            try
            {
                txtQty23Days.Value = Convert.ToDouble(DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.Qty23Days));
            }
            catch
            {
                txtQty23Days.Value = 0;
            }

            //var std = new AppStandardReferenceItemQuery();
            //std.Select(std.ItemID, std.ItemName);
            //std.Where
            //    (
            //    std.StandardReferenceID == AppEnum.StandardReference.DosageUnit,
            //    std.ItemID == (String)DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.SRConsumeUnit)
            //    );

            //var dtb = std.LoadDataTable();

            //if (chkIsCompound.Checked)
            //{
            //    var emb = new EmbalaceQuery();
            //    emb.Where(emb.EmbalaceID == (String)DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.SRConsumeUnit));
            //    emb.Select(emb.EmbalaceID.As("ItemID"), emb.EmbalaceName.As("ItemName"));
            //    dtb.Merge(emb.LoadDataTable());
            //}

            ////cboConsumeUnit.DataSource = dtb;
            ////cboConsumeUnit.DataBind();

            //if (cboParentNo.SelectedValue == string.Empty)
            //{
            //    foreach (DataRow row in dtb.Rows)
            //    {
            //        cboConsumeUnit.Items.Add(new RadComboBoxItem(row["ItemName"].ToString(), row["ItemID"].ToString()));
            //    }

            //    cboConsumeUnit.SelectedValue = (String)DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.SRConsumeUnit);
            //}

            ComboBox.SelectedValue(cboConsumeUnit, (String)DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.SRConsumeUnit));

            tdFormula.Visible = chkIsCompound.Checked;
            tdFormula2.Visible = chkIsCompound.Checked;
            tdFormula3.Visible = chkIsCompound.Checked;
            tdFormula4.Visible = chkIsCompound.Checked;
            txtTakenQty.ReadOnly = chkIsCompound.Checked;

            tdlblQty23Days.Visible = !ChkIs23Days.Checked && !chkIsCompound.Checked && IsGuarantorBpjs && Request.QueryString["rt"] == "opr" && (Request.QueryString["type"] == "realization" || Request.QueryString["type"] == "sales");
            tdTxtQty23Days.Visible = tdlblQty23Days.Visible;

            chkIsPending.Checked = ((bool?)DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.IsPendingDelivery)) ?? false;

            ShowPrevBuy(PatientIds, cboItemID.SelectedValue, cboItemID.Text, cboItemInterventionID.SelectedValue, cboItemInterventionID.Text);
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //if ((txtDiscountPercent.Value > 0 || txtDiscountAmount.Value > 0) && string.IsNullOrEmpty(cboDiscountReason.SelectedValue))
            //{
            //    args.IsValid = false;
            //    ((CustomValidator)source).ErrorMessage = "Discount reason is required";
            //}

            //if (Request.QueryString["type"] != "realization")
            //{
            //    if (txtResultQty.Value == 0)
            //    {
            //        args.IsValid = false;
            //        ((CustomValidator)source).ErrorMessage = "Qty must greather than 0";
            //    }
            //}

            // validation of discount must not be greater than price * qty
            if (txtDiscountPercent.Value > 100)
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = "Invalid discount in percent value";
                return;
            }

            if (getLineAmount() < 0)
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = "Invalid discount amount. Discount amount must not be greater than price multiplied by qty";
                return;
            }

            if (!string.IsNullOrEmpty(cboItemInterventionID.SelectedValue))
            {
                if (string.IsNullOrEmpty(cboSRInterventionReason.SelectedValue) && AppSession.Parameter.IsMandatoryInterventionReason)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = "Intervention Reason required.";
                    return;
                }
            }

            var itemID = string.IsNullOrEmpty(cboItemInterventionID.SelectedValue) ? cboItemID.SelectedValue : cboItemInterventionID.SelectedValue;
            var itemName = string.IsNullOrEmpty(cboItemInterventionID.SelectedValue) ? cboItemID.Text : cboItemInterventionID.Text;


            //untuk resep bpjs boleh dobel item untuk pemisahan tagihan
            //var bridging = new GuarantorBridging();
            //bridging.Query.Where(bridging.Query.GuarantorID == (Helper.FindControlRecursive(Page, "cboGuarantorID") as RadComboBox).SelectedValue,
            //                     bridging.Query.SRBridgingType.In(AppEnum.BridgingType.BPJS.ToString(),
            //                                                      AppEnum.BridgingType.BPJS_TNI_POLRI_PNS.ToString(),
            //                                                      AppEnum.BridgingType.BPJS_PASIEN_UMUM.ToString()));
            //if (!bridging.Query.Load())
            {
                CheckBox chkUnitDose = (Helper.FindControlRecursive(this.Page, "chkUnitDose") as CheckBox);
                if (!chkUnitDose.Checked)
                {
                    var parentNo = cboParentNo.SelectedValue;
                    var isCompound = chkIsCompound.Checked;

                    if (ViewState["IsNewRecord"].Equals(true))
                    {
                        var coll = (TransPrescriptionItemCollection)Session["collTransPrescriptionItem" + UniqueID() + PageId];
                        var isExist = coll.Any(entity => entity.ItemID.Equals(itemID) &&
                                                         entity.ParentNo.Equals(parentNo) &&
                                                         entity.IsCompound.Equals(isCompound) &&
                                                         (entity.IsPendingDelivery ?? false) == chkIsPending.Checked);
                        if (isExist)
                        {
                            args.IsValid = false;
                            ((CustomValidator)source).ErrorMessage = string.Format("Item: {0} has exist", itemName);
                            return;
                        }
                    }
                }
            }

            var item = new Item();
            if (item.LoadByPrimaryKey(itemID))
            {
                if (!chkIsPending.Checked)
                {
                    if (txtResultQty.Value > 0)
                    {
                        if (item.SRItemType == ItemType.Medical || item.SRItemType == ItemType.NonMedical || item.SRItemType == ItemType.Kitchen)
                        {
                            var cboLocationID = (RadComboBox)Helper.FindControlRecursive(Page, "cboLocationID");

                            var balance = new ItemBalance();
                            if (balance.LoadByPrimaryKey(cboLocationID.SelectedValue, itemID))
                            {
                                if (balance.Balance < 0)
                                {
                                    args.IsValid = false;
                                    ((CustomValidator)source).ErrorMessage = "Insufficient balance of item";
                                    return;
                                }
                                if (balance.Balance < (decimal)txtResultQty.Value)
                                {
                                    args.IsValid = false;
                                    ((CustomValidator)source).ErrorMessage = "Insufficient balance of item";
                                    return;
                                }
                            }
                            else
                            {
                                args.IsValid = false;
                                ((CustomValidator)source).ErrorMessage = "Insufficient balance of item";
                                return;
                            }
                        }
                    }
                }

                switch (item.SRItemType)
                {
                    case ItemType.Medical:
                        var imedic = new ItemProductMedic();
                        if (!imedic.LoadByPrimaryKey(itemID)) return;
                        if (!string.IsNullOrEmpty(imedic.SRItemUnit)) return;
                        break;
                    case ItemType.NonMedical:
                        var inmedic = new ItemProductNonMedic();
                        if (!inmedic.LoadByPrimaryKey(itemID)) return;
                        if (!string.IsNullOrEmpty(inmedic.SRItemUnit)) return;
                        break;
                    case ItemType.Kitchen:
                        var ik = new ItemKitchen();
                        if (!ik.LoadByPrimaryKey(itemID)) return;
                        if (!string.IsNullOrEmpty(ik.SRItemUnit)) return;
                        break;
                }

                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = "Invalid item unit";
            }
            else
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = "Item not found";
            }
        }

        //protected void txtFrequencyOfDosing_TextChanged(object sender, EventArgs e)
        //{
        //    PopulatePrescriptionQtyAndTakenQty();
        //    PopulateResultQtyAndLineAmount();
        //}

        //protected void txtNumberOfDosage_TextChanged(object sender, EventArgs e)
        //{
        //    PopulatePrescriptionQtyAndTakenQty();
        //    PopulateResultQtyAndLineAmount();
        //}

        //protected void txtDurationOfDosing_TextChanged(object sender, EventArgs e)
        //{
        //    PopulatePrescriptionQtyAndTakenQty();
        //    PopulateResultQtyAndLineAmount();
        //}

        protected void txtPrescriptionQty_TextChanged(object sender, EventArgs e)
        {
            if (!chkIsCompound.Checked) txtDosage.Text = txtPrescriptionQty.Text;

            txtTakenQty.Value = new Fraction(txtPrescriptionQty.Text);
            txtResultQty.Value = new Fraction(txtPrescriptionQty.Text);

            CalculateResultQtyAndLineAmount();
            PopulateResultQtyAndLineAmount();

            if (btnInsert.Visible)
                btnInsert.Focus();
            else
                btnUpdate.Focus();
        }

        protected void txtTakenQty_TextChanged(object sender, EventArgs e)
        {
            txtResultQty.Value = txtTakenQty.Value;

            PopulateResultQtyAndLineAmount();
        }

        protected void txtDiscountAmount_TextChanged(object sender, EventArgs e)
        {
            //txtLineAmount.Value = txtLineAmount.Value - txtDiscountAmount.Value; ;
            //PopulateResultQtyAndLineAmount();
            txtLineAmount.Value = getLineAmount();
        }

        protected void txtRecipeAmount_TextChanged(object sender, EventArgs e)
        {
            txtLineAmount.Value = getLineAmount();
        }

        //protected void txtItemQtyInString_TextChanged(object sender, EventArgs e)
        //{
        //    PopulateResultQtyAndLineAmount();
        //}

        protected void chkIsCompound_CheckedChanged(object sender, EventArgs e)
        {
            trHeader.Visible = chkIsCompound.Checked;
            trHeader2.Visible = chkIsCompound.Checked;
            //chkIsCalcPercentage.Enabled = false;
            //txtResultQty.ReadOnly = !chkIsCompound.Checked;

            txtItemUnit.Visible = !chkIsCompound.Checked;
            cboEmbalace.Visible = chkIsCompound.Checked;

            tdFormula.Visible = chkIsCompound.Checked;
            tdFormula2.Visible = chkIsCompound.Checked;
            tdFormula3.Visible = chkIsCompound.Checked;
            tdFormula4.Visible = chkIsCompound.Checked;

            tdlblQty23Days.Visible = !ChkIs23Days.Checked && !chkIsCompound.Checked && IsGuarantorBpjs && Request.QueryString["rt"] == "opr" && (Request.QueryString["type"] == "realization" || Request.QueryString["type"] == "sales");
            tdTxtQty23Days.Visible = tdlblQty23Days.Visible;
            txtQty23Days.Value = 0;

            pnlInfoLastBuy.Visible = false;
            //JHO DI DETAIL APOL AMBIL DARI SINI AJA - Danang
            if (Helper.IsApotekOnlineIntegration)
            {
                var reg = new Registration();
                if (reg.LoadByPrimaryKey(TxtRegistrationNo.Text))
                {
                    if (!string.IsNullOrWhiteSpace(reg.BpjsSepNo) && reg.GuarantorID == AppSession.Parameter.GuarantorAskesID[0])
                    {
                        txtDaysOfUsage.ReadOnly = false;
                    }
                }
            }
            else
                txtDaysOfUsage.ReadOnly = chkIsCompound.Checked;

            PopulateParentNo();
            SetEnabledControl(true);

            PopulateResultQtyAndLineAmount();
        }

        protected void cboParentNo_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            PopulateParentNoDetail();

            cboItemID.Focus();
        }

        private void PopulateParentNo()
        {
            cboParentNo.Items.Clear();
            cboParentNo.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));

            if (chkIsCompound.Checked)
            {
                var coll = (TransPrescriptionItemCollection)Session["collTransPrescriptionItem" + UniqueID() + PageId];
                foreach (var row in coll.Where(row => (row.IsCompound ?? false) && (row.ParentNo == string.Empty)))
                {
                    cboParentNo.Items.Add(new RadComboBoxItem(row.ItemName, row.SequenceNo));
                }
            }
        }

        //private void PopulatePrescriptionQtyAndTakenQty()
        //{
        //    double prescriptionQty = (txtFrequencyOfDosing.Value ?? 0) * (txtNumberOfDosage.Value ?? 0) * (txtDurationOfDosing.Value ?? 0);

        //    if (txtTakenQty.Value == null || txtTakenQty.Value == 0 ||
        //        txtTakenQty.Value.Equals(txtPrescriptionQty.Value)) //Samakan taken qty dgn jumlah resep
        //        txtTakenQty.Value = prescriptionQty;

        //    txtPrescriptionQty.Value = prescriptionQty;
        //}

        private void PopulateItemInfo(string itemID)
        {
            //txtDiscountAmount.Value = 0;
            //txtPrice.Value = 0;
            //txtLineAmount.Value = 0;
            //cboSRItemUnitOrEmbalaceID.Items.Clear();

            if (itemID == string.Empty)
                return;

            var query = new ItemQuery("a");
            var qMed = new VwItemProductMedicNonMedicQuery("b");

            query.Select
                (
                    query.ItemID,
                    query.ItemName,
                    qMed.Dosage,
                    qMed.SRDosageUnit,
                    qMed.IsUsingCigna,
                    qMed.SRItemUnit,
                    qMed.FornasRestrictionNotes
                );
            query.LeftJoin(qMed).On(query.ItemID == qMed.ItemID);
            query.Where(query.ItemID == itemID);

            var dtb = query.LoadDataTable();
            if (dtb.Rows.Count > 0)
            {
                txtItemUnit.Text = dtb.Rows[0]["SRItemUnit"].ToString();
                cboItemUnit.SelectedValue = string.Empty;
                cboItemUnit.Items.Clear();
                lblFornasRestrictionNotes.Text = dtb.Rows[0]["FornasRestrictionNotes"].ToString();

                if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSCH")
                {
                    cboItemUnit.Items.Add(new RadComboBoxItem(dtb.Rows[0]["SRItemUnit"].ToString(), dtb.Rows[0]["SRItemUnit"].ToString()));

                    if (dtb.Rows[0]["SRDosageUnit"].ToString() != dtb.Rows[0]["SRItemUnit"].ToString() && !string.IsNullOrEmpty(dtb.Rows[0]["SRDosageUnit"].ToString()))
                    {
                        cboItemUnit.Items.Add(new RadComboBoxItem(dtb.Rows[0]["SRDosageUnit"].ToString(), dtb.Rows[0]["SRDosageUnit"].ToString()));
                    }

                    var dosages = new ItemProductDosageDetailCollection();
                    dosages.Query.Where(dosages.Query.ItemID == itemID);
                    dosages.LoadAll();

                    foreach (var dosage in dosages)
                    {
                        if (cboItemUnit.Items.Select(c => c.Value).Contains(dosage.SRDosageUnit)) continue;
                        cboItemUnit.Items.Add(new RadComboBoxItem(dosage.SRDosageUnit, dosage.SRDosageUnit));
                    }
                }
                else
                {
                    var prevConsumeUnitVal = cboConsumeUnit.SelectedValue;
                    cboConsumeUnit.Items.Clear();

                    cboItemUnit.Items.Add(new RadComboBoxItem(dtb.Rows[0]["SRItemUnit"].ToString(), dtb.Rows[0]["SRItemUnit"].ToString()));
                    cboConsumeUnit.Items.Add(new RadComboBoxItem(dtb.Rows[0]["SRItemUnit"].ToString(), dtb.Rows[0]["SRItemUnit"].ToString()));

                    if (dtb.Rows[0]["SRDosageUnit"].ToString() != dtb.Rows[0]["SRItemUnit"].ToString() && !string.IsNullOrEmpty(dtb.Rows[0]["SRDosageUnit"].ToString()))
                    {
                        if (!cboItemUnit.Items.Select(c => c.Value).Contains(dtb.Rows[0]["SRDosageUnit"].ToString()))
                            cboItemUnit.Items.Add(new RadComboBoxItem(dtb.Rows[0]["SRDosageUnit"].ToString(), dtb.Rows[0]["SRDosageUnit"].ToString()));

                        if (!cboConsumeUnit.Items.Select(c => c.Value).Contains(dtb.Rows[0]["SRDosageUnit"].ToString()))
                            cboConsumeUnit.Items.Add(new RadComboBoxItem(dtb.Rows[0]["SRDosageUnit"].ToString(), dtb.Rows[0]["SRDosageUnit"].ToString()));
                    }

                    var dosages = new ItemProductDosageDetailCollection();
                    dosages.Query.Where(dosages.Query.ItemID == itemID);
                    dosages.LoadAll();

                    foreach (var dosage in dosages)
                    {
                        if (!cboItemUnit.Items.Select(c => c.Value).Contains(dosage.SRDosageUnit))
                            cboItemUnit.Items.Add(new RadComboBoxItem(dosage.SRDosageUnit, dosage.SRDosageUnit));

                        if (!cboConsumeUnit.Items.Select(c => c.Value).Contains(dosage.SRDosageUnit))
                            cboConsumeUnit.Items.Add(new RadComboBoxItem(dosage.SRDosageUnit, dosage.SRDosageUnit));
                    }

                    var std = new AppStandardReferenceItemCollection();
                    std.Query.Where(std.Query.StandardReferenceID == AppEnum.StandardReference.GlobalConsumeUnit,
                        std.Query.ItemID.NotIn(cboConsumeUnit.Items.Select(c => c.Value)),
                        std.Query.IsActive == true);
                    if (std.Query.Load())
                    {
                        foreach (var entity in std)
                        {
                            if (!cboConsumeUnit.Items.Select(c => c.Value).Contains(entity.ItemID))
                                cboConsumeUnit.Items.Add(new RadComboBoxItem(entity.ItemName, entity.ItemID));
                        }
                    }

                    var emb = new EmbalaceCollection();
                    if (emb.Query.Load())
                    {
                        foreach (var entity in emb)
                        {
                            if (!cboConsumeUnit.Items.Select(c => c.Value).Contains(entity.EmbalaceID))
                                cboConsumeUnit.Items.Add(new RadComboBoxItem(entity.EmbalaceName, entity.EmbalaceID));
                        }
                    }

                    // Restore selected value
                    ComboBox.SelectedValue(cboConsumeUnit, prevConsumeUnitVal);

                }
            }

            PopulateParentNoDetail();
        }

        private string GetCoveredItem(string itemId)
        {
            var grr = new Guarantor();
            grr.LoadByPrimaryKey(CboGuarantorId.SelectedValue);

            if (grr.SRGuarantorType != AppSession.Parameter.GuarantorTypeSelf)
            {
                bool isInclude, isGuarantor;
                string srGuarantorRuleType = string.Empty;
                decimal amountValue = 0;
                bool isValueInPercent = true;
                string srTherapyGroup = string.Empty;

                var types = new GuarantorItemTypeRuleCollection();
                types.Query.Where(types.Query.GuarantorID == CboGuarantorId.SelectedValue);
                types.LoadAll();

                var item = new Item();
                item.LoadByPrimaryKey(itemId);
                if (item.SRItemType == ItemType.Medical)
                {
                    isInclude = grr.IsIncludeItemMedical ?? false;
                    isGuarantor = grr.IsIncludeItemMedicalToGuarantor ?? false;
                    var ipm = new ItemProductMedic();
                    if (ipm.LoadByPrimaryKey(itemId))
                        srTherapyGroup = ipm.SRTherapyGroup;
                }
                else if (item.SRItemType == ItemType.NonMedical || item.SRItemType == ItemType.Kitchen)
                {
                    isInclude = grr.IsIncludeItemNonMedical ?? false;
                    isGuarantor = grr.IsIncludeItemNonMedicalToGuarantor ?? false;
                }
                else
                {
                    isInclude = true;
                    if (types.AsEnumerable().Any())
                        isGuarantor = types.SingleOrDefault(t => t.SRItemType == item.SRItemType).IsToGuarantor ?? false;
                    else
                        isGuarantor = true;
                }

                var grrItem = new GuarantorItemRule();
                if (grrItem.LoadByPrimaryKey(CboGuarantorId.SelectedValue, itemId))
                {
                    srGuarantorRuleType = grrItem.SRGuarantorRuleType;
                    amountValue = grrItem.AmountValue ?? 0;
                    isValueInPercent = grrItem.IsValueInPercent ?? false;
                    isInclude = grrItem.IsInclude ?? false;
                    isGuarantor = grrItem.IsToGuarantor ?? false;
                }

                var grrItemTherapy = new GuarantorItemPrescriptionByTherapyRule();
                if (grrItemTherapy.LoadByPrimaryKey(CboGuarantorId.SelectedValue, srTherapyGroup))
                {
                    srGuarantorRuleType = grrItemTherapy.SRGuarantorRuleType;
                    amountValue = grrItemTherapy.AmountValue ?? 0;
                    isValueInPercent = grrItemTherapy.IsValueInPercent ?? false;
                    isInclude = grrItemTherapy.IsInclude ?? false;
                    isGuarantor = grrItemTherapy.IsToGuarantor ?? false;
                }

                var grrItemPresc = new GuarantorItemPrescriptionRule();
                if (grrItemPresc.LoadByPrimaryKey(CboGuarantorId.SelectedValue, itemId))
                {
                    srGuarantorRuleType = grrItemPresc.SRGuarantorRuleType;
                    amountValue = grrItemPresc.AmountValue ?? 0;
                    isValueInPercent = grrItemPresc.IsValueInPercent ?? false;
                    isInclude = grrItemPresc.IsInclude ?? false;
                    isGuarantor = grrItemPresc.IsToGuarantor ?? false;
                }

                var regItems = new RegistrationItemRuleCollection();
                regItems.Query.Where(regItems.Query.RegistrationNo == TxtRegistrationNo.Text, regItems.Query.ItemID == itemId);
                regItems.LoadAll();
                foreach (var regItem in regItems)
                {
                    srGuarantorRuleType = regItem.SRGuarantorRuleType;
                    amountValue = regItem.AmountValue ?? 0;
                    isValueInPercent = regItem.IsValueInPercent ?? false;
                    isInclude = regItem.IsInclude ?? false;
                    isGuarantor = regItem.IsToGuarantor ?? false;
                }

                if (string.IsNullOrEmpty(srGuarantorRuleType) & isGuarantor)
                    return string.Empty;

                if (string.IsNullOrEmpty(srGuarantorRuleType) & isGuarantor == false)
                    return "** Item not covered by the guarantor.";

                var msg = "** Item guarantor rule: ";
                if (isInclude)
                {
                    var std = new AppStandardReferenceItem();
                    if (std.LoadByPrimaryKey(AppEnum.StandardReference.GuarantorRuleType.ToString(), srGuarantorRuleType))
                    {
                        msg += std.ItemName + " with value ";

                        if (isValueInPercent)
                            msg += string.Format("{0:n0}", amountValue) + "%";
                        else
                            msg += "Rp. " + string.Format("{0:n0}", amountValue);

                        if (isGuarantor)
                            msg += " to Guarantor";
                        else
                            msg += " to Patient";
                    }
                }

                return msg;
            }

            return string.Empty;
        }

        //private void PopulateItemInfoEdit(string itemID)
        //{
        //    //txtDiscountAmount.Value = 0;
        //    //txtPrice.Value = 0;

        //    //txtDiscountPercent.ReadOnly = true;
        //    //txtDiscountPercent.Value = 0;
        //    //txtDiscountAmount.ReadOnly = true;
        //    //txtDiscountAmount.Value = 0;

        //    //txtLineAmount.Value = 0;
        //    //cboSRItemUnitOrEmbalaceID.Items.Clear();

        //    if (itemID == string.Empty)
        //        return;

        //    var query = new ItemQuery("a");
        //    //var qMed = new ItemProductMedicQuery("b");
        //    var qMed = new VwItemProductMedicNonMedicQuery("b");
        //    query.LeftJoin(qMed).On(query.ItemID == qMed.ItemID);
        //    query.Select
        //        (
        //            query.ItemID,
        //            query.ItemName,
        //            qMed.Dosage,
        //            qMed.SRDosageUnit,
        //            qMed.IsUsingCigna,
        //            qMed.SRItemUnit
        //        );
        //    query.Where(query.ItemID == itemID);

        //    var dtb = query.LoadDataTable();
        //    if (dtb.Rows.Count > 0)
        //    {
        //        //trCignaEntry.Visible = dtb.Rows[0]["IsUsingCigna"] != DBNull.Value && Convert.ToBoolean(dtb.Rows[0]["IsUsingCigna"]);

        //        txtItemUnit.Text = dtb.Rows[0]["SRItemUnit"].ToString();
        //        cboItemUnit.Items.Clear();
        //        cboItemUnit.Items.Add(new RadComboBoxItem(dtb.Rows[0]["SRItemUnit"].ToString(), dtb.Rows[0]["SRItemUnit"].ToString()));
        //        if (dtb.Rows[0]["SRDosageUnit"].ToString() != dtb.Rows[0]["SRItemUnit"].ToString() && !string.IsNullOrEmpty(dtb.Rows[0]["SRDosageUnit"].ToString()))
        //            cboItemUnit.Items.Add(new RadComboBoxItem(dtb.Rows[0]["SRDosageUnit"].ToString(), dtb.Rows[0]["SRDosageUnit"].ToString()));

        //        //cboSRItemUnitOrEmbalaceID.Items.Add(new RadComboBoxItem(dtb.Rows[0]["SRDosageUnit"].ToString(), dtb.Rows[0]["SRDosageUnit"].ToString()));
        //    }
        //}

        private void PopulateParentNoDetail()
        {
            //chkIsCalcPercentage.Checked = false;

            if (cboParentNo.SelectedValue != string.Empty)
            {
                var coll = (TransPrescriptionItemCollection)Session["collTransPrescriptionItem" + UniqueID() + PageId];

                var row = coll.FindByPrimaryKey(((RadTextBox)Helper.FindControlRecursive(Page, "txtPrescriptionNo")).Text, cboParentNo.SelectedValue);
                //txtFrequencyOfDosing.Value = row.FrequencyOfDosing;
                //cboDosingPeriod.SelectedValue = row.DosingPeriod;
                //cboSRMedicationRoute.SelectedValue = row.SRMedicationRoute;
                //txtDurationOfDosing.Value = row.DurationOfDosing;

                txtPrescriptionQty.Text = row.ItemQtyInString;
                //txtTakenQty.Value = Convert.ToDouble(row.TakenQty);
                //txtResultQty.Value = Convert.ToDouble(row.ResultQty);
                ComboBox.SelectedValue(cboEmbalace, row.EmbalaceID);
                PopulateCboConsumeMethod(row.SRConsumeMethod, false);
                ComboBox.SelectedValue(cboConsumeMethod, row.SRConsumeMethod);

                txtQtyConsume.Text = row.ConsumeQty;

                var std = new AppStandardReferenceItemQuery();
                std.Select(std.ItemID, std.ItemName);
                std.Where
                    (
                    std.StandardReferenceID == AppEnum.StandardReference.DosageUnit,
                    std.ItemID == row.SRConsumeUnit
                    );

                var dtb = std.LoadDataTable();

                if (chkIsCompound.Checked)
                {
                    var emb = new EmbalaceQuery();
                    emb.Where(emb.EmbalaceID == row.SRConsumeUnit);
                    emb.Select(emb.EmbalaceID.As("ItemID"), emb.EmbalaceName.As("ItemName"));
                    dtb.Merge(emb.LoadDataTable());

                    // WTF (what to find?)
                    var stdGC = new AppStandardReferenceItemQuery();
                    stdGC.Select(stdGC.ItemID, stdGC.ItemName);
                    stdGC.Where
                        (
                        stdGC.StandardReferenceID == AppEnum.StandardReference.GlobalConsumeUnit,
                        stdGC.ItemID == row.SRConsumeUnit
                        );
                    dtb.Merge(stdGC.LoadDataTable());
                }

                cboConsumeUnit.SelectedValue = string.Empty;
                cboConsumeUnit.DataSource = dtb;
                cboConsumeUnit.DataBind();
                ComboBox.SelectedValue(cboConsumeUnit, row.SRConsumeUnit);

                txtRecipeAmount.Value = 0;
                txtOrder.Text = row.OrderText;
                txtIter.Text = row.IterText;
                txtDaysOfUsage.Value = row.DaysOfUsage;

                if (!string.IsNullOrEmpty(cboItemID.SelectedValue))
                {
                    try
                    {
                        CalculateResultQtyAndLineAmount();
                        PopulateResultQtyAndLineAmount();
                    }
                    catch { }
                }

                chkIsPending.Checked = row.IsPendingDelivery ?? false;

                SetEnabledControl(false);
            }
            else
                SetEnabledControl(true);
        }

        private void SetEnabledControl(bool param)
        {
            //txtFrequencyOfDosing.Enabled = param;
            //cboDosingPeriod.Enabled = param;
            //cboSRMedicationRoute.Enabled = param;
            //cboAcpcdc.Enabled = param;
            //txtDurationOfDosing.Enabled = param;
            txtPrescriptionQty.ReadOnly = !param;
            cboEmbalace.Enabled = param;
            cboConsumeMethod.Enabled = param;
            txtTakenQty.ReadOnly = !param;
            //txtResultQty.ReadOnly = !param;
            //chkIsCalcPercentage.Enabled = !param;
            txtOrder.ReadOnly = !param;
            txtIter.ReadOnly = !param;
            txtQtyConsume.ReadOnly = !param;
            cboConsumeUnit.Enabled = param;
            chkIsPending.Enabled = param;
        }

        //public static double RecipeForNonCompound(string entryMode, string itemId, decimal resultQty)
        //{
        //    double recipeAmount = 0;
        //    if (AppParameter.IsYes(AppParameter.ParameterItem.IsOtcFreeRecipeMargin))
        //    {
        //        if (!string.IsNullOrEmpty(entryMode) && entryMode == "otc")
        //            recipeAmount = 0;
        //        else
        //            if (resultQty > 0)
        //            recipeAmount = Convert.ToDouble(AppParameter.GetParameterValue(AppParameter.ParameterItem.RecipeMarginValueNonCompound));
        //        else
        //            recipeAmount = 0;
        //    }
        //    else
        //    {
        //        if (resultQty > 0)
        //        {
        //            recipeAmount = Convert.ToDouble(AppParameter.GetParameterValue(AppParameter.ParameterItem.RecipeMarginValueNonCompound));

        //            var i = new ItemProductMedic();
        //            if (i.LoadByPrimaryKey(itemId))
        //            {
        //                if (i.IsNoPrescriptionFee ?? false)
        //                    recipeAmount = 0;
        //            }
        //        }
        //        else
        //            recipeAmount = 0;

        //    }
        //    return recipeAmount;
        //}
        //public static double RecipeForCompound(string entryMode, string parentNo, string prescriptionQty)
        //{
        //    double recipeAmount = 0;
        //    if (string.IsNullOrEmpty(parentNo) || AppParameter.IsYes(AppParameter.ParameterItem.IsRecipeMarginValueForEachItemCompound))
        //    {
        //        var margin = new RecipeMarginValue();
        //        margin.Query.Where(string.Format("<{0} BETWEEN StartingValue AND EndingValue>",
        //            new Fraction(string.IsNullOrEmpty(prescriptionQty) ? "0" : prescriptionQty)));
        //        if (margin.Query.Load()) recipeAmount = Convert.ToDouble(margin.RecipeAmount);
        //        else recipeAmount = 0;
        //    }
        //    else recipeAmount = 0;
        //    return recipeAmount;
        //}
        private void PopulateResultQtyAndLineAmount()
        {
            if (!chkIsCompound.Checked)
            {
                // RS UTAMA, OBAT BIASA PASIEN OTC TIDAK KENA UANG RACIK
                //if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSUTAMA")
                //{
                //    if (!string.IsNullOrEmpty(Request.QueryString["mode"]) && Request.QueryString["mode"] == "otc")
                //        txtRecipeAmount.Value = 0;
                //    else
                //        if (txtResultQty.Value > 0)
                //        txtRecipeAmount.Value = Convert.ToDouble(AppParameter.GetParameterValue(AppParameter.ParameterItem.RecipeMarginValueNonCompound));
                //    else
                //        txtRecipeAmount.Value = 0;
                //}
                //else
                //{
                //    if (txtResultQty.Value > 0)
                //    {
                //        txtRecipeAmount.Value = Convert.ToDouble(AppParameter.GetParameterValue(AppParameter.ParameterItem.RecipeMarginValueNonCompound));

                //        var itemId = string.IsNullOrEmpty(cboItemInterventionID.SelectedValue) ? cboItemID.SelectedValue : cboItemInterventionID.SelectedValue;
                //        var i = new ItemProductMedic();
                //        if (i.LoadByPrimaryKey(itemId))
                //        {
                //            if (i.IsNoPrescriptionFee ?? false)
                //                txtRecipeAmount.Value = 0;
                //        }
                //    }
                //    else
                //        txtRecipeAmount.Value = 0;

                //    txtRecipeAmount.ReadOnly = !(txtRecipeAmount.Value == 0);
                //}

                var itemId = string.IsNullOrEmpty(cboItemInterventionID.SelectedValue) ? cboItemID.SelectedValue : cboItemInterventionID.SelectedValue;
                var recipe = new TransPrescription();
                txtRecipeAmount.Value = recipe.RecipeAmount(Request.QueryString["mode"], CboGuarantorId.SelectedValue, itemId, Convert.ToDecimal(txtResultQty.Value ?? 0), cboParentNo.SelectedValue, txtPrescriptionQty.Text, chkIsCompound.Checked);
                //txtRecipeAmount.Value = RecipeForNonCompound(Request.QueryString["mode"], itemId, Convert.ToDecimal(txtResultQty.Value ?? 0));
                if (AppSession.Parameter.GuarantorIdExeptionForRecipeAmount.Contains(CboGuarantorId.SelectedValue))
                    txtRecipeAmount.Value = 0;

                if (AppSession.Parameter.HealthcareInitialAppsVersion != "RSUTAMA" && AppSession.Parameter.HealthcareInitialAppsVersion != "KLUTAMA")
                    txtRecipeAmount.ReadOnly = !(txtRecipeAmount.Value == 0);
            }
            else
            {
                //if (string.IsNullOrEmpty(cboParentNo.SelectedValue) || AppParameter.IsYes(AppParameter.ParameterItem.IsRecipeMarginValueForEachItemCompound))
                //{
                //    var margin = new RecipeMarginValue();
                //    margin.Query.Where(string.Format("<{0} BETWEEN StartingValue AND EndingValue>",
                //        new Fraction(string.IsNullOrEmpty(txtPrescriptionQty.Text) ? "0" : txtPrescriptionQty.Text)));
                //    if (margin.Query.Load()) txtRecipeAmount.Value = Convert.ToDouble(margin.RecipeAmount);
                //    else txtRecipeAmount.Value = 0;
                //}
                //else txtRecipeAmount.Value = 0;
                //txtRecipeAmount.ReadOnly = !(txtRecipeAmount.Value == 0);

                var itemId = string.IsNullOrEmpty(cboItemInterventionID.SelectedValue) ? cboItemID.SelectedValue : cboItemInterventionID.SelectedValue;
                var recipe = new TransPrescription();
                txtRecipeAmount.Value = recipe.RecipeAmount(Request.QueryString["mode"], CboGuarantorId.SelectedValue, itemId, Convert.ToDecimal(txtResultQty.Value ?? 0), cboParentNo.SelectedValue, txtPrescriptionQty.Text, chkIsCompound.Checked);
                //txtRecipeAmount.Value = RecipeForCompound(Request.QueryString["mode"], cboParentNo.SelectedValue, txtPrescriptionQty.Text);
                if (AppSession.Parameter.GuarantorIdExeptionForRecipeAmount.Contains(CboGuarantorId.SelectedValue))
                    txtRecipeAmount.Value = 0;

                txtRecipeAmount.ReadOnly = !(txtRecipeAmount.Value == 0);
            }

            if (txtResultQty.Value == 0) txtRecipeAmount.Value = 0;

            //var lineAmt = (txtResultQty.Value * (txtPrice.Value - txtDiscountAmount.Value)) + txtRecipeAmount.Value + Convert.ToDouble(EmbalaceAmount);
            //if (lineAmt <= 0) lineAmt = (txtResultQty.Value * txtPrice.Value) + txtRecipeAmount.Value + Convert.ToDouble(EmbalaceAmount);
            //txtLineAmount.Value = Convert.ToDouble(Helper.Rounding(Convert.ToDecimal(lineAmt), AppEnum.RoundingType.Prescription));

            //txtDiscountAmount.Value = (txtDiscountPercent.Value / 100D * (txtLineAmount.Value));
            //txtLineAmount.Value = txtLineAmount.Value - txtDiscountAmount.Value;

            if (!Convert.ToBoolean(ViewState["IsNewRecord"]))
            {
                if (txtDiscountAmount.Value > 0) txtDiscountAmount.Value *= (txtTakenQty.Value / Convert.ToDouble(txtPrescriptionQty.Text));
            }

            txtLineAmount.Value = getLineAmount();
        }

        private double getLineAmount()
        {
            var lineAmt = txtResultQty.Value * txtPrice.Value + txtRecipeAmount.Value + Convert.ToDouble(EmbalaceAmount);
            if (!txtDiscountAmount.Value.HasValue) txtDiscountAmount.Value = 0;
            if (txtDiscountPercent.Value > 0)
            {
                bool IsIncR = (AppSession.Parameter.IsPrescriptionDiscountIncludeR);
                decimal rPrice = (Convert.ToDecimal(EmbalaceAmount) + Convert.ToDecimal(txtRecipeAmount.Value));
                decimal _lineAmt = Convert.ToDecimal(txtResultQty.Value * txtPrice.Value) + (IsIncR ? rPrice : 0);

                if (AppSession.Parameter.IsPrescriptionDiscountAfterRounding)
                    _lineAmt = Helper.Rounding(_lineAmt, AppEnum.RoundingType.Prescription);

                txtDiscountAmount.Value = (txtDiscountPercent.Value / 100D * (Convert.ToDouble(_lineAmt)));
            }
            txtEmbalaceAmount.Value = Convert.ToDouble(EmbalaceAmount);

            if (AppSession.Parameter.IsPrescriptionDiscountAfterRounding)
                lineAmt = Convert.ToDouble(Helper.Rounding(Convert.ToDecimal(lineAmt), AppEnum.RoundingType.Prescription)) - txtDiscountAmount.Value;
            else
                lineAmt = Convert.ToDouble(Helper.Rounding(Convert.ToDecimal(lineAmt - txtDiscountAmount.Value), AppEnum.RoundingType.Prescription));

            return Convert.ToDouble(lineAmt);
        }

        #region Properties for return entry value

        public String QtyOfDosage
        {
            get { return txtQtyConsume.Text; }
        }

        public String SequenceNo
        {
            get { return (string)ViewState["SequenceNo"]; }
        }

        public Boolean IsCompound
        {
            get { return chkIsCompound.Checked; }
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
            get { return cboAcPcDc.SelectedValue; }
        }
        public String ItemInterventionID
        {
            get { return cboItemInterventionID.SelectedValue; }
        }

        public String ItemInterventionName
        {
            get { return cboItemInterventionID.Text; }
        }

        //public Boolean IsUsingDosageUnit
        //{
        //    get { return false; }
        //}

        public String SRDosageUnit
        {
            get { return cboItemUnit.SelectedValue; }
        }

        public String ItemUnit
        {
            get
            {
                var itemID = string.IsNullOrEmpty(cboItemInterventionID.SelectedValue) ? cboItemID.SelectedValue : cboItemInterventionID.SelectedValue;
                string srItemUnit;

                var item = new ItemProductMedic();
                if (item.LoadByPrimaryKey(itemID))
                    srItemUnit = item.SRItemUnit;
                else
                {
                    var ipnm = new ItemProductNonMedic();
                    if (ipnm.LoadByPrimaryKey(itemID))
                        srItemUnit = ipnm.SRItemUnit;
                    else
                    {
                        var ik = new ItemKitchen();
                        ik.LoadByPrimaryKey(itemID);
                        srItemUnit = ik.SRItemUnit;
                    }
                }

                return srItemUnit;
            }
        }

        public String ItemQtyInString
        {
            get { return txtPrescriptionQty.Text; }
        }

        //public Byte FrequencyOfDosing
        //{
        //    get { return Convert.ToByte(txtFrequencyOfDosing.Value ?? 0); }
        //}

        //public String DosingPeriod
        //{
        //    get { return cboDosingPeriod.SelectedValue; }
        //}

        public string NumberOfDosage
        {
            get { return txtDosage.Text; }
        }

        //public Byte DurationOfDosing
        //{
        //    get { return Convert.ToByte(txtDurationOfDosing.Value ?? 0); }
        //}

        //public String Acpcdc
        //{
        //    get { return cboAcpcdc.SelectedValue; }
        //}

        //public String SRMedicationRoute
        //{
        //    get { return cboSRMedicationRoute.SelectedValue; }
        //}

        //public String ConsumeMethod
        //{
        //    get
        //    {
        //        var cm = new ConsumeMethod();
        //        cm.LoadByPrimaryKey(SRConsumeMethod);

        //        return string.IsNullOrEmpty(cm.SygnaText) ? string.Empty : cm.SygnaText;
        //    }
        //}

        public String SRConsumeMethod
        {
            get { return cboConsumeMethod.SelectedValue; }
        }

        public String SRConsumeMethodName
        {
            get { return cboConsumeMethod.Text; }
        }
        public String AcPcDcName
        {
            get { return cboAcPcDc.Text; }
        }
        public Decimal PrescriptionQty
        {
            get { return Convert.ToDecimal(new Fraction(txtPrescriptionQty.Text)); }
        }

        public Decimal TakenQty
        {
            get { return Convert.ToDecimal(txtTakenQty.Value); }
        }

        public Decimal ResultQty
        {
            get { return Convert.ToDecimal(txtResultQty.Value); }
        }

        public Decimal CostPrice
        {
            get { return Convert.ToDecimal(ViewState["CostPrice"]); }
        }

        public Decimal InitialPrice
        {
            get { return Convert.ToDecimal(ViewState["InitialPrice"]); }
        }

        public Decimal Price
        {
            get { return Convert.ToDecimal(txtPrice.Value); }
        }

        public Decimal RecipeAmount
        {
            get { return Convert.ToDecimal(txtRecipeAmount.Value); }
        }

        public Decimal DiscountAmount
        {
            get { return Convert.ToDecimal(txtDiscountAmount.Value); }
        }

        public String SRDiscountReason
        {
            get { return cboDiscountReason.SelectedValue; }
        }

        public String EmbalaceID
        {
            get
            {
                return chkIsCompound.Checked ? cboEmbalace.SelectedValue : string.Empty;
            }
        }

        public decimal EmbalaceAmount
        {
            get
            {
                if (string.IsNullOrEmpty(EmbalaceID)) return 0;
                if (IsCompound && !string.IsNullOrEmpty(ParentNo)) return 0;
                if (ResultQty == 0) return 0;
                var emb = new Embalace();
                emb.LoadByPrimaryKey(EmbalaceID);
                return emb.EmbalaceFeeAmount ?? 0;
            }
        }

        public String EmbalaceLabel
        {
            get
            {
                return chkIsCompound.Checked ? cboEmbalace.Text : string.Empty;
            }
        }

        //public Boolean IsCalcPercentage
        //{
        //    get { return chkIsCalcPercentage.Checked; }
        //}

        //public Boolean IsUseSweetener
        //{
        //    get { return chkIsUseSweetener.Checked; }
        //}

        public Decimal LineAmount
        {
            get { return Convert.ToDecimal(txtLineAmount.Value); }
        }

        public String Notes
        {
            get { return txtNotes.Text; }
        }

        public string EmbalaceQty
        {
            get
            {
                if (IsCompound)
                    return ItemQtyInString;
                else
                    return "0";
            }
        }

        public String Iter
        {
            get { return txtIter.Text; }
        }

        public String Order
        {
            get { return txtOrder.Text; }
        }

        public String SRConsumeUnit
        {
            get { return cboConsumeUnit.SelectedValue; }
        }

        public String SRInterventionReason
        {
            get { return cboSRInterventionReason.SelectedValue; }
        }

        public Boolean IsPendingDelivery
        {
            get { return chkIsPending.Checked; }
        }

        public Boolean IsCasemixApproved
        {
            get { return chkIsCasemixApproved.Checked; }
        }

        public Boolean IsVoid
        {
            get { return chkIsVoid.Checked; }
        }

        public double DaysOfUsage
        {
            get { return txtDaysOfUsage.Value ?? 0; }
        }

        public String CasemixApprovedByUserID
        {
            get { return hdnCasemixApprovedUserId.Value; }
        }

        public String CasemixNotes
        {
            get { return hdnCasemixNotes.Value; }
        }

        public Decimal Qty23Days
        {
            get { return Convert.ToDecimal(txtQty23Days.Value); }
        }

        public String FornasRestrictionNotes
        {
            get { return lblFornasRestrictionNotes.Text; }
        }

        #endregion

        protected void txtDiscountPercent_TextChanged(object sender, EventArgs e)
        {
            //var lineAmt = (txtResultQty.Value * (txtPrice.Value - txtDiscountAmount.Value)) + txtRecipeAmount.Value + Convert.ToDouble(EmbalaceAmount);
            //if (lineAmt <= 0) lineAmt = (txtResultQty.Value * txtPrice.Value) + txtRecipeAmount.Value + Convert.ToDouble(EmbalaceAmount);
            //txtLineAmount.Value = Convert.ToDouble(Helper.Rounding(Convert.ToDecimal(lineAmt), AppEnum.RoundingType.Prescription));

            //txtDiscountAmount.Value = (txtDiscountPercent.Value / 100D * (txtLineAmount.Value));
            //txtLineAmount.Value = txtLineAmount.Value - txtDiscountAmount.Value;
            //PopulateResultQtyAndLineAmount();
            txtLineAmount.Value = getLineAmount();
        }

        //protected void chkIsCalcPercentage_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (chkIsCalcPercentage.Checked)
        //        PopulateResultQtyAndLineAmount2();
        //    else
        //        PopulateResultQtyAndLineAmount();
        //}

        protected void cboEmbalace_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            PopulateResultQtyAndLineAmount();
        }

        protected void cboItemUnit_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            CalculateResultQtyAndLineAmount();
            PopulateResultQtyAndLineAmount();
        }

        protected void txtDosage_TextChanged(object sender, EventArgs e)
        {
            CalculateResultQtyAndLineAmount();
            PopulateResultQtyAndLineAmount();
        }

        private void CalculateResultQtyAndLineAmount()
        {
            if (!IsCompound) return;

            decimal itemDosage = 1;
            var item = new ItemProductMedic();
            if (item.LoadByPrimaryKey(string.IsNullOrEmpty(cboItemInterventionID.SelectedValue) ? cboItemID.SelectedValue : cboItemInterventionID.SelectedValue))
                itemDosage = item.Dosage ?? 1;

            var qty = new Fraction((txtPrescriptionQty.Text));
            var dosing = new Fraction((txtDosage.Text));

            if (item.SRItemUnit == SRDosageUnit)
            {
                txtTakenQty.Value = dosing * qty;
                txtResultQty.Value = dosing * qty;
            }
            else
            {
                if (item.SRDosageUnit == SRDosageUnit)
                {
                    txtTakenQty.Value = (dosing / Convert.ToDouble(itemDosage)) * qty;
                    txtResultQty.Value = (dosing / Convert.ToDouble(itemDosage)) * qty;
                }
                else
                {
                    var detail = new ItemProductDosageDetailCollection();
                    detail.Query.Where(detail.Query.ItemID == item.ItemID);
                    detail.LoadAll();

                    var dosage = detail.SingleOrDefault(d => d.SRDosageUnit == SRDosageUnit);
                    if (dosage != null)
                    {
                        txtTakenQty.Value = (dosing / Convert.ToDouble(dosage.Dosage)) * qty;
                        txtResultQty.Value = (dosing / Convert.ToDouble(dosage.Dosage)) * qty;
                    }
                }
            }

            var x = Convert.ToDecimal(txtTakenQty.Value) - Math.Floor(Convert.ToDecimal(txtTakenQty.Value));

            if (item.IsActualDeduct ?? false) return;

            var deduct = new ItemProductDeductionDetail();
            deduct.Query.Where(string.Format("<{0} BETWEEN MinAmount AND MaxAmount>", x));
            if (deduct.Query.Load())
            {
                txtTakenQty.Value = Convert.ToDouble(decimal.Truncate(Convert.ToDecimal(txtTakenQty.Value)) + deduct.DeductionAmount);
                txtResultQty.Value = Convert.ToDouble(decimal.Truncate(Convert.ToDecimal(txtResultQty.Value)) + deduct.DeductionAmount);
            }
        }
    }
}