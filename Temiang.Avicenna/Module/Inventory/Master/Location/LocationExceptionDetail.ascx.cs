using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Inventory.Master
{
    public partial class LocationExceptionDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        private string PageId
        {
            get { return ((HiddenField)Helper.FindControlRecursive(Page, "hdnPageId")).Value; }
        }

        private RadTextBox txtLocationID
        {
            get
            {
                return (RadTextBox)Helper.FindControlRecursive(Page, "txtLocationID");
            }
        }

        protected override void OnDataBinding(EventArgs e)
        {

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                return;
            }
            ViewState["IsNewRecord"] = false;

            var loc = new LocationQuery();
            loc.Where(loc.LocationID == (String)DataBinder.Eval(DataItem, "LocationExceptionID"));
            cboLocationExceptionID.DataSource = loc.LoadDataTable();
            cboLocationExceptionID.DataBind();
            cboLocationExceptionID.SelectedValue = (String)DataBinder.Eval(DataItem, "LocationExceptionID");
        }
        #region ComboBox 
        protected void cboLocationExceptionID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new LocationQuery();
            query.Select(query.LocationID, query.LocationName);
            query.Where
                        (
                            query.Or
                            (
                                query.LocationID.Like(searchTextContain),
                                query.LocationName.Like(searchTextContain)
                            )
                        );
            query.Where(query.LocationID != txtLocationID.Text, query.IsActive == 1, query.IsConsignment == false);
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboLocationExceptionID.DataSource = dtb;
            cboLocationExceptionID.DataBind();
        }

        protected void cboLocationExceptionID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["LocationName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["LocationID"].ToString();
        }
        #endregion

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check Entry LocationID
            var qr = new LocationQuery();
            qr.es.Top = 1;
            qr.Where(qr.LocationName == cboLocationExceptionID.Text);
            var loc = new Location();
            if (!loc.Load(qr))
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = "Selected location not valid, please select exist location";
                return;
            }

            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                LocationExceptionCollection coll =
                    (LocationExceptionCollection)Session["collLocationException" + PageId];

                string id = cboLocationExceptionID.SelectedValue;
                bool isExist = false;
                foreach (LocationException row in coll)
                {
                    if (row.LocationExceptionID.Equals(id))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Location: {0} has exist", cboLocationExceptionID.Text);
                }
            }
        }

        #region Properties for return entry value
        public String LocationExceptionID
        {
            get { return cboLocationExceptionID.SelectedValue; }
        }
        public String LocationExceptionName
        {
            get { return cboLocationExceptionID.Text; }
        }
        #endregion
    }
}