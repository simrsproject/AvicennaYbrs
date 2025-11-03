using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
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
    /// Project : RSBK (2020)
    /// Modification History:
    /// 
    /// </remarks>
    public partial class RasproFormLine : BasePageDialog
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
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ElectronicHealthRecord;

            if (!IsPostBack)
            {
                var rr = (RegistrationRaspro)Session["rr"];
                // For Current Line
                var raspro = new Raspro();
                raspro.Query.Where(raspro.Query.SRRaspro == rr.SRRaspro, raspro.Query.SeqNo > PrevRasproLineSeqNo);
                raspro.Query.OrderBy(raspro.Query.SeqNo.Ascending);
                raspro.Query.es.Top = 1;
                raspro.Query.Load();

                lblSpecification.Text = string.Format("{0}. {1}", raspro.SeqNo, raspro.Spesification);

                hdnRasproLineID.Value = raspro.RasproLineID;
                hdnRasproLineSeqNo.Value = raspro.SeqNo.ToString();

                RegistrationRasproLine rrl = null;
                var rrlcoll = (RegistrationRasproLineCollection)Session["rrlcoll"];
                foreach (RegistrationRasproLine line in rrlcoll)
                {
                    if (line.RasproLineID == hdnRasproLineID.Value)
                    {
                        rrl = line;
                        break;
                    }
                }

                if (rrl != null)
                    optConddition.SelectedValue = rrl.Condition;

                btnNext.Enabled = !string.IsNullOrEmpty(optConddition.SelectedValue);

            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Footer.Visible = false;
        }


        protected void btnNext_Click(object sender, EventArgs e)
        {
            ApplyValueAndMoveToNextQuestion();
        }

        protected void btnPrev_OnClick(object sender, EventArgs e)
        {
            string url;
            // Check previouse is start
            if (PrevRasproLineSeqNo == 1)
            {
                var rr = (RegistrationRaspro)Session["rr"];
                url = string.Format("RasproForm.aspx?patid={0}&regno={1}&raspro={2}&seqno={3}&ccm=rebind&cet={4}", PatientID, rr.RegistrationNo, rr.SRRaspro, rr.SeqNo, Request.QueryString["cet"]);
            }
            else
                url = string.Format("RasproFormLine.aspx?patid={0}&rlsno={1}&ccm=rebind&cet={2}", PatientID, PrevRasproLineSeqNo - 1, Request.QueryString["cet"]);

            Response.Redirect(url);
        }

        protected void optConddition_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyValueAndMoveToNextQuestion();
        }
        private void ApplyValueAndMoveToNextQuestion()
        {
            var rr = (RegistrationRaspro)Session["rr"];

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

            var condition = optConddition.SelectedValue;
            rrl.Condition = condition;
            Session["rrlcoll"] = rrlcoll;

            // Redirect
            var url = string.Empty;
            if ((condition == "1" && raspro.YesAction == "NEXT") || (condition == "0" && raspro.NoAction == "NEXT"))
            {
                url = string.Format("RasproFormLine.aspx?patid={0}&rlsno={1}&ccm=rebind&cet={2}", PatientID, hdnRasproLineSeqNo.Value, Request.QueryString["cet"]);
            }
            else
            {
                url = string.Format("RasproFormEnd.aspx?patid={0}&rlsno={1}&rlcon={3}&ccm=rebind&cet={2}", PatientID, hdnRasproLineSeqNo.Value, Request.QueryString["cet"], condition);
            }

            Response.Redirect(url);
        }
    }
}
