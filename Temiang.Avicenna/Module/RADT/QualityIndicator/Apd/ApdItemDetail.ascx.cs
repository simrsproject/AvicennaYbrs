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
    public partial class ApdItemDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            StandardReference.InitializeIncludeSpace(cboSRApdType, AppEnum.StandardReference.ApdType);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                return;
            }
            ViewState["IsNewRecord"] = false;
            cboSRApdType.Enabled = false;

            cboSRApdType.SelectedValue = (String)DataBinder.Eval(DataItem, ApdSurveyItemMetadata.ColumnNames.SRApdType);
            try
            {
                rbtIndication.SelectedValue = (bool)DataBinder.Eval(DataItem, ApdSurveyItemMetadata.ColumnNames.IsCorrectIndication) ? "1" : "0";
            }
            catch (Exception exception)
            {
            }
            try
            {
                rbtUsageTime.SelectedValue = (bool)DataBinder.Eval(DataItem, ApdSurveyItemMetadata.ColumnNames.IsCorrectUsageTime) ? "1" : "0";
            }
            catch (Exception exception)
            {
            }
            try
            {
                rbtUsageTechnique.SelectedValue = (bool)DataBinder.Eval(DataItem, ApdSurveyItemMetadata.ColumnNames.IsCorrectUsageTechnique) ? "1" : "0";
            }
            catch (Exception exception)
            {
            }
            try
            {
                rbtReleaseTechnique.SelectedValue = (bool)DataBinder.Eval(DataItem, ApdSurveyItemMetadata.ColumnNames.IsCorrectReleaseTechnique) ? "1" : "0";
            }
            catch (Exception exception)
            {
            }
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (ApdSurveyItemCollection)Session["collApdSurveyItem" + Request.UserHostName];
                string itemId = cboSRApdType.SelectedValue;

                if (coll.Any(row => row.SRApdType.Equals(itemId)))
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("APD Type {0} has exist", cboSRApdType.Text);
                    return;
                }
            }
        }

        #region Properties for return entry value
        public String SRApdType
        {
            get { return cboSRApdType.SelectedValue; }
        }
        public String ApdTypeName
        {
            get { return cboSRApdType.Text; }
        }
        public Boolean IsCorrectIndication
        {
            get { return rbtIndication.SelectedValue == "1"; }
        }
        public Boolean IsCorrectUsageTime
        {
            get { return rbtUsageTime.SelectedValue == "1"; }
        }
        public Boolean IsCorrectUsageTechnique
        {
            get { return rbtUsageTechnique.SelectedValue == "1"; }
        }
        public Boolean IsCorrectReleaseTechnique
        {
            get { return rbtReleaseTechnique.SelectedValue == "1"; }
        }
        #endregion
    }
}