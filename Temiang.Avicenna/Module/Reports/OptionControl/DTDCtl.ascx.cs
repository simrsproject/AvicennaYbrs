using System;
using System.Collections.Generic;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class DTDCtl : BaseOptionCtl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        #region ComboBox DiagnoseID
        protected void cboDtdNo_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new DtdQuery("a");
            query.Where(query.DtdName.Like(searchTextContain), query.IsActive == true);

            query.es.Top = 20;
            query.Select(query.DtdNo, query.DtdName);
            
            DataTable dtb = query.LoadDataTable();

            cboDtdNo.DataSource = dtb;
            cboDtdNo.DataBind();
        }
        protected void cboDtdNo_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["DtdName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["DtdNo"].ToString();
        }

        #endregion
        public override PrintJobParameterCollection PrintJobParameters()
        {
            var parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_DtdNo", cboDtdNo.SelectedValue);

            //Retun List
            return parameters;
        }
    }
}