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

namespace Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl
{
    public partial class AssessmentImageItem : BaseUserControl
    {
        public object DataItem { get; set; }

        protected override void OnDataBinding(EventArgs e)
        {
            ViewState["IsNewRecord"] = (DataItem is GridInsertionObject);
            if (DataItem is GridInsertionObject)
            {
                return;
            }
            //var tciImgs = (PatientAssessmentImageCollection)Session["paimg"];

            //var img = tciImgs.FirstOrDefault(rec => rec.ImageNo.Equals(hdnImageNo.Value) && rec.ImageNo.Equals(1));
            var img = (byte[])DataBinder.Eval(DataItem, PatientAssessmentImageMetadata.ColumnNames.DocumentImage);
            if (img != null)
            {
                imgAssessmentImage.ImageUrl = string.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(img));
                hdnAssessmentImage.Value = imgAssessmentImage.ImageUrl;
            }
            txtDocumentName.Text = (string)DataBinder.Eval(DataItem, PatientAssessmentImageMetadata.ColumnNames.DocumentName);
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (string.IsNullOrEmpty(hdnAssessmentImage.Value))
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = "Image still empty";
                return;
            }
        }


        #region Properties for return entry value


        public String ImageCaptureInString
        {
            get { return hdnAssessmentImage.Value; }
        }

        public Boolean IsNewRecord
        {
            get { return (bool)ViewState["IsNewRecord"]; }
        }

        public String DocumentName
        {
            get { return txtDocumentName.Text; }
        }

        #endregion

    }
}