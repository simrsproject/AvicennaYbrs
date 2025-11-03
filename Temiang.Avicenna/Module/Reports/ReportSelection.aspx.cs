using System;
using System.Linq;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI;

namespace Temiang.Avicenna.Module.Reports
{
    public partial class ReportSelection : BasePage
    {
        private string TopLevelProgId
        {
            get
            {
                return string.IsNullOrEmpty(Request.QueryString["tlv"]) ? string.Empty : Request.QueryString["tlv"];
            }
        }

        private DataTable GetDataSource
        {
            get
            {
                object obj = Session["collAppProgram"];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }
                string progType = Request.QueryString["tp"];
                if (string.IsNullOrEmpty(progType))
                    progType = "RPT";

                var query = new AppProgramQuery("a");
                var modQ = new AppProgramQuery("b");

                query.InnerJoin(modQ).On(query.TopLevelProgramID == modQ.ProgramID);
                query.Select(query.ProgramID, query.ProgramName, query.Note, modQ.ProgramName.As("ModuleName"), query.RowIndex, modQ.RowIndex.As("ModuleRowIndex"));
                query.Where(
                    query.Or(
                        query.ProgramType == progType,
                        query.ProgramType == "XML",
                        query.ProgramType == "SSRS"
                    ),
                    query.IsProgram == true,
                    query.IsVisible == true
                    );

                query.OrderBy(modQ.RowIndex.Ascending, query.RowIndex.Ascending, query.ProgramID.Ascending);
                query.GroupBy(query.ProgramID, query.ProgramName, query.Note, modQ.ProgramName, query.RowIndex, modQ.RowIndex);

                if (string.IsNullOrEmpty(TopLevelProgId))
                {
                    var groupProgramQuery = new AppUserGroupProgramQuery("c");
                    var userGroupQuery = new AppUserUserGroupQuery("d");

                    query.InnerJoin(groupProgramQuery).On(query.ProgramID == groupProgramQuery.ProgramID);
                    query.InnerJoin(userGroupQuery).On(userGroupQuery.UserGroupID == groupProgramQuery.UserGroupID &
                                                       userGroupQuery.UserID == AppSession.UserLogin.UserID);

                    if (!string.IsNullOrEmpty(AppSession.Parameter.HumanResourceUserID))
                    {
                        var hrUser = AppSession.Parameter.HumanResourceUserID.Split(',').Where(h => !string.IsNullOrEmpty(h.Trim()));
                        if (hrUser.Any() && !hrUser.Any(h => AppSession.UserLogin.UserID.Contains(h.Trim())))
                            query.Where(query.TopLevelProgramID.NotIn(AppConstant.Module.HumanResourceModules));
                    }

                    if (cboModule.SelectedValue != string.Empty)
                        query.Where(query.TopLevelProgramID == cboModule.SelectedValue);
                }
                else
                    query.Where(query.TopLevelProgramID == TopLevelProgId);

                if (!txtProgramName.Text.Trim().Equals(string.Empty))
                {
                    string searchTextContain = string.Format("%{0}%", txtProgramName.Text);
                    query.Where(query.ProgramName.Like(searchTextContain));
                }

                DataTable dtb = query.LoadDataTable();
                Session["collAppProgram"] = dtb;
                return dtb;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session.Remove("collAppProgram");
                var prgType = Request.QueryString["tp"];
                ClientScript.RegisterHiddenField("hdnProgramType", prgType);

                PopulateModule();
            }
            if (!Page.ClientScript.IsClientScriptIncludeRegistered("wPopMax"))
            {
                Page.ClientScript.RegisterClientScriptInclude("wPopMax", "../../JavaScript/OpenWindowMax.js");
            }

        }

        protected void btnSearchReport_Click(object sender, ImageClickEventArgs e)
        {
            Session.Remove("collAppProgram"); //reset
            grdList.Rebind();
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = GetDataSource;
        }
        private void PopulateModule()
        {
            string progType = Request.QueryString["tp"];
            if (string.IsNullOrEmpty(progType))
                progType = "RPT";

            var query = new AppProgramQuery("a");
            var modQ = new AppProgramQuery("b");
            
            query.InnerJoin(modQ).On(query.TopLevelProgramID == modQ.ProgramID);
            
            query.Select(query.TopLevelProgramID.As("ModuleID"), modQ.ProgramName.As("ModuleName"), modQ.RowIndex);
            query.Where(query.ProgramType.In(progType, "XML", "SSRS"), query.IsProgram == true, query.IsVisible == true);

            if (!string.IsNullOrEmpty(AppSession.Parameter.HumanResourceUserID))
            {
                var hrUser = AppSession.Parameter.HumanResourceUserID.Split(',').Where(h => !string.IsNullOrEmpty(h.Trim()));
                if (hrUser.Any() && !hrUser.Any(h => AppSession.UserLogin.UserID.Contains(h.Trim())))
                    query.Where(query.TopLevelProgramID.NotIn(AppConstant.Module.HumanResourceModules));
            }

            query.GroupBy(query.TopLevelProgramID, modQ.ProgramName, modQ.RowIndex);

            if (string.IsNullOrEmpty(TopLevelProgId))
            {
                var groupProgramQuery = new AppUserGroupProgramQuery("c");
                var userGroupQuery = new AppUserUserGroupQuery("d");

                query.InnerJoin(groupProgramQuery).On(query.ProgramID == groupProgramQuery.ProgramID);
                query.InnerJoin(userGroupQuery).On(userGroupQuery.UserGroupID == groupProgramQuery.UserGroupID &
                                               userGroupQuery.UserID == AppSession.UserLogin.UserID);
                trModule.Visible = true;
            }
            else
            {
                query.Where(query.TopLevelProgramID == TopLevelProgId);
                trModule.Visible = false;
            }

            var dtb = query.LoadDataTable();
            var emptyRow = dtb.NewRow();
            emptyRow[0] = string.Empty;
            emptyRow[1] = string.Empty;
            dtb.Rows.InsertAt(emptyRow, 0);

            cboModule.DataValueField = "ModuleID";
            cboModule.DataTextField = "ModuleName";
            cboModule.DataSource = dtb;
            cboModule.DataBind();
        }
    }
}