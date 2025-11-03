using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.BusinessObject;
using Telerik.Web.UI;
using System.Text;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Emr
{
    public partial class RasproHeader : System.Web.UI.UserControl
    {
        public string PatientID
        {
            get
            {
                return Request.QueryString["patid"];
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var rr = (RegistrationRaspro)Session["rr"];
                
                var stdi = new AppStandardReferenceItem();
                stdi.LoadByPrimaryKey(AppEnum.StandardReference.RASPRO.ToString(), rr.SRRaspro);
                this.Page.Title = stdi.ItemName;

                if (rr.SRRaspro.Equals("RASLAN") || rr.SRRaspro.Equals("RASPRAJA"))
                {
                    // Tambah info ke berapa
                    var rrq = new RegistrationRasproQuery("rr");
                    rrq.Where(rrq.RegistrationNo == rr.RegistrationNo, rrq.SRRaspro == rr.SRRaspro);
                    rrq.Select(rrq.RegistrationNo.Count().As("RasproCount"));
                    var dtbCount = rrq.LoadDataTable();
                    if (dtbCount.Rows.Count>0)
                        this.Page.Title = String.Format("{0} #{1}", stdi.ItemName, dtbCount.Rows[0][0].ToInt() + ("new".Equals( Request.QueryString["mod"])? 1 :0));
                }

                if (!string.IsNullOrWhiteSpace(stdi.Note))
                lblRasproNote.Text = string.Format("<fieldset style=\"background-color: lightyellow;\">{0}</fieldset>", stdi.Note);

                txtRasproDateTime.SelectedDate = rr.RasproDateTime;

                var par = new Paramedic();
                par.LoadByPrimaryKey(rr.ParamedicID);
                txtParamedicName.Text = par.ParamedicName;

                var su = new ServiceUnit();
                su.LoadByPrimaryKey(rr.ServiceUnitID);
                txtServiceUnitName.Text = su.ServiceUnitName;

                if (!string.IsNullOrWhiteSpace(rr.AdviseByParamedicID))
                {
                    par = new Paramedic();
                    par.LoadByPrimaryKey(rr.AdviseByParamedicID);
                    txtAdviseByParamedicName.Text = par.ParamedicName;
                }

                var pat = new Patient();
                if (pat.LoadByPrimaryKey(PatientID))
                {
                    txtRegistrationNo.Text = rr.RegistrationNo;
                    txtPatientName.Text = pat.PatientName;
                    txtMedicalNo.Text = pat.MedicalNo;
                    txtDateOfBirth.SelectedDate = pat.DateOfBirth;
                }

                if (!string.IsNullOrWhiteSpace(rr.AbRestrictionID))
                {
                    var inf = new AbRestriction();
                    inf.LoadByPrimaryKey(rr.AbRestrictionID);
                    txtAbRestrictionName.Text = inf.AbRestrictionName;
                }
            }
        }
    }
}