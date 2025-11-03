using System;
using System.Data;
using System.Collections.Generic;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class CssdMachineCtl : BaseOptionCtl
    {
        #region ComboBox 
        protected void cboMachineID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new CssdMachineQuery("a");
            query.Where(
                query.Or(query.MachineID == e.Text, query.MachineName.Like(searchTextContain)), 
                query.IsActive == true
                );
            query.Select(query.MachineID, query.MachineName);

            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboMachineID.DataSource = dtb;
            cboMachineID.DataBind();
        }
        protected void cboMachineID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)[CssdMachineMetadata.ColumnNames.MachineName].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)[CssdMachineMetadata.ColumnNames.MachineID].ToString();
        }
        #endregion

        public override PrintJobParameterCollection PrintJobParameters()
        {
            PrintJobParameterCollection parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_MachineID", cboMachineID.SelectedValue);

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
                return string.Format("Machine : {0}", cboMachineID.Text);
            }
        }
    }
}