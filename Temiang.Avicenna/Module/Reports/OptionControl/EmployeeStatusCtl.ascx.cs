using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class EmployeeStatusCtl : BaseOptionCtl
    {
        #region ComboBox
        protected void cboEmployeeStatus_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void cboEmployeeStatus_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new AppStandardReferenceItemQuery("a");

            query.es.Top = 20;
            query.es.Distinct = true;
            query.Select(query.ItemID, query.ItemName);
            query.Where(query.StandardReferenceID == AppEnum.StandardReference.EmployeeStatus, query.ItemName.Like(searchTextContain), query.IsActive == true);
            query.OrderBy(query.ItemName.Ascending);
            DataTable dtb = query.LoadDataTable();
            cboEmployeeStatus.DataSource = dtb;
            cboEmployeeStatus.DataBind();
        }
        #endregion

        public override PrintJobParameterCollection PrintJobParameters()
        {
            var parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_EmployeeStatus", cboEmployeeStatus.SelectedValue);

            //Retun List
            return parameters;
        }

    }
}