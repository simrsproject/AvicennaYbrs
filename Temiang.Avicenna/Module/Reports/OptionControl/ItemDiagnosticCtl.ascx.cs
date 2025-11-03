using System;
using System.Collections.Generic;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class ItemDiagnosticCtl : BaseOptionCtl
    {
        #region ComboBox
        protected void cboItemDiagnostic_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            var query = new ItemQuery("a");
            query.Select(query.Notes.As("ItemName"));
            query.Where(query.Notes.Length() > 0);
            query.es.Distinct = true;
            query.OrderBy(query.Notes.Ascending);
            
            var dtb = query.LoadDataTable();

            cboItemDiagnostic.DataSource = dtb;
            cboItemDiagnostic.DataBind();
        }
        protected void cboItemDiagnostic_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
        }
        #endregion

        public override PrintJobParameterCollection PrintJobParameters()
        {
            var parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_ItemID", cboItemDiagnostic.SelectedValue);

            //Retun List
            return parameters;
        }
    }
}