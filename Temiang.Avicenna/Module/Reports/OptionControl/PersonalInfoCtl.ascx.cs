using System;
using System.Collections.Generic;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class PersonalInfoCtl : BaseOptionCtl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        #region ComboBox ProcedureID
        protected void cboPersonID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new VwEmployeeTableQuery();
            query.es.Top = 20;
            query.Select(
                query.PersonID,
                query.EmployeeNumber,
                query.EmployeeName
                );
            query.Where(
                query.Or(
                    query.EmployeeNumber.Like(searchTextContain),
                    query.EmployeeName.Like(searchTextContain)
                    )
                );

            (sender as RadComboBox).DataSource = query.LoadDataTable();
            (sender as RadComboBox).DataBind();
        }
        protected void cboPersonID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["EmployeeNumber"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["EmployeeName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PersonID"].ToString();
        }

        #endregion
        public override PrintJobParameterCollection PrintJobParameters()
        {
            PrintJobParameterCollection parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_PersonID", cboPersonID.SelectedValue);

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
                return string.Format("Employee : {0}", cboPersonID.Text);
            }
        }
    }
}