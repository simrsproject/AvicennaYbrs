using System;
using System.Collections.Generic;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class IncidentTypeCtl : BaseOptionCtl
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            //StandardReference Initialize
            if (!IsPostBack)
            {
                var coll = new AppStandardReferenceItemCollection();
                coll.Query.Where(coll.Query.StandardReferenceID == AppEnum.StandardReference.IncidentType,
                                 coll.Query.IsActive == true);
                coll.LoadAll();

                cboSRIncidentType.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                
                foreach (var item in coll)
                {
                    cboSRIncidentType.Items.Add(new RadComboBoxItem(item.ItemName, item.ItemID));
                }
            }
        }

        protected void cboSRIncidentType_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboComponentID.Items.Clear();
            cboComponentID.Text = string.Empty;
        }

        protected void cboComponentID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            var query = new IncidentTypeQuery();
            query.Select
                (
                    query.ComponentID,
                    query.ComponentName
                );
            query.Where
                (
                    query.SRIncidentType == cboSRIncidentType.SelectedValue
                );
            query.OrderBy(query.ComponentID.Ascending);

            DataTable dtb = query.LoadDataTable();
            cboComponentID.DataSource = dtb;
            cboComponentID.DataBind();
        }

        protected void cboComponentID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ComponentName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ComponentID"].ToString();
        }

        public override PrintJobParameterCollection PrintJobParameters()
        {
            var parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_SRIncidentType", cboSRIncidentType.SelectedValue);
            parameters.AddNew("p_ComponentID", cboComponentID.SelectedValue);

            //Retun List
            return parameters;
        }
    }
}