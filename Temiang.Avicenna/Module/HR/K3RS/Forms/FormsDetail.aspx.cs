using System;
using System.Data;
using System.Linq;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.HR.K3RS
{
    public partial class FormsDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;

        #region Page Event & Initialize
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Url Search & List
            UrlPageSearch = "FormsSearch.aspx";
            UrlPageList = "FormsList.aspx";

            ProgramID = AppConstant.Program.K3RS_Form;

            this.WindowSearch.Height = 400;

            if (!IsPostBack)
            {
            }
        }

        //protected override void OnLoad(EventArgs e)
        //{
        //    base.OnLoad(e);
        //}
        #endregion

        #region Override Method & Function
        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            ToolBarMenuDelete.Enabled = false;
        }
        #endregion

        #region Toolbar Menu Event
        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new K3rsForm());

            txtTransactionNo.Text = GetNewNumber();
            txtTransactionDate.SelectedDate = (new DateTime()).NowAtSqlServer();
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            args.IsCancel = true;

            var entity = new K3rsForm();
            entity.LoadByPrimaryKey(txtTransactionNo.Text);
            entity.MarkAsDeleted();
            SaveEntity(entity);

            OnPopulateEntryControl(new string[1] { Request.QueryString["ono"] });
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            if (string.IsNullOrEmpty(txtResult.Text))
            {
                args.MessageText = "Result required.";
                args.IsCancel = true;
                return;
            }

            var entity = new K3rsForm();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            if (string.IsNullOrEmpty(txtResult.Text))
            {
                args.MessageText = "Result required.";
                args.IsCancel = true;
                return;
            }

            var entity = new K3rsForm();
            if (!entity.LoadByPrimaryKey(txtTransactionNo.Text))
                entity.AddNew();

            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuMoveNextClick(ValidateArgs args)
        {
            MoveRecord(true);
        }

        protected override void OnMenuMovePrevClick(ValidateArgs args)
        {
            MoveRecord(false);
        }

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            printJobParameters.AddNew("p_TransactionNo", txtTransactionNo.Text);
        }

        protected override void OnMenuAuditLogClick(AuditLogFilter auditLogFilter)
        {
            auditLogFilter.PrimaryKeyData = String.Format("TransactionNo='{0}'", txtTransactionNo.Text.Trim());
            auditLogFilter.TableName = "K3rsForm";
        }
        #endregion

        #region ToolBar Menu Support
        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new K3rsForm();
            if (parameters.Length > 0)
            {
                String orderNo = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(orderNo);
            }
            else
            {
                entity.LoadByPrimaryKey(txtTransactionNo.Text);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var result = (K3rsForm)entity;

            txtTransactionNo.Text = result.TransactionNo;

            if (result.TransactionDate.HasValue)
                txtTransactionDate.SelectedDate = result.TransactionDate;
            else
                txtTransactionDate.SelectedDate = (new DateTime()).NowAtSqlServer();

            if (!string.IsNullOrEmpty(result.TransactionNo))
            {
                var template = new K3rsFormTemplateQuery();
                template.Where(template.TemplateID == Convert.ToInt32(result.TemplateID));
                cboTemplateID.DataSource = template.LoadDataTable();
                cboTemplateID.DataBind();
                cboTemplateID.SelectedValue = result.TemplateID.ToString();
            }
            else
            {
                cboTemplateID.Items.Clear();
                cboTemplateID.SelectedValue = string.Empty;
                cboTemplateID.Text = string.Empty;
            }

            txtNotes.Text = result.Notes;
            txtResult.Content = result.Result;

        }
        #endregion

        #region Private Method Standard
        private void SetEntityValue(K3rsForm entity)
        {
            if (DataModeCurrent == AppEnum.DataMode.New)
            {
                txtTransactionNo.Text = GetNewNumber();
                // save autonumber immediately to decrease time gap between create and save
                _autoNumber.Save();
            }

            entity.TransactionNo = txtTransactionNo.Text;
            entity.TransactionDate = txtTransactionDate.SelectedDate;
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

        private static void SaveEntity(K3rsForm entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new K3rsFormQuery("a");

            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.TransactionNo > txtTransactionNo.Text);
                que.OrderBy(que.TransactionNo.Ascending);
            }
            else
            {
                que.Where(que.TransactionNo < txtTransactionNo.Text);
                que.OrderBy(que.TransactionNo.Descending);
            }

            var entity = new K3rsForm();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }
        #endregion

        #region ComboBox cboTemplateID
        protected void cboTemplateID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            cboTemplateID.Items.Clear();
            ComboBox.K3rsFormTemplateItemsRequested((RadComboBox)sender, e.Text);
        }

        protected void cboTemplateID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.K3rsFormTemplateItemDataBound(e);
        }

        protected void cboTemplateID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(cboTemplateID.SelectedValue))
            {
                txtResult.Content = string.Empty;
                return;
            }

            var template = new K3rsFormTemplate();
            if (template.LoadByPrimaryKey(Convert.ToInt32(cboTemplateID.SelectedValue)))
            {
                txtResult.Content = template.Result;
            }
        }

        #endregion

        private string GetNewNumber()
        {
            _autoNumber = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.K3rsFormNo);

            return _autoNumber.LastCompleteNumber;
        }
    }
}