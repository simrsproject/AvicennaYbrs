using System;
using System.Data;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class ParamedicFeeByArSettingDetail : BasePageDetail
    {
        private void SetEntityValue(ParamedicFeeByArSetting entity)
        {
            entity.SRRegistrationType = cboSRRegistrationType.SelectedValue;
            entity.IsMergeToIPR = chkIsMergeToIPR.Checked;
            entity.ServiceUnitID = cboServiceUnit.SelectedValue;
            entity.SmfID = cboSmf.SelectedValue;
            entity.SRParamedicFeeCaseType = cboSRParamedicFeeCaseType.SelectedValue;
            entity.SRParamedicFeeIsTeam = cboSRParamedicFeeIsTeam.SelectedValue;
            entity.LosStart = txtLosStart.Value.ToInt();
            entity.LosEnd = txtLosEnd.Value.ToInt();
            entity.SRParamedicFeeTeamStatus = cboSRParamedicFeeTeamStatus.SelectedValue;
            entity.IsFeeValueInPercent = chkIsFeeValueInPercent.Checked;
            entity.FeeValue = (decimal)txtFeeValue.Value.Value;

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
            ParamedicFeeByArSettingQuery que = new ParamedicFeeByArSettingQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.Id > hfId.Value);
                que.OrderBy(que.Id.Ascending);
            }
            else
            {
                que.Where(que.Id < hfId.Value);
                que.OrderBy(que.Id.Descending);
            }
            ParamedicFeeByArSetting entity = new ParamedicFeeByArSetting();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #region Override Method & Function

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            ParamedicFeeByArSetting entity = new ParamedicFeeByArSetting();
            if (parameters.Length > 0)
            {
                Int32 SettingId = System.Convert.ToInt32(parameters[0]);
                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(SettingId);
            }
            else
                entity.LoadByPrimaryKey(System.Convert.ToInt32(hfId.Value));
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var s = (ParamedicFeeByArSetting)entity;
            hfId.Value = (s.Id ?? 0).ToString();
            cboSRRegistrationType.SelectedValue = s.SRRegistrationType;
            cboSRRegistrationType_SelectedIndexChanged(cboSRRegistrationType, 
                new RadComboBoxSelectedIndexChangedEventArgs(
                    cboSRRegistrationType.Text,
                    cboSRRegistrationType.Text,
                    cboSRRegistrationType.SelectedValue,
                    cboSRRegistrationType.SelectedValue));

            chkIsMergeToIPR.Checked = s.IsMergeToIPR ?? false;
            cboServiceUnit.SelectedValue = s.ServiceUnitID;
            cboSmf.SelectedValue = s.SmfID;
            cboSRParamedicFeeCaseType.SelectedValue = s.SRParamedicFeeCaseType;
            cboSRParamedicFeeIsTeam.SelectedValue = s.SRParamedicFeeIsTeam;
            txtLosStart.Value = s.LosStart;
            txtLosEnd.Value = s.LosEnd;
            cboSRParamedicFeeTeamStatus.SelectedValue = s.SRParamedicFeeTeamStatus;
            chkIsFeeValueInPercent.Checked = s.IsFeeValueInPercent ?? false;
            txtFeeValue.Value = System.Convert.ToDouble((s.FeeValue ?? 0));
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {

        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new ParamedicFeeByArSetting());
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
            auditLogFilter.PrimaryKeyData = string.Format("SmfID='{0}'", hfId.Value);
            auditLogFilter.TableName = "ParamedicFeeByArSetting";
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //cboSRRegistrationType.Enabled = (newVal == DataMode.New);
            //chkIsMergeToIPR.Enabled = (newVal == DataMode.New);
            //cboServiceUnit.Enabled = (newVal == DataMode.New);
            //cboSRParamedicFeeCaseType.Enabled = (newVal == DataMode.New);
            //cboSRParamedicFeeIsTeam.Enabled = (newVal == DataMode.New);
            ////txtLosStart.Enabled = (newVal == DataMode.New);
            ////txtLosEnd.Enabled = (newVal == DataMode.New);
            //cboSRParamedicFeeTeamStatus.Enabled = (newVal == DataMode.New);
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Url Search & List
            UrlPageSearch = "ParamedicFeeByArSettingSearch.aspx";
            UrlPageList = "ParamedicFeeByArSettingList.aspx";

            ProgramID = AppConstant.Program.PhysicianFeeByArSetting;

            // search tidak diperlukan disini
            ToolBarMenuSearch.Visible = false;

            //StandardReference Initialize
            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRRegistrationType, AppEnum.StandardReference.RegistrationType);
                StandardReference.InitializeIncludeSpace(cboSRParamedicFeeCaseType, AppEnum.StandardReference.ParamedicFeeCaseType);
                StandardReference.InitializeIncludeSpace(cboSRParamedicFeeIsTeam, AppEnum.StandardReference.ParamedicFeeIsTeam);
                StandardReference.InitializeIncludeSpace(cboSRParamedicFeeTeamStatus, AppEnum.StandardReference.ParamedicFeeTeamStatus);
            
                // load service unit
                //cboServiceUnit_ItemsRequested(cboSRRegistrationType, new RadComboBoxItemsRequestedEventArgs() { Text = "xxxxx" });
                //var smfColl = new SmfCollection();
                //if (smfColl.LoadAll()) {
                //    cboSmf.Items.Clear();
                //    cboSmf.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                //    foreach (var smf in smfColl) {
                //        cboSmf.Items.Add(new RadComboBoxItem(smf.SmfName, smf.SmfID));
                //    }
                //}
                var smfColl = new AppStandardReferenceItemCollection();
                smfColl.Query.Where(smfColl.Query.StandardReferenceID.Equal("SurgerySpecialty"));
                if (smfColl.LoadAll())
                {
                    cboSmf.Items.Clear();
                    cboSmf.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                    foreach (var smf in smfColl)
                    {
                        cboSmf.Items.Add(new RadComboBoxItem(smf.ItemName, smf.ItemID));
                    }
                }
            }
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new ParamedicFeeByArSetting();
            entity.LoadByPrimaryKey(System.Convert.ToInt32(hfId.Value));
            entity.MarkAsDeleted();
            SaveEntity(entity);
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new ParamedicFeeByArSetting();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        private void SaveEntity(ParamedicFeeByArSetting entity)
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
            var entity = new ParamedicFeeByArSetting();
            if (entity.LoadByPrimaryKey(System.Convert.ToInt32(hfId.Value)))
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
                    query.ServiceUnitName.Like(string.Format("%{0}%", e.Text)),
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
    }
}
