using System;
using System.Collections.Generic;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class DiagnosaNeonatalCtl : BaseOptionCtl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var query = new AppStandardReferenceItemQuery();
                query.Where(query.StandardReferenceID == AppEnum.StandardReference.DiagnosaNeonatal);
                query.Select(query.ReferenceID);
                query.OrderBy(query.ReferenceID.Ascending);
                query.es.Distinct = true;

                DataTable dtb = query.LoadDataTable();
                cboDiagnosaNeonatal.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                if (dtb.Rows.Count > 0)
                {
                    foreach (DataRow row in dtb.Rows)
                    {
                        cboDiagnosaNeonatal.Items.Add(new RadComboBoxItem(row["ReferenceID"].ToString(), row["ReferenceID"].ToString()));
                    }
                }
            }
        }

        public override PrintJobParameterCollection PrintJobParameters()
        {
            var parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_DiagnosaID", cboDiagnosaNeonatal.SelectedValue);

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
                return string.Format("Diagnose Name : {0}", cboDiagnosaNeonatal.SelectedValue);
            }
        }

    }
}