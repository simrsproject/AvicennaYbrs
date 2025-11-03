using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using System.Data;
using System.IO;
using System.Configuration;
using Telerik.Web.UI;
using System.Globalization;


namespace Temiang.Avicenna.Module.HR.Leave
{
    public partial class Import : BasePageDialog
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.EmployeeLeave;

            if (!IsPostBack)
            {
                txtLeaveEntitlementsQty.Value = 0;
            }
        }

        public override bool OnButtonOkClicked()
        {

            if (txtStartDate.IsEmpty || txtEnddate.IsEmpty)
            {
                ShowInformationHeader("Leave Period required.");
                return false;
            }

            if (txtLeaveEntitlementsQty.Value <= 0)
            {
                ShowInformationHeader("Leave Entitlements (Days) required.");
                return false;
            }

            if (string.IsNullOrEmpty(txtNotes.Text))
            {
                ShowInformationHeader("Notes required.");
                return false;
            }

            if (!fileuploadExcel.HasFile) return true;

            //if (ConfigurationManager.AppSettings["DocumentFolder"] == null) return true;
            //if (!Directory.Exists(ConfigurationManager.AppSettings["DocumentFolder"])) Directory.CreateDirectory(ConfigurationManager.AppSettings["DocumentFolder"]);
            //string path = ConfigurationManager.AppSettings["DocumentFolder"] + fileuploadExcel.PostedFile.FileName;

            string tmp_doc = AppParameter.GetParameterValue(AppParameter.ParameterItem.TmpDocumentFolder);
            if (string.IsNullOrEmpty(tmp_doc))
                tmp_doc = ConfigurationManager.AppSettings["DocumentFolder"];

            if (string.IsNullOrEmpty(tmp_doc)) return true;
            if (!Directory.Exists(tmp_doc))
                Directory.CreateDirectory(tmp_doc);
            string path = tmp_doc + fileuploadExcel.PostedFile.FileName;

            fileuploadExcel.SaveAs(path);

            try
            {
                DataTable table = Common.CreateExcelFile.LoadExcelFileToDataTable(path);
                if (table.Rows.Count > 0)
                {
                    var coll = new EmployeeLeaveCollection();
                    
                    foreach (DataRow row in table.Rows)
                    {
                        if (string.IsNullOrEmpty(row["LeaveTypeID"].ToString())) continue;
                        if (string.IsNullOrEmpty(row["PersonID"].ToString())) continue;

                        var leaveTypeID = row["LeaveTypeID"].ToString();
                        var personID = row["PersonID"].ToInt();

                        try
                        {
                            var entity = coll.AddNew();
                            entity.PersonID = personID;
                            entity.SREmployeeLeaveType = leaveTypeID;
                            entity.StartDate = txtStartDate.SelectedDate;
                            entity.EndDate = txtEnddate.SelectedDate;
                            entity.LeaveEntitlementsQty = Convert.ToInt32(txtLeaveEntitlementsQty.Value);
                            entity.Notes = txtNotes.Text;
                            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            entity.LastUpdateDateTime = DateTime.Now;
                            
                        }
                        catch (Exception e)
                        {
                            var i = e.Message.ToString();
                        }
                    }

                    coll.Save();
                }
                File.Delete(path);
            }
            catch (Exception ex)
            {
                //var i = e.Message.ToString();
                File.Delete(path);

                Logger.LogException(ex, Request.UserHostName, AppSession.UserLogin.UserName);
                if (Page.IsCallback)
                {
                    string script = string.Format("document.location.href = '{0}');", "~/ErrorPage.aspx");
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "redirect", script, true);
                }
                else
                {
                    Response.Redirect("~/ErrorPage.aspx");
                }
            }

            return true;
        }
    }
}