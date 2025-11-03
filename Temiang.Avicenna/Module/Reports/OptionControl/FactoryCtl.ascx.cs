using System;
using System.Data;
using System.Collections.Generic;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.Common;


namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class FactoryCtl : BaseOptionCtl
    {
        #region ComboBox 
        protected void cboFabricID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            FabricQuery query = new FabricQuery();
            query.es.Top = 10;
            query.Select
                (
                    query.FabricID,
                    query.FabricName
                );
            query.Where
                (
                    query.Or
                        (
                            query.FabricID.Like(searchTextContain),
                            query.FabricName.Like(searchTextContain)
                        ),
                        query.IsActive == true
                );

            cboFabricID.DataSource = query.LoadDataTable();
            cboFabricID.DataBind();
        }

        protected void cboFabricID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["FabricName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["FabricID"].ToString();
        }
        #endregion

        public override PrintJobParameterCollection PrintJobParameters()
        {
            PrintJobParameterCollection parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_FabricID", cboFabricID.SelectedValue);

            //Retun List
            return parameters;
        }

        public override string ParameterCaption
        {
            get { return lblCaption.Text; }
            set { lblCaption.Text = value; }
        }
        public override string ReportSubTitle
        {
            get
            {
                return string.Format("Factory : {0}", cboFabricID.Text);
            }
        }
    }
}