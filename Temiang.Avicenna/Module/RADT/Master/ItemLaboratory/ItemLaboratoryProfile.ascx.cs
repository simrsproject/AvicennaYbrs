using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Reference;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ItemLaboratoryProfile : BaseUserControl
    {
        private RadTextBox txtItemID
        {
            get
            {
                return Helper.FindControlRecursive(this.Page, "txtItemID") as RadTextBox;
            }
        }

        private RadioButtonList rblProfileType
        {
            get
            {
                return Helper.FindControlRecursive(this.Page, "rblProfileType") as RadioButtonList;
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            lblCaptionName.Text = rblProfileType.SelectedValue == "0" ? "Detail Item" : "Parent Item";
            RequiredFieldValidator5.ErrorMessage = rblProfileType.SelectedValue == "0" ? "Detail Item required." : "Parent Item required.";
        }

        protected void cboCaptionName_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ItemQuery("a");
            query.es.Top = 10;
            query.Select(query.ItemID, query.ItemName);
            query.Where(
                    query.SRItemType == ItemType.Laboratory,
                    query.Or(
                            query.ItemName.Like(searchTextContain),
                            query.ItemID.Like(searchTextContain)
                        ),
                    query.IsActive == true,
                    query.ItemID != txtItemID.Text
                );

            /*hanya item lab yg tidak ada parent yg muncul*/
            //var profile = new ItemLaboratoryProfileQuery("b");
            //query.LeftJoin(profile).On(query.ItemID == profile.DetailItemID);
            //query.Where(profile.ParentItemID.IsNull());

            cboCaptionName.DataSource = query.LoadDataTable();
            cboCaptionName.DataBind();
        }

        protected void cboCaptionName_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (ItemLaboratoryProfileCollection)Session["collItemLaboratoryProfile"];

                string itemID = cboCaptionName.SelectedValue;
                bool isExist = false;
                foreach (var item in coll)
                {
                    if (rblProfileType.SelectedValue == "0")
                    {
                        if (item.ParentItemID == txtItemID.Text && item.DetailItemID == itemID)
                        {
                            isExist = true;
                            break;
                        }
                    }
                    else
                    {
                        if (item.ParentItemID == itemID && item.DetailItemID == txtItemID.Text)
                        {
                            isExist = true;
                            break;
                        }
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Profile already exist.", itemID);
                }
            }
        }

        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                return;
            }
            ViewState["IsNewRecord"] = false;

            var profileId = rblProfileType.SelectedValue == "0" ? (String)DataBinder.Eval(DataItem, ItemLaboratoryProfileMetadata.ColumnNames.DetailItemID) :
                (String)DataBinder.Eval(DataItem, ItemLaboratoryProfileMetadata.ColumnNames.ParentItemID);

            var query = new ItemQuery("a");
            //var profile = new ItemLaboratoryProfileQuery("b");
            //query.LeftJoin(profile).On(query.ItemID == profile.DetailItemID);
            query.es.Top = 10;
            query.Select(query.ItemID, query.ItemName);
            query.Where(query.ItemID == profileId);
            cboCaptionName.DataSource = query.LoadDataTable();
            cboCaptionName.DataBind();

            cboCaptionName.SelectedValue = profileId;
        }

        public string ProfileItemID
        {
            get { return cboCaptionName.SelectedValue; }
        }

        public string ProfileItemName
        {
            get { return cboCaptionName.Text; }
        }
    }
}