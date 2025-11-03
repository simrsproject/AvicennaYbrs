using System;
using System.Collections.Generic;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class ShiftBaseAsriCtl : BaseOptionCtl
    {
        protected void Page_Init(object sender, EventArgs e)
        {

            //StandardReference Initialize
            if (!IsPostBack)
            {
                var coll = new AppStandardReferenceItemCollection();
                coll.Query.Where(coll.Query.StandardReferenceID == "Shift", coll.Query.IsActive == true);
                coll.LoadAll();
                cboShift.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (var i in coll)
                {
                    cboShift.Items.Add(new RadComboBoxItem(i.ItemName, i.ItemID));
                }
            }
        }

        public override PrintJobParameterCollection PrintJobParameters()
        {
            PrintJobParameterCollection parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_Shift", cboShift.SelectedValue);

            //Retun List
            return parameters;
        }
    }
}