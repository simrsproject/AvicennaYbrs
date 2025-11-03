using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.CRM
{
    public partial class MembershipList : BasePageList
    {
        private string FormType
        {
            get
            {
                return Request.QueryString["t"];
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            WindowSearch.Height = 300;
            UrlPageSearch = "MembershipSearch.aspx?t=" + FormType;
            UrlPageDetail = "MembershipDetail.aspx?t=" + FormType;

            ProgramID = FormType == "g" ? AppConstant.Program.Membership : AppConstant.Program.MembershipEmployee;

            // Quick Search
            ToolBarMenuQuickSearch.Visible = true;

            if (!IsPostBack)
            {
                grdList.Columns[3].Visible = FormType == "g";
                grdList.Columns[4].Visible = FormType == "e";
            }
        }

        public override string OnGetScriptToolBarNewClicking()
        {
            string script = string.Format(@"gotoAddUrl('{0}'); args.set_cancel(true);", FormType);
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
            string id = dataItem.GetDataKeyValue(MembershipMetadata.ColumnNames.MembershipNo).ToString();
            string url = string.Format("MembershipDetail.aspx?md={0}&id={1}&t={2}", mode, id, FormType);
            Page.Response.Redirect(url, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
                grdList.DataSource = Memberships;
        }

        private DataTable Memberships
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                MembershipQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (MembershipQuery)Session[SessionNameForQuery];
                else
                {
                    query = new MembershipQuery("a");
                    var pat = new PatientQuery("b");
                    var mt = new AppStandardReferenceItemQuery("c");
                    var sal = new AppStandardReferenceItemQuery("d");
                    var emp = new PersonalInfoQuery("e");
                    query.LeftJoin(pat).On(pat.PatientID == query.PatientID);
                    query.LeftJoin(emp).On(emp.PersonID == query.PersonID);
                    query.InnerJoin(mt).On(mt.StandardReferenceID == AppEnum.StandardReference.MembershipType && mt.ItemID == query.SRMembershipType);
                    query.LeftJoin(sal).On(sal.StandardReferenceID == AppEnum.StandardReference.Salutation && sal.ItemID == pat.SRSalutation);
                    query.Select(
                                    query.MembershipNo,
                                    query.JoinDate,
                                    mt.ItemName.As("MembershipTypeName"),
                                    query.PatientID,
                                    pat.MedicalNo,
                                    emp.EmployeeNumber,
                                    sal.ItemName.As("SalutationName"),
                                    query.MemberName,
                                    query.Sex,
                                    query.DateOfBirth,
                                    query.Address,
                                    query.PhoneNo,
                                    query.MobilePhoneNo,
                                    query.Email,
                                    query.IsActive
                                );

                    if (FormType == "g")
                        query.Where(query.SRMembershipType == "01");
                    else
                        query.Where(query.SRMembershipType == "02");

                    query.OrderBy(query.MembershipNo.Descending);

                    //Quick Search
                    ApplyQuickSearch(query, "MembershipNo", "MembershipNo");
                }

                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;
                return dtb;
            }
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = e.DetailTableView.ParentItem;
            string id = dataItem.GetDataKeyValue("MembershipNo").ToString();

            if (e.DetailTableView.Name.Equals("grdListDetail"))
            {
                var query = new MembershipDetailQuery("a");
                query.Select(query, @"<a.RewardPoint - a.ClaimedPoint AS Balance>");
                if (FormType == "g")
                    query.Select(@"<CAST(0 AS BIT) AS IsEmployee>");
                else
                {
                    query.Select(@"<CAST(1 AS BIT) AS IsEmployee>");
                    query.es.Top = 50;
                }

                query.Where(query.MembershipNo == id);
                query.OrderBy(query.StartDate.Descending);

                e.DetailTableView.DataSource = query.LoadDataTable();
            }
            else
            {
                var query = new MembershipMemberQuery("a");
                var pat = new PatientQuery("b");
                query.InnerJoin(pat).On(pat.PatientID == query.PatientID);

                query.Select(query,
                    pat.MedicalNo,
                    pat.PatientName,
                    pat.Sex,
                    pat.CityOfBirth,
                    pat.DateOfBirth,
                    pat.Address,
                    pat.PhoneNo,
                    pat.MobilePhoneNo,
                    pat.Email
                    );

                query.Where(query.MembershipNo == id);
                query.OrderBy(query.CreateDateTime.Ascending);

                e.DetailTableView.DataSource = query.LoadDataTable();
            }
        }
    }
}