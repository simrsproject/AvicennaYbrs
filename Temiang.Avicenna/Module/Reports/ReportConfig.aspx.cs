using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.Reports
{
    public partial class ReportConfig : BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            cboDatasource.DataSource = esConfigSettings.ConnectionInfo.Connections;
            cboDatasource.DataTextField = "Name";
            cboDatasource.DataValueField = "Name";
            cboDatasource.DataBind();

            cboDatasource_SelectedIndexChanged(null, new RadComboBoxSelectedIndexChangedEventArgs(null, null, cboDatasource.SelectedValue, null));

            cboModule_SelectedIndexChanged(null, new RadComboBoxSelectedIndexChangedEventArgs(null, null, cboModule.SelectedValue, null));
        }

        protected void cboDatasource_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (ViewState["module"] == null)
            {
                var apps = new AppProgramCollection();
                apps.es.Connection.ConnectionString = esConfigSettings.ConnectionInfo.Connections[e.Value].ConnectionString;
                apps.Query.Where(
                    apps.Query.ProgramID.NotIn("09", "95"), //rpt, pvt
                    apps.Query.ParentProgramID == string.Empty,
                    apps.Query.IsParentProgram == true,
                    apps.Query.ProgramType == "PRG",
                    apps.Query.IsVisible == true
                    );
                apps.LoadAll();
                ViewState["module"] = apps;
            }

            cboModule.DataSource = ViewState["module"];
            cboModule.DataTextField = AppProgramMetadata.ColumnNames.ProgramName;
            cboModule.DataValueField = AppProgramMetadata.ColumnNames.ProgramID;
            cboModule.DataBind();
        }

        protected void cboStoredProcedure_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string query = string.Format(@" SELECT DISTINCT TOP 20 so.name AS [Name]
                                            FROM sys.objects AS so
                                            INNER JOIN sys.parameters AS p ON so.OBJECT_ID = p.OBJECT_ID
                                            WHERE so.OBJECT_ID IN (SELECT OBJECT_ID
                                                                   FROM sys.objects
                                                                   WHERE TYPE IN ('P', 'FN'))
                                                AND so.type_desc = 'SQL_STORED_PROCEDURE'
                                                AND so.name LIKE '%{0}%'
                                            ORDER BY so.name", e.Text);

            var cmd = new SqlCommand(query, new SqlConnection(esConfigSettings.ConnectionInfo.Connections[cboDatasource.SelectedValue].ConnectionString));
            cmd.Connection.Open();

            var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (reader.HasRows)
            {
                cboStoredProcedure.Items.Clear();

                while (reader.Read())
                {
                    cboStoredProcedure.Items.Add(new RadComboBoxItem(reader["Name"].ToString(), reader["Name"].ToString()));
                }
            }

            cmd.Connection.Close();
        }

        protected void cboStoredProcedure_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["Name"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["Name"].ToString();
        }

        protected void cboModule_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var app = (ViewState["module"] as AppProgramCollection).FindByPrimaryKey(e.Value);
            txtUrl.Text = (app.ProgramName.IndexOf(' ') == -1 ? app.ProgramName : app.ProgramName.Remove(app.ProgramName.IndexOf(' '), 1)) + "/";
        }

        protected void cboStoredProcedure_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var query = string.Format(@"SELECT p.name AS [ParameterName],
                                            TYPE_NAME(p.user_type_id) + '(' + CAST(p.max_length AS VARCHAR(MAX)) + ')' AS [ParameterDataType]
                                        FROM sys.objects AS so
                                        INNER JOIN sys.parameters AS p ON so.OBJECT_ID = p.OBJECT_ID
                                        WHERE so.OBJECT_ID IN (SELECT OBJECT_ID
                                                               FROM sys.objects
                                                               WHERE TYPE IN ('P', 'FN'))
                                            AND so.type_desc = 'SQL_STORED_PROCEDURE'
                                            AND so.name = '{0}'
                                        ORDER BY so.name, 
                                            p.parameter_id", e.Value);

            var cmd = new SqlCommand(query, new SqlConnection(esConfigSettings.ConnectionInfo.Connections[cboDatasource.SelectedValue].ConnectionString));
            cmd.Connection.Open();

            var table = new DataTable();
            table.Load(cmd.ExecuteReader(CommandBehavior.CloseConnection));

            grdParameters.DataSource = table;
            grdParameters.DataBind();

            cmd.Connection.Close();
        }

        protected void grdParameters_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem) e.Item.PreRender += grdParameters_ItemPreRender;
        }

        private void grdParameters_ItemPreRender(object sender, EventArgs e)
        {
            var dataItem = sender as GridDataItem;
            if (dataItem == null) return;

            if (ViewState["OptionControl"] == null)
            {
                var options = new OptionControlCollection();
                options.LoadAll();
                ViewState["OptionControl"] = options;
            }

            var cbo = (dataItem["ParameterName"].FindControl("cboOptionControl") as RadComboBox);
            cbo.DataSource = ViewState["OptionControl"] as OptionControlCollection;
            cbo.DataTextField = OptionControlMetadata.ColumnNames.Parameters;
            cbo.DataValueField = OptionControlMetadata.ColumnNames.ControlName;
            cbo.DataBind();
        }
    }
}
