using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using System.Data;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class TherapyCtl : BaseOptionCtl
    {
        #region ComboBox
        protected void cboSRTherapyGroupID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            Common.ComboBox.StandardReferenceItemsRequested(cboSRTherapyGroupID, "TherapyGroup", e.Text);
        }

        protected void cboSRTherapyGroupID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            Common.ComboBox.StandardReferenceItemDataBound(e);
        }

        protected void cboSRTherapyGroupID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            PopulatecboTherapyID(e.Value);

            cboTherapyID.Text = string.Empty;
            cboTherapyID.SelectedValue = string.Empty;
        }

        protected void cboTherapyID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            TherapyQuery query = new TherapyQuery();
            query.es.Top = 10;
            query.Select
                (
                    query.TherapyID,
                    query.TherapyName
                );
            query.Where
                (
                    query.Or
                        (
                            query.TherapyID.Like(searchTextContain),
                            query.TherapyName.Like(searchTextContain)
                        )
                );
            query.Where(
                    query.SRTherapyGroup == cboSRTherapyGroupID.SelectedValue
                );
            query.OrderBy(query.SRTherapyGroup.Ascending, query.TherapyID.Ascending);

            cboTherapyID.DataSource = query.LoadDataTable();
            cboTherapyID.DataBind();
        }

        protected void PopulatecboTherapyID(string SRTherapyGroupID)
        {

            TherapyQuery query = new TherapyQuery();
            query.es.Top = 10;
            query.Select
                (
                    query.TherapyID,
                    query.TherapyName
                );
            query.Where(
                    query.SRTherapyGroup == SRTherapyGroupID
                );
            query.OrderBy(query.SRTherapyGroup.Ascending, query.TherapyID.Ascending);

            cboTherapyID.DataSource = query.LoadDataTable();
            cboTherapyID.DataBind();
        }

        protected void cboTherapyID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["TherapyName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["TherapyID"].ToString();
        }
        #endregion

        public override PrintJobParameterCollection PrintJobParameters()
        {
            var parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_SRTherapyGroupID", cboSRTherapyGroupID.SelectedValue);
            parameters.AddNew("p_TherapyID", cboTherapyID.SelectedValue);

            //Retun List
            return parameters;
        }

        //public override string ParameterCaption
        //{
        //    get { return lblCaption.Text; }
        //    set { lblCaption.Text = value; }
        //}

        //public override string ReportSubTitle
        //{
        //    get
        //    {
        //        return string.Format("Therapy : {0}", cboTherapyID.Text);
        //    }
        //}
    }
}