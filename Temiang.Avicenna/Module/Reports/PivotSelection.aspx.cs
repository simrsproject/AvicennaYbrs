using System;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI;

namespace Temiang.Avicenna.Module.Reports
{
    public partial class PivotSelection : BasePage
    {
        private DataTable GetDataSource
        {
            get
            {
                object obj = Session["collAppProgram"];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }

                const string progType = "PVT";

                // Standard Pivot
                AppProgramQuery query = new AppProgramQuery("a");
                AppUserGroupProgramQuery groupProgramQuery = new AppUserGroupProgramQuery("c");
                AppUserUserGroupQuery userGroupQuery = new AppUserUserGroupQuery("d");

                query.InnerJoin(groupProgramQuery).On(query.ProgramID == groupProgramQuery.ProgramID);

                query.InnerJoin(userGroupQuery).On(userGroupQuery.UserGroupID == groupProgramQuery.UserGroupID &
                                                   userGroupQuery.UserID == AppSession.UserLogin.UserID);


                query.Select(query.ProgramID, query.ProgramName, query.Note);
                query.Where(query.ProgramType == progType, query.IsProgram == true);

                if (!txtProgramName.Text.Trim().Equals(string.Empty))
                {
                    string searchTextContain = string.Format("%{0}%", txtProgramName.Text);
                    query.Where(query.ProgramName.Like(searchTextContain));
                }
                DataTable dtbStd = query.LoadDataTable();


                //Custom Pivot
                AppUserCustomPivotQuery custQue = new AppUserCustomPivotQuery("a");
                custQue.Select(custQue.ProgramID, custQue.CustomPivotName, custQue.CustomPivotID);
                //custQue.Where(custQue.CustomPivotID > 0, custQue.UserID == AppSession.UserLogin.UserID);
                custQue.Where(custQue.CustomPivotID > 0);
                if (AppSession.Parameter.IsCustomPivotFilterByUser)
                    custQue.Where(custQue.UserID == AppSession.UserLogin.UserID);
                if (!txtProgramName.Text.Trim().Equals(string.Empty))
                {
                    string searchTextContain = string.Format("%{0}%", txtProgramName.Text);
                    custQue.Where(custQue.CustomPivotName.Like(searchTextContain));
                }

                DataTable dtbCust = custQue.LoadDataTable();


                //Gabung
                DataTable dtbPivot = new DataTable("pivot");
                dtbPivot.Columns.Add("ProgramID", Type.GetType("System.String"));
                dtbPivot.Columns.Add("PivotName", Type.GetType("System.String"));
                dtbPivot.Columns.Add("CustomPivotID", Type.GetType("System.Int32"));
                dtbPivot.Columns.Add("PivotType", Type.GetType("System.String"));
                dtbPivot.Columns.Add("Note", Type.GetType("System.String"));

                foreach (DataRow row in dtbStd.Rows)
                {
                    DataRow newRow = dtbPivot.NewRow();
                    newRow["ProgramID"] = row["ProgramID"];
                    newRow["PivotName"] = row["ProgramName"];
                    newRow["CustomPivotID"] = 0;
                    newRow["PivotType"] = "STD";
                    newRow["Note"] = row["Note"];
                    dtbPivot.Rows.Add(newRow);
                }
                foreach (DataRow row in dtbCust.Rows)
                {
                    DataRow newRow = dtbPivot.NewRow();
                    newRow["ProgramID"] = row["ProgramID"];
                    newRow["PivotName"] = row["CustomPivotName"];
                    newRow["CustomPivotID"] = row["CustomPivotID"];
                    newRow["PivotType"] ="CUST";
                    dtbPivot.Rows.Add(newRow);
                }

                Session["collAppProgram"] = dtbPivot;
                return dtbPivot;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session.Remove("collAppProgram");
                string prgType = Request.QueryString["tp"];
                this.ClientScript.RegisterHiddenField("hdnProgramType", prgType);
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

        protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
        {
            base.RaisePostBackEvent(source, eventArgument);

            if (!(source is RadGrid)) 
                return;

            // Proses Delete custom Pivot
            string[] pars = eventArgument.Split('|');
            if (pars.Length <= 1)
                return;

            Int32 customPivotID = Convert.ToInt32(pars[0].Split(':')[2]);

            AppReportPivotCollection cutomPivotField = new AppReportPivotCollection();
            AppUserCustomPivot cutomPivot = new AppUserCustomPivot();

            using (esTransactionScope trans = new esTransactionScope())
            {
                cutomPivotField.Query.Where(cutomPivotField.Query.CustomPivotID == customPivotID);
                cutomPivotField.LoadAll();
                cutomPivotField.MarkAllAsDeleted();

                cutomPivot.LoadByPrimaryKey(pars[0].Split(':')[1], customPivotID);
                cutomPivot.MarkAsDeleted();
                cutomPivotField.Save();
                cutomPivot.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }

            Session.Remove("collAppProgram"); //reset
            grdList.Rebind();
        }
    }
}