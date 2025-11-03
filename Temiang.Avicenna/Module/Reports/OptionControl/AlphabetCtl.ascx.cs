using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class AlphabetCtl : BaseOptionCtl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Value initialized
            if (!Page.IsPostBack)
            {
                txtFromAlphabet.Text = "A";
                txtToAlphabet.Text = "Z";
            }
        }
        public override PrintJobParameterCollection PrintJobParameters()
        {
            var parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_FromAlphabet", txtFromAlphabet.Text);
            parameters.AddNew("p_ToAlphabet", txtToAlphabet.Text);

            //Retun List
            return parameters;
        }
    }
}