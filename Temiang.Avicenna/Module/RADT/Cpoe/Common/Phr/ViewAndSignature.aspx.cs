using System;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Media;
using Telerik.Windows.Documents.Common.FormatProviders;
using Telerik.Windows.Documents.Flow.FormatProviders.Docx;
using Telerik.Windows.Documents.Flow.FormatProviders.Pdf;
using Telerik.Windows.Documents.Flow.Model;
using Telerik.Windows.Documents.Flow.Model.Editing;
using Temiang.Avicenna.Common;
using Telerik.Windows.Documents.Flow.Model.Styles;
using Telerik.Windows.Documents.Spreadsheet.Model;
using Temiang.Avicenna.BusinessObject;
using BorderStyle = Telerik.Windows.Documents.Flow.Model.Styles.BorderStyle;
using System.Collections.Generic;
using System.Web.Services;
using Telerik.Windows.Documents.Flow.Model.Shapes;

namespace Temiang.Avicenna.Module.Emr.Phr
{
    [System.Web.Script.Services.ScriptService]
    public partial class ViewAndSignature : BasePage
    {
        private string QuestionFormID
        {
            get { return AppSession.PrintJobParameters.FindByParameterName("p_QuestionFormID").ValueString; }
        }
        private string TransactionNo
        {
            get { return AppSession.PrintJobParameters.FindByParameterName("p_TransactionNo").ValueString; }
        }
        private string RegistrationNo
        {
            get { return AppSession.PrintJobParameters.FindByParameterName("p_RegistrationNo").ValueString; }
        }


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            var wordFile = string.Empty;
            var reg = new Registration();
            reg.LoadByPrimaryKey(RegistrationNo);

