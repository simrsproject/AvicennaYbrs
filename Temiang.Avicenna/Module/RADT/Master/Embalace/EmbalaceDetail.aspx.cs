using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class EmbalaceDetail : BasePageDetail
    {
        #region Page Event & Initialize
        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "EmbalaceSearch.aspx";
            UrlPageList = "EmbalaceList.aspx";

            ProgramID = AppConstant.Program.Embalace;

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
            OnPopulateEntryControl(new Embalace());
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            Embalace entity = new Embalace();
            if (entity.LoadByPrimaryKey(txtEmbalaceID.Text))
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
            Embalace entity = new Embalace();
            if (entity.LoadByPrimaryKey(txtEmbalaceID.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }
            entity = new Embalace();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }
        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            Embalace entity = new Embalace();
            if (entity.LoadByPrimaryKey(txtEmbalaceID.Text))
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
            auditLogFilter.PrimaryKeyData = string.Format("EmbalaceID='{0}'", txtEmbalaceID.Text.Trim());
            auditLogFilter.TableName = "Embalace";
        }
        #endregion

        #region ToolBar Menu Support
        protected override void OnDataModeChanged(Temiang.Avicenna.Common.AppEnum.DataMode oldVal, Temiang.Avicenna.Common.AppEnum.DataMode newVal)
        {
            //TODO: Set status entry control
            txtEmbalaceID.Enabled = (newVal == Temiang.Avicenna.Common.AppEnum.DataMode.New);

        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            Embalace entity = new Embalace();
            if (parameters.Length > 0)
            {
                String embalaceID = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(embalaceID);
            }
            else
            {
                entity.LoadByPrimaryKey(txtEmbalaceID.Text);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            Embalace embalace = (Embalace)entity;
            txtEmbalaceID.Text = embalace.EmbalaceID;
            txtEmbalaceName.Text = embalace.EmbalaceName;
            txtEmbalaceLabel.Text = embalace.EmbalaceLabel;
            txtEmbalaceFeeAmount.Value = Convert.ToDouble(embalace.EmbalaceFeeAmount ?? 0);
        }

        #endregion

        #region Private Method Standard
        private void SetEntityValue(Embalace entity)
        {
            entity.EmbalaceID = txtEmbalaceID.Text;
            entity.EmbalaceName = txtEmbalaceName.Text;
            entity.EmbalaceLabel = txtEmbalaceLabel.Text;
            entity.EmbalaceFeeAmount = Convert.ToDecimal(txtEmbalaceFeeAmount.Value);

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(Embalace entity)
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
            EmbalaceQuery que = new EmbalaceQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.EmbalaceID > txtEmbalaceID.Text);
                que.OrderBy(que.EmbalaceID.Ascending);
            }
            else
            {
                que.Where(que.EmbalaceID < txtEmbalaceID.Text);
                que.OrderBy(que.EmbalaceID.Descending);
            }
            Embalace entity = new Embalace();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }
        #endregion

        #region Method & Event TextChanged

        #endregion
    }
}
