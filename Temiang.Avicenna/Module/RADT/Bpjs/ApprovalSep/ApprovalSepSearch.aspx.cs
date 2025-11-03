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
    public partial class ApprovalSepSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.BpjsApproval;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
                RestoreValueFromCookie();
        }

        public override bool OnButtonOkClicked()
        {
            //var query = new BpjsApprovalQuery("a");
            //var std = new AppStandardReferenceItemQuery("b");

            //query.InnerJoin(std).On(std.StandardReferenceID == AppEnum.StandardReference.BpjsTypeOfService.ToString() && std.ItemID == query.JnsPelayanan);
            //query.Select(
            //    query.NoKartu,
            //    query.TglSep,
            //    query.Keterangan,
            //    query.JnsPelayanan,
            //    std.ItemName.As("TypeOfService"),
            //    "<a.NamaPasien + ' (' + a.JenisKelamin + ')' AS NamaPasienJK>",
            //    query.IsApproved
            //    );
            //query.OrderBy(query.NoKartu.Descending, query.TglSep.Descending);
            //query.es.Top = AppSession.Parameter.MaxResultRecord;

            var query = new BpjsApprovalQuery("a");
            var std = new AppStandardReferenceItemQuery("b");
            var std2 = new AppStandardReferenceItemQuery("c");

            query.InnerJoin(std).On(std.StandardReferenceID == AppEnum.StandardReference.BpjsTypeOfService.ToString() && std.ItemID == query.JnsPelayanan);
            query.InnerJoin(std2).On(std2.StandardReferenceID == AppEnum.StandardReference.BpjsTypeOfApproval.ToString() && std2.ItemID == query.JnsPengajuan);

            query.Select(
                query.NoKartu,
                query.TglSep,
                query.Keterangan,
                query.JnsPelayanan,
                std.ItemName.As("TypeOfService"),
                query.JnsPengajuan,
                std2.ItemName.As("TypeOfApproval"),
                "<a.NamaPasien + ' (' + a.JenisKelamin + ')' AS NamaPasienJK>",
                query.IsApproved
                );
            query.Where(query.TglSep == DateTime.Now.Date);
            query.OrderBy(query.NoKartu.Descending, query.TglSep.Descending);

            if (!string.IsNullOrWhiteSpace(txtNoPeserta.Text)) query.Where(query.NoKartu == txtNoPeserta.Text);
            if (!txtTglSep.IsEmpty) query.Where(query.TglSep == txtTglSep.SelectedDate.Value.Date);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }
    }
}