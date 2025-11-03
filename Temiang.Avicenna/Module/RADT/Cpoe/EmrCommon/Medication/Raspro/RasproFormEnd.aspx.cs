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
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Avicenna.Module.Charges;
using Temiang.Dal.DynamicQuery;
using System.Drawing.Imaging;


namespace Temiang.Avicenna.Module.RADT.Emr
{
    /// <summary>
    /// Lanjutan Entry Form RASAL dan RASLAN dipanggil dari RasproForm.aspx
    /// </summary>
    /// <remarks>
    /// Untuk entry form raspro yg line questionnya dari table Raspro & RasproLine
    /// Saat ini untuk entry Form RASAL dan RASLAN
    /// 
    /// Start Created by : Handono (08128362806)
    /// Project : RSI (2020)
    /// Modification History:
    /// 
    /// </remarks>
    public partial class RasproFormEnd : BasePageDialog
    {
        public string PatientID
        {
            get
            {
                return Request.QueryString["patid"];
            }
        }
        public int PrevRasproLineSeqNo
        {
            get
            {
                return Request.QueryString["rlsno"].ToInt();
            }
        }
        public bool PrevConditionIsYes
        {
            get
            {
                return Request.QueryString["rlcon"].ToInt() == 1;
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ElectronicHealthRecord;

            if (!IsPostBack)
            {
                // Header
                var rr = (RegistrationRaspro)Session["rr"];
                var raspro = new Raspro();
                raspro.Query.Where(raspro.Query.SRRaspro == rr.SRRaspro, raspro.Query.SeqNo == PrevRasproLineSeqNo);
                raspro.Query.OrderBy(raspro.Query.SeqNo.Ascending);
                raspro.Query.es.Top = 1;
                raspro.Query.Load();

                hdnRasproLineID.Value = raspro.RasproLineID;

                // Check Selection
                var ras = new RasproActionCollection();
                ras.Query.Where(ras.Query.RasproLineID == raspro.RasproLineID, ras.Query.Condition == (PrevConditionIsYes ? "1" : "0"));
                ras.Query.Load();

                if (ras.Count > 1)
                {
                    tblSelectAction.Visible = true;

                    // Isi dg info prev raspro form 
                    var lastrr = new RegistrationRaspro();
                    lastrr.Query.Where(lastrr.Query.RegistrationNo == rr.RegistrationNo);
                    lastrr.Query.OrderBy(lastrr.Query.SeqNo.Descending);
                    lastrr.Query.es.Top = 1;
                    if (lastrr.Query.Load())
                    {
                        var lastAbLevel = lastrr.AntibioticLevel.ToInt();
                        switch (lastAbLevel)
                        {
                            case AppConstant.AntibioticLevel.AllAntibiotic:
                                lblAction.Text = "Previouse suggestion : All Antibiotic allowed";
                                break;
                            case AppConstant.AntibioticLevel.NoNeedAntibiotic:
                                lblAction.Text = "Previouse suggestion : No need Antibiotic";
                                break;
                            default:
                                if (lastAbLevel > 0 && lastAbLevel < 4)
                                    lblAction.Text = string.Format("Previouse suggestion : Antibiotic Stratification Tipe {0}", "I;II;III".Split(';')[lastAbLevel - 1]);
                                break;
                        }

                    }
                    else
                        lblAction.Visible = false;

                    cboAction.DataTextField = "ActionDescription";
                    cboAction.DataValueField = "ActionNo";

                    cboAction.DataSource = ras;
                    cboAction.DataBind();
                }
                else
                {
                    tblSelectAction.Visible = false;
                    lblAction.Visible = true;
                    lblAction.Text = PrevConditionIsYes ? raspro.YesActionDescription : raspro.NoActionDescription;
                    hdnAbLevel.Value = PrevConditionIsYes ? raspro.YesAction : raspro.NoAction;

                    // Check apakah antibiotic dibatasi
                    if (rr.AbRestrictionID != null)
                    {
                        var abr = new AbRestriction();
                        if (abr.LoadByPrimaryKey(rr.AbRestrictionID))
                        {
                            if (abr.IsNotRestricted ?? false)
                            {
                                hdnAbLevel.Value = AppConstant.AntibioticLevel.AllAntibiotic.ToString();
                                lblAction.Text = "No Antibiotic suggestion for this focus infection, system not restricting antibiotic";
                            }
                            else
                            {
                                var abrs = new AbRestrictionSuggestion();
                                abrs.LoadByPrimaryKey(rr.AbRestrictionID, hdnAbLevel.Value.ToInt());
                                litAntibioticSugest.Text = string.Format("{0}<br/> {1}", AbRestriction.AntibioticSuggestionSymbolInfo(), abrs.SuggestionNote);
                                hdnAbRestrictionID.Value = rr.AbRestrictionID;
                            }
                        }
                    }
                    else
                    {
                        pnlAntibioticSugest.Visible = false;
                    }

                }

                // Flow Chart
                //rasproFlowChart.PopulateFlowChart(rr.SRRaspro, raspro.RasproLineID);
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Footer.Visible = false;
        }

        protected void btnPrev_OnClick(object sender, EventArgs e)
        {
            string url;
            // Check previouse is start
            if (PrevRasproLineSeqNo == 1)
            {
                var rr = (RegistrationRaspro)Session["rr"];
                url = string.Format("RasproForm.aspx?patid={0}&regno={1}&raspro={2}&seqno={3}&ccm=rebind&cet={4}", PatientID, rr.RegistrationNo, rr.SRRaspro, 1, Request.QueryString["cet"]);
            }
            else
                url = string.Format("RasproFormLine.aspx?patid={0}&rlsno={1}&ccm=rebind&cet={2}", PatientID, PrevRasproLineSeqNo - 1, Request.QueryString["cet"]);

            Response.Redirect(url);
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (eventArgument == "save")
            {
                SaveAndClose();
            }
        }
        private void SaveAndClose()
        {
            var rr = (RegistrationRaspro)Session["rr"];

            // Set Sign Value
            if (!string.IsNullOrWhiteSpace(hdnImage.Value))
            {
                var imgHelper = new ImageHelper();
                var resized = imgHelper.ResizeImage(imgHelper.ToImage(hdnImage.Value), new System.Drawing.Size(332, 185));
                rr.SignImage = imgHelper.ToByteArray(resized, ImageFormat.Png);
            }


            var raspro = new Raspro();
            raspro.Query.Where(raspro.Query.SRRaspro == rr.SRRaspro, raspro.Query.SeqNo == PrevRasproLineSeqNo);
            raspro.Query.OrderBy(raspro.Query.SeqNo.Ascending);
            raspro.Query.es.Top = 1;
            raspro.Query.Load();

            if (rr.SeqNo == null || rr.SeqNo == 0)
            {
                var newSeqNo = 1;
                var lastrr = new RegistrationRaspro();
                lastrr.Query.Where(lastrr.Query.RegistrationNo == rr.RegistrationNo);
                lastrr.Query.OrderBy(lastrr.Query.SeqNo.Descending);
                lastrr.Query.es.Top = 1;
                if (lastrr.Query.Load())
                    newSeqNo = (lastrr.SeqNo ?? 0) + 1;

                rr.SeqNo = newSeqNo;
            }

            // abLevel
            if (tblSelectAction.Visible == true)
            {
                var ra = new RasproAction();
                ra.Query.Where(ra.Query.RasproLineID == hdnRasproLineID.Value, ra.Query.Condition == (PrevConditionIsYes ? "1" : "0"), ra.Query.ActionNo == cboAction.SelectedValue);
                ra.Query.Load();

                if (ra.AntibioticLevel >= 1000 || rr.AbRestrictionID == null)
                {
                    var lastrr = new RegistrationRaspro();
                    lastrr.Query.Where(lastrr.Query.RegistrationNo == rr.RegistrationNo,
                        lastrr.Query.AntibioticLevel > 0,
                        lastrr.Query.AntibioticLevel < 5);
                    lastrr.Query.OrderBy(lastrr.Query.SeqNo.Descending);
                    lastrr.Query.es.Top = 1;
                    if (lastrr.Query.Load())
                    {
                        rr.PrevAntibioticLevel = lastrr.AntibioticLevel;
                        rr.PrevAbRestrictionID = lastrr.AbRestrictionID;

                        switch (ra.AntibioticLevel)
                        {
                            case AppConstant.AntibioticLevel.StepUp: // Eskalasi antibiotik ke Stratifikasi yg lebih tinggi
                                if ((lastrr.AntibioticLevel ?? 0) < 3) // Max ab level = 3
                                {
                                    hdnAbLevel.Value = ((lastrr.AntibioticLevel ?? 0) + 1).ToString();
                                }
                                else
                                    hdnAbLevel.Value = lastrr.AntibioticLevel.ToString();
                                break;
                            case AppConstant.AntibioticLevel.StepDown: // Step Down antibiotik ke stratifikasi yg lebih rendah
                                if ((lastrr.AntibioticLevel ?? 0) > 1)
                                {
                                    hdnAbLevel.Value = (lastrr.AntibioticLevel - 1).ToString();
                                }
                                else
                                    hdnAbLevel.Value = lastrr.AntibioticLevel.ToString();
                                break;
                            case AppConstant.AntibioticLevel.AddAntibiotic: // Tambahkan AB sesuai panduan
                            case AppConstant.AntibioticLevel.SwitchIvToOral:
                                hdnAbLevel.Value = ((lastrr.AntibioticLevel ?? 0)).ToString();
                                break;
                            case AppConstant.AntibioticLevel.NoNeedAntibiotic:
                                hdnAbLevel.Value = AppConstant.AntibioticLevel.NoNeedAntibiotic.ToString();
                                break;
                            default:
                                hdnAbLevel.Value = ra.AntibioticLevel.ToString();
                                break;
                        }
                    }
                }
                else
                {
                    hdnAbLevel.Value = ra.AntibioticLevel.ToString();
                }

                rr.ActionNo = cboAction.SelectedValue.ToInt();
            }

            rr.AntibioticLevel = hdnAbLevel.Value.ToInt();

            var rrlcoll = (RegistrationRasproLineCollection)Session["rrlcoll"];

            // Delete line yg tidak diperlukan
            var isRunDelete = false;
            foreach (RegistrationRasproLine line in rrlcoll)
            {
                if (isRunDelete)
                    line.MarkAsDeleted();
                else
                {
                    line.SeqNo = rr.SeqNo;

                    if (line.RasproLineID == raspro.RasproLineID)
                    {
                        // Tandai mulai menghapus
                        isRunDelete = true;
                        line.AntibioticLevel = hdnAbLevel.Value.ToInt();
                    }
                    else
                    {
                        line.str.AntibioticLevel = string.Empty;
                    }
                }
            }

            rr.Save();
            rrlcoll.Save();


            // Form RASLAN digunakan untuk MENGGANTI AB SEBELUMNYA
            // ---------------------------------------------------
            if (rr.SRRaspro == AppConstant.RasproType.Raslan)
            {
                StopPreviouseAntibiotic(rr);
            }

            // Utk refresh di PrescriptionEntry
            Session["RasproSeqNo"] = rr.SeqNo;
            Session["SRRaspro"] = rr.SRRaspro;

            var script = "<script type='text/javascript'>CloseAndApply();</script>";
            //Create Startup Javascript for close window              
            Page.ClientScript.RegisterStartupScript(this.GetType(), "closeMe", script);
        }

        private void StopPreviouseAntibiotic(RegistrationRaspro newRaspro)
        {
            // Stop all AB in prev Raspro except item raspraja
            // ------------------------------------------------------
            var rasproItems = new RegistrationRasproItemCollection();
            var qrri = new RegistrationRasproItemQuery("rri");
            var qrr = new RegistrationRasproQuery("rr");
            qrri.InnerJoin(qrr).On(qrri.RegistrationNo == qrr.RegistrationNo & qrri.RasproSeqNo == qrr.SeqNo);
            qrri.Where(qrri.RegistrationNo == newRaspro.RegistrationNo, qrri.RasproSeqNo < newRaspro.SeqNo);

            qrri.Select(qrri);
            rasproItems.Load(qrri);
            foreach (var itemPrev in rasproItems)
            {
                if (itemPrev.StopDateTime == null)
                    itemPrev.StopDateTime = DateTime.Now;

            }
            rasproItems.Save();

            // Stop all AB in prev UDD Item (RASAL, RASLAN, & RASPATUR)
            // ------------------------------------------------------
            var uddItems = new UddItemCollection();
            var qrUdd = new UddItemQuery("ui");
            var qrIpm = new ItemProductMedicQuery("ipm");
            qrUdd.InnerJoin(qrIpm).On(qrUdd.ItemID == qrIpm.ItemID);
            qrUdd.Where(qrIpm.IsAntibiotic == true);

            qrUdd.Where(qrUdd.RegistrationNo == newRaspro.RegistrationNo, qrUdd.RasproSeqNo < newRaspro.SeqNo);

            uddItems.Load(qrUdd);
            foreach (var uddItem in uddItems)
            {
                if (uddItem.StopDateTime == null)
                {
                    if (!string.IsNullOrEmpty(uddItem.ParentNo)) // Racikan
                    {
                        // Stop status di header nya
                        var uddCompound = new UddItem();
                        uddCompound.LoadByPrimaryKey(uddItem.RegistrationNo, uddItem.LocationID, uddItem.ParentNo);
                        uddCompound.IsStop = true;
                        uddCompound.StopDateTime = DateTime.Now;
                        uddCompound.Save();
                    }

                    uddItem.IsStop = true;
                    uddItem.StopDateTime = DateTime.Now;
                }
            }
            uddItems.Save();
        }
    }
}
