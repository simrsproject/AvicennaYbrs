using System;
using System.Data;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class ParamedicFeeByServiceSettingDetail : BasePageDetail
    {
        private void SetEntityValue(ParamedicFeeByServiceSetting entity)
        {
            entity.SRRegistrationType = cboSRRegistrationType.SelectedValue;
            entity.ServiceUnitID = cboServiceUnit.SelectedValue;
            entity.ItemID = cboItem.SelectedValue;
            entity.ClassID = cboClass.SelectedValue;
            entity.SRParamedicFeeCaseType = cboSRParamedicFeeCaseType.SelectedValue;
            entity.SRParamedicFeeIsTeam = cboSRParamedicFeeIsTeam.SelectedValue;
            entity.SRParamedicFeeTeamStatus = cboSRParamedicFeeTeamStatus.SelectedValue;
            entity.TariffComponentID = cboTariffComponent.SelectedValue;
            entity.IsFeeValueInPercent = chkIsFeeValueInPercent.Checked;
            entity.FeeValue = (decimal)txtFeeValue.Value.Value;
            entity.CountMax = txtCountMax.Value.Value.ToInt();
            entity.IgnoredIfAnyReplacement = chkIgnoredIfAnyReplacement.Checked;
            entity.IsReplacement = chkIsReplacement.Checked;
            entity.IsReplacementForFeeByPercentageOfAR = chkIsReplacementForFeeByPercentageOfAR.Checked;

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
            ParamedicFeeByServiceSettingQuery que = new ParamedicFeeByServiceSettingQuery();
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
            ParamedicFeeByServiceSetting entity = new ParamedicFeeByServiceSetting();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #region Override Method & Function

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            ParamedicFeeByServiceSetting entity = new ParamedicFeeByServiceSetting();
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
            var s = (ParamedicFeeByServiceSetting)entity;
            hfId.Value = (s.Id ?? 0).ToString();
            cboSRRegistrationType.SelectedValue = s.SRRegistrationType;
            cboSRRegistrationType_SelectedIndexChanged(cboSRRegistrationType, 
                new RadComboBoxSelectedIndexChangedEventArgs(
                    cboSRRegistrationType.Text,
                    cboSRRegistrationType.Text,
                    cboSRRegistrationType.SelectedValue,
                    cboSRRegistrationType.SelectedValue));
            cboServiceUnit.SelectedValue = s.ServiceUnitID;
            cboItem_ItemsRequested(cboItem, new RadComboBoxItemsRequestedEventArgs() { Text = s.ItemID ?? string.Empty });
            cboItem.SelectedValue = s.ItemID;
            cboClass.SelectedValue = s.ClassID;
            cboSRParamedicFeeCaseType.SelectedValue = s.SRParamedicFeeCaseType;
            cboSRParamedicFeeIsTeam.SelectedValue = s.SRParamedicFeeIsTeam;
            cboSRParamedicFeeTeamStatus.SelectedValue = s.SRParamedicFeeTeamStatus;
            cboItem_SelectedIndexChanged(cboItem,
                new RadComboBoxSelectedIndexChangedEventArgs(
                    cboItem.Text,
                    cboItem.Text,
                    cboItem.SelectedValue,
                    cboItem.SelectedValue));
            cboTariffComponent.SelectedValue = s.TariffComponentID;
            chkIsFeeValueInPercent.Checked = s.IsFeeValueInPercent ?? false;
            txtFeeValue.Value = System.Convert.ToDouble((s.FeeValue ?? 0));
            txtCountMax.Value = s.CountMax ?? 0;
            chkIgnoredIfAnyReplacement.Checked = s.IgnoredIfAnyReplacement ?? false;
            chkIsReplacement.Checked = s.IsReplacement ?? true; // mark as replacement for default
            chkIsReplacementForFeeByPercentageOfAR.Checked = s.IsReplacementForFeeByPercentageOfAR ?? true;
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {

        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new ParamedicFeeByServiceSetting());
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
            auditLogFilter.TableName = "ParamedicFeeByServiceSetting";
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //cboSRRegistrationType.Enabled = (newVal == DataMode.New);
            //cboServiceUnit.Enabled = (newVal == DataMode.New);
            //cboItem.Enabled = (newVal == DataMode.New);
            //cboClass.Enabled = (newVal == DataMode.New);
            //cboSRParamedicFeeCaseType.Enabled = (newVal == DataMode.New);
            //cboSRParamedicFeeIsTeam.Enabled = (newVal == DataMode.New);
            //cboSRParamedicFeeTeamStatus.Enabled = (newVal == DataMode.New);
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Url Search & List
            UrlPageSearch = "ParamedicFeeByServiceSettingSearch.aspx";
            UrlPageList = "ParamedicFeeByServiceSettingList.aspx";

            WindowSearch.Height = 300;

            ProgramID = AppConstant.Program.PhysicianFeeByServiceSetting;

            // search tidak diperlukan disini
            ToolBarMenuSearch.Visible = false;

            //StandardReference Initialize
            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRRegistrationType, AppEnum.StandardReference.RegistrationType);
                StandardReference.InitializeIncludeSpace(cboSRParamedicFeeCaseType, AppEnum.StandardReference.ParamedicFeeCaseType);
                StandardReference.InitializeIncludeSpace(cboSRParamedicFeeIsTeam, AppEnum.StandardReference.ParamedicFeeIsTeam);
                StandardReference.InitializeIncludeSpace(cboSRParamedicFeeTeamStatus, AppEnum.StandardReference.ParamedicFeeTeamStatus);
            
                // init item service


                // init class
                //Class
                var coll = new ClassCollection();
                coll.Query.Where(coll.Query.IsActive == true);
                coll.LoadAll();

                cboClass.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (Class c in coll)
                {
                    cboClass.Items.Add(new RadComboBoxItem(c.ClassName, c.ClassID));
                }


                // load service unit
                //cboServiceUnit_ItemsRequested(cboSRRegistrationType, new RadComboBoxItemsRequestedEventArgs() { Text = "xxxxx" });
            }
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new ParamedicFeeByServiceSetting();
            entity.LoadByPrimaryKey(System.Convert.ToInt32(hfId.Value));
            entity.MarkAsDeleted();
            SaveEntity(entity);
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new ParamedicFeeByServiceSetting();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        private void SaveEntity(ParamedicFeeByServiceSetting entity)
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
            var entity = new ParamedicFeeByServiceSetting();
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

        protected void cboItem_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var i = new ItemQuery("a");
            //var isv = new ItemServiceQuery("b");
            var std = new AppStandardReferenceItemQuery("c");
            //i.InnerJoin(isv).On(i.ItemID.Equal(isv.ItemID))
                i.InnerJoin(std).On(std.StandardReferenceID.Equal("ItemType") && 
                    i.SRItemType.Equal(std.ItemID) && std.ReferenceID.Equal("Service"))
                    .Where(
                    i.IsActive == true,
                    i.Or(i.ItemID.Like("%" + e.Text + "%"), i.ItemName.Like("%" + e.Text + "%"))
                )
                .Select(i.ItemID, i.ItemName)
                .OrderBy(i.ItemName.Ascending);
            i.es.Top = 20;

            DataTable tbl = i.LoadDataTable();
            cboItem.DataSource = tbl;
            cboItem.DataBind();
        }

        protected void cboItem_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void cboItem_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            // load tariff component paramedic
            LoadTariffComponentToCBO();
        }

        protected void cboClass_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            // load tariff component paramedic
            LoadTariffComponentToCBO();
        }

        private void LoadTariffComponentToCBO() {
            var itemID = cboItem.SelectedValue;
            var classID = cboClass.SelectedValue;
            var oldVal = cboTariffComponent.SelectedValue;

            var tcColl = new TariffComponentCollection();
            var tc = new TariffComponentQuery("a");
            var itc = new ItemTariffComponentQuery("b");
            tc.InnerJoin(itc).On(tc.TariffComponentID.Equal(itc.TariffComponentID))
                .Where(itc.ItemID.Equal(itemID), tc.IsTariffParamedic.Equal(true))
                .Where(tc.Or(itc.IsAllowVariable.Equal(true), itc.Price.GreaterThan(0)))
                .Select(tc);
            if (!string.IsNullOrEmpty(classID)) {
                tc.Where(tc.Or(itc.ClassID.Equal(classID),itc.ClassID.Equal(AppSession.Parameter.DefaultTariffClass)));
            }
            tc.es.Distinct = true;

            cboTariffComponent.Items.Clear();
            cboTariffComponent.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            if (tcColl.Load(tc)) {
                foreach (var c in tcColl)
                {
                    cboTariffComponent.Items.Add(new RadComboBoxItem(c.TariffComponentName, c.TariffComponentID));
                }
            }

            // return old selected
            cboTariffComponent.SelectedValue = oldVal;
        }
    }
}
