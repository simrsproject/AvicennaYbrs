using System;
using System.Data;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class ParamedicFeeByFee4ServiceSettingSearchV2 : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.PhysicianFeeByServiceSetting;

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSpecialty, AppEnum.StandardReference.Specialty);
                StandardReference.InitializeIncludeSpace(cboSRRegistrationType, AppEnum.StandardReference.RegistrationType);

                StandardReference.InitializeIncludeSpace(cboTariffType, AppEnum.StandardReference.TariffType);
                StandardReference.InitializeIncludeSpace(cboGuarantorType, AppEnum.StandardReference.GuarantorType);

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

                StandardReference.InitializeIncludeSpace(cboSRProcedure, AppEnum.StandardReference.Procedure);
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
            var query = new ParamedicFeeByFee4ServiceSettingQuery("a");
            var par = new ParamedicQuery("par");
            var pStat = new AppStandardReferenceItemQuery("pStat");
            var pSpec = new AppStandardReferenceItemQuery("pSpec");
            var rType = new AppStandardReferenceItemQuery("rType");
            var tType = new AppStandardReferenceItemQuery("tType");
            var g = new GuarantorQuery("g");
            var gType = new AppStandardReferenceItemQuery("gType");
            var su = new ServiceUnitQuery("su");
            var iGroup = new ItemGroupQuery("iGroup");
            var i = new ItemQuery("i");
            var tc = new TariffComponentQuery("tc");
            var srProc = new AppStandardReferenceItemQuery("srProc");
            var cl = new ClassQuery("cl");

            query.LeftJoin(par).On(query.ParamedicID.Equal(par.ParamedicID))
                .LeftJoin(pStat).On(query.SRParamedicStatus.Equal(pStat.ItemID) && pStat.StandardReferenceID.Equal(AppEnum.StandardReference.ParamedicStatus))
                .LeftJoin(pSpec).On(query.SRSpecialty.Equal(pSpec.ItemID) && pSpec.StandardReferenceID.Equal(AppEnum.StandardReference.Specialty))
                .LeftJoin(rType).On(query.SRRegistrationType.Equal(rType.ItemID) && rType.StandardReferenceID.Equal(AppEnum.StandardReference.RegistrationType))
                .LeftJoin(tType).On(query.SRTariffType.Equal(tType.ItemID) && tType.StandardReferenceID.Equal(AppEnum.StandardReference.TariffType))
                .LeftJoin(cl).On(query.ClassID.Equal(cl.ClassID))
                .LeftJoin(g).On(query.GuarantorID.Equal(g.GuarantorID))
                .LeftJoin(gType).On(query.SRGuarantorType.Equal(gType.ItemID) && gType.StandardReferenceID.Equal(AppEnum.StandardReference.GuarantorType))
                .LeftJoin(su).On(query.ServiceUnitID.Equal(su.ServiceUnitID))
                .LeftJoin(iGroup).On(query.ItemGroupID.Equal(iGroup.ItemGroupID))
                .LeftJoin(i).On(query.ItemID.Equal(i.ItemID))
                .LeftJoin(tc).On(query.TariffComponentID.Equal(tc.TariffComponentID))
                .LeftJoin(srProc).On(query.SRProcedure.Equal(srProc.ItemID) && srProc.StandardReferenceID.Equal(AppEnum.StandardReference.Procedure))
                .Where(query.Level == 1)
                .Select
                (
                    query,
                    par.ParamedicName,
                    pStat.ItemName.As("ParamedicStatusName"),
                    pSpec.ItemName.As("SpecialtyName"),
                    rType.ItemName.As("RegistrationTypeName"),
                    tType.ItemName.As("TariffTypeName"),
                    cl.ClassName.As("ClassName"),
                    gType.ItemName.As("GuarantorTypeName"),
                    g.GuarantorName,
                    su.ServiceUnitName,
                    iGroup.ItemGroupName,
                    i.ItemName,
                    srProc.ItemName.As("ProcedureName"),
                    tc.TariffComponentName
                )
                .OrderBy(
                    pStat.ItemName.Ascending,
                    par.ParamedicName.Ascending,
                    pSpec.ItemName.Ascending,
                    rType.ItemName.Ascending,
                    tType.ItemName.Ascending,
                    cl.ClassName.Ascending,
                    gType.ItemName.Ascending,
                    g.GuarantorName.Ascending,
                    su.ServiceUnitName.Ascending,
                    iGroup.ItemGroupName.Ascending,
                    i.ItemName.Ascending,
                    srProc.ItemName.Ascending,
                    tc.TariffComponentName.Ascending
                );

            if (txtID.Value.HasValue)
            {
                query.Where(query.Id == txtID.Value.Value);
            }
            if (!string.IsNullOrEmpty(cboParamedicStatus.SelectedValue))
            {
                query.Where(query.SRParamedicStatus.Like(string.Format("{0}", cboParamedicStatus.SelectedValue)));
            }
            if (!string.IsNullOrEmpty(cboParamedic.SelectedValue))
            {
                query.Where(query.ParamedicID.Like(string.Format("{0}", cboParamedic.SelectedValue)));
            }
            if (!string.IsNullOrEmpty(cboSpecialty.SelectedValue))
            {
                query.Where(query.SRSpecialty.Like(string.Format("{0}", cboSpecialty.SelectedValue)));
            }
            if (!string.IsNullOrEmpty(cboSRRegistrationType.SelectedValue))
            {
                query.Where(query.SRRegistrationType.Like(string.Format("{0}", cboSRRegistrationType.SelectedValue)));
            }
            if (!string.IsNullOrEmpty(cboTariffType.SelectedValue))
            {
                query.Where(query.SRTariffType.Like(string.Format("{0}", cboTariffType.SelectedValue)));
            }
            if (!string.IsNullOrEmpty(cboClass.SelectedValue))
            {
                query.Where(query.ClassID.Like(string.Format("{0}", cboClass.SelectedValue)));
            }
            if (!string.IsNullOrEmpty(cboGuarantorType.SelectedValue))
            {
                query.Where(query.SRGuarantorType.Like(string.Format("{0}", cboGuarantorType.SelectedValue)));
            }
            if (!string.IsNullOrEmpty(cboGuarantor.SelectedValue))
            {
                query.Where(query.GuarantorID.Like(string.Format("{0}", cboGuarantor.SelectedValue)));
            }
            if (!string.IsNullOrEmpty(cboServiceUnit.SelectedValue))
            {
                query.Where(query.ServiceUnitID.Like(string.Format("{0}", cboServiceUnit.SelectedValue)));
            }
            if (!string.IsNullOrEmpty(cboItemGroup.SelectedValue))
            {
                query.Where(query.ItemGroupID.Like(string.Format("{0}", cboItemGroup.SelectedValue)));
            }
            if (!string.IsNullOrEmpty(cboItem.SelectedValue))
            {
                query.Where(query.ItemID.Like(string.Format("{0}", cboItem.SelectedValue)));
            }
            if (!string.IsNullOrEmpty(cboSRProcedure.SelectedValue))
            {
                query.Where(query.SRProcedure.Like(string.Format("{0}", cboSRProcedure.SelectedValue)));
            }

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }

        protected void cboServiceUnit_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var su = new ServiceUnitQuery("a");
            su.Where(su.IsActive.Equal(true));
            if (cboFilterServiceUnit.SelectedIndex == 1)
                su.Where(su.Or(su.ServiceUnitID.Like(e.Text + "%"), su.ServiceUnitName.Like(e.Text + "%")));
            else
                su.Where(su.Or(su.ServiceUnitID.Like("%" + e.Text + "%"), su.ServiceUnitName.Like("%" + e.Text + "%")));
            su.Select(su.ServiceUnitID, su.ServiceUnitName);
            su.es.Top = 20;

            DataTable tbl = su.LoadDataTable();
            var r = tbl.NewRow();
            r["ServiceUnitID"] = r["ServiceUnitName"] = string.Empty;
            tbl.Rows.InsertAt(r, 0);
            cboServiceUnit.DataSource = tbl;
            cboServiceUnit.DataBind();
        }

        protected void cboServiceUnit_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ServiceUnitName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ServiceUnitID"].ToString();
        }

        protected void cboFilterServiceUnit_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboServiceUnit.Items.Clear();
            cboServiceUnit.SelectedValue = string.Empty;
            cboServiceUnit.Text = string.Empty;
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

        protected void cboParamedic_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var par = new ParamedicQuery("a");
            par.Where(par.IsActive.Equal(true), par.ParamedicFee.Equal(true));
            if (cboFilterParamedic.SelectedIndex == 1)
                par.Where(par.Or(par.ParamedicID.Like(e.Text + "%"), par.ParamedicName.Like(e.Text + "%")));
            else
                par.Where(par.Or(par.ParamedicID.Like("%" + e.Text + "%"), par.ParamedicName.Like("%" + e.Text + "%")));
            par.Select(par.ParamedicID, par.ParamedicName);
            par.es.Top = 20;

            DataTable tbl = par.LoadDataTable();
            var r = tbl.NewRow();
            r["ParamedicID"] = r["ParamedicName"] = string.Empty;
            tbl.Rows.InsertAt(r, 0);
            cboParamedic.DataSource = tbl;
            cboParamedic.DataBind();
        }

        protected void cboParamedic_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ParamedicName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ParamedicID"].ToString();
        }

        protected void cboFilterParamedic_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboParamedic.Items.Clear();
            cboParamedic.SelectedValue = string.Empty;
            cboParamedic.Text = string.Empty;
        }

        protected void cboGuarantor_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var gua = new GuarantorQuery("a");
            gua.Where(gua.IsActive.Equal(true));
            if (cboFilterGuarantor.SelectedIndex == 1)
                gua.Where(gua.Or(gua.GuarantorID.Like(e.Text + "%"), gua.GuarantorName.Like(e.Text + "%")));
            else
                gua.Where(gua.Or(gua.GuarantorID.Like("%" + e.Text + "%"), gua.GuarantorName.Like("%" + e.Text + "%")));
            gua.Select(gua.GuarantorID, gua.GuarantorName);
            gua.es.Top = 20;

            DataTable tbl = gua.LoadDataTable();
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

        protected void cboFilterGuarantor_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboGuarantor.Items.Clear();
            cboGuarantor.SelectedValue = string.Empty;
            cboGuarantor.Text = string.Empty;
        }

        protected void cboItemGroup_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var ig = new ItemGroupQuery("a");
            ig.Where(ig.IsActive.Equal(true));
            if (cboFilterItemGroup.SelectedIndex == 1)
                ig.Where(ig.Or(ig.ItemGroupID.Like(e.Text + "%"), ig.ItemGroupName.Like(e.Text + "%")));
            else
                ig.Where(ig.Or(ig.ItemGroupID.Like("%" + e.Text + "%"), ig.ItemGroupName.Like("%" + e.Text + "%")));
            ig.Select(ig.ItemGroupID, ig.ItemGroupName);
            ig.es.Top = 20;

            DataTable tbl = ig.LoadDataTable();
            var r = tbl.NewRow();
            r["ItemGroupID"] = r["ItemGroupName"] = string.Empty;
            tbl.Rows.InsertAt(r, 0);
            cboItemGroup.DataSource = tbl;
            cboItemGroup.DataBind();
        }

        protected void cboItemGroup_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemGroupName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemGroupID"].ToString();
        }

        protected void cboFilterItemGroup_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboItemGroup.Items.Clear();
            cboItemGroup.SelectedValue = string.Empty;
            cboItemGroup.Text = string.Empty;
        }

        protected void cboSRProcedure_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var std = new AppStandardReferenceItemQuery("a");
            std.Where(std.StandardReferenceID.Equal("Procedure"), std.IsActive.Equal(true));
            if (cboFilterSRProcedure.SelectedIndex == 1)
                std.Where(std.Or(std.ItemID.Like(e.Text + "%"), std.ItemName.Like(e.Text + "%")));
            else
                std.Where(std.Or(std.ItemID.Like("%" + e.Text + "%"), std.ItemName.Like("%" + e.Text + "%")));
            std.Select(std.ItemID, std.ItemName);
            std.es.Top = 20;

            DataTable tbl = std.LoadDataTable();
            var r = tbl.NewRow();
            r["ItemID"] = r["ItemName"] = string.Empty;
            tbl.Rows.InsertAt(r, 0);
            cboSRProcedure.DataSource = tbl;
            cboSRProcedure.DataBind();
        }

        protected void cboSRProcedure_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void cboFilterSRProcedure_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSRProcedure.Items.Clear();
            cboSRProcedure.SelectedValue = string.Empty;
            cboSRProcedure.Text = string.Empty;
        }
    }
}
