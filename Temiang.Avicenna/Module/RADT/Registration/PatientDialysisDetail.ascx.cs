using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.Module.RADT
{
    public partial class PatientDialysisDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;
                return;
            }
            ViewState["IsNewRecord"] = false;
            
            txtInitialDiagnosis.Text = (String)DataBinder.Eval(DataItem, PatientDialysisMetadata.ColumnNames.InitialDiagnosis);
            txtPhysicianSenders.Text = (String)DataBinder.Eval(DataItem, PatientDialysisMetadata.ColumnNames.PhysicianSenders);
            txtRSSender.Text = (String)DataBinder.Eval(DataItem, PatientDialysisMetadata.ColumnNames.RSSender);


        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (PatientDialysisCollection)Session["collPatientDialysis"];

                bool isExist = false;
                foreach (PatientDialysis item in coll)
                {
                    if (item.InitialDiagnosis.Equals(txtInitialDiagnosis.Text))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Initial Diagnosis: {0} has exist", txtInitialDiagnosis.Text);
                }
            }
        }


        public string InitialDiagnosis
        {
            get { return txtInitialDiagnosis.Text; }
        }

        public string PhysicianSenders
        {
            get { return txtPhysicianSenders.Text; }
        }

        public string RSSender
        {
            get { return txtRSSender.Text; }
        }

        public string Hb
        {
            get { return txtHb.Text; }
        }

        public DateTime HbDate
        {
            get { return txtHbDate.SelectedDate.Value.Date; }
        }

        public string Urea
        {
            get { return txtUrea.Text; }
        }

        public DateTime UreaDate
        {
            get { return txtUreaDate.SelectedDate.Value.Date; }
        }

        public string Creatinine
        {
            get { return txtCreatinine.Text; }
        }

        public DateTime CreatinineDate
        {
            get { return txtCreatinineDate.SelectedDate.Value.Date; }
        }

        public string HBsAg
        {
            get { return txtHBsAg.Text; }
        }

        public DateTime HBsAgDate
        {
            get { return txtHBsAgDate.SelectedDate.Value.Date; }
        }

        public string AntiHCV
        {
            get { return txtAntiHCV.Text; }
        }

        public DateTime AntiHCVDate
        {
            get { return txtAntiHCVDate.SelectedDate.Value.Date; }
        }

        public string AntiHIV
        {
            get { return txtAntiHIV.Text; }
        }

        public DateTime AntiHIVDate
        {
            get { return txtAntiHIVDate.SelectedDate.Value.Date; }
        }

        public string KidneyUltrasound
        {
            get { return txtKidneyUltrasound.Text; }
        }

        public DateTime KidneyUltrasoundDate
        {
            get { return txtKidneyUltrasoundDate.SelectedDate.Value.Date; }
        }

        public string ECHO
        {
            get { return txtECHO.Text; }
        }

        public DateTime ECHODate
        {
            get { return txtECHODate.SelectedDate.Value.Date; }
        }

        public DateTime HDDate
        {
            get { return txtHDDate.SelectedDate.Value.Date; }
        }

        public DateTime TransferHDDate
        {
            get { return txtTransferHDDate.SelectedDate.Value.Date; }
        }

        //public string TransferHDDate2
        //{
        //    get { return txtTransferHDDate2.Text; }
        //}

        //public string TransferHDDate3
        //{
        //    get { return txtTransferHDDate3.Text; }
        //}

        public DateTime PDDate
        {
            get { return txtPDDate.SelectedDate.Value.Date; }
        }

        public DateTime TransferPDDate
        {
            get { return txtTransferPDDate.SelectedDate.Value.Date; }
        }

        public DateTime KidneytransplantDate
        {
            get { return txtKidneytransplantDate.SelectedDate.Value.Date; }
        }
    }
}