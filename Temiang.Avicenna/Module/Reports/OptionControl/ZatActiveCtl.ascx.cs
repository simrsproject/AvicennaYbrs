using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using System.Data;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class ZatActiveCtl : BaseOptionCtl
    {
        #region ComboBox
        protected void cboZatActiveID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ZatActiveName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ZatActiveID"].ToString();
        }

        protected void cboZatActiveID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            DataTable tbl = LoadItem(e.Text);
            cboZatActiveID.DataSource = tbl;
            cboZatActiveID.DataBind();
        }

        private DataTable LoadItem(string searchText)
        {
            string searchTextContain = string.Format("%{0}%", searchText);
            var query = new ZatActiveQuery("a");
            query.es.Top = 20;
            query.Select(query.ZatActiveID, query.ZatActiveName);
            query.Where(query.IsActive == true,
                        query.Or(query.ZatActiveName.Like(searchTextContain),
                                 query.ZatActiveID.Like(searchTextContain)));
            query.OrderBy(query.ZatActiveName.Ascending);

            return query.LoadDataTable();
        }
        #endregion

        public override PrintJobParameterCollection PrintJobParameters()
        {
            var parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_ZatActiveID", cboZatActiveID.SelectedValue);

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
                return string.Format("Zat Active : {0}", cboZatActiveID.SelectedValue);
            }
        }
    }
}