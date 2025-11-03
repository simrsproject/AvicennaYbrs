using System;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class LocationExpireCtl : BaseOptionCtl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var coll = new LocationCollection();
                coll.Query.Where(coll.Query.ShortName == "EXP", coll.Query.IsActive == true);
                coll.LoadAll();

                cboLocationName.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (var item in coll)
                {
                    cboLocationName.Items.Add(new RadComboBoxItem(item.LocationName, item.LocationID));
                }
            }
        }
        
        public override PrintJobParameterCollection PrintJobParameters()
        {
            var parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_LocationID", cboLocationName.SelectedValue);

            //Retun List
            return parameters;
        }

        public override string ReportSubTitle
        {
            get
            {
                return string.Format("Location : {0}", cboLocationName.SelectedValue);
            }
        }
    }
}