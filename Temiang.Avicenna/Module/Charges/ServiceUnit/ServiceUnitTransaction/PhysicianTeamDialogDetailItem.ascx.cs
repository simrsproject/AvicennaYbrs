using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
namespace Temiang.Avicenna.Module.Charges
{
    public partial class PhysicianTeamDialogDetailItem : BaseUserControl
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
                cboPhysicianID.Enabled = true;
                txtStartDate.SelectedDate = DateTime.Now.Date;
                return;
            }
            ViewState["IsNewRecord"] = false;
            cboPhysicianID.Enabled = false;
            cboPhysicianID.Items.Clear();
            cboPhysicianID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            var parColl = new ParamedicCollection();
            parColl.Query.Where(parColl.Query.IsActive == true);
            parColl.LoadAll();
            foreach (Paramedic par in parColl)
            {
                cboPhysicianID.Items.Add(new RadComboBoxItem(par.ParamedicName, par.ParamedicID));
            }

            cboPhysicianID.SelectedValue = (String)DataBinder.Eval(DataItem, ParamedicTeamMetadata.ColumnNames.ParamedicID);
            txtStartDate.SelectedDate = (DateTime)DataBinder.Eval(DataItem, ParamedicTeamMetadata.ColumnNames.StartDate);
            txtNotes.Text = (String)DataBinder.Eval(DataItem, ParamedicTeamMetadata.ColumnNames.Notes);
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            var coll = (ParamedicTeamCollection)Session["collParamedicTeam" + Request.UserHostName];

            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                string regNo = ((RadTextBox)Helper.FindControlRecursive(Page, "txtRegistrationNo")).Text;
                string parId = cboPhysicianID.SelectedValue;
                DateTime sDate = txtStartDate.SelectedDate ?? DateTime.Now;

                bool isExist = false;
                foreach (ParamedicTeam item in coll)
                {
                    if (item.RegistrationNo.Equals(regNo) && item.ParamedicID.Equals(parId) && item.StartDate.Equals(sDate))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("ID: {0} has exist", parId);
                    return;
                }
            }
        }

        #region Properties for return entry value

        public String ParamedicID
        {
            get { return cboPhysicianID.SelectedValue; }
        }

        public String SRParamedicTeamStatus
        {
            get { return "99"; }
        }

        public DateTime? StartDate
        {
            get { return txtStartDate.SelectedDate; }
        }

        public DateTime? EndDate
        {
            get { return txtStartDate.SelectedDate; }
        }

        public String Notes
        {
            get { return txtNotes.Text; }
        }

        #endregion

        protected void cboPhysicianID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ParamedicName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ParamedicID"].ToString();
        }

        protected void cboPhysicianID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ParamedicQuery("a");
            query.Select
                (
                    query.ParamedicID,
                    query.ParamedicName
                );
            query.OrderBy(query.ParamedicName.Ascending);
            query.Where
                (
                    query.ParamedicName.Like(searchTextContain), query.IsActive == true
                );

            cboPhysicianID.DataSource = query.LoadDataTable();
            cboPhysicianID.DataBind();
        }
    }
}