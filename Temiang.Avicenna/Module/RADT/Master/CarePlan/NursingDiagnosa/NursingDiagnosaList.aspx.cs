using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.NursingCare.Master
{
    public partial class NursingDiagnosaList : BasePageList
    {
        private string getPageID {
            get
            {
                return Request.QueryString["ndl"];
            }
        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            UrlPageSearch = "NursingDiagnosaSearch.aspx?ndl=" + Request.QueryString["ndl"];
            UrlPageDetail = "NursingDiagnosaDetail.aspx";
            UrlPageDetailNew = "NursingDiagnosaDetail.aspx" + "?md=new&ndl=" + Request.QueryString["ndl"];

            switch (getPageID){
                case "00": {
                    ProgramID = AppConstant.Program.NursingDomain;
                    break;
                }
                case "10":
                    {
                        ProgramID = AppConstant.Program.NursingDiag;
                        grdList.Columns[5].Visible = true;
                        break;
                    }
                case "11":
                    {
                        ProgramID = AppConstant.Program.NursingProblem;
                        grdList.Columns[6].Visible = true;
                        break;
                    }
                case "20":
                    {
                        ProgramID = AppConstant.Program.NursingNOC;
                        break;
                    }
                case "21":
                    {
                        ProgramID = AppConstant.Program.NursingNOCObjcetive;
                        break;
                    }
                case "30":
                    {
                        ProgramID = AppConstant.Program.NursingNIC;
                        break;
                    }
                case "31":
                    {
                        ProgramID = AppConstant.Program.NursingNICImplementation;
                        break;
                    }
                case "32":
                    {
                        ProgramID = AppConstant.Program.NursingNICImplementationCustom;
                        break;
                    }
                default :
                    {
                        ProgramID = AppConstant.Program.NursingDiagnosa;
                        break;
                    }
            }

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
            string id = dataItem.GetDataKeyValue(NursingDiagnosaMetadata.ColumnNames.NursingDiagnosaID).ToString();
            Page.Response.Redirect("NursingDiagnosaDetail.aspx?md=" + mode + "&id=" + id + "&ndl=" + Request.QueryString["ndl"], true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = Diagnosa;
        }

        private DataTable Diagnosa
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                NursingDiagnosaQuery query;
                NursingDiagnosaQuery parent;
                if (Session[SessionNameForQuery] != null)
                    query = (NursingDiagnosaQuery)Session[SessionNameForQuery];
                else
                {
                    query = new NursingDiagnosaQuery("a");
                    parent = new NursingDiagnosaQuery("p");
                    AppStandardReferenceItemQuery level = new AppStandardReferenceItemQuery("b");
                    AppStandardReferenceItemQuery NsDiagType = new AppStandardReferenceItemQuery("dt");
                    AppStandardReferenceItemQuery NsEtioType = new AppStandardReferenceItemQuery("et");

                    query.LeftJoin(parent).On(query.NursingDiagnosaParentID == parent.NursingDiagnosaID);
                    query.InnerJoin(level).On(query.SRNursingDiagnosaLevel == level.ItemID & level.StandardReferenceID == "NursingDiagnosaLevel");
                    query.LeftJoin(NsDiagType).On(query.SRNsDiagnosaType == NsDiagType.ItemID & NsDiagType.StandardReferenceID == "NsDiagnosaType");
                    query.LeftJoin(NsEtioType).On(query.SRNsEtiologyType == NsEtioType.ItemID & NsEtioType.StandardReferenceID == "NsEtiologyType");
                    query.Select
                        (   query,
                            //query.NursingDiagnosaID,
                            //query.NursingDiagnosaName,
                            //query.NursingDiagnosaParentID,
                            parent.NursingDiagnosaCode.As("NursingDiagnosaParentCode"),
                            parent.NursingDiagnosaName.As("NursingDiagnosaParentName"),
                            //query.SRNursingDiagnosaLevel,
                            level.ItemName,
                            NsDiagType.ItemName.As("NsDiagnosaTypeName"),
                            NsEtioType.ItemName.As("NsEtiologyTypeName")
                            //query.IsActive
                        );
                    if (Request.QueryString["ndl"] != null)
                    {
                        if (Request.QueryString["ndl"] != string.Empty)
                        {
                            query.Where(query.SRNursingDiagnosaLevel == Request.QueryString["ndl"]);
                        }
                    }
                    query.OrderBy(query.NursingDiagnosaID.Ascending);

                    //Quick Search
                    ApplyQuickSearch(query, "NursingDiagnosaName", "NursingDiagnosaID");
                }
                //query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}
