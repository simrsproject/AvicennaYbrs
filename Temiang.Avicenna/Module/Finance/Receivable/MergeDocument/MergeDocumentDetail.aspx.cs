using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf.Streaming;
using Telerik.Windows.Documents.Fixed.Model.Editing;
using Telerik.Windows.Documents.Fixed.Model;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Interfaces;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf;
using Telerik.Windows.Documents.Fixed.Model.Resources;

namespace Temiang.Avicenna.Module.Finance.Receivable
{
    public partial class MergeDocumentDetail : BasePage
    {
        private string _regType=null;
        public override string RegistrationType
        {
            get
            {
                if (_regType == null)
                {
                    var reg = new Registration();
                    reg.Query.Select(reg.Query.SRRegistrationType);
                    reg.Query.Where(reg.Query.RegistrationNo == Request.QueryString["regno"]);
                    if (reg.Query.Load())
                        _regType = reg.SRRegistrationType;
                    else
                        _regType = String.Empty;
                }
                return _regType;
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.AR_MERGE_RECEIPT;

            if (!IsCallback)
            {
                //For Grid Detail
                PopUpSearch.RegisterClientScript(AppEnum.PopUpSearch.Item, Page);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            txtRegistrationNo.Text = Request.QueryString["regno"];

            var reg = new Registration();
            if (reg.LoadByPrimaryKey(txtRegistrationNo.Text))
            {               
                var pat = new Patient();
                pat.LoadByPrimaryKey(reg.str.PatientID);
                txtMedicalNo.Text = pat.MedicalNo;
                var std = new AppStandardReferenceItem();
                txtSalutation.Text = std.LoadByPrimaryKey("Salutation", pat.SRSalutation) ? std.ItemName : string.Empty;
                txtPatientName.Text = pat.PatientName;
                txtPlaceDOB.Text = pat.CityOfBirth;
                txtDateOfBirth.SelectedDate = pat.DateOfBirth;
                txtGender.Text = pat.Sex;
                txtAgeInYear.Text = Convert.ToString(reg.AgeInYear);
                txtAgeInMonth.Text = Convert.ToString(reg.AgeInMonth);
                txtAgeInDay.Text = Convert.ToString(reg.AgeInDay);

                var unit = new ServiceUnit();
                unit.LoadByPrimaryKey(reg.ServiceUnitID);
                txtServiceUnitID.Text = unit.ServiceUnitID;
                lblServiceUnitName.Text = unit.ServiceUnitName;

                var room = new ServiceRoom();
                room.LoadByPrimaryKey(reg.RoomID);
                txtRoomID.Text = room.RoomID;
                lblRoomName.Text = room.RoomName;

                var cls = new Class();
                cls.LoadByPrimaryKey(reg.ChargeClassID);
                txtClassID.Text = cls.ClassID;
                lblClassName.Text = cls.ClassName;

                cls = new Class();
                cls.LoadByPrimaryKey(reg.CoverageClassID);
                txtCoverageClass.Text = cls.ClassID;
                lblCoverageClassName.Text = cls.ClassName;

                txtBedID.Text = reg.BedID;

                var par = new Paramedic();
                par.LoadByPrimaryKey(reg.ParamedicID);
                txtParamedicID.Text = par.ParamedicID;
                lblParamedicName.Text = par.ParamedicName;

                var guar = new Guarantor();
                guar.LoadByPrimaryKey((string.IsNullOrEmpty(pat.str.MemberID) ? reg.GuarantorID : pat.MemberID));
                txtGuarantorID.Text = guar.GuarantorID;
                lblGuarantorName.Text = guar.GuarantorName;

                var x = reg.DischargeDate != null ? reg.DischargeDate.Value.Date : (new DateTime()).NowAtSqlServer().Date;
                var y = reg.RegistrationDate.Value.Date;
                txtLengthOfStay.Value = (x - y).TotalDays == 0 ? 1 : (x - y).TotalDays + 1;

                PopulatePatientImage(pat.PatientID);
                
            }            
        }
     

        private string[] MergeRegistrationList()
        {
            if (ViewState["BillingVerification:MergeRegistration" + Request.UserHostName] == null)
                ViewState["BillingVerification:MergeRegistration" + Request.UserHostName] = Helper.MergeBilling.GetMergeRegistration(txtRegistrationNo.Text);

            return (string[])ViewState["BillingVerification:MergeRegistration" + Request.UserHostName];
        }
        
        protected string GetStatus(object isOrder, object IsOrderRealization, object IsApprove)
        {
            if (IsApprove.Equals(false))
                return "<img src=\"../../../../../Images/Toolbar/post16_d.png\" border=\"0\" />";
            else
            {
                if (isOrder.Equals(false))
                    return "<img src=\"../../../../../Images/Toolbar/post16.png\" border=\"0\" />";
                else
                {
                    if (IsOrderRealization.Equals(false))
                        return "<img src=\"../../../../../Images/Toolbar/post16_d.png\" border=\"0\" />";
                    else
                        return "<img src=\"../../../../../Images/Toolbar/post16.png\" border=\"0\" />";
                }
            }
        }


        #region PatientImage
        private void PopulatePatientImage(string patientID)
        {
            //// Patient Photo
            //imgPatientPhoto.ImageUrl = string.Empty;

            //// Load from database
            //var patientImg = new PatientImage();
            //if (patientImg.LoadByPrimaryKey(patientID))
            //{
            //    // Show Image
            //    imgPatientPhoto.ImageUrl = string.Format("data:image/jpg;base64,{0}",
            //        Convert.ToBase64String(patientImg.Photo));
            //}

            // Load from database
            var patientImg = new PatientImage();
            if (patientImg.LoadByPrimaryKey(patientID))
            {
                // Show Image
                if (patientImg.Photo != null)
                {
                    imgPatientPhoto.ImageUrl = string.Format("data:image/jpg;base64,{0}",
                        Convert.ToBase64String(patientImg.Photo));
                }
                else
                {
                    imgPatientPhoto.ImageUrl = txtGender.Text == "M" ? "~/Images/Asset/Patient/ManVector.png" : "~/Images/Asset/Patient/WomanVector.png";
                }
            }
            else
                imgPatientPhoto.ImageUrl = txtGender.Text == "M" ? "~/Images/Asset/Patient/ManVector.png" : "~/Images/Asset/Patient/WomanVector.png";

        }
        #endregion
     

        protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
        {
            base.RaisePostBackEvent(source, eventArgument);
            if (string.IsNullOrEmpty(eventArgument)) return;
           

            var args = eventArgument.Split('|').Count() > 0 ? eventArgument.Split('|')[0] : eventArgument;
            switch (args)
            {
                case "mergeDocuments":
                    MergeFiles();
                    grdDocument.Rebind();
                    break;                               
                case "rebindDocument":
                    grdDocument.Rebind();
                    break;
                case "rebindAll":                    
                    grdDocument.Rebind();                    
                    break;
            }
        }
       
        private void PopulateDocuments()
        {
            var reg = new Registration();
            reg.LoadByPrimaryKey(txtRegistrationNo.Text);

            var gdcc = new GuarantorDocumentChecklistCollection();
            gdcc.Query.Where(gdcc.Query.GuarantorID == txtGuarantorID.Text, gdcc.Query.SRRegistrationType == reg.SRRegistrationType);
            if (gdcc.Query.Load())
            {
                foreach (var gdc in gdcc)
                {
                    var dcdc = new BusinessObject.DocumentChecklistDefinitionCollection();
                    dcdc.Query.Where(dcdc.Query.SRDocumentChecklist == gdc.SRDocumentChecklist);
                    if (!dcdc.Query.Load()) continue;
                    foreach (var dcd in dcdc)
                    {
                        var rdcl = new BusinessObject.RegistrationDocumentCheckList();
                        if (!rdcl.LoadByPrimaryKey(txtRegistrationNo.Text, dcd.DocumentFilesID ?? 0))
                        {
                            rdcl = new BusinessObject.RegistrationDocumentCheckList();
                            rdcl.RegistrationNo = txtRegistrationNo.Text;
                            rdcl.DocumentFilesID = dcd.DocumentFilesID;
                            rdcl.str.LastUpdateDateTime = string.Empty;
                            rdcl.LastUpdateByUserID = string.Empty;
                            rdcl.FileName = string.Empty;

                            rdcl.Save();
                        }
                    }
                }
            }
        }

        protected void ToggleSelectedStateDocuments(object sender, EventArgs e)
        {
            var selected = ((CheckBox)sender).Checked;

            foreach (GridDataItem dataItem in grdDocument.MasterTableView.Items)
            {
                ((CheckBox)dataItem.FindControl("detailChkbox")).Checked = selected;
            }
        }

        protected void grdDocument_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var reg = new Registration();
            reg.LoadByPrimaryKey(txtRegistrationNo.Text);

            var bpjs = new BusinessObject.BpjsSEP();
            bpjs.LoadByPrimaryKey(reg.BpjsSepNo);

            var grr = new Guarantor();
            grr.LoadByPrimaryKey(reg.GuarantorID);
            grr.Query.Where(grr.Query.GuarantorHeaderID > string.Empty);
            grr.Query.es.Top = 1;
            grr.Query.Load();

            if (grr.SRGuarantorType == AppSession.Parameter.GuarantorTypeBpjs)
            {
                var endFolderName = grr.LoadByPrimaryKey(reg.GuarantorID) ? (bpjs.NoSEP ?? (reg.BpjsSepNo ?? txtRegistrationNo.Text)) : txtRegistrationNo.Text;
                endFolderName = endFolderName.Trim().Replace("/", "-");

                var sepFolder = AppParameter.GetParameterValue(AppParameter.ParameterItem.SepFolder);
                if (string.IsNullOrWhiteSpace(sepFolder)) sepFolder = Path.Combine(AppSession.Parameter.ApplicationDocumentFolder, "BpjsSepDocument");

                //sepFolder = "\\\\26.32.31.198\\DOCUMENT_AVICENNA";

                string path = string.Format("{0}\\{1:0000}\\{2:00}\\{3:00}\\{5:00}\\{4}", sepFolder, reg.RegistrationDate?.Year, reg.RegistrationDate?.Month, grr.GuarantorHeaderID, endFolderName, reg.RegistrationDate?.Day);

                var list = new List<DirFileModel>();

                if (Directory.Exists(path))
                {
                    var dirListModel = MapDirs(path);
                    var fileListModel = MapFiles(path);
                    var explorerModel = new ExplorerModel(dirListModel, fileListModel);

                    list.AddRange(explorerModel.FileModelList.Where(d => d.FileName != "Thumbs.db").Select(d => new DirFileModel()
                    {
                        Type = 2,
                        Url = string.Format("{0}/{1:0000}/{2:00}/{3:00}/{5:00}/{4}/{6}", ConfigurationManager.AppSettings["CasemixDocumentVirtualUrl"], reg.RegistrationDate?.Year, reg.RegistrationDate?.Month, grr.GuarantorHeaderID, endFolderName, reg.RegistrationDate?.Day, d.FileName),
                        //Url = ConfigurationManager.AppSettings["CasemixDocumentVirtualUrl"] + "//" + d.FileName,
                        Path = path + "\\" + d.FileName,
                        Name = d.FileName,
                        Size = d.FileSizeText,
                        Accessed = d.FileAccessed
                    }));
                }

                grdDocument.DataSource = list;
            }
            else
            {
                var endFolderName = grr.LoadByPrimaryKey(reg.GuarantorID) ? (/*tpa.PaymentNo ?? (tpa.PaymentNo ?? */txtRegistrationNo.Text/*)*/) : txtRegistrationNo.Text;
                endFolderName = endFolderName.Trim().Replace("/", "-");

                var sepFolder = AppParameter.GetParameterValue(AppParameter.ParameterItem.SepFolder);
                if (string.IsNullOrWhiteSpace(sepFolder)) sepFolder = Path.Combine(AppSession.Parameter.ApplicationDocumentFolder, "SepDocument");

                //sepFolder = "\\\\26.32.31.198\\DOCUMENT_AVICENNA";

                string path = string.Format("{0}\\{1:0000}\\{2:00}\\{3:00}\\{5:00}\\{4}", sepFolder, reg.RegistrationDate?.Year, reg.RegistrationDate?.Month, grr.GuarantorHeaderID, endFolderName, reg.RegistrationDate?.Day);
                //string[] files = Directory.GetFiles(path);            
                var list = new List<DirFileModel>();

                if (Directory.Exists(path))
                {
                    var dirListModel = MapDirs(path);
                    var fileListModel = MapFiles(path);
                    var explorerModel = new ExplorerModel(dirListModel, fileListModel);

                    list.AddRange(explorerModel.FileModelList.Where(d => d.FileName != "Thumbs.db").Select(d => new DirFileModel()
                    {
                        Type = 2,
                        Url = string.Format("{0}/{1:0000}/{2:00}/{3:00}/{5:00}/{4}/{6}", ConfigurationManager.AppSettings["DocumentReceiptUrl"], reg.RegistrationDate?.Year, reg.RegistrationDate?.Month, grr.GuarantorHeaderID, endFolderName, reg.RegistrationDate?.Day, d.FileName),
                        Path = path + "\\" + d.FileName,
                        Name = d.FileName,
                        Size = d.FileSizeText,
                        Accessed = d.FileAccessed
                    }));
                }

                grdDocument.DataSource = list;
            }
        }

        public class DirModel
        {
            public string DirName { get; set; }
            public DateTime DirAccessed { get; set; }
        }

        public class FileModel
        {
            public string FileName { get; set; }
            public string FileSizeText { get; set; }
            public DateTime FileAccessed { get; set; }
        }

        public class ExplorerModel
        {
            public List<DirModel> DirModelList;
            public List<FileModel> FileModelList;

            public string FolderName;
            public string ParentFolderName;
            public string URL;

            public ExplorerModel(List<DirModel> dirModelList, List<FileModel> fileModelList)
            {
                DirModelList = dirModelList;
                FileModelList = fileModelList;
            }
        }

        private List<DirModel> MapDirs(string realPath)
        {
            List<DirModel> dirListModel = new List<DirModel>();

            IEnumerable<string> dirList = Directory.EnumerateDirectories(realPath);
            foreach (string dir in dirList)
            {
                DirectoryInfo d = new DirectoryInfo(dir);

                DirModel dirModel = new DirModel
                {
                    DirName = Path.GetFileName(dir),
                    DirAccessed = d.LastAccessTime
                };

                dirListModel.Add(dirModel);
            }

            return dirListModel;
        }

        private List<FileModel> MapFiles(string realPath)
        {
            List<FileModel> fileListModel = new List<FileModel>();

            IEnumerable<string> fileList = Directory.EnumerateFiles(realPath);
            foreach (string file in fileList)
            {
                FileInfo f = new FileInfo(file);

                FileModel fileModel = new FileModel();

                //if (f.Extension.ToLower() != "php" && f.Extension.ToLower() != "aspx"
                //    && f.Extension.ToLower() != "asp" && f.Extension.ToLower() != "exe")
                {
                    fileModel.FileName = Path.GetFileName(file);
                    fileModel.FileAccessed = f.LastAccessTime;
                    fileModel.FileSizeText = (f.Length < 1024) ?
                                    f.Length.ToString() + " B" : f.Length / 1024 + " KB";

                    fileListModel.Add(fileModel);
                }
            }

            return fileListModel;
        }

        private class DirFileModel
        {
            public short Type { get; set; }
            public string Path { get; set; }
            public string Name { get; set; }
            public string Size { get; set; }
            public string Url { get; set; }
            public DateTime Accessed { get; set; }
        }

        protected void grdDocument_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;
            string name = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Name"]);

            //var realPath = "D:\\";

            var reg = new Registration();
            reg.LoadByPrimaryKey(txtRegistrationNo.Text);

            var bpjs = new BusinessObject.BpjsSEP();
            bpjs.LoadByPrimaryKey(reg.BpjsSepNo);

            var grr = new Guarantor();
            grr.LoadByPrimaryKey(reg.GuarantorID);
            grr.Query.Where(grr.Query.GuarantorHeaderID > string.Empty);
            grr.Query.es.Top = 1;
            grr.Query.Load();

            if (grr.SRGuarantorType == AppSession.Parameter.GuarantorTypeBpjs)
            {
                var endFolderName = grr.LoadByPrimaryKey(reg.GuarantorID) ? (bpjs.NoSEP ?? txtRegistrationNo.Text) : txtRegistrationNo.Text;
                endFolderName = endFolderName.Trim().Replace("/", "-");

                var sepFolder = AppParameter.GetParameterValue(AppParameter.ParameterItem.SepFolder);
                if (string.IsNullOrWhiteSpace(sepFolder)) sepFolder = Path.Combine(AppSession.Parameter.ApplicationDocumentFolder, "BpjsSepDocument");

                string path = string.Format("{0}\\{1:0000}\\{2:00}\\{3:00}\\{5:00}\\{4}", sepFolder, reg.RegistrationDate?.Year, reg.RegistrationDate?.Month, grr.GuarantorHeaderID, endFolderName, reg.RegistrationDate?.Day);

                if (File.Exists(path + "\\" + name))
                {
                    File.Delete(path + "\\" + name);

                    grdDocument.Rebind();
                }
            }
            else
            {
                var endFolderName = grr.LoadByPrimaryKey(reg.GuarantorID) ? (/*bpjs.NoSEP ??*/ txtRegistrationNo.Text) : txtRegistrationNo.Text;
                endFolderName = endFolderName.Trim().Replace("/", "-");

                var sepFolder = AppParameter.GetParameterValue(AppParameter.ParameterItem.SepFolder);
                if (string.IsNullOrWhiteSpace(sepFolder)) sepFolder = Path.Combine(AppSession.Parameter.ApplicationDocumentFolder, "SepDocument");

                string path = string.Format("{0}\\{1:0000}\\{2:00}\\{3:00}\\{5:00}\\{4}", sepFolder, reg.RegistrationDate?.Year, reg.RegistrationDate?.Month, grr.GuarantorHeaderID, endFolderName, reg.RegistrationDate?.Day);

                if (File.Exists(path + "\\" + name))
                {
                    File.Delete(path + "\\" + name);

                    grdDocument.Rebind();
                }
            }
        }

