using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ItemLaboratoryResult : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            StandardReference.InitializeIncludeSpace(cboSRAgeType, AppEnum.StandardReference.AgeUnit);
            StandardReference.InitializeIncludeSpace(cboSRanswerType, AppEnum.StandardReference.AnswerType);

            var qst = new QuestionAnswerSelectionCollection();
            qst.LoadAll();

            cboValueReference.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (var entity in qst)
            {
                cboValueReference.Items.Add(new RadComboBoxItem(entity.QuestionAnswerSelectionText, entity.QuestionAnswerSelectionID));
            }

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                var coll = (ItemLaboratoryDetailCollection)Session["collItemLaboratoryDetail"];
                if (coll.Count == 0) ViewState["SequenceNo"] = "001";
                else
                {
                    var sequenceNo = (coll.OrderByDescending(c => c.SequenceNo).Select(c => c.SequenceNo)).Take(1);
                    int seqNo = int.Parse(sequenceNo.Single()) + 1;
                    ViewState["SequenceNo"] = string.Format("{0:000}", seqNo);
                }
                return;
            }
            ViewState["IsNewRecord"] = false;

            ViewState["SequenceNo"] = (String)DataBinder.Eval(DataItem, ItemLaboratoryDetailMetadata.ColumnNames.SequenceNo);
            cboSex.SelectedValue = (String)DataBinder.Eval(DataItem, ItemLaboratoryDetailMetadata.ColumnNames.Sex);
            cboSRAgeType.SelectedValue = (String)DataBinder.Eval(DataItem, ItemLaboratoryDetailMetadata.ColumnNames.SRAgeUnit);
            txtAgeMin.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ItemLaboratoryDetailMetadata.ColumnNames.AgeMin));
            txtAgeMax.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ItemLaboratoryDetailMetadata.ColumnNames.AgeMax));
            cboSRanswerType.SelectedValue = (String)DataBinder.Eval(DataItem, ItemLaboratoryDetailMetadata.ColumnNames.SRAnswerType);
            txtNormalValueMin.Text = (String)DataBinder.Eval(DataItem, ItemLaboratoryDetailMetadata.ColumnNames.NormalValueMin);
            txtNormalValueMax.Text = (String)DataBinder.Eval(DataItem, ItemLaboratoryDetailMetadata.ColumnNames.NormalValueMax);
            txtNotes.Text = (String)DataBinder.Eval(DataItem, ItemLaboratoryDetailMetadata.ColumnNames.Notes);
            cboValueReference.SelectedValue = (String)DataBinder.Eval(DataItem, ItemLaboratoryDetailMetadata.ColumnNames.AnswerTypeReferenceID);
            cboValueReference.Enabled = !string.IsNullOrEmpty(cboValueReference.SelectedValue);
        }

        public string SequenceNo
        {
            get { return ViewState["SequenceNo"].ToString(); }
        }

        public string Sex
        {
            get { return cboSex.SelectedValue; }
        }

        public string SRAgeUnit
        {
            get { return cboSRAgeType.SelectedValue; }
        }

        public string AgeUnit
        {
            get { return cboSRAgeType.Text; }
        }

        public decimal AgeMin
        {
            get { return Convert.ToDecimal(txtAgeMin.Value); }
        }

        public decimal AgeMax
        {
            get { return Convert.ToDecimal(txtAgeMax.Value); }
        }

        public string SRAnswerType
        {
            get { return cboSRanswerType.SelectedValue; }
        }

        public string AnswerTypeName
        {
            get { return cboSRanswerType.Text; }
        }

        public string NormalValueMin
        {
            get { return txtNormalValueMin.Text.Trim(); }
        }

        public string NormalValueMax
        {
            get { return txtNormalValueMax.Text.Trim(); }
        }

        public string Notes
        {
            get { return txtNotes.Text.Trim(); }
        }

        public string AnswerTypeReferenceID
        {
            get { return cboValueReference.SelectedValue; }
        }

        public string AnswerTypeReferenceName
        {
            get { return cboValueReference.Text; }
        }

        protected void cboSRanswerType_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboValueReference.Enabled = (e.Value == "CBO");
            cboValueReference.SelectedValue = string.Empty;
        }
    }
}