using System;
using System.Data;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class ServiceFeeSettingDetail : BasePageDetail
    {
        private void SetEntityValue(ServiceFeeSetting entity)
        {
            entity.SRParamedicStatus = cboParamedicStatus.SelectedValue;
            entity.ParamedicID = cboParamedic.SelectedValue;
            entity.SRSpecialty = cboSmf.SelectedValue;
            entity.SRRegistrationType = cboSRRegistrationType.SelectedValue;
            entity.SRTariffType = cboTariffType.SelectedValue;
            entity.ClassID = cboClass.SelectedValue;
            entity.SRGuarantorType = cboGuarantorType.SelectedValue;
            entity.GuarantorID = cboGuarantor.SelectedValue;
            entity.ServiceUnitID = cboServiceUnit.SelectedValue;
            entity.SRItemConditionRuleType = cboItemConditionRuleType.SelectedValue;
            entity.ItemConditionRuleID = cboItemConditionRule.SelectedValue;
            entity.ItemGroupID = cboItemGroup.SelectedValue;
            entity.ItemID = cboItem.SelectedValue;
            entity.SRProcedure = cboSRProcedure.SelectedValue;
            entity.TariffComponentID = cboTariffComponent.SelectedValue;
            entity.FormulaDirektur = txtFormulaDirektur.Text;
            entity.FormulaStruktural = txtFormulaStruktural.Text;
            entity.FormulaMedis = txtFormulaMedis.Text;
            entity.FormulaUnit = txtFormulaUnit.Text;
            entity.FormulaPemerataan = txtFormulaPemerataan.Text;
            entity.Notes = txtNotes.Text;

            entity.Level = System.Convert.ToInt32(txtLevel.Value);

            //Last Update Status
            if (entity.es.IsAdded)
            {
                entity.CreateByUserID = AppSession.UserLogin.UserID;
                entity.CreateDateTime = DateTime.Now;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
            if (entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            ServiceFeeSettingQuery que = new ServiceFeeSettingQuery();
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
            ServiceFeeSetting entity = new ServiceFeeSetting();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #region Override Method & Function

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            ServiceFeeSetting entity = new ServiceFeeSetting();
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
            var s = (ServiceFeeSetting)entity;
            hfId.Value = (s.Id ?? 0).ToString();
            cboParamedicStatus.SelectedValue = s.SRParamedicStatus;
            cboParamedic.SelectedValue = s.ParamedicID;
            cboSmf.SelectedValue = s.SRSpecialty;
            cboSRRegistrationType.SelectedValue = s.SRRegistrationType;
            cboSRRegistrationType_SelectedIndexChanged(cboSRRegistrationType,
                new RadComboBoxSelectedIndexChangedEventArgs(
                    cboSRRegistrationType.Text,
                    cboSRRegistrationType.Text,
                    cboSRRegistrationType.SelectedValue,
                    cboSRRegistrationType.SelectedValue));
            cboTariffType.SelectedValue = s.SRTariffType;
            cboClass.SelectedValue = s.ClassID;
            cboGuarantorType.SelectedValue = s.SRGuarantorType;
            cboGuarantor_ItemsRequested(cboGuarantor, new RadComboBoxItemsRequestedEventArgs() { Text = s.GuarantorID ?? string.Empty });
            cboGuarantor.SelectedValue = s.GuarantorID;
            cboServiceUnit.SelectedValue = s.ServiceUnitID;

            cboItemConditionRuleType.SelectedValue = s.SRItemConditionRuleType;
            cboItemConditionRule.SelectedValue = s.ItemConditionRuleID;

            cboItemGroup.SelectedValue = s.ItemGroupID;
            cboItem_ItemsRequested(cboItem, new RadComboBoxItemsRequestedEventArgs() { Text = s.ItemID ?? string.Empty });
            cboItem.SelectedValue = s.ItemID;
            cboItem_SelectedIndexChanged(cboItem,
                new RadComboBoxSelectedIndexChangedEventArgs(
                    cboItem.Text,
                    cboItem.Text,
                    cboItem.SelectedValue,
                    cboItem.SelectedValue));

            cboSRProcedure.SelectedValue = s.SRProcedure;

            cboTariffComponent.SelectedValue = s.TariffComponentID;

            txtFormulaDirektur.Text = s.FormulaDirektur;
            txtFormulaStruktural.Text = s.FormulaStruktural;
            txtFormulaMedis.Text = s.FormulaMedis;
            txtFormulaUnit.Text = s.FormulaUnit;
            txtFormulaPemerataan.Text = s.FormulaPemerataan;
            txtNotes.Text = s.Notes;

            txtLevel.Value = System.Convert.ToInt32(s.Level ?? 1);
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {

        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new ServiceFeeSetting());
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
            auditLogFilter.PrimaryKeyData = string.Format("Id='{0}'", hfId.Value);
            auditLogFilter.TableName = "ServiceFeeSetting";
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
            if (newVal != AppEnum.DataMode.Read)
            {

            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Url Search & List
            UrlPageSearch = "ServiceFeeSettingSearch.aspx";
            UrlPageList = "ServiceFeeSettingList.aspx";

            WindowSearch.Height = 500;

            ProgramID = AppConstant.Program.ServiceFeeSetting;


            // search tidak diperlukan disini
            ToolBarMenuSearch.Visible = false;

            //StandardReference Initialize
            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboParamedicStatus, AppEnum.StandardReference.ParamedicStatus);
                var parColl = new ParamedicCollection();
                parColl.Query.Where(parColl.Query.IsActive.Equal(true),
                    parColl.Query.ParamedicFee.Equal(true))
                    .OrderBy(parColl.Query.ParamedicName.Ascending);
                parColl.LoadAll();
                cboParamedic.Items.Clear();
                cboParamedic.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (var par in parColl)
                {
                    cboParamedic.Items.Add(new RadComboBoxItem(par.ParamedicName, par.ParamedicID));
                }

                var smfColl = new SmfCollection();
                //smfColl.Query.Where(smfColl.Query.IsActive.Equal(true));
                smfColl.LoadAll();
                cboSmf.Items.Clear();
                cboSmf.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (var smf in smfColl)
                {
                    cboSmf.Items.Add(new RadComboBoxItem(smf.SmfName, smf.SmfID));
                }

                //StandardReference.InitializeIncludeSpace(cboSpecialty, AppEnum.StandardReference.Specialty, true);
                StandardReference.InitializeIncludeSpace(cboSRRegistrationType, AppEnum.StandardReference.RegistrationType);

                StandardReference.InitializeIncludeSpace(cboTariffType, AppEnum.StandardReference.TariffType);
                StandardReference.InitializeIncludeSpace(cboGuarantorType, AppEnum.StandardReference.GuarantorType);

                var guarColl = new GuarantorCollection();
                guarColl.Query.Where(guarColl.Query.IsActive.Equal(true));
                guarColl.LoadAll();
                cboGuarantor.Items.Clear();
                cboGuarantor.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (var guar in guarColl)
                {
                    cboGuarantor.Items.Add(new RadComboBoxItem(guar.GuarantorName, guar.GuarantorID));
                }

                StandardReference.InitializeIncludeSpace(cboItemConditionRuleType, AppEnum.StandardReference.ItemConditionRuleType);

                var icrColl = new ItemConditionRuleCollection();
                icrColl.LoadAll();
                foreach (var icr in icrColl) {
                    cboItemConditionRule.Items.Add(new RadComboBoxItem(icr.ItemConditionRuleName, icr.ItemConditionRuleID));
                }

                var igColl = new ItemGroupCollection();
                igColl.Query.Where(igColl.Query.IsActive == true)
                    .OrderBy(igColl.Query.ItemGroupName.Ascending);
                igColl.LoadAll();

                cboItemGroup.Items.Clear();
                cboItemGroup.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (ItemGroup c in igColl)
                {
                    cboItemGroup.Items.Add(new RadComboBoxItem(c.ItemGroupName, c.ItemGroupID));
                }

                var clColl = new ClassCollection();
                clColl.Query.Where(clColl.Query.IsActive == true)
                    .OrderBy(clColl.Query.ClassName.Ascending);
                clColl.LoadAll();

                cboClass.Items.Clear();
                cboClass.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (Class c in clColl)
                {
                    cboClass.Items.Add(new RadComboBoxItem(c.ClassName, c.ClassID));
                }

                LoadTariffComponentToCBO();

                StandardReference.InitializeIncludeSpace(cboSRProcedure, AppEnum.StandardReference.Procedure);
            }
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new ServiceFeeSetting();
            entity.LoadByPrimaryKey(System.Convert.ToInt32(hfId.Value));
            entity.MarkAsDeleted();
            SaveEntity(entity);
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new ServiceFeeSetting();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        private void SaveEntity(ServiceFeeSetting entity)
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
            var entity = new ServiceFeeSetting();
            if (entity.LoadByPrimaryKey(System.Convert.ToInt32(hfId.Value)))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
        }

        #endregion
        protected void cboGuarantor_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var query = new GuarantorQuery("a");
            query.Select
                (
                    query.GuarantorID,
                    query.GuarantorName
                );
            query.es.Distinct = true;
            query.OrderBy(query.GuarantorName.Ascending);
            query.Where
                (
                    //query.GuarantorName.Like(string.Format("%{0}%", e.Text)),
                    query.IsActive == true,
                    query.Or(query.GuarantorID.Like("%" + e.Text + "%"), query.GuarantorName.Like("%" + e.Text + "%"))
                );
            var dt = query.LoadDataTable();
            // insert empty row
            var r = dt.NewRow();
            r["GuarantorID"] = r["GuarantorName"] = string.Empty;
            dt.Rows.InsertAt(r, 0);

            cboGuarantor.DataSource = dt;
            cboGuarantor.DataBind();
        }

        protected void cboGuarantor_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["GuarantorName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["GuarantorID"].ToString();
        }

        protected void cboSRRegistrationType_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            // load service unit
            cboServiceUnit_ItemsRequested(cboServiceUnit, new RadComboBoxItemsRequestedEventArgs() { Text = cboServiceUnit.Text });
        }
        protected void cboServiceUnit_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var RegistrationType = cboSRRegistrationType.SelectedValue;

            var query = new ServiceUnitQuery("a");
            var srQ = new ServiceRoomQuery("b");
            //var bedQ = new BedQuery("c");
            query.InnerJoin(srQ).On(query.ServiceUnitID == srQ.ServiceUnitID);

            //if (RegistrationType == AppConstant.RegistrationType.InPatient)
            //    query.InnerJoin(bedQ).On(srQ.RoomID == bedQ.RoomID);
            //else
            //    query.LeftJoin(bedQ).On(srQ.RoomID == bedQ.RoomID);

            query.Select
                (
                    query.ServiceUnitID,
                    query.ServiceUnitName
                );
            query.es.Distinct = true;
            query.OrderBy(query.ServiceUnitID.Ascending);
            query.Where
                (
                    //query.ServiceUnitName.Like(string.Format("%{0}%", e.Text)),
                    query.IsActive == true
                );
            if (!string.IsNullOrEmpty(RegistrationType))
            {
                query.Where(query.Or(query.SRRegistrationType == RegistrationType, query.IsUsingJobOrder == true));
            }
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
            var ig = new ItemGroupQuery("ig");
            //i.InnerJoin(isv).On(i.ItemID.Equal(isv.ItemID))
            i.InnerJoin(std).On(std.StandardReferenceID.Equal("ItemType") &&
                i.SRItemType.Equal(std.ItemID) && std.ReferenceID.Equal("Service"))
            .InnerJoin(ig).On(i.ItemGroupID == ig.ItemGroupID)
            .Where(
                i.IsActive == true,
                i.Or(i.ItemID.Like("%" + e.Text + "%"), i.ItemName.Like("%" + e.Text + "%"))
            )
            .Select(i.ItemID, i.ItemName, ig.ItemGroupName)
            .OrderBy(i.ItemName.Ascending);
            i.es.Top = 20;

            DataTable tbl = i.LoadDataTable();
            var r = tbl.NewRow();
            r["ItemID"] = r["ItemName"] = r["ItemGroupName"] = string.Empty;
            tbl.Rows.InsertAt(r, 0);

            cboItem.DataSource = tbl;
            cboItem.DataBind();
        }

        protected void cboItem_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString() + (string.IsNullOrEmpty(((DataRowView)e.Item.DataItem)["ItemGroupName"].ToString()) ? "" : " (" + ((DataRowView)e.Item.DataItem)["ItemGroupName"].ToString() + ")");
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

        private void LoadTariffComponentToCBO()
        {
            var itemID = cboItem.SelectedValue;
            var classID = "";// cboClass.SelectedValue;
            var oldVal = cboTariffComponent.SelectedValue;

            var tcColl = new TariffComponentCollection();
            var tc = new TariffComponentQuery("a");

            if (!string.IsNullOrEmpty(itemID))
            {
                var itc = new ItemTariffComponentQuery("b");
                tc.InnerJoin(itc).On(tc.TariffComponentID.Equal(itc.TariffComponentID))
                    .Where(itc.ItemID.Equal(itemID))
                    .Where(tc.Or(itc.IsAllowVariable.Equal(true), itc.Price.GreaterThan(0)));

                if (!string.IsNullOrEmpty(classID))
                {
                    tc.Where(tc.Or(itc.ClassID.Equal(classID), itc.ClassID.Equal(AppSession.Parameter.DefaultTariffClass)));
                }
            }
            tc.Where(tc.IsTariffParamedic.Equal(true))
                .Select(tc);
            tc.es.Distinct = true;

            cboTariffComponent.Items.Clear();
            cboTariffComponent.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            if (tcColl.Load(tc))
            {
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
