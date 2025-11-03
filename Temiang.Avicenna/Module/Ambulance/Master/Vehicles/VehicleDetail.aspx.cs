using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.DynamicQuery;
using System.Web.UI;
using System.Linq;
using Temiang.Avicenna.Module.Nutrient.Initialization;

namespace Temiang.Avicenna.Module.Ambulance.Master
{
    public partial class VehicleDetail : BasePageDetail
    {
        private void SetEntityValue(Vehicles entity)
        {
            entity.PlateNo = txtPlatNo.Text;
            entity.SRVehicleType = cboVehicleType.SelectedValue;
            entity.SRVehicleStatus = cboVehicleStatus.SelectedValue;
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
            VehiclesQuery que = new VehiclesQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.PlateNo > txtPlatNo.Text);
                que.OrderBy(que.PlateNo.Ascending);
            }
            else
            {
                que.Where(que.PlateNo < txtPlatNo.Text);
                que.OrderBy(que.PlateNo.Descending);
            }
            Vehicles entity = new Vehicles();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #region Override Method & Function

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            Vehicles entity = new Vehicles();
            if (parameters.Length > 0)
            {
                int VehicleID = Convert.ToInt32(parameters[0]);

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(VehicleID);
            }
            else
            {
                entity.LoadByPrimaryKey(Convert.ToInt32(hfVehicleID.Value));
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            Vehicles classes = (Vehicles)entity;
            hfVehicleID.Value = classes.VehicleID.ToString();
            txtPlatNo.Text = classes.PlateNo;
            cboVehicleType.SelectedValue = classes.SRVehicleType;
            cboVehicleStatus.SelectedValue = classes.SRVehicleStatus;
            txtNotes.Text = classes.Notes;
            chkIsActive.Checked = classes.IsActive ?? false;
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new Vehicles());
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
            auditLogFilter.PrimaryKeyData = string.Format("VehicleID='{0}'", hfVehicleID.Value);
            auditLogFilter.TableName = "Vehicles";
        }

        protected override void OnDataModeChanged(Temiang.Avicenna.Common.AppEnum.DataMode oldVal, Temiang.Avicenna.Common.AppEnum.DataMode newVal)
        {
            //txtPlatNo.Enabled = (newVal == Temiang.Avicenna.Common.AppEnum.DataMode.New);
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Url Search & List
            UrlPageSearch = "VehicleSearch.aspx";
            UrlPageList = "VehicleList.aspx";

            ProgramID = AppConstant.Program.Vehicle;

            //StandardReference Initialize
            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboVehicleType, AppEnum.StandardReference.VehicleType);
                StandardReference.InitializeIncludeSpace(cboVehicleStatus, AppEnum.StandardReference.VehicleStatus);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            Vehicles entity = new Vehicles();
            entity.LoadByPrimaryKey(Convert.ToInt32(hfVehicleID.Value));
            entity.MarkAsDeleted();
            SaveEntity(entity);
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new Vehicles();
            if (!string.IsNullOrEmpty(hfVehicleID.Value)) {
                if (entity.LoadByPrimaryKey(Convert.ToInt32(hfVehicleID.Value)))
                {
                    args.MessageText = AppConstant.Message.DuplicateKey;
                    args.IsCancel = true;
                    return;
                }
            }
            entity = new Vehicles();

            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);

            hfVehicleID.Value = entity.VehicleID.ToString();
        }

        private void SaveEntity(Vehicles entity)
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
            Vehicles entity = new Vehicles();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(hfVehicleID.Value)))
            {
                SetEntityValue(entity);
                SaveEntity(entity);

                hfVehicleID.Value = entity.VehicleID.ToString();
            }
        }

        #endregion
    }
}