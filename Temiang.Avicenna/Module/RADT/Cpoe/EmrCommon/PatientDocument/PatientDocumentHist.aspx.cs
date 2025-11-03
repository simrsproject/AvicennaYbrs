using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Emr
{
    public partial class PatientDocumentHist : BasePageDialog
    {
        private List<string> _patientRelateds;
        protected List<string> PatientRelateds
        {
            get
            {
                if (_patientRelateds == null)
                {
                    _patientRelateds = Patient.PatientRelateds(PatientID);
                }

                return _patientRelateds;
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
            if (!string.IsNullOrWhiteSpace(Request.QueryString["progid"]))
            {
                ProgramID = Request.QueryString["progid"];
            }
            else
                ProgramID = AppConstant.Program.ElectronicMedicalRecord;

            if (!IsPostBack)
            {
                var pat = new Patient();
                if (pat.LoadByPrimaryKey(PatientID))
                {
                    this.Title = pat.PatientName + " (MRN: " + pat.MedicalNo + ")";
                }
                Footer.Visible = false;

                //dokter dikasih akses buat upload gambar
                //if (string.IsNullOrWhiteSpace(Request.QueryString["surgno"]))
                //{
                //    if (AppSession.UserLogin.SRUserType == AppUser.UserType.Doctor)
                //    {
                //        //tbarMain.Items[0].Style.Add("display", "none");
                //        tbarMain.Items[0].Enabled = false;
                //        tbarMain.Items[1].Enabled = false;
                //    }
                //}

                // Filter RegNo
                tbarMain.Items[2].Visible = !string.IsNullOrWhiteSpace(RegistrationNo);
                tbarMain.Items[3].Visible = !string.IsNullOrWhiteSpace(RegistrationNo);

                if (Request.QueryString["lockreg"] == "1")
                {
                    var chkReg = ((RadCheckBox)tbarMain.Items[3].FindControl("chkIsCurrentReg"));
                    chkReg.Checked = true;
                    chkReg.Enabled = false;
                    chkReg.Text = string.Format("{0} ", RegistrationNo);
                }
            }
        }

        private DataTable PatientDocumentDataTable()
        {
            var query = new PatientDocumentQuery("a");

            if (PatientRelateds.Count == 1)
                query.Where(query.PatientID == PatientID);
            else
                query.Where(query.PatientID.In(PatientRelateds));

            query.Where(query.Or(query.IsDeleted.IsNull(), query.IsDeleted == false));

            var isCurrentEp = ((RadCheckBox)tbarMain.Items[3].FindControl("chkIsCurrentEpisode")).Checked ?? false;
            if (isCurrentEp)
                query.Where(query.RegistrationNo.In(MergeRegistrations));

            var searchName = ((RadTextBox)tbarMain.Items[5].FindControl("txtQuickSearch")).Text;
            if (!string.IsNullOrWhiteSpace(searchName))
                query.Where(query.DocumentName.Like("%" + searchName.Trim() + "%"));

            if (!string.IsNullOrWhiteSpace(Request.QueryString["surgno"]))
                query.Where(query.ReferenceNo == Request.QueryString["surgno"].ToString());
            else
                query.Where(query.Or(query.ReferenceNo.IsNull(), query.ReferenceNo == string.Empty));

            query.OrderBy(query.DocumentDate.Descending, query.PatientDocumentID.Descending);

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
                            var entity = new PatientDocument();
                            if (entity.LoadByPrimaryKey(row["PatientDocumentID"].ToInt()))
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
            grdDocument.DataSource = PatientDocumentDataTable();
        }

        protected void grdDocument_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;
            var pdid = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex]["PatientDocumentID"]);

        }


    }
}

