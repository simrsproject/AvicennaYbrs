using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.AssetManagement
{
    public partial class WorkOrderRealizationImplementerDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        private RadComboBox CboToServiceUnitId
        {
            get
            { return (RadComboBox)Helper.FindControlRecursive(Page, "cboToServiceUnitID"); }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                cboUserID.Enabled = true;

                return;
            }

            ViewState["IsNewRecord"] = false;

            this.UserRequested(cboUserID, (String)DataBinder.Eval(DataItem, "UserID"));
            cboUserID.SelectedValue = (String)DataBinder.Eval(DataItem, AssetWorkOrderImplementerMetadata.ColumnNames.UserID);
            txtNotes.Text = (String)DataBinder.Eval(DataItem, AssetWorkOrderImplementerMetadata.ColumnNames.Notes);
        }

        protected void cboUserID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            this.UserRequested(cboUserID, e.Text);
        }

        protected void cboUserID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["UserName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["UserID"].ToString();
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (AssetWorkOrderImplementerCollection)Session["collAssetWorkOrderImplementer" + Request.UserHostName];
                var isExist =
                    coll.Any(
                        entity =>
                        entity.UserID.Equals(cboUserID.SelectedValue));
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Implemented By: {0} has exist", cboUserID.Text);
                }
            }
        }

        public String UserID
        {
            get { return cboUserID.SelectedValue; }
        }

        public String UserName
        {
            get { return cboUserID.Text; }
        }

        public String Notes
        {
            get { return txtNotes.Text; }
        }

        protected void btnCancel_ButtonClick(object sender, EventArgs e)
        {
        }

        private void UserRequested(RadComboBox comboBox, string textSearch)
        {
            if (textSearch == null)
                textSearch = string.Empty;

            string searchTextContain = string.Format("%{0}%", textSearch);
            var query = new AppUserQuery("a");
            var usrUnit = new AppUserServiceUnitQuery("c");
            var unit = new ServiceUnitTransactionCodeQuery("b");
            
            query.InnerJoin(usrUnit).On(query.UserID == usrUnit.UserID);
            query.InnerJoin(unit).On(usrUnit.ServiceUnitID == unit.ServiceUnitID &&
                                     unit.SRTransactionCode == TransactionCode.AssetWorkOrderRealization);

            query.es.Top = 20;
            query.Where
                (query.Or(query.UserName.Like(searchTextContain),
                          query.UserID.Like(searchTextContain)),
                 query.ExpireDate >= DateTime.Now.Date,
                 usrUnit.ServiceUnitID == CboToServiceUnitId.SelectedValue);
            query.OrderBy(query.UserName.Ascending);
            DataTable dtb = query.LoadDataTable();

            comboBox.DataSource = dtb;
            comboBox.DataBind();

            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0]["UserName"].ToString();
                comboBox.SelectedValue = dtb.Rows[0]["UserID"].ToString();
            }
        }
    }
}