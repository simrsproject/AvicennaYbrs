using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.Threading;
using System.Globalization;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Auth.OAuth2.Flows;

namespace Temiang.Avicenna.Module.HR.EmployeeHR
{
    public partial class PersonalInfoGoogleFormImport : BasePageDialog
    {
        AppStandardReferenceItemCollection _stdicol = null;
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            Title = "Applicant Personal Information - Import From Google Form";
            ProgramID = AppConstant.Program.ApplicantPersonalInfo;
            btnFixFieldMapping.Visible = false;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            _stdicol = new AppStandardReferenceItemCollection();
            _stdicol.Query.Where(_stdicol.Query.StandardReferenceID == "EmpRecGoogleSpreadField");
            _stdicol.Query.OrderBy(_stdicol.Query.LineNumber.Ascending);
            _stdicol.LoadAll();

            if (grdGoogleForm.Columns.Count > 2)
            {
                for (int i = grdGoogleForm.Columns.Count - 1; i >= 2; i--)
                {
                    grdGoogleForm.Columns.RemoveAt(i);
                }
            }

            foreach (var item in _stdicol)
            {
                if (item.LineNumber == 0) continue;
                var itemNoteLower = item.Note.ToLower();
                if (itemNoteLower.Contains("date"))
                {
                    if (itemNoteLower.Contains("-show"))
                    {
                        var gc = new GridDateTimeColumn();
                        gc.DataField = item.ItemID;
                        gc.HeaderText = item.ItemName;
                        grdGoogleForm.Columns.Add(gc);
                    }
                }
                else
                {
                    if (itemNoteLower.Contains("-show"))
                    {
                        var gc = new GridBoundColumn();
                        gc.DataField = item.ItemID;
                        gc.HeaderText = item.ItemName;
                        grdGoogleForm.Columns.Add(gc);
                    }
                }
            }
            grdGoogleForm.InitializeCultureGrid();
        }
        #region Google Form
        private string _googleEmpRecruitmentUser = AppParameter.GetParameterValue(AppParameter.ParameterItem.GoogleEmpRecruitmentUser);
        private string _googleEmpRecruitmentAppName = AppParameter.GetParameterValue(AppParameter.ParameterItem.GoogleEmpRecruitmentAppName);
        private string _googleEmpRecruitmentSpreadsheetId = AppParameter.GetParameterValue(AppParameter.ParameterItem.GoogleEmpRecruitmentSpreadsheetID);
        private string _googleClientID = AppParameter.GetParameterValue(AppParameter.ParameterItem.GoogleClientID);
        private string _googleProjectID = AppParameter.GetParameterValue(AppParameter.ParameterItem.GoogleProjectID);
        private string _googleClientSecret = AppParameter.GetParameterValue(AppParameter.ParameterItem.GoogleClientSecret);

        protected void grdGoogleForm_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var grd = (RadGrid)sender;
            if (!IsPostBack)
            {
                grd.DataSource = string.Empty;
                return;
            }

