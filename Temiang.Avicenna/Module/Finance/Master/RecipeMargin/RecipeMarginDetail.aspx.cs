using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class RecipeMarginDetail : BasePageDetail
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "RecipeMarginSearch.aspx";
            UrlPageList = "RecipeMarginList.aspx";

            ProgramID = AppConstant.Program.RecipeMargin;
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new RecipeMarginValue());

            ViewState["CounterID"] = 0;
            txtStartingValue.Value = 0;
            txtEndingValue.Value = 0;
            txtRecipeAmount.Value = 0;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new RecipeMarginValue();
            entity.LoadByPrimaryKey((int)ViewState["CounterID"]);
            entity.MarkAsDeleted();

            using (var trans = new esTransactionScope())
            {
                entity.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var query = new RecipeMarginValueQuery();
            query.Where(
                query.StartingValue == txtStartingValue.Value,
                query.EndingValue == txtEndingValue.Value
                );

            var entity = new RecipeMarginValue();
            if (entity.Load(query))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }

            entity = new RecipeMarginValue();
            entity.AddNew();

            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new RecipeMarginValue();
            if (entity.LoadByPrimaryKey((int)ViewState["CounterID"]))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
        }

        protected override void OnMenuMoveNextClick(ValidateArgs args)
        {
            MoveRecord(true);
        }

        protected override void OnMenuMovePrevClick(ValidateArgs args)
        {
            MoveRecord(false);
        }

        protected override void OnMenuAuditLogClick(AuditLogFilter auditLogFilter)
        {
            auditLogFilter.PrimaryKeyData = string.Format("CounterID='{0}'", ViewState["CounterID"].ToString());
            auditLogFilter.TableName = "RecipeMarginValue";
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new RecipeMarginValue();
            if (parameters.Length > 0)
            {
                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(int.Parse(parameters[0]));
            }
            else
                entity.LoadByPrimaryKey((int)ViewState["CounterID"]);

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var mgn = (RecipeMarginValue)entity;

            ViewState["CounterID"] = mgn.CounterID;
            txtStartingValue.Value = Convert.ToDouble(mgn.StartingValue);
            txtEndingValue.Value = Convert.ToDouble(mgn.EndingValue);
            txtRecipeAmount.Value = Convert.ToDouble(mgn.RecipeAmount);
        }

        private void SetEntityValue(RecipeMarginValue entity)
        {
            entity.StartingValue = Convert.ToDecimal(txtStartingValue.Value);
            entity.EndingValue = Convert.ToDecimal(txtEndingValue.Value);
            entity.RecipeAmount = Convert.ToDecimal(txtRecipeAmount.Value);

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(RecipeMarginValue entity)
        {
            using (var trans = new esTransactionScope())
            {
                entity.Save();

                ViewState["CounterID"] = entity.CounterID;
                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new RecipeMarginValueQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.CounterID > (int)ViewState["CounterID"]);
                que.OrderBy(que.CounterID.Ascending);
            }
            else
            {
                que.Where(que.CounterID < (int)ViewState["CounterID"]);
                que.OrderBy(que.CounterID.Descending);
            }

            var entity = new RecipeMarginValue();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }
    }
}
