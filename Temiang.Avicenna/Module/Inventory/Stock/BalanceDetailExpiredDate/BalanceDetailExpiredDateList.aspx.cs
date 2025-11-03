using System;
using System.Collections.Generic;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;
using System.Data;
using Telerik.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Dal.DynamicQuery;


namespace Temiang.Avicenna.Module.Inventory.Stock
{
    public partial class BalanceDetailExpiredDateList : BasePageDialog
    {
        private bool IsReadOnly
        {
            get
            {
                return string.IsNullOrEmpty(Request.QueryString["t"]) ? false : Request.QueryString["t"] == "1";
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtLocationID.Text = Request.QueryString["lid"];
                txtItemID.Text = Request.QueryString["iid"];

                var loc = new Location();
                if (loc.LoadByPrimaryKey(txtLocationID.Text))
                    txtLocationName.Text = loc.LocationName;
                else
                    txtLocationName.Text = string.Empty;

                var item = new Item();
                if (item.LoadByPrimaryKey(txtItemID.Text))
                    txtItemName.Text = item.ItemName;
                else
                    txtItemName.Text = string.Empty;
            }

            if (IsReadOnly)
            {
                grdEd.Columns[0].Visible = false;
                grdEd.Columns[grdEd.Columns.Count - 1].Visible = false;

                grdEd.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
            }
        }

        public override bool OnButtonOkClicked()
        {
            if (!Page.IsValid)
                return false;

            ItemBalanceDetailEds.Save();
            return true;
        }

        #region Record Detail Method Function - ED
        private ItemBalanceDetailEdCollection ItemBalanceDetailEds
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collItemBalanceDetailEd" + txtLocationID.Text + txtItemID.Text + Request.UserHostName];
                    if (obj != null)
                        return ((ItemBalanceDetailEdCollection)(obj));
                }

                var coll = new ItemBalanceDetailEdCollection();
                var query = new ItemBalanceDetailEdQuery("a");

                query.Select
                    (
                        query
                    );

                query.Where(query.LocationID == txtLocationID.Text, query.ItemID == txtItemID.Text);

                query.OrderBy(query.ExpiredDate.Ascending, query.CreatedDateTime.Ascending);

                coll.Load(query);

                Session["collItemBalanceDetailEd" + txtLocationID.Text + txtItemID.Text + Request.UserHostName] = coll;
                return coll;
            }
            set
            {
                Session["collItemBalanceDetailEd" + txtLocationID.Text + txtItemID.Text + Request.UserHostName] = value;
            }
        }

        protected void grdEd_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdEd.DataSource = ItemBalanceDetailEds;
        }

        protected void grdEd_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;
            string bn = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][ItemBalanceDetailEdMetadata.ColumnNames.BatchNumber]);
            DateTime ed = Convert.ToDateTime(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][ItemBalanceDetailEdMetadata.ColumnNames.ExpiredDate]);

            ItemBalanceDetailEd entity = FindEdGrid(bn, ed);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdEd_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;
            string bn = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][ItemBalanceDetailEdMetadata.ColumnNames.BatchNumber]);
            DateTime ed = Convert.ToDateTime(item.OwnerTableView.DataKeyValues[item.ItemIndex][ItemBalanceDetailEdMetadata.ColumnNames.ExpiredDate]);

            ItemBalanceDetailEd entity = FindEdGrid(bn, ed);
            if (entity != null && entity.Balance == 0 && entity.IsActive == false)
                entity.MarkAsDeleted();
        }

        protected void grdEd_InsertCommand(object source, GridCommandEventArgs e)
        {
            ItemBalanceDetailEd entity = ItemBalanceDetailEds.AddNew();
            SetEntityValue(entity, e);

            e.Canceled = true;
            grdEd.Rebind();
        }

        private void SetEntityValue(ItemBalanceDetailEd entity, GridCommandEventArgs e)
        {
            var userControl = (BalanceDetailExpiredDateItem)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.LocationID = txtLocationID.Text;
                entity.ItemID = txtItemID.Text;
                entity.ExpiredDate = userControl.ExpiredDate;
                entity.BatchNumber = userControl.BatchNumber;
                if (userControl.IsNewRecord)
                {
                    entity.Balance = 0;
                    entity.CreatedByUserID = AppSession.UserLogin.UserID;
                    entity.CreatedDateTime = (new DateTime()).NowAtSqlServer();
                }
                entity.IsActive = userControl.IsActive;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }
        }

        private ItemBalanceDetailEd FindEdGrid(string bn, DateTime ed)
        {
            ItemBalanceDetailEdCollection coll = ItemBalanceDetailEds;
            ItemBalanceDetailEd retval = null;
            foreach (ItemBalanceDetailEd rec in coll)
            {
                if (rec.BatchNumber.Equals(bn) && rec.ExpiredDate.Equals(ed))
                {
                    retval = rec;
                    break;
                }
            }
            return retval;
        }
        #endregion
    }
}