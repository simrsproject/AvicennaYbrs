using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Bpjs
{
    public partial class RujukanSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.BpjsRujukan;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
                RestoreValueFromCookie();
        }

        public override bool OnButtonOkClicked()
        {
            var query = new BpjsRujukanQuery("a");
            var std = new AppStandardReferenceItemQuery("b");
            var diag = new DiagnoseQuery("c");
            var reg = new BpjsSEPQuery("e");

            query.InnerJoin(std).On(std.StandardReferenceID == AppEnum.StandardReference.BpjsTypeOfService.ToString() && std.ItemID == query.JnsPelayanan);
            query.InnerJoin(diag).On(query.DiagRujukan == diag.DiagnoseID);
            query.InnerJoin(reg).On(query.NoSep == reg.NoSEP);
            query.Select(
                query,
                std.ItemName.As("TypeOfService"),
                diag.DiagnoseName,
                reg.NomorKartu,
                "<e.NamaPasien + ' (' + e.JenisKelamin + ')' AS NamaPasienJK>"
                );
            query.es.Top = AppSession.Parameter.MaxResultRecord;

            if (!string.IsNullOrEmpty(txtNoSep.Text)) query.Where(query.NoSep == txtNoSep.Text);
            if (!string.IsNullOrEmpty(txtNoRujukan.Text)) query.Where(query.NoRujukan == txtNoRujukan.Text);
            if (!txtTanggalRujukan.IsEmpty) query.Where(query.TglRujukan == txtTanggalRujukan.SelectedDate.Value.Date);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }
    }
}