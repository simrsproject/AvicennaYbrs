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

namespace Temiang.Avicenna.Module.Inventory.Warehouse
{
    public partial class ItemExpiryDateDetail : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["read"]))
            {
                var btkOk = (Button)Helper.FindControlRecursive(Master, "btnOk");
                btkOk.Visible = false;
                grdEd.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
                grdEd.Columns[0].Visible = false;
                grdEd.Columns[grdEd.Columns.Count - 2].Visible = false;
            }

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRItemUnit, AppEnum.StandardReference.ItemUnit);
                StandardReference.InitializeIncludeSpace(cboSRItemType, AppEnum.StandardReference.ItemType);

                txtTransactionNo.Text = Request.QueryString["trn"];
                txtSequenceNo.Text = Request.QueryString["sqn"];

                var iti = new ItemTransactionItem();
                if (iti.LoadByPrimaryKey(txtTransactionNo.Text, txtSequenceNo.Text))
                {
                    txtItemID.Text = iti.ItemID;
                    var item = new Item();
                    if (item.LoadByPrimaryKey(txtItemID.Text))
                        txtItemName.Text = item.ItemName;

                    txtQuantity.Value = Convert.ToDouble(iti.Quantity);
                    cboSRItemUnit.SelectedValue = iti.SRItemUnit;
                    txtConversionFactor.Value = Convert.ToDouble(iti.ConversionFactor);
                    cboSRItemType.SelectedValue = Request.QueryString["itype"];
                }
            }
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            // Untuk data refresh item id bersangkutan
            var script = @" //create the argument that will be returned to the parent page
                    var oArg = new Object();
                    oArg.callbackMethod = 'submitEd';
                    oArg.eventArgument = 'rebindEd|" + Request.QueryString["sqn"] + @"';
                    oArg.eventTarget = '" + Request.QueryString["cet"] + @"';

                    //Close the RadWindow            
                    oWnd.close(oArg);";

            return script;

            //return "oWnd.argument = 'rebind:'";
        }

        public override bool OnButtonOkClicked()
        {
            if (!Page.IsValid)
                return false;

            ItemTransactionItemEds.Save();
            return true;
        }

        #region Record Detail Method Function - ED
        private ItemTransactionItemEdCollection ItemTransactionItemEds
        {
            get
            {
                if (IsPostBack)
                {
                    object obj =
                        Session[
                            "collItemTransactionItemEd" + txtTransactionNo.Text + txtSequenceNo.Text +
                            Request.UserHostName];
                    if (obj != null)
                        return ((ItemTransactionItemEdCollection)(obj));
                }

                var coll = new ItemTransactionItemEdCollection();
                var query = new ItemTransactionItemEdQuery("a");

                query.Select
                    (
                        query
                    );

                query.Where(query.TransactionNo == txtTransactionNo.Text, query.SequenceNo == txtSequenceNo.Text);

                query.OrderBy(query.ExpiredDate.Ascending);

                coll.Load(query);

                Session["collItemTransactionItemEd" + txtTransactionNo.Text + txtSequenceNo.Text + Request.UserHostName]
                    = coll;
                return coll;
            }
            set
            {
                Session["collItemTransactionItemEd" + txtTransactionNo.Text + txtSequenceNo.Text + Request.UserHostName]
                    = value;
            }
        }

        private ItemTransactionItemCollection ItemTransactionItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj =
                        Session[
                            "collItemTransactionItem" + txtTransactionNo.Text + txtSequenceNo.Text +
                            Request.UserHostName];
                    if (obj != null)
                        return ((ItemTransactionItemCollection)(obj));
                }

                var coll = new ItemTransactionItemCollection();
                var query = new ItemTransactionItemQuery("a");

                query.Select
                    (
                        query
                    );

                query.Where(query.TransactionNo == txtTransactionNo.Text, query.SequenceNo == txtSequenceNo.Text);

                query.OrderBy(query.ExpiredDate.Ascending);

                coll.Load(query);

                Session["collItemTransactionItem" + txtTransactionNo.Text + txtSequenceNo.Text + Request.UserHostName]
                    = coll;
                return coll;
            }
            set
            {
                Session["collItemTransactionItem" + txtTransactionNo.Text + txtSequenceNo.Text + Request.UserHostName]
                    = value;
            }
        }

        protected void grdEd_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (AppSession.Parameter.IsTxUsingEdDetail)
                grdEd.DataSource = ItemTransactionItemEds;
            else grdEd.DataSource = ItemTransactionItems;
        }

        protected void grdEd_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;
            DateTime ed =
                Convert.ToDateTime(
                    editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][
                        ItemTransactionItemEdMetadata.ColumnNames.ExpiredDate]);


            ItemTransactionItemEd entity = FindEdGrid(ed);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdEd_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;
            DateTime ed =
                Convert.ToDateTime(
                    item.OwnerTableView.DataKeyValues[item.ItemIndex][
                        ItemTransactionItemEdMetadata.ColumnNames.ExpiredDate]);

            ItemTransactionItemEd entity = FindEdGrid(ed);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdEd_InsertCommand(object source, GridCommandEventArgs e)
        {
            ItemTransactionItemEd entity = ItemTransactionItemEds.AddNew();
            SetEntityValue(entity, e);

            e.Canceled = true;
            grdEd.Rebind();
        }

        private void SetEntityValue(ItemTransactionItemEd entity, GridCommandEventArgs e)
        {
            var userControl = (ItemExpiryDateDetailItem)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.TransactionNo = txtTransactionNo.Text;
                entity.SequenceNo = txtSequenceNo.Text;
                entity.ExpiredDate = userControl.ExpiredDate;
                entity.BatchNumber = userControl.BatchNumber;
                entity.ItemID = userControl.ItemId;
                entity.Quantity = userControl.Quantity;
                entity.SRItemUnit = userControl.SrItemUnit;
                entity.ConversionFactor = userControl.ConversionFactor;
                entity.QuantityFinishInBaseUnit = 0;
                entity.IsClosed = false;
                entity.ReferenceNo = userControl.ReferenceNo;
                entity.ReferenceSequenceNo = userControl.ReferenceSequenceNo;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();// DateTime.Now;
            }
        }

        private ItemTransactionItemEd FindEdGrid(DateTime ed)
        {
            ItemTransactionItemEdCollection coll = ItemTransactionItemEds;
            ItemTransactionItemEd retval = null;
            foreach (ItemTransactionItemEd rec in coll)
            {
                if (rec.ExpiredDate.Equals(ed))
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
