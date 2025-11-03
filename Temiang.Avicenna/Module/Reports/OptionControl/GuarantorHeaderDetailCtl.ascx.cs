using System;
using System.Collections.Generic;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class GuarantorHeaderDetailCtl : BaseOptionCtl
    {
        #region ComboBox

        protected void cboGuarantorGroupID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new GuarantorQuery("a");
            var querydt = new GuarantorQuery("b");
            query.InnerJoin(querydt).On(query.GuarantorHeaderID == querydt.GuarantorID);
            query.Select(query.GuarantorHeaderID.As("GuarantorID"), querydt.GuarantorName);
            query.es.Top = 20;
            query.es.Distinct = true;
            query.Where
                (
                    querydt.GuarantorName.Like(searchTextContain),
                    query.IsActive == true,
                    query.SRGuarantorType != AppSession.Parameter.GuarantorTypeMemberID
                );
            query.OrderBy(querydt.GuarantorName.Ascending);

            cboGuarantorGroupID.DataSource = query.LoadDataTable();
            cboGuarantorGroupID.DataBind();
        }

        protected void cboGuarantorGroupID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboGuarantorID.Items.Clear();
            cboGuarantorID.Text = string.Empty;
            cboGuarantorID.SelectedValue = string.Empty;
        }

        protected void cboGuarantorID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new GuarantorQuery();
            query.es.Top = 20;
            query.Where
                (
                    query.GuarantorName.Like(searchTextContain),
                    query.SRGuarantorType != AppSession.Parameter.GuarantorTypeMemberID,
                    query.IsActive == true
                );
            query.Where(query.GuarantorHeaderID == cboGuarantorGroupID.SelectedValue);

            query.OrderBy(query.GuarantorName.Ascending);

            cboGuarantorID.DataSource = query.LoadDataTable();
            cboGuarantorID.DataBind();
        }

        protected void cboGuarantorID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["GuarantorName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["GuarantorID"].ToString();
        }

        #endregion

        public override PrintJobParameterCollection PrintJobParameters()
        {
            var parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_GuarantorGroupID", cboGuarantorGroupID.SelectedValue);
            parameters.AddNew("p_GuarantorID", cboGuarantorID.SelectedValue);

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
                return string.Format("Guarantor : {0}", cboGuarantorID.SelectedValue);
            }
        }
    }
}