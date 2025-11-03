using System;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class pCashTransactionPeriode : BaseOptionCtl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Value initialized
            if (!Page.IsPostBack)
            {
                txtToDate.SelectedDate = DateTime.Today;
                txtFromDate.SelectedDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);

                foreach (var entity in Bank.Get())
                {
                    ddlBankAccount.Items.Add(new ListItem(string.Format("{0} - {1}", entity.BankName, entity.NoRek), entity.BankID));
                }
            }
        }
        public override PrintJobParameterCollection PrintJobParameters()
        {
            PrintJobParameterCollection parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_DateBetween_Start", txtFromDate.SelectedDate);
            parameters.AddNew("p_DateBetween_End", txtToDate.SelectedDate);
            parameters.AddNew("p_BankId", ddlBankAccount.SelectedItem.Value);
            
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
                return string.Format("Period : {0} to {1}", txtFromDate.SelectedDate, txtToDate.SelectedDate);
            }
        }

    }
}