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
    public partial class RasproFormView : BasePageDialog
    {
        public string PatientID
        {
            get
            {
                return Request.QueryString["patid"];
            }
        }
        public int RasproSeqNo
        {
            get
            {
                return Request.QueryString["rseqno"].ToInt();
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ElectronicHealthRecord;

            if (!IsPostBack)
            {
                var rr = new RegistrationRaspro();
                if (rr.LoadByPrimaryKey(RegistrationNo, RasproSeqNo))
                {
                    var usedRasproSeqno = 0;
                    litAntibioticSuggest.Text = AbRestriction.AntibioticSuggestion(rr, ref usedRasproSeqno);
                }
                else
                    litAntibioticSuggest.Text = String.Empty;

                Session["rr"] = rr; // Untuk header

                // History input
                var rrlCol = new RegistrationRasproLineCollection();
                rrlCol.Query.Where(rrlCol.Query.RegistrationNo == RegistrationNo, rrlCol.Query.SeqNo == rr.SeqNo);
                rrlCol.LoadAll();
                litRasproLine.Text = RasproForm.PreviouseSpecificationHtml(rr, rrlCol, rrlCol.Count);


                // Flow Chart
                //rasproFlowChart.PopulateFlowChart(rr.SRRaspro, raspro.RasproLineID);

                ButtonOk.Visible = false;
                ButtonCancel.Text = "Close";

            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }



    }
}
