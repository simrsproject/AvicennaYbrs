using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.HR.Master.HRBase
{
    public partial class EducationLevelItemDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            StandardReference.InitializeIncludeSpace(cboSREducationGroup, AppEnum.StandardReference.EducationGroup);

            //var coll = new RlMasterReportItemCollection();
            //coll.Query.Where(coll.Query.RlMasterReportID == Convert.ToInt32(3), coll.Query.IsActive == true);
            //coll.LoadAll();

            //cboRlReportID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            //foreach (RlMasterReportItem entity in coll)
            //{
            //    cboRlReportID.Items.Add(new RadComboBoxItem(entity.RlMasterReportItemCode + " - " + entity.RlMasterReportItemName, entity.RlMasterReportItemID.ToString()));
            //}


            var query = new RlMasterReportItemQuery("a");
            query.Where(query.RlMasterReportID == Convert.ToInt32(3), query.IsActive == true);
            query.Where(@"<RIGHT(a.RlMasterReportItemNo, 2) <> '00'>");

            DataTable dtb = query.LoadDataTable();

            cboRlReportID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (DataRow row in dtb.Rows)
            {
                cboRlReportID.Items.Add(new RadComboBoxItem(row["RlMasterReportItemCode"].ToString() + " - " + row["RlMasterReportItemName"].ToString(), row["RlMasterReportItemID"].ToString()));
            }

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                cboRlReportID.SelectedValue = string.Empty;
                cboRlReportID.Text = string.Empty;
                cboSREducationGroup.SelectedValue = string.Empty;
                cboSREducationGroup.SelectedValue = string.Empty;
                chkIsActive.Checked = true;

                return;
            }
            ViewState["IsNewRecord"] = false;

            txtEducationLevelID.Text = (String)DataBinder.Eval(DataItem, AppStandardReferenceItemMetadata.ColumnNames.ItemID);
            txtEducationLevelName.Text = (String)DataBinder.Eval(DataItem, AppStandardReferenceItemMetadata.ColumnNames.ItemName);
            cboSREducationGroup.SelectedValue = (String)DataBinder.Eval(DataItem, AppStandardReferenceItemMetadata.ColumnNames.CustomField);
            cboRlReportID.SelectedValue = (String)DataBinder.Eval(DataItem, AppStandardReferenceItemMetadata.ColumnNames.ReferenceID);
            chkIsActive.Checked = (Boolean)DataBinder.Eval(DataItem, AppStandardReferenceItemMetadata.ColumnNames.IsActive);
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (ViewState["IsNewRecord"].Equals(true))
            {
                AppStandardReferenceItemCollection coll = (AppStandardReferenceItemCollection)Session["collEducationLevel"];

                bool isExist = false;
                foreach (var i in coll)
                {
                    if (i.ItemID.Equals(txtEducationLevelID.Text))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Code {0} has exist.", txtEducationLevelID.Text);
                }
            }
        }

        #region Properties for return entry value
        public String ItemID
        {
            get { return txtEducationLevelID.Text; }
        }
        public String ItemName
        {
            get { return txtEducationLevelName.Text; }
        }
        public String ReferenceID
        {
            get { return cboRlReportID.SelectedValue; }
        }

        public String CustomField
        {
            get { return cboSREducationGroup.SelectedValue; }
        }

        public bool IsActive
        {
            get { return chkIsActive.Checked; }
        }

        #endregion

        #region Method & Event TextChanged

        #endregion
    }
}