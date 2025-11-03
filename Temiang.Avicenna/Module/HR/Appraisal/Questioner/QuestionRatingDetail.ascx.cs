using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.Appraisal
{
    public partial class QuestionRatingDetail : BaseUserControl
    {
        private object _dataItem;
        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }
        protected override void OnDataBinding(EventArgs e)
        {
            trRatingValue.Visible = AppSession.Parameter.AppraisalVersionNo == "2";
            trRatingValueMinMax.Visible = AppSession.Parameter.AppraisalVersionNo == "3";
            trRatingValueMinMaxPercent.Visible = AppSession.Parameter.AppraisalVersionNo == "2";
            
            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;
                txtRatingValue.Value = 0;
                txtRatingValueMin.Value = 0;
                txtRatingValueMax.Value = 0;
                txtRatingValueMinPercent.Value = 0;
                txtRatingValueMaxPercent.Value = 0;

                return;
            }
            ViewState["IsNewRecord"] = false;

            txtRatingCode.Text = (String)DataBinder.Eval(DataItem, AppraisalQuestionRatingMetadata.ColumnNames.RatingCode);
            txtRatingName.Text = (String)DataBinder.Eval(DataItem, AppraisalQuestionRatingMetadata.ColumnNames.RatingName);
            txtRatingValue.Value = Convert.ToDouble(DataBinder.Eval(DataItem, AppraisalQuestionRatingMetadata.ColumnNames.RatingValue));
            txtRatingValueMin.Value = Convert.ToDouble(DataBinder.Eval(DataItem, AppraisalQuestionRatingMetadata.ColumnNames.RatingValueMin));
            txtRatingValueMax.Value = Convert.ToDouble(DataBinder.Eval(DataItem, AppraisalQuestionRatingMetadata.ColumnNames.RatingValueMax));
            txtRatingValueMinPercent.Value = Convert.ToDouble(DataBinder.Eval(DataItem, AppraisalQuestionRatingMetadata.ColumnNames.RatingValueMin));
            txtRatingValueMaxPercent.Value = Convert.ToDouble(DataBinder.Eval(DataItem, AppraisalQuestionRatingMetadata.ColumnNames.RatingValueMax));
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (AppraisalQuestionRatingCollection)Session["collAppraisalQuestionRating" + Request.UserHostName];

                //TODO: Betulkan cara pengecekannya
                string code = txtRatingCode.Text;
                bool isExist = false;
                foreach (AppraisalQuestionRating item in coll)
                {
                    if (item.RatingCode.Equals(code))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Rating Code: {0} has exist", code);
                }
            }
        }

        #region Properties for return entry value
        public String RatingCode
        {
            get { return txtRatingCode.Text; }
        }

        public String RatingName
        {
            get { return txtRatingName.Text; }
        }

        public Decimal RatingValue
        {
            get { return Convert.ToDecimal(txtRatingValue.Value); }
        }

        public Decimal RatingValueMin
        {
            get 
            {
                if (AppSession.Parameter.AppraisalVersionNo == "2")
                    return Convert.ToDecimal(txtRatingValueMinPercent.Value);

                return Convert.ToDecimal(txtRatingValueMin.Value); 
            }
        }

        public Decimal RatingValueMax
        {
            get 
            {
                if (AppSession.Parameter.AppraisalVersionNo == "2")
                    return Convert.ToDecimal(txtRatingValueMaxPercent.Value);

                return Convert.ToDecimal(txtRatingValueMax.Value); 
            }
        }
        #endregion
    }
}