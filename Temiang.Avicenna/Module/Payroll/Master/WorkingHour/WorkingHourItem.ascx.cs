using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Payroll.Master
{
    public partial class WorkingHourItem : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            var orgs = new OrganizationUnitCollection();
            orgs.Query.es.Distinct = true;
            orgs.Query.Where(orgs.Query.SROrganizationLevel == "0", orgs.Query.IsActive == true);
            orgs.Query.OrderBy(orgs.Query.OrganizationUnitName.Ascending);
            orgs.Query.Load();

            cboOrganizationUnit.Items.Add(new Telerik.Web.UI.RadComboBoxItem(string.Empty, string.Empty));
            foreach (var org in orgs)
            {
                cboOrganizationUnit.Items.Add(new Telerik.Web.UI.RadComboBoxItem(org.OrganizationUnitName, org.OrganizationUnitID.ToString()));
            }

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                return;
            }
            ViewState["IsNewRecord"] = false;

            cboOrganizationUnit.SelectedValue = (String)DataBinder.Eval(DataItem, WorkingHourOrganizationUnitMetadata.ColumnNames.OrganizationUnitID);
        }

        public int OrganizationUnitID
        {
            get { return cboOrganizationUnit.SelectedValue.ToInt(); }
        }

        public string OrganizationUnitName
        {
            get { return cboOrganizationUnit.Text; }
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (WorkingHourOrganizationUnitCollection)Session["collWorkingHourOrganizationUnit"];

                int itemID = cboOrganizationUnit.SelectedValue.ToInt();
                bool isExist = false;
                foreach (var item in coll)
                {
                    if (item.OrganizationUnitID.Equals(itemID))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Organization Unit : {0} already exist", cboOrganizationUnit.Text);
                }
            }
        }
    }
}