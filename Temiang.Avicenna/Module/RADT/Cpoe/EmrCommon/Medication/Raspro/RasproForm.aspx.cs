using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.DynamicQuery;
using System.Configuration;
using System.Net;
using System.IO;

namespace Temiang.Avicenna.Module.RADT.Emr
{
    /// <summary>
    /// Entry Form RASAL dan RASLAN
    /// </summary>
    /// <remarks>
    /// Untuk entry form raspro yg line questionnya dari table Raspro & RasproLine
    /// Saat ini untuk entry Form RASAL dan RASLAN
    /// 
    /// Start Created by : Handono (08128362806)
    /// Project : RSBK (2020)
    /// Modification History:
    /// 
    /// </remarks>
    public partial class RasproForm : BasePageDialog
    {
        private string RasproID => Request.QueryString["raspro"];
        private int RasproSeqNo => Request.QueryString["seqno"].ToInt();
        private string Mode => Request.QueryString["mod"];

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ElectronicHealthRecord;

            if (!IsPostBack)
            {
                RegistrationRaspro rr = null;

                //Load Session 
                //Form ini dipanggil ketika klik previose atau ketika pertama panggil  form RASAL / RASLAN
                if (RasproSeqNo > 0)
                {
                    if (Session["rr"] == null)
                    {
                        LoadDataSession();
                        rr = (RegistrationRaspro)Session["rr"];
                    }
                    else
                    {
                        // Jika ada maka cek key nya dulu
                        rr = (RegistrationRaspro)Session["rr"];
                        if (rr.SeqNo != null && (rr.RegistrationNo != RegistrationNo || rr.SeqNo != RasproSeqNo))
                        {
                            LoadDataSession();
                            rr = (RegistrationRaspro)Session["rr"];
                        }
                    }
                }

                // Initialize create new
                if ("new".Equals(Mode))
                {
                    rr = new RegistrationRaspro
                    {
                        SRRaspro = RasproID,
                        RegistrationNo = RegistrationNo,
                        ParamedicID = ParamedicTeam.DPJP(RegistrationNo).ParamedicID,
                        RasproDateTime = DateTime.Now
                    };

                    // TODO: Betulkan ServiceUnit saat isi form Raspro 
                    var reg = new Registration();
                    reg.LoadByPrimaryKey(RegistrationNo);
                    rr.ServiceUnitID = reg.ServiceUnitID;

                    // Ambil work diagnose
                    rr.Diagnose = RegistrationInfoMedicDiagnose.DiagnoseSummary(RegistrationNo);

                    Session["rr"] = rr;

                    var rrlcoll = new RegistrationRasproLineCollection();
                    Session["rrlcoll"] = rrlcoll;
                }

                if (!string.IsNullOrWhiteSpace(rr.AdviseByParamedicID))
                    ComboBox.PopulateWithOneParamedic(cboAdviseByParamedicID, rr.AdviseByParamedicID);

                PopulateAbRestrictionSelection();

                if (!string.IsNullOrWhiteSpace(rr.AbRestrictionID))
                    cboAbRestrictionID.SelectedValue = rr.AbRestrictionID;
                else if (!rr.es.IsAdded)
                    cboAbRestrictionID.SelectedValue = "NO";

                txtOtherInfection.Text = rr.OtherInfection;
                txtDiagnose.Text = rr.Diagnose;

                // First Line
                var raspro = new Raspro();
                raspro.Query.Where(raspro.Query.SRRaspro == rr.SRRaspro);
                raspro.Query.OrderBy(raspro.Query.SeqNo.Ascending);
                raspro.Query.es.Top = 1;
                raspro.Query.Load();
                lblSpecification.Text = raspro.Spesification;

                hdnRasproLineID.Value = raspro.RasproLineID;
                hdnRasproLineSeqNo.Value = raspro.SeqNo.ToString();
            }
        }


