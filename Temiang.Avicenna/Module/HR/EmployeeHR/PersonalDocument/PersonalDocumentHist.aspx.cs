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

namespace Temiang.Avicenna.Module.HR.EmployeeHR
{
    public partial class PersonalDocumentHist : BasePageDialog
    {
        private string QsPageId
        {
            get
            {
                return string.IsNullOrEmpty(Request.QueryString["pageId"]) ? string.Empty : Request.QueryString["pageId"];
            }
        }

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
            switch (QsPageId)
            {
                case "epi":
                    ProgramID = AppConstant.Program.PersonalInfo;
                    break;
                case "ewi":
                    ProgramID = AppConstant.Program.EmployeeWorkingInfo;
                    break;

                case "gen":
                    ProgramID = AppConstant.Program.EmployeeLogbook;
                    break;
                case "c01":
                    ProgramID = AppConstant.Program.EmployeeLogbookMedicalCommitte;
                    break;
                case "c02":
                    ProgramID = AppConstant.Program.EmployeeLogbookNursingCommitte;
                    break;
                case "c03":
                    ProgramID = AppConstant.Program.EmployeeLogbookKtkl;
                    break;

                default:
                    ProgramID = AppConstant.Program.EmployeeWorkingInfo;
                    break;
            }
            
            if (!IsPostBack)
            {
                var emps = new VwEmployeeTable();
                emps.Query.Where(emps.Query.PersonID == Request.QueryString["pid"].ToInt());
                if (emps.Query.Load())
                {
                    string addTittle = string.Empty;
                    var dname = new AppStandardReferenceItem();
                    if (dname.LoadByPrimaryKey("PersonalDocumentCode", Request.QueryString["dc"].ToString()))
                        addTittle = dname.ItemName;

                    if (addTittle == string.Empty)
                        this.Title = emps.EmployeeName + " [" + emps.EmployeeNumber + "]";
                    else
                        this.Title = emps.EmployeeName + " [" + emps.EmployeeNumber + "] - " + addTittle;

                }
                Footer.Visible = false;

                if (QsPageId != "epi" && QsPageId != "ewi")
                    grdDocument.Columns.FindByUniqueName("DeleteColumn").Visible = this.IsUserDeleteAble;
            }
        }

        private DataTable PersonalDocumentDataTable()
        {
            var query = new PersonalDocumentQuery("a");
            query.Where(query.PersonID == Request.QueryString["pid"].ToInt(), query.Or(query.IsDeleted.IsNull(), query.IsDeleted == false));
            if (Request.QueryString["dc"].ToString() == "00" || Request.QueryString["dc"].ToString() == "01")
                query.Where(query.DocumentCode.In("00", "01"));
            else
                query.Where(query.DocumentCode == Request.QueryString["dc"].ToString(), query.RefferenceID == Request.QueryString["rid"].ToInt());

            var searchName = ((RadTextBox)tbarMain.Items[3].FindControl("txtQuickSearch")).Text;
            if (!string.IsNullOrWhiteSpace(searchName))
                query.Where(query.DocumentName.Like("%" + searchName.Trim() + "%"));

            query.OrderBy(query.DocumentDate.Descending, query.PersonalDocumentID.Descending);

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
                            var entity = new PersonalDocument();
                            if (entity.LoadByPrimaryKey(row["PersonalDocumentID"].ToInt()))
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
            grdDocument.DataSource = PersonalDocumentDataTable();
        }

        protected void grdDocument_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;
            var pdid = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex]["PersonalDocumentID"]);

            var ent = new PersonalDocument();
            if (ent.LoadByPrimaryKey(pdid))
            {
                // Rename File
                var filePath = (ent.IsUpload ?? false) ?
                System.IO.Path.Combine(AppSession.Parameter.EmployeeDocumentFolder, "PersonalDocument", ent.PersonID.ToString().Trim(), ent.FileAttachName)
                : ent.OriPath;

                if (System.IO.File.Exists(filePath))
                {
                    var newFilePath = Path.Combine(System.IO.Path.GetDirectoryName(filePath), "DEL_" + System.IO.Path.GetFileName(filePath));

                    File.Move(filePath, newFilePath);
                }

                ent.IsDeleted = true;
                ent.Save();
            }
        }
    }
}