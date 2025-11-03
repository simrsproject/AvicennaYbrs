using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class ParamedicFeeByFee4ServiceSettingMultiLevelList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            UrlPageSearch = "ParamedicFeeByFee4ServiceSettingMultiLevelSearch.aspx";
            UrlPageDetail = "ParamedicFeeByFee4ServiceSettingMultiLevelDetail.aspx";

            WindowSearch.Height = 500;

            ProgramID = AppConstant.Program.PhysicianFeeByServiceSetting;

            ToolBarMenuEdit.Visible = false;
            ToolBarMenuView.Visible = false;

            if (!Page.IsPostBack) {
                FeeSetting = null;
            }

            var lvls = FeeSetting.AsEnumerable().Select(fs => fs.Field<int>("Level")).Distinct().OrderBy(fs => fs).ToArray();

            foreach (var lvl in lvls) {
                if (lvl == 1) continue;

                var pv = new RadPageView();
                pv.ID = "pageView_" + lvl.ToString();
                multiPage.PageViews.Add(pv);

                var tab = new RadTab("Level " + lvl.ToString());
                tab.PageViewID = pv.ID;
                tabStrip.Tabs.Add(tab);

                var grd = new RadGrid();
                grd.ID = "grdList_" + lvl.ToString();
                foreach (GridColumn c in grdList.Columns)
                {
                    var gc = c.Clone();
                    grd.Columns.Add(gc);
                }
                pv.Controls.Add(grd);
                grd.NeedDataSource += grdList_NeedDataSource;
                grd.PreRender += grdList_PreRender;

                grd.AllowPaging = true;
                grd.PageSize = 18;
                grd.AllowSorting = true;
                grd.AutoGenerateColumns = false;
                grd.ClientSettings.Selecting.AllowRowSelect = true;
                grd.ClientSettings.Resizing.AllowColumnResize = true;
                grd.PagerStyle.Mode = GridPagerMode.NextPrevNumericAndAdvanced;
                grd.ShowStatusBar = true;
            }
        }

        protected override void OnInitComplete(EventArgs e)
        {
            base.OnInitComplete(e);
            grdList.PageSize = 100;
        }

        public override void OnMenuEditClick(GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "edit");
        }

        public override void OnMenuViewClick(GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "view");
        }

        private void RedirectToPageDetail(GridDataItem dataItem, string mode)
        {
            string id = dataItem.GetDataKeyValue(ParamedicFeeByFee4ServiceSettingMetadata.ColumnNames.Id).ToString();
            string url = string.Format("{0}?md={1}&id={2}", UrlPageDetail, mode, id);
            Page.Response.Redirect(url, true);
        }

        protected void grdList_PreRender(object sender, EventArgs e)
        {
            var grd = ((RadGrid)sender);

            for (int rowIndex = grd.Items.Count - 2; rowIndex >= 0; rowIndex--)
            {
                GridDataItem row = grd.Items[rowIndex];
                GridDataItem previousRow = grd.Items[rowIndex + 1];

                if (row["ParamedicStatusName"].Text == previousRow["ParamedicStatusName"].Text)
                {
                    row["ParamedicStatusName"].RowSpan = previousRow["ParamedicStatusName"].RowSpan < 2 ?
                        2 : previousRow["ParamedicStatusName"].RowSpan + 1;
                    previousRow["ParamedicStatusName"].Visible = false;
                }

                if (row["ParamedicStatusName"].Text == previousRow["ParamedicStatusName"].Text &&
                    row["ParamedicName"].Text == previousRow["ParamedicName"].Text)
                {
                    row["ParamedicName"].RowSpan = previousRow["ParamedicName"].RowSpan < 2 ?
                        2 : previousRow["ParamedicName"].RowSpan + 1;
                    previousRow["ParamedicName"].Visible = false;
                }

                if (row["ParamedicStatusName"].Text == previousRow["ParamedicStatusName"].Text &&
                    row["ParamedicName"].Text == previousRow["ParamedicName"].Text &&
                    row["SpecialtyName"].Text == previousRow["SpecialtyName"].Text)
                {
                    row["SpecialtyName"].RowSpan = previousRow["SpecialtyName"].RowSpan < 2 ?
                        2 : previousRow["SpecialtyName"].RowSpan + 1;
                    previousRow["SpecialtyName"].Visible = false;
                }

                if (row["ParamedicStatusName"].Text == previousRow["ParamedicStatusName"].Text &&
                    row["ParamedicName"].Text == previousRow["ParamedicName"].Text &&
                    row["SpecialtyName"].Text == previousRow["SpecialtyName"].Text &&
                    row["RegistrationTypeName"].Text == previousRow["RegistrationTypeName"].Text)
                {
                    row["RegistrationTypeName"].RowSpan = previousRow["RegistrationTypeName"].RowSpan < 2 ?
                        2 : previousRow["RegistrationTypeName"].RowSpan + 1;
                    previousRow["RegistrationTypeName"].Visible = false;
                }

                if (row["ParamedicStatusName"].Text == previousRow["ParamedicStatusName"].Text &&
                    row["ParamedicName"].Text == previousRow["ParamedicName"].Text &&
                    row["SpecialtyName"].Text == previousRow["SpecialtyName"].Text &&
                    row["RegistrationTypeName"].Text == previousRow["RegistrationTypeName"].Text &&
                    row["TariffTypeName"].Text == previousRow["TariffTypeName"].Text)
                {
                    row["TariffTypeName"].RowSpan = previousRow["TariffTypeName"].RowSpan < 2 ?
                        2 : previousRow["TariffTypeName"].RowSpan + 1;
                    previousRow["TariffTypeName"].Visible = false;
                }
                //
                if (row["ParamedicStatusName"].Text == previousRow["ParamedicStatusName"].Text &&
                    row["ParamedicName"].Text == previousRow["ParamedicName"].Text &&
                    row["SpecialtyName"].Text == previousRow["SpecialtyName"].Text &&
                    row["RegistrationTypeName"].Text == previousRow["RegistrationTypeName"].Text &&
                    row["TariffTypeName"].Text == previousRow["TariffTypeName"].Text &&
                    row["ClassName"].Text == previousRow["ClassName"].Text)
                {
                    row["ClassName"].RowSpan = previousRow["ClassName"].RowSpan < 2 ?
                        2 : previousRow["ClassName"].RowSpan + 1;
                    previousRow["ClassName"].Visible = false;
                }
                //
                if (row["ParamedicStatusName"].Text == previousRow["ParamedicStatusName"].Text &&
                    row["ParamedicName"].Text == previousRow["ParamedicName"].Text &&
                   row["SpecialtyName"].Text == previousRow["SpecialtyName"].Text &&
                   row["RegistrationTypeName"].Text == previousRow["RegistrationTypeName"].Text &&
                   row["TariffTypeName"].Text == previousRow["TariffTypeName"].Text &&
                   row["ClassName"].Text == previousRow["ClassName"].Text &&
                   row["GuarantorTypeName"].Text == previousRow["GuarantorTypeName"].Text)
                {
                    row["GuarantorTypeName"].RowSpan = previousRow["GuarantorTypeName"].RowSpan < 2 ?
                        2 : previousRow["GuarantorTypeName"].RowSpan + 1;
                    previousRow["GuarantorTypeName"].Visible = false;
                }
                //
                if (row["ParamedicStatusName"].Text == previousRow["ParamedicStatusName"].Text &&
                    row["ParamedicName"].Text == previousRow["ParamedicName"].Text &&
                    row["SpecialtyName"].Text == previousRow["SpecialtyName"].Text &&
                    row["RegistrationTypeName"].Text == previousRow["RegistrationTypeName"].Text &&
                    row["TariffTypeName"].Text == previousRow["TariffTypeName"].Text &&
                    row["ClassName"].Text == previousRow["ClassName"].Text &&
                    row["GuarantorTypeName"].Text == previousRow["GuarantorTypeName"].Text &&
                    row["GuarantorName"].Text == previousRow["GuarantorName"].Text)
                {
                    row["GuarantorName"].RowSpan = previousRow["GuarantorName"].RowSpan < 2 ?
                        2 : previousRow["GuarantorName"].RowSpan + 1;
                    previousRow["GuarantorName"].Visible = false;
                }

                if (row["ParamedicStatusName"].Text == previousRow["ParamedicStatusName"].Text &&
                    row["ParamedicName"].Text == previousRow["ParamedicName"].Text &&
                    row["SpecialtyName"].Text == previousRow["SpecialtyName"].Text &&
                    row["RegistrationTypeName"].Text == previousRow["RegistrationTypeName"].Text &&
                    row["TariffTypeName"].Text == previousRow["TariffTypeName"].Text &&
                    row["ClassName"].Text == previousRow["ClassName"].Text &&
                    row["GuarantorTypeName"].Text == previousRow["GuarantorTypeName"].Text &&
                    row["GuarantorName"].Text == previousRow["GuarantorName"].Text &&
                    row["ServiceUnitName"].Text == previousRow["ServiceUnitName"].Text)
                {
                    row["ServiceUnitName"].RowSpan = previousRow["ServiceUnitName"].RowSpan < 2 ?
                        2 : previousRow["ServiceUnitName"].RowSpan + 1;
                    previousRow["ServiceUnitName"].Visible = false;
                }

                if (row["ParamedicStatusName"].Text == previousRow["ParamedicStatusName"].Text &&
                   row["ParamedicName"].Text == previousRow["ParamedicName"].Text &&
                   row["SpecialtyName"].Text == previousRow["SpecialtyName"].Text &&
                   row["RegistrationTypeName"].Text == previousRow["RegistrationTypeName"].Text &&
                   row["TariffTypeName"].Text == previousRow["TariffTypeName"].Text &&
                   row["ClassName"].Text == previousRow["ClassName"].Text &&
                   row["GuarantorTypeName"].Text == previousRow["GuarantorTypeName"].Text &&
                   row["GuarantorName"].Text == previousRow["GuarantorName"].Text &&
                   row["ServiceUnitName"].Text == previousRow["ServiceUnitName"].Text &&
                   row["ItemGroupName"].Text == previousRow["ItemConditionRuleTypeName"].Text)
                {
                    row["ItemConditionRuleTypeName"].RowSpan = previousRow["ItemConditionRuleTypeName"].RowSpan < 2 ?
                        2 : previousRow["ItemConditionRuleTypeName"].RowSpan + 1;
                    previousRow["ItemConditionRuleTypeName"].Visible = false;
                }

                if (row["ParamedicStatusName"].Text == previousRow["ParamedicStatusName"].Text &&
                  row["ParamedicName"].Text == previousRow["ParamedicName"].Text &&
                  row["SpecialtyName"].Text == previousRow["SpecialtyName"].Text &&
                  row["RegistrationTypeName"].Text == previousRow["RegistrationTypeName"].Text &&
                  row["TariffTypeName"].Text == previousRow["TariffTypeName"].Text &&
                  row["ClassName"].Text == previousRow["ClassName"].Text &&
                  row["GuarantorTypeName"].Text == previousRow["GuarantorTypeName"].Text &&
                  row["GuarantorName"].Text == previousRow["GuarantorName"].Text &&
                  row["ServiceUnitName"].Text == previousRow["ServiceUnitName"].Text &&
                  row["ItemGroupName"].Text == previousRow["ItemConditionRuleTypeName"].Text &&
                  row["ItemGroupName"].Text == previousRow["ItemConditionRuleName"].Text)
                {
                    row["ItemConditionRuleName"].RowSpan = previousRow["ItemConditionRuleName"].RowSpan < 2 ?
                        2 : previousRow["ItemConditionRuleName"].RowSpan + 1;
                    previousRow["ItemConditionRuleName"].Visible = false;
                }

                if (row["ParamedicStatusName"].Text == previousRow["ParamedicStatusName"].Text &&
                    row["ParamedicName"].Text == previousRow["ParamedicName"].Text &&
                    row["SpecialtyName"].Text == previousRow["SpecialtyName"].Text &&
                    row["RegistrationTypeName"].Text == previousRow["RegistrationTypeName"].Text &&
                    row["TariffTypeName"].Text == previousRow["TariffTypeName"].Text &&
                    row["ClassName"].Text == previousRow["ClassName"].Text &&
                    row["GuarantorTypeName"].Text == previousRow["GuarantorTypeName"].Text &&
                    row["GuarantorName"].Text == previousRow["GuarantorName"].Text &&
                    row["ServiceUnitName"].Text == previousRow["ServiceUnitName"].Text &&
                    row["ItemGroupName"].Text == previousRow["ItemConditionRuleTypeName"].Text &&
                    row["ItemGroupName"].Text == previousRow["ItemConditionRuleName"].Text &&
                    row["ItemGroupName"].Text == previousRow["ItemGroupName"].Text)
                {
                    row["ItemGroupName"].RowSpan = previousRow["ItemGroupName"].RowSpan < 2 ?
                        2 : previousRow["ItemGroupName"].RowSpan + 1;
                    previousRow["ItemGroupName"].Visible = false;
                }

                if (row["ParamedicStatusName"].Text == previousRow["ParamedicStatusName"].Text &&
                    row["ParamedicName"].Text == previousRow["ParamedicName"].Text &&
                    row["SpecialtyName"].Text == previousRow["SpecialtyName"].Text &&
                    row["RegistrationTypeName"].Text == previousRow["RegistrationTypeName"].Text &&
                    row["TariffTypeName"].Text == previousRow["TariffTypeName"].Text &&
                    row["ClassName"].Text == previousRow["ClassName"].Text &&
                    row["GuarantorTypeName"].Text == previousRow["GuarantorTypeName"].Text &&
                    row["GuarantorName"].Text == previousRow["GuarantorName"].Text &&
                    row["ServiceUnitName"].Text == previousRow["ServiceUnitName"].Text &&
                    row["ItemGroupName"].Text == previousRow["ItemConditionRuleTypeName"].Text &&
                    row["ItemGroupName"].Text == previousRow["ItemConditionRuleName"].Text &&
                    row["ItemGroupName"].Text == previousRow["ItemGroupName"].Text &&
                    row["ItemName"].Text == previousRow["ItemName"].Text)
                {
                    row["ItemName"].RowSpan = previousRow["ItemName"].RowSpan < 2 ?
                        2 : previousRow["ItemName"].RowSpan + 1;
                    previousRow["ItemName"].Visible = false;
                }
            }

            var cCount = new int[grd.Columns.Count];
            foreach (GridDataItem g in grdList.MasterTableView.Items)
            {
                foreach (GridColumn c in grd.Columns)
                {
                    cCount[grd.Columns.IndexOf(c)] += string.IsNullOrEmpty(g[c.UniqueName].Text.Replace("&nbsp;", "")) ? 0 : 1;
                    if (c.UniqueName == "cedit") cCount[grd.Columns.IndexOf(c)] = 1;
                    if (c.UniqueName == "cview") cCount[grd.Columns.IndexOf(c)] = 1;
                }
            }

            foreach (GridColumn c in grd.Columns)
            {
                c.Visible = cCount[grd.Columns.IndexOf(c)] > 0;
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var grd = ((RadGrid)source);
            var lvl = 1;
            var nPart = grd.ID.Split('_');
            if (nPart.Length > 1) lvl = System.Convert.ToInt32(nPart[1]);

            var dtb = new DataTable();
            var filtered = FeeSetting.AsEnumerable().Where(f => f.Field<int>("Level") == lvl);
            if (filtered.Any()) {
                dtb = filtered.CopyToDataTable();
            }

            grd.DataSource = dtb;
        }

        protected void grdList_DataBound(object sender, EventArgs e)
        {

        }

        private DataTable FeeSetting
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                ParamedicFeeByFee4ServiceSettingQuery query;

                if (Session[SessionNameForQuery] != null)
                    query = (ParamedicFeeByFee4ServiceSettingQuery)Session[SessionNameForQuery];
                else
                {
                    query = new ParamedicFeeByFee4ServiceSettingQuery("a");
                    var par = new ParamedicQuery("par");
                    var pType = new AppStandardReferenceItemQuery("pType");
                    var smf = new SmfQuery("smf");
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
                    var crType = new AppStandardReferenceItemQuery("crType");
                    var icr = new ItemConditionRuleQuery("icr");

                    query.LeftJoin(par).On(query.ParamedicID.Equal(par.ParamedicID))
                        .LeftJoin(pType).On(query.SRParamedicStatus.Equal(pType.ItemID) && pType.StandardReferenceID.Equal(AppEnum.StandardReference.ParamedicStatus))
                        .LeftJoin(smf).On(query.SRSpecialty.Equal(smf.SmfID))
                        .LeftJoin(rType).On(query.SRRegistrationType.Equal(rType.ItemID) && rType.StandardReferenceID.Equal(AppEnum.StandardReference.RegistrationType))
                        .LeftJoin(tType).On(query.SRTariffType.Equal(tType.ItemID) && tType.StandardReferenceID.Equal(AppEnum.StandardReference.TariffType))
                        .LeftJoin(cl).On(query.ClassID.Equal(cl.ClassID))
                        .LeftJoin(g).On(query.GuarantorID.Equal(g.GuarantorID))
                        .LeftJoin(gType).On(query.SRGuarantorType.Equal(gType.ItemID) && gType.StandardReferenceID.Equal(AppEnum.StandardReference.GuarantorType))
                        .LeftJoin(su).On(query.ServiceUnitID.Equal(su.ServiceUnitID))
                        .LeftJoin(crType).On(query.SRItemConditionRuleType.Equal(crType.ItemID) && crType.StandardReferenceID.Equal(AppEnum.StandardReference.ItemConditionRuleType))
                        .LeftJoin(icr).On(query.ItemConditionRuleID.Equal(icr.ItemConditionRuleID))
                        .LeftJoin(iGroup).On(query.ItemGroupID.Equal(iGroup.ItemGroupID))
                        .LeftJoin(i).On(query.ItemID.Equal(i.ItemID))
                        .LeftJoin(tc).On(query.TariffComponentID.Equal(tc.TariffComponentID))
                        .LeftJoin(srProc).On(query.SRProcedure.Equal(srProc.ItemID) && srProc.StandardReferenceID.Equal(AppEnum.StandardReference.Procedure))
                        //.Where(query.Level == 2)
                        .Select
                        (
                            query,
                            "<case a.IsFeeValueInPercent when 1 then '%' else 'Rp' end unit>",
                            par.ParamedicName,
                            pType.ItemName.As("ParamedicStatusName"),
                            smf.SmfName.As("SpecialtyName"),
                            rType.ItemName.As("RegistrationTypeName"),
                            tType.ItemName.As("TariffTypeName"),
                            cl.ClassName.As("ClassName"),
                            gType.ItemName.As("GuarantorTypeName"),
                            g.GuarantorName,
                            su.ServiceUnitName,
                            crType.ItemName.As("ItemConditionRuleTypeName"),
                            icr.ItemConditionRuleName,
                            iGroup.ItemGroupName,
                            i.ItemName,
                            srProc.ItemName.As("ProcedureName"),
                            tc.TariffComponentName
                        )
                        .OrderBy(
                            pType.ItemName.Ascending,
                            par.ParamedicName.Ascending,
                            smf.SmfName.Ascending,
                            rType.ItemName.Ascending,
                            tType.ItemName.Ascending,
                            cl.ClassName.Ascending,
                            gType.ItemName.Ascending,
                            g.GuarantorName.Ascending,
                            su.ServiceUnitName.Ascending,
                            crType.ItemName.Ascending,
                            icr.ItemConditionRuleName.Ascending,
                            iGroup.ItemGroupName.Ascending,
                            i.ItemName.Ascending,
                            srProc.ItemName.Ascending,
                            tc.TariffComponentName.Ascending
                        );
                }
                //query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }
            set {
                this.Session[SessionNameForList] = value;
                Session[SessionNameForQuery] = null;

            }
        }
    }
}
