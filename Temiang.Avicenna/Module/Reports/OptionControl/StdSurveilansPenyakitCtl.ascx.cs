using System;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;


namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class StdSurveilansPenyakitCtl : BaseOptionCtl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var query = new AppStandardReferenceItemQuery();
                query.Where(query.StandardReferenceID == "RSCH-SurveilansPenyakit");
                query.Select(query.Note, query.ReferenceID,query.ItemName);
                query.OrderBy(query.ItemName.Ascending);
                query.es.Distinct = true;
                DataTable dtb = query.LoadDataTable();
                cboSurveilansPenyakit.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                if (dtb.Rows.Count > 0)
                {
                    foreach (DataRow row in dtb.Rows)
                    {
                        cboSurveilansPenyakit.Items.Add(new RadComboBoxItem(row["Note"].ToString(), row["Note"].ToString()));
                    }
                }
            }
        }

        public override PrintJobParameterCollection PrintJobParameters()
        {
            var parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_DiagnoseID", cboSurveilansPenyakit.SelectedValue);

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
                return string.Format("Nama Penyakit : {0}", cboSurveilansPenyakit.SelectedValue);
            }
        }

    }
    
}