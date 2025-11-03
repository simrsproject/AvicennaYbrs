using System;
using System.Collections.Generic;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class ItemPathologiAnatomyCtl : BaseOptionCtl
    {
        #region ComboBox
        protected void cboItemPathologiAnatomy_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            var itemGroupPa = AppSession.Parameter.ItemGroupPathologyAnatomyID;
            var query = new ItemQuery("a");
            query.Select(query.ItemName.As("ItemName"));
            if (!string.IsNullOrWhiteSpace(itemGroupPa))
            {
                if (itemGroupPa.Contains(","))
                    query.Where(query.ItemGroupID.In(itemGroupPa.Split(',')));
                else
                    query.Where(query.ItemGroupID == itemGroupPa);
            }
           
            query.OrderBy(query.ItemID.Ascending);
            
            var dtb = query.LoadDataTable();

            cboItemPathologiAnatomy.DataSource = dtb;
            cboItemPathologiAnatomy.DataBind();
        }
        protected void cboItemPathologiAnatomy_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
        }
        #endregion

        public override PrintJobParameterCollection PrintJobParameters()
        {
            var parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_ItemID", cboItemPathologiAnatomy.SelectedValue);

            //Retun List
            return parameters;
        }
    }
}