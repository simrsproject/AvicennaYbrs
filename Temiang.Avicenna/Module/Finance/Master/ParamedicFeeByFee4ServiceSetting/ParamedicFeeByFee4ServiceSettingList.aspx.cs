using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class ParamedicFeeByFee4ServiceSettingList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            //UrlPageSearch = "ParamedicFeeByFee4ServiceSettingSearch.aspx";
            UrlPageSearch = "ParamedicFeeByFee4ServiceSettingSearchV2.aspx";
            UrlPageDetail = "ParamedicFeeByFee4ServiceSettingDetail.aspx";

            WindowSearch.Height = 500;

            ProgramID = AppConstant.Program.PhysicianFeeByServiceSetting;
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
            //foreach (GridDataItem g in grdList.MasterTableView.Items)
            //{
            //    var dr = g.DataItem as DataRow;
            //    if ((bool)(g.DataItem as DataRowView)["IsFeeValueInPercent"])
            //    {
            //        RadTextBox x = (RadTextBox)g["colFeeValue"].FindControl("txtFeeValue");
            //        if (x != null)
            //        {
            //            x.Text = g["FeeValue"].Text + "%";
            //        }
            //    }
            //}

            for (int rowIndex = grdList.Items.Count - 2; rowIndex >= 0; rowIndex--)
            {
                GridDataItem row = grdList.Items[rowIndex];
                GridDataItem previousRow = grdList.Items[rowIndex + 1];

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
                    row["ItemGroupName"].Text == previousRow["ItemGroupName"].Text &&
                    row["ItemName"].Text == previousRow["ItemName"].Text)
                {
                    row["ItemName"].RowSpan = previousRow["ItemName"].RowSpan < 2 ?
                        2 : previousRow["ItemName"].RowSpan + 1;
                    previousRow["ItemName"].Visible = false;
                }
            }

            var cCount = new int[grdList.Columns.Count];
            foreach (GridDataItem g in grdList.MasterTableView.Items)
            {
                foreach (GridColumn c in grdList.Columns)
                {
                    cCount[grdList.Columns.IndexOf(c)] += string.IsNullOrEmpty(g[c.UniqueName].Text.Replace("&nbsp;", "")) ? 0 : 1;
                }
            }

            foreach (GridColumn c in grdList.Columns)
            {
                c.Visible = cCount[grdList.Columns.IndexOf(c)] > 0;
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = FeeSetting;
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
                        .LeftJoin(pType).On(query.SRParamedicStatus.Equal(pType.ItemID) && pType.StandardReferenceID.Equal(AppEnum.StandardReference.ParamedicStatus))
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
                            "<case a.IsFeeValueInPercent when 1 then '%' else 'Rp' end unit>",
                            par.ParamedicName,
                            pType.ItemName.As("ParamedicStatusName"),
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
                            pType.ItemName.Ascending,
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
                }
                //query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}
