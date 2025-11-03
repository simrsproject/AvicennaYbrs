using System;
using System.Collections.Generic;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class QueuePharmacyCtl : BaseOptionCtl
    {

        protected void Page_Init(object sender, EventArgs e)
        {

            //StandardReference Initialize
            if (!IsPostBack)
            {
                var coll = new AppStandardReferenceItemCollection();
                coll.Query.Where(coll.Query.StandardReferenceID == AppEnum.StandardReference.KioskQueueFar,
                                 coll.Query.ReferenceID == "KioskQueueFar", coll.Query.IsActive == true);
                coll.LoadAll();
                foreach (var item in coll)
                {
                    cboQuePhar.Items.Add(new RadComboBoxItem(item.ItemName, item.ItemID));
                }
            }
        }
        
        public override PrintJobParameterCollection PrintJobParameters()
        {
            PrintJobParameterCollection parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_QueuePharID", cboQuePhar.SelectedValue);

            //Retun List
            return parameters;
        }
    }
}