using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using System.Web.UI;
using Temiang.Dal.Interfaces;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class GuarantorRuleList : BasePage
    {
        private bool _isHideEmptySearchMessage = false;

        protected void Page_Init(object sender, EventArgs e)
        {
            _isHideEmptySearchMessage = false;
            if (Page.IsPostBack)
            {
                if (Request["__EVENTTARGET"].Contains("grd") &&
                    Request["__EVENTARGUMENT"].Contains("rebind"))
                {
                    _isHideEmptySearchMessage = true;
                }
            }

            ProgramID = AppConstant.Program.GuarantorItemRule;
            if (!IsPostBack)
            {
                cboRuleId.Items.Add(new RadComboBoxItem("", ""));
                cboRuleId.Items.Add(new RadComboBoxItem("Prescription", "1"));
                cboRuleId.Items.Add(new RadComboBoxItem("All Transaction", "2"));

                StandardReference.InitializeIncludeSpace(cboSRItemType, AppEnum.StandardReference.ItemType);

                GuarantorItemRuleTariffComponents = null;
            }
        }

        private bool ValidateSearch(bool isEmptyFilter, string searchingLabel)
        {
            if (!IsListLoadRecordIfFiltered) return true;
            if (!IsPostBack) return false;
            if (!isEmptyFilter) return true;
            if (!_isHideEmptySearchMessage)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "invalid",
                    string.Format("alert('Please entry {0} searching criteria');", searchingLabel), true);
            }
            return false;
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var grd = (RadGrid)source;

            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grd.DataSource = new String[] { };
                return;
            }

            var dataSource = GuarantorItems;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
        }

        protected void grdList_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem dataItem)
            {
                CheckBox chk = (CheckBox)dataItem.FindControl("deleteChkBox");
                string itemID = dataItem.GetDataKeyValue("ItemID").ToString();

                bool isSelectAll = Convert.ToBoolean(Session["IsSelectAllChecked"]);
                List<string> selectedIDs = Session["SelectedItemIDs"] as List<string> ?? new List<string>();
                List<string> unselectedIDs = Session["UnselectedItemIDs"] as List<string> ?? new List<string>();

                if (isSelectAll)
                {
                    chk.Checked = !unselectedIDs.Contains(itemID);
                }
                else
                {
                    chk.Checked = selectedIDs.Contains(itemID);
                }
            }
        }

        protected void deleteChkBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridDataItem item = (GridDataItem)chk.NamingContainer;
            string itemID = item.GetDataKeyValue("ItemID").ToString();

            List<string> unselectedIDs = Session["UnselectedItemIDs"] as List<string> ?? new List<string>();
            List<string> selectedIDs = Session["SelectedItemIDs"] as List<string> ?? new List<string>();

            if (chk.Checked)
            {
                // Kalau select all aktif, dan user centang manual, hapus dari list unselected
                if (Convert.ToBoolean(Session["IsSelectAllChecked"]))
                    unselectedIDs.Remove(itemID);
                else
                {
                    if (!selectedIDs.Contains(itemID))
                        selectedIDs.Add(itemID);
                }
            }
            else
            {
                if (Convert.ToBoolean(Session["IsSelectAllChecked"]))
                {
                    if (!unselectedIDs.Contains(itemID))
                        unselectedIDs.Add(itemID);
                }
                else
                {
                    selectedIDs.Remove(itemID);
                }
            }

            Session["SelectedItemIDs"] = selectedIDs;
            Session["UnselectedItemIDs"] = unselectedIDs;
        }


        protected void grdList_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {            
            grdList.CurrentPageIndex = e.NewPageIndex;
            Session["SelectedItemIDs"] = Session["SelectedItemIDs"]; 
            grdList.Rebind();
        }

        protected void grdList_PageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            grdList.PageSize = e.NewPageSize;
            grdList.Rebind(); 
        }

        private DataTable GuarantorItems
        {
            get
            {
                var isEmptyFilter = string.IsNullOrEmpty(cboGuarantorID.SelectedValue) && string.IsNullOrEmpty(cboRuleId.SelectedValue) && string.IsNullOrEmpty(cboSRItemType.SelectedValue) && string.IsNullOrEmpty(cboItemID.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "Guarantor Item Rule")) return null;

                var dtb = new DataTable();
                switch (cboRuleId.SelectedValue)
                {
                    case "1": //prescription item rule
                        var query = new GuarantorItemPrescriptionRuleQuery("a");
                        var item = new ItemQuery("b");
                        var std = new AppStandardReferenceItemQuery("c");

                        query.es.Top = AppSession.Parameter.MaxResultRecord;

                        query.Select
                            (
                                query.GuarantorID,
                                query.ItemID,
                                item.ItemName,
                                query.SRGuarantorRuleType,
                                std.ItemName.As("GuarantorRuleTypeName"),
                                query.AmountValue,
                                query.OutpatientAmountValue,
                                query.EmergencyAmountValue,
                                query.IsValueInPercent,
                                query.IsInclude,
                                query.IsToGuarantor,
                                @"<CAST(0 AS BIT) AS IsByTariffComponent>"
                            );

                        query.InnerJoin(item).On(query.ItemID == item.ItemID);
                        query.LeftJoin(std).On(query.SRGuarantorRuleType == std.ItemID &&
                                               std.StandardReferenceID == "GuarantorRuleType");
                        query.Where(query.GuarantorID == cboGuarantorID.SelectedValue);
                        if (!string.IsNullOrEmpty(cboSRItemType.SelectedValue))
                            query.Where(item.SRItemType == cboSRItemType.SelectedValue);
                        if (!(string.IsNullOrEmpty(cboItemID.SelectedValue)))
                            query.Where(query.ItemID == cboItemID.SelectedValue);
                        
                        query.OrderBy(item.SRItemType.Ascending, item.ItemGroupID.Ascending, query.ItemID.Ascending);
                        dtb = query.LoadDataTable();

                        break;

                    case "2":
                        var query2 = new GuarantorItemRuleQuery("a");
                        var item2 = new ItemQuery("b");
                        var std2 = new AppStandardReferenceItemQuery("c");

                        query2.es.Top = AppSession.Parameter.MaxResultRecord;

                        query2.Select
                            (
                                query2.GuarantorID,
                                query2.ItemID,
                                item2.ItemName,
                                query2.SRGuarantorRuleType,
                                std2.ItemName.As("GuarantorRuleTypeName"),
                                query2.AmountValue,
                                query2.OutpatientAmountValue,
                                query2.EmergencyAmountValue,
                                query2.IsValueInPercent,
                                query2.IsInclude,
                                query2.IsToGuarantor,
                                query2.IsByTariffComponent
                            );

                        query2.InnerJoin(item2).On(query2.ItemID == item2.ItemID);
                        query2.LeftJoin(std2).On(query2.SRGuarantorRuleType == std2.ItemID &&
                                               std2.StandardReferenceID == "GuarantorRuleType");
                        query2.Where(query2.GuarantorID == cboGuarantorID.SelectedValue);
                        if (!string.IsNullOrEmpty(cboSRItemType.SelectedValue))
                            query2.Where(item2.SRItemType == cboSRItemType.SelectedValue);
                        if (!(string.IsNullOrEmpty(cboItemID.SelectedValue)))
                            query2.Where(query2.ItemID == cboItemID.SelectedValue);

                        query2.OrderBy(item2.SRItemType.Ascending, item2.ItemGroupID.Ascending, query2.ItemID.Ascending);
                        dtb = query2.LoadDataTable();
                        break;
                }
                return dtb;
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
                a.Where(a.GuarantorID == cboGuarantorID.SelectedValue);
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

        protected void grdGuarantorItemRule_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            e.DetailTableView.DataSource = GuarantorItemRuleTariffComponents.Where(g => g.GuarantorID == cboGuarantorID.SelectedValue && g.ItemID == e.DetailTableView.ParentItem.GetDataKeyValue("ItemID").ToString());
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            Session["SelectedItemIDs"] = new List<string>();
            Session["UnselectedItemIDs"] = new List<string>();
            Session["IsSelectAllChecked"] = false;
            grdList.Rebind();
        }

        protected void cboGuarantorID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["GuarantorName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["GuarantorID"].ToString();
        }

        protected void cboGuarantorID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new GuarantorQuery();
            query.es.Top = 30;
            query.Where
                (
                    query.GuarantorName.Like(searchTextContain),
                    query.SRGuarantorType != AppSession.Parameter.GuarantorTypeMemberID,
                    query.IsActive == true
                );
            query.OrderBy(query.GuarantorName.Ascending);

            cboGuarantorID.DataSource = query.LoadDataTable();
            cboGuarantorID.DataBind();
        }

        protected void cboGuarantorID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            grdList.Rebind();
        }

        protected void cboSRItemType_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboItemID.Items.Clear();
            cboItemID.Text = string.Empty;
            grdList.Rebind();
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
            if (!string.IsNullOrEmpty(cboSRItemType.SelectedValue))
                query.Where(query.SRItemType == cboSRItemType.SelectedValue);
            
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["SelectedItemIDs"] = new List<string>();
                Session["UnselectedItemIDs"] = new List<string>();
                Session["IsSelectAllChecked"] = false;
            }

            grdList.ItemDataBound += new GridItemEventHandler(grdList_ItemDataBound);
            grdList.ItemCreated += new GridItemEventHandler(grdList_ItemCreated);
        }

        protected void grdList_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridHeaderItem headerItem)
            {
                CheckBox chkSelectAll = (CheckBox)headerItem.FindControl("chkSelectAll");
                if (chkSelectAll != null)
                {
                    chkSelectAll.Checked = Session["IsSelectAllChecked"] != null && (bool)Session["IsSelectAllChecked"];
                }
            }
        }

        protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkSelectAll = (CheckBox)sender;
            bool isChecked = chkSelectAll.Checked;

            Session["IsSelectAllChecked"] = isChecked;

            // Jika select all dicentang, kosongkan list uncheck manual
            if (isChecked)
            {
                Session["UnselectedItemIDs"] = new List<string>();
            }
            else
            {
                // Kalau uncheck, maka kita reset juga semua
                Session["SelectedItemIDs"] = new List<string>();
                Session["UnselectedItemIDs"] = new List<string>();
            }

            grdList.Rebind();
        }

        private List<string> GetSelectedItemIDs()
        {
            bool isSelectAll = Convert.ToBoolean(Session["IsSelectAllChecked"]);
            var selectedIDs = new List<string>();

            if (isSelectAll)
            {
                var query = new GuarantorItemRuleQuery("a");
                var item = new ItemQuery("b");
                query.InnerJoin(item).On(query.ItemID == item.ItemID);
                query.Select(query.ItemID);
                query.Where(query.GuarantorID == cboGuarantorID.SelectedValue);

                if (!string.IsNullOrEmpty(cboSRItemType.SelectedValue))
                    query.Where(item.SRItemType == cboSRItemType.SelectedValue);

                if (!string.IsNullOrEmpty(cboItemID.SelectedValue))
                    query.Where(query.ItemID == cboItemID.SelectedValue);

                var allIDs = query.LoadDataTable().AsEnumerable()
                                   .Select(r => r["ItemID"].ToString())
                                   .ToList();

                var unselectedIDs = Session["UnselectedItemIDs"] as List<string> ?? new List<string>();

                selectedIDs = allIDs.Except(unselectedIDs).ToList();
            }
            else
            {
                selectedIDs = Session["SelectedItemIDs"] as List<string> ?? new List<string>();
            }

            return selectedIDs;
        }


        protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
        {
            base.RaisePostBackEvent(source, eventArgument);

            if (!(source is RadGrid))
                return;

            if (eventArgument == "rebind")
                grdList.Rebind();

            else if (eventArgument.Contains("|"))
            {
                var param = eventArgument.Split('|');
                List<string> selectedIDs = GetSelectedItemIDs();


                using (var trans = new esTransactionScope())
                {
                    switch (cboRuleId.SelectedValue)
                    {
                        case "1":
                            foreach (var itemId in selectedIDs)
                            {
                                var gipr = new GuarantorItemPrescriptionRule();
                                gipr.LoadByPrimaryKey(cboGuarantorID.SelectedValue, itemId);
                                gipr.MarkAsDeleted();
                                gipr.Save();
                            }                            
                            break;
                        case "2":
                            foreach (var itemId in selectedIDs)
                            {
                                var gir = new GuarantorItemRule();
                                gir.LoadByPrimaryKey(cboGuarantorID.SelectedValue, itemId);
                                gir.MarkAsDeleted();
                                gir.Save();

                                var girtc = new GuarantorItemRuleTariffComponentCollection();
                                girtc.Query.Where(girtc.Query.GuarantorID == cboGuarantorID.SelectedValue,
                                                  girtc.Query.ItemID == itemId);
                                girtc.LoadAll();
                                girtc.MarkAllAsDeleted();
                                girtc.Save();
                            }
                            break;
                    }

                    trans.Complete();
                }
                Session["SelectedItemIDs"] = null;
                Session["UnselectedItemIDs"] = null;
                Session["IsSelectAllChecked"] = null;
                grdList.Rebind();
            }
        }
    }
}
