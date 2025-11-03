using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class JournalGroupUserItem : BaseUserControl
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
                ViewState["IsNewRecord"] = true;

                var coll = (JournalGroupUserCollection)Session["collJournalGroupUser"];
                ViewState["id"] = coll.Count + 1;

                return;
            }
            ViewState["IsNewRecord"] = false;

            ViewState["id"] = Convert.ToInt32(DataBinder.Eval(DataItem, JournalGroupUserMetadata.ColumnNames.JournalUserID));

            var user = new AppUserQuery();
            user.Where(user.UserID == (String)DataBinder.Eval(DataItem, JournalGroupUserMetadata.ColumnNames.UserID));
            cboUserID.DataSource = user.LoadDataTable();
            cboUserID.DataBind();

            cboUserID.SelectedValue = (String)DataBinder.Eval(DataItem, JournalGroupUserMetadata.ColumnNames.UserID);
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (JournalGroupUserCollection)Session["collJournalGroupUser"];

                bool isExist = false;
                foreach (var item in coll)
                {
                    if (item.UserID.Equals(cboUserID.SelectedValue))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("User Name : {0} already exist", cboUserID.Text);
                }
            }
        }

        public int JournalUserID
        {
            get { return ViewState["id"].ToInt(); }
        }

        public String UserID
        {
            get { return cboUserID.SelectedValue; }
        }

        public String UserName
        {
            get { return cboUserID.Text; }
        }

        protected void cboUserID_ItemDataBound(object sender, Telerik.Web.UI.RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["UserName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["UserID"].ToString();
        }

        protected void cboUserID_ItemsRequested(object o, Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var user = new AppUserQuery();
            user.es.Top = 20;
            user.Where(user.Or(user.UserID.Like(searchTextContain), user.UserName.Like(searchTextContain)));

            cboUserID.DataSource = user.LoadDataTable();
            cboUserID.DataBind();
        }
    }
}