using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Bpjs
{
    public partial class RujukBalikSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.BpjsRujukBalik;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) RestoreValueFromCookie();
        }

        public override bool OnButtonOkClicked()
        {
            var parameters = new List<string>
            {
                txtNoSRB.Text,
                txtNoSep.Text,
                txtTglAwal.IsEmpty ? string.Empty : txtTglAwal.SelectedDate.Value.ToString("MM/dd/yyyy"),
                txtTglAwal.IsEmpty ? string.Empty : (txtTglAkhir.IsEmpty ? string.Empty : txtTglAkhir.SelectedDate.Value.ToString("MM/dd/yyyy"))
            };

            Session[SessionNameForQuery] = parameters;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }
    }
}