            if (!IsPostBack)
                LoadToPdf(reg, false);
            else
            {
                if (Request.Params.Get("__EVENTARGUMENT") == "signature" && !string.IsNullOrWhiteSpace(hdnImage.Value))
                {
                    if (SaveSignature())
                    {
                        hdnImage.Value = string.Empty; // Reset
                        LoadToPdf(reg, true);
                    }
                }
                else if (Request.Params.Get("__EVENTARGUMENT") == "saveToGuarantorDoc")
                {
                    var resultMsg = SaveToGuarantorDoc();
                    resultMsg = resultMsg.Replace("\\", "\\\\");
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "saveMsg", string.Format("alert('{0}');", resultMsg), true);
                }
                else if (Request.Params.Get("__EVENTARGUMENT") == "downloadWordFile")
                {
                    var doc = LoadAndReplaceWordFile(reg, out wordFile);

                    byte[] renderedBytes = null;
                    IFormatProvider<RadFlowDocument> formatProvider = new DocxFormatProvider();
                    using (MemoryStream ms = new MemoryStream())
                    {
                        formatProvider.Export(doc, ms);
                        renderedBytes = ms.ToArray();
                    }

                    Response.ClearHeaders();
                    Response.ClearContent();
                    Response.AppendHeader("content-disposition", "attachment; filename=DownloadFile.docx");
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    Response.BinaryWrite(renderedBytes);
                    Response.End();
                }
            }
        }

        private RadFlowDocument LoadAndReplaceWordFile(Registration reg, out string wordFile)
        {
            var isUsingReportHeader = false;
            var qform = new QuestionForm();
            qform.LoadByPrimaryKey(QuestionFormID);

            var appProgramHC = new AppProgramHealthcare();
            if (appProgramHC.LoadByPrimaryKey(qform.ReportProgramID, AppSession.Parameter.HealthcareInitial))
            {
                wordFile = appProgramHC.StoreProcedureName;
                isUsingReportHeader = appProgramHC.IsUsingReportHeader ?? false;
            }
            else
            {
                var prg = new AppProgram();
                prg.LoadByPrimaryKey(qform.ReportProgramID);
                wordFile = prg.StoreProcedureName;
                isUsingReportHeader = prg.IsUsingReportHeader ?? false;
            }

            // Lengkapi
            var wordFilePath = Path.Combine(AppSession.Parameter.ApplicationDocumentFolder, "PhrWordDocTemplate", wordFile);

            // Load and Replace Doc
            var fileFormatProvider = new DocxFormatProvider();

            RadFlowDocument document;
            using (FileStream input = new FileStream(wordFilePath, FileMode.Open))
            {
                document = fileFormatProvider.Import(input);
            }

            PopulateDocument(document, isUsingReportHeader, reg);
            return document;

        }
        private byte[] LoadToPdf(Registration reg, bool isSaveToPatientDocument)
        {
            var wordFile = string.Empty;
            var document = LoadAndReplaceWordFile(reg, out wordFile);
            // Export to PDF File
            byte[] renderedBytes = null;

            IFormatProvider<RadFlowDocument> formatProvider = new PdfFormatProvider();
            using (MemoryStream ms = new MemoryStream())
            {
                formatProvider.Export(document, ms);
                renderedBytes = ms.ToArray();

                if (isSaveToPatientDocument)
                {
                    var qf = new QuestionForm();
                    qf.LoadByPrimaryKey(QuestionFormID);

                    var entity = new PatientDocument();
                    entity.PatientID = reg.PatientID;
                    entity.RegistrationNo = RegistrationNo;
                    entity.DocumentName = qf.QuestionFormName;
                    entity.DocumentDate = DateTime.Now;
                    entity.Notes = string.Format("Generated from file: {0} with data PHR Form: {1} Doc No: {2} @ {3}", wordFile, qf.QuestionFormID, TransactionNo, DateTime.Now.ToString(AppConstant.DisplayFormat.LongDatePattern));
                    entity.FileAttachName = string.Empty; //tidak boleh null
                    entity.OriFileName = string.Empty;
                    entity.Save(); // Save dulu supaya PatientDocumentID terisi

                    //var targetFolder = Path.Combine(AppSession.Parameter.ApplicationDocumentFolder, "PatientDocument", entity.PatientID.Trim());

                    //if (!Directory.Exists(targetFolder))
                    //    Directory.CreateDirectory(targetFolder);

                    var targetFolderOld = Path.Combine(AppSession.Parameter.ApplicationDocumentFolder, "PatientDocument", entity.PatientID.Trim());
                    var targetFolderYearly = "";
                    if (!string.IsNullOrEmpty(entity.DocumentFolderYearly))
                        targetFolderYearly = Path.Combine(AppSession.Parameter.ApplicationDocumentFolder, "PatientDocumentYearly", entity.DocumentFolderYearly, entity.PatientID.Trim());

                    var targetFolder = targetFolderOld;
                    if (!System.IO.Directory.Exists(targetFolder))
                    {
                        // jika old blm ada brarti pakai yearly
                        targetFolder = string.IsNullOrEmpty(targetFolderYearly) ? targetFolderOld : targetFolderYearly;
                    }

                    if (!System.IO.Directory.Exists(targetFolder))
                        System.IO.Directory.CreateDirectory(targetFolder);

                    var pdfFileName = string.Format("{0:000000000000000}_{1}.pdf", entity.PatientDocumentID, QuestionFormID.Replace('.', '_'));
                    var filePath = Path.Combine(targetFolder, pdfFileName);
                    if (File.Exists(filePath))
                        File.Delete(filePath);

                    File.WriteAllBytes(filePath, renderedBytes);

                    entity.FileAttachName = pdfFileName;
                    entity.OriFileName = pdfFileName;
                    entity.Save();
                }
            }

            pdfViewer.PdfjsProcessingSettings.FileSettings.Data = Convert.ToBase64String(renderedBytes);
            return renderedBytes;
        }

        private PatientHealthRecordLine FindLineInSingleGroup(PatientHealthRecordLineCollection lines, string questionID)
        {
            foreach (PatientHealthRecordLine recordLine in lines)
            {
                if (recordLine.QuestionID == questionID)
                    return recordLine;
            }
            return new PatientHealthRecordLine();
        }

        private void PopulateDocument(RadFlowDocument document, bool isUsingReportHeader, Registration reg)
        {
            var editor = new RadFlowDocumentEditor(document);

            var phrLines = new PatientHealthRecordLineCollection();
            var query = new PatientHealthRecordLineQuery("phrl");
            var quest = new QuestionQuery("q");
            query.InnerJoin(quest).On(query.QuestionID == quest.QuestionID);
            query.Select(query, quest.SRAnswerType.As("refToQuestion_SRAnswerType"));
            query.Where(
                query.RegistrationNo == RegistrationNo, query.QuestionFormID == QuestionFormID, query.TransactionNo == TransactionNo);
            phrLines.Load(query);


            int LengthOfStay;

            var reg2 = new Registration();
            reg2.LoadByPrimaryKey(reg.RegistrationNo);
            {
                var x = reg2.DischargeDate != null ? reg2.DischargeDate.Value.Date : (new DateTime()).NowAtSqlServer().Date;
                var y = reg2.RegistrationDate.Value.Date;

                double v = (x - y).TotalDays == 0 ? 1 : (x - y).TotalDays + 1;
                LengthOfStay = (int)v;

            }

            var responsible = new RegistrationResponsiblePerson();
            responsible.LoadByPrimaryKey(reg.RegistrationNo);

            // Common Field
            var pat = new Patient();
            pat.LoadByPrimaryKey(reg.PatientID);

            // User Name
            var lastUpdateByUserName = string.Empty;
            var phr = new PatientHealthRecord();
            if (phr.LoadByPrimaryKey(TransactionNo, RegistrationNo, QuestionFormID))
            {
                var usr = new AppUser();
                if (usr.LoadByPrimaryKey(phr.LastUpdateByUserID))
                    lastUpdateByUserName = usr.UserName;
            }

            //var qf = new QuestionForm();
            //qf.LoadByPrimaryKey(query.ques);


            editor.ReplaceText("[PATIENTNAME]", pat.PatientName, false, false);
            editor.ReplaceText("[MEDICALNO]", pat.MedicalNo, false, false);
            editor.ReplaceText("[PATIENTADDR]", pat.Address, false, false);
            editor.ReplaceText("[PATIENTSEX]", pat.Sex == "M" ? "L" : "P", false, false);
            editor.ReplaceText("[PATIENTDOB]", Convert.ToDateTime(pat.DateOfBirth).ToString(AppConstant.DisplayFormat.DateShortMonth), false, false);
            editor.ReplaceText("[PATIENTAGE]", string.Format("{0}/{1}/{2}", reg.AgeInYear, reg.AgeInMonth, reg.AgeInDay), false, false);
            editor.ReplaceText("[PATIENTHP]", pat.MobilePhoneNo, false, false);
            editor.ReplaceText("[DATE]", DateTime.Today.ToString(AppConstant.DisplayFormat.DateShortMonth), false, false);
            editor.ReplaceText("[DATETIME]", DateTime.Today.ToString(AppConstant.DisplayFormat.LongDatePattern), false, false);
            editor.ReplaceText("[PARAMEDICNAME]", ParamedicTeam.DPJP(reg.RegistrationNo).ParamedicName, false, false);
            editor.ReplaceText("[TRANSACTIONNO]", TransactionNo, false, false);
            editor.ReplaceText("[REGISTRATIONDATE]", Convert.ToDateTime(reg.RegistrationDate).ToString("dd-MM-yyyy"), false, false);
            editor.ReplaceText("[REGISTRATIONTIME]", Convert.ToDateTime(reg.RegistrationTime).ToString("HH:mm"), false, false);
            editor.ReplaceText("[PATIENTAGE2]", string.Format("{0}", reg.AgeInYear), false, false);
            editor.ReplaceText("[PATIENTSEX2]", pat.Sex == "M" ? "Laki-laki" : "Perempuan", false, false);
            editor.ReplaceText("[USERNAME]", lastUpdateByUserName, false, false);
            editor.ReplaceText("[KTP]", pat.Ssn, false, false);
            editor.ReplaceText("[DischargeDate]", Convert.ToDateTime(reg.DischargeDate).ToString("dd-MM-yyyy"), false, false);
            editor.ReplaceText("[DischargeTime]", Convert.ToDateTime(reg.DischargeTime).ToString("HH:mm"), false, false);
            editor.ReplaceText("[LamaRawat]", LengthOfStay.ToString(), false, false);
            editor.ReplaceText("[ResponsiblePerson]", responsible.NameOfTheResponsible, false, false);
            editor.ReplaceText("[ResponsibleTlp]", responsible.PhoneNo, false, false);
            editor.ReplaceText("[ResponsibleAlamat]", responsible.HomeAddress, false, false);
            //editor.ReplaceText("[USERNAME]", usr.UserName, false, false);

            //editor.ReplaceText("[PAT.NAME]", pat.PatientName, false, false);
            //editor.ReplaceText("[PAT.MRN]", pat.MedicalNo, false, false);
            //editor.ReplaceText("[PAT.ROOM]", ServiceRoom.GetRoomName(reg.RoomID), false, false);
            //editor.ReplaceText("[PAT.ADDR]", pat.Address, false, false);
            //editor.ReplaceText("[PAT.SEX]", pat.Sex == "M" ? "L" : "P", false, false);
            //editor.ReplaceText("[PAT.DOB]", Convert.ToDateTime(pat.DateOfBirth).ToString(AppConstant.DisplayFormat.DateShortMonth), false, false);
            //editor.ReplaceText("[PAT.COB]", pat.CityOfBirth, false, false);
            //editor.ReplaceText("[PAT.COBDOB]", string.Format("{0}, {1}",pat.CityOfBirth,Convert.ToDateTime(pat.DateOfBirth).ToString(AppConstant.DisplayFormat.DateShortMonth)), false, false);
            //editor.ReplaceText("[PAT.AGE]", string.Format("{0}/{1}/{2}", reg.AgeInYear, reg.AgeInMonth, reg.AgeInDay), false, false);
            //editor.ReplaceText("[PAT.HP]", pat.MobilePhoneNo, false, false);


            // Add Header
            if (isUsingReportHeader)
                AddHeader(document, editor);

            // Insert Patient Signature
            var questionID = "SIGN.PAT";
            var patsign = FindLineInSingleGroup(phrLines, questionID);
            if (patsign.BodyImage != null)
            {
                var signByName = FindLineInSingleGroup(phrLines, "SIGN.NAME").QuestionAnswerText;
                var ms = new MemoryStream(patsign.BodyImage);
                AddSignature(document, editor, ms, questionID, signByName);

            }
            editor.ReplaceText("[" + questionID + "]", string.Empty, false, false);

            // Insert User / Paramedic Signature
            questionID = "SIGN.PPA";
            var usrsign = FindLineInSingleGroup(phrLines, questionID);
            if (usrsign.BodyImage != null)
            {
                var ms = new MemoryStream(usrsign.BodyImage);
                AddSignature(document, editor, ms, questionID, AppSession.UserLogin.UserName);

            }
            editor.ReplaceText("[" + questionID + "]", string.Empty, false, false);

            // Additional Entry
            foreach (PatientHealthRecordLine line in phrLines)
            {
                //if (line.QuestionID=="STPN004")
                //{
                //    // Riset import from HTML table
                //    string html = line.QuestionAnswerText; 
                //    Telerik.Windows.Documents.Flow.FormatProviders.Html.HtmlFormatProvider provider = new Telerik.Windows.Documents.Flow.FormatProviders.Html.HtmlFormatProvider(); 
                //    RadFlowDocument table = provider.Import(html);
                    

                //    //foreach (Run run in document.EnumerateChildrenOfType<Run>().ToList())
                //    //{
                //    //    if (run.Text == "[STPN004]")
                //    //    {
                //    //        Paragraph paragraph = run.Paragraph;
                //    //        int childIndex = paragraph.Inlines.IndexOf(run);

 
                //    //        //ImageInline image = new ImageInline(documentX);
                //    //        //using (Stream stream = File.OpenRead("example_image.png"))
                //    //        //{
                //    //        //    image.Image.ImageSource = new Telerik.Windows.Documents.Media.ImageSource(stream, "png");
                //    //        //}
                //    //        //paragraph.Inlines.Insert(childIndex, table);
                //    //        paragraph.Inlines.Remove(run);
                //    //    }
                //    //}

                //    InsertDocumentOptions options = new InsertDocumentOptions(); 
                //    options.ConflictingStylesResolutionMode = ConflictingStylesResolutionMode.RenameSourceStyle; 
                //    options.InsertLastParagraphMarker = false; 

                //    editor.InsertDocument(table,options);

                //}
                if (line.SRAnswerType == "SIG")
                {
                    var sign = FindLineInSingleGroup(phrLines, line.QuestionID);
                    if (sign.BodyImage != null)
                    {
                        var ms = new MemoryStream(sign.BodyImage);
                        AddSignature(document, editor, ms, line.QuestionID, string.Empty);
                        editor.ReplaceText("[" + line.QuestionID + "]", string.Empty, false, false);
                    }
                }
                else if (!string.IsNullOrWhiteSpace(line.QuestionAnswerText))
                    editor.ReplaceText(string.Format("[{0}]", line.QuestionID), line.QuestionAnswerText, false, false);
                else
                    editor.ReplaceText(string.Format("[{0}]", line.QuestionID), string.Empty, false, false);
            }

            foreach (PatientHealthRecordLine line in phrLines)
            {
                if (line.SRAnswerType == "DTM")
                {
                    editor.ReplaceText("[DATETIME2]", DateTime.Now.ToString("dd-MM-yyyy HH:mm"), false, false);
                    editor.ReplaceText("[DATETIME3]", Convert.ToDateTime(line.QuestionAnswerText).ToString("dd-MM-yyyy HH:mm"), false, false);
                }

            }

        }

        private void AddHeader(RadFlowDocument document, RadFlowDocumentEditor editor)
        {
            // Add Header Section
            document.Sections.AddSection();
            var defaultHeader = document.Sections.First().Headers.Add();

            var defaultHeaderParagraph = defaultHeader.Blocks.AddParagraph();
            editor.MoveToParagraphStart(defaultHeaderParagraph);

            // Create Table
            var tbl = editor.InsertTable(1, 2);
            tbl.Rows[0].Cells[0].PreferredWidth = new TableWidthUnit(80);

            // Create paragraph with image
            var paragraphWithImage = tbl.Rows[0].Cells[0].Blocks.AddParagraph();
            editor.MoveToParagraphStart(paragraphWithImage);
            using (FileStream fs = new FileStream(Server.MapPath("~/Images/LogoRSMM.png"), FileMode.Open))
            {
                editor.InsertImageInline(fs, "png", new Size(60, 60));
            }

            // Hospital Info
            var cellParagraph = tbl.Rows[0].Cells[1].Blocks.AddParagraph();
            cellParagraph.Spacing.SpacingAfter = 0;
            editor.MoveToParagraphStart(cellParagraph);

            var healthcare = new Healthcare();
            healthcare.LoadByPrimaryKey(AppSession.Parameter.HealthcareID);

            var run = editor.InsertText(healthcare.HealthcareName);
            run.FontWeight = FontWeights.Bold;
            run.FontSize = 18;

            editor.InsertParagraph().Spacing.SpacingAfter = 0;
            run = editor.InsertText(healthcare.AddressLine1);
            run.FontSize = 10;


            if (healthcare.AddressLine2 != string.Empty)
            {
                editor.InsertParagraph().Spacing.SpacingAfter = 0;
                run = editor.InsertText(healthcare.AddressLine2);
                run.FontSize = 10;
            }

            if (healthcare.PhoneNo != string.Empty)
            {
                editor.InsertParagraph().Spacing.SpacingAfter = 0;
                run = editor.InsertText("Phone: " + healthcare.PhoneNo);
                run.FontSize = 10;
            }

            if (healthcare.FaxNo != string.Empty)
            {
                editor.InsertParagraph().Spacing.SpacingAfter = 0;
                run = editor.InsertText("Fax: " + healthcare.FaxNo);
                run.FontSize = 10;
            }

            editor.MoveToTableEnd(tbl);

            // Line
            editor.InsertParagraph().Borders = new ParagraphBorders(
                new Border(BorderStyle.None),
                new Border(BorderStyle.None),
                new Border(BorderStyle.None),
                new Border(2, BorderStyle.Single, new ThemableColor(Colors.Black))
            );
        }

        private static List<Paragraph> FindParagraphContainingText(RadFlowDocument document, string searchedText)
        {
            var retVal = new List<Paragraph>();
            foreach (var paragraph in document.EnumerateChildrenOfType<Paragraph>())
            {
                var paragraphText = new StringBuilder();

                foreach (var inline in paragraph.Inlines)
                {
                    var run = inline as Run;
                    if (run != null)
                    {
                        paragraphText.Append(run.Text);
                    }
                }

                string text = paragraphText.ToString();
                if (text.Contains(searchedText))
                {
                    retVal.Add(paragraph);
                }
            }

            return retVal;
        }

        private static void AddSignature(RadFlowDocument document, RadFlowDocumentEditor editor, MemoryStream ms, string signatureID, string signatureBy)
        {
            var findField = string.Format("[{0}]", signatureID);
            var signParagraphs = FindParagraphContainingText(document, findField);

            if (signParagraphs.Count == 0) return;

            foreach (var signParagraph in signParagraphs)
            {
                editor.MoveToParagraphStart(signParagraph);

                // Create Table
                var signatureTable = editor.InsertTable(2, 1);
                signatureTable.Rows[0].Cells[0].PreferredWidth = new TableWidthUnit(240);

                // Create paragraph for image
                var paragraphWithImage = signatureTable.Rows[0].Cells[0].Blocks.AddParagraph();
                paragraphWithImage.Spacing.SpacingAfter = 0;

                editor.MoveToParagraphStart(paragraphWithImage);

                editor.InsertImageInline(ms, "png", new Size(166, 92));

                // Create cell for Signature By name 
                var cellParagraph = signatureTable.Rows[1].Cells[0].Blocks.AddParagraph();
                cellParagraph.Spacing.SpacingAfter = 0;
                editor.MoveToParagraphStart(cellParagraph);
                editor.InsertText(signatureBy);
            }
        }

        private bool SaveSignature()
        {
            var phrLine = new PatientHealthRecordLine();
            phrLine.Query.es.Top = 1;
            phrLine.Query.Where(phrLine.Query.TransactionNo == TransactionNo,
                phrLine.Query.RegistrationNo == RegistrationNo, phrLine.Query.QuestionFormID == QuestionFormID,
                phrLine.Query.QuestionID == "SIGN.PAT");

            if (phrLine.Query.Load())
            {
                var imgHelper = new ImageHelper();
                var resized = imgHelper.ResizeImage(imgHelper.ToImage(hdnImage.Value), new System.Drawing.Size(332, 185));
                phrLine.BodyImage = imgHelper.ToByteArray(resized, ImageFormat.Png);
                phrLine.Save();
                return true;
            }

            return false;
        }

        private string SaveToGuarantorDoc()
        {
            var regType = string.Empty;
            var guarantorID = string.Empty;

            var qf = new QuestionForm();
            qf.LoadByPrimaryKey(QuestionFormID);
            var fileName = string.Format("{0}_{1}.pdf", QuestionFormID.Replace('.', '_'), TransactionNo.Replace('.', '_').Replace('/', '_'));

            var regNo = AppSession.PrintJobParameters.FindByParameterName("p_RegistrationNo").ValueString;
            var filePath = Reports.ReportViewer.GuarantorDocumentFilePath(regNo, qf.ReportProgramID, fileName, string.Empty, ref regType, ref guarantorID, string.Empty);

            try
            {
                var path = Path.GetDirectoryName(filePath);

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                // Save File
                var reg = new Registration();
                reg.LoadByPrimaryKey(regNo);
                var renderedBytes = LoadToPdf(reg, true);

                if (File.Exists(filePath))
                    File.Delete(filePath);

                File.WriteAllBytes(filePath, renderedBytes);

                return string.Format("File has save to {0}", filePath);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return string.Format("File has save to {0} has failed", filePath);

        }

    }
}