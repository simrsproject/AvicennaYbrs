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

namespace Temiang.Avicenna.Module.BloodBank.Stock
{
    public partial class Import : BasePageDialog
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ProgramID = Request.QueryString["id"];

            if (!IsPostBack)
            {
            }
        }

        private AppAutoNumberLast _autoNumber;
        private string GetNewTransactionNo()
        {
            _autoNumber = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.BloodReceivedNo);

            return _autoNumber.LastCompleteNumber;
        }
        public override bool OnButtonOkClicked()
        {
            HideInformationHeader();

            if (!fileuploadExcel.HasFile)
            {
                ShowInformationHeader("There is no file to upload.");
                return false;
            }
            if (ConfigurationManager.AppSettings["DocumentFolder"] == null)
            {
                ShowInformationHeader("Temporary document folder is not configured.");
                return false;
            }

            if (!Directory.Exists(ConfigurationManager.AppSettings["DocumentFolder"])) Directory.CreateDirectory(ConfigurationManager.AppSettings["DocumentFolder"]);
            string path = ConfigurationManager.AppSettings["DocumentFolder"] + fileuploadExcel.PostedFile.FileName;
            fileuploadExcel.SaveAs(path);

            try
            {
                DataTable table = Common.CreateExcelFile.LoadExcelFileToDataTable(path);
                if (table.Rows.Count > 0)
                {
                    foreach (DataRow row in table.Rows)
                    {                        
                        var transNo = string.IsNullOrEmpty(row["TransactionNo"].ToString()) ? string.Empty : row["TransactionNo"].ToString();
                        if (Convert.ToDateTime(row["TransactionDate"]) == null) continue;
                        var transDate = Convert.ToDateTime(row["TransactionDate"]);

                        var std = new AppStandardReferenceItem();
                        var bloodSource = string.IsNullOrEmpty(row["SRBloodSource"].ToString()) ? string.Empty : row["SRBloodSource"].ToString();
                        std = new AppStandardReferenceItem();
                        if (!std.LoadByPrimaryKey("BloodSource", bloodSource))
                            bloodSource = string.Empty;

                        var bloodSourceFrom = string.IsNullOrEmpty(row["SRBloodSourceFrom"].ToString()) ? string.Empty : row["SRBloodSourceFrom"].ToString();
                        std = new AppStandardReferenceItem();
                        if (!std.LoadByPrimaryKey("BloodSourceFrom", bloodSourceFrom))
                            bloodSourceFrom = string.Empty;                        

                        var isApprove = row["isApproved"].ToBoolean() == false ? false : true;
                        var isVoid = row["isVoid"].ToBoolean() == false ? false : true;

                        {
                            transNo = GetNewTransactionNo();
                            _autoNumber.Save();
                        }

                        var entity = new BloodReceived();
                        entity.AddNew();

                        entity.TransactionNo = transNo;
                        entity.TransactionDate = transDate;
                        entity.SRBloodSource = bloodSource;
                        entity.SRBloodSourceFrom = bloodSourceFrom;
                        entity.Notes = string.Empty;
                        entity.IsApproved = isApprove;
                        entity.ApprovedDateTime = DateTime.Now;
                        entity.ApprovedByUserID = AppSession.UserLogin.UserID;
                        entity.IsVoid = isVoid;
                        entity.VoidDateTime = DateTime.Now;
                        entity.VoidByUserID = AppSession.UserLogin.UserID;
                        entity.LastUpdateDateTime = DateTime.Now;
                        entity.LastUpdateByUserID = AppSession.UserLogin.UserID;

                        foreach (DataRow detailRow in table.Rows)
                        {
                            //if (string.IsNullOrEmpty(row["BagNo"].ToString())) continue;
                            //var bagN = detailRow["BagNo"].ToString();

                            //var bloodType = string.IsNullOrEmpty(detailRow["SRBloodType"].ToString()) ? string.Empty : detailRow["SRBloodType"].ToString();
                            //std = new AppStandardReferenceItem();
                            //if (!std.LoadByPrimaryKey("BloodType", bloodType))
                            //    bloodType = string.Empty;

                            //var bloodGroup = string.IsNullOrEmpty(detailRow["SRBloodGroup"].ToString()) ? string.Empty : detailRow["SRBloodGroup"].ToString();
                            //std = new AppStandardReferenceItem();
                            //if (!std.LoadByPrimaryKey("BloodGroup", bloodGroup))
                            //    bloodGroup = string.Empty;

                            //if (string.IsNullOrEmpty(detailRow["BloodRhesus"].ToString())) continue;
                            //var blRhes = detailRow["BloodRhesus"].ToString();

                            //if (Convert.ToDecimal(detailRow["VolumeBag"]) == 0) continue;
                            //var volBag = Convert.ToDecimal(detailRow["VolumeBag"]);

                            //if (Convert.ToDateTime(detailRow["ExpiredDateTime"]) == null) continue;
                            //var exDate = Convert.ToDateTime(detailRow["ExpiredDateTime"]);

                            var bagN = detailRow["BagNo"].ToString();
                            var bloodType = string.IsNullOrEmpty(detailRow["SRBloodType"].ToString()) ? string.Empty : detailRow["SRBloodType"].ToString();
                            var bloodGroup = string.IsNullOrEmpty(detailRow["SRBloodGroup"].ToString()) ? string.Empty : detailRow["SRBloodGroup"].ToString();
                            var blRhes = detailRow["BloodRhesus"].ToString();
                            var volBag = Convert.ToDecimal(detailRow["VolumeBag"]);
                            var exDate = Convert.ToDateTime(detailRow["ExpiredDateTime"]);

                            var detail = new BloodReceivedItem();

                            detail.TransactionNo = entity.TransactionNo;
                            detail.BagNo = bagN;
                            detail.SRBloodType = bloodType;
                            detail.BloodRhesus = blRhes;
                            detail.SRBloodGroup = bloodGroup;
                            detail.ExpiredDateTime = exDate;
                            detail.LastUpdateDateTime = DateTime.Now;
                            detail.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            detail.VolumeBag = volBag;

                            detail.Save();
                        }
                        using (esTransactionScope trans = new esTransactionScope())
                        {
                            entity.Save();                           

                            //Commit if success, Rollback if failed
                            trans.Complete();
                        }
                        break;
                    }
                }
                File.Delete(path);
            }
            catch (Exception ex)
            {
                File.Delete(path);

                ShowInformationHeader(ex.Message);
                return false;

                //Logger.LogException(ex, Request.UserHostName, AppSession.UserLogin.UserName);
                //if (Page.IsCallback)
                //{
                //    string script = string.Format("document.location.href = '{0}');", "~/ErrorPage.aspx");
                //    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "redirect", script, true);
                //}
                //else
                //{
                //    Response.Redirect("~/ErrorPage.aspx");
                //}
            }

            return true;
        }
    }
}