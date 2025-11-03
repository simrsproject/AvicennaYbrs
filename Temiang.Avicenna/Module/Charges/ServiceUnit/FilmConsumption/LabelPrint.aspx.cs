using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class LabelPrint : BasePageDialog
    {
        private AppAutoNumberLast _amplopFilmAutoNumber;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var tc = new TransCharges();
                tc.LoadByPrimaryKey(Request.QueryString["tno"]);

                var r = new Registration();
                r.LoadByPrimaryKey(tc.RegistrationNo);

                var p = new Patient();
                p.LoadByPrimaryKey(r.PatientID);

                txtTransactionNo.Text = tc.TransactionNo;
                txtTransactionDate.SelectedDate = tc.TransactionDate;
                txtMedicalNo.Text = p.MedicalNo;
                txtPatientName.Text = (p.FirstName + " " + p.MiddleName + " " + p.LastName).Trim();

                var notes = string.Empty;

                //switch (Request.QueryString["init"].Trim())
                switch(AppSession.Parameter.HealthcareInitialAppsVersion.ToLower())
                {
                    case "rsch":
                        trFilmNo.Visible = Request.QueryString["type"] == "3";
                        lblExamination.Text = Request.QueryString["type"] == "1" ? "Film No" : "Physician Examiner";
                        
                        notes = tc.Notes;

                        pnlEnvelopeSize.Visible = (Request.QueryString["type"] == "1" && AppSession.Parameter.HealthcareInitialAppsVersion == "RSCH");
                        if (Request.QueryString["type"] == "1")
                            txtExamination.Text = notes;
                        else
                            txtFilmNo.Text = notes;
                        
                        break;
                    
                    default: //other
                        lblExamination.Text = "Examination";

                        var tciq = new TransChargesItemQuery("a");
                        var iq = new ItemQuery("b");
                        tciq.Select(iq.ItemName);
                        tciq.InnerJoin(iq).On(tciq.ItemID == iq.ItemID);
                        tciq.Where(tciq.TransactionNo == Request.QueryString["tno"],
                                   iq.SRItemType.In(ItemType.Service, ItemType.Radiology, ItemType.Laboratory));
                        tciq.OrderBy(tciq.SequenceNo.Ascending);
                        DataTable dtb = tciq.LoadDataTable();
                        foreach (DataRow row in dtb.Rows)
                        {
                            if (notes == string.Empty)
                                notes = row["ItemName"].ToString();
                            else
                                notes += ", " + row["ItemName"];
                        }
                        pnlEnvelopeSize.Visible = false;
                        txtExamination.Text = notes;
                        break;
                }

                
            }
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument.print = '" + Page.Request.QueryString["tno"] + "|" + AppConstant.Report.JobOrderLabel +
                   "'";
        }

        public override bool OnButtonOkClicked()
        {
            if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSCH")
            //if (Request.QueryString["init"].Trim() == "rsch")
            {
                var tc = new TransCharges();
                tc.LoadByPrimaryKey(Request.QueryString["tno"]);
                tc.Notes = Request.QueryString["type"] == "3" ? txtFilmNo.Text : txtExamination.Text;
                tc.LastUpdateByUserID = AppSession.UserLogin.UserID;
                tc.LastUpdateDateTime = DateTime.Now;
                tc.Save();

                if (Request.QueryString["type"] == "2")
                {
                    var tciColl = new TransChargesItemCollection();
                    tciColl.Query.Where(tciColl.Query.TransactionNo == Request.QueryString["tno"]);
                    tciColl.LoadAll();
                    foreach (var transChargesItem in tciColl)
                    {
                        if (transChargesItem.IsPackage == true)
                        {
                            var detailQ = new TransChargesItemQuery("a");
                            var headerQ = new TransChargesQuery("b");
                            var itemQ = new ItemQuery("c");
                            detailQ.Select(detailQ.TransactionNo, detailQ.SequenceNo, itemQ.Notes);
                            detailQ.InnerJoin(headerQ).On(detailQ.TransactionNo == headerQ.TransactionNo);
                            detailQ.InnerJoin(itemQ).On(detailQ.ItemID == itemQ.ItemID && itemQ.Notes.Length() > 0);
                            detailQ.Where(headerQ.PackageReferenceNo == transChargesItem.TransactionNo,
                                          detailQ.IsPackage == false);
                            DataTable detailDt = detailQ.LoadDataTable();
                            foreach (DataRow row in detailDt.Rows)
                            {
                                var tci = new TransChargesItem();
                                if (tci.LoadByPrimaryKey(row["TransactionNo"].ToString(), row["SequenceNo"].ToString()))
                                {
                                    if (string.IsNullOrEmpty(tci.FilmNo))
                                    {
                                        _amplopFilmAutoNumber = Helper.GetNewAutoNumber(tc.TransactionDate.Value.Date,
                                                 AppEnum.AutoNumber.AmplopFilmNo,
                                                 row["Notes"].ToString().Length >= 3 ? row["Notes"].ToString().Substring(0, 3).ToUpper() : row["Notes"].ToString().ToUpper(),
                                                 AppSession.UserLogin.UserID);

                                        var filmNo = _amplopFilmAutoNumber.LastCompleteNumber;
                                        _amplopFilmAutoNumber.Save();

                                        tci.FilmNo = filmNo;
                                        tci.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                        tci.LastUpdateDateTime = DateTime.Now;
                                        tci.Save();
                                    }
                                }
                            }
                        }
                        else if (string.IsNullOrEmpty(transChargesItem.FilmNo))
                        {
                            var item = new Item();
                            item.LoadByPrimaryKey(transChargesItem.ItemID);
                            if (item.Notes.Length > 0 && item.SRItemType != ItemType.Medical && item.SRItemType != ItemType.NonMedical && item.SRItemType != ItemType.Kitchen)
                            {
                                _amplopFilmAutoNumber = Helper.GetNewAutoNumber(tc.TransactionDate.Value.Date,
                                                 AppEnum.AutoNumber.AmplopFilmNo,
                                                 item.Notes.Length >= 3 ? item.Notes.Substring(0, 3).ToUpper() : item.Notes.ToUpper(),
                                                 AppSession.UserLogin.UserID);

                                var filmNo = _amplopFilmAutoNumber.LastCompleteNumber;
                                _amplopFilmAutoNumber.Save();

                                transChargesItem.FilmNo = filmNo;
                            }
                        }
                    }
                    tciColl.Save();
                }
            }
            else {
                var tciColl = new TransChargesItemCollection();
                tciColl.Query.Where(tciColl.Query.TransactionNo == Request.QueryString["tno"]);
                tciColl.LoadAll();
                foreach (var tci in tciColl)
                {
                    if (string.IsNullOrEmpty(tci.FilmNo))
                    {
                        var item = new Item();
                        item.LoadByPrimaryKey(tci.ItemID);
                        if (item.IsHasTestResults ?? false) {
                            if (trFilmNo.Visible)
                            {
                                tci.FilmNo = txtFilmNo.Text;
                            }
                            else
                            {
                                tci.FilmNo = txtExamination.Text;
                            }
                        }
                    }
                }
                tciColl.Save();
            }

            AppSession.PrintShowToolBarPrint = false;
            var jobParameters = new PrintJobParameterCollection();

            var pTransNo = jobParameters.AddNew();
            pTransNo.Name = "p_TransactionNo";
            pTransNo.ValueString = Request.QueryString["tno"];

            var pNotes = jobParameters.AddNew();
            pNotes.Name = "p_Notes";
            pNotes.ValueString = txtExamination.Text;

            AppSession.PrintJobParameters = jobParameters;

            if (Request.QueryString["init"] == "rsch")
            {
                switch (Request.QueryString["type"])
                {
                    case "1":
                        AppSession.PrintJobReportID = rbtEnvelopeSize.SelectedValue == "0" ? AppConstant.Report.JobOrderLabel : AppConstant.Report.JobOrderLabelLarge;
                        break;

                    case "2":
                        AppSession.PrintJobReportID = AppConstant.Report.JobOrderLabelDiagnostic;
                        break;

                    case "3":
                        AppSession.PrintJobReportID = AppConstant.Report.JobOrderLabelRadiologi;
                        break;
                }
            }
            else
                AppSession.PrintJobReportID = AppConstant.Report.JobOrderLabel;
            
            return true;
        }
    }
}
