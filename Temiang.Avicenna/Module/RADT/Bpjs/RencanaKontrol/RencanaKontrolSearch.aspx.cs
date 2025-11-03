using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Bpjs
{
    public partial class RencanaKontrolSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.BpjsRencanaKontrol;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) RestoreValueFromCookie();
        }

        public override bool OnButtonOkClicked()
        {
            var parameters = new List<string>();
            parameters.Add(txtNoSRB.Text);
            parameters.Add(txtTglAwal.IsEmpty ? string.Empty : txtTglAwal.SelectedDate.Value.ToString("MM/dd/yyyy"));
            parameters.Add(txtTglAwal.IsEmpty ? string.Empty : (txtTglAkhir.IsEmpty ? string.Empty : txtTglAkhir.SelectedDate.Value.ToString("MM/dd/yyyy")));
            parameters.Add(txtNoPeserta.Text);
            parameters.Add(cboFilter.SelectedValue);

            Session[SessionNameForQuery] = parameters;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }
    }
}