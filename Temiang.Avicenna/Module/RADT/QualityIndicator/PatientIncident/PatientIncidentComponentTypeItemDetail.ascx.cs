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
    public partial class PatientIncidentComponentTypeItemDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            //PopulateIncidentType();

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                return;
            }
            ViewState["IsNewRecord"] = false;

            var incidentItem = (String)DataBinder.Eval(DataItem, PatientIncidentComponentTypeMetadata.ColumnNames.SRIncidentType);
            var itq = new AppStandardReferenceItemQuery();
            itq.Where
                (
                    itq.StandardReferenceID == AppEnum.StandardReference.IncidentType,
                    itq.ItemID == incidentItem
                );
            cboSRIncidentType.DataSource = itq.LoadDataTable();
            cboSRIncidentType.DataBind();
            cboSRIncidentType.SelectedValue = incidentItem;
            
            PopulateIncidentTypeComponentId(cboSRIncidentType.SelectedValue);
            cboComponentID.SelectedValue = (String)DataBinder.Eval(DataItem, PatientIncidentComponentTypeMetadata.ColumnNames.ComponentID);
            PopulateIncidentTypeSubComponentId(cboSRIncidentType.SelectedValue, cboComponentID.SelectedValue);
            cboSubComponentID.SelectedValue = (String)DataBinder.Eval(DataItem, PatientIncidentComponentTypeMetadata.ColumnNames.SubComponentID);
            txtSubComponentName.Text = (String)DataBinder.Eval(DataItem, PatientIncidentComponentTypeMetadata.ColumnNames.SubComponentName);
            txtModus.Text = (String)DataBinder.Eval(DataItem, PatientIncidentComponentTypeMetadata.ColumnNames.Modus);

            var typeItem = new IncidentTypeItem();
            if (typeItem.LoadByPrimaryKey(cboSRIncidentType.SelectedValue, cboComponentID.SelectedValue, cboSubComponentID.SelectedValue))
            {
                txtSubComponentName.Enabled = typeItem.IsAllowEdit ?? false;
                chkIsAllowEdit.Checked = typeItem.IsAllowEdit ?? false;
            }
            else
            {
                txtSubComponentName.Enabled = false;
                chkIsAllowEdit.Checked = false;
            }
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (PatientIncidentComponentTypeCollection)Session["collPatientIncidentComponentType" + Request.UserHostName];

                string itype = cboSRIncidentType.SelectedValue;
                string compId = cboComponentID.SelectedValue;
                string subCompId = cboSubComponentID.SelectedValue;
                bool isExist = false;

                foreach (PatientIncidentComponentType item in coll)
                {
                    if (item.SRIncidentType.Equals(itype) && item.ComponentID.Equals(compId) && item.SubComponentID.Equals(subCompId))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage =
                        string.Format("Incident Type : {0}, Component : {1}, Sub Component : {2} already exist", cboSRIncidentType.Text, cboComponentID.Text, cboSubComponentID.Text);
                }
            }
        }

        protected void cboSRIncidentType_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Value))
            {
                PopulateIncidentTypeComponentId(e.Value);
            }
            else
            {
                cboComponentID.Items.Clear();
                cboComponentID.SelectedValue = string.Empty;
                cboComponentID.Text = string.Empty;
            }

            cboSubComponentID.Items.Clear();
            cboSubComponentID.SelectedValue = string.Empty;
            cboSubComponentID.Text = string.Empty;
        }

        protected void cboSRIncidentType_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var query = new AppStandardReferenceItemQuery();
            query.Select
                (
                    query.ItemID,
                    query.ItemName
                );
            query.Where
                (
                    query.StandardReferenceID == AppEnum.StandardReference.IncidentType,
                    query.ItemName.Like(string.Format("{0}%", e.Text)),
                    query.IsActive == true
                );
            query.OrderBy(query.ItemID.Ascending);

            cboSRIncidentType.DataSource = query.LoadDataTable();
            cboSRIncidentType.DataBind();
        }

        protected void cboSRIncidentType_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void cboComponentID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Value))
            {
                PopulateIncidentTypeSubComponentId(cboSRIncidentType.SelectedValue, e.Value);
            }
            else
            {
                cboSubComponentID.Items.Clear();
                cboSubComponentID.SelectedValue = string.Empty;
                cboSubComponentID.Text = string.Empty;
            }
        }

        protected void cboSubComponentID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Value))
            {
                var typeItem = new IncidentTypeItem();
                if (typeItem.LoadByPrimaryKey(cboSRIncidentType.SelectedValue, cboComponentID.SelectedValue, e.Value))
                {
                    txtSubComponentName.Enabled = typeItem.IsAllowEdit ?? false;
                    txtSubComponentName.Text = string.Empty;
                    chkIsAllowEdit.Checked = typeItem.IsAllowEdit ?? false;
                }
            }
            else
            {
                txtSubComponentName.Text = string.Empty;
                txtSubComponentName.Enabled = false;
                chkIsAllowEdit.Checked = false;
            }
        }

        private void PopulateIncidentType()
        {
            cboSRIncidentType.Items.Clear();

            var query = new AppStandardReferenceItemQuery();
            query.Select
                (
                    query.ItemID,
                    query.ItemName
                );
            query.Where
                (
                    query.StandardReferenceID == AppEnum.StandardReference.IncidentType
                );
            query.OrderBy(query.ItemID.Ascending);

            DataTable dtb = query.LoadDataTable();

            cboSRIncidentType.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (DataRow row in dtb.Rows)
            {
                cboSRIncidentType.Items.Add(new RadComboBoxItem(row["ItemName"].ToString(), row["ItemID"].ToString()));
            }
        }

        private void PopulateIncidentTypeComponentId(string srIncidentType)
        {
            cboComponentID.Items.Clear();

            var query = new IncidentTypeQuery();
            query.Select
                (
                    query.ComponentID,
                    query.ComponentName
                );
            query.Where
                (
                    query.SRIncidentType == srIncidentType
                );
            query.OrderBy(query.ComponentID.Ascending);

            DataTable dtb = query.LoadDataTable();

            cboComponentID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (DataRow row in dtb.Rows)
            {
                cboComponentID.Items.Add(new RadComboBoxItem(row["ComponentName"].ToString(), row["ComponentID"].ToString()));
            }
        }

        private void PopulateIncidentTypeSubComponentId(string srIncidentType, string componentId)
        {
            cboSubComponentID.Items.Clear();

            var query = new IncidentTypeItemQuery();
            query.Select
                (
                    query.SubComponentID,
                    query.SubComponentName
                );
            query.Where
                (
                    query.SRIncidentType == srIncidentType, query.ComponentID == componentId
                );
            query.OrderBy(query.SubComponentID.Ascending);

            DataTable dtb = query.LoadDataTable();

            cboSubComponentID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (DataRow row in dtb.Rows)
            {
                cboSubComponentID.Items.Add(new RadComboBoxItem(row["SubComponentName"].ToString(), row["SubComponentID"].ToString()));
            }
        }

        #region Properties for return entry value

        public String SRIncidentType
        {
            get { return cboSRIncidentType.SelectedValue; }
        }

        public String IncidentType
        {
            get { return cboSRIncidentType.Text; }
        }

        public String ComponentID
        {
            get { return cboComponentID.SelectedValue; }
        }

        public String ComponentName
        {
            get { return cboComponentID.Text; }
        }

        public String SubComponentID
        {
            get { return cboSubComponentID.SelectedValue; }
        }

        public String SubComponent
        {
            get { return cboSubComponentID.Text; }
        }

        public String SubComponentName
        {
            get { return txtSubComponentName.Text; }
        }

        public String Modus
        {
            get { return txtModus.Text; }
        }

        public bool IsAllowEdit
        {
            get { return chkIsAllowEdit.Checked; }
        }
        #endregion
    }
}