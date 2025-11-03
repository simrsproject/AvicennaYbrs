using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
namespace Temiang.Avicenna.Module.Finance.Tariff
{
    public partial class ItemTariffRequest2ItemDetail : BaseUserControl
    {
        private object _dataItem;
        private List<TariffRequest2Component> _listTariffRequest2Comp = new List<TariffRequest2Component>();


        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        private RadComboBox cboSRItemType
        {
            get
            { return (RadComboBox)Helper.FindControlRecursive(Page, "cboSRItemType"); }
        }

        private RadComboBox cboItemGroup
        {
            get
            { return (RadComboBox)Helper.FindControlRecursive(Page, "cboItemGroup"); }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            cboSRItemType.Enabled = false;
            cboItemGroup.Enabled = false;

            trCito.Visible = false;
            trCitoFromStdRef.Visible = false;

            if (DataItem is GridInsertionObject)
            {
                var coll = new ClassCollection();
                coll.Query.Where(coll.Query.IsActive == true, coll.Query.IsTariffClass == true);
                coll.LoadAll();

                cboClassID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (Class c in coll)
                {
                    cboClassID.Items.Add(new RadComboBoxItem(c.ClassName, c.ClassID));
                }

                //Tariff Component
                PopulateTariffComponent(string.Empty, string.Empty);
                ViewState["IsNewRecord"] = true;
                cboClassID.Enabled = true;
                cboItemID.Enabled = true;
                txtCitoValue.Value = 0;

                chkIsItemAllowDiscount.Checked = false;
                chkIsItemAllowVariable.Checked = false;

                return;
            }
            ViewState["IsNewRecord"] = false;
            cboClassID.Enabled = false;
            cboItemID.Enabled = false;

            var clQ = new ClassQuery();
            clQ.Select(clQ.ClassID, clQ.ClassName);
            clQ.Where(clQ.ClassID == (String)DataBinder.Eval(DataItem, ItemTariffRequest2ItemMetadata.ColumnNames.ClassID));
            //cboClassID.DataSource = clQ.LoadDataTable();
            //cboClassID.DataBind();
            var colll = new ClassCollection();
            colll.Load(clQ);
            foreach (Class c in colll)
            {
                cboClassID.Items.Add(new RadComboBoxItem(c.ClassName, c.ClassID));
            }

            var query = new ItemQuery();
            query.Select(query.ItemID,query.ItemName);
            query.Where(query.ItemID == (String)DataBinder.Eval(DataItem, ItemTariffRequest2ItemMetadata.ColumnNames.ItemID));
            cboItemID.DataSource = query.LoadDataTable();
            cboItemID.DataBind();

            cboClassID.SelectedValue = (String)DataBinder.Eval(DataItem, ItemTariffRequest2ItemMetadata.ColumnNames.ClassID);
            cboItemID.SelectedValue = (String)DataBinder.Eval(DataItem, ItemTariffRequest2ItemMetadata.ColumnNames.ItemID);
            txtCitoValue.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ItemTariffRequest2ItemMetadata.ColumnNames.CitoValue));
            chkIsCitoInPercent.Checked = (Boolean)DataBinder.Eval(DataItem, ItemTariffRequest2ItemMetadata.ColumnNames.IsCitoInPercent);
            chkIsCitoFromStandardReference.Checked = (Boolean)DataBinder.Eval(DataItem, ItemTariffRequest2ItemMetadata.ColumnNames.IsCitoFromStandardReference);

