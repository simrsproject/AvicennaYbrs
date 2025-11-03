using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Windows;
using System.Linq;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class PoliSoundDetail : BasePageDetail
    {        
        private void SetEntityValue(ServiceUnit entity)
        {
            entity.ServiceUnitID = txtServiceUnitID.Text;
            entity.ServiceUnitName = txtServiceUnitName.Text;
            entity.QueueCode = txtQueueCode.Text;
            entity.SoundFilePath = txtSoundFilePath.Text;
            entity.SrqueueinglocationReg = cboSRQueueingLocReg.SelectedValue;
            entity.SrqueueinglocationPoli = cboSRQueueingLocPoli.SelectedValue;
           
            //Last Update Status

            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            ServiceUnitQuery que = new ServiceUnitQuery();
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
            ServiceUnit entity = new ServiceUnit();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #region Override Method & Function

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            ServiceUnit entity = new ServiceUnit();
            if (parameters.Length > 0)
            {
                String serviceUnitID = (String)parameters[0];
                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(serviceUnitID);
            }
            else
                entity.LoadByPrimaryKey(txtServiceUnitID.Text);
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            ServiceUnit serviceUnit = (ServiceUnit)entity;
            txtServiceUnitID.Text = serviceUnit.ServiceUnitID;
            txtServiceUnitName.Text = serviceUnit.ServiceUnitName;
            txtQueueCode.Text = serviceUnit.QueueCode;
            txtSoundFilePath.Text = serviceUnit.SoundFilePath;
            btnUpload.Enabled = true;

            cboSRQueueingLocReg.SelectedValue = serviceUnit.SrqueueinglocationReg;
            cboSRQueueingLocPoli.SelectedValue = serviceUnit.SrqueueinglocationPoli;
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }

        protected override void OnMenuNewClick()
        {           
            OnPopulateEntryControl(new ServiceUnit());
            //btnUpload.Enabled = true;         
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

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            txtServiceUnitID.Enabled = false;//(newVal == AppEnum.DataMode.New);
            txtServiceUnitName.Enabled = false;
            btnUpload.Enabled = newVal != AppEnum.DataMode.Read;
         
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Url Search & List
            //UrlPageSearch = "PSSearch.aspx";
            UrlPageList = "QueueingSoundList.aspx";

            ProgramID = AppConstant.Program.QueueingSound;

            if (!Page.IsPostBack) {
                var stdRef = new AppStandardReferenceItemCollection();
                stdRef.LoadByStandardReferenceID("QueueingLocation");

                cboSRQueueingLocReg.Items.Add(new RadComboBoxItem("", ""));
                cboSRQueueingLocPoli.Items.Add(new RadComboBoxItem("", ""));
                foreach (var std in stdRef.OrderBy(s => s.ItemID))
                {
                    cboSRQueueingLocReg.Items.Add(new RadComboBoxItem(std.ItemName, std.ItemID));
                    cboSRQueueingLocPoli.Items.Add(new RadComboBoxItem(std.ItemName, std.ItemID));
                }
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            ServiceUnit entity = new ServiceUnit();
            entity.LoadByPrimaryKey(txtServiceUnitID.Text);
            entity.MarkAsDeleted();
            SaveEntity(entity);
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            ServiceUnit entity = new ServiceUnit();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        private void SaveEntity(ServiceUnit entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            ServiceUnit entity = new ServiceUnit();
            if (entity.LoadByPrimaryKey(txtServiceUnitID.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
        }
        private void ShowProgress(UploadedFile file)
        {
            const int total = 100;

            RadProgressContext progress = RadProgressContext.Current;

            for (int i = 0; i < total; i++)
            {
                progress.PrimaryTotal = 1;
                progress.PrimaryValue = 1;
                progress.PrimaryPercent = 100;

                progress.SecondaryTotal = total;
                progress.SecondaryValue = i;
                progress.SecondaryPercent = i;

                progress.CurrentOperationText = file.GetName() + " is being processed...";

                if (!Response.IsClientConnected)
                {
                    //Cancel button was clicked or the browser was closed, so stop processing
                    break;
                }
            }
        }

        #endregion
    }
}
