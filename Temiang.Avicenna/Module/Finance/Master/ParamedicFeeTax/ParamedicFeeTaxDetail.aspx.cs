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
    public partial class ParamedicFeeTaxDetail : BasePageDetail
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "ParamedicFeeTaxSearch.aspx";
            UrlPageList = "ParamedicFeeTaxList.aspx";

            ProgramID = AppConstant.Program.ParamedicFeeTax;
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new ParamedicFeeProgressiveTax());

            ViewState["CounterID"] = 0;
            txtMinAmount.Value = 0;
            txtMaxAmount.Value = 0;
            txtPercentage.Value = 0;
            txtPercentageNonNpwp.Value = 0;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new ParamedicFeeProgressiveTax();
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
            var query = new ParamedicFeeProgressiveTaxQuery();
            query.Where(
                query.MinAmount == txtMinAmount.Value,
                query.MaxAmount == txtMaxAmount.Value
                );

            var entity = new ParamedicFeeProgressiveTax();
            if (entity.Load(query))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }

            entity = new ParamedicFeeProgressiveTax();
            entity.AddNew();

            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new ParamedicFeeProgressiveTax();
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
            auditLogFilter.TableName = "ParamedicFeeProgressiveTax";
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new ParamedicFeeProgressiveTax();
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
            var fee = (ParamedicFeeProgressiveTax)entity;

            ViewState["CounterID"] = fee.CounterID;
            txtMinAmount.Value = Convert.ToDouble(fee.MinAmount);
            txtMaxAmount.Value = Convert.ToDouble(fee.MaxAmount);
            txtPercentage.Value = Convert.ToDouble(fee.Percentage);
            txtPercentageNonNpwp.Value = Convert.ToDouble(fee.PercentageNonNpwp);
        }

        private void SetEntityValue(ParamedicFeeProgressiveTax entity)
        {
            entity.MinAmount = Convert.ToDecimal(txtMinAmount.Value);
            entity.MaxAmount = Convert.ToDecimal(txtMaxAmount.Value);
            entity.Percentage = Convert.ToDecimal(txtPercentage.Value);
            entity.PercentageNonNpwp = Convert.ToDecimal(txtPercentageNonNpwp.Value);

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(ParamedicFeeProgressiveTax entity)
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
            var que = new ParamedicFeeProgressiveTaxQuery();
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

            var entity = new ParamedicFeeProgressiveTax();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }
    }
}