            VisibleCito(cboItemID.SelectedValue, false, chkIsCitoFromStandardReference.Checked);
            //Tariff Component
            PopulateTariffComponent(cboClassID.SelectedValue, cboItemID.SelectedValue);
        }

        private void VisibleCito(string itemId, bool isNew, bool isCitoFromStdref)
        {
            bool visible = true, citoFromStdRef = false;
            bool isAllowDiscount = false, isAllowVariable = false;
            var i = new Item();
            i.LoadByPrimaryKey(itemId);
            switch (i.SRItemType)
            {
                case BusinessObject.Reference.ItemType.Service:
                    var s = new ItemService();
                    s.LoadByPrimaryKey(i.ItemID);
                    visible = s.IsAllowCito ?? false;
                    citoFromStdRef = s.IsCitoFromStandardReference ?? false;
                    isAllowDiscount = s.IsAllowDiscount ?? false;
                    isAllowVariable = s.IsAllowVariable ?? false;
                    break;
                case BusinessObject.Reference.ItemType.Diagnostic:
                    var d = new ItemDiagnostic();
                    d.LoadByPrimaryKey(i.ItemID);
                    visible = d.IsAllowCito ?? false;
                    isAllowDiscount = d.IsAllowDiscount ?? false;
                    isAllowVariable = d.IsAllowVariable ?? false;
                    break;
                case BusinessObject.Reference.ItemType.Laboratory:
                    var l = new ItemLaboratory();
                    l.LoadByPrimaryKey(i.ItemID);
                    visible = l.IsAllowCito ?? false;
                    citoFromStdRef = l.IsCitoFromStandardReference ?? false;
                    isAllowDiscount = l.IsAllowDiscount ?? false;
                    isAllowVariable = l.IsAllowVariable ?? false;
                    break;
                case BusinessObject.Reference.ItemType.Package:
                    visible = false;
                    break;
                case BusinessObject.Reference.ItemType.Radiology:
                    var r = new ItemRadiology();
                    r.LoadByPrimaryKey(i.ItemID);
                    visible = r.IsAllowCito ?? false;
                    citoFromStdRef = r.IsCitoFromStandardReference ?? false;
                    isAllowDiscount = r.IsAllowDiscount ?? false;
                    isAllowVariable = r.IsAllowVariable ?? false;
                    break;
            }
            if (visible)
            {
                if (isNew)
                {
                    trCitoFromStdRef.Visible = citoFromStdRef;
                    chkIsCitoFromStandardReference.Checked = citoFromStdRef;
                    trCito.Visible = !citoFromStdRef;
                    if (trCito.Visible)
                    {
                        var ig = new ItemGroup();
                        if (ig.LoadByPrimaryKey(i.ItemGroupID))
                        {
                            txtCitoValue.Value = Convert.ToDouble(ig.CitoValue);
                            chkIsCitoInPercent.Checked = ig.IsCitoInPercent ?? false;
                        }
                        else
                        {
                            txtCitoValue.Value = 0;
                            chkIsCitoInPercent.Checked = false;
                        }
                    }
                    else
                    {
                        txtCitoValue.Value = 0;
                        chkIsCitoInPercent.Checked = false;
                    }
                }
                else
                {
                    trCitoFromStdRef.Visible = isCitoFromStdref;
                    trCito.Visible = !isCitoFromStdref;
                }
            }

            chkIsItemAllowDiscount.Checked = isAllowDiscount;
            chkIsItemAllowVariable.Checked = isAllowVariable;
        }

        private void PopulateTariffComponent(string classID, string itemID)
        {
            //Filter ItemTariffRequestItemComps berdasarkan ItemID
            var comps = new TariffComponentCollection();
            if (cboSRItemType.SelectedValue == BusinessObject.Reference.ItemType.Package)
                comps.Query.Where(comps.Query.TariffComponentID == AppSession.Parameter.TariffComponentJasaSaranaID);

            comps.LoadAll();

            foreach (TariffComponent comp in comps)
            {
                var req = new TariffRequest2Component();
                req.TariffComponentID = comp.TariffComponentID;
                req.TariffComponentName = comp.TariffComponentName;
                ItemTariffRequest2ItemComp requestItemComp = null;

                if (itemID != string.Empty)
                    requestItemComp = GetItemTariffRequestItemComp(classID, itemID, comp.TariffComponentID);

                if (requestItemComp != null)
                {
                    req.Price = requestItemComp.Price ?? 0;
                    req.IsAllowDiscount = requestItemComp.IsAllowDiscount ?? false;
                    req.IsAllowVariable = requestItemComp.IsAllowVariable ?? false;
                }
                else
                {
                    req.Price = 0;
                    req.IsAllowDiscount = false;
                    req.IsAllowVariable = false;
                }

                _listTariffRequest2Comp.Add(req);
            }

            grdItemTariffRequest2ItemComp.DataSource = _listTariffRequest2Comp;
            grdItemTariffRequest2ItemComp.Rebind();
        }


        private ItemTariffRequest2ItemComp GetItemTariffRequestItemComp(string classID, string itemID, string tariffComponentID)
        {
            ItemTariffRequest2ItemCompCollection coll = ItemTariffRequestItemComps;
            foreach (ItemTariffRequest2ItemComp comp in coll)
            {
                if (comp.TariffComponentID.Equals(tariffComponentID) && comp.ItemID.Equals(itemID) && comp.ClassID.Equals(classID))
                    return comp;
            }
            return null;
        }

        private ItemTariffRequest2ItemCompCollection ItemTariffRequestItemComps
        {
            get
            {
                ItemTariffRequest2ItemCompCollection coll;

                if (ViewState["IsNewRecord"].Equals(false))
                    coll = ((ItemTariffRequest2ItemCompCollection)Session["ItemTariffRequest2ItemComps" + Request.UserHostName]);
                else
                    coll = new ItemTariffRequest2ItemCompCollection();

                if (coll.Count == 0)
                {
                    string tariffType = ((RadComboBox)Helper.FindControlRecursive(Page, "cboSRTariffType")).SelectedValue;

                    var comp = Helper.Tariff.GetItemTariffComponentCollection(DateTime.Now.Date, tariffType, ClassID, ItemID);
                    foreach (var c in comp)
                    {
                        var entity = coll.AddNew();
                        entity.TariffRequestNo = string.Empty;
                        entity.ClassID = ClassID;
                        entity.ItemID = ItemID;
                        entity.TariffComponentID = c.TariffComponentID;

                        var co = new TariffComponent();
                        co.LoadByPrimaryKey(entity.TariffComponentID);
                        entity.TariffComponentName = co.TariffComponentName;

                        entity.Price = c.Price;
                        entity.IsAllowDiscount = c.IsAllowDiscount;
                        entity.IsAllowVariable = c.IsAllowVariable;
                        entity.LastUpdateDateTime = DateTime.Now;
                        entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    }
                }
                return coll;
            }
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (ItemTariffRequest2ItemCollection)Session["collItemTariffRequest2Item" + Request.UserHostName];

                bool isExist = false;
                foreach (ItemTariffRequest2Item item in coll)
                {
                    if (item.ItemID.Equals(cboItemID.SelectedValue) & item.ClassID.Equals(cboClassID.SelectedValue))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Item ID: {0} for class: {1} has exist", cboItemID.SelectedValue, cboClassID.Text);
                }
            }
        }

        #region Properties for return entry value

        public String ClassID
        {
            get { return cboClassID.SelectedValue; }
        }

        public String ClassName
        {
            get { return cboClassID.Text; }
        }

        public String ItemID
        {
            get { return cboItemID.SelectedValue; }
        }

        public String ItemName
        {
            get { return cboItemID.Text; }
        }

        public Decimal CitoValue
        {
            get { return Convert.ToDecimal(txtCitoValue.Value); }
        }

        public Boolean IsCitoInPercent
        {
            get { return chkIsCitoInPercent.Checked; }
        }

        public Boolean IsCitoFromStandardReference
        {
            get { return chkIsCitoFromStandardReference.Checked; }
        }

        public GridDataItemCollection GridDataItemCollection
        {
            get { return grdItemTariffRequest2ItemComp.MasterTableView.Items; }
        }

        #endregion

        #region Grid Event

        protected void grdItemTariffRequest2ItemComp_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
                e.Item.PreRender += new EventHandler(grdItemTariffRequest2ItemComp_ItemPreRender);
        }

        private void grdItemTariffRequest2ItemComp_ItemPreRender(object sender, EventArgs e)
        {
            GridDataItem dataItem = sender as GridDataItem;

            if (dataItem == null)
                return;

            RadNumericTextBox txtComponentPrice = (dataItem["Price"].FindControl("txtComponentPrice") as RadNumericTextBox);
            CheckBox chkIsAllowDiscount = (dataItem["IsAllowDiscount"].FindControl("chkIsAllowDiscount") as CheckBox);
            CheckBox chkIsAllowVariable = (dataItem["IsAllowVariable"].FindControl("chkIsAllowVariable") as CheckBox);

            string tariffComponentID = dataItem.GetDataKeyValue("TariffComponentID").ToString();
            TariffRequest2Component reqComp = GetTariffRequestComponent(tariffComponentID);

            if (txtComponentPrice != null)
                txtComponentPrice.Value = (double)reqComp.Price;

            if (chkIsAllowDiscount != null)
            {
                if (chkIsItemAllowDiscount.Checked)
                    chkIsAllowDiscount.Checked = reqComp.IsAllowDiscount;
                else
                    chkIsAllowDiscount.Checked = false;
                chkIsAllowDiscount.Enabled = chkIsItemAllowDiscount.Checked;
            }

            if (chkIsAllowVariable != null)
            {
                if (chkIsItemAllowVariable.Checked)
                    chkIsAllowVariable.Checked = reqComp.IsAllowVariable;
                else
                    chkIsAllowVariable.Checked = false;
                chkIsAllowVariable.Enabled = chkIsItemAllowVariable.Checked;
            }
        }

        private TariffRequest2Component GetTariffRequestComponent(string tariffComponentID)
        {
            foreach (TariffRequest2Component item in _listTariffRequest2Comp)
            {
                if (item.TariffComponentID.Equals(tariffComponentID))
                    return item;
            }
            return new TariffRequest2Component();
        }

        #endregion

        protected void cboItemID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ItemQuery();
            query.es.Top = 20;
            query.Select
                (
                    query.ItemID,
                    query.ItemName
                );
            query.Where
                (
                    query.Or
                        (
                            query.ItemID.Like(searchTextContain),
                            query.ItemName.Like(searchTextContain)
                        ),
                    query.SRItemType == ((RadComboBox)Helper.FindControlRecursive(Page, "cboSRItemType")).SelectedValue,
                    query.IsActive == true
                );

            if (!string.IsNullOrEmpty(cboItemGroup.SelectedValue))
                query.Where(query.ItemGroupID == cboItemGroup.SelectedValue);

            query.OrderBy(query.ItemName.Ascending);

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
            _listTariffRequest2Comp.Clear();
            VisibleCito(e.Value, true, false);
            PopulateTariffComponent(cboClassID.SelectedValue, e.Value);
        }

        protected void cboClassID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            _listTariffRequest2Comp.Clear();
            PopulateTariffComponent(e.Value, cboItemID.SelectedValue);
            //VisibleCito(cboItemID.SelectedValue, true, false);
        }
    }
    public class TariffRequest2Component
    {
        private string _tariffComponentID;
        private string _tariffComponentName;
        private decimal _price;
        private bool _isAllowDiscount;
        private bool _isAllowVariable;

        public string TariffComponentID
        {
            get { return _tariffComponentID; }
            set { _tariffComponentID = value; }
        }

        public string TariffComponentName
        {
            get { return _tariffComponentName; }
            set { _tariffComponentName = value; }
        }

        public decimal Price
        {
            get { return _price; }
            set { _price = value; }
        }

        public bool IsAllowDiscount
        {
            get { return _isAllowDiscount; }
            set { _isAllowDiscount = value; }
        }

        public bool IsAllowVariable
        {
            get { return _isAllowVariable; }
            set { _isAllowVariable = value; }
        }
    }
}