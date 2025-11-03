using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.QualityIndicator
{
    public partial class PatientIncidentInvestigationItemDetail : BaseUserControl
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

                var collitem = (PatientIncidentInvestigationCollection)Session["collPatientIncidentInvestigation"];
                if (collitem.Count == 0)
                    txtSeqNo.Text = "001";
                else
                {
                    int seqNo = 0;
                    foreach (PatientIncidentInvestigation item in collitem)
                    {
                        if (int.Parse(item.SeqNo) > seqNo)
                            seqNo = int.Parse(item.SeqNo);
                    }
                    txtSeqNo.Text = string.Format("{0:000}", seqNo + 1);
                }

                return;
            }
            ViewState["IsNewRecord"] = false;
            txtSeqNo.Text = (String)DataBinder.Eval(DataItem, PatientIncidentInvestigationMetadata.ColumnNames.SeqNo);
            txtRecomendation.Text = (String)DataBinder.Eval(DataItem, PatientIncidentInvestigationMetadata.ColumnNames.Recomendation);
            object recomendationDate = DataBinder.Eval(DataItem, PatientIncidentInvestigationMetadata.ColumnNames.RecomendationDateTime);
            if (recomendationDate != null)
                txtRecomendationDateTime.SelectedDate = (DateTime)DataBinder.Eval(DataItem, PatientIncidentInvestigationMetadata.ColumnNames.RecomendationDateTime);
            else
                txtRecomendationDateTime.Clear();
            txtPersonInCharge.Text = (String)DataBinder.Eval(DataItem, PatientIncidentInvestigationMetadata.ColumnNames.PersonInCharge);
            txtImplementation.Text = (String)DataBinder.Eval(DataItem, PatientIncidentInvestigationMetadata.ColumnNames.Implementation);
            object implementarionDate = DataBinder.Eval(DataItem, PatientIncidentInvestigationMetadata.ColumnNames.ImplementationDateTime);
            if (implementarionDate != null)
                txtImplementationDateTime.SelectedDate = (DateTime)DataBinder.Eval(DataItem, PatientIncidentInvestigationMetadata.ColumnNames.ImplementationDateTime);
            else
                txtImplementationDateTime.Clear();
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (PatientIncidentInvestigationCollection)Session["collPatientIncidentInvestigation"];

                string seqNo = txtSeqNo.Text;
                bool isExist = false;

                foreach (PatientIncidentInvestigation item in coll)
                {
                    if (item.SeqNo.Equals(seqNo))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage =
                        string.Format("Seq No : {0} already exist", txtSeqNo.Text);
                }
            }
        }

        #region Properties for return entry value

        public String SeqNo
        {
            get { return txtSeqNo.Text; }
        }

        public String Recomendation
        {
            get { return txtRecomendation.Text; }
        }

        public DateTime? RecomendationDateTime
        {
            get { return txtRecomendationDateTime.SelectedDate; }
        }

        public String PersonInCharge
        {
            get { return txtPersonInCharge.Text; }
        }

        public String Implementation
        {
            get { return txtImplementation.Text; }
        }

        public DateTime? ImplementationDateTime
        {
            get { return txtImplementationDateTime.SelectedDate; }
        }

        #endregion
    }
}