using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using System.Data;

namespace Temiang.Avicenna.Module.HR.TrainingHR
{
    public partial class EmployeeTrainingExternalTrainerDetail : BaseUserControl
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

                var coll = (EmployeeTrainingExternalTrainerCollection)Session["collEmployeeTrainingExternalTrainer" + Request.UserHostName];
                if (coll.Count == 0)
                {
                    txtExternalTrainerSeqNo.Text = "001";
                }
                else
                {
                    var seqNoMax = (coll.OrderByDescending(c => c.ExternalTrainerSeqNo).Select(c => c.ExternalTrainerSeqNo)).Take(1);
                    int seqNo = int.Parse(seqNoMax.Single()) + 1;
                    txtExternalTrainerSeqNo.Text = string.Format("{0:000}", seqNo);
                }

                return;
            }
            ViewState["IsNewRecord"] = false;

            txtExternalTrainerSeqNo.Text = Convert.ToString(DataBinder.Eval(DataItem, EmployeeTrainingExternalTrainerMetadata.ColumnNames.ExternalTrainerSeqNo));
            txtExternalTrainerName.Text = (String)DataBinder.Eval(DataItem, EmployeeTrainingExternalTrainerMetadata.ColumnNames.ExternalTrainerName);
            txtPositionAs.Text = (String)DataBinder.Eval(DataItem, EmployeeTrainingExternalTrainerMetadata.ColumnNames.PositionAs);
            txtNotes.Text = (String)DataBinder.Eval(DataItem, EmployeeTrainingExternalTrainerMetadata.ColumnNames.Notes);
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (EmployeeTrainingExternalTrainerCollection)Session["collEmployeeTrainingExternalTrainer" + Request.UserHostName];

                //TODO: Betulkan cara pengecekannya
                bool isExist = false;
                foreach (EmployeeTrainingExternalTrainer item in coll)
                {
                    if (item.ExternalTrainerName.ToString().Equals(txtExternalTrainerName.Text))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Trainer Name: {0} has exist", txtExternalTrainerName.Text);
                }
            }
        }

        #region Properties for return entry value

        public String ExternalTrainerSeqNo
        {
            get { return txtExternalTrainerSeqNo.Text; }
        }

        public String ExternalTrainerName
        {
            get { return txtExternalTrainerName.Text; }
        }

        public String PositionAs
        {
            get { return txtPositionAs.Text; }
        }

        public String Notes
        {
            get { return txtNotes.Text; }
        }
        #endregion
    }
}