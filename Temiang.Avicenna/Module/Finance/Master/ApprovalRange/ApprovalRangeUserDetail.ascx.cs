using System;
using System.Data;
using System.Text;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Web.UI.WebControls;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class ApprovalRangeUserDetail : BaseUserControl
    {
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
                var coll = (ApprovalRangeUserCollection) Session["collApprovalRangeUser"];

                int? apprLevel = 0;
                foreach (ApprovalRangeUser item in coll)
                {
                    if (apprLevel < item.ApprovalLevel)
                        apprLevel = item.ApprovalLevel;
                }

                txtApprovalLevel.Value = apprLevel+1;
                ViewState["IsNewRecord"] = true;
                return;
            }

            ViewState["IsNewRecord"] = false;

            var query = new AppUserQuery("a");
            query.Select
                (
                    query.UserID,
                    query.UserName
                );
            query.Where(query.UserID == (String)DataBinder.Eval(DataItem, ApprovalRangeUserMetadata.ColumnNames.UserID));

            var tbl = query.LoadDataTable();
            cboUserID.DataSource = tbl;
            cboUserID.DataBind();
            ComboBox.SelectedValue(cboUserID, (String)DataBinder.Eval(DataItem, ApprovalRangeUserMetadata.ColumnNames.UserID));
            txtApprovalLevel.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ApprovalRangeUserMetadata.ColumnNames.ApprovalLevel));
        }

        #region cboUserID

        protected void cboUserID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new AppUserQuery("a");

            query.es.Top = 20;
            query.Where(query.Or(query.UserID.Like(searchTextContain),
                                 query.UserName.Like(searchTextContain)));
            query.Select
                (
                    query.UserID,
                    query.UserName
                );
            cboUserID.DataSource = query.LoadDataTable();
            cboUserID.DataBind();
        }

        protected void cboUserID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["UserName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["UserID"].ToString();
        }


        #endregion

        #region Properties for return entry value

        public String UserID
        {
            get { return cboUserID.SelectedValue; }
        }

        public String UserName
        {
            get { return cboUserID.Text; }
        }

        public int ApprovalLevel
        {
            get { return Convert.ToInt16(txtApprovalLevel.Value); }
        }


        #endregion

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            ////Check duplicate key
            //if (ViewState["IsNewRecord"].Equals(true))
            //{
            //    var coll = (ApprovalRangeUserCollection)Session["collApprovalRangeUser"];

            //    string itemID = cboUserID.SelectedValue;
            //    bool isExist = false;
            //    foreach (ApprovalRangeUser item in coll)
            //    {
            //        if (item.UserID.Equals(itemID))
            //        {
            //            isExist = true;
            //            break;
            //        }
            //    }
            //    if (isExist)
            //    {
            //        args.IsValid = false;
            //        ((CustomValidator)source).ErrorMessage = string.Format("Item ID: {0} has exist", itemID);
            //    }
            //}
        }
    }
}