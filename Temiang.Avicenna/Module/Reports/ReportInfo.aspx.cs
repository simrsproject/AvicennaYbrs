using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Configuration;

namespace Temiang.Avicenna.Module.Reports
{
    public partial class ReportInfo : BasePageDialog
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ButtonCancel.Text = "Close";
            ButtonOk.Visible = false;
            Title = "Report Setting Information";
        }

        protected void grdInfo_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            var dtb = new DataTable();
            dtb.Columns.Add("ID", typeof(System.String));
            dtb.Columns.Add("ParameterName", typeof(System.String));
            dtb.Columns.Add("ParentID", typeof(System.String));
            dtb.Columns.Add("ParameterValue", typeof(System.String));

            var prg = new AppProgram();
            if (!prg.LoadByPrimaryKey(AppSession.PrintJobReportID))
            {
                throw new Exception(string.Format("AppProgram {0} not found.", AppSession.PrintJobReportID));
            }

            string path;
            string sp;
            string reportType;
            var appProgramHC = new AppProgramHealthcare();
            if (appProgramHC.LoadByPrimaryKey(AppSession.PrintJobReportID, AppSession.Parameter.HealthcareInitial))
            {
                reportType = appProgramHC.ProgramType.ToUpper();
                path = reportType == "XML" || reportType == "WORD" ? appProgramHC.NavigateUrl : appProgramHC.AssemblyClassName;
                sp = appProgramHC.StoreProcedureName;
            }
            else
            {
                reportType = prg.ProgramType.ToUpper();
                path = reportType == "XML" || reportType == "WORD" ? prg.NavigateUrl : prg.AssemblyClassName;
                sp = prg.StoreProcedureName;
            }

            if (sp.Contains("/")) // anggap menggunakan webservice
                sp = string.Format("{0}/{1}",
                                ConfigurationManager.AppSettings.Get("WebServiceDataSourceUrlRoot"), sp);

            var newRow = dtb.NewRow();
            newRow["ID"] = "10";
            newRow["ParameterName"] = "Report Setting";
            newRow["ParentID"] = string.Empty;
            newRow["ParameterValue"] = string.Empty;
            dtb.Rows.Add(newRow);

            newRow = dtb.NewRow();
            newRow["ID"] = "11";
            newRow["ParameterName"] = "Report ID";
            newRow["ParentID"] = "10";
            newRow["ParameterValue"] = AppSession.PrintJobReportID;
            dtb.Rows.Add(newRow);

            newRow = dtb.NewRow();
            newRow["ID"] = "12";
            newRow["ParameterName"] = "Report Type";
            newRow["ParentID"] = "10";
            newRow["ParameterValue"] = reportType;
            dtb.Rows.Add(newRow);

            newRow = dtb.NewRow();
            newRow["ID"] = "13";
            newRow["ParameterName"] = "Report Address";
            newRow["ParentID"] = "10";
            newRow["ParameterValue"] = path;
            dtb.Rows.Add(newRow);

            newRow = dtb.NewRow();
            newRow["ID"] = "14";
            newRow["ParameterName"] = "Datasource";
            newRow["ParentID"] = "10";
            newRow["ParameterValue"] = sp;
            dtb.Rows.Add(newRow);

            newRow = dtb.NewRow();
            newRow["ID"] = "20";
            newRow["ParameterName"] = "Report Parameter";
            newRow["ParentID"] = string.Empty;
            newRow["ParameterValue"] = string.Empty;
            dtb.Rows.Add(newRow);
            var i = 20;
            foreach (var par in AppSession.PrintJobParameters)
            {
                i++;
                newRow = dtb.NewRow();
                newRow["ID"] = string.Format("0:00", i);
                newRow["ParameterName"] = par.Name;
                newRow["ParentID"] = "20";
                if (par.ValueDateTime != null)
                    newRow["ParameterValue"] = par.ValueDateTime.Value.ToString(AppConstant.DisplayFormat.DateSql);
                else if (par.ValueNumeric != null)
                    newRow["ParameterValue"] = par.ValueNumeric.ToString();
                else
                    newRow["ParameterValue"] = par.ValueString;

                dtb.Rows.Add(newRow);

            }
            grdInfo.DataSource = dtb;
        }
    }
}