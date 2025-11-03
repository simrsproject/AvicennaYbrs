using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class BedDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            //PopUpSearch.InitializeOnButtonClick(AppEnum.PopUpSearch.Class, txtClassID);

            var coll = new ClassCollection();
            coll.Query.Where
                (
                    coll.Query.IsActive == true,
                    coll.Query.IsInPatientClass == true
                );
            coll.Query.OrderBy(coll.Query.ClassID.Ascending);
            coll.LoadAll();

            cboClassID.Items.Clear();
            cboDefaultChargeClassID.Items.Clear();
            cboClassID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            cboDefaultChargeClassID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (Class cl in coll)
            {
                cboClassID.Items.Add(new RadComboBoxItem(cl.ClassName, cl.ClassID));
                if (cl.IsTariffClass == true)
                    cboDefaultChargeClassID.Items.Add(new RadComboBoxItem(cl.ClassName, cl.ClassID));
            }

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                chkIsNeedConfirmation.Checked = AppSession.Parameter.IsBedNeedConfirmation;
                chkIsActive.Checked = true;

                hdnRegistrationNo.Value = string.Empty;
                hdnSRBedStatus.Value = AppSession.Parameter.BedStatusUnoccupied;

                return;
            }
            ViewState["IsNewRecord"] = false;

            txtBedID.Text = (String)DataBinder.Eval(DataItem, BedMetadata.ColumnNames.BedID);
            cboClassID.SelectedValue = (String)DataBinder.Eval(DataItem, BedMetadata.ColumnNames.ClassID);
            cboDefaultChargeClassID.SelectedValue = (String)DataBinder.Eval(DataItem, BedMetadata.ColumnNames.DefaultChargeClassID);
            chkIsTemporary.Checked = (bool)DataBinder.Eval(DataItem, BedMetadata.ColumnNames.IsTemporary);
            chkIsNeedConfirmation.Checked = (bool)DataBinder.Eval(DataItem, BedMetadata.ColumnNames.IsNeedConfirmation);
            chkIsActive.Checked = (bool)DataBinder.Eval(DataItem, BedMetadata.ColumnNames.IsActive);
            chkIsSharedTo3rdParty.Checked = (bool)DataBinder.Eval(DataItem, BedMetadata.ColumnNames.IsVisibleTo3rdParty);
            txtNotes.Text = (String)DataBinder.Eval(DataItem, BedMetadata.ColumnNames.Notes);

            hdnRegistrationNo.Value = (String)DataBinder.Eval(DataItem, BedMetadata.ColumnNames.RegistrationNo);
            hdnSRBedStatus.Value= (String)DataBinder.Eval(DataItem, BedMetadata.ColumnNames.SRBedStatus);

            if (!string.IsNullOrEmpty((String)DataBinder.Eval(DataItem, BedMetadata.ColumnNames.RegistrationNo)))
            {
                txtBedID.Enabled = false;
                cboClassID.Enabled = false;
                chkIsActive.Enabled = false;
            }
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                BedCollection coll = (BedCollection)Session["collBed"];

                string id = txtBedID.Text;
                bool isExist = false;
                foreach (Bed item in coll)
                {
                    if (item.BedID.Equals(id) && item.ClassID == cboClassID.SelectedValue)
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("ID: {0} has exist", id);
                }
            }
        }

        #region Properties for return entry value

        public String BedID
        {
            get { return txtBedID.Text; }
        }

        public String ClassID
        {
            get { return cboClassID.SelectedValue; }
        }

        public String ClassName
        {
            get { return cboClassID.Text; }
        }

        public String DefaultChargeClassID
        {
            get { return cboDefaultChargeClassID.SelectedValue; }
        }

        public String DefaultChargeClassName
        {
            get { return cboDefaultChargeClassID.Text; }
        }

        public Boolean IsTemporary
        {
            get { return chkIsTemporary.Checked; }
        }

        public Boolean IsActive
        {
            get { return chkIsActive.Checked; }
        }

        public Boolean IsNeedConfirmation
        {
            get { return chkIsNeedConfirmation.Checked; }
        }

        public Boolean IsSharedTo3rdParty
        {
            get { return chkIsSharedTo3rdParty.Checked; }
        }

        public String RegistrationNo
        {
            get { return hdnRegistrationNo.Value; }
        }

        public String SRBedStatus
        {
            get { return hdnSRBedStatus.Value; }
        }

        public String Notes
        {
            get { return txtNotes.Text; }
        }
        #endregion

        #region Method & Event TextChanged

        #endregion
    }
}
