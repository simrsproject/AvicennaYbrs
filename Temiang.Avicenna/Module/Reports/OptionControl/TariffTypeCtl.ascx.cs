using System;
using System.Collections.Generic;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class TariffTypeCtl : BaseOptionCtl
    {

        protected void Page_Init(object sender, EventArgs e)
        {

            //StandardReference Initialize
            if (!IsPostBack)
            {
                var coll = new AppStandardReferenceItemCollection();
                coll.Query.Where(coll.Query.StandardReferenceID == "TariffType", coll.Query.IsActive == true);
                coll.LoadAll();
                foreach (var i in coll)
                {
                    cboTariffType.Items.Add(new RadComboBoxItem(i.ItemName, i.ItemID));
                }
            }
        }
        
        public override PrintJobParameterCollection PrintJobParameters()
        {
            PrintJobParameterCollection parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_TariffType", cboTariffType.SelectedValue);

            //Retun List
            return parameters;
        }
    }
}