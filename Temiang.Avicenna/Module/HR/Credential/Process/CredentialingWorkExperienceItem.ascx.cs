using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.Credential.Process
{
    public partial class CredentialingWorkExperienceItem : BaseUserControl
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

                var coll = (CredentialProcessWorkExperienceCollection)Session["collCredentialProcessWorkExperience" + Request.UserHostName];
                if (coll.Count == 0)
                {
                    txtWorkExperienceNo.Text = "001";
                }
                else
                {
                    var seqNoMax = (coll.OrderByDescending(c => c.WorkExperienceNo).Select(c => c.WorkExperienceNo)).Take(1);
                    int seqNo = int.Parse(seqNoMax.Single()) + 1;
                    txtWorkExperienceNo.Text = string.Format("{0:000}", seqNo);
                }

                return;
            }
            ViewState["IsNewRecord"] = false;

            txtWorkExperienceNo.Text = DataBinder.Eval(DataItem, CredentialProcessWorkExperienceMetadata.ColumnNames.WorkExperienceNo).ToString();
            txtInstitutionName.Text = DataBinder.Eval(DataItem, CredentialProcessWorkExperienceMetadata.ColumnNames.InstitutionName).ToString();
            txtStartPeriod.Text = DataBinder.Eval(DataItem, CredentialProcessWorkExperienceMetadata.ColumnNames.StartPeriod).ToString();
            txtEndPeriod.Text = DataBinder.Eval(DataItem, CredentialProcessWorkExperienceMetadata.ColumnNames.EndPeriod).ToString();
            txtPositionName.Text = DataBinder.Eval(DataItem, CredentialProcessWorkExperienceMetadata.ColumnNames.PositionName).ToString();
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll =
                    (CredentialProcessWorkExperienceCollection)Session["collCredentialProcessWorkExperience" + Request.UserHostName];

                //TODO: Betulkan cara pengecekannya
                string id = txtWorkExperienceNo.Text;
                bool isExist = false;
                foreach (CredentialProcessWorkExperience item in coll)
                {
                    if (item.WorkExperienceNo.Equals(id))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Seq No : {0} has exist", id);
                }
            }
        }

        #region Properties for return entry value
        public String WorkExperienceNo
        {
            get { return txtWorkExperienceNo.Text; }
        }
        public String InstitutionName
        {
            get { return txtInstitutionName.Text; }
        }
        public String StartPeriod
        {
            get { return txtStartPeriod.Text; }
        }
        public String EndPeriod
        {
            get { return txtEndPeriod.Text; }
        }
        public String PositionName
        {
            get { return txtPositionName.Text; }
        }
        #endregion
    }
}