using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class QueueingSoundDetail : BasePageDetail
    {

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "QueueingSoundSearch.aspx";
            UrlPageList = "QueueingSoundList.aspx";

            ProgramID = AppConstant.Program.QueueingSound;

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
            OnPopulateEntryControl(new QueueingSound());
            var query = new QueueingSoundQuery();
            query.es.Top = 1;
            query.Select(query.SoundID);
            query.OrderBy(query.LastUpdateDateTime.Descending);
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            QueueingSound entity = new QueueingSound();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtSoundID.Text)))
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
            QueueingSound entity = new QueueingSound();
            entity = new QueueingSound();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);

        }
        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            QueueingSound entity = new QueueingSound();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtSoundID.Text)))
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
            auditLogFilter.PrimaryKeyData = string.Format("SoundID='{0}'", txtSoundID.Text.Trim());
            auditLogFilter.TableName = "QueueingSound";
        }
        #endregion


        #region ToolBar Menu Support
        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            txtSoundID.Enabled = (newVal == AppEnum.DataMode.New);
            btnUpload.Enabled = newVal != AppEnum.DataMode.Read;
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            QueueingSound entity = new QueueingSound();
            if (parameters.Length > 0)
            {
                string soundID = (string)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(Convert.ToInt32(soundID));

            }
            else
            {
                entity.LoadByPrimaryKey(Convert.ToInt32(txtSoundID.Text));
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            QueueingSound qsound = (QueueingSound)entity;
            txtSoundID.Value = Convert.ToInt32(qsound.SoundID);
            txtName.Text = qsound.Name;
            txtNumber.Value = qsound.Number ?? null;
            chkIsServiceCounter.Checked = qsound.IsServiceCounter ?? false;
            txtFilePath.Text = qsound.FilePath;
        }

        #endregion

        #region Private Method Standard
        private QueueingSoundCollection QueueingSounds
        {
            get
            {
                if (Session["QueueingSounds"] == null)
                {
                    var qscoll = new QueueingSoundCollection();

                    qscoll.Query.Where(qscoll.Query.SoundID == txtSoundID.Text);
                    //ra.OrderBy(ra.AktivitasDate.Ascending, ra.AktivitasTimeStart.Ascending);

                    //qscoll.Load(qs);
                    qscoll.LoadAll();
                    Session["QueueingSounds"] = qscoll;

                }
                return Session["QueueingSounds"] as QueueingSoundCollection;
            }
            set
            {
                Session["QueueingSounds"] = value;
            }
        }
        private void SetEntityValue(QueueingSound entity)
        {
            entity.SoundID = Convert.ToInt32(txtSoundID.Value);
            entity.Name = txtName.Text;
            if (txtNumber.Value.HasValue)
            {
                entity.Number = Convert.ToInt32(txtNumber.Value);
            }
            else
            {
                entity.Number = null;
            }
            entity.IsServiceCounter = chkIsServiceCounter.Checked;
            entity.FilePath = txtFilePath.Text;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(QueueingSound entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                //QueueingSounds.Save();
                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            QueueingSoundQuery que = new QueueingSoundQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.SoundID > txtSoundID.Text);
                que.OrderBy(que.SoundID.Ascending);
            }
            else
            {
                que.Where(que.SoundID < txtSoundID.Text);
                que.OrderBy(que.SoundID.Descending);
            }

            QueueingSound entity = new QueueingSound();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }
        #endregion
        #region Method & Event TextChanged
        #endregion
    }
}