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
    public partial class CredentialingCpdItem : BaseUserControl
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

                var coll = (CredentialProcessCpdCollection)Session["collCredentialProcessCpd" + Request.UserHostName];
                if (coll.Count == 0)
                {
                    txtCpdNo.Text = "001";
                }
                else
                {
                    var seqNoMax = (coll.OrderByDescending(c => c.CpdNo).Select(c => c.CpdNo)).Take(1);
                    int seqNo = int.Parse(seqNoMax.Single()) + 1;
                    txtCpdNo.Text = string.Format("{0:000}", seqNo);
                }

                return;
            }
            ViewState["IsNewRecord"] = false;

            txtCpdNo.Text = DataBinder.Eval(DataItem, CredentialProcessCpdMetadata.ColumnNames.CpdNo).ToString();
            txtCpdName.Text = DataBinder.Eval(DataItem, CredentialProcessCpdMetadata.ColumnNames.CpdName).ToString();
            txtInstitutionName.Text = DataBinder.Eval(DataItem, CredentialProcessCpdMetadata.ColumnNames.InstitutionName).ToString();
            txtTimeAndHours.Text = DataBinder.Eval(DataItem, CredentialProcessCpdMetadata.ColumnNames.TimeAndHours).ToString();
            txtSkp.Value = Convert.ToDouble(DataBinder.Eval(DataItem, CredentialProcessCpdMetadata.ColumnNames.Skp));
            txtAchievedCompetence.Text = DataBinder.Eval(DataItem, CredentialProcessCpdMetadata.ColumnNames.AchievedCompetence).ToString();
            txtPhysicalEvidence.Text = DataBinder.Eval(DataItem, CredentialProcessCpdMetadata.ColumnNames.PhysicalEvidence).ToString();
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll =
                    (CredentialProcessCpdCollection)Session["collCredentialProcessCpd" + Request.UserHostName];

                //TODO: Betulkan cara pengecekannya
                string id = txtCpdNo.Text;
                bool isExist = false;
                foreach (CredentialProcessCpd item in coll)
                {
                    if (item.CpdNo.Equals(id))
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
        public String CpdNo
        {
            get { return txtCpdNo.Text; }
        }
        public String CpdName
        {
            get { return txtCpdName.Text; }
        }
        public String InstitutionName
        {
            get { return txtInstitutionName.Text; }
        }
        public String TimeAndHours
        {
            get { return txtTimeAndHours.Text; }
        }
        public Decimal Skp
        {
            get { return Convert.ToDecimal(txtSkp.Value); }
        }
        public String AchievedCompetence
        {
            get { return txtAchievedCompetence.Text; }
        }
        public String PhysicalEvidence
        {
            get { return txtPhysicalEvidence.Text; }
        }
        #endregion
    }
}