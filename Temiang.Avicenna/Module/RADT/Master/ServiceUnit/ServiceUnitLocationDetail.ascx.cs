using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ServiceUnitLocationDetail : BaseUserControl
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
                
                return;
            }
            ViewState["IsNewRecord"] = false;
            ComboBox.PopulateWithOneLocation(cboLocationID, (String)DataBinder.Eval(DataItem, ServiceUnitLocationMetadata.ColumnNames.LocationID));
            chkIsLocationMain.Checked = (bool)DataBinder.Eval(DataItem, ServiceUnitLocationMetadata.ColumnNames.IsLocationMain);
        }

        protected void cboLocationID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["LocationName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["LocationID"].ToString();
        }

        private DataTable LoadLocation(string searchText)
        {
            string searchTextContain = string.Format("%{0}%", searchText);
            var query = new LocationQuery("a");
            query.es.Top = 30;
            query.Select
                (
                    query.LocationID,
                    query.LocationName
                );
            query.Where(
                query.IsActive == true, query.IsConsignment == false,
                query.Or(
                    query.LocationName.Like(searchTextContain),
                    query.LocationID.Like(searchTextContain)
                    )
                );
            query.OrderBy(query.LocationName.Ascending);

            return query.LoadDataTable();
        }

        protected void cboLocationID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            DataTable tbl = LoadLocation(e.Text);
            cboLocationID.DataSource = tbl;
            cboLocationID.DataBind();
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            ServiceUnitLocationCollection coll;

            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                coll = (ServiceUnitLocationCollection)Session["collServiceUnitLocation"];

                string id = cboLocationID.SelectedValue;
                bool isExist = coll.Any(item => item.LocationID.Equals(id));

                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Location ID: {0} has exist", id);
                }
                else if (chkIsLocationMain.Checked)
                {
                    if (coll.Any(item => item.IsLocationMain.Equals(true)))
                    {
                        isExist = true;
                    }
                    if (isExist)
                    {
                        args.IsValid = false;
                        ((CustomValidator)source).ErrorMessage = string.Format("The main location is already defined");
                    }
                }
            }
        }

        #region Properties for return entry value

        public String LocationID
        {
            get { return cboLocationID.SelectedValue; }
        }

        public String LocationName
        {
            get { return cboLocationID.Text; }
        }

        public bool IsLocationMain
        {
            get { return chkIsLocationMain.Checked; }
        }
        #endregion
    }
}