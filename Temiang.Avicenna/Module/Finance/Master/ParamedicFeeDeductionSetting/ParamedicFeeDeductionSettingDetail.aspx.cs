using System;
using System.Data;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class ParamedicFeeDeductionSettingDetail : BasePageDetail
    {
        private void SetEntityValue(ParamedicFeeDeductionSetting entity)
        {
            entity.SRParamedicFeeDeduction = cboSRParamedicFeeDeduction.SelectedValue;
            entity.SRRegistrationType = cboSRRegistrationType.SelectedValue;
            entity.SRGuarantorType = cboSRGuarantorType.SelectedValue;
            entity.GuarantorID = cboGuarantor.SelectedValue;
            entity.ServiceUnitID = cboServiceUnit.SelectedValue;
            entity.TariffComponentID = cboTariffComponent.SelectedValue;
            entity.SRParamedicFeeDeductionMethod = cboSRParamedicFeeDeductionMethod.SelectedValue;
            entity.IsDeductionValueInPercent = chkIsDeductionValueInPercent.Checked;
            entity.DeductionValue = (decimal)txtDeductionValue.Value.Value;
            entity.IsMainPhysicianOnly = chkIsMainPhysicianOnly.Checked;
            entity.IsActive = chkIsActive.Checked;
            entity.IsAfterTax = chkIsAfterTax.Checked;

            //Last Update Status
            if (entity.es.IsAdded) {
                entity.CreatedByUserID = AppSession.UserLogin.UserID;
                entity.CreatedDateTime = DateTime.Now;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
            if(entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            ParamedicFeeDeductionSettingQuery que = new ParamedicFeeDeductionSettingQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.DeductionID > hfDeductionID.Value);
                que.OrderBy(que.DeductionID.Ascending);
            }
            else
            {
                que.Where(que.DeductionID < hfDeductionID.Value);
                que.OrderBy(que.DeductionID.Descending);
            }
            ParamedicFeeDeductionSetting entity = new ParamedicFeeDeductionSetting();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #region Override Method & Function

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            ParamedicFeeDeductionSetting entity = new ParamedicFeeDeductionSetting();
            if (parameters.Length > 0)
            {
                Int32 SettingId = System.Convert.ToInt32(parameters[0]);
                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(SettingId);
            }
            else
                entity.LoadByPrimaryKey(System.Convert.ToInt32(hfDeductionID.Value));
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var s = (ParamedicFeeDeductionSetting)entity;
            hfDeductionID.Value = (s.DeductionID ?? 0).ToString();
            cboSRParamedicFeeDeduction.SelectedValue = s.SRParamedicFeeDeduction;
            cboSRRegistrationType.SelectedValue = s.SRRegistrationType;
            cboSRRegistrationType_SelectedIndexChanged(cboSRRegistrationType, 
                new RadComboBoxSelectedIndexChangedEventArgs(
                    cboSRRegistrationType.Text,
                    cboSRRegistrationType.Text,
                    cboSRRegistrationType.SelectedValue,
                    cboSRRegistrationType.SelectedValue));
            cboServiceUnit.SelectedValue = s.ServiceUnitID;
            cboSRGuarantorType.SelectedValue = s.SRGuarantorType;
            cboGuarantor_ItemsRequested(cboGuarantor, new RadComboBoxItemsRequestedEventArgs() { Text = s.GuarantorID ?? string.Empty });
            cboGuarantor.SelectedValue = s.GuarantorID;
            cboTariffComponent.SelectedValue = s.TariffComponentID;
            cboSRParamedicFeeDeductionMethod.SelectedValue = s.SRParamedicFeeDeductionMethod;
            chkIsDeductionValueInPercent.Checked = s.IsDeductionValueInPercent ?? false;
            txtDeductionValue.Value = System.Convert.ToDouble((s.DeductionValue ?? 0));
            chkIsMainPhysicianOnly.Checked = s.IsMainPhysicianOnly ?? false;
            chkIsActive.Checked = s.IsActive ?? false;
            chkIsAfterTax.Checked = s.IsAfterTax ?? false;
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {

        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new ParamedicFeeDeductionSetting());
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
            auditLogFilter.PrimaryKeyData = string.Format("SmfID='{0}'", hfDeductionID.Value);
            auditLogFilter.TableName = "ParamedicFeeDeductionSetting";
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {

        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Url Search & List
            UrlPageSearch = "ParamedicFeeDeductionSettingSearch.aspx";
            UrlPageList = "ParamedicFeeDeductionSettingList.aspx";

            ProgramID = AppConstant.Program.PhysicianFeeDeductionSetting;

            // search tidak diperlukan disini
            ToolBarMenuSearch.Visible = false;

            //StandardReference Initialize
            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRParamedicFeeDeduction, AppEnum.StandardReference.ParamedicFeeDeduction);
                StandardReference.InitializeIncludeSpace(cboSRRegistrationType, AppEnum.StandardReference.RegistrationType);
                StandardReference.InitializeIncludeSpace(cboSRParamedicFeeDeductionMethod, AppEnum.StandardReference.ParamedicFeeDeductionMethod);
                StandardReference.InitializeIncludeSpace(cboSRGuarantorType, AppEnum.StandardReference.GuarantorType);

                // tariff component id
                var tcColl = new TariffComponentCollection();
                tcColl.Query.Where(tcColl.Query.IsTariffParamedic == true);
                tcColl.LoadAll();
                cboTariffComponent.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (var tc in tcColl)
                {
                    cboTariffComponent.Items.Add(new RadComboBoxItem(tc.TariffComponentName, tc.TariffComponentID));
                }
            }
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new ParamedicFeeDeductionSetting();
            entity.LoadByPrimaryKey(System.Convert.ToInt32(hfDeductionID.Value));
            entity.MarkAsDeleted();
            SaveEntity(entity);
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new ParamedicFeeDeductionSetting();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        private void SaveEntity(ParamedicFeeDeductionSetting entity)
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
            var entity = new ParamedicFeeDeductionSetting();
            if (entity.LoadByPrimaryKey(System.Convert.ToInt32(hfDeductionID.Value)))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
        }

        #endregion

        protected void cboSRRegistrationType_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            // load service unit
            cboServiceUnit_ItemsRequested(cboServiceUnit, new RadComboBoxItemsRequestedEventArgs() { Text = cboServiceUnit.Text });
        }
        protected void cboServiceUnit_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var RegistrationType = cboSRRegistrationType.SelectedValue;
            if (RegistrationType.Equals(string.Empty)) RegistrationType = "xxx";

            string searchTextContain = string.Format("%{0}%", e.Text);

            var query = new ServiceUnitQuery("a");
            var srQ = new ServiceRoomQuery("b");
            var bedQ = new BedQuery("c");
            query.InnerJoin(srQ).On(query.ServiceUnitID == srQ.ServiceUnitID);

            if (RegistrationType == AppConstant.RegistrationType.InPatient)
                query.InnerJoin(bedQ).On(srQ.RoomID == bedQ.RoomID);
            else
                query.LeftJoin(bedQ).On(srQ.RoomID == bedQ.RoomID);

            query.Select
                (
                    query.ServiceUnitID,
                    query.ServiceUnitName
                );
            query.es.Distinct = true;
            query.OrderBy(query.ServiceUnitID.Ascending);
            query.Where
                (
                    query.ServiceUnitName.Like(searchTextContain),
                    query.IsActive == true
                );
            query.Where(query.SRRegistrationType == RegistrationType);
            var dt = query.LoadDataTable();
            // insert empty row
            var r = dt.NewRow();
            r["ServiceUnitID"] = r["ServiceUnitName"] = string.Empty;
            dt.Rows.InsertAt(r, 0);

            cboServiceUnit.DataSource = dt;
            cboServiceUnit.DataBind();
        }

        protected void cboServiceUnit_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ServiceUnitName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ServiceUnitID"].ToString();
        }

        protected void cboGuarantor_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var i = new GuarantorQuery("a");
            i.Where(
                    i.IsActive == true,
                    i.Or(i.GuarantorID.Like("%" + e.Text + "%"), i.GuarantorName.Like("%" + e.Text + "%"))
                )
                .Select(i.GuarantorID, i.GuarantorName)
                .OrderBy(i.GuarantorName.Ascending);
            if (cboSRGuarantorType.SelectedValue != string.Empty) {
                i.Where(i.SRGuarantorType.Equal(cboSRGuarantorType.SelectedValue));
            }
            i.es.Top = 20;

            DataTable tbl = i.LoadDataTable();
            // insert empty row
            var r = tbl.NewRow();
            r["GuarantorID"] = r["GuarantorName"] = string.Empty;
            tbl.Rows.InsertAt(r, 0);

            cboGuarantor.DataSource = tbl;
            cboGuarantor.DataBind();
        }

        protected void cboGuarantor_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["GuarantorName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["GuarantorID"].ToString();
        }
    }
}
