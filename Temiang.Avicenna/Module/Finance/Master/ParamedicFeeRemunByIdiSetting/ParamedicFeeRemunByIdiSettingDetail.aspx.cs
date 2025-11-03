using System;
using System.Data;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class ParamedicFeeRemunByIdiSettingDetail : BasePageDetail
    {
        private void SetEntityValue(ParamedicFeeRemunByIdiSettings entity)
        {
            entity.ParamedicID = cboParamedic.SelectedValue;
            entity.SmfID = cboSmf.SelectedValue;
            entity.ItemGroupID = cboItemGroup.SelectedValue;
            entity.ItemID = cboItem.SelectedValue;
            entity.MultiplierValue = (decimal)txtMultiplierValue.Value.Value;

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
            ParamedicFeeRemunByIdiSettingsQuery que = new ParamedicFeeRemunByIdiSettingsQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.SettingID> hfId.Value);
                que.OrderBy(que.SettingID.Ascending);
            }
            else
            {
                que.Where(que.SettingID < hfId.Value);
                que.OrderBy(que.SettingID.Descending);
            }
            ParamedicFeeRemunByIdiSettings entity = new ParamedicFeeRemunByIdiSettings();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #region Override Method & Function

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            ParamedicFeeRemunByIdiSettings entity = new ParamedicFeeRemunByIdiSettings();
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
            var s = (ParamedicFeeRemunByIdiSettings)entity;
            hfId.Value = (s.SettingID ?? 0).ToString();
            cboParamedic.SelectedValue = s.ParamedicID;
            cboSmf.SelectedValue = s.SmfID;
            cboItemGroup.SelectedValue = s.ItemGroupID;
            cboItem_ItemsRequested(cboItem, new RadComboBoxItemsRequestedEventArgs() { Text = s.ItemID ?? string.Empty });
            cboItem.SelectedValue = s.ItemID;
            txtMultiplierValue.Value = System.Convert.ToDouble((s.MultiplierValue ?? 0));
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {

        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new ParamedicFeeRemunByIdiSettings());
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
            auditLogFilter.PrimaryKeyData = string.Format("SettingId='{0}'", hfId.Value);
            auditLogFilter.TableName = "ParamedicFeeRemunByIdiSettings";
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            if (newVal != AppEnum.DataMode.Read)
            {
                
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Url Search & List
            UrlPageSearch = "ParamedicFeeRemunByIdiSettingSearch.aspx";
            UrlPageList = "ParamedicFeeRemunByIdiSettingList.aspx";

            WindowSearch.Height = 500;

            ProgramID = AppConstant.Program.PhysicianFeeRemunByIdiSetting;

            // search tidak diperlukan disini
            ToolBarMenuSearch.Visible = false;

            //StandardReference Initialize
            if (!IsPostBack)
            {
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
                smfColl.LoadAll();
                cboSmf.Items.Clear();
                cboSmf.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (var smf in smfColl)
                {
                    cboSmf.Items.Add(new RadComboBoxItem(smf.SmfName, smf.SmfID));
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
            }
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new ParamedicFeeRemunByIdiSettings();
            entity.LoadByPrimaryKey(System.Convert.ToInt32(hfId.Value));
            entity.MarkAsDeleted();
            SaveEntity(entity);
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new ParamedicFeeRemunByIdiSettings();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        private void SaveEntity(ParamedicFeeRemunByIdiSettings entity)
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
            var entity = new ParamedicFeeRemunByIdiSettings();
            if (entity.LoadByPrimaryKey(System.Convert.ToInt32(hfId.Value)))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
        }

        #endregion
       
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
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString() + " (" + ((DataRowView)e.Item.DataItem)["ItemGroupName"].ToString() + ")";
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }
    }
}