        private void LoadDataSession()
        {
            var rr = new RegistrationRaspro();
            rr.LoadByPrimaryKey(RegistrationNo, RasproSeqNo);
            Session["rr"] = rr;

            var rrlcoll = new RegistrationRasproLineCollection();
            rrlcoll.Query.Where(rrlcoll.Query.RegistrationNo == RegistrationNo, rrlcoll.Query.SeqNo == RasproSeqNo);
            rrlcoll.Query.Load();
            Session["rrlcoll"] = rrlcoll;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Footer.Visible = false;
        }


        protected void btnNext_Click(object sender, EventArgs e)
        {
            ApplyValueAndGoToNextQuestion();
        }

        public static string PreviouseSpecificationHtml(RegistrationRaspro rr, RegistrationRasproLineCollection rrlcoll, int prevRasproLineSeqNo)
        {
            var strb = new StringBuilder();

            var colls = new RasproCollection();
            colls.Query.Where(colls.Query.SRRaspro == rr.SRRaspro, colls.Query.SeqNo <= prevRasproLineSeqNo);
            colls.Query.OrderBy(colls.Query.SeqNo.Ascending);
            colls.Query.Load();

            strb.Append("<table>");
            var no = 1;
            foreach (Raspro raspro in colls)
            {
                var yesOrNo = rrlcoll[no - 1].Condition == "0" ? "Tidak" : "Ya";
                strb.AppendFormat("<tr><td style=\"width:10px\" valign=\"top\">{0}.</td><td>{1}</td><td style=\"width:100px\">: {2}</td></tr>", no, raspro.Spesification, yesOrNo);
                no++;
            }
            strb.Append("</table>");

            return strb.ToString();
        }

        private void ApplyValueAndGoToNextQuestion()
        {
            if (!Page.IsValid) return;
            // Header
            var rr = (RegistrationRaspro)Session["rr"];
            if (rr.RasproDateTime == null)
                rr.RasproDateTime = DateTime.Now;

            if (!string.IsNullOrWhiteSpace(cboAdviseByParamedicID.SelectedValue))
                rr.AdviseByParamedicID = cboAdviseByParamedicID.SelectedValue;
            else
                rr.str.AdviseByParamedicID = string.Empty;

            if ("NONE".Equals(cboAbRestrictionID.SelectedValue))
                rr.str.AbRestrictionID = string.Empty; // Set NULL
            else
                rr.AbRestrictionID = cboAbRestrictionID.SelectedValue;

            rr.Diagnose = txtDiagnose.Text;
            rr.OtherInfection = txtOtherInfection.Text;

            Session["rr"] = rr;

            // Line Detail
            var rrlcoll = (RegistrationRasproLineCollection)Session["rrlcoll"];
            RegistrationRasproLine rrl = null;


            foreach (RegistrationRasproLine line in rrlcoll)
            {
                if (line.RasproLineID == hdnRasproLineID.Value)
                {
                    rrl = line;
                    break;
                }
            }

            if (rrl == null)
            {
                rrl = rrlcoll.AddNew();
                rrl.RegistrationNo = rr.RegistrationNo;
                rrl.SeqNo = rr.SeqNo;
                rrl.RasproLineID = hdnRasproLineID.Value;
            }

            var raspro = new Raspro();
            raspro.LoadByPrimaryKey(hdnRasproLineID.Value);

            var condition = "NO".Equals(cboAbRestrictionID.SelectedValue) ? "0" : "1";
            rrl.Condition = condition;

            Session["rrlcoll"] = rrlcoll;

            // Redirect
            var url = string.Format("RasproFormEnd.aspx?patid={0}&rlsno={1}&rlcon={3}&ccm=rebind&cet={2}", PatientID, hdnRasproLineSeqNo.Value, Request.QueryString["cet"], condition);

            if ((condition == "1" && raspro.YesAction == "NEXT") || (condition == "0" && raspro.NoAction == "NEXT"))
            {
                if (rr.AbRestrictionID != null)
                {
                    // Check apakah antibiotic dibatasi
                    var abr = new AbRestriction();
                    abr.LoadByPrimaryKey(rr.AbRestrictionID);
                    if (!(abr.IsNotRestricted ?? false))
                        url = string.Format("RasproFormLine.aspx?patid={0}&rlsno={1}&ccm=rebind&cet={2}", PatientID, hdnRasproLineSeqNo.Value, Request.QueryString["cet"]);
                }
            }

            Response.Redirect(url);
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            imgRfvOtherInfection.Visible = false;
            if (string.IsNullOrWhiteSpace(cboAbRestrictionID.SelectedValue))
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = "Please answer first question \"" + lblSpecification.Text + "\"";
                return;
            }

