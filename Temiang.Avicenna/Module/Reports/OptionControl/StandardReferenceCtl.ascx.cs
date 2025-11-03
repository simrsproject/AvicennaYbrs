using System;
using System.Collections.Generic;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class StandardReferenceCtl : BaseOptionCtl
    {
        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    if (!IsPostBack)
        //    {
        //        var query = new AppStandardReferenceItemQuery();
        //        query.Where(query.StandardReferenceID == hdnReferenceID.Value);
        //        query.Select(query.ItemName);
        //        query.OrderBy(query.ItemName.Ascending);
        //        query.es.Distinct = true;
        //        DataTable dtb = query.LoadDataTable();
        //        cboItemID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
        //        if (dtb.Rows.Count > 0)
        //        {
        //            foreach (DataRow row in dtb.Rows)
        //            {
        //                cboItemID.Items.Add(new RadComboBoxItem(row["ItemName"].ToString(), row["ItemName"].ToString()));
        //            }
        //        }
        //    }
        //}
        public override PrintJobParameterCollection PrintJobParameters()
        {
            PrintJobParameterCollection parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_ItemID", cboItemID.SelectedValue);

            //Retun List
            return parameters;
        }

        public override string ParameterCaption
        {
            get { return lblCaption.Text; }
            set { lblCaption.Text = value; }
        }

        public override string ReferenceID
        {
            get { return hdnReferenceID.Value; }
            set { hdnReferenceID.Value = value; }
        }

        public override string ReportSubTitle
        {
            get
            {
                return string.Format("Item : {0}", cboItemID.SelectedValue);
            }
        }

    }
}