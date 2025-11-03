using System;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using System.Data;

namespace Temiang.Avicenna.Module.Inventory.Master
{
    public partial class ReOrderPointDetail : BasePageDialog
    {
        private bool IsAdd
        {
            get
            {
                return (Request.QueryString["type"] == "add");
            }
        }

        private string LocationId
        {
            get
            {
                return Request.QueryString["locId"];
            }
        }

        private string ItemId
        {
            get
            {
                return Request.QueryString["itemId"];
            }
        }

        private string Itype
        {
            get
            {
                return Request.QueryString["itype"];
            }
        }


        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ReOrderPoint;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var loc = new Location();
                loc.LoadByPrimaryKey(LocationId);
                txtLocation.Text = loc.LocationName;

                if (!IsAdd)
                {
                    var item = new Item();
                    item.LoadByPrimaryKey(ItemId);
                    txtItemName.Text = item.ItemName;

                    PopulateUnit(ItemId);
                    PopulateBalance(ItemId);
                    cboItemID.Visible = false;
                    rfvItemID.Visible = false;
                }
                else
                    txtItemName.Visible = false;
            }
        }

        protected override void OnInitComplete(EventArgs e)
        {
            base.OnInitComplete(e);

            if (IsAdd)
            {
                ajaxMgrProxy.AjaxSettings.AddAjaxSetting(cboItemID, txtItemUnit);
                ajaxMgrProxy.AjaxSettings.AddAjaxSetting(cboItemID, txtMinimum);
                ajaxMgrProxy.AjaxSettings.AddAjaxSetting(cboItemID, txtMaximum);
                ajaxMgrProxy.AjaxSettings.AddAjaxSetting(cboItemID, cboSRItemBin);
            }
        }

        protected void cboSRItemBin_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void cboSRItemBin_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulateCboSRItemBin((RadComboBox)sender, e.Text);
        }

        private void PopulateCboSRItemBin(RadComboBox comboBox, string textSearch)
        {
            string searchTextContain = string.Format("%{0}%", textSearch);
            var query = new AppStandardReferenceItemQuery("a");
            var header = new AppStandardReferenceQuery("b");

            query.Select(query.ItemID, query.ItemName);
            query.InnerJoin(header).On(query.StandardReferenceID == header.StandardReferenceID &&
                                       query.StandardReferenceID == "ItemBin");
            query.Where(query.IsActive == true, query.ItemName.Like(searchTextContain),
                        query.ReferenceID == Request.QueryString["locId"]);
            query.OrderBy(query.ItemName.Ascending);

            query.es.Top = 30;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
        }

        public override bool OnButtonOkClicked()
        {
            Validate();

            if (!IsValid)
                return false;

            if (txtMaximum.Value < txtMinimum.Value)
            {
                ShowInformationHeader("Maximum value must be greater then or equal Minimum value.");
                return false;
            }

            var entity = new ItemBalance();
            if (!entity.LoadByPrimaryKey(LocationId, IsAdd ? cboItemID.SelectedValue : ItemId))
            {
                entity.AddNew();
                entity.LocationID = LocationId;
                entity.ItemID = IsAdd ? cboItemID.SelectedValue : ItemId;
                entity.Balance = 0;
                entity.Booking = 0;
            }

            decimal fmin = entity.Minimum ?? 0;
            decimal fmax = entity.Maximum ?? 0;

            entity.Minimum = Convert.ToDecimal(txtMinimum.Value);
            entity.Maximum = Convert.ToDecimal(txtMaximum.Value);
            entity.SRItemBin = cboSRItemBin.SelectedValue;
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = DateTime.Now;

            var history = new SettingRopHistory();
            history.AddNew();
            history.RopHistoryDateTime = DateTime.Now;
            history.LocationID = LocationId;
            history.ItemID = ItemId;
            history.FromMinimum = fmin;
            history.ToMinimum = Convert.ToDecimal(txtMinimum.Value);
            history.FromMaximum = fmax;
            history.ToMaximum = Convert.ToDecimal(txtMaximum.Value);
            history.LastUpdateByUserID = AppSession.UserLogin.UserID;
            history.LastUpdateDateTime = DateTime.Now;

            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                if (IsAdd)
                {
                    var ibd = new ItemBalanceDetail();
                    ibd.AddNew();
                    ibd.LocationID = LocationId;
                    ibd.ItemID = "";
                    ibd.ReferenceNo = string.Empty;
                    ibd.TransactionCode = string.Empty;
                    ibd.BalanceDate = DateTime.Now;
                    ibd.Balance = 0;
                    ibd.Booking = 0;
                    ibd.Price = 0;

                    ibd.Save();
                }
                history.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }

            return true;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = 'rebind'";
        }

        protected void cboItemID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            DataTable tbl = PopulateItem(e.Text);
            cboItemID.DataSource = tbl.Rows.Count == 0 ? PopulateItem(e.Text) : tbl;
            cboItemID.DataBind();
        }

        protected void cboItemID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void cboItemID_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateUnit(cboItemID.SelectedValue);
            PopulateBalance(cboItemID.SelectedValue);
        }

        private DataTable PopulateItem(string parameter)
        {
            string searchTextContain = string.Format("%{0}%", parameter);
            var query = new ItemQuery("a");
            var ib = new ItemBalanceQuery("b");
            query.LeftJoin(ib).On(ib.LocationID == LocationId && ib.ItemID == query.ItemID);
            query.es.Top = 30;
            query.Select
                (
                    query.ItemID,
                    (query.ItemName + " [" + query.ItemID + "]").As("ItemName")
                );
            query.Where(query.SRItemType == Itype,
                    query.Or(
                            query.ItemName.Like(searchTextContain),
                            query.ItemID.Like(searchTextContain)
                        ),
                    query.IsActive == true, 
                    ib.ItemID.IsNull()
                );
            query.OrderBy(query.ItemName.Ascending);

            DataTable tbl = query.LoadDataTable();

            return tbl;
        }

        private void PopulateUnit(string itemId)
        {
            string unit = string.Empty;
            switch (Itype)
            {
                case BusinessObject.Reference.ItemType.Medical:
                    var med = new ItemProductMedic();
                    if (med.LoadByPrimaryKey(itemId))
                        unit = med.SRItemUnit;

                    break;
                case BusinessObject.Reference.ItemType.NonMedical:
                    var nomed = new ItemProductNonMedic();
                    if (nomed.LoadByPrimaryKey(itemId))
                        unit = nomed.SRItemUnit;

                    break;
                case BusinessObject.Reference.ItemType.Kitchen:
                    var kitc = new ItemKitchen();
                    if (kitc.LoadByPrimaryKey(itemId))
                        unit = kitc.SRItemUnit;
                    break;
            }

            var std = new AppStandardReferenceItem();
            txtItemUnit.Text = std.LoadByPrimaryKey("ItemUnit", unit) ? std.ItemName : string.Empty;
        }

        private void PopulateBalance(string itemId)
        {
            var ib = new ItemBalance();
            if (ib.LoadByPrimaryKey(LocationId, itemId))
            {
                txtMinimum.Value = Convert.ToDouble(ib.Minimum);
                txtMaximum.Value = Convert.ToDouble(ib.Maximum);
                if (!string.IsNullOrEmpty(ib.SRItemBin))
                {
                    var query = new AppStandardReferenceItemQuery();
                    query.Select(query.ItemID, query.ItemName);
                    query.Where(query.ItemID == ib.str.SRItemBin, query.StandardReferenceID == "ItemBin",
                                query.ReferenceID == LocationId);
                    DataTable dtbg = query.LoadDataTable();
                    if (dtbg.Rows.Count > 0)
                    {
                        cboSRItemBin.DataSource = dtbg;
                        cboSRItemBin.DataBind();
                        cboSRItemBin.SelectedValue = ib.SRItemBin;
                        cboSRItemBin.Text = dtbg.Rows[0]["ItemName"].ToString();
                    }
                    else
                    {
                        cboSRItemBin.DataSource = null;
                        cboSRItemBin.DataBind();
                        cboSRItemBin.SelectedValue = string.Empty;
                        cboSRItemBin.Text = string.Empty;
                    }
                }
                else
                {
                    cboSRItemBin.DataSource = null;
                    cboSRItemBin.DataBind();
                    cboSRItemBin.SelectedValue = string.Empty;
                    cboSRItemBin.Text = string.Empty;
                }
            }
            else
            {
                txtMinimum.Value = 0;
                txtMaximum.Value = 0;

                cboSRItemBin.DataSource = null;
                cboSRItemBin.DataBind();
                cboSRItemBin.SelectedValue = string.Empty;
                cboSRItemBin.Text = string.Empty;
            }
        }
    }
}