            if (cboAbRestrictionID.SelectedText.ToLower().Contains("non ppab") && string.IsNullOrWhiteSpace(txtOtherInfection.Text))
            {
                imgRfvOtherInfection.Visible = true;
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = string.Format("{0} must fill Description", cboAbRestrictionID.SelectedText);
                return;
            }

            // Check selected AB restriction
            if (!string.IsNullOrWhiteSpace(cboAbRestrictionID.SelectedValue))
            {
                var abr = new AbRestriction();
                if (abr.LoadByPrimaryKey(cboAbRestrictionID.SelectedValue))
                {
                    // Check have child
                    var abrChild = new AbRestriction();
                    abrChild.Query.Where(abrChild.Query.ParentID == abr.AbRestrictionID);
                    abrChild.Query.es.Top = 1;
                    if (abrChild.Query.Load())
                    {
                        args.IsValid = false;
                        ((CustomValidator)source).ErrorMessage = "Please select at detail selection from \"" + cboAbRestrictionID.SelectedText + "\"";
                        return;
                    }
                }
            }
        }
        protected void btnPpabPdf_Click(object sender, EventArgs e)
        {
            //var filePath = Server.MapPath("~\\App_Document\\PPRA\\PPAB.pdf");
            //Helper.DownloadFile(Response, filePath);

            var urlFile = string.Format("{0}/App_Document/PPRA/PPAB.pdf", System.Configuration.ConfigurationManager.AppSettings.Get("ReportUrlLocation"));
            try
            {
                //using (var client = new WebClient())
                //{
                //    client.DownloadFile(urlFile, "PPAB.pdf");
                //}

                using (WebClient client = new WebClient())
                {
                    using (Stream ms = new MemoryStream(client.DownloadData(urlFile)))
                    {
                        MemoryStream download = new MemoryStream();
                        download.SetLength(ms.Length);
                        ms.Read(download.GetBuffer(), 0, (int)ms.Length);

                        var response = Response;
                        response.ContentType = "application/octet-stream";
                        response.AddHeader("Content-Disposition", "attachment; filename=\"PPAB.pdf\"");

                        // Write the file to the Response  
                        const int bufferLength = 10000;
                        byte[] buffer = new Byte[bufferLength];
                        int length = 0;
                        try
                        {
                            do
                            {
                                if (response.IsClientConnected)
                                {
                                    length = download.Read(buffer, 0, bufferLength);
                                    response.OutputStream.Write(buffer, 0, length);
                                    buffer = new Byte[bufferLength];
                                }
                                else
                                {
                                    length = -1;
                                }
                            }
                            while (length > 0);
                            response.Flush();
                            response.End();
                        }
                        finally
                        {
                            if (download != null)
                                download.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //nothing
            }


        }

        private void PopulateAbRestrictionSelection()
        {
            var inf = new AbRestrictionQuery();

            inf.Select(inf.AbRestrictionID, inf.ParentID, inf.AbRestrictionName);
            inf.Where(inf.SRAbRestrictionType == "INF");
            inf.OrderBy(inf.AbRestrictionID.Ascending);
            var dtbInf = inf.LoadDataTable();

            //var newRow = dtbInf.NewRow();
            //newRow["AbRestrictionID"] = "NONE";
            //newRow["AbRestrictionName"] = "No Infection";
            //dtbInf.Rows.InsertAt(newRow, 0);

            cboAbRestrictionID.DataSource = dtbInf;
            cboAbRestrictionID.DataBind();
        }
    }
}