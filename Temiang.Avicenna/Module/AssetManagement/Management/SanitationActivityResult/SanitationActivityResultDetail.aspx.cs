using System;
using System.Data;
using System.Linq;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.AssetManagement.Management
{
    public partial class SanitationActivityResultDetail : BasePageDetail
    {
        #region Page Event & Initialize
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Url Search & List
            UrlPageSearch = "SanitationActivityResultSearch.aspx";
            UrlPageList = "SanitationActivityResultList.aspx";

            ProgramID = AppConstant.Program.SanitationActivityResult;

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRWorkType, AppEnum.StandardReference.WorkType);
                
                var awo = new AssetWorkOrder();
                awo.LoadByPrimaryKey(Request.QueryString["ono"]);
                txtOrderNo.Text = awo.OrderNo;
                txtOrderDate.SelectedDate = awo.OrderDate;

                var unit = new ServiceUnit();
                if (unit.LoadByPrimaryKey(awo.FromServiceUnitID))
                    txtFromServiceUnitName.Text = unit.ServiceUnitName;
                else 
                    txtFromServiceUnitName.Text = string.Empty;

                txtProblemDescription.Text = awo.ProblemDescription;

                cboSRWorkType.SelectedValue = awo.SRWorkType;
                ComboBox.PopulateWorkTradeItemList(cboSRWorkTradeItem, awo.SRWorkTrade, false);
                cboSRWorkTradeItem.SelectedValue = awo.SRWorkTradeItem;
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

            ToolBarMenuSearch.Enabled = false;
            ToolBarMenuAdd.Enabled = false;
            ToolBarMenuDelete.Enabled = false;
        }
        #endregion

        #region Toolbar Menu Event
        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            args.IsCancel = true;

            var entity = new SanitationActivityResult();
            entity.LoadByPrimaryKey(txtOrderNo.Text);
            entity.MarkAsDeleted();
            SaveEntity(entity);

            OnPopulateEntryControl(new string[1] { Request.QueryString["ono"]});
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            if (string.IsNullOrEmpty(txtResult.Text))
            {
                args.MessageText = "Result required.";
                args.IsCancel = true;
                return;
            }

            var entity = new SanitationActivityResult();
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

            var entity = new SanitationActivityResult();
            if (!entity.LoadByPrimaryKey(txtOrderNo.Text))
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
            printJobParameters.AddNew("p_OrderNo", txtOrderNo.Text);
        }

        protected override void OnMenuAuditLogClick(AuditLogFilter auditLogFilter)
        {
            auditLogFilter.PrimaryKeyData = String.Format("OrderNo='{0}'", txtOrderNo.Text.Trim());
            auditLogFilter.TableName = "SanitationActivityResult";
        }
        #endregion

        #region ToolBar Menu Support
        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            var entity = new SanitationActivityResult();
            if (entity.LoadByPrimaryKey(txtOrderNo.Text))
            {
                OnPopulateEntryControl(entity);
            }
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new SanitationActivityResult();
            if (parameters.Length > 0)
            {
                String orderNo = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(orderNo);
            }
            else
            {
                entity.LoadByPrimaryKey(txtOrderNo.Text);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var result = (SanitationActivityResult)entity;

            if (!string.IsNullOrEmpty(result.OrderNo))
                txtOrderNo.Text = result.OrderNo;
            
            if (result.ResultDateTime.HasValue)
            {
                txtEntryDate.SelectedDate = result.ResultDateTime;
                txtEntryTime.Text = result.ResultDateTime.Value.ToString("HH:mm");
            }
            else
            {
                txtEntryDate.SelectedDate = (new DateTime()).NowAtSqlServer();
                txtEntryTime.Text = (new DateTime()).NowAtSqlServer().ToString("HH:mm");
            }
            
            var awo = new AssetWorkOrder();
            if (awo.LoadByPrimaryKey(txtOrderNo.Text))
            {
                txtOrderDate.SelectedDate = awo.OrderDate;

                var unit = new ServiceUnit();
                if (unit.LoadByPrimaryKey(awo.FromServiceUnitID))
                    txtFromServiceUnitName.Text = unit.ServiceUnitName;
                else
                    txtFromServiceUnitName.Text = string.Empty;

                txtProblemDescription.Text = awo.ProblemDescription;

                cboSRWorkType.SelectedValue = awo.SRWorkType;
                ComboBox.PopulateWorkTradeItemList(cboSRWorkTradeItem, awo.SRWorkTrade, false);
                cboSRWorkTradeItem.SelectedValue = awo.SRWorkTradeItem;
            }

            txtResult.Content = result.Result;
            txtSummary.Content = result.Summary;
            txtSuggest.Content = result.Suggest;

            cboSanitationActivityResultID.DataSource = null;
            cboSanitationActivityResultID.DataBind();
            cboSanitationActivityResultID.SelectedValue = string.Empty;
            cboSanitationActivityResultID.Text = string.Empty;
        }
        #endregion

        #region Private Method Standard
        private void SetEntityValue(SanitationActivityResult entity)
        {
            entity.OrderNo = txtOrderNo.Text;
            entity.ResultDateTime = DateTime.Parse(txtEntryDate.SelectedDate.Value.ToShortDateString() + " " + txtEntryTime.TextWithLiterals);

            entity.Result = txtResult.Content;
            entity.Summary = txtSummary.Content;
            entity.Suggest = txtSuggest.Content;
            
            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }
        }

        private static void SaveEntity(SanitationActivityResult entity)
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
            var que = new SanitationActivityResultQuery("a");

            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.OrderNo > txtOrderNo.Text);
                que.OrderBy(que.OrderNo.Ascending);
            }
            else
            {
                que.Where(que.OrderNo < txtOrderNo.Text);
                que.OrderBy(que.OrderNo.Descending);
            }

            var entity = new SanitationActivityResult();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }
        #endregion

        #region ComboBox SanitationActivityResultID
        protected void cboSanitationActivityResultID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.SanitationActivityResultTemplateItemsRequested((RadComboBox)sender, cboSRWorkTradeItem.SelectedValue, e.Text);
        }

        protected void cboSanitationActivityResultID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.SanitationActivityResultTemplateItemDataBound(e);
        }

        protected void cboSanitationActivityResultID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (cboSanitationActivityResultID.SelectedValue == string.Empty) return;

            var resultTemplate = new SanitationActivityResultTemplate();
            if (resultTemplate.LoadByPrimaryKey(Convert.ToInt32(cboSanitationActivityResultID.SelectedValue)))
            {
                txtResult.Content = resultTemplate.Result;
                txtSuggest.Content = resultTemplate.Suggest;
                txtSummary.Content = resultTemplate.Summary;
            }
        }

        #endregion
    }
}