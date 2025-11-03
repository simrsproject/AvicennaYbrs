using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Payroll.Master
{
    public partial class AttedanceMatrixDetail : BasePageDetail
    {
        #region Page Event & Initialize
        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "AttedanceMatrixSearch.aspx";
            UrlPageList = "AttedanceMatrixList.aspx";
			
			ProgramID = AppConstant.Program.AttedanceMatrix; //TODO: Isi ProgramID
            txtAttedanceMatrixID.Text = "1";
			//StandardReference Initialize
			if (!IsPostBack)
            {
            }
			
			//PopUp Search
			if (!IsCallback)
			{
				
			}
        }
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }
        #endregion

        #region Toolbar Menu Event
        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new AttedanceMatrix());
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            AttedanceMatrix entity = new AttedanceMatrix();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtAttedanceMatrixID.Text)))
            {
                entity.MarkAsDeleted();
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            AttedanceMatrix entity = new AttedanceMatrix();
            entity = new AttedanceMatrix();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }
        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            AttedanceMatrix entity = new AttedanceMatrix();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtAttedanceMatrixID.Text)))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
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
            //TODO: Betulkan PrimaryKeyData nya
            auditLogFilter.PrimaryKeyData = string.Format("AttedanceMatrixID='{0}'", txtAttedanceMatrixID.Text.Trim());
            auditLogFilter.TableName = "AttedanceMatrix";
        }
        #endregion

        #region ToolBar Menu Support
        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //TODO: Set status entry control
            txtAttedanceMatrixID.Enabled = (newVal == AppEnum.DataMode.New);

        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            AttedanceMatrix entity = new AttedanceMatrix();
            if (parameters.Length > 0)
            {
                string attedanceMatrixID = (string)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(Convert.ToInt32(attedanceMatrixID));
            }
            else
            {
                entity.LoadByPrimaryKey(Convert.ToInt32(txtAttedanceMatrixID.Text));
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            AttedanceMatrix attedanceMatrix = (AttedanceMatrix)entity;
            txtAttedanceMatrixID.Value = Convert.ToDouble(attedanceMatrix.AttedanceMatrixID);
            txtAttedanceMatrixName.Text = attedanceMatrix.AttedanceMatrixName;
            txtAttedanceMatrixFieldt.Text = attedanceMatrix.AttedanceMatrixFieldt;

            //Display Data Detail
        }

        #endregion

        #region Private Method Standard
        private void SetEntityValue(AttedanceMatrix entity)
        {
            entity.AttedanceMatrixID = Convert.ToInt32(txtAttedanceMatrixID.Value);
            entity.AttedanceMatrixName = txtAttedanceMatrixName.Text;
            entity.AttedanceMatrixFieldt = txtAttedanceMatrixFieldt.Text;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(AttedanceMatrix entity)
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
            AttedanceMatrixQuery que = new AttedanceMatrixQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.AttedanceMatrixID > txtAttedanceMatrixID.Text);
                que.OrderBy(que.AttedanceMatrixID.Ascending);
            }
            else
            {
                que.Where(que.AttedanceMatrixID < txtAttedanceMatrixID.Text);
                que.OrderBy(que.AttedanceMatrixID.Descending);
            }
            AttedanceMatrix entity = new AttedanceMatrix();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }
        #endregion

        #region Method & Event TextChanged

        #endregion
    }
}
