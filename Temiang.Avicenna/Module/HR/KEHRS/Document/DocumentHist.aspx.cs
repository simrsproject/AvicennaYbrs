using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.KEHRS.Document
{
    public partial class DocumentHist : BasePageDialog
    {
        protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
        {
            base.RaisePostBackEvent(source, eventArgument);
            if (source == grdDocument)
            {
                if (eventArgument.Equals("quicksearch") || eventArgument.Equals("refresh"))
                {
                    grdDocument.CurrentPageIndex = 0;
                    grdDocument.Rebind();
                }
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.KEHRS_SafetyCultureIncidentReportsVerification;

            if (!IsPostBack)
            {
                var rpt = new EmployeeSafetyCultureIncidentReports();
                if (rpt.LoadByPrimaryKey(Request.QueryString["pid"].ToString()))
                {
                    this.Title = rpt.TransactionNo + " [" + rpt.ReportDescription + "]";
                }

                Footer.Visible = false;
            }
        }

        private DataTable DocumentDataTable()
        {
            var query = new EmployeeSafetyCultureIncidentReportsDocumentQuery("a");
            query.Where(query.TransactionNo == Request.QueryString["pid"].ToString(), query.Or(query.IsDeleted.IsNull(), query.IsDeleted == false));

            var searchName = ((RadTextBox)tbarMain.Items[2].FindControl("txtQuickSearch")).Text;
            if (!string.IsNullOrWhiteSpace(searchName))
                query.Where(query.DocumentName.Like("%" + searchName.Trim() + "%"));

            query.OrderBy(query.DocumentDate.Descending, query.DocumentID.Descending);

            var dtb = query.LoadDataTable();

            // Replace empty smallimage
            var imgHelper = new ImageHelper();
            var pdfImage = imgHelper.LoadImageToArray(string.Format("{0}\\Images\\pdf100.png", Server.MapPath("~")));
            var dicomImage = imgHelper.LoadImageToArray(string.Format("{0}\\Images\\dicom.png", Server.MapPath("~")));
            foreach (DataRow row in dtb.Rows)
            {
                var fileName = row["FileAttachName"].ToString().ToLower();

                var oriPath = string.Empty;
                if (row["OriPath"] != null)
                {
                    oriPath = row["OriPath"].ToString();
                }

                if (fileName.Contains(".pdf"))
                {
                    row["SmallImage"] = pdfImage;
                }
                else if (fileName.Contains(".dcm"))
                {
                    row["SmallImage"] = dicomImage;
                }
                else if (row["SmallImage"] == DBNull.Value && !string.IsNullOrWhiteSpace(oriPath) && (oriPath.ToLower().Contains(".jpg") || oriPath.ToLower().Contains(".jpeg") || oriPath.ToLower().Contains(".png") || oriPath.ToLower().Contains(".bmp")))
                {
                    // Update thumbnail
                    if (FileHelper.FileExists(oriPath, 1000))
                    {
                        try
                        {
                            var entity = new EmployeeSafetyCultureIncidentReportsDocument();
                            if (entity.LoadByPrimaryKey(row["DocumentID"].ToInt()))
                            {
                                var imgArr = imgHelper.LoadImageToArray(oriPath);
                                row["SmallImage"] = imgArr;

                                var smallImg = imgHelper.ResizeImage(imgArr, new Size(100, 100), true, InterpolationMode.Low);
                                oriPath = oriPath.ToLower();
                                if (oriPath.Contains(".jpg") || oriPath.Contains(".jpeg"))
                                    entity.SmallImage = imgHelper.ConvertImageToByteArray(smallImg, ImageFormat.Jpeg);
                                else if (oriPath.Contains(".png"))
                                    entity.SmallImage = imgHelper.ConvertImageToByteArray(smallImg, ImageFormat.Png);
                                else if (oriPath.Contains(".bmp"))
                                    entity.SmallImage = imgHelper.ConvertImageToByteArray(smallImg, ImageFormat.Bmp);

                                entity.Save();
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
            }

            return dtb;
        }

        protected void grdDocument_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            grdDocument.DataSource = DocumentDataTable();
        }

        protected void grdDocument_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;
            var pdid = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex]["DocumentID"]);

        }
    }
}