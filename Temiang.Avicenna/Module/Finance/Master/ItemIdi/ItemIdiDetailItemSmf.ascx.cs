using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class ItemIdiDetailItemSmf : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            LoadSmfToCombo();

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;
                return;
            }
            ViewState["IsNewRecord"] = false;

            string ItemID = (String)DataBinder.Eval(DataItem, ItemIdiItemSmfMetadata.ColumnNames.ItemID);

            cboItemID_ItemsRequested(cboItemID,
                new RadComboBoxItemsRequestedEventArgs() { Text = ItemID });
            if (cboItemID.FindItemByValue(ItemID) != null) {
                cboItemID.SelectedValue = ItemID;
            }

            string SmfID = (String)DataBinder.Eval(DataItem, ItemIdiItemSmfMetadata.ColumnNames.SmfID);

            if (cboSmfID.FindItemByValue(SmfID) != null)
            {
                cboSmfID.SelectedValue = SmfID;
            }
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (ItemIdiItemSmfCollection)Session["collItemIdiItemSmf"];
                if (coll.Where(c => c.ItemID == ItemID && c.SmfID == SmfID).Any())
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Item ID: {0}-{1} with smf {2} has exist", ItemID, ItemName, SmfName);
                }
                else {
                    var collAll = new ItemIdiItemSmfCollection();
                    var idismf = new ItemIdiItemSmfQuery("idismf");
                    var item = new ItemQuery("item");
                    var smf = new SmfQuery("smf");
                    var idi = new ItemIdiQuery("idi");

                    idismf.InnerJoin(item).On(idismf.ItemID == item.ItemID)
                        .InnerJoin(smf).On(idismf.SmfID == smf.SmfID)
                        .InnerJoin(idi).On(idismf.IdiCode == idi.IdiCode)
                        .Select(idismf, item.ItemName.As("refToItem_ItemName"), smf.SmfName.As("refToSmf_SmfName"), idi.IdiName.As("refToItemIdi_IdiName"))
                        .Where(idismf.ItemID == ItemID, idismf.SmfID == SmfID);

                    if (collAll.Load(idismf))
                    {
                        args.IsValid = false;
                        var imfExistings = collAll;
                        string msg = string.Empty;
                        foreach (var imfExisting in imfExistings)
                        {
                            msg += string.Format("Item {0}-{1} with smf {2} has already been mapped to idi {3}-{4}{5}",
                                ItemID, ItemName, SmfName, imfExisting.IdiCode, imfExisting.IdiName, Environment.NewLine);
                        }
                        ((CustomValidator)source).ErrorMessage = msg;
                    }
                }
            }
        }

        #region Properties for return entry value

        public String ItemID
        {
            get { return cboItemID.SelectedValue; }
        }

        public String ItemName
        {
            get { return cboItemID.Text; }
        }

        public string SmfID {
            get { return cboSmfID.SelectedValue; }
        }

        public string SmfName
        {
            get { return cboSmfID.Text; }
        }
        #endregion

        protected void cboItemID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            ItemQuery query = new ItemQuery("i");
            ItemTariffComponentQuery itc = new ItemTariffComponentQuery("itc");
            TariffComponentQuery tc = new TariffComponentQuery("tc");

            query.InnerJoin(itc).On(query.ItemID == itc.ItemID)
                .InnerJoin(tc).On(itc.TariffComponentID == tc.TariffComponentID)
                .Where
                (
                    tc.IsTariffParamedic == true,
                    query.IsActive == true,
                    query.SRItemType.In(ItemType.Service, ItemType.Radiology, ItemType.Laboratory),
                    query.Or
                    (
                        query.ItemName.Like(searchTextContain),
                        query.ItemID.Equal(searchTextContain)
                    )
                ).Select(query.ItemID, query.ItemName);
            query.OrderBy(query.ItemName.Ascending);
            query.es.Top = 50;
            query.es.Distinct = true;

            cboItemID.DataSource = query.LoadDataTable();
            cboItemID.DataBind();
        }
        protected void cboItemID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        public void LoadSmfToCombo() {
            var smfColl = new SmfCollection();
            smfColl.LoadAll();
            cboSmfID.Items.Add(new RadComboBoxItem() { Value = string.Empty, Text = string.Empty });
            foreach (var smf in smfColl) {
                cboSmfID.Items.Add(new RadComboBoxItem() { Value = smf.SmfID, Text = smf.SmfName });
            }
        }
    }
}