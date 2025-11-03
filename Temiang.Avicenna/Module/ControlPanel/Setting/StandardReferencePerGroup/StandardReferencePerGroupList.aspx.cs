using System;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.ControlPanel.Setting
{
    public partial class StandardReferencePerGroupList : BasePageList
    {
        private string StdGroup
        {
            get
            {
                return string.IsNullOrEmpty(Request.QueryString["gr"]) ? string.Empty : Request.QueryString["gr"];
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            switch (StdGroup)
            {
                case "PINCIDENT":
                    ProgramID = AppConstant.Program.IncidentOtherMaster;
                    break;
                case "BLOODBANK":
                    ProgramID = AppConstant.Program.BloodBankStandardReference;
                    break;
                case "CSSD":
                    ProgramID = AppConstant.Program.CssdStandardReference;
                    break;
                case "PMKP":
                    ProgramID = AppConstant.Program.PmkpStandardReference;
                    break;
                case "ASSET":
                    ProgramID = AppConstant.Program.AssetStandardReference;
                    break;
                case "KEPK":
                    ProgramID = AppConstant.Program.KEPK_StandardReference;
                    break;
                case "LAUNDRY":
                    ProgramID = AppConstant.Program.LaundryStandardReference;
                    break;
                case "INV":
                    ProgramID = AppConstant.Program.InventoryStandardReference;
                    break;
                default:
                    ProgramID = AppConstant.Program.StandardReference;
                    break;
            }

            UrlPageSearch = "StandardReferencePerGroupSearch.aspx?gr=" + StdGroup ;
            UrlPageDetail = "StandardReferencePerGroupDetail.aspx?gr=" + StdGroup;

            // Quick Search
            ToolBarMenuQuickSearch.Visible = true;
        }

        public override string OnGetScriptToolBarNewClicking()
        {
            string script = string.Format(@"gotoAddUrl('{0}'); args.set_cancel(true);", StdGroup);
            return script;
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
            string id = dataItem.GetDataKeyValue(AppStandardReferenceMetadata.ColumnNames.StandardReferenceID).ToString();
            string url = string.Format("StandardReferencePerGroupDetail.aspx?md={0}&id={1}&gr={2}", mode, id, StdGroup);
            Page.Response.Redirect(url, true);
        }

        protected void grdList_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = AppStandardReferences;
        }

        private DataTable AppStandardReferences
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                AppStandardReferenceQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (AppStandardReferenceQuery)Session[SessionNameForQuery];
                else
                {
                    query = new AppStandardReferenceQuery();
                    query.Where(query.StandardReferenceGroup == StdGroup, query.IsActive == true);
                    query.OrderBy(query.StandardReferenceID.Ascending);

                    //Quick Search
                    ApplyQuickSearch(query);
                }

                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}
