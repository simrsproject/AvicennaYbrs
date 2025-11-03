using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class ParamedicFeeByServiceSettingList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            UrlPageSearch = "ParamedicFeeByServiceSettingSearch.aspx";
            UrlPageDetail = "ParamedicFeeByServiceSettingDetail.aspx";

            WindowSearch.Height = 300;

            ProgramID = AppConstant.Program.PhysicianFeeByServiceSetting;
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
            string id = dataItem.GetDataKeyValue(ParamedicFeeByServiceSettingMetadata.ColumnNames.Id).ToString();
            string url = string.Format("{0}?md={1}&id={2}", UrlPageDetail, mode, id);
            Page.Response.Redirect(url, true);
        }

        protected void grdList_PreRender(object sender, EventArgs e)
        {
            foreach (GridDataItem row in grdList.Items)
            {
                CheckBox chkPercent = (CheckBox)row["IsFeeValueInPercent"].Controls[0];
                if (chkPercent != null)
                {
                    RadTextBox x = (RadTextBox)row["colFeeValue"].FindControl("txtFeeValue");
                    if (x != null)
                    {
                        x.Text = row["FeeValue"].Text + (chkPercent.Checked ? "%" : "");
                    }
                }
            }

            for (int rowIndex = grdList.Items.Count - 2; rowIndex >= 0; rowIndex--)
            {
                GridDataItem row = grdList.Items[rowIndex];
                GridDataItem previousRow = grdList.Items[rowIndex + 1];
                if (row["RegistrationTypeName"].Text == previousRow["RegistrationTypeName"].Text)
                {
                    row["RegistrationTypeName"].RowSpan = previousRow["RegistrationTypeName"].RowSpan < 2 ? 2 : previousRow["RegistrationTypeName"].RowSpan + 1;
                    previousRow["RegistrationTypeName"].Visible = false;
                }

                if (row["RegistrationTypeName"].Text == previousRow["RegistrationTypeName"].Text &&
                    row["ServiceUnitName"].Text == previousRow["ServiceUnitName"].Text)
                {
                    row["ServiceUnitName"].RowSpan = previousRow["ServiceUnitName"].RowSpan < 2 ? 2 : previousRow["ServiceUnitName"].RowSpan + 1;
                    previousRow["ServiceUnitName"].Visible = false;
                }

                if (row["RegistrationTypeName"].Text == previousRow["RegistrationTypeName"].Text &&
                    row["ServiceUnitName"].Text == previousRow["ServiceUnitName"].Text &&
                    row["ItemName"].Text == previousRow["ItemName"].Text)
                {
                    row["ItemName"].RowSpan = previousRow["ItemName"].RowSpan < 2 ? 2 : previousRow["ItemName"].RowSpan + 1;
                    previousRow["ItemName"].Visible = false;
                }

                if (row["RegistrationTypeName"].Text == previousRow["RegistrationTypeName"].Text &&
                    row["ServiceUnitName"].Text == previousRow["ServiceUnitName"].Text &&
                    row["ItemName"].Text == previousRow["ItemName"].Text &&
                    row["ClassName"].Text == previousRow["ClassName"].Text)
                {
                    row["ClassName"].RowSpan = previousRow["ClassName"].RowSpan < 2 ? 2 : previousRow["ClassName"].RowSpan + 1;
                    previousRow["ClassName"].Visible = false;
                }

                if (row["RegistrationTypeName"].Text == previousRow["RegistrationTypeName"].Text &&
                    row["ServiceUnitName"].Text == previousRow["ServiceUnitName"].Text &&
                    row["ItemName"].Text == previousRow["ItemName"].Text &&
                    row["ClassName"].Text == previousRow["ClassName"].Text &&
                    row["ParamedicFeeCaseTypeName"].Text == previousRow["ParamedicFeeCaseTypeName"].Text)
                {
                    row["ParamedicFeeCaseTypeName"].RowSpan = previousRow["ParamedicFeeCaseTypeName"].RowSpan < 2 ? 2 : previousRow["ParamedicFeeCaseTypeName"].RowSpan + 1;
                    previousRow["ParamedicFeeCaseTypeName"].Visible = false;
                }

                if (row["RegistrationTypeName"].Text == previousRow["RegistrationTypeName"].Text &&
                    row["ServiceUnitName"].Text == previousRow["ServiceUnitName"].Text &&
                    row["ItemName"].Text == previousRow["ItemName"].Text &&
                    row["ClassName"].Text == previousRow["ClassName"].Text &&
                    row["ParamedicFeeCaseTypeName"].Text == previousRow["ParamedicFeeCaseTypeName"].Text &&
                    row["ParamedicFeeIsTeamName"].Text == previousRow["ParamedicFeeIsTeamName"].Text)
                {
                    row["ParamedicFeeIsTeamName"].RowSpan = previousRow["ParamedicFeeIsTeamName"].RowSpan < 2 ? 2 : previousRow["ParamedicFeeIsTeamName"].RowSpan + 1;
                    previousRow["ParamedicFeeIsTeamName"].Visible = false;
                }

                if (row["RegistrationTypeName"].Text == previousRow["RegistrationTypeName"].Text &&
                    row["ServiceUnitName"].Text == previousRow["ServiceUnitName"].Text &&
                    row["ItemName"].Text == previousRow["ItemName"].Text &&
                    row["ClassName"].Text == previousRow["ClassName"].Text &&
                    row["ParamedicFeeCaseTypeName"].Text == previousRow["ParamedicFeeCaseTypeName"].Text &&
                    row["ParamedicFeeIsTeamName"].Text == previousRow["ParamedicFeeIsTeamName"].Text &&
                    row["ParamedicFeeTeamStatusName"].Text == previousRow["ParamedicFeeTeamStatusName"].Text)
                {
                    row["ParamedicFeeTeamStatusName"].RowSpan = previousRow["ParamedicFeeTeamStatusName"].RowSpan < 2 ? 2 : previousRow["ParamedicFeeTeamStatusName"].RowSpan + 1;
                    previousRow["ParamedicFeeTeamStatusName"].Visible = false;
                }
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = FeeSetting;
        }

        private DataTable FeeSetting
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                ParamedicFeeByServiceSettingQuery query;
                
                if (Session[SessionNameForQuery] != null)
                    query = (ParamedicFeeByServiceSettingQuery)Session[SessionNameForQuery];
                else
                {
                    query = new ParamedicFeeByServiceSettingQuery("a");
                    var srRegType = new AppStandardReferenceItemQuery("b");
                    var su = new ServiceUnitQuery("c");
                    var item = new ItemQuery("d");
                    var cls = new ClassQuery("e");
                    var srFeeCaseType = new AppStandardReferenceItemQuery("f");
                    var srFeeIsTeam = new AppStandardReferenceItemQuery("g");
                    var srFeeTeamStatus = new AppStandardReferenceItemQuery("h");
                    var tComp = new TariffComponentQuery("i");
                    query.LeftJoin(srRegType).On(query.SRRegistrationType.Equal(srRegType.ItemID) && srRegType.StandardReferenceID.Equal(AppEnum.StandardReference.RegistrationType))
                        .LeftJoin(su).On(query.ServiceUnitID.Equal(su.ServiceUnitID))
                        .InnerJoin(item).On(query.ItemID.Equal(item.ItemID))
                        .LeftJoin(cls).On(query.ClassID.Equal(cls.ClassID))
                        .LeftJoin(srFeeCaseType).On(query.SRParamedicFeeCaseType.Equal(srFeeCaseType.ItemID) && srFeeCaseType.StandardReferenceID.Equal(AppEnum.StandardReference.ParamedicFeeCaseType))
                        .LeftJoin(srFeeIsTeam).On(query.SRParamedicFeeIsTeam.Equal(srFeeIsTeam.ItemID) && srFeeIsTeam.StandardReferenceID.Equal(AppEnum.StandardReference.ParamedicFeeIsTeam))
                        .LeftJoin(srFeeTeamStatus).On(query.SRParamedicFeeTeamStatus.Equal(srFeeTeamStatus.ItemID) && srFeeTeamStatus.StandardReferenceID.Equal(AppEnum.StandardReference.ParamedicFeeTeamStatus))
                        .LeftJoin(tComp).On(query.TariffComponentID.Equal(tComp.TariffComponentID))
                        .Select
                        (
                            query,
                            srRegType.ItemName.As("RegistrationTypeName"),
                            su.ServiceUnitName,
                            item.ItemName,
                            cls.ClassName,
                            srFeeCaseType.ItemName.As("ParamedicFeeCaseTypeName"),
                            srFeeIsTeam.ItemName.As("ParamedicFeeIsTeamName"),
                            srFeeTeamStatus.ItemName.As("ParamedicFeeTeamStatusName"),
                            tComp.TariffComponentName
                        )
                        .OrderBy(
                            query.SRRegistrationType.Ascending,
                            query.ServiceUnitID.Ascending,
                            item.ItemName.Ascending,
                            query.ClassID.Ascending,
                            query.SRParamedicFeeCaseType.Ascending,
                            query.SRParamedicFeeIsTeam.Ascending,
                            query.SRParamedicFeeTeamStatus.Ascending
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
