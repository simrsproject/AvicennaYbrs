using System;
using System.Collections.Generic;
using DevExpress.Xpo.DB.Helpers;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class BankHRDCtl : BaseOptionCtl
    {

        protected void Page_Init(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                var bank = new AppStandardReferenceItemCollection();
                bank.Query.Where(bank.Query.StandardReferenceID == "BankHRD", bank.Query.IsActive == true);
                bank.LoadAll();

                cboBankID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (AppStandardReferenceItem entity in bank)
                {
                    cboBankID.Items.Add(new RadComboBoxItem(entity.ItemName, entity.ItemID));
                }

            }
        }

        public override PrintJobParameterCollection PrintJobParameters()
        {
            PrintJobParameterCollection parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_BankID", cboBankID.SelectedValue);

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
                return string.Format("Bank : {0}", cboBankID.SelectedValue);
            }
        }

    }
}