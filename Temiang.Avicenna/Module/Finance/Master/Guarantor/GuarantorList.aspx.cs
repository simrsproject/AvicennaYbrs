using System;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class GuarantorList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            WindowSearch.Height = 230;

            UrlPageSearch = "GuarantorSearch.aspx";
            UrlPageDetail = "GuarantorDetail.aspx";

            ProgramID = AppConstant.Program.GUARANTOR;

            // Quick Search
            ToolBarMenuQuickSearch.Visible = true;
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
            string id = dataItem.GetDataKeyValue(GuarantorMetadata.ColumnNames.GuarantorID).ToString();
            Page.Response.Redirect("GuarantorDetail.aspx?md=" + mode + "&id=" + id, true);
        }	

        protected void grdList_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = Guarantors;
        }

        private DataTable Guarantors
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                GuarantorQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (GuarantorQuery)Session[SessionNameForQuery];
                else
                {
                    query = new GuarantorQuery("a");

                    var asriq = new AppStandardReferenceItemQuery("b");
                    var coaq = new ChartOfAccountsQuery("c");
                    query.LeftJoin(asriq).On(query.SRTariffType == asriq.ItemID && asriq.StandardReferenceID == AppEnum.StandardReference.TariffType);

                    query.LeftJoin(coaq).On(query.ChartOfAccountId == coaq.ChartOfAccountId);

                    query.Select
                        (
                            query.GuarantorID,
                            query.GuarantorName,
                            query.ContractStart,
                            query.ContractEnd,
                            query.ContactPerson,
                            asriq.ItemName.As("SRTariffType"),
                            coaq.ChartOfAccountCode,
                            query.PhoneNo,
                            query.IsActive,
                            query.ContractNumber
                        );
                    query.OrderBy(query.GuarantorID.Descending);

                    //Quick Search
                    ApplyQuickSearch(query, "GuarantorName", "GuarantorID");
                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}

