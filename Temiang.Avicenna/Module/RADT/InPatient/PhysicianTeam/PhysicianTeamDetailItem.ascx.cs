using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.RADT.InPatient
{
    public partial class PhysicianTeamDetailItem : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            StandardReference.InitializeIncludeSpace(cboSRParamedicTeamStatus, AppEnum.StandardReference.ParamedicTeamStatus);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;
                cboPhysicianID.Enabled = true;
                hdnSourceType.Value = string.Empty;
                return;
            }
            ViewState["IsNewRecord"] = false;
            cboPhysicianID.Enabled = false;
            cboPhysicianID.Items.Clear();
            var parId = (String) DataBinder.Eval(DataItem, ParamedicTeamMetadata.ColumnNames.ParamedicID);
            cboPhysicianID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            var parColl = new ParamedicCollection();
            parColl.Query.Where(parColl.Query.ParamedicID == parId);
            parColl.LoadAll();
            foreach (Paramedic par in parColl)
            {
                cboPhysicianID.Items.Add(new RadComboBoxItem(par.ParamedicName, par.ParamedicID));
            }

            cboPhysicianID.SelectedValue = (String)DataBinder.Eval(DataItem, ParamedicTeamMetadata.ColumnNames.ParamedicID);
            cboSRParamedicTeamStatus.SelectedValue = (String)DataBinder.Eval(DataItem, ParamedicTeamMetadata.ColumnNames.SRParamedicTeamStatus);
            txtStartDateTime.SelectedDate = (DateTime)DataBinder.Eval(DataItem, ParamedicTeamMetadata.ColumnNames.StartDate);
            object endDate = DataBinder.Eval(DataItem, ParamedicTeamMetadata.ColumnNames.EndDate);
            if (endDate != null)
                txtEndDateTime.SelectedDate = (DateTime)DataBinder.Eval(DataItem, ParamedicTeamMetadata.ColumnNames.EndDate);
            else
                txtEndDateTime.Clear();
            
            txtNotes.Text = (String)DataBinder.Eval(DataItem, ParamedicTeamMetadata.ColumnNames.Notes);
            hdnSourceType.Value = (String)DataBinder.Eval(DataItem, ParamedicTeamMetadata.ColumnNames.SourceType);
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            var coll = (ParamedicTeamCollection)Session["collParamedicTeam"];

            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                string regNo = ((RadTextBox)Helper.FindControlRecursive(Page, "txtRegistrationNo")).Text;

                bool isMainExist = false;
                if (cboSRParamedicTeamStatus.SelectedValue == AppSession.Parameter.ParamedicTeamStatusMain)
                {
                    foreach (var item in coll.Where(item => item.SRParamedicTeamStatus == AppSession.Parameter.ParamedicTeamStatusMain && item.StartDate <= (new DateTime()).NowAtSqlServer() && (item.EndDate == null || item.EndDate > (new DateTime()).NowAtSqlServer())))
                    {
                        isMainExist = true;
                    }
                }
                if (isMainExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("DPJP in active period has exist");
                    return;
                }

                string parId = cboPhysicianID.SelectedValue;
                string parName = cboPhysicianID.Text;

                DateTime sDate = txtStartDateTime.SelectedDate ?? (new DateTime()).NowAtSqlServer();
                DateTime eDate = txtEndDateTime.SelectedDate ?? (new DateTime()).NowAtSqlServer().AddMonths(1);

                bool isExist = false;
                foreach (ParamedicTeam item in coll)
                {
                    if (item.RegistrationNo.Equals(regNo) && item.ParamedicID.Equals(parId))
                    {
                        DateTime eDateExists = item.EndDate ?? eDate;
                        //tgl akhir input = tgl akhir exist, kondisi tgl akhir null
                        if (eDate == eDateExists) 
                        {
                            isExist = true;
                            break;
                        }
                        //tgl awal input <= tgl akhir exist 
                        if (sDate <= eDateExists)
                        {
                            isExist = true;
                            break;
                        }
                        //tgl akhir input <= tgl akhir exist
                        if (eDate <= eDateExists)
                        {
                            isExist = true;
                            break;
                        }
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("{0} in active period has exist", parName);
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
            get { return cboSRParamedicTeamStatus.SelectedValue; }
        }

        public DateTime? StartDate
        {
            get { return  txtStartDateTime.SelectedDate; }
        }

        public DateTime? EndDate
        {
            get { return txtEndDateTime.SelectedDate; }
        }

        public String Notes
        {
            get { return txtNotes.Text; }
        }

        public String SourceType
        {
            get { return hdnSourceType.Value; }
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