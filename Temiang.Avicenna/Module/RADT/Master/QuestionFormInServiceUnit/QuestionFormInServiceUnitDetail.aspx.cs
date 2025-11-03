using System;
using System.Data;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class QuestionFormInServiceUnitDetail : BasePageDetail
    {

        private void MoveRecord(bool isNextRecord)
        {
            var que = new ServiceUnitQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.ServiceUnitID > txtServiceUnitID.Text);
                que.OrderBy(que.ServiceUnitID.Ascending);
            }
            else
            {
                que.Where(que.ServiceUnitID < txtServiceUnitID.Text);
                que.OrderBy(que.ServiceUnitID.Descending);
            }
            var entity = new ServiceUnit();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #region Override Method & Function

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            ServiceUnit entity = new ServiceUnit();
            if (parameters.Length > 0)
            {
                String classID = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(classID);
            }
            else
            {
                entity.LoadByPrimaryKey(txtServiceUnitID.Text);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var questionGroup = (ServiceUnit)entity;
            txtServiceUnitID.Text = questionGroup.ServiceUnitID;
            txtServiceUnitName.Text = questionGroup.ServiceUnitName;
            //chkIsActive.Checked = questionGroup.IsActive ?? false;
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }

        protected override void OnMenuNewClick()
        {
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
            auditLogFilter.PrimaryKeyData = string.Format("ServiceUnitID='{0}'", txtServiceUnitID.Text.Trim());
            auditLogFilter.TableName = "ServiceUnit";
        }

        protected override void OnDataModeChanged(Temiang.Avicenna.Common.AppEnum.DataMode oldVal, Temiang.Avicenna.Common.AppEnum.DataMode newVal)
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Url Search & List
            UrlPageSearch = "QuestionFormInServiceUnitSearch.aspx";
            UrlPageList = "QuestionFormInServiceUnitList.aspx";

            ProgramID = AppConstant.Program.QuestionFormInServiceUnit;

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {

        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {

        }


        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new ServiceUnit();
            if (entity.LoadByPrimaryKey(txtServiceUnitID.Text))
            {
                ctlMatrix.SaveMatrix();
            }
        }

        #endregion
    }
}