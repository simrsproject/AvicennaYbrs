using System;
using System.Linq;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI.WebControls;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.RADT
{
    public partial class PatientNotesDialog : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            (Helper.FindControlRecursive(this, "btnOk") as Button).Visible = false;
            (Helper.FindControlRecursive(this, "btnCancel") as Button).Visible = false;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
            {
                var patient = new Patient();
                patient.LoadByPrimaryKey(Request.QueryString["patId"]);
                txtNotes.Text = patient.Notes;
            }
        }
    }
}
