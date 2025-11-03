using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class ParamedicFeeByArSettingList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            UrlPageSearch = "ParamedicFeeByArSettingSearch.aspx";
            UrlPageDetail = "ParamedicFeeByArSettingDetail.aspx";

            ProgramID = AppConstant.Program.PhysicianFeeByArSetting;
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
            string id = dataItem.GetDataKeyValue(ParamedicFeeByArSettingMetadata.ColumnNames.Id).ToString();
            string url = string.Format("{0}?md={1}&id={2}", UrlPageDetail, mode, id);
            Page.Response.Redirect(url, true);
        }

        protected void grdList_PreRender(object sender, EventArgs e)
        {
            foreach (GridDataItem row in grdList.Items) {
                CheckBox chkPercent = (CheckBox)row["IsFeeValueInPercent"].Controls[0];
                if (chkPercent != null) {
                    RadTextBox x = (RadTextBox)row["colFeeValue"].FindControl("txtFeeValue");
                    if (x != null) {
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
                    row["IsMergeToIPR"].Text == previousRow["IsMergeToIPR"].Text)
                {
                    row["IsMergeToIPR"].RowSpan = previousRow["IsMergeToIPR"].RowSpan < 2 ? 2 : previousRow["IsMergeToIPR"].RowSpan + 1;
                    previousRow["IsMergeToIPR"].Visible = false;
                }

                if (row["RegistrationTypeName"].Text == previousRow["RegistrationTypeName"].Text &&
                    row["IsMergeToIPR"].Text == previousRow["IsMergeToIPR"].Text &&
                    row["ServiceUnitName"].Text == previousRow["ServiceUnitName"].Text)
                {
                    row["ServiceUnitName"].RowSpan = previousRow["ServiceUnitName"].RowSpan < 2 ? 2 : previousRow["ServiceUnitName"].RowSpan + 1;
                    previousRow["ServiceUnitName"].Visible = false;
                }

                if (row["RegistrationTypeName"].Text == previousRow["RegistrationTypeName"].Text &&
                    row["IsMergeToIPR"].Text == previousRow["IsMergeToIPR"].Text &&
                    row["ServiceUnitName"].Text == previousRow["ServiceUnitName"].Text &&
                    row["SmfName"].Text == previousRow["SmfName"].Text)
                {
                    row["SmfName"].RowSpan = previousRow["SmfName"].RowSpan < 2 ? 2 : previousRow["SmfName"].RowSpan + 1;
                    previousRow["SmfName"].Visible = false;
                }

                if (row["RegistrationTypeName"].Text == previousRow["RegistrationTypeName"].Text &&
                    row["IsMergeToIPR"].Text == previousRow["IsMergeToIPR"].Text &&
                    row["ServiceUnitName"].Text == previousRow["ServiceUnitName"].Text &&
                    row["SmfName"].Text == previousRow["SmfName"].Text &&
                    row["ParamedicFeeCaseTypeName"].Text == previousRow["ParamedicFeeCaseTypeName"].Text)
                {
                    row["ParamedicFeeCaseTypeName"].RowSpan = previousRow["ParamedicFeeCaseTypeName"].RowSpan < 2 ? 2 : previousRow["ParamedicFeeCaseTypeName"].RowSpan + 1;
                    previousRow["ParamedicFeeCaseTypeName"].Visible = false;
                }

                if (row["RegistrationTypeName"].Text == previousRow["RegistrationTypeName"].Text &&
                    row["IsMergeToIPR"].Text == previousRow["IsMergeToIPR"].Text &&
                    row["ServiceUnitName"].Text == previousRow["ServiceUnitName"].Text &&
                    row["SmfName"].Text == previousRow["SmfName"].Text &&
                    row["ParamedicFeeCaseTypeName"].Text == previousRow["ParamedicFeeCaseTypeName"].Text &&
                    row["ParamedicFeeIsTeamName"].Text == previousRow["ParamedicFeeIsTeamName"].Text)
                {
                    row["ParamedicFeeIsTeamName"].RowSpan = previousRow["ParamedicFeeIsTeamName"].RowSpan < 2 ? 2 : previousRow["ParamedicFeeIsTeamName"].RowSpan + 1;
                    previousRow["ParamedicFeeIsTeamName"].Visible = false;
                }

                if (row["RegistrationTypeName"].Text == previousRow["RegistrationTypeName"].Text &&
                    row["IsMergeToIPR"].Text == previousRow["IsMergeToIPR"].Text &&
                    row["ServiceUnitName"].Text == previousRow["ServiceUnitName"].Text &&
                    row["SmfName"].Text == previousRow["SmfName"].Text &&
                    row["ParamedicFeeCaseTypeName"].Text == previousRow["ParamedicFeeCaseTypeName"].Text &&
                    row["ParamedicFeeIsTeamName"].Text == previousRow["ParamedicFeeIsTeamName"].Text &&
                    row["LosStart"].Text == previousRow["LosStart"].Text)
                {
                    row["LosStart"].RowSpan = previousRow["LosStart"].RowSpan < 2 ? 2 : previousRow["LosStart"].RowSpan + 1;
                    previousRow["LosStart"].Visible = false;
                }

                if (row["RegistrationTypeName"].Text == previousRow["RegistrationTypeName"].Text &&
                    row["IsMergeToIPR"].Text == previousRow["IsMergeToIPR"].Text &&
                    row["ServiceUnitName"].Text == previousRow["ServiceUnitName"].Text &&
                    row["SmfName"].Text == previousRow["SmfName"].Text &&
                    row["ParamedicFeeCaseTypeName"].Text == previousRow["ParamedicFeeCaseTypeName"].Text &&
                    row["ParamedicFeeIsTeamName"].Text == previousRow["ParamedicFeeIsTeamName"].Text &&
                    row["LosStart"].Text == previousRow["LosStart"].Text &&
                    row["LosEnd"].Text == previousRow["LosEnd"].Text)
                {
                    row["LosEnd"].RowSpan = previousRow["LosEnd"].RowSpan < 2 ? 2 : previousRow["LosEnd"].RowSpan + 1;
                    previousRow["LosEnd"].Visible = false;
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

                ParamedicFeeByArSettingQuery query;
                
                if (Session[SessionNameForQuery] != null)
                    query = (ParamedicFeeByArSettingQuery)Session[SessionNameForQuery];
                else
                {
                    query = new ParamedicFeeByArSettingQuery("a");
                    var srRegType = new AppStandardReferenceItemQuery("b");
                    var su = new ServiceUnitQuery("c");
                    var srFeeCaseType = new AppStandardReferenceItemQuery("d");
                    var srFeeIsTeam = new AppStandardReferenceItemQuery("e");
                    var srFeeTeamStatus = new AppStandardReferenceItemQuery("f");
                    //var smf = new SmfQuery("g");
                    var smf = new AppStandardReferenceItemQuery("g");

                    query.LeftJoin(srRegType).On(query.SRRegistrationType.Equal(srRegType.ItemID) && srRegType.StandardReferenceID.Equal(AppEnum.StandardReference.RegistrationType))
                        .LeftJoin(su).On(query.ServiceUnitID.Equal(su.ServiceUnitID))
                        .LeftJoin(srFeeCaseType).On(query.SRParamedicFeeCaseType.Equal(srFeeCaseType.ItemID) && srFeeCaseType.StandardReferenceID.Equal(AppEnum.StandardReference.ParamedicFeeCaseType))
                        .LeftJoin(srFeeIsTeam).On(query.SRParamedicFeeIsTeam.Equal(srFeeIsTeam.ItemID) && srFeeIsTeam.StandardReferenceID.Equal(AppEnum.StandardReference.ParamedicFeeIsTeam))
                        .LeftJoin(srFeeTeamStatus).On(query.SRParamedicFeeTeamStatus.Equal(srFeeTeamStatus.ItemID) && srFeeTeamStatus.StandardReferenceID.Equal(AppEnum.StandardReference.ParamedicFeeTeamStatus))
                        //.LeftJoin(smf).On(query.SmfID.Equal(smf.SmfID))
                        .LeftJoin(smf).On(query.SmfID.Equal(smf.ItemID) && smf.StandardReferenceID.Equal(AppEnum.StandardReference.SurgerySpecialty))
                        .Select
                        (
                            query.Id,
                            query.SRRegistrationType,
                            srRegType.ItemName.As("RegistrationTypeName"),
                            //query.IsMergeToIPR,
                            "<CASE a.IsMergeToIPR WHEN 0 THEN '' ELSE 'Merge To IPR' END IsMergeToIPR>",
                            query.ServiceUnitID,
                            su.ServiceUnitName,
                            query.SmfID,
                            //smf.SmfName,
                            smf.ItemName.As("SmfName"),
                            query.SRParamedicFeeCaseType,
                            srFeeCaseType.ItemName.As("ParamedicFeeCaseTypeName"),
                            query.SRParamedicFeeIsTeam,
                            srFeeIsTeam.ItemName.As("ParamedicFeeIsTeamName"),
                            query.LosStart,
                            query.LosEnd,
                            query.SRParamedicFeeTeamStatus,
                            srFeeTeamStatus.ItemName.As("ParamedicFeeTeamStatusName"),
                            query.IsFeeValueInPercent,
                            query.FeeValue
                        )
                        .OrderBy(
                            query.SRRegistrationType.Ascending,
                            query.IsMergeToIPR.Ascending,
                            query.ServiceUnitID.Ascending,
                            query.SmfID.Ascending,
                            query.SRParamedicFeeCaseType.Ascending,
                            query.SRParamedicFeeIsTeam.Ascending,
                            query.LosStart.Ascending,
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