            grd.DataSource = DownloadGoogleSheetRow();
        }

        private DataTable DownloadGoogleSheetRow()
        {
            if (Session["gs"] != null) return (DataTable)Session["gs"];

            var service = GoogleSheetService();

            // Define request parameters.
            //var range = "Form Responses 1!A2:M";
            var range = "Form Responses 1"; // All range
            var request = service.Spreadsheets.Values.Get(_googleEmpRecruitmentSpreadsheetId, range);

            ValueRange response = request.Execute();
            IList<IList<Object>> values = response.Values;

            // Check spreadsheet column
            btnFixFieldMapping.Visible = false;
            litMsg.Text = String.Empty;
            if (values != null && values.Count > 1)
            {
                var colums = values[0]; // Header row
                var colCount = colums.Count;
                var msg = string.Empty;
                foreach (var item in _stdicol)
                {
                    if (item.LineNumber == 0) continue;
                    if (item.LineNumber > colCount)
                        msg = string.Concat(msg, "Line number: ", item.ItemName, " [", item.LineNumber, "] > Google Spread col count [", colCount, "], ");
                    else if (!item.ItemName.ToLower().Equals(colums[item.LineNumber ?? 0].ToString().ToLower()))
                        msg = string.Concat(msg, item.ItemName, " != ", colums[item.LineNumber ?? 0].ToString(), ", ");
                }

                if (!string.IsNullOrEmpty(msg))
                {
                    litMsg.Text = string.Concat("Field mapping problem :", msg);
                    btnFixFieldMapping.Visible = AppSession.UserLogin.UserID.Equals("han"); // Tidak untuk yg lain karena harus ada penyesuaian secara manual
                    return null;
                }
            }

            var dtb = new DataTable();
            dtb.Columns.Add("Timestamp", typeof(DateTime));
            foreach (var item in _stdicol)
            {
                if (item.LineNumber == 0) continue;
                if (item.ItemName.ToLower().Contains("date") || item.Note.ToLower().Contains("date"))
                {
                    dtb.Columns.Add(item.ItemID, typeof(DateTime));
                }
                else
                {
                    dtb.Columns.Add(item.ItemID, typeof(string));
                }
            }

            if (values != null && values.Count > 1)
            {
                foreach (var val in values)
                {
                    var ts = val[0].ToString();
                    if (ts.ToLower().Equals("timestamp")) continue; // Skip row header

                    var tsa = ts.Split('/');

                    var mf = "M";
                    if (tsa[0].Trim().Length == 2)
                        mf = "MM";

                    var df = "d";
                    if (tsa[1].Trim().Length == 2)
                        df = "dd";

                    var lf = "yyyy H:mm:ss";
                    if (tsa[2].Trim().Length == 13)
                        lf = "yyyy HH:mm:ss";

                    var timestamp = DateTime.ParseExact(ts, String.Format("{0}/{1}/{2}", mf, df, lf), CultureInfo.InvariantCulture);

                    if (!txtDate.IsEmpty)
                        if (timestamp.Date != txtDate.SelectedDate)
                            continue;

                    if (!string.IsNullOrEmpty(txtName.Text))
                        if (!val[1].ToString().ToLower().Contains(txtName.Text.ToLower()))
                            continue;

                    // Registered check
                    var rgf = new PersonalInfoGoogleForm();
                    if (rgf.LoadByPrimaryKey(timestamp))
                        continue;

                    var acount = val.Count;
                    var newRow = dtb.NewRow();
                    newRow["Timestamp"] = timestamp;

                    foreach (var item in _stdicol)
                    {
                        if (item.LineNumber == 0) continue;
                        if (acount > item.LineNumber.ToInt())
                        {
                            if (item.ItemName.ToLower().Contains("date") || item.Note.ToLower().Contains("date"))
                            {
                                var valDate = val[item.LineNumber.ToInt()].ToString();
                                if (!string.IsNullOrWhiteSpace(valDate))
                                {
                                    var valDates = valDate.Split('/');

                                    mf = "M";
                                    if (valDates[0].Trim().Length == 2)
                                        mf = "MM";

                                    df = "d";
                                    if (valDates[1].Trim().Length == 2)
                                        df = "dd";

                                    newRow[item.ItemID] = DateTime.ParseExact(valDate, String.Format("{0}/{1}/yyyy", mf, df), CultureInfo.InvariantCulture);
                                }
                            }
                            else
                                newRow[item.ItemID] = val[item.LineNumber.ToInt()];
                        }
                    }
                    dtb.Rows.Add(newRow);
                }

            }
            else
            {
                //Console.WriteLine("No data found.");
            }

            //Ad key
            dtb.PrimaryKey = new DataColumn[] { dtb.Columns["Timestamp"] };

            Session["gs"] = dtb;

            return dtb;
        }

        private SheetsService GoogleSheetService()
        {
            var oAuthClient = string.Concat("{\"installed\":{\"client_id\":\"", _googleClientID, "\",\"project_id\":\"", _googleProjectID, "\",\"auth_uri\":\"https://accounts.google.com/o/oauth2/auth\",\"token_uri\":\"https://oauth2.googleapis.com/token\",\"auth_provider_x509_cert_url\":\"https://www.googleapis.com/oauth2/v1/certs\",\"client_secret\":\"", _googleClientSecret, "\",\"redirect_uris\":[\"http://localhost\"]}}");
            byte[] byteArray = Encoding.UTF8.GetBytes(oAuthClient);
            var stream = new MemoryStream(byteArray);

            // The file token.json stores the user's access and refresh tokens, and is created
            // automatically when the authorization flow completes for the first time.
            string credPath = Server.MapPath("~/Token");
            var credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                GoogleClientSecrets.Load(stream).Secrets,
                new[] { SheetsService.Scope.SpreadsheetsReadonly },
                _googleEmpRecruitmentUser,
                CancellationToken.None,
                new FileDataStore(credPath, true)).Result;

            // Create Google Sheets API service.
            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = _googleEmpRecruitmentAppName,
            });
            return service;
        }
        private string GoogleSheetRange(SheetsService service, string sheetId)
        {
            // Define request parameters.  
            String spreadsheetId = sheetId;
            String range = "A:A";

            SpreadsheetsResource.ValuesResource.GetRequest getRequest =
                       service.Spreadsheets.Values.Get(spreadsheetId, range);
            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
            ValueRange getResponse = getRequest.Execute();
            IList<IList<Object>> getValues = getResponse.Values;
            if (getValues == null)
            {
                // spreadsheet is empty return Row A Column A  
                return "A:A";
            }

            int currentCount = getValues.Count() + 1;
            String newRange = "A" + currentCount + ":A";
            return newRange;
        }
        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            Session["gs"] = null;
            grdGoogleForm.Rebind();
        }

        #endregion

        public override bool OnButtonOkClicked()
        {
            var dtb = (DataTable)Session["gs"];
            foreach (GridDataItem item in grdGoogleForm.MasterTableView.Items)
            {
                var chkIsSelected = ((CheckBox)item.FindControl("chkIsSelected"));
                if (chkIsSelected.Checked)
                {
                    var row = dtb.Rows.Find(item.GetDataKeyValue("Timestamp"));
                    Import(row);
                }
            }
            return true;
        }

        private void Import(DataRow gfDataRow)
        {
            using (var tr = new esTransactionScope())
            {
                // 1. PersonalInfo
                var pInfo = (PersonalInfo)SetEntity(gfDataRow, 0, "PersonalInfo");

                // New PersonID
                var piMax = new PersonalInfo();
                piMax.Query.es.Top = 1;
                piMax.Query.OrderBy(piMax.Query.PersonID.Descending);
                if (piMax.Query.Load())
                    pInfo.PersonID = (piMax.PersonID ?? 0) + 1;
                else
                    pInfo.PersonID = 1;

                // EmployeeNumber
                var autoNumber = new AppAutoNumberLast();
                autoNumber = Helper.GetNewAutoNumber(DateTime.Now.Date, AppEnum.AutoNumber.ApplicantNo);
                pInfo.EmployeeNumber = autoNumber.LastCompleteNumber;
                autoNumber.Save();

                pInfo.Save();

                // EmployeeEmploymentPeriod
                var eep = new EmployeeEmploymentPeriod();
                eep.PersonID = pInfo.PersonID;
                eep.SREmploymentType = "0";
                eep.SREmploymentCategory = "";
                eep.ValidFrom = DateTime.Now.Date;
                eep.ValidTo = DateTime.Now.Date.AddYears(10);
                eep.Note = string.Empty;
                eep.RecruitmentPlanID = -1;
                eep.EmployeeNumber = pInfo.EmployeeNumber;
                eep.Save();

                // EmployeeWorkingInfo
                string empType = string.Empty;
                string empStatus = string.Empty;
                var ewi = new EmployeeWorkingInfo();
                ewi.PersonID = pInfo.PersonID;
                ewi.JoinDate = DateTime.Now.Date;
                ewi.SupervisorId = -1;
                ewi.SREmployeeStatus = string.Empty;
                ewi.SREmployeeType = string.Empty;
                ewi.PositionGradeID = -1;
                ewi.SRRemunerationType = string.Empty;
                ewi.AbsenceCardNo = string.Empty;
                ewi.Note = string.Empty;
                ewi.IsKWI = false;
                ewi.GradeYear = 0;
                ewi.SREducationLevel = string.Empty;
                ewi.str.ResignDate = string.Empty;
                ewi.SRResignReason = string.Empty;
                ewi.Save();

                // 2. PersonalAddress
                SetEntity(gfDataRow, pInfo.PersonID, "PersonalAddress").Save();

                // 3. PersonalIdentification
                SetEntity(gfDataRow, pInfo.PersonID, "PersonalIdentification").Save();

                // 4. PersonalFamily
                SetEntity(gfDataRow, pInfo.PersonID, "PersonalFamily").Save();

                // 5.1 PersonalLicence STR
                var pLic01 = SetEntity(gfDataRow, pInfo.PersonID, "PersonalLicence", "-STR");
                pLic01.SetColumn("SRLicenceType", "01");
                pLic01.Save();

                // 5.2 PersonalLicence SIP
                var pLic02 = SetEntity(gfDataRow, pInfo.PersonID, "PersonalLicence", "-SIP");
                pLic02.SetColumn("SRLicenceType", "02");
                pLic02.Save();

                // 5.3 PersonalLicence SIM
                var pLic03 = SetEntity(gfDataRow, pInfo.PersonID, "PersonalLicence", "-SIM");
                pLic03.SetColumn("SRLicenceType", "03");
                pLic03.Save();

                //06. PersonalContact
                //01	Telephone
                //02	Mobile Phone
                //03	Email
                // 6.1 PersonalContact Telephone
                var pContact01 = SetEntity(gfDataRow, pInfo.PersonID, "PersonalContact", "-Telephone");
                pContact01.SetColumn("SRContactType", "01");
                pContact01.Save();

                // 6.2 PersonalContact Mobile Phone
                var pContact02 = SetEntity(gfDataRow, pInfo.PersonID, "PersonalContact", "-Mobile Phone");
                pContact02.SetColumn("SRContactType", "02");
                pContact02.Save();

                // 6.3 PersonalContact Email
                var pContact03 = SetEntity(gfDataRow, pInfo.PersonID, "PersonalContact", "-Mobile Phone");
                pContact03.SetColumn("SRContactType", "03");
                pContact03.Save();


                // 7 PersonalWorkExperience
                SetEntity(gfDataRow, pInfo.PersonID, "PersonalWorkExperience").Save();


                // 8. PersonalEducationHistory
                SetEntity(gfDataRow, pInfo.PersonID, "PersonalEducationHistory").Save();


                var gf = new PersonalInfoGoogleForm();
                gf.Timestamp = (DateTime)gfDataRow["TimeStamp"];
                gf.PersonID = pInfo.PersonID;
                gf.Save();

                tr.Complete();
            }
        }

        private esEntityWAuditLog SetEntity(DataRow gfDataRow, int? personID, string toEntityName, string group = "")
        {
            var ent = Temiang.Avicenna.BusinessObject.Common.Utils.GetEntity(toEntityName);
            ent.SetColumn("PersonID", personID);
            var entityNameSearch = string.Format("{0}-", toEntityName);
            foreach (var item in _stdicol)
            {
                if (!item.Note.Contains(entityNameSearch)) continue;
                if (!string.IsNullOrEmpty(group) && !item.Note.Contains(group)) continue;

                object gfValue = null;
                if (item.LineNumber == 0)
                {
                    // Tidak ada di GF dan ada default valuenya
                    if (!string.IsNullOrWhiteSpace(item.ReferenceID))
                        gfValue = item.ReferenceID.Substring(3); // ex. DV:1-1-1900
                    else
                        continue; //skip
                }
                else
                    gfValue = gfDataRow[item.ItemID].ToString();

                var fieldName = item.Note.Split('-')[1];
                if (fieldName.ToLower().Substring(0, 2) == "sr")
                {
                    var itemID = StdRefItemID(fieldName, item.ReferenceID, gfValue.ToString());
                    if (!string.IsNullOrWhiteSpace(itemID))
                        ent.SetColumn(fieldName, itemID);
                }
                else
                {
                    if (!(string.IsNullOrEmpty(gfValue.ToString()) && (item.ItemName.ToLower().Contains("date") || item.Note.ToLower().Contains("date"))))
                        ent.SetColumn(fieldName, gfValue);
                }

            }
            return ent;
        }

        private string StdRefItemID(string fieldName, string refID, string itemName)
        {
            var stdi = new AppStandardReferenceItem();
            var stdRefID = fieldName.Substring(2);

            // Override dengan yg di AppStandardReferenceItem.ReferenceID
            if (!string.IsNullOrWhiteSpace(refID))
                stdRefID = refID.Substring(3); // ex. SR:LineBusiness

            stdi.Query.Where(stdi.Query.StandardReferenceID == stdRefID, stdi.Query.ItemName == itemName);
            if (stdi.Query.Load())
                return stdi.ItemID;
            return string.Empty;
        }

        protected void btnFixFieldMapping_Click(object sender, EventArgs e)
        {
            var service = GoogleSheetService();

            // Define request parameters.
            //var range = "Form Responses 1!A2:M";
            var range = "Form Responses 1"; // All range
            var request = service.Spreadsheets.Values.Get(_googleEmpRecruitmentSpreadsheetId, range);

            ValueRange response = request.Execute();
            IList<IList<Object>> values = response.Values;

            // Check spreadsheet column
            if (values != null && values.Count > 1)
            {
                var colums = values[0]; // Header row
                var colCount = colums.Count;
                for (int i = 1; i < colCount; i++)
                {
                    var stdi = _stdicol.FirstOrDefault(item => item.ItemName.ToLower().Equals(colums[i].ToString().ToLower()));
                    if (stdi != null)
                    {
                        if (stdi.LineNumber != i)
                            stdi.LineNumber = i;
                    }
                    else
                    {
                        var lastItemID = _stdicol.Max(item => item.ItemID);

                        stdi = _stdicol.AddNew();
                        stdi.ItemID = String.Format("ERGSF-{0:000}", lastItemID.Substring(6).ToInt() + 1); //ERGSF-017
                        stdi.StandardReferenceID = "EmpRecGoogleSpreadField";
                        stdi.ItemName = colums[i].ToString();
                        stdi.IsUsedBySystem = true;
                        stdi.IsActive = true;
                        stdi.ReferenceID = String.Empty;
                        stdi.Note = String.Empty;
                        stdi.LineNumber = i;
                    }
                }
                _stdicol.Save();
            }

            Session["gs"] = null;
            grdGoogleForm.Rebind();
        }
    }
}