        private void MergeFiles()
        {
            var documentsToMerge = new List<string>();

            foreach (GridDataItem item in grdDocument.MasterTableView.Items)
            {
                if (!((CheckBox)item.FindControl("detailChkbox")).Checked) continue;
                var order = ((RadNumericTextBox)item.FindControl("orderTextBox")).Value ?? 0;
                documentsToMerge.Add($"{order}#{item["Path"].Text}");
            }

            if (!documentsToMerge.Any()) return;

            var document = new RadFixedDocument();
            var provider = new PdfFormatProvider();
            foreach (string documentName in documentsToMerge.OrderBy(t => t))
            {
                using (Stream input = File.OpenRead(documentName.Split('#')[1]))
                {
                    if (documentName.Contains(".pdf")) document.Merge(provider.Import(input));
                    else if (documentName.Contains(".jpg") || documentName.Contains(".jpeg") || documentName.Contains(".png") || documentName.Contains(".bmp"))
                    {
                        var imageSource = new ImageSource(input);
                        var lastPage = document.Pages.AddPage();
                        var editor = new FixedContentEditor(lastPage);

                        editor.Position.Translate(offsetX: 50, offsetY: 50);
                        editor.DrawImage(imageSource);
                    }
                }
            }

            //var path = "D:\\testdoc\\";

            var reg = new Registration();
            reg.LoadByPrimaryKey(txtRegistrationNo.Text);

            var bpjs = new BusinessObject.BpjsSEP();
            bpjs.LoadByPrimaryKey(reg.BpjsSepNo);

            var grr = new Guarantor();
            grr.LoadByPrimaryKey(reg.GuarantorID);
            grr.Query.Where(grr.Query.GuarantorHeaderID > string.Empty);
            grr.Query.es.Top = 1;
            grr.Query.Load();

            if (grr.SRGuarantorType == AppSession.Parameter.GuarantorTypeBpjs)
            {
                var endFolderName = grr.LoadByPrimaryKey(reg.GuarantorID) ? (bpjs.NoSEP ?? (reg.BpjsSepNo ?? txtRegistrationNo.Text)) : txtRegistrationNo.Text;
                endFolderName = endFolderName.Trim().Replace("/", "-");

                var sepFolder = AppParameter.GetParameterValue(AppParameter.ParameterItem.SepFolder);
                if (string.IsNullOrWhiteSpace(sepFolder)) sepFolder = Path.Combine(AppSession.Parameter.ApplicationDocumentFolder, "BpjsSepDocument");

                string path = string.Format("{0}\\{1:0000}\\{2:00}\\{3:00}\\{5:00}\\{4}\\", sepFolder, reg.RegistrationDate?.Year, reg.RegistrationDate?.Month, grr.GuarantorHeaderID, endFolderName, reg.RegistrationDate?.Day);
                var exportedDocument = $"{path}{reg.BpjsSepNo}_{DateTime.Now.Date:yyyyMMdd}_Merged.pdf";

                //if (string.IsNullOrWhiteSpace(sepFolder))
                //    sepFolder = Path.Combine(AppSession.Parameter.ApplicationDocumentFolder, "BpjsSepDocument");

                // Create Directory
                //var path = Path.GetDirectoryName(exportedDocument);
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                // Delete
                if (File.Exists(exportedDocument)) File.Delete(exportedDocument);

                //Create File
                using (Stream output = File.OpenWrite(exportedDocument))
                {
                    provider.Export(document, output);
                }

                //Helper.DownloadFile(Response, exportedDocument);
            }
            else
            {
                var endFolderName = grr.LoadByPrimaryKey(reg.GuarantorID) ? (/*bpjs.NoSEP ??*/ (/*reg.BpjsSepNo ??*/ txtRegistrationNo.Text)) : txtRegistrationNo.Text;
                endFolderName = endFolderName.Trim().Replace("/", "-");

                var sepFolder = AppParameter.GetParameterValue(AppParameter.ParameterItem.SepFolder);
                if (string.IsNullOrWhiteSpace(sepFolder)) sepFolder = Path.Combine(AppSession.Parameter.ApplicationDocumentFolder, "SepDocument");

                string path = string.Format("{0}\\{1:0000}\\{2:00}\\{3:00}\\{5:00}\\{4}\\", sepFolder, reg.RegistrationDate?.Year, reg.RegistrationDate?.Month, grr.GuarantorHeaderID, endFolderName, reg.RegistrationDate?.Day);
                var exportedDocument = $"{path}{DateTime.Now.Date:yyyyMMdd}_{endFolderName}_Merged.pdf";

                //if (string.IsNullOrWhiteSpace(sepFolder))
                //    sepFolder = Path.Combine(AppSession.Parameter.ApplicationDocumentFolder, "BpjsSepDocument");

                // Create Directory
                //var path = Path.GetDirectoryName(exportedDocument);
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                // Delete
                if (File.Exists(exportedDocument)) File.Delete(exportedDocument);

                //Create File
                using (Stream output = File.OpenWrite(exportedDocument))
                {
                    provider.Export(document, output);
                }

                //Helper.DownloadFile(Response, exportedDocument);
            }
        }
    }
}
