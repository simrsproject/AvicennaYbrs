using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.HR.Credential.Process.Medic
{
    public partial class CredentialingList : BasePageList
    {
        private string FormType
        {
            get
            {
                return Request.QueryString["type"];
            }
        }

        private string Role
        {
            get
            {
                return Request.QueryString["role"];
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            WindowSearch.Height = 400;

            UrlPageSearch = "CredentialingSearch.aspx?type=" + FormType + "&role=" + Role;
            UrlPageDetail = "CredentialingDetail.aspx?type=" + FormType + "&role=" + Role;
            ProgramID = Role == "usr" ? AppConstant.Program.MedicCredentialSelfAssessment : AppConstant.Program.MedicCredentialSelfAssessmentAdmin;

            if (!IsPostBack)
            {
            }
        }

        public override string OnGetScriptToolBarNewClicking()
        {
            string script = string.Format(@"gotoAddUrl('{0}', '{1}'); args.set_cancel(true);", FormType, Role);
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
            string id = dataItem.GetDataKeyValue(CredentialProcessMetadata.ColumnNames.TransactionNo).ToString();
            string url = string.Format("CredentialingDetail.aspx?md={0}&id={1}&type={2}&role={3}", mode, id, FormType, Role);
            Page.Response.Redirect(url, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = CredentialProcesses;
        }

        private DataTable CredentialProcesses
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }

                CredentialProcessQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (CredentialProcessQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new CredentialProcessQuery("a");
                    var personal = new PersonalInfoQuery("b");
                    var profession = new AppStandardReferenceItemQuery("c");
                    var area = new AppStandardReferenceItemQuery("d");
                    var level = new AppStandardReferenceItemQuery("e");

                    query.InnerJoin(personal).On(query.PersonID == personal.PersonID);
                    query.InnerJoin(profession).On(profession.StandardReferenceID == AppEnum.StandardReference.ProfessionGroup.ToString() & profession.ItemID == query.SRProfessionGroup);
                    query.InnerJoin(area).On(area.StandardReferenceID == AppEnum.StandardReference.ClinicalWorkArea.ToString() & area.ItemID == query.SRClinicalWorkArea);
                    query.InnerJoin(level).On(level.StandardReferenceID == AppEnum.StandardReference.ClinicalAuthorityLevel.ToString() & level.ItemID == query.SRClinicalAuthorityLevel);
                    
                    query.OrderBy
                        (
                            query.TransactionNo.Descending
                        );

                    query.Select(
                        query.TransactionNo,
                        query.TransactionDate,
                        query.PersonID,
                        personal.EmployeeNumber,
                        personal.EmployeeName,
                        profession.ItemName.As("ProfessionGroupName"),
                        area.ItemName.As("ClinicalWorkAreaName"),
                        level.ItemName.As("ClinicalAuthorityLevelName"),
                        query.IsApproved,
                        query.IsVoid
                        );

                    query.Where(query.SRProfessionGroup == "01");
                    
                    if (Role == "usr")
                        query.Where(query.PersonID == AppSession.UserLogin.PersonID.ToInt());
                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}