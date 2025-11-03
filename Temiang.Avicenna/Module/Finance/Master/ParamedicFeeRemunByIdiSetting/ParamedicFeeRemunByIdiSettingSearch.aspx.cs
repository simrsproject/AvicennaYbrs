using System;
using System.Data;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class ParamedicFeeRemunByIdiSettingSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.PhysicianFeeRemunByIdiSetting;

            if (!IsPostBack)
            {
                var parColl = new ParamedicCollection();
                parColl.Query.Where(parColl.Query.IsActive.Equal(true),
                    parColl.Query.ParamedicFee.Equal(true));
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
                igColl.Query.Where(igColl.Query.IsActive == true);
                igColl.LoadAll();

                cboItem.Items.Clear();
                cboItemGroup.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (ItemGroup c in igColl)
                {
                    cboItemGroup.Items.Add(new RadComboBoxItem(c.ItemGroupName, c.ItemGroupID));
                }
            }
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
                RestoreValueFromCookie();
        }
        public override bool OnButtonOkClicked()
        {
            var query = new ParamedicFeeRemunByIdiSettingsQuery("a");
            var smf = new SmfQuery("smf");
            var par = new ParamedicQuery("par");
            var iGroup = new ItemGroupQuery("iGroup");
            var i = new ItemQuery("i");

            query.LeftJoin(par).On(query.ParamedicID.Equal(par.ParamedicID))
                .LeftJoin(smf).On(query.SmfID.Equal(smf.SmfID))
                .LeftJoin(iGroup).On(query.ItemGroupID.Equal(iGroup.ItemGroupID))
                .LeftJoin(i).On(query.ItemID.Equal(i.ItemID))
                .Select
                (
                    query,
                    par.ParamedicName,
                    smf.SmfName,
                    iGroup.ItemGroupName,
                    i.ItemName
                )
                .OrderBy(
                    smf.SmfName.Ascending,
                    par.ParamedicName.Ascending,
                    iGroup.ItemGroupName.Ascending,
                    i.ItemName.Ascending
                );
            if (txtID.Value.HasValue)
            {
                query.Where(query.SettingID == txtID.Value.Value);
            }
            if (!string.IsNullOrEmpty(cboSmf.SelectedValue))
            {
                query.Where(query.SmfID.Like(string.Format("{0}", cboSmf.SelectedValue)));
            }
            if (!string.IsNullOrEmpty(cboParamedic.SelectedValue))
            {
                query.Where(query.ParamedicID.Like(string.Format("{0}", cboParamedic.SelectedValue)));
            }
            if (!string.IsNullOrEmpty(cboItemGroup.SelectedValue))
            {
                query.Where(query.ItemGroupID.Like(string.Format("{0}", cboItemGroup.SelectedValue)));
            }
            if (!string.IsNullOrEmpty(cboItem.SelectedValue))
            {
                query.Where(query.ItemID.Like(string.Format("{0}", cboItem.SelectedValue)));
            }

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
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
                i.IsActive == true
            );
            if (cboFilterItem.SelectedIndex == 1)
                i.Where(i.Or(i.ItemID.Like(e.Text + "%"), i.ItemName.Like(e.Text + "%")));
            else
                i.Where(i.Or(i.ItemID.Like("%" + e.Text + "%"), i.ItemName.Like("%" + e.Text + "%")));
            i.Select(i.ItemID, i.ItemName)
            .OrderBy(i.ItemName.Ascending);
            i.es.Top = 20;

            DataTable tbl = i.LoadDataTable();
            var r = tbl.NewRow();
            r["ItemID"] = r["ItemName"] = string.Empty;
            tbl.Rows.InsertAt(r, 0);
            cboItem.DataSource = tbl;
            cboItem.DataBind();
        }

        protected void cboItem_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void cboFilterItem_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboItem.Items.Clear();
            cboItem.SelectedValue = string.Empty;
            cboItem.Text = string.Empty;
        }
    }
}
