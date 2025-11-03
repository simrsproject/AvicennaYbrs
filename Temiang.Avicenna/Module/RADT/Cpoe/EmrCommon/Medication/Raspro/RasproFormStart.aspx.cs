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


namespace Temiang.Avicenna.Module.RADT.Emr
{
    [Obsolete("Page ini sudah tidak dipakai lagi krn sudah digabung ke ReasproForm.aspx (Sudah boleh dihapus)", true)]
    public partial class RasproFormStart : BasePageDialog
    {
        public string PatientID
        {
            get
            {
                return Request.QueryString["patid"];
            }
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ElectronicHealthRecord;
            if (!IsPostBack)
            {
                var inf = new AbRestrictionQuery();
                inf.Select(inf.AbRestrictionID, inf.ParentID, inf.AbRestrictionName);
                inf.Where(inf.SRAbRestrictionType == "INF");
                inf.OrderBy(inf.AbRestrictionName.Ascending);
                var dtbInf = inf.LoadDataTable();
                var newRow = dtbInf.NewRow();
                newRow["AbRestrictionID"] = string.Empty;
                newRow["AbRestrictionName"] = "No Infection";
                dtbInf.Rows.InsertAt(newRow, 0);
                cboAbRestrictionID.DataSource = dtbInf;
                cboAbRestrictionID.DataBind();



                // First Line
                var rr = (RegistrationRaspro)Session["rr"];
                var raspro = new Raspro();
                raspro.Query.Where(raspro.Query.SRRaspro == rr.SRRaspro);
                raspro.Query.OrderBy(raspro.Query.SeqNo.Ascending);
                raspro.Query.es.Top = 1;
                raspro.Query.Load();
                lblSpecification.Text = string.Format("{0}. {1}", raspro.SeqNo, raspro.Spesification);

                hdnRasproLineID.Value = raspro.RasproLineID;
                hdnRasproLineSeqNo.Value = raspro.SeqNo.ToString();


            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Footer.Visible = false;
        }


        protected void btnNext_Click(object sender, EventArgs e)
        {
            // Header
            var rr = (RegistrationRaspro)Session["rr"];
            if (string.IsNullOrWhiteSpace(cboAbRestrictionID.SelectedValue))
                rr.str.AbRestrictionID = string.Empty;
            else
                rr.AbRestrictionID = cboAbRestrictionID.SelectedValue;

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

            var condition = string.IsNullOrWhiteSpace(cboAbRestrictionID.SelectedValue) ? "0" : "1";
            rrl.Condition = condition;

            Session["rrlcoll"] = rrlcoll;

            // Redirect
            var url = string.Format("RasproFormEnd.aspx?patid={0}&rlsno={1}}&rlcon={3}&ccm=rebind&cet={2}", PatientID, hdnRasproLineSeqNo.Value, Request.QueryString["cet"], condition);

            if ((condition == "1" && raspro.YesAction == "NEXT") || (condition == "0" && raspro.NoAction == "NEXT"))
            {
                // Check apakah antibiotic dibatasi
                var abr = new AbRestriction();
                abr.LoadByPrimaryKey(rr.AbRestrictionID);
                if (!(abr.IsNotRestricted ?? false))
                    url = string.Format("RasproFormLine.aspx?patid={0}&rlsno={1}&ccm=rebind&cet={2}", PatientID, hdnRasproLineSeqNo.Value, Request.QueryString["cet"]);
            }

            Response.Redirect(url);
        }

        protected void btnPrev_OnClick(object sender, EventArgs e)
        {
            var rr = (RegistrationRaspro)Session["rr"];
            var url = string.Format("RasproForm.aspx?patid={0}&regno={1}&raspro={2}&seqno={3}&ccm=rebind&cet={4}", PatientID, rr.RegistrationNo, rr.SRRaspro, rr.SeqNo, Request.QueryString["cet"]);
            Response.Redirect(url);
        }
    }
}
