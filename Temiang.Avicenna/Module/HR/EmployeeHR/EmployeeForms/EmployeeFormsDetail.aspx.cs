using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;

using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.HR.EmployeeHR.EmployeeForms
{
    public partial class EmployeeFormsDetail : BasePageDialogEntry
    {
        private string getPageID
        {
            get
            {
                return string.IsNullOrEmpty(Request.QueryString["pid"]) ? string.Empty : Request.QueryString["pid"];
            }
        }
        private string personID
        {
            get
            {
                return string.IsNullOrEmpty(Request.QueryString["eid"]) ? "-1" : Request.QueryString["eid"];
            }
        }
        private string supervisorId
        {
            get
            {
                return string.IsNullOrEmpty(Request.QueryString["svr"]) ? "-1" : Request.QueryString["svr"];
            }
        }
        private string managerId
        {
            get
            {
                return string.IsNullOrEmpty(Request.QueryString["mng"]) ? "-1" : Request.QueryString["mng"];
            }
        }


        private AppAutoNumberLast _autoNumber;

        protected void Page_Init(object sender, EventArgs e)
        {
            switch (getPageID)
            {
                case "ew":
                    ProgramID = AppConstant.Program.EmployeeWorkingInfo;
                    break;
                case "gen":
                    ProgramID = AppConstant.Program.EmployeeLogbook;
                    break;
                case "c01":
                    ProgramID = AppConstant.Program.EmployeeLogbookMedicalCommitte;
                    break;
                case "c02":
                    ProgramID = AppConstant.Program.EmployeeLogbookNursingCommitte;
                    break;
                case "c03":
                    ProgramID = AppConstant.Program.EmployeeLogbookKtkl;
                    break;
            }

            // Menu hardcode
            IsSingleRecordMode = true;

            ToolBar.ApprovalUnApprovalVisible = false;
            ToolBar.VoidUnVoidVisible = false;
            ToolBar.PrintVisible = false;
            ToolBar.EditVisible = (personID.ToInt() == AppSession.UserLogin.PersonID || supervisorId.ToInt() == AppSession.UserLogin.PersonID || managerId.ToInt() == AppSession.UserLogin.PersonID);

            if (!IsPostBack)
            {
                var emps = new VwEmployeeTable();
                emps.Query.Where(emps.Query.PersonID == personID.ToInt());
                if (emps.Query.Load())
                {
                    this.Title = "Forms of : " + emps.EmployeeName + " [" + emps.EmployeeNumber + "]";
                }

                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                    txtTransactionNo.Text = Request.QueryString["id"];
                else
                    txtTransactionNo.Text = GetNewNumber();
            }
        }


        protected override void OnPopulateEntryControl(ValidateArgs args)
        {
            var form = new EmployeeForm();
            form.LoadByPrimaryKey(txtTransactionNo.Text);
            PopulateEntryControl(form);
        }
        protected void PopulateEntryControl(esEntity entity)
        {
            var result = (EmployeeForm)entity;

            txtTransactionNo.Text = result.TransactionNo;

            if (result.TransactionDate.HasValue)
                txtTransactionDate.SelectedDate = result.TransactionDate;
            else
                txtTransactionDate.SelectedDate = (new DateTime()).NowAtSqlServer();

            if (!string.IsNullOrEmpty(result.TransactionNo))
            {
                var emps = new VwEmployeeTableQuery();
                emps.Where(emps.PersonID == Convert.ToInt32(result.PersonID));
                cboPersonID.DataSource = emps.LoadDataTable();
                cboPersonID.DataBind();
                cboPersonID.SelectedValue = result.PersonID.ToString();

                var template = new EmployeeFormTemplateQuery();
                template.Where(template.TemplateID == Convert.ToInt32(result.TemplateID));
                cboTemplateID.DataSource = template.LoadDataTable();
                cboTemplateID.DataBind();
                cboTemplateID.SelectedValue = result.TemplateID.ToString();
            }
            else
            {
                var emps = new VwEmployeeTableQuery();
                emps.Where(emps.PersonID == Convert.ToInt32(personID));
                cboPersonID.DataSource = emps.LoadDataTable();
                cboPersonID.DataBind();
                cboPersonID.SelectedValue = personID.ToString();

                cboTemplateID.Items.Clear();
                cboTemplateID.SelectedValue = string.Empty;
                cboTemplateID.Text = string.Empty;
            }

            txtNotes.Text = result.Notes;
            txtResult.Content = result.Result;
        }
        protected override void OnMenuNewClick()
        {
            var ent = new EmployeeForm();
            PopulateEntryControl(ent);
            txtTransactionNo.Text = GetNewNumber();
            txtTransactionDate.SelectedDate = (new DateTime()).NowAtSqlServer();
        }
        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            if (string.IsNullOrEmpty(txtResult.Text))
            {
                args.MessageText = "Result required.";
                args.IsCancel = true;
                return;
            }

            var entity = new EmployeeForm();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //rowUploadFile.Visible = newVal == (AppEnum.DataMode.New);
        }
        protected override void OnMenuEditClick()
        {
        }
        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            if (string.IsNullOrEmpty(txtResult.Text))
            {
                args.MessageText = "Result required.";
                args.IsCancel = true;
                return;
            }

            var entity = new EmployeeForm();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }
        }

        private void SetEntityValue(EmployeeForm entity)
        {
            if (DataModeCurrent == AppEnum.DataMode.New)
            {
                txtTransactionNo.Text = GetNewNumber();
                // save autonumber immediately to decrease time gap between create and save
                _autoNumber.Save();
            }

            entity.TransactionNo = txtTransactionNo.Text;
            entity.TransactionDate = txtTransactionDate.SelectedDate;
            entity.PersonID = cboPersonID.SelectedValue.ToInt();
            entity.TemplateID = Convert.ToInt32(cboTemplateID.SelectedValue);
            entity.Notes = txtNotes.Text;
            entity.Result = txtResult.Content;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }
        }

        private static void SaveEntity(EmployeeForm entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var ent = new EmployeeForm();
            if (ent.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                ent.MarkAsDeleted();
                ent.Save();
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }
        }

        #region ComboBox 
        protected void cboPersonID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["EmployeeNumber"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["EmployeeName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PersonID"].ToString();
        }

        protected void cboPersonID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var vw = new VwEmployeeTableQuery();
            vw.es.Top = 20;
            vw.Where(vw.SREmployeeStatus == "1", vw.EmployeeName.Like(searchTextContain));

            (o as RadComboBox).DataSource = vw.LoadDataTable();
            (o as RadComboBox).DataBind();
        }
        protected void cboTemplateID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            cboTemplateID.Items.Clear();
            ComboBox.EmployeeFormTemplateItemsRequested((RadComboBox)sender, e.Text);
        }

        protected void cboTemplateID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.EmployeeFormTemplateItemDataBound(e);
        }

        protected void cboTemplateID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(cboTemplateID.SelectedValue))
            {
                txtResult.Content = string.Empty;
                return;
            }

            var template = new EmployeeFormTemplate();
            if (template.LoadByPrimaryKey(Convert.ToInt32(cboTemplateID.SelectedValue)))
            {
                txtResult.Content = template.Result;
            }
        }

        #endregion

        private string GetNewNumber()
        {
            _autoNumber = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.EmployeeFormNo);

            return _autoNumber.LastCompleteNumber;
        }
    }
}