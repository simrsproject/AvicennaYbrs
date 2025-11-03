using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Bpjs
{
    public partial class SisruteSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.BpjsSep;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) RestoreValueFromCookie();
        }

        public override bool OnButtonOkClicked()
        {
            var list = new string[2];
            list.SetValue(string.IsNullOrEmpty(txtNoRujukan.Text) ? null : txtNoRujukan.Text, 0);
            list.SetValue(txtTanggalSep.IsEmpty ? null : txtTanggalSep.SelectedDate.Value.ToString("yyyy-MM-dd"), 1);

            if (list.Any(l => !string.IsNullOrEmpty(l))) Session[SessionNameForQuery] = list;
            else Session.Remove(SessionNameForQuery);
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }
    }
}
