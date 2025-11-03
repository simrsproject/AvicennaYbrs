using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.DynamicQuery;
using System.Web.UI;
using System.Linq;

namespace Temiang.Avicenna.Module.Ambulance.Master
{
    public partial class DriverDetail : BasePageDetail
    {
        private void SetEntityValue(VehicleDrivers entity)
        {
            entity.DriverName = txtDriverName.Text;
            entity.SRDriverStatus = cboDriverStatus.SelectedValue;
            entity.Notes = txtNotes.Text;
            entity.IsActive = chkIsActive.Checked;

            if (entity.es.IsAdded)
            {
                entity.CreateByUserID = AppSession.UserLogin.UserID;
                entity.CreateDateTime = DateTime.Now;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
            if (entity.es.IsModified) {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            VehicleDriversQuery que = new VehicleDriversQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.DriverID > hfDriverID.Value);
                que.OrderBy(que.DriverID.Ascending);
            }
            else
            {
                que.Where(que.DriverID < hfDriverID.Value);
                que.OrderBy(que.DriverID.Descending);
            }
            VehicleDrivers entity = new VehicleDrivers();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #region Override Method & Function

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            VehicleDrivers entity = new VehicleDrivers();
            if (parameters.Length > 0)
            {
                int DriverID = Convert.ToInt32(parameters[0]);

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(DriverID);
            }
            else
            {
                entity.LoadByPrimaryKey(Convert.ToInt32(hfDriverID.Value));
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            VehicleDrivers classes = (VehicleDrivers)entity;
            hfDriverID.Value = classes.DriverID.ToString();
            txtDriverName.Text = classes.DriverName;
            cboDriverStatus.SelectedValue = classes.SRDriverStatus;
            txtNotes.Text = classes.Notes;
            chkIsActive.Checked = classes.IsActive ?? false;
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new VehicleDrivers());
            chkIsActive.Checked = true;
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
            auditLogFilter.PrimaryKeyData = string.Format("DriverID='{0}'", hfDriverID.Value.Trim());
            auditLogFilter.TableName = "VehicleDrivers";
        }

        protected override void OnDataModeChanged(Temiang.Avicenna.Common.AppEnum.DataMode oldVal, Temiang.Avicenna.Common.AppEnum.DataMode newVal)
        {
            //txtEmployeeID.Enabled = (newVal == Temiang.Avicenna.Common.AppEnum.DataMode.New);
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Url Search & List
            UrlPageSearch = "DriverSearch.aspx";
            UrlPageList = "DriverList.aspx";

            ProgramID = AppConstant.Program.Driver;

            //StandardReference Initialize
            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboDriverStatus, AppEnum.StandardReference.DriverStatus);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            VehicleDrivers entity = new VehicleDrivers();
            entity.LoadByPrimaryKey(Convert.ToInt32(hfDriverID.Value));
            entity.MarkAsDeleted();
            SaveEntity(entity);
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new VehicleDrivers();
            if (!string.IsNullOrEmpty(hfDriverID.Value))
            {
                if (entity.LoadByPrimaryKey(Convert.ToInt32(hfDriverID.Value)))
                {
                    args.MessageText = AppConstant.Message.DuplicateKey;
                    args.IsCancel = true;
                    return;
                }
            }
                
            entity = new VehicleDrivers();

            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);

            hfDriverID.Value = entity.DriverID.ToString();
        }

        private void SaveEntity(VehicleDrivers entity)
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
            VehicleDrivers entity = new VehicleDrivers();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(hfDriverID.Value)))
            {
                SetEntityValue(entity);
                SaveEntity(entity);

                hfDriverID.Value = entity.DriverID.ToString();
            }
        }

        #endregion
    }
}