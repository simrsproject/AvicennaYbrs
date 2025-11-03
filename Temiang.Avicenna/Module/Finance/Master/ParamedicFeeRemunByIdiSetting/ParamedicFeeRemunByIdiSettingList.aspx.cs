using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class ParamedicFeeRemunByIdiSettingList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            UrlPageSearch = "ParamedicFeeRemunByIdiSettingSearch.aspx";
            UrlPageDetail = "ParamedicFeeRemunByIdiSettingDetail.aspx";

            WindowSearch.Height = 500;

            ProgramID = AppConstant.Program.PhysicianFeeRemunByIdiSetting;
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
            string id = dataItem.GetDataKeyValue(ParamedicFeeRemunByIdiSettingsMetadata.ColumnNames.SettingID).ToString();
            string url = string.Format("{0}?md={1}&id={2}", UrlPageDetail, mode, id);
            Page.Response.Redirect(url, true);
        }

        protected void grdList_PreRender(object sender, EventArgs e)
        {
            for (int rowIndex = grdList.Items.Count - 2; rowIndex >= 0; rowIndex--)
            {
                GridDataItem row = grdList.Items[rowIndex];
                GridDataItem previousRow = grdList.Items[rowIndex + 1];

                if (row["ParamedicName"].Text == previousRow["ParamedicName"].Text)
                {
                    row["ParamedicName"].RowSpan = previousRow["ParamedicName"].RowSpan < 2 ?
                        2 : previousRow["ParamedicName"].RowSpan + 1;
                    previousRow["ParamedicName"].Visible = false;
                }

                if (row["ParamedicName"].Text == previousRow["ParamedicName"].Text &&
                    row["SmfName"].Text == previousRow["SmfName"].Text)
                {
                    row["SmfName"].RowSpan = previousRow["SmfName"].RowSpan < 2 ?
                        2 : previousRow["SmfName"].RowSpan + 1;
                    previousRow["SmfName"].Visible = false;
                }

                if (row["ParamedicName"].Text == previousRow["ParamedicName"].Text &&
                    row["SmfName"].Text == previousRow["SmfName"].Text &&
                    row["ItemGroupName"].Text == previousRow["ItemGroupName"].Text)
                {
                    row["ItemGroupName"].RowSpan = previousRow["ItemGroupName"].RowSpan < 2 ?
                        2 : previousRow["ItemGroupName"].RowSpan + 1;
                    previousRow["ItemGroupName"].Visible = false;
                }

                if (row["ParamedicName"].Text == previousRow["ParamedicName"].Text &&
                   row["SmfName"].Text == previousRow["SmfName"].Text &&
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
            grdList.DataSource = FeeRemunSetting;
        }

        protected void grdList_DataBound(object sender, EventArgs e)
        {

        }

        private DataTable FeeRemunSetting
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                ParamedicFeeRemunByIdiSettingsQuery query;

                if (Session[SessionNameForQuery] != null)
                    query = (ParamedicFeeRemunByIdiSettingsQuery)Session[SessionNameForQuery];
                else
                {
                    query = new ParamedicFeeRemunByIdiSettingsQuery("a");
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
                }
                //query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}
