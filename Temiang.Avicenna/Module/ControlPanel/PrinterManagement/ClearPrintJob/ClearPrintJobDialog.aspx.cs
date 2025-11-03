using System;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;


namespace Temiang.Avicenna.Module.ControlPanel.PrinterManagement
{
    public partial class ClearPrintJobDialog : BasePage
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            ProgramID = AppConstant.Program.ClearPrintJob;
        }

        private void ShowInformation(string information)
        {
            lblInformation.Text = information;
            pnlInformation.Visible = true;
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            var usrGroup = new AppUserUserGroupCollection();
            usrGroup.Query.Where(usrGroup.Query.UserID == AppSession.UserLogin.UserID, usrGroup.Query.UserGroupID == "ADMIN");
            usrGroup.LoadAll();

            if (usrGroup.Count > 0)
            {
                using (var trans = new esTransactionScope())
                {
                    var pjlcoll = new PrintJobLogCollection();
                    pjlcoll.LoadAll();
                    pjlcoll.MarkAllAsDeleted();
                    pjlcoll.Save();

                    var pjplcoll = new PrintJobParameterLogCollection();
                    pjplcoll.LoadAll();
                    pjplcoll.MarkAllAsDeleted();
                    pjplcoll.Save();
                    
                    var pjcoll = new PrintJobCollection();
                    pjcoll.LoadAll();
                    pjcoll.MarkAllAsDeleted();
                    pjcoll.Save();

                    var pjpcoll = new PrintJobParameterCollection();
                    pjpcoll.LoadAll();

                    pjpcoll.MarkAllAsDeleted();
                    pjpcoll.Save();

                    trans.Complete();
                }
            }
            else
            {
                string searchText = string.Format("%{0}%", Request.UserHostName);
                var printer = new PrinterQuery();
                printer.Where(printer.PrinterName.Like(searchText));
                DataTable dtprinter = printer.LoadDataTable();
                if (dtprinter.Rows.Count > 0)
                {
                    string printerId = dtprinter.Rows[0]["PrinterID"].ToString();

                    using (var trans = new esTransactionScope())
                    {
                        var pjlcoll = new PrintJobLogCollection();
                        pjlcoll.Query.Where(pjlcoll.Query.PrinterID == printerId);
                        pjlcoll.LoadAll();

                        foreach (var pjl in pjlcoll)
                        {
                            var pjplcoll = new PrintJobParameterLogCollection();
                            pjplcoll.Query.Where(pjplcoll.Query.PrintNo == pjl.PrintNo);
                            pjplcoll.LoadAll();

                            pjplcoll.MarkAllAsDeleted();
                            pjplcoll.Save();
                        }
                        pjlcoll.MarkAllAsDeleted();
                        pjlcoll.Save();

                        var pjcoll = new PrintJobCollection();
                        pjcoll.Query.Where(pjcoll.Query.PrinterID == printerId);
                        pjcoll.LoadAll();

                        foreach (var pj in pjcoll)
                        {
                            var pjpcoll = new PrintJobParameterCollection();
                            pjpcoll.Query.Where(pjpcoll.Query.PrintNo == pj.PrintNo);
                            pjpcoll.LoadAll();

                            pjpcoll.MarkAllAsDeleted();
                            pjpcoll.Save();
                        }
                        pjcoll.MarkAllAsDeleted();
                        pjcoll.Save();

                        trans.Complete();
                    }
                }
            }
            ShowInformation("Print Job is clear.");
        }
    }
}
