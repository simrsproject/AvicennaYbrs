using System;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.ControlPanel.PrinterManagement
{
    public partial class PrinterDetail : BasePageDetail
    {
        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "PrinterSearch.aspx";
            UrlPageList = "PrinterList.aspx";

            ProgramID = AppConstant.Program.PrinterLocation;
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
            OnPopulateEntryControl(new Printer());
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new Printer();
            if (entity.LoadByPrimaryKey(txtPrinterID.Text))
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
            var entity = new Printer();
            if (entity.LoadByPrimaryKey(txtPrinterID.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }
            entity = new Printer();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new Printer();
            if (entity.LoadByPrimaryKey(txtPrinterID.Text))
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
            auditLogFilter.PrimaryKeyData = string.Format("PrinterID='{0}'", txtPrinterID.Text.Trim());
            auditLogFilter.TableName = "Printer";
        }

        #endregion

        #region ToolBar Menu Support	

        protected override void OnDataModeChanged(Temiang.Avicenna.Common.AppEnum.DataMode oldVal, Temiang.Avicenna.Common.AppEnum.DataMode newVal)
        {
            //TODO: Set status entry control
            txtPrinterID.Enabled = (newVal == Temiang.Avicenna.Common.AppEnum.DataMode.New);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new Printer();
            if (parameters.Length > 0)
            {
                String printerID = parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(printerID);
            }
            else
            {
                entity.LoadByPrimaryKey(txtPrinterID.Text);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var printer = (Printer) entity;
            txtPrinterID.Text = printer.PrinterID;
            txtPrinterName.Text = printer.PrinterName;
            txtPrinterLocationHost.Text = printer.PrinterLocationHost;
            txtPrinterManagerHost.Text = printer.PrinterManagerHost;
            txtNotes.Text = printer.Notes;
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(Printer entity)
        {
            entity.PrinterID = txtPrinterID.Text;
            entity.PrinterName = txtPrinterName.Text;
            entity.PrinterLocationHost = txtPrinterLocationHost.Text;
            entity.PrinterManagerHost = txtPrinterManagerHost.Text;
            entity.Notes = txtNotes.Text;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(Printer entity)
        {
            using (var trans = new esTransactionScope())
            {
                entity.Save();
                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new PrinterQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.PrinterID > txtPrinterID.Text);
                que.OrderBy(que.PrinterID.Ascending);
            }
            else
            {
                que.Where(que.PrinterID < txtPrinterID.Text);
                que.OrderBy(que.PrinterID.Descending);
            }
            var entity = new Printer();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #endregion
    }
}