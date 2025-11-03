using System;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class BankCtl : BaseOptionCtl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                foreach (var entity in Bank.Get())
                {
                    ddlBankAccount.Items.Add(new ListItem(string.Format("{0}-{1}", entity.BankName, entity.NoRek),
                                                          entity.BankID));
                }
            }

        }

        public override PrintJobParameterCollection PrintJobParameters()
        {
            PrintJobParameterCollection parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_BankId",ddlBankAccount.SelectedItem.Value);
            return parameters;
        }


    }
}