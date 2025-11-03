using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class ParamedicFeeDeductionSettingList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            UrlPageSearch = "ParamedicFeeDeductionSettingSearch.aspx";
            UrlPageDetail = "ParamedicFeeDeductionSettingDetail.aspx";

            ProgramID = AppConstant.Program.PhysicianFeeDeductionSetting;
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
            string id = dataItem.GetDataKeyValue(ParamedicFeeDeductionSettingMetadata.ColumnNames.DeductionID).ToString();
            string url = string.Format("{0}?md={1}&id={2}", UrlPageDetail, mode, id);
            Page.Response.Redirect(url, true);
        }

        protected void grdList_PreRender(object sender, EventArgs e)
        {
            foreach (GridDataItem row in grdList.Items)
            {
                CheckBox chkPercent = (CheckBox)row["IsDeductionValueInPercent"].Controls[0];
                if (chkPercent != null)
                {
                    RadTextBox x = (RadTextBox)row["colDeductionValue"].FindControl("txtDeductionValue");
                    if (x != null)
                    {
                        x.Text = row["DeductionValue"].Text + (chkPercent.Checked ? "%" : "");
                    }
                }
            }
            for (int rowIndex = grdList.Items.Count - 2; rowIndex >= 0; rowIndex--)
            {
                GridDataItem row = grdList.Items[rowIndex];
                GridDataItem previousRow = grdList.Items[rowIndex + 1];
                if (row["DeductionName"].Text == previousRow["DeductionName"].Text)
                {
                    row["DeductionName"].RowSpan = previousRow["DeductionName"].RowSpan < 2 ? 2 : previousRow["DeductionName"].RowSpan + 1;
                    previousRow["DeductionName"].Visible = false;
                }
                if (row["DeductionName"].Text == previousRow["DeductionName"].Text && 
                    row["RegistrationTypeName"].Text == previousRow["RegistrationTypeName"].Text)
                {
                    row["RegistrationTypeName"].RowSpan = previousRow["RegistrationTypeName"].RowSpan < 2 ? 2 : previousRow["RegistrationTypeName"].RowSpan + 1;
                    previousRow["RegistrationTypeName"].Visible = false;
                }

                if (row["DeductionName"].Text == previousRow["DeductionName"].Text &&
                    row["RegistrationTypeName"].Text == previousRow["RegistrationTypeName"].Text &&
                    row["GuarantorTypeName"].Text == previousRow["GuarantorTypeName"].Text)
                {
                    row["GuarantorTypeName"].RowSpan = previousRow["GuarantorTypeName"].RowSpan < 2 ? 2 : previousRow["GuarantorTypeName"].RowSpan + 1;
                    previousRow["GuarantorTypeName"].Visible = false;
                }

                if (row["DeductionName"].Text == previousRow["DeductionName"].Text &&
                    row["RegistrationTypeName"].Text == previousRow["RegistrationTypeName"].Text &&
                    row["GuarantorTypeName"].Text == previousRow["GuarantorTypeName"].Text &&
                    row["GuarantorName"].Text == previousRow["GuarantorName"].Text)
                {
                    row["GuarantorName"].RowSpan = previousRow["GuarantorName"].RowSpan < 2 ? 2 : previousRow["GuarantorName"].RowSpan + 1;
                    previousRow["GuarantorName"].Visible = false;
                }

                if (row["DeductionName"].Text == previousRow["DeductionName"].Text && 
                    row["RegistrationTypeName"].Text == previousRow["RegistrationTypeName"].Text &&
                    row["GuarantorTypeName"].Text == previousRow["GuarantorTypeName"].Text &&
                    row["GuarantorName"].Text == previousRow["GuarantorName"].Text &&
                    row["ServiceUnitName"].Text == previousRow["ServiceUnitName"].Text)
                {
                    row["ServiceUnitName"].RowSpan = previousRow["ServiceUnitName"].RowSpan < 2 ? 2 : previousRow["ServiceUnitName"].RowSpan + 1;
                    previousRow["ServiceUnitName"].Visible = false;
                }

                if (row["DeductionName"].Text == previousRow["DeductionName"].Text &&
                    row["RegistrationTypeName"].Text == previousRow["RegistrationTypeName"].Text &&
                    row["GuarantorTypeName"].Text == previousRow["GuarantorTypeName"].Text &&
                    row["GuarantorName"].Text == previousRow["GuarantorName"].Text &&
                    row["ServiceUnitName"].Text == previousRow["ServiceUnitName"].Text &&
                    row["TariffComponentName"].Text == previousRow["TariffComponentName"].Text)
                {
                    row["TariffComponentName"].RowSpan = previousRow["TariffComponentName"].RowSpan < 2 ? 2 : previousRow["ServiceUnitName"].RowSpan + 1;
                    previousRow["TariffComponentName"].Visible = false;
                }
            }

            // hide empty columns
            int colDeductionName = 0, colRegistrationTypeName = 0, colGuarantorTypeName = 0,
                colGuarantorName = 0, colServiceUnitName = 0, colTariffComponentName = 0;
            for (int rowIndex = 0; rowIndex < grdList.Items.Count; rowIndex++)
            {
                GridDataItem row = grdList.Items[rowIndex];

                colDeductionName += (row["DeductionName"].Text.Replace("&nbsp;", "") == string.Empty) ? 0 : 1;
                colRegistrationTypeName += (row["RegistrationTypeName"].Text.Replace("&nbsp;", "") == string.Empty) ? 0 : 1;
                colGuarantorTypeName += (row["GuarantorTypeName"].Text.Replace("&nbsp;", "") == string.Empty) ? 0 : 1;
                colGuarantorName += (row["GuarantorName"].Text.Replace("&nbsp;", "") == string.Empty) ? 0 : 1;
                colServiceUnitName += (row["ServiceUnitName"].Text.Replace("&nbsp;", "") == string.Empty) ? 0 : 1;
                colTariffComponentName += (row["TariffComponentName"].Text.Replace("&nbsp;", "") == string.Empty) ? 0 : 1;
            }
            grdList.Columns.FindByDataField("DeductionName").Visible = colDeductionName > 0;
            grdList.Columns.FindByDataField("RegistrationTypeName").Visible = colRegistrationTypeName > 0;
            grdList.Columns.FindByDataField("GuarantorTypeName").Visible = colGuarantorTypeName > 0;
            grdList.Columns.FindByDataField("GuarantorName").Visible = colGuarantorName > 0;
            grdList.Columns.FindByDataField("ServiceUnitName").Visible = colServiceUnitName > 0;
            grdList.Columns.FindByDataField("TariffComponentName").Visible = colTariffComponentName > 0;
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = FeeDeductionSetting;
        }

        private DataTable FeeDeductionSetting
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                ParamedicFeeDeductionSettingQuery query;
                
                if (Session[SessionNameForQuery] != null)
                    query = (ParamedicFeeDeductionSettingQuery)Session[SessionNameForQuery];
                else
                {
                    query = new ParamedicFeeDeductionSettingQuery("a");
                    var srRegType = new AppStandardReferenceItemQuery("b");
                    var su = new ServiceUnitQuery("c");
                    var srFeeDedMethod = new AppStandardReferenceItemQuery("d");
                    var guar = new GuarantorQuery("e");
                    var tc = new TariffComponentQuery("tc");
                    var srFD = new AppStandardReferenceItemQuery("f");
                    var srGuarType = new AppStandardReferenceItemQuery("g");
                    query.LeftJoin(srRegType).On(query.SRRegistrationType.Equal(srRegType.ItemID) && srRegType.StandardReferenceID.Equal(AppEnum.StandardReference.RegistrationType))
                        .LeftJoin(su).On(query.ServiceUnitID.Equal(su.ServiceUnitID))
                        .LeftJoin(srFeeDedMethod).On(query.SRParamedicFeeDeductionMethod.Equal(srFeeDedMethod.ItemID) && srFeeDedMethod.StandardReferenceID.Equal(AppEnum.StandardReference.ParamedicFeeDeductionMethod))
                        .LeftJoin(guar).On(query.GuarantorID.Equal(guar.GuarantorID))
                        .LeftJoin(tc).On(query.TariffComponentID.Equal(tc.TariffComponentID))
                        .InnerJoin(srFD).On(query.SRParamedicFeeDeduction.Equal(srFD.ItemID) && srFD.StandardReferenceID.Equal(AppEnum.StandardReference.ParamedicFeeDeduction))
                        .LeftJoin(srGuarType).On(query.SRGuarantorType.Equal(srGuarType.ItemID) && srGuarType.StandardReferenceID.Equal(AppEnum.StandardReference.GuarantorType))
                        .Select
                        (
                            query,
                            srFD.ItemName.As("DeductionName"),
                            srRegType.ItemName.As("RegistrationTypeName"),
                            srGuarType.ItemName.As("GuarantorTypeName"),
                            guar.GuarantorName,
                            su.ServiceUnitName,
                            tc.TariffComponentName,
                            srFeeDedMethod.ItemName.As("ParamedicFeeDeductionMethodName")
                        )
                        .OrderBy(
                            srFD.ItemName.Ascending,
                            query.SRRegistrationType.Ascending,
                            query.GuarantorID.Ascending,
                            query.ServiceUnitID.Ascending,
                            query.SRParamedicFeeDeductionMethod.Ascending
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
