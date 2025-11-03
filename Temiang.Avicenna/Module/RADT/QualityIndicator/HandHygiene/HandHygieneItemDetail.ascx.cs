using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.QualityIndicator
{
    public partial class HandHygieneItemDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            StandardReference.InitializeIncludeSpace(cboSROpportunity, AppEnum.StandardReference.Opportunity);
            StandardReference.InitializeIncludeSpace(cboSRHandWashType, AppEnum.StandardReference.HandWashType);
            StandardReference.InitializeIncludeSpace(cboSRHandHygieneNote, AppEnum.StandardReference.HandHygieneNote);
            StandardReference.InitializeIncludeSpace(cboSRApply6StepsResult, AppEnum.StandardReference.Apply6StepsResult);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;
                if (!AppSession.Parameter.IsHandHygieneNoteNoValidation)
                {
                    rfvSRHandHygieneNote.Visible = true;
                }
                    return;
            }
            ViewState["IsNewRecord"] = false;
            cboSROpportunity.Enabled = false;


            cboSROpportunity.SelectedValue = (String)DataBinder.Eval(DataItem, HandHygieneItemMetadata.ColumnNames.SROpportunity);
            cboSRHandWashType.SelectedValue = (String)DataBinder.Eval(DataItem, HandHygieneItemMetadata.ColumnNames.SRHandWashType);
            chkIsWearGloves.Checked = Convert.ToBoolean(DataBinder.Eval(DataItem, HandHygieneItemMetadata.ColumnNames.IsWearGloves));
            cboSRHandHygieneNote.SelectedValue = (String)DataBinder.Eval(DataItem, HandHygieneItemMetadata.ColumnNames.SRHandHygieneNote);
            chkIsApply6Steps.Checked = Convert.ToBoolean(DataBinder.Eval(DataItem, HandHygieneItemMetadata.ColumnNames.IsApply6Steps));
            cboSRApply6StepsResult.SelectedValue = (String)DataBinder.Eval(DataItem, HandHygieneItemMetadata.ColumnNames.SRApply6StepsResult);
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
           

            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (HandHygieneItemCollection)Session["collHandHygieneItem" + Request.UserHostName];
                string itemId = cboSROpportunity.SelectedValue;

                if (coll.Any(row => row.SROpportunity.Equals(itemId)))
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Opportunity {0} has exist", cboSROpportunity.Text);
                    return;
                }
                if (string.IsNullOrEmpty(cboSROpportunity.SelectedValue))
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Invalid Opportunity.");
                    return;
                }
            }
            if(!AppSession.Parameter.IsHandHygieneNoteNoValidation)
            {
                if (string.IsNullOrEmpty(cboSRHandHygieneNote.SelectedValue))
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Invalid Note.");
                    return;
                }
            }
            if (string.IsNullOrEmpty(cboSRHandWashType.SelectedValue))
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = string.Format("Invalid Hand Wash Type.");
                return;
            }
            

            if (chkIsApply6Steps.Checked && string.IsNullOrEmpty(cboSRApply6StepsResult.SelectedValue))
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = string.Format("Apply 6 Steps Result required.");
                return;
            }

        }

        #region Properties for return entry value
        public String SROpportunity
        {
            get { return cboSROpportunity.SelectedValue; }
        }
        public String OpportunityName
        {
            get { return cboSROpportunity.Text; }
        }
        public String SRHandWashType
        {
            get { return cboSRHandWashType.SelectedValue; }
        }
        public String HandWashTypeName
        {
            get { return cboSRHandWashType.Text; }
        }
        public String SRHandHygieneNote
        {
            get { return cboSRHandHygieneNote.SelectedValue; }
        }
        public String HandHygieneNoteName
        {
            get { return cboSRHandHygieneNote.Text; }
        }
        public bool IsWearGloves
        {
            get { return chkIsWearGloves.Checked; }
        }
        public bool IsApply6Steps
        {
            get { return chkIsApply6Steps.Checked; }
        }
        public String SRApply6StepsResult
        {
            get { return cboSRApply6StepsResult.SelectedValue; }
        }
        public String Apply6StepsResultName
        {
            get { return cboSRApply6StepsResult.Text; }
        }
        #endregion
    }
}