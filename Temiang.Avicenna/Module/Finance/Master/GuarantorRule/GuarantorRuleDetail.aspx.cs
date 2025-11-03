using System;
using System.Linq;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using System.Data;


namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class GuarantorRuleDetail : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.GuarantorItemRule;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRGuarantorRuleType, AppEnum.StandardReference.GuarantorRuleType);
                StandardReference.InitializeIncludeSpace(cboItemType, AppEnum.StandardReference.ItemType);

                var guar = new Guarantor();
                guar.LoadByPrimaryKey(Request.QueryString["guarId"]);
                txtGuarantor.Text = guar.GuarantorName;

                switch (Request.QueryString["ruleId"])
                {
                    case "":
                        txtRule.Text = string.Empty;
                        break;
                    case "1":
                        txtRule.Text = "Prescription";
                        break;
                    case "2":
                        txtRule.Text = "All Transaction";
                        break;
                }

                txtAmountValue.Value = 0D;
                txtOutpatientAmountValue.Value = 0D;
                txtEmergencyAmountValue.Value = 0D;

                GuarantorItemRuleTariffComponents = null;
                var girc = GuarantorItemRuleTariffComponents;

                if (Request.QueryString["type"] == "edit")
                {
                    cboItemID.DataSource = PopulateItem(Request.QueryString["itemId"]);
                    cboItemID.DataBind();
                    cboItemID.SelectedValue = Request.QueryString["itemId"];

                    if (Request.QueryString["ruleId"] == "1")
                    {
                        var gipr = new GuarantorItemPrescriptionRule();
                        gipr.LoadByPrimaryKey(Request.QueryString["guarId"], Request.QueryString["itemId"]);

                        rblInclude.SelectedIndex = gipr.IsInclude == true ? 0 : 1;
                        cboSRGuarantorRuleType.SelectedValue = gipr.SRGuarantorRuleType;
                        txtAmountValue.Value = Convert.ToDouble(gipr.AmountValue);
                        txtOutpatientAmountValue.Value = Convert.ToDouble(gipr.OutpatientAmountValue);
                        txtEmergencyAmountValue.Value = Convert.ToDouble(gipr.EmergencyAmountValue);
                        chkIsValueInPercent.Checked = gipr.IsValueInPercent ?? false;
                        rblToGuarantor.SelectedIndex = gipr.IsToGuarantor == true ? 0 : 1;
                        chkIsByTariffComponent.Checked = false;
                        chkIsByTariffComponent.Enabled = false;
                        tblTariffComponent.Visible = false;
                    }
                    else
                    {
                        var gir = new GuarantorItemRule();
                        gir.LoadByPrimaryKey(Request.QueryString["guarId"], Request.QueryString["itemId"]);

                        rblInclude.SelectedIndex = gir.IsInclude == true ? 0 : 1;
                        cboSRGuarantorRuleType.SelectedValue = gir.SRGuarantorRuleType;
                        txtAmountValue.Value = Convert.ToDouble(gir.AmountValue);
                        txtOutpatientAmountValue.Value = Convert.ToDouble(gir.OutpatientAmountValue);
                        txtEmergencyAmountValue.Value = Convert.ToDouble(gir.EmergencyAmountValue);
                        chkIsValueInPercent.Checked = gir.IsValueInPercent ?? false;
                        rblToGuarantor.SelectedIndex = gir.IsToGuarantor == true ? 0 : 1;
                        chkIsByTariffComponent.Checked = gir.IsByTariffComponent ?? false;
                        chkIsByTariffComponent.Enabled = true;

                        tblTariffComponent.Visible = chkIsByTariffComponent.Checked;
                        if (tblTariffComponent.Visible)
                        {
                            grdTariffComponent.DataSource = GuarantorItemRuleTariffComponent;
                            grdTariffComponent.DataBind();
                        }
                    }
                    tblRuleType.Visible = rblInclude.SelectedIndex != 1;
                    cboItemID.Enabled = false;
                }
                else
                {
                    if (Request.QueryString["ruleId"] == "1")
                    {
                        chkIsByTariffComponent.Checked = false;
                        chkIsByTariffComponent.Enabled = false;
                        tblTariffComponent.Visible = false;
                    }
                    else
                    {
                        chkIsByTariffComponent.Checked = false;
                        chkIsByTariffComponent.Enabled = true;
                        tblTariffComponent.Visible = false;
                    }
                }

                pnlGroup.Visible = Request.QueryString["type"] == "import";
                pnlItem.Visible = Request.QueryString["type"] != "import";

                switch (Request.QueryString["type"])
                {
                    case "add":
                        this.Title = "Add Guarantor Item Rule";
                        break;
                    case "edit":
                        this.Title = "Edit Guarantor Item Rule";
                        break;
                    default:
                        this.Title = "Import Guarantor Item Rule";
                        break;
                }
            }
        }

        public override bool OnButtonOkClicked()
        {
            Validate();

            if (!IsValid)
                return false;

            if (Request.QueryString["type"] == "add")
            {
                if (Request.QueryString["ruleId"] == "1")
                {
                    var entity = new GuarantorItemPrescriptionRule();
                    if (entity.LoadByPrimaryKey(Request.QueryString["guarId"], cboItemID.SelectedValue))
                    {
                        ShowInformationHeader("Item is already exist.");
                        return false;
                    }
                }
                else
                {
                    var entity = new GuarantorItemRule();
                    if (entity.LoadByPrimaryKey(Request.QueryString["guarId"], cboItemID.SelectedValue))
                    {
                        ShowInformationHeader("Item is already exist.");
                        return false;
                    }
                }
            }
            if (rblInclude.SelectedIndex == 0 && string.IsNullOrEmpty(cboSRGuarantorRuleType.SelectedValue))
            {
                ShowInformationHeader("Rule Type Name required.");
                return false;
            }

            if (Request.QueryString["type"] == "edit")
            {
                if (Request.QueryString["ruleId"] == "1")
                {
                    var entity = new GuarantorItemPrescriptionRule();
                    entity.LoadByPrimaryKey(Request.QueryString["guarId"], cboItemID.SelectedValue);

                    entity.IsInclude = rblInclude.SelectedIndex == 0;
                    entity.SRGuarantorRuleType = cboSRGuarantorRuleType.SelectedValue;
                    entity.AmountValue = Convert.ToDecimal(txtAmountValue.Value);
                    entity.OutpatientAmountValue = Convert.ToDecimal(txtOutpatientAmountValue.Value);
                    entity.EmergencyAmountValue = Convert.ToDecimal(txtEmergencyAmountValue.Value);
                    entity.IsValueInPercent = chkIsValueInPercent.Checked;
                    entity.IsToGuarantor = rblToGuarantor.SelectedIndex == 0;

                    using (esTransactionScope trans = new esTransactionScope())
                    {
                        entity.Save();

                        //Commit if success, Rollback if failed
                        trans.Complete();
                    }
                }
                else
                {
                    var entity = new GuarantorItemRule();
                    entity.LoadByPrimaryKey(Request.QueryString["guarId"], cboItemID.SelectedValue);

                    entity.IsInclude = rblInclude.SelectedIndex == 0;
                    entity.SRGuarantorRuleType = cboSRGuarantorRuleType.SelectedValue;
                    entity.AmountValue = Convert.ToDecimal(txtAmountValue.Value);
                    entity.OutpatientAmountValue = Convert.ToDecimal(txtOutpatientAmountValue.Value);
                    entity.EmergencyAmountValue = Convert.ToDecimal(txtEmergencyAmountValue.Value);
                    entity.IsValueInPercent = chkIsValueInPercent.Checked;
                    entity.IsToGuarantor = rblToGuarantor.SelectedIndex == 0;
                    entity.IsByTariffComponent = chkIsByTariffComponent.Checked;

                    if (entity.IsByTariffComponent ?? false)
                    {
                        entity.AmountValue = 0;
                        entity.OutpatientAmountValue = 0;
                        entity.EmergencyAmountValue = 0;

                        foreach (DataRow row in GetGuarantorItemRuleTariffComponent.Rows)
                        {
                            var girtc = GuarantorItemRuleTariffComponents.FindByPrimaryKey(entity.GuarantorID, entity.ItemID, row["TariffComponentID"].ToString());
                            if (girtc == null) girtc = GuarantorItemRuleTariffComponents.AddNew();
                            girtc.GuarantorID = entity.GuarantorID;
                            girtc.ItemID = entity.ItemID;
                            girtc.TariffComponentID = row["TariffComponentID"].ToString();

                            var tc = new TariffComponent();
                            tc.LoadByPrimaryKey(girtc.TariffComponentID);
                            girtc.TariffComponentName = tc.TariffComponentName;

                            girtc.AmountValue = Convert.ToDecimal(row["AmountValue"]);
                            girtc.LastUpdateDateTime = DateTime.Now;
                            girtc.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            girtc.OutpatientAmountValue = Convert.ToDecimal(row["OutpatientAmountValue"]);
                            girtc.EmergencyAmountValue = Convert.ToDecimal(row["EmergencyAmountValue"]);
                        }
                    }
                    else
                    {
                        var coll = GuarantorItemRuleTariffComponents;
                        coll.Filter = "GuarantorID = '" + entity.GuarantorID + "' AND ItemID = '" + entity.ItemID + "'";
                        coll.MarkAllAsDeleted();
                    }

                    using (esTransactionScope trans = new esTransactionScope())
                    {
                        entity.Save();
                        GuarantorItemRuleTariffComponents.Save();

                        //Commit if success, Rollback if failed
                        trans.Complete();
                    }
                }
            }
            else if (Request.QueryString["type"] == "add")
            {
                if (Request.QueryString["ruleId"] == "1")
                {
                    var entity = new GuarantorItemPrescriptionRule
                                     {
                                         GuarantorID = Request.QueryString["guarId"],
                                         ItemID = cboItemID.SelectedValue,
                                         IsInclude = rblInclude.SelectedIndex == 0,
                                         SRGuarantorRuleType = cboSRGuarantorRuleType.SelectedValue,
                                         AmountValue = Convert.ToDecimal(txtAmountValue.Value),
                                         OutpatientAmountValue = Convert.ToDecimal(txtOutpatientAmountValue.Value),
                                         EmergencyAmountValue = Convert.ToDecimal(txtEmergencyAmountValue.Value),
                                         IsValueInPercent = chkIsValueInPercent.Checked,
                                         IsToGuarantor = rblToGuarantor.SelectedIndex == 0
                                     };

                    using (esTransactionScope trans = new esTransactionScope())
                    {
                        entity.Save();

                        //Commit if success, Rollback if failed
                        trans.Complete();
                    }
                }
                else
                {
                    var entity = new GuarantorItemRule
                                     {
                                         GuarantorID = Request.QueryString["guarId"],
                                         ItemID = cboItemID.SelectedValue,
                                         IsInclude = rblInclude.SelectedIndex == 0,
                                         SRGuarantorRuleType = cboSRGuarantorRuleType.SelectedValue,
                                         AmountValue = Convert.ToDecimal(txtAmountValue.Value),
                                         OutpatientAmountValue = Convert.ToDecimal(txtOutpatientAmountValue.Value),
                                         EmergencyAmountValue = Convert.ToDecimal(txtEmergencyAmountValue.Value),
                                         IsValueInPercent = chkIsValueInPercent.Checked,
                                         IsToGuarantor = rblToGuarantor.SelectedIndex == 0,
                                         IsByTariffComponent = chkIsByTariffComponent.Checked
                                     };

                    if (entity.IsByTariffComponent ?? false)
                    {
                        entity.AmountValue = 0;
                        entity.OutpatientAmountValue = 0;
                        entity.EmergencyAmountValue = 0;

                        foreach (DataRow row in GetGuarantorItemRuleTariffComponent.Rows)
                        {
                            var girtc = GuarantorItemRuleTariffComponents.FindByPrimaryKey(entity.GuarantorID, entity.ItemID, row["TariffComponentID"].ToString());
                            if (girtc == null) girtc = GuarantorItemRuleTariffComponents.AddNew();
                            girtc.GuarantorID = entity.GuarantorID;
                            girtc.ItemID = entity.ItemID;
                            girtc.TariffComponentID = row["TariffComponentID"].ToString();

                            var tc = new TariffComponent();
                            tc.LoadByPrimaryKey(girtc.TariffComponentID);
                            girtc.TariffComponentName = tc.TariffComponentName;

                            girtc.AmountValue = Convert.ToDecimal(row["AmountValue"]);
                            girtc.LastUpdateDateTime = DateTime.Now;
                            girtc.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            girtc.OutpatientAmountValue = Convert.ToDecimal(row["OutpatientAmountValue"]);
                            girtc.EmergencyAmountValue = Convert.ToDecimal(row["EmergencyAmountValue"]);
                        }
                    }
                    else
                    {
                        var coll = GuarantorItemRuleTariffComponents;
                        coll.Filter = "GuarantorID = '" + entity.GuarantorID + "' AND ItemID = '" + entity.ItemID + "'";
                        coll.MarkAllAsDeleted();
                    }

                    using (esTransactionScope trans = new esTransactionScope())
                    {
                        entity.Save();
                        GuarantorItemRuleTariffComponents.Save();

                        //Commit if success, Rollback if failed
                        trans.Complete();
                    }
                }
            }
            else if (Request.QueryString["type"] == "import")
            {
                ShowInformationHeader("");

                var coll = new ItemCollection();
                coll.Query.Where(coll.Query.ItemGroupID == cboItemGroup.SelectedValue, coll.Query.IsActive == true);
                coll.LoadAll();

                using (esTransactionScope trans = new esTransactionScope())
                {
                    if (Request.QueryString["ruleId"] == "1")
                    {
                        foreach (var c in coll)
                        {
                            var rule = new GuarantorItemPrescriptionRule();
                            if (!rule.LoadByPrimaryKey(Request.QueryString["guarId"], c.ItemID))
                                rule.AddNew();

                            rule.GuarantorID = Request.QueryString["guarId"];
                            rule.ItemID = c.ItemID;
                            rule.IsInclude = rblInclude.SelectedIndex == 0;
                            rule.SRGuarantorRuleType = cboSRGuarantorRuleType.SelectedValue;
                            rule.AmountValue = Convert.ToDecimal(txtAmountValue.Value);
                            rule.OutpatientAmountValue = Convert.ToDecimal(txtOutpatientAmountValue.Value);
                            rule.EmergencyAmountValue = Convert.ToDecimal(txtEmergencyAmountValue.Value);
                            rule.IsValueInPercent = chkIsValueInPercent.Checked;
                            rule.IsToGuarantor = rblToGuarantor.SelectedIndex == 0;
                            rule.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            rule.LastUpdateDateTime = DateTime.Now;

                            rule.Save();
                        }
                    }
                    else
                    {
                        foreach (var c in coll)
                        {
                            var rule = new GuarantorItemRule();
                            if (!rule.LoadByPrimaryKey(Request.QueryString["guarId"], c.ItemID))
                            {
                                rule.AddNew();
                                rule.IsByTariffComponent = false;
                            }
                            rule.GuarantorID = Request.QueryString["guarId"];
                            rule.ItemID = c.ItemID;
                            rule.IsInclude = rblInclude.SelectedIndex == 0;
                            rule.SRGuarantorRuleType = cboSRGuarantorRuleType.SelectedValue;
                            rule.AmountValue = Convert.ToDecimal(txtAmountValue.Value);
                            rule.OutpatientAmountValue = Convert.ToDecimal(txtOutpatientAmountValue.Value);
                            rule.EmergencyAmountValue = Convert.ToDecimal(txtEmergencyAmountValue.Value);
                            rule.IsValueInPercent = chkIsValueInPercent.Checked;
                            rule.IsToGuarantor = rblToGuarantor.SelectedIndex == 0;
                            rule.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            rule.LastUpdateDateTime = DateTime.Now;

                            rule.Save();
                        }
                    }


                    //Commit if success, Rollback if failed
                    trans.Complete();
                }

                ShowInformationHeader("Import has been completed.");
                return false;
            }

            return true;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = 'rebind'";
        }

        protected void cboItemID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        private DataTable PopulateItem(string parameter)
        {
            string searchTextContain = string.Format("%{0}%", parameter);
            var query = new ItemQuery("a");
            query.es.Top = 30;
            query.Select
                (
                    query.ItemID,
                    (query.ItemName + " [" + query.ItemID + "]").As("ItemName")
                );
            query.Where(
                    query.Or(
                            query.ItemName.Like(searchTextContain),
                            query.ItemID.Like(searchTextContain)
                        ),
                    query.IsActive == true
                );
            query.OrderBy(query.ItemName.Ascending);

            DataTable tbl = query.LoadDataTable();

            return tbl;
        }

        protected void cboItemID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            DataTable tbl = PopulateItem(e.Text);
            cboItemID.DataSource = tbl.Rows.Count == 0 ? PopulateItem(e.Text) : tbl;
            cboItemID.DataBind();
        }

        protected void rblInclude_SelectedIndexChanged(object sender, EventArgs e)
        {
            tblRuleType.Visible = rblInclude.SelectedIndex != 1;

            cboSRGuarantorRuleType.SelectedValue = string.Empty;
            txtAmountValue.Value = 0D;
            txtOutpatientAmountValue.Value = 0D;
            txtEmergencyAmountValue.Value = 0D;
            chkIsValueInPercent.Checked = false;
        }

        protected void cboItemType_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            HideInformationHeader();

            cboItemGroup.Items.Clear();

            var query = new ItemGroupQuery();
            query.Select
                (
                    query.ItemGroupID,
                    query.ItemGroupName
                );
            query.Where(query.IsActive == true, query.SRItemType == e.Value);
            query.OrderBy(query.ItemGroupID.Ascending);

            DataTable dtb = query.LoadDataTable();

            cboItemGroup.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (DataRow row in dtb.Rows)
            {
                cboItemGroup.Items.Add(new RadComboBoxItem(row["ItemGroupName"].ToString(),
                                                                 row["ItemGroupID"].ToString()));
            }
        }

        protected void cboItemGroup_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            HideInformationHeader();
        }

        protected void chkIsByTariffComponent_CheckedChanged(object sender, EventArgs e)
        {
            txtAmountValue.Value = 0;
            txtOutpatientAmountValue.Value = 0;
            txtEmergencyAmountValue.Value = 0;

            tblTariffComponent.Visible = false;//chkIsByTariffComponent.Checked;
            if (tblTariffComponent.Visible)
            {
                grdTariffComponent.DataSource = GuarantorItemRuleTariffComponent;
                grdTariffComponent.DataBind();
            }
        }

        private GuarantorItemRuleTariffComponentCollection GuarantorItemRuleTariffComponents
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collGuarantorItemRuleTariffComponent"];
                    if (obj != null) return ((GuarantorItemRuleTariffComponentCollection)(obj));
                }

                var a = new GuarantorItemRuleTariffComponentQuery("a");
                var b = new TariffComponentQuery("b");

                a.Select(a, b.TariffComponentName.As("refToTariffComponent_TariffComponentName"));
                a.InnerJoin(b).On(a.TariffComponentID == b.TariffComponentID);
                a.Where(a.GuarantorID == Request.QueryString["guarId"]);
                a.OrderBy(b.TariffComponentID.Ascending);

                var coll = new GuarantorItemRuleTariffComponentCollection();
                coll.Load(a);

                Session["collGuarantorItemRuleTariffComponent"] = coll;
                return coll;
            }
            set
            {
                Session["collGuarantorItemRuleTariffComponent"] = value;
            }
        }

        private DataTable GuarantorItemRuleTariffComponent
        {
            get
            {
                var guar = new Guarantor();
                guar.LoadByPrimaryKey(Request.QueryString["guarId"]);

                var tc = new TariffComponentQuery("a");
                tc.Select(
                    tc.TariffComponentID,
                    tc.TariffComponentName,
                    "<0 AS AmountValue>",
                    "<0 AS OutpatientAmountValue>",
                    "<0 AS EmergencyAmountValue>"
                    );
                tc.OrderBy(tc.TariffComponentID.Ascending);
                var table = tc.LoadDataTable();

                var comps = Helper.Tariff.GetItemTariffComponentCollection(DateTime.Now.Date, guar.SRTariffType, AppSession.Parameter.DefaultTariffClass, cboItemID.SelectedValue);
                if (comps == null || comps.Count() == 0)
                {
                    comps = Helper.Tariff.GetItemTariffComponentCollection(DateTime.Now.Date, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, cboItemID.SelectedValue);
                    if (comps == null || comps.Count() == 0)
                    {
                        table.Rows.Clear();
                        return table;
                    }
                }

                foreach (DataRow row in table.Rows)
                {
                    var comp = comps.Where(c => c.TariffComponentID == row["TariffComponentID"].ToString()).SingleOrDefault();
                    if (comp == null) row.Delete();
                }

                table.AcceptChanges();

                if (table.Rows.Count == 0) return table;

                var coll = (Session["collGuarantorItemRuleTariffComponent"] as GuarantorItemRuleTariffComponentCollection).Where(c => c.GuarantorID == guar.GuarantorID && c.ItemID == cboItemID.SelectedValue);
                if (coll != null && coll.Any() && coll.Count() > 0)
                {
                    foreach (var entity in coll)
                    {
                        var row = table.AsEnumerable().Where(t => t.Field<string>("TariffComponentID") == entity.TariffComponentID).SingleOrDefault();
                        if (row != null)
                        {
                            row["AmountValue"] = Convert.ToDouble(entity.AmountValue ?? 0);
                            row["OutpatientAmountValue"] = Convert.ToDouble(entity.OutpatientAmountValue ?? 0);
                            row["EmergencyAmountValue"] = Convert.ToDouble(entity.EmergencyAmountValue ?? 0);
                        }
                    }
                    table.AcceptChanges();
                }

                return table;
            }
        }

        public DataTable GetGuarantorItemRuleTariffComponent
        {
            get
            {
                var table = new DataTable();
                table.Columns.Add(new DataColumn("TariffComponentID", typeof(string)));
                table.Columns.Add(new DataColumn("AmountValue", typeof(decimal)));
                table.Columns.Add(new DataColumn("OutpatientAmountValue", typeof(decimal)));
                table.Columns.Add(new DataColumn("EmergencyAmountValue", typeof(decimal)));

                foreach (GridDataItem dataItem in grdTariffComponent.MasterTableView.Items)
                {
                    var ipr = Convert.ToDecimal((dataItem["TariffComponentID"].FindControl("txtIPR") as RadNumericTextBox).Value);
                    var opr = Convert.ToDecimal((dataItem["TariffComponentID"].FindControl("txtOPR") as RadNumericTextBox).Value);
                    var emr = Convert.ToDecimal((dataItem["TariffComponentID"].FindControl("txtEMR") as RadNumericTextBox).Value);
                    if (ipr == 0 && opr == 0 && emr == 0) continue;
                    var row = table.NewRow();
                    row["TariffComponentID"] = dataItem["TariffComponentID"].Text;
                    row["AmountValue"] = ipr;
                    row["OutpatientAmountValue"] = opr;
                    row["EmergencyAmountValue"] = emr;
                    table.Rows.Add(row);
                }

                return table;
            }
        }
    }
}
